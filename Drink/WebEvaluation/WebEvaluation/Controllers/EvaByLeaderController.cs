using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebEvaluation.Models;
using WebEvaluation.DAL;
using PagedList;
using WebEvaluation.ViewModels;
using WebEvaluation.Utils;
using WebEvaluation.Controllers.Filters;
using WebEvaluation.DataModels;
using System.Data.Entity.Validation;
using WebEvaluation.Common;
using SafeNeeds.DySmat.Logic;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Util;

namespace WebEvaluation.Controllers
{
    [AuthenticationFilter(Order = 1)]
    [ExceptionFilter(Order = 2)]
    [WebAuthorizeAttribute(Roles = "02,04,09")]
    public class EvaByLeaderController : Controller
    {
        private EvaluationContext db = new EvaluationContext();

        /*上長評価入力-----2014-07-04-----李*/
        public ActionResult EvaByLeaderIndex(string ShopCD, string PartyDateFrom, string PartyDateTo, string StaffEva, string LeaderEva, string isPostBack, string Range, string divisionCD, string StaffCD, int? page, string msg)
        {
            ViewBag.msg = msg;

            ViewBag.Range = Range;
            if (Range == null)
            {
                ViewBag.Range = "division";
            }

            if (PartyDateFrom == null)
            {
                DateTime today = DateTime.Now;
                PartyDateFrom = string.Format("{0:yyyy/MM}", today) + "/01";
                ViewBag.PartyDateFrom = PartyDateFrom;
            }
            else if (PartyDateFrom == " ")
            {
                PartyDateFrom = "";
            }
            if (isPostBack != null && isPostBack == "1")
            {
                ViewBag.isPostBack = "1";

                var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start
                var staffCD_Change = "";

                if (ShopCD == null)
                {
                    shopCD_Change = "";
                }
                else
                {
                    shopCD_Change = ShopCD.Trim().ToUpper();
                }

                if (StaffCD == null)
                {
                    staffCD_Change = "";
                }
                else
                {
                    staffCD_Change = StaffCD.Trim();
                }                       //---2014/09/10---LI---店舗番号と担当者番号を格式化---end

                int pageSize = 20;
                int pageNumber = (page ?? 1);

                int totalItemCount = 0;

                var models = GetModels(shopCD_Change, PartyDateFrom, PartyDateTo, StaffEva, LeaderEva, Range, divisionCD, staffCD_Change,pageSize, pageNumber, true, ref totalItemCount);

                if (models.ToList().Count == 0)
                {
                    ViewBag.msg = "データがありません。";
                    ViewBag.msgType = "info";
                }

                DyPagedList<EvaByleaderIndexViewModel> lit = new DyPagedList<EvaByleaderIndexViewModel>(models, pageNumber, pageSize, totalItemCount);


                GC.Collect();

                return View(lit);
            }
            else
            {
                ViewBag.isPostBack = "1";
                return View();
            }
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns></returns>
        public ActionResult csv(string ShopCD, string PartyDateFrom, string PartyDateTo, string StaffEva, string LeaderEva, string Range, string divisionCD, string StaffCD)
        {
            if (PartyDateFrom == " ")
            {
                PartyDateFrom = "";
            }

            int totalItemCount = 0;
            List<EvaByleaderIndexViewModel> models = GetModels(ShopCD, PartyDateFrom, PartyDateTo, StaffEva, LeaderEva, Range, divisionCD, StaffCD,2000, 0, false, ref totalItemCount).ToList();

            if (models.Count == 0)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\上長評価パーティ_{0}.csv", date);

            CsvUtils.ModlesToCsv<EvaByleaderIndexViewModel>(basePath + fileName, models);

            return Json(new { Path = string.Format("/CSV/上長評価パーティ_{0}.csv", date), ResultType = EnumResultType.Success });
        }

        //全データ取得
        private IEnumerable<EvaByleaderIndexViewModel> GetModels_old(string ShopCD, string PartyDateFrom, string PartyDateTo, string StaffEva, string LeaderEva, string Range, string divisionCD, string StaffCD)
        {
            ViewBag.ShopCD = ShopCD;
            ViewBag.PartyDateFrom = PartyDateFrom;
            ViewBag.PartyDateTo = PartyDateTo;
            ViewBag.StaffEva = StaffEva;
            ViewBag.LeaderEva = LeaderEva;

            if (ViewBag.PartyDateFrom == "")
            {
                ViewBag.PartyDateFrom = " ";
            }

            ViewBag.Range = Range;
            ViewBag.divisionCD = divisionCD;
            ViewBag.StaffCD = StaffCD;

            List<T_Party> Partys = new List<T_Party>();
            List<M_Shop> Shops = new List<M_Shop>();
            List<T_EvaByLeader> EvaByLeaders = new List<T_EvaByLeader>();
            List<T_EvaByStaff> StaffEvas = new List<T_EvaByStaff>();
            List<M_Staff> Staffs = new List<M_Staff>();
            //var Reports = db.Reports.ToList();
            List<M_Group> groups = new List<M_Group>();


            #region  where

            //date
            if (String.IsNullOrEmpty(PartyDateTo))
            {
                PartyDateTo = "9999/12/01";
            }
            if (String.IsNullOrEmpty(PartyDateFrom))
            {
                PartyDateFrom = "1900/01/01";
            }
            DateTime dateFrom = Convert.ToDateTime(PartyDateFrom);
            DateTime dateTo = Convert.ToDateTime(PartyDateTo).AddHours(23).AddMinutes(59).AddSeconds(59);

            bool noCondition = true;

            if (Range == "division")
            {
                if (!string.IsNullOrEmpty(divisionCD))
                {
                    if (divisionCD.StartsWith("g_"))
                    {
                        divisionCD = divisionCD.Replace("g_", "");

                        //
                        var partyQuery = from party in db.Partys
                                         join shop in db.Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                                         from p_shop in g_party_shop
                                         join staff in db.Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                                         from p_staff in g_party_staff
                                         join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                         from s_group in g_shop_group
                                         where party.PartyDate.CompareTo(dateFrom) >= 0
                                                && party.PartyDate.CompareTo(dateTo) <= 0
                                                && s_group.GroupCD == divisionCD
                                         select party;
                        Partys = partyQuery.ToList();

                        //StaffEva
                        StaffEvas = (from staffEva in db.EvaByStaffs

                                     join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
                                     from p_party in g_party_party
                                     join shop in db.Shops on p_party.ShopCD equals shop.ShopCD into g_party_shop
                                     from p_shop in g_party_shop
                                     join staff in db.Staffs on p_party.TantoCD equals staff.StaffCD into g_party_staff
                                     from p_staff in g_party_staff
                                     join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                     from s_group in g_shop_group

                                     where (p_party.PartyDate.CompareTo(dateFrom) >= 0
                                 && p_party.PartyDate.CompareTo(dateTo) <= 0
                                 && s_group.GroupCD == divisionCD)

                                     select staffEva).ToList();

                        //LeaderEva
                        EvaByLeaders = (from leaderEva in db.EvaByLeaders

                                        join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
                                        from p_party in g_party_party

                                        join shop in db.Shops on p_party.ShopCD equals shop.ShopCD into g_party_shop
                                        from p_shop in g_party_shop
                                        join staff in db.Staffs on p_party.TantoCD equals staff.StaffCD into g_party_staff
                                        from p_staff in g_party_staff
                                        join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                        from s_group in g_shop_group

                                        where (p_party.PartyDate.CompareTo(dateFrom) >= 0
                                    && p_party.PartyDate.CompareTo(dateTo) <= 0
                                    && s_group.GroupCD == divisionCD)

                                        select leaderEva).ToList();

                        Shops = db.Shops.Where(s => s.GroupCD == divisionCD).ToList();
                        divisionCD = "g_" + divisionCD;

                        noCondition = false;
                    }
                    else
                    {
                        //
                        var partyQuery = from party in db.Partys
                                         join shop in db.Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                                         from p_shop in g_party_shop
                                         join staff in db.Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                                         from p_staff in g_party_staff
                                         join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                         from s_group in g_shop_group
                                         where party.PartyDate.CompareTo(dateFrom) >= 0
                                                && party.PartyDate.CompareTo(dateTo) <= 0
                                                && s_group.DivCD == divisionCD
                                         select party;
                        Partys = partyQuery.ToList();

                        //StaffEva
                        StaffEvas = (from staffEva in db.EvaByStaffs

                                     join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
                                     from p_party in g_party_party

                                     join shop in db.Shops on p_party.ShopCD equals shop.ShopCD into g_party_shop
                                     from p_shop in g_party_shop
                                     join staff in db.Staffs on p_party.TantoCD equals staff.StaffCD into g_party_staff
                                     from p_staff in g_party_staff
                                     join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                     from s_group in g_shop_group

                                     where (
                                     p_party.PartyDate.CompareTo(dateFrom) >= 0
                                     && p_party.PartyDate.CompareTo(dateTo) <= 0
                                     && s_group.DivCD == divisionCD
                                     )

                                     select staffEva).ToList();

                        //LeaderEva
                        EvaByLeaders = (from leaderEva in db.EvaByLeaders

                                        join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
                                        from p_party in g_party_party

                                        join shop in db.Shops on p_party.ShopCD equals shop.ShopCD into g_party_shop
                                        from p_shop in g_party_shop
                                        join staff in db.Staffs on p_party.TantoCD equals staff.StaffCD into g_party_staff
                                        from p_staff in g_party_staff
                                        join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                        from s_group in g_shop_group

                                        where (
                                        p_party.PartyDate.CompareTo(dateFrom) >= 0
                                        && p_party.PartyDate.CompareTo(dateTo) <= 0
                                        && s_group.DivCD == divisionCD
                                        )

                                        select leaderEva).ToList();

                        groups = db.Groups.Where(s => s.DivCD == divisionCD).ToList();

                        noCondition = false;
                    }
                }
            }
            else if (Range == "shop")
            {
                if (!string.IsNullOrEmpty(ShopCD))
                {
                    //
                    var partyQuery = from party in db.Partys
                                     join shop in db.Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                                     from p_shop in g_party_shop
                                     join staff in db.Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                                     from p_staff in g_party_staff
                                     join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                     from s_group in g_shop_group
                                     where party.PartyDate.CompareTo(dateFrom) >= 0
                                            && party.PartyDate.CompareTo(dateTo) <= 0
                                            && p_shop.ShopCD == ShopCD
                                     select party;
                    Partys = partyQuery.ToList();


                    //StaffEva
                    StaffEvas = (from staffEva in db.EvaByStaffs

                                 join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
                                 from p_party in g_party_party

                                 join shop in db.Shops on p_party.ShopCD equals shop.ShopCD into g_party_shop
                                 from p_shop in g_party_shop
                                 join staff in db.Staffs on p_party.TantoCD equals staff.StaffCD into g_party_staff
                                 from p_staff in g_party_staff
                                 join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                 from s_group in g_shop_group

                                 where (
                                 p_party.PartyDate.CompareTo(dateFrom) >= 0
                                 && p_party.PartyDate.CompareTo(dateTo) <= 0
                                && p_shop.ShopCD == ShopCD
                                 )

                                 select staffEva).ToList();

                    //LeaderEva
                    EvaByLeaders = (from leaderEva in db.EvaByLeaders

                                    join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
                                    from p_party in g_party_party

                                    join shop in db.Shops on p_party.ShopCD equals shop.ShopCD into g_party_shop
                                    from p_shop in g_party_shop
                                    join staff in db.Staffs on p_party.TantoCD equals staff.StaffCD into g_party_staff
                                    from p_staff in g_party_staff
                                    join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                    from s_group in g_shop_group

                                    where (
                                    p_party.PartyDate.CompareTo(dateFrom) >= 0
                                    && p_party.PartyDate.CompareTo(dateTo) <= 0
                                    && p_shop.ShopCD == ShopCD
                                    )

                                    select leaderEva).ToList();

                    Shops = db.Shops.Where(p => p.ShopCD == ShopCD).ToList();

                    noCondition = false;
                }
            }
            else if (Range == "tanto")
            {
                if (!string.IsNullOrEmpty(StaffCD))
                {
                    //
                    var partyQuery = from party in db.Partys
                                     join shop in db.Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                                     from p_shop in g_party_shop
                                     join staff in db.Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                                     from p_staff in g_party_staff
                                     join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                     from s_group in g_shop_group
                                     where party.PartyDate.CompareTo(dateFrom) >= 0
                                            && party.PartyDate.CompareTo(dateTo) <= 0
                                            && p_staff.StaffCD == StaffCD
                                     select party;
                    Partys = partyQuery.ToList();

                    //StaffEva
                    StaffEvas = (from staffEva in db.EvaByStaffs

                                 join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
                                 from p_party in g_party_party

                                 join shop in db.Shops on p_party.ShopCD equals shop.ShopCD into g_party_shop
                                 from p_shop in g_party_shop
                                 join staff in db.Staffs on p_party.TantoCD equals staff.StaffCD into g_party_staff
                                 from p_staff in g_party_staff
                                 join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                 from s_group in g_shop_group

                                 where (
                                 p_party.PartyDate.CompareTo(dateFrom) >= 0
                                 && p_party.PartyDate.CompareTo(dateTo) <= 0
                                && p_staff.StaffCD == StaffCD
                                 )

                                 select staffEva).ToList();

                    //LeaderEva
                    EvaByLeaders = (from leaderEva in db.EvaByLeaders

                                    join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
                                    from p_party in g_party_party

                                    join shop in db.Shops on p_party.ShopCD equals shop.ShopCD into g_party_shop
                                    from p_shop in g_party_shop
                                    join staff in db.Staffs on p_party.TantoCD equals staff.StaffCD into g_party_staff
                                    from p_staff in g_party_staff
                                    join grp in db.Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                                    from s_group in g_shop_group

                                    where (
                                    p_party.PartyDate.CompareTo(dateFrom) >= 0
                                    && p_party.PartyDate.CompareTo(dateTo) <= 0
                                    && p_staff.StaffCD == StaffCD
                                    )

                                    select leaderEva).ToList();

                    Staffs = db.Staffs.Where(p => p.StaffCD == StaffCD).ToList();

                    noCondition = false;
                }
            }

            //Partys = db.Partys.Where(p => p.PartyDate.CompareTo(dateFrom) >= 0 && p.PartyDate.CompareTo(dateTo) <= 0).ToList();
            if (Partys.Count == 0 && noCondition) Partys = db.Partys.Where(p => p.PartyDate.CompareTo(dateFrom) >= 0 && p.PartyDate.CompareTo(dateTo) <= 0).ToList();

            //StaffEva
            if (StaffEvas.Count == 0 && noCondition)
            {
                StaffEvas = (from staffEva in db.EvaByStaffs

                             join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
                             from p_party in g_party_party

                             where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

                             select staffEva).ToList();
            }
            //if (!String.IsNullOrEmpty(StaffEva))
            //{
            //    if (StaffEva == "ZA")
            //    {
            //        StaffEvas = (from staffEva in db.EvaByStaffs

            //                     join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
            //                     from p_party in g_party_party

            //                     where (String.IsNullOrEmpty(staffEva.StatffEva) && p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

            //                     select staffEva).ToList();
            //    }
            //    else if (StaffEva == "ZB")
            //    {
            //        StaffEvas = (from staffEva in db.EvaByStaffs

            //                     join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
            //                     from p_party in g_party_party

            //                     where (!String.IsNullOrEmpty(staffEva.StatffEva) && p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

            //                     select staffEva).ToList();
            //    }
            //    else
            //    {
            //        StaffEvas = (from staffEva in db.EvaByStaffs

            //                     join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
            //                     from p_party in g_party_party

            //                     where (staffEva.StatffEva == StaffEva && p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

            //                     select staffEva).ToList();
            //    }
            //}
            //else
            //{
            //    StaffEvas = (from staffEva in db.EvaByStaffs

            //                 join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
            //                 from p_party in g_party_party

            //                 where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

            //                 select staffEva).ToList();
            //}


            ////LeaderEva
            if (EvaByLeaders.Count == 0 && noCondition)
            {
                EvaByLeaders = (from leaderEva in db.EvaByLeaders

                                join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
                                from p_party in g_party_party

                                where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

                                select leaderEva).ToList();
            }
            //if (!String.IsNullOrEmpty(LeaderEva))
            //{
            //    if (LeaderEva == "ZA")
            //    {
            //        EvaByLeaders = (from leaderEva in db.EvaByLeaders

            //                        join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
            //                        from p_party in g_party_party

            //                        where (String.IsNullOrEmpty(leaderEva.LeaderEva) && p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

            //                        select leaderEva).ToList();
            //    }
            //    else if (LeaderEva == "ZB")
            //    {
            //        EvaByLeaders = (from leaderEva in db.EvaByLeaders

            //                        join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
            //                        from p_party in g_party_party

            //                        where (!String.IsNullOrEmpty(leaderEva.LeaderEva) && p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

            //                        select leaderEva).ToList();
            //    }
            //    else
            //    {
            //        EvaByLeaders = (from leaderEva in db.EvaByLeaders

            //                        join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
            //                        from p_party in g_party_party

            //                        where (leaderEva.LeaderEva == LeaderEva && p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

            //                        select leaderEva).ToList();
            //    }
            //}
            //else
            //{
            //    EvaByLeaders = (from leaderEva in db.EvaByLeaders

            //                    join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
            //                    from p_party in g_party_party

            //                    where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

            //                    select leaderEva).ToList();
            //}
            #endregion

            #region  findAll
            if (Shops.Count == 0) Shops = db.Shops.ToList();
            if (groups.Count == 0) groups = db.Groups.ToList();
            if (Staffs.Count == 0) Staffs = db.Staffs.ToList();
            if (EvaByLeaders.Count == 0) EvaByLeaders = db.EvaByLeaders.ToList();

            #endregion

            var models = from party in Partys
                         join shop in Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                         from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
                         join evaByLeader in EvaByLeaders on party.PartyID equals evaByLeader.PartyID into g_party_evaByLeader
                         from p_evaByLeader in g_party_evaByLeader.DefaultIfEmpty(new T_EvaByLeader())
                         join staffEva in StaffEvas on party.PartyID equals staffEva.PartyID into g_party_staffEva
                         from p_staffEva in g_party_staffEva.DefaultIfEmpty(new T_EvaByStaff())
                         join staff in Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                         from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
                         //join report in Reports on party.PartyID equals report.PartyID into g_party_report
                         //from p_report in g_party_report.DefaultIfEmpty()
                         join grp in groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                         from s_group in g_shop_group.DefaultIfEmpty(new M_Group())
                         //where (p_report != null)
                         select new EvaByleaderIndexViewModel
                         {
                             PartyID = party.PartyID,
                             ShopCD = party.ShopCD,
                             GroupCD = p_shop.GroupCD,
                             DivisionCD = s_group.DivCD,
                             ShopName = p_shop.ShopName,
                             PartyDate = party.PartyDate,
                             StaffCD = p_staff.StaffCD,
                             TantoCD = p_staff.StaffName,
                             BrideName = party.BrideName,
                             GroomName = party.GroomName,
                             StatffEva = p_staffEva.StatffEva,
                             LeaderEva = p_evaByLeader.LeaderEva,
                             HallType = party.HallType,
                             StartTime = party.StartTime
                         };


            //if (Range == "division")
            //{
            //    if (!string.IsNullOrEmpty(divisionCD))
            //    {
            //        if (divisionCD.StartsWith("g_"))
            //        {
            //            divisionCD = divisionCD.Replace("g_", "");
            //            models = models.Where(s => s.GroupCD == divisionCD);
            //        }
            //        else
            //        {
            //            models = models.Where(s => s.DivisionCD == divisionCD);
            //        }
            //    }
            //}
            //else if (Range == "shop")
            //{
            //    if (!string.IsNullOrEmpty(ShopCD))
            //    {
            //        models = models.Where(p => p.ShopCD == ShopCD);
            //    }
            //}
            //else if (Range == "tanto")
            //{
            //    if (!string.IsNullOrEmpty(StaffCD))
            //    {
            //        models = models.Where(p => p.StaffCD == StaffCD);
            //    }
            //}

            if (!String.IsNullOrEmpty(StaffEva))
            {
                if (StaffEva == "ZA")
                {
                    models = models.Where(p => String.IsNullOrEmpty(p.StatffEva));
                }
                else if (StaffEva == "ZB")
                {
                    models = models.Where(p => !String.IsNullOrEmpty(p.StatffEva));
                }
                else
                {
                    models = models.Where(p => p.StatffEva == StaffEva);
                }
            }

            if (!String.IsNullOrEmpty(LeaderEva))
            {
                if (LeaderEva == "ZA")
                {
                    models = models.Where(p => String.IsNullOrEmpty(p.LeaderEva));
                }
                else if (LeaderEva == "ZB")
                {
                    models = models.Where(p => !String.IsNullOrEmpty(p.LeaderEva));
                }
                else
                {
                    models = models.Where(p => p.LeaderEva == LeaderEva);
                }
            }

            models = models.OrderBy(p => p.ShopCD).ThenBy(p => p.PartyDate).ThenBy(p => p.HallType).ThenBy(p => p.StartTime);

            return models;
        }

        //全データ取得
        private List<EvaByleaderIndexViewModel> GetModels(string ShopCD, string PartyDateFrom, string PartyDateTo, string StaffEva, string LeaderEva, string Range, string divisionCD, string StaffCD,int pageSize, int pNumber, bool GetPageCount, ref int total)
        {
            db.Database.CommandTimeout = 600000;

            ViewBag.ShopCD = ShopCD;
            ViewBag.PartyDateFrom = PartyDateFrom;
            ViewBag.PartyDateTo = PartyDateTo;
            ViewBag.StaffEva = StaffEva;
            ViewBag.LeaderEva = LeaderEva;

            if (ViewBag.PartyDateFrom == "")
            {
                ViewBag.PartyDateFrom = " ";
            }

            ViewBag.Range = Range;
            ViewBag.divisionCD = divisionCD;
            ViewBag.StaffCD = StaffCD;


            DyEntityLogic logic = new DyEntityLogic();

            DynamicsViewRequest req = new DynamicsViewRequest();
            req.ProjID = 1;
            req.EntityName = "T_Party";
            req.ViewName = "PartyEvaIndex";
            req.pageNumber = pNumber;
            req.GetPageCount = GetPageCount;
            req.GetPageSize = pageSize;

            #region  where

            //date
            if (String.IsNullOrEmpty(PartyDateTo))
            {
                PartyDateTo = "9999/12/01";
            }
            if (String.IsNullOrEmpty(PartyDateFrom))
            {
                PartyDateFrom = "1900/01/01";
            }
            DateTime dateFrom = Convert.ToDateTime(PartyDateFrom);
            DateTime dateTo = Convert.ToDateTime(PartyDateTo).AddHours(23).AddMinutes(59).AddSeconds(59);


            req.FilterDic = new Dictionary<string, string>();

            req.FilterDic.Add("PartyDateFormFilter", PartyDateFrom);
            req.FilterDic.Add("PartyDateToFilter", PartyDateTo);


            if (Range == "division")
            {
                if (!string.IsNullOrEmpty(divisionCD))
                {
                    if (divisionCD.StartsWith("g_"))
                    {
                        divisionCD = divisionCD.Replace("g_", "");
                        //group
                        req.FilterDic.Add("GroupCDFilter", divisionCD);
                    }
                    else 
                    {
                        req.FilterDic.Add("DivCDFilter", divisionCD);
                    }
                }
            }
            else if (Range == "shop")
            {
                req.FilterDic.Add("ShopCDFilter", ShopCD);
            } 
            else if (Range == "tanto")
            {
                req.FilterDic.Add("TantoCDFilter", StaffCD);
            }

            if (!String.IsNullOrEmpty(StaffEva))
            {
                if (StaffEva == "ZA")
                {
                    req.FilterDic.Add("EvaStaffEmpty", StaffEva);
                }
                else if (StaffEva == "ZB")
                {
                    req.FilterDic.Add("EvaStaffNotEmpty", StaffEva);
                }
                else
                {
                    req.FilterDic.Add("EvaStaff", StaffEva);
                }
            }

            if (!String.IsNullOrEmpty(LeaderEva))
            {
                if (LeaderEva == "ZA")
                {
                    req.FilterDic.Add("EvaLeaderEmpty", LeaderEva);
                }
                else if (LeaderEva == "ZB")
                {
                    req.FilterDic.Add("EvaLeaderNotEmpty", LeaderEva);
                }
                else
                {
                    req.FilterDic.Add("EvaLeader", LeaderEva);
                }
            }
           
            #endregion
                         //select new EvaByleaderIndexViewModel
                         //{
                         //    PartyID = party.PartyID,
                         //    ShopCD = party.ShopCD,
                         //    GroupCD = p_shop.GroupCD,
                         //    DivisionCD = s_group.DivCD,
                         //    ShopName = p_shop.ShopName,
                         //    PartyDate = party.PartyDate,
                         //    StaffCD = p_staff.StaffCD,
                         //    TantoCD = p_staff.StaffName,
                         //    BrideName = party.BrideName,
                         //    GroomName = party.GroomName,
                         //    StatffEva = p_staffEva.StatffEva,
                         //    LeaderEva = p_evaByLeader.LeaderEva,
                         //    HallType = party.HallType,
                         //    StartTime = party.StartTime
                         //};

            PageViewResult list = logic.GetList(req);

            List<EvaByleaderIndexViewModel> models = new List<EvaByleaderIndexViewModel>();

            if (list.DataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in list.DataTable.Rows)
                {
                    EvaByleaderIndexViewModel m = new EvaByleaderIndexViewModel();
                    m.BrideName = DataUtil.CStr(dr["BrideName"]);
                    m.PartyID = DataUtil.CInt(dr["PartyID"]);
                    m.ShopCD = DataUtil.CStr(dr["ShopCD"]);
                    m.GroupCD = DataUtil.CStr(dr["GroupCD"]);
                    m.DivisionCD = DataUtil.CStr(dr["DivCD"]);
                    m.ShopName = DataUtil.CStr(dr["ShopName"]);
                    m.PartyDate = DataUtil.CDate(dr["PartyDate"]);
                    m.StaffCD = DataUtil.CStr(dr["StaffCD"]);
                    m.TantoCD = DataUtil.CStr(dr["StaffName"]);
                    m.BrideName = DataUtil.CStr(dr["BrideName"]);
                    m.GroomName = DataUtil.CStr(dr["GroomName"]);
                    m.StatffEva = DataUtil.CStr(dr["StatffEva"]);
                    m.LeaderEva = DataUtil.CStr(dr["LeaderEva"]);
                    m.HallType = DataUtil.CStr(dr["HallType"]);
                    m.StartTime = DataUtil.CStr(dr["StartTime"]);
                    models.Add(m);
                }
            }

            total = list.PageCount;
            
            return models;
        }

