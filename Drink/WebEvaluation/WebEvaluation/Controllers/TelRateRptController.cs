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
using WebEvaluation.ViewModels;
using WebEvaluation.Utils;
using PagedList;
using WebEvaluation.Controllers.Filters;
using WebEvaluation.Common;
using WebEvaluation.ReportModels;
using SafeNeeds.DySmat.Logic;
using SafeNeeds.DySmat.Util;

namespace WebEvaluation.Controllers
{
    [AuthenticationFilter(Order = 1)]
    [ExceptionFilter(Order = 2)]
    public class TelRateRptController : Controller
    {
        private EvaluationContext db = new EvaluationContext();

        public ActionResult Index()
        {
            List<TelRateRptViewModel> list = new List<TelRateRptViewModel>();
            for (int i = 1; i < 100;i++)
                list.Add(new TelRateRptViewModel());

            return View(list);
        }
        /*通話率検索-----2014-07-04-----李*/
        public ActionResult TelRptFrequencySearch(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string StaffCD, string Sort, string GroupBy, string SortRpt, string Tel, string isPostBack, int? page)
        {
            ViewBag.Time = Time;
            if (Time == null)
            {
                ViewBag.Time = "Months";
            }

            ViewBag.Range = Range;
            if (Range == null)
            {
                ViewBag.Range = "division";
            }

            DateTime today = DateTime.Now;

            if (Year == null)
            {
                Year = string.Format("{0:yyyy}", today);
            }

            if (Months == null)
            {
                Months = string.Format("{0:yyyy/MM}", today);
            }

            if (DateFrom == null)
            {
                DateFrom = string.Format("{0:yyyy/MM}", today) + "/01";
                ViewBag.PartyDateFrom = DateFrom;
            }
            else if (DateFrom == " ")
            {
                DateFrom = "";
            }

            ViewBag.Year = Year;
            ViewBag.Season = Season;
            ViewBag.Months = Months;
            ViewBag.DateFrom = DateFrom;
            ViewBag.DateTo = DateTo;
            ViewBag.divisionCD = divisionCD;
            ViewBag.ShopCD = ShopCD;
            ViewBag.StaffCD = StaffCD;
            ViewBag.Sort = Sort;
            ViewBag.GroupBy = GroupBy;
            if (Sort == null)
            {
                ViewBag.Sort = "unit";
            }

            if (GroupBy == null)
            {
                ViewBag.GroupBy = "shop";
            }

            ViewBag.Tel = Tel;
            ViewBag.SortRpt = SortRpt;
            if (Tel == null)
            {
                ViewBag.Tel = "memo_special";
            }
            if (SortRpt == null)
            {
                ViewBag.SortRpt = "partyDate";
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

                var models = GetModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change, staffCD_Change, Sort, GroupBy);

                if (models.ToList().Count == 0)
                {
                    ViewBag.msg = "データがありません。";
                    ViewBag.msgType = "info";
                }
                GC.Collect();
                int pageSize = 100;
                int pageNumber = (page ?? 1);
                return View(models.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.isPostBack = "1";
                return View();
            }
        }

        public ActionResult TelRptFrequencyIndex(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string StaffCD, string Sort,string GroupBy, int? page)
        {
            ViewBag.Time = Time;
            ViewBag.Year = Year;
            ViewBag.Season = Season;
            ViewBag.Months = Months;
            ViewBag.DateFrom = DateFrom;
            ViewBag.DateTo = DateTo;
            ViewBag.Range = Range;
            ViewBag.divisionCD = divisionCD;
            ViewBag.ShopCD = ShopCD;
            ViewBag.StaffCD = StaffCD;
            ViewBag.Sort = Sort;
            ViewBag.GroupBy = GroupBy;

            var models = GetModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, ShopCD, StaffCD, Sort, GroupBy);

            if (models.Count() == 0)
            {
                ViewBag.msg = "データがありません。";
                ViewBag.msgType = "info";
            }
            GC.Collect();
            return View(models);
        }

        public ActionResult TelRptFrequencyIndexPreview(string Time, string Year, string Season, string Months, string DateFrom, string DateTo,string Range, string divisionCD, string ShopCD, string StaffCD, string Sort,string GroupBy, int? page)
        {
            ViewBag.Time = Time;
            ViewBag.Year = Year;
            ViewBag.Season = Season;
            ViewBag.Months = Months;
            ViewBag.DateFrom = DateFrom;
            ViewBag.DateTo = DateTo;
            ViewBag.divisionCD = divisionCD;
            ViewBag.ShopCD = ShopCD;
            ViewBag.StaffCD = StaffCD;
            ViewBag.Sort = Sort;
            ViewBag.GroupBy = GroupBy;
            var models = GetModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, ShopCD, StaffCD, Sort, GroupBy);


            List<TelRateRptViewModel> modelDatas = models.ToList();

