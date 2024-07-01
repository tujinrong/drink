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
using WebEvaluation.Common;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Logic;
using SafeNeeds.DySmat.Util;

namespace WebEvaluation.Controllers
{
    [AuthenticationFilter(Order = 1)]
    [ExceptionFilter(Order = 2)]
    public class PartyController : Controller
    {
        private EvaluationContext db = new EvaluationContext();
        //LILIANG :/User/Login 2014/07/03===========↓

        public ActionResult PartyIndex(string ShopCD, string PartyDateFrom, string PartyDateTo, string isPostBack, int? page, string msg, string TelState, string ReportState)
        {
            ViewBag.msg = msg;
            ViewBag.ShopCD = ShopCD;
            ViewBag.PartyDateFrom = PartyDateFrom;
            ViewBag.PartyDateTo = PartyDateTo;
            ViewBag.isPostBack = isPostBack;
            ViewBag.TelState = TelState;
            ViewBag.ReportState = ReportState;

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

            UserSession _user = Session["user"] as UserSession;

            if (_user.RoleCD == "01")
            {
                //ShopCD = _user.ShopCD;
                //if (ShopCD == null && isPostBack == "1")
                //{
                //    ViewBag.msg = "データがありません。";
                //    ViewBag.msgType = "info";
                //    return View();
                //}

                if (isPostBack != "1")
                {
                    ShopCD = _user.ShopCD;
                    ViewBag.ShopCD = ShopCD;
                }
            }

            if (isPostBack != null && isPostBack == "1")
            {
                ViewBag.isPostBack = "1";

                var shopCD_Change = "";//---2014/09/10---LI---店舗番号を格式化---start
                
                if (ShopCD == null)
                {
                    shopCD_Change = "";
                }
                else
                {
                    shopCD_Change = ShopCD.Trim().ToUpper();
                }                      //---2014/09/10---LI---店舗番号番号を格式化---end


                int pageSize = 25;
                int pageNumber = (page ?? 1);

                 int totalItemCount = 0;
                 var models = GetModels(shopCD_Change, PartyDateFrom, PartyDateTo, pageSize,pageNumber, true, ref totalItemCount, TelState, ReportState);

                if (models.ToList().Count == 0)
                {
                    ViewBag.msg = "データがありません。";
                    ViewBag.msgType = "info";
                }
                GC.Collect();



                DyPagedList<PartyIndexViewModel> lit = new DyPagedList<PartyIndexViewModel>(models, pageNumber, pageSize, totalItemCount);


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
        public ActionResult csv(string ShopCD, string PartyDateFrom, string PartyDateTo, string TelState, string ReportState)
        {
            if (PartyDateFrom == " ")
            {
                PartyDateFrom = "";
            }

            UserSession _user = Session["user"] as UserSession;

            if (_user.RoleCD == "01")
            {
                ShopCD = _user.ShopCD;
            }


            int totalItemCount = 0;
            List<PartyIndexViewModel> models = GetModels(ShopCD, PartyDateFrom, PartyDateTo,2000, 0, false, ref totalItemCount, TelState, ReportState).ToList();

            if (models.Count == 0)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            foreach (PartyIndexViewModel model in models) {
                if (model.LeaderEva != null && model.LeaderEva.Trim().Length > 0) 
                {
                    model.StatffEva = model.LeaderEva;
                }
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\パーティ_{0}.csv", date);

            CsvUtils.ModlesToCsv<PartyIndexViewModel>(basePath + fileName, models);
            GC.Collect();
            return Json(new { Path = string.Format("/CSV/パーティ_{0}.csv", date), ResultType = EnumResultType.Success });
        }

        public ActionResult detailCsv(string PartyMonth)
        {
            string PartyDateFrom = "";
            string PartyDateTo = "";

            if (null == PartyMonth)
            {
                DateTime today = DateTime.Now;
                PartyDateFrom = today.AddMonths(1).ToString("yyyy/MM") + "/01";
                PartyDateTo = today.AddMonths(2).ToString("yyyy/MM/dd");
            }
            else
            {
                PartyDateFrom = PartyMonth + "/01";
                DateTime month = DateTime.ParseExact(PartyMonth, "yyyy/MM", System.Globalization.CultureInfo.InvariantCulture);
                PartyDateTo = month.AddMonths(1).ToString("yyyy/MM/dd");
            }


            int totalItemCount = 0;
            List<PartyIndexViewModel> models = GetModels(null, PartyDateFrom, PartyDateTo,2000, 0, false, ref totalItemCount, null, null).ToList();

            if (models.Count == 0)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            List<PartyDetailModel> detailModels = new List<PartyDetailModel>();

            foreach (PartyIndexViewModel item in models)
            {
                PartyDetailModel detail = new PartyDetailModel();
                detail.PartyNo = item.PartyNo;
                detail.ShopCD = item.ShopCD;
                detail.ShopName = item.ShopName;
                detail.PartyDate = item.PartyDate.ToString("yyyy/M/d");
                detail.TantoCD = item.StaffCD;
                detail.StaffName = item.TantoCD;
                detail.BrideName = item.BrideName;
                detail.BrideKana = item.BrideKana;
                detail.GroomName = item.GroomName;
                detail.GroomKana = item.GroomKana;
                detail.BrideHomeTel = item.BrideHomeTel;
                detail.BrideMobile = item.BrideMobile;
                detail.GroomHomeTel = item.GroomHomeTel;
                detail.GroomMobile = item.GroomMobile;
                detail.StartTime = item.StartTime;
                detail.HallType = item.HallType;
                detailModels.Add(detail);
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\パーティ_{0}.csv", date);

            CsvUtils.ModlesToCsv<PartyDetailModel>(basePath + fileName, detailModels);
            GC.Collect();
            return Json(new { Path = string.Format("/CSV/パーティ_{0}.csv", date), ResultType = EnumResultType.Success });
        }

        //全データ取得
        private List<PartyIndexViewModel> GetModels(string ShopCD, string PartyDateFrom, string PartyDateTo,int pageSize, int pNumber, bool GetPageCount, ref int total, string TelState, string ReportState)
        {
            db.Database.CommandTimeout = 600000;

            ViewBag.ShopCD = ShopCD;
            ViewBag.PartyDateFrom = PartyDateFrom;
            ViewBag.PartyDateTo = PartyDateTo;

            if (ViewBag.PartyDateFrom == "")
            {
                ViewBag.PartyDateFrom = " ";
            }

            //List<T_Report> reports = new List<T_Report>();

            //List<T_Party> partys = new List<T_Party>();
            //List<M_Shop> shops = new List<M_Shop>();
            //List<T_EvaByLeader> evaByLeader = new List<T_EvaByLeader>();
            //List<T_EvaByStaff> evaByStaff = new List<T_EvaByStaff>();
            //List<M_Staff> staffs = new List<M_Staff>();

            //#region  where

            ////date
            //if (String.IsNullOrEmpty(PartyDateTo))
            //{
            //    PartyDateTo = "9999/12/01";
            //}
            //if (String.IsNullOrEmpty(PartyDateFrom))
            //{
            //    PartyDateFrom = "1900/01/01";
            //}

            //DateTime dateFrom = Convert.ToDateTime(PartyDateFrom);
            //DateTime dateTo = Convert.ToDateTime(PartyDateTo).AddHours(23).AddMinutes(59).AddSeconds(59);
            //partys = db.Partys.Where(p => p.PartyDate.CompareTo(dateFrom) >= 0 && p.PartyDate.CompareTo(dateTo) <= 0).ToList();

            ////reports
            //reports = (from report in db.Reports

            //           join party in db.Partys on report.PartyID equals party.PartyID into g_party_party
            //              from p_party in g_party_party

            //              where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

            //           select report).ToList();

            ////StaffEva
            //evaByStaff = (from staffEva in db.EvaByStaffs

            //             join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
            //             from p_party in g_party_party

            //             where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

            //             select staffEva).ToList();

            ////LeaderEva
            //evaByLeader = (from leaderEva in db.EvaByLeaders

            //                join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
            //                from p_party in g_party_party

            //                where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

            //                select leaderEva).ToList();
            //#endregion

            //#region  findAll
            //if (shops.Count == 0) shops = db.Shops.ToList();
            //if (staffs.Count == 0) staffs = db.Staffs.ToList();

            //#endregion

            //var models = from party in partys
            //             join shop in shops on party.ShopCD equals shop.ShopCD into g_party_shop
            //             from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
            //             join report in reports on party.PartyID equals report.PartyID into g_party_report
            //             from p_report in g_party_report.DefaultIfEmpty(new T_Report())
            //             join staff in staffs on party.TantoCD equals staff.StaffCD into g_party_staff
            //             from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
            //             join evaStaff in evaByStaff on party.PartyID equals evaStaff.PartyID into g_party_evaStaff
            //             from p_evaStaff in g_party_evaStaff.DefaultIfEmpty(new T_EvaByStaff())
            //             join evaLeader in evaByLeader on party.PartyID equals evaLeader.PartyID into g_party_evaLeader
            //             from p_evaLeader in g_party_evaLeader.DefaultIfEmpty(new T_EvaByLeader())
            //             select new PartyIndexViewModel
            //             {
            //                 PartyID = party.PartyID,
            //                 PartyNo = party.PartyNo,
            //                 ShopCD = party.ShopCD,
            //                 HallType = party.HallType,
            //                 FinishFlag = party.FinishFlag,
            //                 StartTime = party.StartTime,
            //                 ShopName = p_shop.ShopName,
            //                 PartyDate = party.PartyDate,
            //                 TantoCD =  p_staff.StaffName,
            //                 StaffCD = p_staff.StaffCD,
            //                 BrideName = party.BrideName,
            //                 GroomName = party.GroomName,
            //                 BrideKana = party.BrideKana,
            //                 GroomKana = party.GroomKana,
            //                 BrideHomeTel = party.BrideHomeTel,
            //                 BrideMobile = party.BrideMobile,
            //                 GroomHomeTel = party.GroomHomeTel,
            //                 GroomMobile = party.GroomMobile,
            //                 TelFlg = p_report.TelFlg,
            //                 CareFlg = p_evaStaff.CareFlg,
            //                 StatffEva = p_evaStaff.StatffEva,
            //                 LeaderEva = p_evaLeader.LeaderEva
            //             };
            //if (!String.IsNullOrEmpty(ShopCD))
            //{
            //    models = models.Where(p => p.ShopCD == ShopCD);
            //}

            //models = models.OrderBy(p => p.ShopCD).ThenBy(p => p.PartyDate).ThenBy(p => p.HallType).ThenBy(p => p.StartTime);


            DyEntityLogic logic = new DyEntityLogic();

            DynamicsViewRequest req = new DynamicsViewRequest();
            req.ProjID = 1;
            req.EntityName = "T_Party";
            req.ViewName = "PartyIndex";
            req.pageNumber = pNumber;
            req.GetPageCount = GetPageCount;
            req.GetPageSize = pageSize;

            req.FilterDic = new Dictionary<string, string>();

            req.FilterDic.Add("ShopCDFilter", ShopCD);
            req.FilterDic.Add("PartyDateFormFilter", PartyDateFrom);
            req.FilterDic.Add("PartyDateToFilter", PartyDateTo );

            //string ReportStateStr = "";

            //if (string.IsNullOrEmpty(ReportState0) == false
            //    && string.IsNullOrEmpty(ReportState1) == false
            //    && string.IsNullOrEmpty(ReportState2) == false)
            //{
               
            //}else{


            //    if (string.IsNullOrEmpty(ReportState0) == false)
            //    {
            //        ReportStateStr = " T_PartyReport.ReportState is null or T_PartyReport.ReportState = 0";
            //    }

            //    if (string.IsNullOrEmpty(ReportState1) == false)
            //    {
            //        if (string.IsNullOrEmpty(ReportStateStr))
            //        {
            //            ReportStateStr += "T_PartyReport.ReportState = 1";
            //        }
            //        else
            //        {
            //            ReportStateStr += "or T_PartyReport.ReportState = 1";
            //        }
            //    }

            //    if (string.IsNullOrEmpty(ReportState2) == false)
            //    {
            //        if (string.IsNullOrEmpty(ReportStateStr))
            //        {
            //            ReportStateStr += "T_PartyReport.ReportState = 2";
            //        }
            //        else
            //        {
            //            ReportStateStr += "or T_PartyReport.ReportState = 2";
            //        }
            //    }
            //}

          
            //req.FilterDic.Add("ReportStateInFilter", ReportStateStr);

            string ReportStateStr = "";

            if (string.IsNullOrEmpty(TelState) == false)
            {
                ReportStateStr = " T_Party.FinishFlag = 1 ";
            }

            if (string.IsNullOrEmpty(ReportState) == false)
            {
                if (string.IsNullOrEmpty(ReportStateStr))
                {
                    ReportStateStr += "T_PartyReport.ReportState = 1 or T_PartyReport.ReportState = 2";
                }
                else
                {
                    ReportStateStr += "or T_PartyReport.ReportState = 1 or T_PartyReport.ReportState = 2";
                }
            }

            if (string.IsNullOrEmpty(ReportStateStr) == false) {
                ReportStateStr = "(" + ReportStateStr + ")";
            }


            req.FilterDic.Add("ReportStateInFilter", ReportStateStr);

            PageViewResult list = logic.GetList(req);

            List<PartyIndexViewModel> models = new List<PartyIndexViewModel>();

            if(list.DataTable.Rows.Count > 0){
                foreach (DataRow dr in list.DataTable.Rows) {
                    PartyIndexViewModel m = new PartyIndexViewModel();
                    m.BrideHomeTel = DataUtil.CStr(dr["BrideHomeTel"]);
                    m.BrideKana = DataUtil.CStr(dr["BrideKana"]);
                    m.BrideMobile = DataUtil.CStr(dr["BrideMobile"]);
                    m.BrideName = DataUtil.CStr(dr["BrideName"]);
                    m.CareFlg = DataUtil.CStr(dr["CareFlg"]);
                    m.FinishFlag = DataUtil.CBool(dr["FinishFlag"]);
                    m.GroomHomeTel = DataUtil.CStr(dr["GroomHomeTel"]);
                    m.GroomKana = DataUtil.CStr(dr["GroomKana"]);
                    m.GroomMobile = DataUtil.CStr(dr["GroomMobile"]);
                    m.GroomName = DataUtil.CStr(dr["GroomName"]);
                    m.HallType = DataUtil.CStr(dr["HallType"]);
                    m.LeaderEva = DataUtil.CStr(dr["LeaderEva"]);
                    m.PartyDate = DataUtil.CDate(dr["PartyDate"]);
                    m.PartyID = DataUtil.CInt(dr["PartyID"]);
                    m.PartyNo = DataUtil.CStr(dr["PartyNo"]);
                    m.ShopCD = DataUtil.CStr(dr["ShopCD"]);
                    m.ShopName = DataUtil.CStr(dr["ShopName"]);
                    m.StaffCD = DataUtil.CStr(dr["StaffCD"]);
                    m.StartTime = DataUtil.CStr(dr["StartTime"]);
                    m.StatffEva = DataUtil.CStr(dr["StatffEva"]);
                    m.TantoCD = DataUtil.CStr(dr["StaffName"]);
                    m.TelFlg = DataUtil.CStr(dr["TelFlg"]);
                    m.ReportState = DataUtil.CStr(dr["ReportState"]);
                    models.Add(m);
                }
            }

            total = list.PageCount;

            return models;
        }

        //LILIANG :/User/Login 2014/07/03===========↑
        // GET: /Party/
        public ActionResult Index()
        {
            return View(db.Partys.ToList());
        }

        // GET: /Party/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Party t_party = db.Partys.Find(id);
            if (t_party == null)
            {
                return HttpNotFound();
            }
            return View(t_party);
        }

        // GET: /Party/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Party/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PartyID,Year,PartyNo,ShopCD,PartyDate,TantoCD,BrideName,BrideKana,BrideHomeTel,BrideMobile,GroomName,GroomKana,GroomHomeTel,GroomMobile,UpdateUserID,UpdateTime")] T_Party t_party)
        {
            if (ModelState.IsValid)
            {
                db.Partys.Add(t_party);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_party);
        }

        // GET: /Party/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Party t_party = db.Partys.Find(id);
            if (t_party == null)
            {
                return HttpNotFound();
            }
            return View(t_party);
        }

        // POST: /Party/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PartyID,Year,PartyNo,ShopCD,PartyDate,TantoCD,BrideName,BrideKana,BrideHomeTel,BrideMobile,GroomName,GroomKana,GroomHomeTel,GroomMobile,UpdateUserID,UpdateTime")] T_Party t_party)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_party).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_party);
        }

        // GET: /Party/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Party t_party = db.Partys.Find(id);
            if (t_party == null)
            {
                return HttpNotFound();
            }
            return View(t_party);
        }

        // POST: /Party/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Party t_party = db.Partys.Find(id);
            db.Partys.Remove(t_party);
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

        public ActionResult ReportIndex()
        {
            ViewBag.entityName = "Y_AnalysisField";
            ViewBag.pageName = "PartyReportList";
            return View();
        }

        public ActionResult ReportList()
        {

            ViewBag.entityName = "Y_AnalysisField";
            ViewBag.pageName = "PartyReportReferList";
            return View();
        }
    }
}
