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
    public class ValRptController : Controller
    {
        private EvaluationContext db = new EvaluationContext();

        [HttpPost]
        public ActionResult ValRptStatisticsSearch(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD)
        {

            ViewBag.Time = Time;
            ViewBag.Range = Range;

            ViewBag.Year = Year;
            ViewBag.Season = Season;
            ViewBag.Months = Months;
            ViewBag.DateFrom = DateFrom;
            ViewBag.DateTo = DateTo;
            ViewBag.divisionCD = divisionCD;
            ViewBag.ShopCD = ShopCD;

            var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start

            if (ShopCD == null)
            {
                shopCD_Change = "";
            }
            else
            {
                shopCD_Change = ShopCD.Trim().ToUpper();
            }                     //---2014/09/10---LI---店舗番号と担当者番号を格式化---end

            List<ValResultViewModel> list = GetResultList(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change);
            if (list == null)
            {
                ViewBag.msg = "データがありません。";
                ViewBag.msgType = "info";
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult ValRptStaffStatisticsSearch(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD)
        {

            ViewBag.Time = Time;
            ViewBag.Range = Range;

            ViewBag.Year = Year;
            ViewBag.Season = Season;
            ViewBag.Months = Months;
            ViewBag.DateFrom = DateFrom;
            ViewBag.DateTo = DateTo;
            ViewBag.divisionCD = divisionCD;
            ViewBag.ShopCD = ShopCD;

            var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start

            if (ShopCD == null)
            {
                shopCD_Change = "";
            }
            else
            {
                shopCD_Change = ShopCD.Trim().ToUpper();
            }                     //---2014/09/10---LI---店舗番号と担当者番号を格式化---end

            List<ValResultStaffViewModel> list = GetStaffResultList(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change);
            if (list == null)
            {
                ViewBag.msg = "データがありません。";
                ViewBag.msgType = "info";
            }
            return View(list);
        }

        public ActionResult ValResultPreview(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD)
        {
            var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start

            if (ShopCD == null)
            {
                shopCD_Change = "";
            }
            else
            {
                shopCD_Change = ShopCD.Trim().ToUpper();
            }                     //---2014/09/10---LI---店舗番号と担当者番号を格式化---end

            List<ValResultViewModel> list = GetResultList(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change);

            if (list == null)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            ExcelReport report = new ExcelReport();
            //ReportType
            report.ReportType = EnumReportType.FixedReport;
            //report.isAutoRowHeight = true;

            //Template
            report.Template = "評価構成.xls";
            report.FileName = "評価構成";

            //データ
            List<IReportData> datas = new List<IReportData>();

            //headerデータ
            ValResultReportHeaderModel header = new ValResultReportHeaderModel();
            if (Time == "Months")
            {
                header.Time_Title = "月度：";
                header.Time = CStr(Months);
            }
            else if (Time == "Custom")
            {
                header.Time_Title = "自由：";
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
                    M_Group group = db.Groups.Find(divisionCD.Replace("g_", ""));
                    if (group != null)
                    {
                        header.Range_Title = "グループ：";
                        header.Range = group.GroupName;
                    }
                }
                else if (divisionCD != null)
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

            //明細データ
            header.Detail = new List<IReportData>();

            ValResultViewModel evaModel = list[0];
            ValResultViewModel cpsModel = list[1];

            header.EvaSS = Int32.Parse(evaModel.EvaSS);
            header.EvaS = Int32.Parse(evaModel.EvaS);
            header.EvaA = Int32.Parse(evaModel.EvaA);
            header.EvaB = Int32.Parse(evaModel.EvaB);
            header.EvaC = Int32.Parse(evaModel.EvaC);
            header.EvaD = Int32.Parse(evaModel.EvaD);
            header.EvaE = Int32.Parse(evaModel.EvaE);
            header.EvaF = Int32.Parse(evaModel.EvaF);

            header.CpsSS = cpsModel.EvaSS;
            header.CpsS = cpsModel.EvaS;
            header.CpsA = cpsModel.EvaA;
            header.CpsB = cpsModel.EvaB;
            header.CpsC = cpsModel.EvaC;
            header.CpsD = cpsModel.EvaD;
            header.CpsE = cpsModel.EvaE;
            header.CpsF = cpsModel.EvaF;

            datas.Add(header);

            report.Data = datas;

            //帳票生成
            report.CreateReport();

            return Json(new { Path = report.DownLoadPath(), ResultType = EnumResultType.Success });
        }

        public ActionResult ValStaffResultPreview(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD)
        {
            var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start

            if (ShopCD == null)
            {
                shopCD_Change = "";
            }
            else
            {
                shopCD_Change = ShopCD.Trim().ToUpper();
            }                     //---2014/09/10---LI---店舗番号と担当者番号を格式化---end

            List<ValResultStaffViewModel> list = GetStaffResultList(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change);

            if (list == null)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            ExcelReport report = new ExcelReport();
            //ReportType
            report.ReportType = EnumReportType.ListReport;
            //report.isAutoRowHeight = true;

            //Template
            report.Template = "担当者別評価構成.xls";
            report.FileName = "担当者別評価構成";

            //データ
            List<IReportData> datas = new List<IReportData>();

            //headerデータ
            ValResultStaffReportHeaderModel header = new ValResultStaffReportHeaderModel();
            if (Time == "Months")
            {
                header.Time_Title = "月度：";
                header.Time = CStr(Months);
            }
            else if (Time == "Custom")
            {
                header.Time_Title = "自由：";
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
                    M_Group group = db.Groups.Find(divisionCD.Replace("g_", ""));
                    if (group != null)
                    {
                        header.Range_Title = "グループ：";
                        header.Range = group.GroupName;
                    }
                }
                else if (divisionCD != null)
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

            //明細データ
            header.Detail = new List<IReportData>();

            

            for (int i = 0; i < list.Count; i++) {
                ValResultStaffViewModel evaModel = list[i];
                ValResultStaffViewModel cpsModel = list[i+1];
                i++;

                ValResultStaffReportDetailModel detail = new ValResultStaffReportDetailModel();

                detail.ShopName = evaModel.ShopName;
                detail.StaffName = evaModel.StaffName;

                detail.EvaSS = Int32.Parse(evaModel.EvaSS);
                detail.EvaS = Int32.Parse(evaModel.EvaS);
                detail.EvaA = Int32.Parse(evaModel.EvaA);
                detail.EvaB = Int32.Parse(evaModel.EvaB);
                detail.EvaC = Int32.Parse(evaModel.EvaC);
                detail.EvaD = Int32.Parse(evaModel.EvaD);
                detail.EvaE = Int32.Parse(evaModel.EvaE);
                detail.EvaF = Int32.Parse(evaModel.EvaF);

                detail.CpsSS = cpsModel.EvaSS;
                detail.CpsS = cpsModel.EvaS;
                detail.CpsA = cpsModel.EvaA;
                detail.CpsB = cpsModel.EvaB;
                detail.CpsC = cpsModel.EvaC;
                detail.CpsD = cpsModel.EvaD;
                detail.CpsE = cpsModel.EvaE;
                detail.CpsF = cpsModel.EvaF;

                header.Detail.Add(detail);
            }

            datas.Add(header);

            report.Data = datas;

            //帳票生成
            report.CreateReport();

            return Json(new { Path = report.DownLoadPath(), ResultType = EnumResultType.Success });
        }

        public List<ValResultViewModel> GetResultList(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD)
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

            

            #endregion


            DyEntityLogic logic = new DyEntityLogic();

            string sql = @"
                        select
	                   sum(CASE WHEN T6.LeaderEva ='SS' THEN 1 ELSE 0 END ) AS EvaSS,
                       sum(CASE WHEN T6.LeaderEva ='S' THEN 1 ELSE 0 END ) AS EvaS,
                       sum(CASE WHEN T6.LeaderEva ='A' THEN 1 ELSE 0 END ) AS EvaA,
                       sum(CASE WHEN T6.LeaderEva ='B' THEN 1 ELSE 0 END ) AS EvaB,
                       sum(CASE WHEN T6.LeaderEva ='C' THEN 1 ELSE 0 END ) AS EvaC,
                       sum(CASE WHEN T6.LeaderEva ='D' THEN 1 ELSE 0 END ) AS EvaD,
                       sum(CASE WHEN T6.LeaderEva ='E' THEN 1 ELSE 0 END ) AS EvaE,
                       sum(CASE WHEN T6.LeaderEva ='F' THEN 1 ELSE 0 END ) AS EvaF

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
                    inner join T_EvaByLeader T6
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


            DataTable dt = logic.FillDataTableBySQL(1, sql);

            List<ValRptResultViewModel> modelEva = new List<ValRptResultViewModel>();
            foreach (DataRow dr in dt.Rows)
            {
                ValRptResultViewModel m = new ValRptResultViewModel();
                m.EvaSS = DataUtil.CInt(dr["EvaSS"]);
                m.EvaS = DataUtil.CInt(dr["EvaS"]);
                m.EvaA = DataUtil.CInt(dr["EvaA"]);
                m.EvaB = DataUtil.CInt(dr["EvaB"]);
                m.EvaC = DataUtil.CInt(dr["EvaC"]);
                m.EvaD = DataUtil.CInt(dr["EvaD"]);
                m.EvaE = DataUtil.CInt(dr["EvaE"]);
                m.EvaF = DataUtil.CInt(dr["EvaF"]);

                modelEva.Add(m);
            }


            if (modelEva.Count == 0)
            {
                return null;
            }
            else
            {
                List<ValResultViewModel> list = new List<ValResultViewModel>();

                int sumAll = modelEva[0].EvaSS + modelEva[0].EvaS + modelEva[0].EvaA + modelEva[0].EvaB + modelEva[0].EvaC + modelEva[0].EvaD + modelEva[0].EvaE + modelEva[0].EvaF;
                ViewBag.pie = new ValResultViewModel
                {
                    Type = "評価数",
                    EvaSS = DataUtil.CStr(modelEva[0].EvaSS),
                    EvaS = DataUtil.CStr(modelEva[0].EvaS),
                    EvaA = DataUtil.CStr(modelEva[0].EvaA),
                    EvaB = DataUtil.CStr(modelEva[0].EvaB),
                    EvaC = DataUtil.CStr(modelEva[0].EvaC),
                    EvaD = DataUtil.CStr(modelEva[0].EvaD),
                    EvaE = DataUtil.CStr(modelEva[0].EvaE),
                    EvaF = DataUtil.CStr(modelEva[0].EvaF),

                    SumAll = DataUtil.CStr(sumAll)
                };

                list.Add(new ValResultViewModel
                {
                    Type = "評価数",
                    EvaSS = DataUtil.CStr(modelEva[0].EvaSS),
                    EvaS = DataUtil.CStr(modelEva[0].EvaS),
                    EvaA = DataUtil.CStr(modelEva[0].EvaA),
                    EvaB = DataUtil.CStr(modelEva[0].EvaB),
                    EvaC = DataUtil.CStr(modelEva[0].EvaC),
                    EvaD = DataUtil.CStr(modelEva[0].EvaD),
                    EvaE = DataUtil.CStr(modelEva[0].EvaE),
                    EvaF = DataUtil.CStr(modelEva[0].EvaF),

                    SumAll = DataUtil.CStr(sumAll)
                
                });
                list.Add(new ValResultViewModel
                {
                    Type = "構成比",
                    EvaSS = (double.Parse(DataUtil.CStr(modelEva[0].EvaSS)) / sumAll).ToString("0.00%"),
                    EvaS = (double.Parse(DataUtil.CStr(modelEva[0].EvaS)) / sumAll).ToString("0.00%"),
                    EvaA = (double.Parse(DataUtil.CStr(modelEva[0].EvaA)) / sumAll).ToString("0.00%"),
                    EvaB = (double.Parse(DataUtil.CStr(modelEva[0].EvaB)) / sumAll).ToString("0.00%"),
                    EvaC = (double.Parse(DataUtil.CStr(modelEva[0].EvaC)) / sumAll).ToString("0.00%"),
                    EvaD = (double.Parse(DataUtil.CStr(modelEva[0].EvaD)) / sumAll).ToString("0.00%"),
                    EvaE = (double.Parse(DataUtil.CStr(modelEva[0].EvaE)) / sumAll).ToString("0.00%"),
                    EvaF = (double.Parse(DataUtil.CStr(modelEva[0].EvaF)) / sumAll).ToString("0.00%"),
                    SumAll = "100%"
                });

                return list;
            }
        }

        public List<ValResultStaffViewModel> GetStaffResultList(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD)
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

           
            #endregion

            
            {

                

                string sql = @"
                        select
	                   T2.ShopCD,
	                   T2.ShopName,
	                   T5.StaffCD,
	                   T5.StaffName,
                       sum(CASE WHEN T6.LeaderEva ='SS' THEN 1 ELSE 0 END ) AS EvaSS,
                       sum(CASE WHEN T6.LeaderEva ='S' THEN 1 ELSE 0 END ) AS EvaS,
                       sum(CASE WHEN T6.LeaderEva ='A' THEN 1 ELSE 0 END ) AS EvaA,
                       sum(CASE WHEN T6.LeaderEva ='B' THEN 1 ELSE 0 END ) AS EvaB,
                       sum(CASE WHEN T6.LeaderEva ='C' THEN 1 ELSE 0 END ) AS EvaC,
                       sum(CASE WHEN T6.LeaderEva ='D' THEN 1 ELSE 0 END ) AS EvaD,
                       sum(CASE WHEN T6.LeaderEva ='E' THEN 1 ELSE 0 END ) AS EvaE,
                       sum(CASE WHEN T6.LeaderEva ='F' THEN 1 ELSE 0 END ) AS EvaF

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
                    inner join T_EvaByLeader T6
	                    on T1.PartyID = T6.PartyID
                    inner join T_Report T7
	                    on T1.PartyID = T7.PartyID
                    where 1=1

            ";

                sql += " and T1.PartyDate >='" + dateFrom + "'";
                sql += " and T1.PartyDate <='" + dateTo + "'";
                sql += " and T7.TelFlg = '1'";

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

                sql += " Group by ";
                sql += "   T2.ShopCD, ";
                sql += "   T2.ShopName, ";
                sql += "   T5.StaffCD, ";
                sql += "   T5.StaffName ";

                sql += " Order by ";
                sql += "   T2.ShopCD, ";
                sql += "   T5.StaffCD ";

                DyEntityLogic logic = new DyEntityLogic();
                DataTable dt = logic.FillDataTableBySQL(1, sql);

                List<ValResultStaffViewModel> resultGroup = new List<ValResultStaffViewModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    ValResultStaffViewModel m = new ValResultStaffViewModel();

                    
                    m.ShopName = DataUtil.CStr(dr["ShopName"]);
                    m.StaffName = DataUtil.CStr(dr["StaffName"]);
                    m.EvaSS = DataUtil.CStr(dr["EvaSS"]);
                    m.EvaS = DataUtil.CStr(dr["EvaS"]);
                    m.EvaA = DataUtil.CStr(dr["EvaA"]);
                    m.EvaB = DataUtil.CStr(dr["EvaB"]);
                    m.EvaC = DataUtil.CStr(dr["EvaC"]);
                    m.EvaD = DataUtil.CStr(dr["EvaD"]);
                    m.EvaE = DataUtil.CStr(dr["EvaE"]);
                    m.EvaF = DataUtil.CStr(dr["EvaF"]);

                    m.SumAll = DataUtil.CStr(DataUtil.CInt(dr["EvaSS"]) + DataUtil.CInt(dr["EvaS"]) + DataUtil.CInt(dr["EvaA"]) + DataUtil.CInt(dr["EvaB"]) + DataUtil.CInt(dr["EvaC"]) + DataUtil.CInt(dr["EvaD"]) + DataUtil.CInt(dr["EvaE"]) + DataUtil.CInt(dr["EvaF"]));


                    resultGroup.Add(m);
                }



                List<ValResultStaffViewModel> list = new List<ValResultStaffViewModel>();

                foreach (var item in resultGroup) {

                    list.Add(new ValResultStaffViewModel
                    {
                        Type = "評価数",
                        ShopName = item.ShopName,
                        StaffName = item.StaffName,
                        EvaSS = item.EvaSS,
                        EvaS = item.EvaS,
                        EvaA = item.EvaA,
                        EvaB = item.EvaB,
                        EvaC = item.EvaC,
                        EvaD = item.EvaD,
                        EvaE = item.EvaE,
                        EvaF = item.EvaF,

                        SumAll = item.SumAll
                    });
                    list.Add(new ValResultStaffViewModel
                    {
                        Type = "構成比",
                        ShopName = item.ShopName,
                        StaffName = item.StaffName,
                        EvaSS = (double.Parse(item.EvaSS) / DataUtil.CInt(item.SumAll)).ToString("0.00%"),
                        EvaS = (double.Parse(item.EvaS) / DataUtil.CInt(item.SumAll)).ToString("0.00%"),
                        EvaA = (double.Parse(item.EvaA) / DataUtil.CInt(item.SumAll)).ToString("0.00%"),
                        EvaB = (double.Parse(item.EvaB) / DataUtil.CInt(item.SumAll)).ToString("0.00%"),
                        EvaC = (double.Parse(item.EvaC) / DataUtil.CInt(item.SumAll)).ToString("0.00%"),
                        EvaD = (double.Parse(item.EvaD) / DataUtil.CInt(item.SumAll)).ToString("0.00%"),
                        EvaE = (double.Parse(item.EvaE) / DataUtil.CInt(item.SumAll)).ToString("0.00%"),
                        EvaF = (double.Parse(item.EvaF) / DataUtil.CInt(item.SumAll)).ToString("0.00%"),

                        SumAll = "100%"
                    });
                }
                return list;
            }
        }

        [HttpPost]
        public ActionResult ValRptIndex(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string Sort, List<string> Show, int? page)
        {
            ViewBag.Time = Time;
            ViewBag.Year = Year;
            ViewBag.Season = Season;
            ViewBag.Months = Months;
            ViewBag.DateFrom = DateFrom;
            ViewBag.DateTo = DateTo;
            ViewBag.Range = Range;
            ViewBag.divisionCD = divisionCD;
            ViewBag.Sort = Sort;
            ViewBag.Show = Show;
            if (Show == null)
            {
                ViewBag.Tel = new List<string> { "null" };
            }

            var models = GetResultModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, ShopCD, Sort, Show);

            if (models == null)
            {
                ViewBag.msg = "データがありません。";
                ViewBag.msgType = "info";
            }

            return View(models);
        }

        public ActionResult ValRptResultPreview(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string Sort, List<string> Show, int? page)
        {
            var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start

            if (ShopCD == null)
            {
                shopCD_Change = "";
            }
            else
            {
                shopCD_Change = ShopCD.Trim().ToUpper();
            }                     //---2014/09/10---LI---店舗番号と担当者番号を格式化---end
            var models = GetResultModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change, Sort, Show);



            if (models == null)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            List<ValRptResultViewModel> modelDatas = models.ToList();

            ExcelReport report = new ExcelReport();
            //ReportType
            report.ReportType = EnumReportType.ListReport;
            //report.isAutoRowHeight = true;

            //Template
            if (Show == null || Show.Count == 0) 
            {
                report.Template = "総合評価-4.xls";
            }
            else if (Show.Contains("eva") && Show.Contains("divgroup"))
            {
                report.Template = "総合評価.xls";
            }
            else if (Show.Contains("eva"))
            {
                report.Template = "総合評価-2.xls";
            }
            else if (Show.Contains("divgroup"))
            {
                report.Template = "総合評価-3.xls";
            }
            
            report.FileName = "総合評価";

            //データ
            List<IReportData> datas = new List<IReportData>();

            //headerデータ
            ValRptResultReportHeaderModel header = new ValRptResultReportHeaderModel();

            header.AvgJp = ViewBag.countryAvg;

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
                    M_Group group = db.Groups.Find(divisionCD.Replace("g_", ""));
                    if (group != null)
                    {
                        header.Range_Title = "グループ：";
                        header.Range = group.GroupName;
                    }
                }
                else if (divisionCD != null)
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

            //明細データ
            header.Detail = new List<IReportData>();

            int index = 1;
            foreach (ValRptResultViewModel viewModel in modelDatas)
            {
                ValRptResultReportDetailModel detail = new ValRptResultReportDetailModel();

                detail.No = index;
                
                detail.Avg = viewModel.Avg;
                detail.AvgDiv = viewModel.AvgDiv;
                detail.AvgGroup = viewModel.AvgGroup;
                detail.CountAll = viewModel.CountAll;
                detail.DivName = viewModel.DivName;
                detail.EvaA = viewModel.EvaA;
                detail.EvaB = viewModel.EvaB;
                detail.EvaC = viewModel.EvaC;
                detail.EvaD = viewModel.EvaD;
                detail.EvaE = viewModel.EvaE;
                detail.EvaF = viewModel.EvaF;
                detail.EvaSS = viewModel.EvaSS;
                detail.EvaS = viewModel.EvaS;
                detail.GroupName = viewModel.GroupName;
                detail.SumAll = viewModel.SumAll;
                detail.ShopName = viewModel.ShopCD;
                
                header.Detail.Add(detail);

                index++;
        }

            datas.Add(header);

            report.Data = datas;

            //帳票生成
            report.CreateReport();

            return Json(new { Path = report.DownLoadPath(), ResultType = EnumResultType.Success });

        }

        public ActionResult ValRptSearch(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string Host, string Sort, List<string> Show, string isPostBack,int? page)
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
            ViewBag.Host = Host;
            if (Host == null)
            {
                ViewBag.Host = "hwpd";
            }
            ViewBag.Sort = Sort;
            if (Sort == null)
            {
                ViewBag.Sort = "shop";
            }

            ViewBag.Show = Show;
            
            if (isPostBack != null && isPostBack == "1")
            {
                ViewBag.isPostBack = "1";

                var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start
               
                if (ShopCD == null)
                {
                    shopCD_Change = "";
                }
                else
                {
                    shopCD_Change = ShopCD.Trim().ToUpper();
                }                     //---2014/09/10---LI---店舗番号と担当者番号を格式化---end

                var models = GetResultModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change, Sort, Show);

                if (null == models || models.ToList().Count == 0)
                {
                    ViewBag.msg = "データがありません。";
                    ViewBag.msgType = "info";
                    return View();
                }
                else
                {
                    int pageSize = 100;
                    int pageNumber = (page ?? 1);
                    return View(models.ToPagedList(pageNumber, pageSize));
                }
            }
            else
            {
                if (Show == null)
                {
                    ViewBag.Show = new List<string> { "eva", "divgroup" };
                }

                ViewBag.isPostBack = "1";
                return View();
            }
        }

        public ActionResult ValRptStatisticsSearch(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string msg, string msgType, string isPostBack)
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

            ViewBag.msg = msg;
            ViewBag.msgType = msgType;

            ViewBag.Year = Year;
            ViewBag.Season = Season;
            ViewBag.Months = Months;
            ViewBag.DateFrom = DateFrom;
            ViewBag.DateTo = DateTo;
            ViewBag.divisionCD = divisionCD;
            ViewBag.ShopCD = ShopCD;

            if (isPostBack != null && isPostBack == "1")
            {
                ViewBag.isPostBack = "1";

                var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start

                if (ShopCD == null)
                {
                    shopCD_Change = "";
                }
                else
                {
                    shopCD_Change = ShopCD.Trim().ToUpper();
                }                     //---2014/09/10---LI---店舗番号と担当者番号を格式化---end

                List<ValResultViewModel> list = GetResultList(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change);
                if (list == null)
                {
                    ViewBag.msg = "データがありません。";
                    ViewBag.msgType = "info";
                }
                return View(list);
            }
            else
            {
                ViewBag.isPostBack = "1";
                return View();
            }
        }

        public ActionResult ValRptStaffStatisticsSearch(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string msg, string msgType, string isPostBack)
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

            ViewBag.msg = msg;
            ViewBag.msgType = msgType;

            ViewBag.Year = Year;
            ViewBag.Season = Season;
            ViewBag.Months = Months;
            ViewBag.DateFrom = DateFrom;
            ViewBag.DateTo = DateTo;
            ViewBag.divisionCD = divisionCD;
            ViewBag.ShopCD = ShopCD;

            if (isPostBack != null && isPostBack == "1")
            {
                ViewBag.isPostBack = "1";

                var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start

                if (ShopCD == null)
                {
                    shopCD_Change = "";
                }
                else
                {
                    shopCD_Change = ShopCD.Trim().ToUpper();
                }                     //---2014/09/10---LI---店舗番号と担当者番号を格式化---end

                List<ValResultViewModel> list = GetResultList(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change);
                if (list == null)
                {
                    ViewBag.msg = "データがありません。";
                    ViewBag.msgType = "info";
                }
                return View(list);
            }
            else
            {
                ViewBag.isPostBack = "1";
                return View();
            }
        }

        public ActionResult Index()
        {
            List<ValRptViewModel> list = new List<ValRptViewModel>();
            for (int i = 1; i < 20;i++)
                list.Add(new ValRptViewModel());

            return View(list);
        }

        //全データ取得
        private IEnumerable<ValRptResultViewModel> GetResultModels(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string Sort, List<string> Show)
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
            #endregion
            

            {

                //int valSS = 5;
                //int valS = 4;
                //int valA = 3;
                //int valB = 2;
                //int valC = 1;
                //int valD = -1;
                //int valE = -2;
                //int valF = -3;
                int valSS = 5;
                int valS = 4;
                int valA = 3;
                int valB = 2;
                int valC = 1;
                int valD = -1;
                int valE = -2;
                int valF = -3;


                #region 評価平均
                /*
                var modelEva = from m in Models
                               group new { p_leadereva = m.p_leadereva } by new { m.party.ShopCD, m.p_shop.ShopName, m.s_div.DivCD, m.s_div.DivName, m.s_grp.GroupCD, m.s_grp.GroupName } into g

                               select new
                               {
                                   No = 1,
                                   DivCD = g.Key.DivCD,
                                   DivName = g.Key.DivName,
                                   GroupCD = g.Key.GroupCD,
                                   GroupName = g.Key.GroupName,
                                   ShopCD = g.Key.ShopCD,
                                   ShopName = g.Key.ShopName,

                                   EvaSS = g.Count(p => p.p_leadereva.LeaderEva == "SS"),
                                   EvaS = g.Count(p => p.p_leadereva.LeaderEva == "S"),
                                   EvaA = g.Count(p => p.p_leadereva.LeaderEva == "A"),
                                   EvaB = g.Count(p => p.p_leadereva.LeaderEva == "B"),
                                   EvaC = g.Count(p => p.p_leadereva.LeaderEva == "C"),
                                   EvaD = g.Count(p => p.p_leadereva.LeaderEva == "D"),
                                   EvaE = g.Count(p => p.p_leadereva.LeaderEva == "E"),
                                   EvaF = g.Count(p => p.p_leadereva.LeaderEva == "F"),
                                   CountAll = g.Count(),
                                   SumAll = g.Count(p => p.p_leadereva.LeaderEva == "SS") * valSS + g.Count(p => p.p_leadereva.LeaderEva == "S") * valS + g.Count(p => p.p_leadereva.LeaderEva == "A") * valA + g.Count(p => p.p_leadereva.LeaderEva == "B") * valB + g.Count(p => p.p_leadereva.LeaderEva == "C") * valC + g.Count(p => p.p_leadereva.LeaderEva == "D") * valD + g.Count(p => p.p_leadereva.LeaderEva == "E") * valE + g.Count(p => p.p_leadereva.LeaderEva == "F") * valF,
                                   Avg = (double)(g.Count(p => p.p_leadereva.LeaderEva == "SS") * valSS + g.Count(p => p.p_leadereva.LeaderEva == "S") * valS + g.Count(p => p.p_leadereva.LeaderEva == "A") * valA + g.Count(p => p.p_leadereva.LeaderEva == "B") * valB + g.Count(p => p.p_leadereva.LeaderEva == "C") * valC + g.Count(p => p.p_leadereva.LeaderEva == "D") * valD + g.Count(p => p.p_leadereva.LeaderEva == "E") * valE + g.Count(p => p.p_leadereva.LeaderEva == "F") * valF)
                                   / g.Count(p => p.p_leadereva.LeaderEva == "SS" || p.p_leadereva.LeaderEva == "S" || p.p_leadereva.LeaderEva == "A" || p.p_leadereva.LeaderEva == "B" || p.p_leadereva.LeaderEva == "C" || p.p_leadereva.LeaderEva == "D" || p.p_leadereva.LeaderEva == "E" || p.p_leadereva.LeaderEva == "F")
                               };
                 * */

                string sql = @"
                        select
	                   T4.DivCD,
	                   T4.DivName,
	                   T3.GroupCD,
	                   T3.GroupName,
	                   T2.ShopCD,
	                   T2.ShopName,
                       sum(CASE WHEN T6.LeaderEva ='SS' THEN 1 ELSE 0 END ) AS EvaSS,
                       sum(CASE WHEN T6.LeaderEva ='S' THEN 1 ELSE 0 END ) AS EvaS,
                       sum(CASE WHEN T6.LeaderEva ='A' THEN 1 ELSE 0 END ) AS EvaA,
                       sum(CASE WHEN T6.LeaderEva ='B' THEN 1 ELSE 0 END ) AS EvaB,
                       sum(CASE WHEN T6.LeaderEva ='C' THEN 1 ELSE 0 END ) AS EvaC,
                       sum(CASE WHEN T6.LeaderEva ='D' THEN 1 ELSE 0 END ) AS EvaD,
                       sum(CASE WHEN T6.LeaderEva ='E' THEN 1 ELSE 0 END ) AS EvaE,
                       sum(CASE WHEN T6.LeaderEva ='F' THEN 1 ELSE 0 END ) AS EvaF,
                       count(T6.LeaderEva) AS CountAll

                    from
	                     T_Party T1
                    inner join M_Shop T2
	                    on T1.ShopCD = T2.ShopCD
                    inner join M_Group T3
	                    on T2.GroupCD =T3.GroupCD 
                    inner join M_Division T4
	                    on T3.DivCD = T4.DivCD
                    inner join T_EvaByLeader T6
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

                sql += " Group by ";
                sql += "   T4.DivCD, ";
                sql += "   T4.DivName, ";
                sql += "   T3.GroupCD, ";
                sql += "   T3.GroupName, ";
                sql += "   T2.ShopCD, ";
                sql += "   T2.ShopName ";

                DyEntityLogic logic = new DyEntityLogic();

                DataTable dt = logic.FillDataTableBySQL(1, sql);

                List<ValRptResultViewModel> modelEva = new List<ValRptResultViewModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    ValRptResultViewModel m = new ValRptResultViewModel();

                    m.DivCD = DataUtil.CStr(dr["DivCD"]);
                    m.DivName = DataUtil.CStr(dr["DivName"]);
                    m.GroupCD = DataUtil.CStr(dr["GroupCD"]);
                    m.GroupName = DataUtil.CStr(dr["GroupName"]);
                    m.ShopCD = DataUtil.CStr(dr["ShopCD"]);
                    m.ShopName = DataUtil.CStr(dr["ShopName"]);
                    m.CountAll = DataUtil.CInt(dr["CountAll"]);
                    m.EvaSS = DataUtil.CInt(dr["EvaSS"]);
                    m.EvaS = DataUtil.CInt(dr["EvaS"]);
                    m.EvaA = DataUtil.CInt(dr["EvaA"]);
                    m.EvaB = DataUtil.CInt(dr["EvaB"]);
                    m.EvaC = DataUtil.CInt(dr["EvaC"]);
                    m.EvaD = DataUtil.CInt(dr["EvaD"]);
                    m.EvaE = DataUtil.CInt(dr["EvaE"]);
                    m.EvaF = DataUtil.CInt(dr["EvaF"]);

                    m.SumAll = m.EvaSS * valSS + m.EvaS * valS + m.EvaA * valA + m.EvaB * valB + m.EvaC * valC + m.EvaD * valD + m.EvaE * valE + m.EvaF * valF;
                    m.Avg = DataUtil.CStr((double)m.SumAll / (m.EvaSS + m.EvaS + m.EvaA + m.EvaB + m.EvaC + m.EvaD + m.EvaE + m.EvaF));

                    modelEva.Add(m);
                }


                #endregion

                #region グループ平均

                var modelGroup = from m in modelEva
                                 group new { p_leadereva = m } by new { m.GroupCD } into g

                                 select new AvgModel
                                 {
                                     CD = g.Key.GroupCD,
                                     Avg = (double)(g.Sum(p => p.p_leadereva.EvaSS * valSS + p.p_leadereva.EvaS * valS + p.p_leadereva.EvaA * valA + p.p_leadereva.EvaB * valB + p.p_leadereva.EvaC * valC + p.p_leadereva.EvaD * valD + p.p_leadereva.EvaE * valE + p.p_leadereva.EvaF * valF))
                                   / g.Sum(p => p.p_leadereva.EvaSS + p.p_leadereva.EvaS + p.p_leadereva.EvaA + p.p_leadereva.EvaB + p.p_leadereva.EvaC + p.p_leadereva.EvaD + p.p_leadereva.EvaE + p.p_leadereva.EvaF)
                                 };

                #endregion

                #region 事業部平均

                var modelDiv = from m in modelEva
                               group new { p_leadereva = m } by new { m.DivCD } into g

                               select new AvgModel
                               {
                                   CD = g.Key.DivCD,
                                   Avg = (double)(g.Sum(p => p.p_leadereva.EvaSS * valSS + p.p_leadereva.EvaS * valS + p.p_leadereva.EvaA * valA + p.p_leadereva.EvaB * valB + p.p_leadereva.EvaC * valC + p.p_leadereva.EvaD * valD + p.p_leadereva.EvaE * valE + p.p_leadereva.EvaF * valF))
                                  / g.Sum(p => p.p_leadereva.EvaSS + p.p_leadereva.EvaS + p.p_leadereva.EvaA + p.p_leadereva.EvaB + p.p_leadereva.EvaC + p.p_leadereva.EvaD + p.p_leadereva.EvaE + p.p_leadereva.EvaF)
                               };

                #endregion

                #region ナショナル平均

                double avg = (double)(modelEva.Sum(p => p.EvaSS * valSS + p.EvaS * valS + p.EvaA * valA + p.EvaB * valB + p.EvaC * valC + p.EvaD * valD + p.EvaE * valE + p.EvaF * valF))
                                  / modelEva.Sum(p => p.EvaSS + p.EvaS + p.EvaA + p.EvaB + p.EvaC + p.EvaD + p.EvaE + p.EvaF);
                               
                string countryAvg = avg.ToString("0.00");
                ViewBag.countryAvg = countryAvg;

                #endregion

                var model = from eva in modelEva
                            join grp in modelGroup on eva.GroupCD equals grp.CD into g_eva_grp
                            from e_grp in g_eva_grp.DefaultIfEmpty(new AvgModel())
                            join div in modelDiv on eva.DivCD equals div.CD into g_eva_div
                            from e_div in g_eva_div.DefaultIfEmpty(new AvgModel())
                            select new ValRptResultViewModel
                            {
                                No = 1,
                                DivCD = eva.DivCD,
                                DivName = eva.DivName,
                                GroupCD = eva.GroupCD,
                                GroupName = eva.GroupName,
                                ShopCD = eva.ShopCD,
                                EvaSS = eva.EvaSS,
                                EvaS = eva.EvaS,
                                EvaA = eva.EvaA,
                                EvaB = eva.EvaB,
                                EvaC = eva.EvaC,
                                EvaD = eva.EvaD,
                                EvaE = eva.EvaE,
                                EvaF = eva.EvaF,
                                CountAll = eva.CountAll,
                                SumAll = eva.SumAll,
                                DblAvg = DataUtil.CDbl(eva.Avg),
                                Avg = double.Parse(eva.Avg.ToString()).ToString("0.00"),
                                AvgGroup = Double2String(e_grp),
                                AvgDiv = Double2String(e_div),

                            };

                if (!string.IsNullOrEmpty(Sort) && Sort == "result")
                {
                    model = model.OrderByDescending(p => p.DblAvg).ThenBy(p => p.DivCD).ThenBy(p => p.GroupCD).ThenBy(p => p.ShopCD);
                }
                else if (!string.IsNullOrEmpty(Sort) && Sort == "shop")
                {
                    if (Show != null && Show.Contains("divgroup"))
                    {
                        model = model.OrderBy(p => p.DivCD).ThenBy(p => p.GroupCD).ThenBy(p => p.ShopCD);
                    }
                    else
                    {
                        model = model.OrderBy(p => p.ShopCD);
                    }
                }

                return model;
            }
        }

        private IEnumerable<ValRptResultViewModel> GetResultModels2(string Time, string Year, string Season, string Months, string DateFrom, string DateTo,string Range, string divisionCD,string ShopCD, string Sort, List<string> Show)
        {

            List<M_Division> Divisions = db.Divisions.ToList();
            List<T_Report> Reports = new List<T_Report>();
            List<T_Party> Partys = new List<T_Party>();
            List<M_Shop> Shops = new List<M_Shop>();
            List<T_EvaByLeader> EvaByLeaders = new List<T_EvaByLeader>();
            List<M_Group> Groups = new List<M_Group>();
            List<M_Staff> Staffs = new List<M_Staff>();

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


            //Partys = db.Partys.Where(p => p.PartyDate.CompareTo(dateFrom) >= 0 && p.PartyDate.CompareTo(dateTo) <= 0).ToList();
            if (Partys.Count == 0 && noCondition) Partys = db.Partys.Where(p => p.PartyDate.CompareTo(dateFrom) >= 0 && p.PartyDate.CompareTo(dateTo) <= 0).ToList();



            //reports
            if (Reports.Count == 0 && noCondition)
            {
                Reports = (from report in db.Reports

                           join party in db.Partys on report.PartyID equals party.PartyID into g_party_party
                           from p_party in g_party_party

                           where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

                           select report).ToList();
            }

            //LeaderEva
            if (EvaByLeaders.Count == 0 && noCondition)
            {
                EvaByLeaders = (from leaderEva in db.EvaByLeaders

                                join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
                                from p_party in g_party_party

                                where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

                                select leaderEva).ToList();
            }
            #endregion

            #region  findAll
            if (Shops.Count == 0) Shops = db.Shops.ToList();
            if (Staffs.Count == 0) Staffs = db.Staffs.ToList();
            if (Groups.Count == 0) Groups = db.Groups.ToList();
            #endregion

            var Models = from party in Partys
                         join shop in Shops on party.ShopCD equals shop.ShopCD into g_party_shop
                         from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
                         join grp in Groups on p_shop.GroupCD equals grp.GroupCD into g_shop_grp
                         from s_grp in g_shop_grp.DefaultIfEmpty(new M_Group())
                         join divsion in Divisions on s_grp.DivCD equals divsion.DivCD into g_shop_div
                         from s_div in g_shop_div.DefaultIfEmpty(new M_Division())
                         join staff in Staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                         from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
                         join evabyleader in EvaByLeaders on party.PartyID equals evabyleader.PartyID into g_party_staffeva
                         from p_leadereva in g_party_staffeva.DefaultIfEmpty(new T_EvaByLeader())
                         join report in Reports on party.PartyID equals report.PartyID into g_party_report
                         from p_report in g_party_report.DefaultIfEmpty(new T_Report())

                         where (party.PartyDate.CompareTo(dateFrom) >= 0 && party.PartyDate.CompareTo(dateTo) <= 0) && (p_leadereva.LeaderEva == "SS" || p_leadereva.LeaderEva == "S" || p_leadereva.LeaderEva == "A" || p_leadereva.LeaderEva == "B" || p_leadereva.LeaderEva == "C" || p_leadereva.LeaderEva == "D" || p_leadereva.LeaderEva == "E" || p_leadereva.LeaderEva == "F") && p_report.TelFlg == "1"
                         select new
                         {
                             party = party,
                             p_shop = p_shop,
                             s_grp = s_grp,
                             s_div = s_div,
                             p_staff = p_staff,
                             p_leadereva = p_leadereva
                         };

            var countryModels = Models;

            if (!string.IsNullOrEmpty(Sort) && Sort == "shop")
            {
                Models.OrderBy(p => p.s_div.DivCD).ThenBy(p => p.s_grp.GroupCD).ThenBy(p => p.p_shop.ShopCD);
            }

            if (Models.ToList().Count == 0)
            {
                return null;
            }
            else
            {

                int valSS = 5;
                int valS = 4;
                int valA = 3;
                int valB = 2;
                int valC = 1;
                int valD = -1;
                int valE = -2;
                int valF = -3;


                #region 評価平均

                var modelEva = from m in Models
                               group new { p_leadereva = m.p_leadereva } by new { m.party.ShopCD, m.p_shop.ShopName, m.s_div.DivCD, m.s_div.DivName, m.s_grp.GroupCD, m.s_grp.GroupName } into g

                               select new
                               {
                                   No = 1,
                                   DivCD = g.Key.DivCD,
                                   DivName = g.Key.DivName,
                                   GroupCD = g.Key.GroupCD,
                                   GroupName = g.Key.GroupName,
                                   ShopCD = g.Key.ShopCD,
                                   ShopName = g.Key.ShopName,

                                   EvaSS = g.Count(p => p.p_leadereva.LeaderEva == "SS"),
                                   EvaS = g.Count(p => p.p_leadereva.LeaderEva == "S"),
                                   EvaA = g.Count(p => p.p_leadereva.LeaderEva == "A"),
                                   EvaB = g.Count(p => p.p_leadereva.LeaderEva == "B"),
                                   EvaC = g.Count(p => p.p_leadereva.LeaderEva == "C"),
                                   EvaD = g.Count(p => p.p_leadereva.LeaderEva == "D"),
                                   EvaE = g.Count(p => p.p_leadereva.LeaderEva == "E"),
                                   EvaF = g.Count(p => p.p_leadereva.LeaderEva == "F"),
                                   CountAll = g.Count(),
                                   SumAll = g.Count(p => p.p_leadereva.LeaderEva == "SS") * valSS + g.Count(p => p.p_leadereva.LeaderEva == "S") * valS + g.Count(p => p.p_leadereva.LeaderEva == "A") * valA + g.Count(p => p.p_leadereva.LeaderEva == "B") * valB + g.Count(p => p.p_leadereva.LeaderEva == "C") * valC + g.Count(p => p.p_leadereva.LeaderEva == "D") * valD + g.Count(p => p.p_leadereva.LeaderEva == "E") * valE + g.Count(p => p.p_leadereva.LeaderEva == "F") * valF,
                                   Avg = (double)(g.Count(p => p.p_leadereva.LeaderEva == "SS") * valSS + g.Count(p => p.p_leadereva.LeaderEva == "S") * valS + g.Count(p => p.p_leadereva.LeaderEva == "A") * valA + g.Count(p => p.p_leadereva.LeaderEva == "B") * valB + g.Count(p => p.p_leadereva.LeaderEva == "C") * valC + g.Count(p => p.p_leadereva.LeaderEva == "D") * valD + g.Count(p => p.p_leadereva.LeaderEva == "E") * valE + g.Count(p => p.p_leadereva.LeaderEva == "F") * valF)
                                   / g.Count(p => p.p_leadereva.LeaderEva == "SS" || p.p_leadereva.LeaderEva == "S" || p.p_leadereva.LeaderEva == "A" || p.p_leadereva.LeaderEva == "B" || p.p_leadereva.LeaderEva == "C" || p.p_leadereva.LeaderEva == "D" || p.p_leadereva.LeaderEva == "E" || p.p_leadereva.LeaderEva == "F")
                               };

                #endregion

                #region グループ平均

                var modelGroup = from m in Models
                                 group new { p_leadereva = m.p_leadereva } by new { m.s_grp.GroupCD } into g

                                 select new AvgModel
                                 {
                                     CD = g.Key.GroupCD,
                                     Avg = (double)(g.Count(p => p.p_leadereva.LeaderEva == "SS") * valSS + g.Count(p => p.p_leadereva.LeaderEva == "S") * valS + g.Count(p => p.p_leadereva.LeaderEva == "A") * valA + g.Count(p => p.p_leadereva.LeaderEva == "B") * valB + g.Count(p => p.p_leadereva.LeaderEva == "C") * valC + g.Count(p => p.p_leadereva.LeaderEva == "D") * valD + g.Count(p => p.p_leadereva.LeaderEva == "E") * valE + g.Count(p => p.p_leadereva.LeaderEva == "F") * valF)
                                   / g.Count(p => p.p_leadereva.LeaderEva == "SS" || p.p_leadereva.LeaderEva == "S" || p.p_leadereva.LeaderEva == "A" || p.p_leadereva.LeaderEva == "B" || p.p_leadereva.LeaderEva == "C" || p.p_leadereva.LeaderEva == "D" || p.p_leadereva.LeaderEva == "E" || p.p_leadereva.LeaderEva == "F")
                                 };

                #endregion

                #region 事業部平均

                var modelDiv = from m in Models
                               group new { p_leadereva = m.p_leadereva } by new { m.s_div.DivCD } into g

                               select new AvgModel
                               {
                                   CD = g.Key.DivCD,
                                   Avg = (double)(g.Count(p => p.p_leadereva.LeaderEva == "SS") * valSS + g.Count(p => p.p_leadereva.LeaderEva == "S") * valS + g.Count(p => p.p_leadereva.LeaderEva == "A") * valA + g.Count(p => p.p_leadereva.LeaderEva == "B") * valB + g.Count(p => p.p_leadereva.LeaderEva == "C") * valC + g.Count(p => p.p_leadereva.LeaderEva == "D") * valD + g.Count(p => p.p_leadereva.LeaderEva == "E") * valE + g.Count(p => p.p_leadereva.LeaderEva == "F") * valF)
                                    / g.Count(p => p.p_leadereva.LeaderEva == "SS" || p.p_leadereva.LeaderEva == "S" || p.p_leadereva.LeaderEva == "A" || p.p_leadereva.LeaderEva == "B" || p.p_leadereva.LeaderEva == "C" || p.p_leadereva.LeaderEva == "D" || p.p_leadereva.LeaderEva == "E" || p.p_leadereva.LeaderEva == "F")
                               };

                #endregion

                #region ナショナル平均

                double avg = (double)(countryModels.Count(p => p.p_leadereva.LeaderEva == "SS") * valSS + countryModels.Count(p => p.p_leadereva.LeaderEva == "S") * valS + countryModels.Count(p => p.p_leadereva.LeaderEva == "A") * valA + countryModels.Count(p => p.p_leadereva.LeaderEva == "B") * valB + countryModels.Count(p => p.p_leadereva.LeaderEva == "C") * valC + countryModels.Count(p => p.p_leadereva.LeaderEva == "D") * valD + countryModels.Count(p => p.p_leadereva.LeaderEva == "E") * valE + countryModels.Count(p => p.p_leadereva.LeaderEva == "F") * valF)
                                       / countryModels.Count(p => p.p_leadereva.LeaderEva == "SS" || p.p_leadereva.LeaderEva == "S" || p.p_leadereva.LeaderEva == "A" || p.p_leadereva.LeaderEva == "B" || p.p_leadereva.LeaderEva == "C" || p.p_leadereva.LeaderEva == "D" || p.p_leadereva.LeaderEva == "E" || p.p_leadereva.LeaderEva == "F");

                string countryAvg = avg.ToString("0.00");
                ViewBag.countryAvg = countryAvg;
 
                #endregion

                var model = from eva in modelEva
                            join grp in modelGroup on eva.GroupCD equals grp.CD into g_eva_grp
                            from e_grp in g_eva_grp.DefaultIfEmpty(new AvgModel())
                            join div in modelDiv on eva.DivCD equals div.CD into g_eva_div
                            from e_div in g_eva_div.DefaultIfEmpty(new AvgModel())
                            select new ValRptResultViewModel
                            {
                                No = 1,
                                DivCD = eva.DivCD,
                                DivName = eva.DivName,
                                GroupCD = eva.GroupCD,
                                GroupName = eva.GroupName,
                                ShopCD = eva.ShopCD,
                                EvaSS = eva.EvaSS,
                                EvaS = eva.EvaS,
                                EvaA = eva.EvaA,
                                EvaB = eva.EvaB,
                                EvaC = eva.EvaC,
                                EvaD = eva.EvaD,
                                EvaE = eva.EvaE,
                                EvaF = eva.EvaF,
                                CountAll = eva.CountAll,
                                SumAll = eva.SumAll,
                                DblAvg = eva.Avg,
                                Avg = double.Parse(eva.Avg.ToString()).ToString("0.00"),
                                AvgGroup = Double2String(e_grp),
                                AvgDiv = Double2String(e_div),

                            };

                if (!string.IsNullOrEmpty(Sort) && Sort == "result")
                {
                    model = model.OrderByDescending(p => p.DblAvg).ThenBy(p => p.DivCD).ThenBy(p => p.GroupCD).ThenBy(p => p.ShopCD);
                }
                else if (!string.IsNullOrEmpty(Sort) && Sort == "shop")
                {
                    if (Show != null && Show.Contains("divgroup"))
                    {
                        model = model.OrderBy(p => p.DivCD).ThenBy(p => p.GroupCD).ThenBy(p => p.ShopCD);
                    }
                    else
                    {
                        model = model.OrderBy(p => p.ShopCD);
                    }
                }

                return model;
            }
        }

        private string Double2String(AvgModel data)
        {
            if (data.CD == null) {
                return "-";
            }

            return data.Avg.ToString("0.00");
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns></returns>
        public ActionResult ResultCsv(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string Sort, List<string> Show)
        {
            var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start

            if (ShopCD == null)
            {
                shopCD_Change = "";
            }
            else
            {
                shopCD_Change = ShopCD.Trim().ToUpper();
            }                     //---2014/09/10---LI---店舗番号と担当者番号を格式化---end
            var querymodels = GetResultModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change, Sort, Show);

            if (querymodels == null)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            List<ValRptResultViewModel> models = querymodels.ToList() ;

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\アンケート評価結果_{0}.csv", date);


            if (Show == null || Show.Count == 0)
            {
                //明細データ
                List<ValRptResultReportDetailModel4> details = new List<ValRptResultReportDetailModel4>();

                int index = 1;
                foreach (ValRptResultViewModel viewModel in models)
                {
                    ValRptResultReportDetailModel4 detail = new ValRptResultReportDetailModel4();

                    detail.No = index;

                    detail.Avg = viewModel.Avg;
                   
                    detail.CountAll = viewModel.CountAll;
                   
                    detail.SumAll = viewModel.SumAll;
                    detail.ShopName = viewModel.ShopCD;

                    details.Add(detail);

                    index++;
                }

                CsvUtils.ModlesToCsv<ValRptResultReportDetailModel4>(basePath + fileName, details);
            }
            else if (Show.Contains("eva") && Show.Contains("divgroup"))
            {
                //明細データ
                List<ValRptResultReportDetailModel> details = new List<ValRptResultReportDetailModel>();

                int index = 1;
                foreach (ValRptResultViewModel viewModel in models)
                {
                    ValRptResultReportDetailModel detail = new ValRptResultReportDetailModel();

                    detail.No = index;

                    detail.Avg = viewModel.Avg;
                    detail.AvgDiv = viewModel.AvgDiv;
                    detail.AvgGroup = viewModel.AvgGroup;
                    detail.CountAll = viewModel.CountAll;
                    detail.DivName = viewModel.DivName;
                    detail.EvaA = viewModel.EvaA;
                    detail.EvaB = viewModel.EvaB;
                    detail.EvaC = viewModel.EvaC;
                    detail.EvaD = viewModel.EvaD;
                    detail.EvaE = viewModel.EvaE;
                    detail.EvaF = viewModel.EvaF;
                    detail.EvaSS = viewModel.EvaSS;
                    detail.EvaS = viewModel.EvaS;
                    detail.GroupName = viewModel.GroupName;
                    detail.SumAll = viewModel.SumAll;
                    detail.ShopName = viewModel.ShopCD;

                    details.Add(detail);

                    index++;
                }

                CsvUtils.ModlesToCsv<ValRptResultReportDetailModel>(basePath + fileName, details);
            }
            else if (Show.Contains("eva"))
            {
                //明細データ
                List<ValRptResultReportDetailModel2> details = new List<ValRptResultReportDetailModel2>();

                int index = 1;
                foreach (ValRptResultViewModel viewModel in models)
                {
                    ValRptResultReportDetailModel2 detail = new ValRptResultReportDetailModel2();

                    detail.No = index;

                    detail.Avg = viewModel.Avg;
                    detail.CountAll = viewModel.CountAll;
                    detail.EvaA = viewModel.EvaA;
                    detail.EvaB = viewModel.EvaB;
                    detail.EvaC = viewModel.EvaC;
                    detail.EvaD = viewModel.EvaD;
                    detail.EvaE = viewModel.EvaE;
                    detail.EvaF = viewModel.EvaF;
                    detail.EvaSS = viewModel.EvaSS;
                    detail.EvaS = viewModel.EvaS;
                    detail.SumAll = viewModel.SumAll;
                    detail.ShopName = viewModel.ShopCD;

                    details.Add(detail);

                    index++;
                }

                CsvUtils.ModlesToCsv<ValRptResultReportDetailModel2>(basePath + fileName, details);
            }
            else if (Show.Contains("divgroup"))
            {
                //明細データ
                List<ValRptResultReportDetailModel3> details = new List<ValRptResultReportDetailModel3>();

                int index = 1;
                foreach (ValRptResultViewModel viewModel in models)
                {
                    ValRptResultReportDetailModel3 detail = new ValRptResultReportDetailModel3();

                    detail.No = index;

                    detail.Avg = viewModel.Avg;
                    detail.AvgDiv = viewModel.AvgDiv;
                    detail.AvgGroup = viewModel.AvgGroup;
                    detail.CountAll = viewModel.CountAll;
                    detail.DivName = viewModel.DivName;
                    detail.GroupName = viewModel.GroupName;
                    detail.SumAll = viewModel.SumAll;
                    detail.ShopName = viewModel.ShopCD;

                    details.Add(detail);

                    index++;
                }

                CsvUtils.ModlesToCsv<ValRptResultReportDetailModel3>(basePath + fileName, details);
            }

           

            return Json(new { Path = string.Format("/CSV/アンケート評価結果_{0}.csv", date), ResultType = EnumResultType.Success });
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns></returns>
        public ActionResult csv(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD)
        {
            var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start

            if (ShopCD == null)
            {
                shopCD_Change = "";
            }
            else
            {
                shopCD_Change = ShopCD.Trim().ToUpper();
            }                     //---2014/09/10---LI---店舗番号と担当者番号を格式化---end
            List<ValResultViewModel> list = GetResultList(Time, Year, Season, Months, DateFrom, DateTo,Range,divisionCD,shopCD_Change);

            if (list == null)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\統計結果_{0}.csv", date);

            CsvUtils.ModlesToCsv<ValResultViewModel>(basePath + fileName, list);

            return Json(new { Path = string.Format("/CSV/統計結果_{0}.csv", date), ResultType = EnumResultType.Success });

        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns></returns>
        public ActionResult staffCsv(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD)
        {
            var shopCD_Change = "";//---2014/09/10---LI---店舗番号と担当者番号を格式化---start

            if (ShopCD == null)
            {
                shopCD_Change = "";
            }
            else
            {
                shopCD_Change = ShopCD.Trim().ToUpper();
            }                     //---2014/09/10---LI---店舗番号と担当者番号を格式化---end
            List<ValResultStaffViewModel> list = GetStaffResultList(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change);

            if (list == null)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\担当者別統計結果_{0}.csv", date);

            CsvUtils.ModlesToCsv<ValResultStaffViewModel>(basePath + fileName, list);

            return Json(new { Path = string.Format("/CSV/担当者別統計結果_{0}.csv", date), ResultType = EnumResultType.Success });

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

    public class AvgModel
    {
        public string CD { get; set; }
        public double Avg { get; set; }
    }
}