            if (modelDatas.Count == 0) {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            ExcelReport report = new ExcelReport();
            //ReportType
            report.ReportType = EnumReportType.ListReport;
            //report.isAutoRowHeight = true;

            //Template
            if (GroupBy == "div")
            {
                report.Template = "通話率一覧div.xls";
            }
            else if (GroupBy == "group")
            {
                report.Template = "通話率一覧group.xls";
            }
            else if (GroupBy == "shop")
            {
                report.Template = "通話率一覧shop.xls";
            }
            else
            {
                report.Template = "通話率一覧.xls";
            }
            
            report.FileName = "通話率一覧";

            //データ
            List<IReportData> datas = new List<IReportData>();

            //headerデータ
            TelRateRptReportHeaderModel header = new TelRateRptReportHeaderModel();
            if (Time == "Months")
            {
                header.Time_Title = "月度：";
                header.Time = CStr(Months);
            }
            else if (Time == "Custom")
            {
                header.Time_Title = "期間：";
                header.Time = CStr(DateFrom) + "～" + CStr(DateTo);
            }
            else if (Time == "Year")
            {
                header.Time_Title = "年度：";
                header.Time = CStr(Year);
            }

            if (Range == "division")
            {
                header.Range_Title = "事業部/G：";
                if (divisionCD != null && divisionCD.StartsWith("g_"))
                {
                    M_Group group = db.Groups.Find(divisionCD.Replace("g_",""));
                    if (group!=null)
                    {
                        header.Range_Title = "グループ：";
                        header.Range = group.GroupName;
                    }
                }
                else if (divisionCD != null )
                {
                    M_Division div = db.Divisions.Find(divisionCD);
                    if (div != null)
                    {
                        header.Range_Title = "事業部：";
                        header.Range = div.DivName;
                    }
                }
                

            }
            else if (Range == "shop")
            {
                header.Range_Title = "店舗：";
                if (ShopCD != null)
                {
                    M_Shop shop = db.Shops.Find(ShopCD);
                    if (shop != null)
                    {
                        header.Range = shop.ShopName;
                    }
                }
            }
            else if (Range == "tanto")
            {
                header.Range_Title = "担当者：";
                if (StaffCD != null)
                {
                    M_Staff staff = db.Staffs.Find(StaffCD);
                    if (staff != null)
                    {
                        header.Range = staff.StaffName;
                    }
                }
            }

            //明細データ
            header.Detail = new List<IReportData>();

            int index = 1;
            foreach (TelRateRptViewModel viewModel in modelDatas)
            {
                TelRateRptReportDetailModel detail = new TelRateRptReportDetailModel();

                detail.StaffCD = viewModel.StaffCD;
                detail.StaffName = viewModel.StaffName;
                detail.DivName = viewModel.DivName;
                detail.GroupName = viewModel.GroupName;
                detail.ShopName = viewModel.ShopCD;
                detail.Connected = viewModel.Connected;
                detail.Count = viewModel.Count;
                detail.NoConnected = viewModel.NoConnected;
                detail.Rate = viewModel.Rate;
                

                header.Detail.Add(detail);

                index++;
            }

            datas.Add(header);

            report.Data = datas;

            //帳票生成
            report.CreateReport();
            GC.Collect();
            return Json(new { Path = report.DownLoadPath(), ResultType = EnumResultType.Success});
        
        }