        // GET: /EvaByLeader/
        public ActionResult Index()
        {
            return View(db.EvaByLeaders.ToList());
        }

        // GET: /EvaByLeader/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EvaByLeader t_evabyleader = db.EvaByLeaders.Find(id);
            if (t_evabyleader == null)
            {
                return HttpNotFound();
            }
            return View(t_evabyleader);
        }

        // GET: /EvaByLeader/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EvaByLeader/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartyID,LeaderEva,UpdateUserID,UpdateTime")] T_EvaByLeader t_evabyleader)
        {
            if (ModelState.IsValid)
            {
                db.EvaByLeaders.Add(t_evabyleader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_evabyleader);
        }

        // GET: /EvaByLeader/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EvaByLeader t_evabyleader = db.EvaByLeaders.Find(id);
            if (t_evabyleader == null)
            {
                return HttpNotFound();
            }
            return View(t_evabyleader);
        }

        // POST: /EvaByLeader/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartyID,LeaderEva,UpdateUserID,UpdateTime")] T_EvaByLeader t_evabyleader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_evabyleader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_evabyleader);
        }

        // GET: /EvaByLeader/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EvaByLeader t_evabyleader = db.EvaByLeaders.Find(id);
            if (t_evabyleader == null)
            {
                return HttpNotFound();
            }
            return View(t_evabyleader);
        }

        // POST: /EvaByLeader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_EvaByLeader t_evabyleader = db.EvaByLeaders.Find(id);
            db.EvaByLeaders.Remove(t_evabyleader);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult MultipleEvaUpdate(List<LeaderEvaData> evas)
        {
            if (evas == null)
            {
                return Json(new { Message = "保存完了しました。" });
            }
            List<T_EvaByLeader> newdata = new List<T_EvaByLeader>();

            foreach (LeaderEvaData eva in evas)
            {
                T_EvaByLeader t_evabyleader = db.EvaByLeaders.Find(eva.PartyID);
                if (t_evabyleader == null)
                {
                    t_evabyleader = new T_EvaByLeader();
                    t_evabyleader.PartyID = eva.PartyID;
                    t_evabyleader.LeaderEva = eva.LeaderEva;
                    t_evabyleader.UpdateTime = DateTime.Now;
                    t_evabyleader.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                    newdata.Add(t_evabyleader);
                }
                else
                {
                    t_evabyleader.LeaderEva = eva.LeaderEva;
                    t_evabyleader.UpdateTime = DateTime.Now;
                    t_evabyleader.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                    db.Entry(t_evabyleader).State = EntityState.Modified;
                }

            }

            try
            {
                db.EvaByLeaders.AddRange(newdata);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Console.Write(ex.ToString());
                return Json(new { Message = ex.ToString() });
            }

            return Json(new { Message = "保存完了しました。" });
        }
    }
}