        //全データ取得
        private IEnumerable<TelRateRptViewModel> GetModels(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string StaffCD, string Sort, string GroupBy)
        {
           
            #region  where

            //date

            string partyDateTo = "9999/12/01";
            string partyDateFrom = "1900/01/01";

            DateTime dateFrom = Convert.ToDateTime(partyDateFrom);
            DateTime dateTo = Convert.ToDateTime(partyDateTo).AddHours(23).AddMinutes(59).AddSeconds(59);
            if (Time == "Year")
            {
                if (!String.IsNullOrEmpty(Year))
                {
                    dateFrom = new DateTime(Int32.Parse(Year), 1, 1);
                    dateTo = dateFrom.AddYears(1);
                    if (!String.IsNullOrEmpty(Season))
                    {
                        switch (Season)
                        {
                            case "1":
                                dateFrom = new DateTime(Int32.Parse(Year), 4, 1);
                                dateTo = new DateTime(Int32.Parse(Year), 7, 1);
                                break;
                            case "2":
                                dateFrom = new DateTime(Int32.Parse(Year), 7, 1);
                                dateTo = new DateTime(Int32.Parse(Year), 10, 1);
                                break;
                            case "3":
                                dateFrom = new DateTime(Int32.Parse(Year), 10, 1);
                                dateTo = new DateTime(Int32.Parse(Year) + 1, 1, 1);
                                break;
                            case "4":
                                dateFrom = new DateTime(Int32.Parse(Year) + 1, 1, 1);
                                dateTo = new DateTime(Int32.Parse(Year) + 1, 4, 1);
                                break;
                        }
                    }
                }
            }
            else if (Time == "Months")
            {
                if (!string.IsNullOrEmpty(Months))
                {
                    dateFrom = DateTime.ParseExact(Months, "yyyy/MM", System.Globalization.CultureInfo.InvariantCulture);
                    dateTo = dateFrom.AddMonths(1);
                }
            }
            else if (Time == "Custom")
            {
                if (String.IsNullOrEmpty(DateTo))
                {
                    DateTo = "9999/12/01";
                }
                if (String.IsNullOrEmpty(DateFrom))
                {
                    DateFrom = "1900/01/01";
                }

                dateFrom = Convert.ToDateTime(DateFrom);
                dateTo = Convert.ToDateTime(DateTo).AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            //
            DyEntityLogic logic = new DyEntityLogic();


            #endregion


            IEnumerable<TelRateRptViewModel> models = null;
            List<TelRateRptViewModel> list = new List<TelRateRptViewModel>();

            if (GroupBy == "div")
            {
                /*
                models = from party in Partys
                         join shop in Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                         from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
                         join grp in Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_grp
                         from s_grp in g_shop_grp.DefaultIfEmpty(new M_Group())
                         join divsion in Divisions on s_grp.DivCD equals divsion.DivCD into g_shop_div
                         from s_div in g_shop_div.DefaultIfEmpty(new M_Division())
                         join staff in Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                         from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
                         join evabystaff in StaffEvas on party.PartyID equals evabystaff.PartyID into g_party_staffeva
                         from p_staffeva in g_party_staffeva.DefaultIfEmpty(new T_EvaByStaff())
                         join report in Reports on party.PartyID equals report.PartyID into g_party_report
                         from p_report in g_party_report.DefaultIfEmpty(new T_Report())
                         where p_report.TelFlg == "1" && checkMonth(party, Time, Year, Season, Months, DateFrom, DateTo)
                         group new { s_div, p_staffeva } by new { s_div.DivCD, s_div.DivName } into g

                         select new TelRateRptViewModel
                         {
                             DivCD = g.Key.DivCD,
                             DivName = g.Key.DivName,
                             Count = g.Count(),
                             Connected = g.Count(p => p.p_staffeva.EvaResultFlag == "0"),
                             NoConnected = g.Count(p => p.p_staffeva.EvaResultFlag == "1"),
                             Rate = CountRate(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1")),
                             RateForOrder = CountRateForOrder(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1"))
                         };
                */



                string sql = @"
                        select
	                   T4.DivCD,
	                   T4.DivName,
                       count(T6.EvaResultFlag) AS Count,
                       sum(CASE WHEN T6.EvaResultFlag ='0' THEN 1 ELSE 0 END ) AS Connected,
                       sum(CASE WHEN T6.EvaResultFlag ='1' THEN 1 ELSE 0 END ) AS NoConnected

                    from
	                     T_Party T1
                    inner join M_Shop T2
	                    on T1.ShopCD = T2.ShopCD
                    inner join M_Group T3
	                    on T2.GroupCD =T3.GroupCD 
                    inner join M_Division T4
	                    on T3.DivCD = T4.DivCD
                    inner join M_Staff T5
	                    on T1.TantoCD = T5.StaffCD
                    inner join T_EvaByStaff T6
	                    on T1.PartyID = T6.PartyID
                    where 1=1

            ";

                sql += " and T1.PartyDate >='" + dateFrom + "'";
                sql += " and T1.PartyDate <='" + dateTo + "'";

                if (Range == "division")
                {
                    if (!string.IsNullOrEmpty(divisionCD))
                    {
                        if (divisionCD.StartsWith("g_"))
                        {
                            divisionCD = divisionCD.Replace("g_", "");

                            sql += " and T3.GroupCD ='" + divisionCD + "'";
                        }
                        else
                        {
                            sql += " and T3.DivCD ='" + divisionCD + "'";
                        }
                    }
                }
                else if (Range == "shop")
                {
                    if (!string.IsNullOrEmpty(ShopCD))
                    {
                        sql += " and T1.ShopCD ='" + ShopCD + "'";
                    }
                }
                else if (Range == "tanto")
                {
                    if (!string.IsNullOrEmpty(StaffCD))
                    {
                        sql += " and T1.TantoCD ='" + StaffCD + "'";
                    }
                }

                sql += " Group by ";
                sql += "   T4.DivCD, ";
                sql += "   T4.DivName ";

                
                DataTable dt = logic.FillDataTableBySQL(1, sql);
                models = new List<TelRateRptViewModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    TelRateRptViewModel m = new TelRateRptViewModel();

                    m.DivCD = DataUtil.CStr(dr["DivCD"]);
                    m.DivName = DataUtil.CStr(dr["DivName"]);
                    m.Count = DataUtil.CInt(dr["Count"]);
                    m.Connected = DataUtil.CInt(dr["Connected"]);
                    m.NoConnected = DataUtil.CInt(dr["NoConnected"]);
                    m.Rate = CountRate(m.Count, m.Connected, m.NoConnected);
                    m.RateForOrder = CountRateForOrder(m.Count, m.Connected, m.NoConnected);
                    list.Add(m);
                }
            }
            else if (GroupBy == "group")
            {
                /*
                models = from party in Partys
                         join shop in Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                         from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
                         join grp in Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_grp
                         from s_grp in g_shop_grp.DefaultIfEmpty(new M_Group())
                         join divsion in Divisions on s_grp.DivCD equals divsion.DivCD into g_shop_div
                         from s_div in g_shop_div.DefaultIfEmpty(new M_Division())
                         join staff in Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                         from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
                         join evabystaff in StaffEvas on party.PartyID equals evabystaff.PartyID into g_party_staffeva
                         from p_staffeva in g_party_staffeva.DefaultIfEmpty(new T_EvaByStaff())
                         join report in Reports on party.PartyID equals report.PartyID into g_party_report
                         from p_report in g_party_report.DefaultIfEmpty(new T_Report())
                         where p_report.TelFlg == "1" && checkMonth(party, Time, Year, Season, Months, DateFrom, DateTo)
                         group new {  s_grp, p_staffeva } by new { s_div.DivCD, s_div.DivName, s_grp.GroupCD, s_grp.GroupName } into g

                         select new TelRateRptViewModel
                         {
                             DivCD = g.Key.DivCD,
                             DivName = g.Key.DivName,
                             GroupCD = g.Key.GroupCD,
                             GroupName = g.Key.GroupName,
                             Count = g.Count(),
                             Connected = g.Count(p => p.p_staffeva.EvaResultFlag == "0"),
                             NoConnected = g.Count(p => p.p_staffeva.EvaResultFlag == "1"),
                             Rate = CountRate(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1")),
                             RateForOrder = CountRateForOrder(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1"))
                         };
                 */

                string sql = @"
                        select
	                   T4.DivCD,
	                   T4.DivName,
	                   T3.GroupCD,
	                   T3.GroupName,
                       count(T6.EvaResultFlag) AS Count,
                       sum(CASE WHEN T6.EvaResultFlag ='0' THEN 1 ELSE 0 END ) AS Connected,
                       sum(CASE WHEN T6.EvaResultFlag ='1' THEN 1 ELSE 0 END ) AS NoConnected

                    from
	                     T_Party T1
                    inner join M_Shop T2
	                    on T1.ShopCD = T2.ShopCD
                    inner join M_Group T3
	                    on T2.GroupCD =T3.GroupCD 
                    inner join M_Division T4
	                    on T3.DivCD = T4.DivCD
                    inner join M_Staff T5
	                    on T1.TantoCD = T5.StaffCD
                    inner join T_EvaByStaff T6
	                    on T1.PartyID = T6.PartyID
                    where 1=1

            ";

                sql += " and T1.PartyDate >='" + dateFrom + "'";
                sql += " and T1.PartyDate <='" + dateTo + "'";

                if (Range == "division")
                {
                    if (!string.IsNullOrEmpty(divisionCD))
                    {
                        if (divisionCD.StartsWith("g_"))
                        {
                            divisionCD = divisionCD.Replace("g_", "");

                            sql += " and T3.GroupCD ='" + divisionCD + "'";
                        }
                        else
                        {
                            sql += " and T3.DivCD ='" + divisionCD + "'";
                        }
                    }
                }
                else if (Range == "shop")
                {
                    if (!string.IsNullOrEmpty(ShopCD))
                    {
                        sql += " and T1.ShopCD ='" + ShopCD + "'";
                    }
                }
                else if (Range == "tanto")
                {
                    if (!string.IsNullOrEmpty(StaffCD))
                    {
                        sql += " and T1.TantoCD ='" + StaffCD + "'";
                    }
                }

                sql += " Group by ";
                sql += "   T4.DivCD, ";
                sql += "   T4.DivName, ";
                sql += "   T3.GroupCD, ";
                sql += "   T3.GroupName ";


                DataTable dt = logic.FillDataTableBySQL(1, sql);

                models = new List<TelRateRptViewModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    TelRateRptViewModel m = new TelRateRptViewModel();

                    m.DivCD = DataUtil.CStr(dr["DivCD"]);
                    m.DivName = DataUtil.CStr(dr["DivName"]);
                    m.GroupCD = DataUtil.CStr(dr["GroupCD"]);
                    m.GroupName = DataUtil.CStr(dr["GroupName"]);
                    m.Count = DataUtil.CInt(dr["Count"]);
                    m.Connected = DataUtil.CInt(dr["Connected"]);
                    m.NoConnected = DataUtil.CInt(dr["NoConnected"]);
                    m.Rate = CountRate(m.Count, m.Connected, m.NoConnected);
                    m.RateForOrder = CountRateForOrder(m.Count, m.Connected, m.NoConnected);
                    list.Add(m);
                }
            }
            else if (GroupBy == "shop")
            {
                /*
                models = from party in Partys
                         join shop in Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                         from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
                         join grp in Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_grp
                         from s_grp in g_shop_grp.DefaultIfEmpty(new M_Group())
                         join divsion in Divisions on s_grp.DivCD equals divsion.DivCD into g_shop_div
                         from s_div in g_shop_div.DefaultIfEmpty(new M_Division())
                         join staff in Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                         from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
                         join evabystaff in StaffEvas on party.PartyID equals evabystaff.PartyID into g_party_staffeva
                         from p_staffeva in g_party_staffeva.DefaultIfEmpty(new T_EvaByStaff())
                         join report in Reports on party.PartyID equals report.PartyID into g_party_report
                         from p_report in g_party_report.DefaultIfEmpty(new T_Report())
                         where p_report.TelFlg == "1" && checkMonth(party, Time, Year, Season, Months, DateFrom, DateTo)
                         group new { party, p_shop, s_grp, s_div, p_staffeva } by new { party.ShopCD, s_div.DivCD, s_div.DivName, s_grp.GroupCD, s_grp.GroupName, } into g

                         select new TelRateRptViewModel
                         {
                             DivCD = g.Key.DivCD,
                             DivName = g.Key.DivName,
                             GroupCD = g.Key.GroupCD,
                             GroupName = g.Key.GroupName,
                             ShopCD = g.Key.ShopCD,
                             Count = g.Count(),
                             Connected = g.Count(p => p.p_staffeva.EvaResultFlag == "0"),
                             NoConnected = g.Count(p => p.p_staffeva.EvaResultFlag == "1"),
                             Rate = CountRate(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1")),
                             RateForOrder = CountRateForOrder(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1"))
                         };
                 */

                string sql = @"
                        select
	                   T4.DivCD,
	                   T4.DivName,
	                   T3.GroupCD,
	                   T3.GroupName,
	                   T2.ShopCD,
                       count(T6.EvaResultFlag) AS Count,
                       sum(CASE WHEN T6.EvaResultFlag ='0' THEN 1 ELSE 0 END ) AS Connected,
                       sum(CASE WHEN T6.EvaResultFlag ='1' THEN 1 ELSE 0 END ) AS NoConnected

                    from
	                     T_Party T1
                    inner join M_Shop T2
	                    on T1.ShopCD = T2.ShopCD
                    inner join M_Group T3
	                    on T2.GroupCD =T3.GroupCD 
                    inner join M_Division T4
	                    on T3.DivCD = T4.DivCD
                    inner join M_Staff T5
	                    on T1.TantoCD = T5.StaffCD
                    inner join T_EvaByStaff T6
	                    on T1.PartyID = T6.PartyID
                    inner join T_Report T7
	                    on T1.PartyID = T7.PartyID
                    where 1=1

            ";

                sql += " and T1.PartyDate >='" + dateFrom + "'";
                sql += " and T1.PartyDate <='" + dateTo + "'";
                sql += " and T7.TelFlg = '1' ";

                if (Range == "division")
                {
                    if (!string.IsNullOrEmpty(divisionCD))
                    {
                        if (divisionCD.StartsWith("g_"))
                        {
                            divisionCD = divisionCD.Replace("g_", "");

                            sql += " and T3.GroupCD ='" + divisionCD + "'";
                        }
                        else
                        {
                            sql += " and T3.DivCD ='" + divisionCD + "'";
                        }
                    }
                }
                else if (Range == "shop")
                {
                    if (!string.IsNullOrEmpty(ShopCD))
                    {
                        sql += " and T1.ShopCD ='" + ShopCD + "'";
                    }
                }
                else if (Range == "tanto")
                {
                    if (!string.IsNullOrEmpty(StaffCD))
                    {
                        sql += " and T1.TantoCD ='" + StaffCD + "'";
                    }
                }
                
                sql +=" Group by ";
                sql +="   T4.DivCD, ";
                sql +="   T4.DivName, ";
                sql +="   T3.GroupCD, ";
                sql +="   T3.GroupName, ";
                sql +="   T2.ShopCD ";


                DataTable dt = logic.FillDataTableBySQL(1, sql);

                models = new List<TelRateRptViewModel>();
                foreach (DataRow dr in dt.Rows) {
                    TelRateRptViewModel m = new TelRateRptViewModel();

                    m.DivCD = DataUtil.CStr(dr["DivCD"]);
	                m.DivName  = DataUtil.CStr(dr["DivName"]);
	                m.GroupCD  = DataUtil.CStr(dr["GroupCD"]);
	                m.GroupName  = DataUtil.CStr(dr["GroupName"]);
	                m.ShopCD  = DataUtil.CStr(dr["ShopCD"]);
                    m.Count = DataUtil.CInt(dr["Count"]);
                    m.Connected = DataUtil.CInt(dr["Connected"]);
                    m.NoConnected = DataUtil.CInt(dr["NoConnected"]);
                    m.Rate = CountRate(m.Count, m.Connected, m.NoConnected);
                    m.RateForOrder = CountRateForOrder(m.Count, m.Connected, m.NoConnected);
                    list.Add(m);
                }

            }
            else if (GroupBy == "staff")
            {
                /*
                models = from party in Partys
                         join shop in Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                         from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
                         join grp in Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_grp
                         from s_grp in g_shop_grp.DefaultIfEmpty(new M_Group())
                         join divsion in Divisions on s_grp.DivCD equals divsion.DivCD into g_shop_div
                         from s_div in g_shop_div.DefaultIfEmpty(new M_Division())
                         join staff in Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                         from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
                         join evabystaff in StaffEvas on party.PartyID equals evabystaff.PartyID into g_party_staffeva
                         from p_staffeva in g_party_staffeva.DefaultIfEmpty(new T_EvaByStaff())
                         join report in Reports on party.PartyID equals report.PartyID into g_party_report
                         from p_report in g_party_report.DefaultIfEmpty(new T_Report())
                         where p_report.TelFlg == "1" && checkMonth(party, Time, Year, Season, Months, DateFrom, DateTo)
                         group new { party, p_shop, s_grp, s_div, p_staff, p_staffeva } by new { party.ShopCD, party.TantoCD, s_div.DivCD, s_div.DivName, s_grp.GroupCD, s_grp.GroupName, p_staff.StaffName } into g

                         select new TelRateRptViewModel
                         {
                             DivCD = g.Key.DivCD,
                             DivName = g.Key.DivName,
                             GroupCD = g.Key.GroupCD,
                             GroupName = g.Key.GroupName,
                             ShopCD = g.Key.ShopCD,
                             StaffCD = g.Key.TantoCD,
                             StaffName = g.Key.StaffName,
                             Count = g.Count(),
                             Connected = g.Count(p => p.p_staffeva.EvaResultFlag == "0"),
                             NoConnected = g.Count(p => p.p_staffeva.EvaResultFlag == "1"),
                             Rate = CountRate(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1")),
                             RateForOrder = CountRateForOrder(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1"))
                         };
                  */

                string sql = @"
                        select
	                   T4.DivCD,
	                   T4.DivName,
	                   T3.GroupCD,
	                   T3.GroupName,
	                   T2.ShopCD,
	                   T5.StaffCD,
	                   T5.StaffName,
                       count(T6.EvaResultFlag) AS Count,
                       sum(CASE WHEN T6.EvaResultFlag ='0' THEN 1 ELSE 0 END ) AS Connected,
                       sum(CASE WHEN T6.EvaResultFlag ='1' THEN 1 ELSE 0 END ) AS NoConnected

                    from
	                     T_Party T1
                    inner join M_Shop T2
	                    on T1.ShopCD = T2.ShopCD
                    inner join M_Group T3
	                    on T2.GroupCD =T3.GroupCD 
                    inner join M_Division T4
	                    on T3.DivCD = T4.DivCD
                    inner join M_Staff T5
	                    on T1.TantoCD = T5.StaffCD
                    inner join T_EvaByStaff T6
	                    on T1.PartyID = T6.PartyID
                    where 1=1

            ";

                sql += " and T1.PartyDate >='" + dateFrom + "'";
                sql += " and T1.PartyDate <='" + dateTo + "'";

                if (Range == "division")
                {
                    if (!string.IsNullOrEmpty(divisionCD))
                    {
                        if (divisionCD.StartsWith("g_"))
                        {
                            divisionCD = divisionCD.Replace("g_", "");

                            sql += " and T3.GroupCD ='" + divisionCD + "'";
                        }
                        else
                        {
                            sql += " and T3.DivCD ='" + divisionCD + "'";
                        }
                    }
                }
                else if (Range == "shop")
                {
                    if (!string.IsNullOrEmpty(ShopCD))
                    {
                        sql += " and T1.ShopCD ='" + ShopCD + "'";
                    }
                }
                else if (Range == "tanto")
                {
                    if (!string.IsNullOrEmpty(StaffCD))
                    {
                        sql += " and T1.TantoCD ='" + StaffCD + "'";
                    }
                }

                sql += " Group by ";
                sql += "   T4.DivCD, ";
                sql += "   T4.DivName, ";
                sql += "   T3.GroupCD, ";
                sql += "   T3.GroupName, ";
                sql += "   T2.ShopCD, ";
                sql += "   T5.StaffCD, ";
                sql += "   T5.StaffName ";


                DataTable dt = logic.FillDataTableBySQL(1, sql);

                models = new List<TelRateRptViewModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    TelRateRptViewModel m = new TelRateRptViewModel();

                    m.DivCD = DataUtil.CStr(dr["DivCD"]);
                    m.DivName = DataUtil.CStr(dr["DivName"]);
                    m.GroupCD = DataUtil.CStr(dr["GroupCD"]);
                    m.GroupName = DataUtil.CStr(dr["GroupName"]);
                    m.ShopCD = DataUtil.CStr(dr["ShopCD"]);
                    m.StaffCD = DataUtil.CStr(dr["StaffCD"]);
                    m.StaffName = DataUtil.CStr(dr["StaffName"]);
                    m.Count = DataUtil.CInt(dr["Count"]);
                    m.Connected = DataUtil.CInt(dr["Connected"]);
                    m.NoConnected = DataUtil.CInt(dr["NoConnected"]);
                    m.Rate = CountRate(m.Count, m.Connected, m.NoConnected);
                    m.RateForOrder = CountRateForOrder(m.Count, m.Connected, m.NoConnected);
                    list.Add(m);
                }
            }

            models = from model in list select model;

            if (Sort == "unit")
            {
                models = models.OrderBy(p => p.DivCD).ThenBy(p => p.GroupCD).ThenBy(p => p.ShopCD);
            }
            else if (Sort == "result")
            {
                models = models.OrderByDescending(p => p.RateForOrder).ThenBy(p => p.DivCD).ThenBy(p => p.GroupCD).ThenBy(p => p.ShopCD);
            }
            
            list = models.ToList();

            return list;
        }

        private IEnumerable<TelRateRptViewModel> GetModels_2(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string StaffCD, string Sort, string GroupBy)
        {
            db.Database.CommandTimeout = 600000; 

            //var Partys = db.Partys.ToList();
            //var Shops = db.Shops.ToList();
            //var Groups = db.Groups.ToList();
            //var Divisions = db.Divisions.ToList();
            //var StaffEvas = db.EvaByStaffs.ToList();
            //var Staffs = db.Staffs.ToList();
            //var Reports = db.Reports.ToList();

            List<M_Division> Divisions = db.Divisions.ToList();

            List<T_Report> Reports = new List<T_Report>();
            List<T_Party> Partys = new List<T_Party>();
            List<M_Shop> Shops = new List<M_Shop>();
            List<T_EvaByStaff> StaffEvas = new List<T_EvaByStaff>();
            List<M_Staff> Staffs = new List<M_Staff>();
            List<M_Group> Groups = new List<M_Group>();

            #region  where

            //date

            string partyDateTo = "9999/12/01";
            string partyDateFrom = "1900/01/01";

            DateTime dateFrom = Convert.ToDateTime(partyDateFrom);
            DateTime dateTo = Convert.ToDateTime(partyDateTo).AddHours(23).AddMinutes(59).AddSeconds(59);
            if (Time == "Year")
            {
                if (!String.IsNullOrEmpty(Year))
                {
                    dateFrom = new DateTime(Int32.Parse(Year), 1, 1);
                    dateTo = dateFrom.AddYears(1);
                    if (!String.IsNullOrEmpty(Season))
                    {
                        switch (Season)
                        {
                            case "1":
                                dateFrom = new DateTime(Int32.Parse(Year), 4, 1);
                                dateTo = new DateTime(Int32.Parse(Year), 7, 1);
                                break;
                            case "2":
                                dateFrom = new DateTime(Int32.Parse(Year), 7, 1);
                                dateTo = new DateTime(Int32.Parse(Year), 10, 1);
                                break;
                            case "3":
                                dateFrom = new DateTime(Int32.Parse(Year), 10, 1);
                                dateTo = new DateTime(Int32.Parse(Year) + 1, 1, 1);
                                break;
                            case "4":
                                dateFrom = new DateTime(Int32.Parse(Year) + 1, 1, 1);
                                dateTo = new DateTime(Int32.Parse(Year) + 1, 4, 1);
                                break;
                        }
                    }
                }
            }
            else if (Time == "Months")
            {
                if (!string.IsNullOrEmpty(Months))
                {
                    dateFrom = DateTime.ParseExact(Months, "yyyy/MM", System.Globalization.CultureInfo.InvariantCulture);
                    dateTo = dateFrom.AddMonths(1);
                }
            }
            else if (Time == "Custom")
            {
                if (String.IsNullOrEmpty(DateTo))
                {
                    DateTo = "9999/12/01";
                }
                if (String.IsNullOrEmpty(DateFrom))
                {
                    DateFrom = "1900/01/01";
                }

                dateFrom = Convert.ToDateTime(DateFrom);
                dateTo = Convert.ToDateTime(DateTo).AddHours(23).AddMinutes(59).AddSeconds(59);
            }

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

                        Reports = (from report in db.Reports

                                   join party in db.Partys on report.PartyID equals party.PartyID into g_party_party
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

                                   select report).ToList();

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

                        //reports
                        Reports = (from report in db.Reports

                                   join party in db.Partys on report.PartyID equals party.PartyID into g_party_party
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

                                   select report).ToList();

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

                        Groups = db.Groups.Where(s => s.DivCD == divisionCD).ToList();

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

                    //reports
                    Reports = (from report in db.Reports

                               join party in db.Partys on report.PartyID equals party.PartyID into g_party_party
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

                               select report).ToList();

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

                    //reports
                    Reports = (from report in db.Reports

                               join party in db.Partys on report.PartyID equals party.PartyID into g_party_party
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

                               select report).ToList();

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

                    Staffs = db.Staffs.Where(p => p.StaffCD == StaffCD).ToList();

                    noCondition = false;
                }
            }

            //Partys = db.Partys.Where(p => p.PartyDate.CompareTo(dateFrom) >= 0 && p.PartyDate.CompareTo(dateTo) <= 0).ToList();
            if (Partys.Count == 0 && noCondition) Partys = db.Partys.Where(p => p.PartyDate.CompareTo(dateFrom) >= 0 && p.PartyDate.CompareTo(dateTo) <= 0).ToList();

            ////reports
            if (Reports.Count == 0 && noCondition)
            {
                Reports = (from report in db.Reports

                           join party in db.Partys on report.PartyID equals party.PartyID into g_party_party
                           from p_party in g_party_party

                           where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

                           select report).ToList();
            }


            //StaffEva
            if (StaffEvas.Count == 0 && noCondition)
            {
                StaffEvas = (from staffEva in db.EvaByStaffs

                             join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
                             from p_party in g_party_party

                             where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

                             select staffEva).ToList();
            }

            #endregion

            #region  findAll
            if (Shops.Count == 0) Shops = db.Shops.ToList();
            if (Staffs.Count == 0) Staffs = db.Staffs.ToList();
            if (Groups.Count == 0) Groups = db.Groups.ToList();
            #endregion


            IEnumerable<TelRateRptViewModel> models = null;

            if (GroupBy == "div")
            {
                models = from party in Partys
                         join shop in Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                         from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
                         join grp in Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_grp
                         from s_grp in g_shop_grp.DefaultIfEmpty(new M_Group())
                         join divsion in Divisions on s_grp.DivCD equals divsion.DivCD into g_shop_div
                         from s_div in g_shop_div.DefaultIfEmpty(new M_Division())
                         join staff in Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                         from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
                         join evabystaff in StaffEvas on party.PartyID equals evabystaff.PartyID into g_party_staffeva
                         from p_staffeva in g_party_staffeva.DefaultIfEmpty(new T_EvaByStaff())
                         join report in Reports on party.PartyID equals report.PartyID into g_party_report
                         from p_report in g_party_report.DefaultIfEmpty(new T_Report())
                         where p_report.TelFlg == "1" && checkMonth(party, Time, Year, Season, Months, DateFrom, DateTo)
                         group new { s_div, p_staffeva } by new { s_div.DivCD, s_div.DivName } into g

                         select new TelRateRptViewModel
                         {
                             DivCD = g.Key.DivCD,
                             DivName = g.Key.DivName,
                             Count = g.Count(),
                             Connected = g.Count(p => p.p_staffeva.EvaResultFlag == "0"),
                             NoConnected = g.Count(p => p.p_staffeva.EvaResultFlag == "1"),
                             Rate = CountRate(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1")),
                             RateForOrder = CountRateForOrder(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1"))
                         };
            }
            else if (GroupBy == "group")
            {
                models = from party in Partys
                         join shop in Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                         from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
                         join grp in Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_grp
                         from s_grp in g_shop_grp.DefaultIfEmpty(new M_Group())
                         join divsion in Divisions on s_grp.DivCD equals divsion.DivCD into g_shop_div
                         from s_div in g_shop_div.DefaultIfEmpty(new M_Division())
                         join staff in Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                         from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
                         join evabystaff in StaffEvas on party.PartyID equals evabystaff.PartyID into g_party_staffeva
                         from p_staffeva in g_party_staffeva.DefaultIfEmpty(new T_EvaByStaff())
                         join report in Reports on party.PartyID equals report.PartyID into g_party_report
                         from p_report in g_party_report.DefaultIfEmpty(new T_Report())
                         where p_report.TelFlg == "1" && checkMonth(party, Time, Year, Season, Months, DateFrom, DateTo)
                         group new { s_grp, p_staffeva } by new { s_div.DivCD, s_div.DivName, s_grp.GroupCD, s_grp.GroupName } into g

                         select new TelRateRptViewModel
                         {
                             DivCD = g.Key.DivCD,
                             DivName = g.Key.DivName,
                             GroupCD = g.Key.GroupCD,
                             GroupName = g.Key.GroupName,
                             Count = g.Count(),
                             Connected = g.Count(p => p.p_staffeva.EvaResultFlag == "0"),
                             NoConnected = g.Count(p => p.p_staffeva.EvaResultFlag == "1"),
                             Rate = CountRate(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1")),
                             RateForOrder = CountRateForOrder(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1"))
                         };
            }
            else if (GroupBy == "shop")
            {
                models = from party in Partys
                         join shop in Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                         from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
                         join grp in Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_grp
                         from s_grp in g_shop_grp.DefaultIfEmpty(new M_Group())
                         join divsion in Divisions on s_grp.DivCD equals divsion.DivCD into g_shop_div
                         from s_div in g_shop_div.DefaultIfEmpty(new M_Division())
                         join staff in Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                         from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
                         join evabystaff in StaffEvas on party.PartyID equals evabystaff.PartyID into g_party_staffeva
                         from p_staffeva in g_party_staffeva.DefaultIfEmpty(new T_EvaByStaff())
                         join report in Reports on party.PartyID equals report.PartyID into g_party_report
                         from p_report in g_party_report.DefaultIfEmpty(new T_Report())
                         where p_report.TelFlg == "1" && checkMonth(party, Time, Year, Season, Months, DateFrom, DateTo)
                         group new { party, p_shop, s_grp, s_div, p_staffeva } by new { party.ShopCD, s_div.DivCD, s_div.DivName, s_grp.GroupCD, s_grp.GroupName, } into g

                         select new TelRateRptViewModel
                         {
                             DivCD = g.Key.DivCD,
                             DivName = g.Key.DivName,
                             GroupCD = g.Key.GroupCD,
                             GroupName = g.Key.GroupName,
                             ShopCD = g.Key.ShopCD,
                             Count = g.Count(),
                             Connected = g.Count(p => p.p_staffeva.EvaResultFlag == "0"),
                             NoConnected = g.Count(p => p.p_staffeva.EvaResultFlag == "1"),
                             Rate = CountRate(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1")),
                             RateForOrder = CountRateForOrder(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1"))
                         };
            }
            else if (GroupBy == "staff")
            {
                models = from party in Partys
                         join shop in Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                         from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
                         join grp in Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_grp
                         from s_grp in g_shop_grp.DefaultIfEmpty(new M_Group())
                         join divsion in Divisions on s_grp.DivCD equals divsion.DivCD into g_shop_div
                         from s_div in g_shop_div.DefaultIfEmpty(new M_Division())
                         join staff in Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                         from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
                         join evabystaff in StaffEvas on party.PartyID equals evabystaff.PartyID into g_party_staffeva
                         from p_staffeva in g_party_staffeva.DefaultIfEmpty(new T_EvaByStaff())
                         join report in Reports on party.PartyID equals report.PartyID into g_party_report
                         from p_report in g_party_report.DefaultIfEmpty(new T_Report())
                         where p_report.TelFlg == "1" && checkMonth(party, Time, Year, Season, Months, DateFrom, DateTo)
                         group new { party, p_shop, s_grp, s_div, p_staff, p_staffeva } by new { party.ShopCD, party.TantoCD, s_div.DivCD, s_div.DivName, s_grp.GroupCD, s_grp.GroupName, p_staff.StaffName } into g

                         select new TelRateRptViewModel
                         {
                             DivCD = g.Key.DivCD,
                             DivName = g.Key.DivName,
                             GroupCD = g.Key.GroupCD,
                             GroupName = g.Key.GroupName,
                             ShopCD = g.Key.ShopCD,
                             StaffCD = g.Key.TantoCD,
                             StaffName = g.Key.StaffName,
                             Count = g.Count(),
                             Connected = g.Count(p => p.p_staffeva.EvaResultFlag == "0"),
                             NoConnected = g.Count(p => p.p_staffeva.EvaResultFlag == "1"),
                             Rate = CountRate(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1")),
                             RateForOrder = CountRateForOrder(g.Count(), g.Count(p => p.p_staffeva.EvaResultFlag == "0"), g.Count(p => p.p_staffeva.EvaResultFlag == "1"))
                         };
            }

            if (Sort == "unit")
            {
                models = models.OrderBy(p => p.DivCD).ThenBy(p => p.GroupCD).ThenBy(p => p.ShopCD);
            }
            else if (Sort == "result")
            {
                models = models.OrderByDescending(p => p.RateForOrder).ThenBy(p => p.DivCD).ThenBy(p => p.GroupCD).ThenBy(p => p.ShopCD);
            }

            List<TelRateRptViewModel> list = models.ToList();

            //foreach (TelRateRptViewModel model in list)
            //{
            //    if (model.Count > 0)
            //    {
            //        model.Rate = (double.Parse(model.Connected.ToString()) / model.Count).ToString("0.00%");
            //    }
            //    else
            //    {
            //        model.Rate = "0%";
            //    }
            //}

            return list;
        }

        private string CountRate(int Count, int Connected, int NoConnected)
        {
            if (Count > 0)
            {
                return  (double.Parse(Connected.ToString()) / Count).ToString("0.00%");
            }
            else
            {
                return "0%";
            }
        }

        private double CountRateForOrder(int Count, int Connected, int NoConnected)
        {
            if (Count > 0)
            {
                return (double.Parse(Connected.ToString()) / Count);
            }
            else
            {
                return 0;
            }
        }

        private bool checkMonth(T_Party party, string Time, string Year,string Season, string Months, string DateFrom, string DateTo)
        {
            if (Time == "Year")
            {
                if (!String.IsNullOrEmpty(Year))
                {
                    DateTime dateFrom = new DateTime(Int32.Parse(Year), 1, 1);
                    DateTime dateTo = dateFrom.AddYears(1);

                    if (!String.IsNullOrEmpty(Season))
                    {
                        switch (Season)
                        {
                            case "1":
                                dateFrom = new DateTime(Int32.Parse(Year), 4, 1);
                                dateTo = new DateTime(Int32.Parse(Year), 7, 1);
                                break;
                            case "2":
                                dateFrom = new DateTime(Int32.Parse(Year), 7, 1);
                                dateTo = new DateTime(Int32.Parse(Year), 10, 1);
                                break;
                            case "3":
                                dateFrom = new DateTime(Int32.Parse(Year), 10, 1);
                                dateTo = new DateTime(Int32.Parse(Year) + 1, 1, 1);
                                break;
                            case "4":
                                dateFrom = new DateTime(Int32.Parse(Year) + 1, 1, 1);
                                dateTo = new DateTime(Int32.Parse(Year) + 1, 4, 1);
                                break;
                        }
                    }

                    if (party.PartyDate.CompareTo(dateFrom) >= 0 && party.PartyDate.CompareTo(dateTo) < 0)
                    {
                        return true;
                    }
                }
            }
            else if (Time == "Months")
            {
                if (!string.IsNullOrEmpty(Months))
                {
                    DateTime dateFrom = DateTime.ParseExact(Months, "yyyy/MM", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dateTo = dateFrom.AddMonths(1);
                    if (party.PartyDate.CompareTo(dateFrom) >= 0 && party.PartyDate.CompareTo(dateTo) < 0)
                    {
                        return true;
                    }
                }
            }
            else if (Time == "Custom")
            {
                if (!String.IsNullOrEmpty(DateFrom) || !String.IsNullOrEmpty(DateTo))
                {
                    if (String.IsNullOrEmpty(DateFrom))
                    {
                        DateTime dateTo = Convert.ToDateTime(DateTo).AddHours(23).AddMinutes(59).AddSeconds(59);
                        if (party.PartyDate.CompareTo(dateTo) <= 0)
                        {
                            return true;
                        }
                    }
                    else if (String.IsNullOrEmpty(DateTo))
                    {
                        DateTime dateFrom = Convert.ToDateTime(DateFrom);
                        if (party.PartyDate.CompareTo(dateFrom) >= 0)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        DateTime dateFrom = Convert.ToDateTime(DateFrom);
                        DateTime dateTo = Convert.ToDateTime(DateTo).AddHours(23).AddMinutes(59).AddSeconds(59);
                        if (party.PartyDate.CompareTo(dateFrom) >= 0 && party.PartyDate.CompareTo(dateTo) <= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns></returns>
        public ActionResult csv(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string StaffCD, string Sort, string GroupBy)
        {
            List<TelRateRptViewModel> models = GetModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, ShopCD, StaffCD, Sort, GroupBy).ToList();

            if (models.Count == 0)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\通話率_{0}.csv", date);

            if (GroupBy == "div") 
            {
                //明細データ
                List<TelRateRptReportDetailModelDiv> details = new List<TelRateRptReportDetailModelDiv>();

                int index = 1;
                foreach (TelRateRptViewModel viewModel in models)
                {
                    TelRateRptReportDetailModelDiv detail = new TelRateRptReportDetailModelDiv();

                    detail.DivName = viewModel.DivName;
                    detail.Connected = viewModel.Connected;
                    detail.Count = viewModel.Count;
                    detail.NoConnected = viewModel.NoConnected;
                    detail.Rate = viewModel.Rate;


                    details.Add(detail);

                    index++;
                }
                CsvUtils.ModlesToCsv<TelRateRptReportDetailModelDiv>(basePath + fileName, details);
            }
            else if (GroupBy == "group")
            {
                //明細データ
                List<TelRateRptReportDetailModelGroup> details = new List<TelRateRptReportDetailModelGroup>();

                int index = 1;
                foreach (TelRateRptViewModel viewModel in models)
                {
                    TelRateRptReportDetailModelGroup detail = new TelRateRptReportDetailModelGroup();

                    detail.DivName = viewModel.DivName;
                    detail.GroupName = viewModel.GroupName;
                    detail.Connected = viewModel.Connected;
                    detail.Count = viewModel.Count;
                    detail.NoConnected = viewModel.NoConnected;
                    detail.Rate = viewModel.Rate;


                    details.Add(detail);

                    index++;
                }
                CsvUtils.ModlesToCsv<TelRateRptReportDetailModelGroup>(basePath + fileName, details);
            }
            else if (GroupBy == "shop")
            {
                //明細データ
                List<TelRateRptReportDetailModelShop> details = new List<TelRateRptReportDetailModelShop>();

                int index = 1;
                foreach (TelRateRptViewModel viewModel in models)
                {
                    TelRateRptReportDetailModelShop detail = new TelRateRptReportDetailModelShop();

                    detail.DivName = viewModel.DivName;
                    detail.GroupName = viewModel.GroupName;
                    detail.ShopName = viewModel.ShopCD;
                    detail.Connected = viewModel.Connected;
                    detail.Count = viewModel.Count;
                    detail.NoConnected = viewModel.NoConnected;
                    detail.Rate = viewModel.Rate;


                    details.Add(detail);

                    index++;
                }
                CsvUtils.ModlesToCsv<TelRateRptReportDetailModelShop>(basePath + fileName, details);
            }
            else 
            {
                //明細データ
                List<TelRateRptReportDetailModel> details = new List<TelRateRptReportDetailModel>();

                int index = 1;
                foreach (TelRateRptViewModel viewModel in models)
                {
                    TelRateRptReportDetailModel detail = new TelRateRptReportDetailModel();

                    detail.StaffCD = viewModel.StaffCD;
                    detail.StaffName = viewModel.StaffName;
                    detail.DivName = viewModel.DivName;
                    detail.GroupName = viewModel.GroupName;
                    detail.ShopName = viewModel.ShopCD;
                    detail.Connected = viewModel.Connected;
                    detail.Count = viewModel.Count;
                    detail.NoConnected = viewModel.NoConnected;
                    detail.Rate = viewModel.Rate;


                    details.Add(detail);

                    index++;
                }
                CsvUtils.ModlesToCsv<TelRateRptReportDetailModel>(basePath + fileName, details);
            }
            GC.Collect();
            return Json(new { Path = string.Format("/CSV/通話率_{0}.csv", date), ResultType = EnumResultType.Success });
        }

        private String CStr(object obj)
        {
            if (obj != null)
            {
                return obj.ToString();
            }
            return "";
        }
    }
}
