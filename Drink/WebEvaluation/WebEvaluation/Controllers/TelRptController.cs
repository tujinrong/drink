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
using WebEvaluation.ReportModels;
using System.Text.RegularExpressions;
using System.Threading;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Logic;
using SafeNeeds.DySmat.Util;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.IO.Compression;

namespace WebEvaluation.Controllers
{
    [AuthenticationFilter(Order = 1)]
    [ExceptionFilter(Order = 2)]
    public class TelRptController : Controller
    {
        private EvaluationContext db = new EvaluationContext();
        private Dictionary<string, string> optionSetDic = null;

        /*テレフォンレポート情報登録-----2014-07-04-----李*/
        public ActionResult TelRptCreate(TelRptCreateViewModel model, int page, string PartyID, string ShopCD, string PartyDateFrom, string PartyDateTo, string UpdateTime, string UpdateUser, string UpdateUserID, int id, string actionType, string mode, string TelState, string ReportState, string isPartyReport)
        {
            ViewBag.page = page;
            ViewBag.ShopCD = ShopCD;
            ViewBag.PartyDateFrom = PartyDateFrom;
            ViewBag.PartyDateTo = PartyDateTo;
            ViewBag.mode = mode;
            ViewBag.TelState = TelState;
            ViewBag.ReportState = ReportState;
            ViewBag.isPartyReport = isPartyReport;

             bool haita = false;

            if (null != model.report)
            {
                T_Report report = model.report;

                T_Report _reportData = db.Reports.Find(int.Parse(PartyID));

                if (_reportData != null)
                {
                    if (report.PartyID == 0)
                    {
                        haita = true;
                    }
                    else
                    {
                        //20141010   李梁　_reportData.UpdateTime.Value ⇒ _reportData.UpdateTime
                        if (UpdateTime == (_reportData.UpdateTime == null ? "" : _reportData.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss")))
                        //20141010   李梁
                        {

                            if (_reportData.TelFlg != report.TelFlg
                                || _reportData.Remark != report.Remark
                                || _reportData.Memo != report.Memo) {

                                    _reportData.TelFlg = report.TelFlg;
                                    _reportData.Remark = report.Remark;
                                    _reportData.Memo = report.Memo;
                                    _reportData.UpdateTime = CommonUtils.getLocalDateTime();
                                    _reportData.UpdateUserID = (Session["user"] as UserSession).StaffCD;

                                    db.Entry(_reportData).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            haita = true;
                        }
                    }
                }
                else
                {
                    report.PartyID = int.Parse(PartyID);
                    report.UpdateTime = CommonUtils.getLocalDateTime();
                    report.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                    db.Reports.Add(report);
                }

                if (!haita)
                {
                    db.SaveChanges();
                    ViewBag.msg = "データを保存しました。";
                    if (actionType != "1")
                    {
                        return RedirectToAction("PartyIndex", "Party", new { page = page, ShopCD = ShopCD, PartyDateFrom = PartyDateFrom, PartyDateTo = PartyDateTo, isPostBack = "1", TelState = TelState, ReportState = ReportState, msg = "データを保存しました。" });
                    }
                    else 
                    {
                        return RedirectToAction("TelRptCreate", "TelRpt", new { page = page, PartyID = PartyID, ShopCD = ShopCD, PartyDateFrom = PartyDateFrom, PartyDateTo = PartyDateTo, TelState = TelState, ReportState = ReportState, isPartyReport = isPartyReport, id = id, msg = "データを保存しました。" });
                    }
                }
                else
                {
                    string updateUser = "";
                    M_Staff staff = db.Staffs.Find(_reportData.UpdateUserID);
                    if (staff != null)
                    {
                        updateUser = staff.StaffName;
                    }

                    ViewBag.msg = "編集中のデータは他の端末(" + updateUser + ")より更新されました。更新できません。一覧画面から、再度入力してください。";
                    ViewBag.msgType = "error";
                }
            }

            T_Party _party = db.Partys.Find(id);
            T_Report _report = db.Reports.Find(id);
            T_EvaByStaff _evaByStaff = db.EvaByStaffs.Find(id);

            if (null != _report)
            {
                if (string.IsNullOrEmpty(_report.TelFlg))
                {
                    _report.TelFlg = "0";
                }

                if (null != _report.UpdateTime)
                {
                    //20141010　李梁　_report.UpdateTime.Value ⇒ _report.UpdateTime
                    ViewBag.UpdateTime = _report.UpdateTime == null ? "" : _report.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                    //20141010　李梁
                    ViewBag.UpdateUser = GetStaffName(_report.UpdateUserID);
                    ViewBag.UpdateUserID = _report.UpdateUserID;
                }

                if (haita && null != model.report)
                {
                    _report.TelFlg = model.report.TelFlg;
                    _report.Memo = model.report.Memo;
                    ViewBag.UpdateTime = UpdateTime;
                    ViewBag.UpdateUser = UpdateUser;
                    ViewBag.UpdateUserID = UpdateUserID;
                }
                ViewBag.telFlg = _report.TelFlg;
            }

            if (null != _party && _party.TantoCD.Length > 0)
            {
                _party.TantoCD = GetStaffName(_party.TantoCD);
            }

            if (null != _evaByStaff)
            {
                if (null != _evaByStaff.Eva1StaffCD && _evaByStaff.Eva1StaffCD.Length > 0)
                {
                    _evaByStaff.Eva1StaffCD = GetStaffName(_evaByStaff.Eva1StaffCD);
                }

                if (null != _evaByStaff.Eva2StaffCD && _evaByStaff.Eva2StaffCD.Length > 0)
                {
                    _evaByStaff.Eva2StaffCD = GetStaffName(_evaByStaff.Eva2StaffCD);
                }

                if (null != _evaByStaff.Eva3StaffCD && _evaByStaff.Eva3StaffCD.Length > 0)
                {
                    _evaByStaff.Eva3StaffCD = GetStaffName(_evaByStaff.Eva3StaffCD);
                }
            }

            model = new TelRptCreateViewModel();

            model.party = _party == null ? new T_Party() : _party; ;
            model.report = _report == null ? new T_Report() : _report;
            model.evaBystaff = _evaByStaff == null ? new T_EvaByStaff() : _evaByStaff;

            return View(model);
        }

        public ActionResult ReportExcel(DynamicsViewRequest request)
        {

            DyEntityLogic logic = new DyEntityLogic();
            //request.GetPageCount = false;
            request.pageNumber = 1;
            request.GetPageSize = 2000;

            PageViewResult list = logic.GetList(request);

            if (list.DataTable.Rows.Count == 0)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            //挙式レポート
            request.EntityName = "T_PartyReport";
            request.ViewName = "PartyReportIndex";
            PageViewResult listReport = logic.GetList(request);

            //挙式タイム
            request.EntityName = "T_PartyTime";
            request.ViewName = "PartyTimeIndex";
            PageViewResult listTime = logic.GetList(request);

            //挙式料理
            request.EntityName = "T_PartyFood";
            request.ViewName = "PartyFoodIndex";
            PageViewResult listFood = logic.GetList(request);

            //挙式メンバー
            request.EntityName = "T_PartyMember";
            request.ViewName = "PartyMemberIndex";
            PageViewResult listMember = logic.GetList(request);

            //挙式メンバータイム
            request.EntityName = "T_PartyMemberTime";
            request.ViewName = "PartyMemberTimeIndex";
            PageViewResult listMemberTime = logic.GetList(request);

            if (optionSetDic == null)
            {
                optionSetDic = new Dictionary<string, string>();
                DataTable optionSetDt = logic.GetOptionSet(request.ProjID, null, "ja-jp", "ja-jp");
                foreach (DataRow row in optionSetDt.Rows)
                {
                    string key = DataUtil.CStr(row["OptSetName"]) + "_" + DataUtil.CStr(row["CD"]);
                    if (optionSetDic.ContainsKey(key)) continue;

                    string value = DataUtil.CStr(row["Name"]);
                    optionSetDic.Add(key, value);
                }
            }

            ExcelReport report = null;

            //データ
            List<IReportData> datas = new List<IReportData>();

            //headerデータ
            PartyReportModel header = new PartyReportModel();

            int partyIndex = 1;
            int colCount = 5;
            string tempShopCD = "";
            string tempPartyID = "";
            List<string> listReportPath = new List<string>();

            foreach (DataRow dr in list.DataTable.Rows)
            {
                string ShopCD = DataUtil.CStr(dr["ShopCD"]);
                DateTime pd = DataUtil.CDate(dr["PartyDate"]);

                if (ShopCD != tempShopCD)
                {
                    if (report != null)
                    {
                        datas.Add(header);
                        report.Data = datas;

                        //帳票生成
                        report.CreateReport();
                        listReportPath.Add(report.DownLoadPath());
                    }

                    report = new ExcelReport();
                    report.Handler = new ReportExcelHandler();


                    //ReportType
                    report.ReportType = EnumReportType.ListReport;
                    //report.isAutoRowHeight = true;

                    report.Template = "パーティ報告書.xls";

                    //Template
                    report.FileName = "パーティ報告書" + "_" + ShopCD;

                    //データ
                    datas = new List<IReportData>();

                    //headerデータ
                    header = new PartyReportModel();
                    partyIndex = 1;
                    colCount = 5;
                }
                else
                {
                    if (DataUtil.CStr(dr["PartyID"]) != tempPartyID)
                    {
                        datas.Add(header);
                        //headerデータ
                        header = new PartyReportModel();
                        partyIndex = 1;
                        colCount = 5;
                    }
                }

                tempShopCD = ShopCD;
                tempPartyID = DataUtil.CStr(dr["PartyID"]);

                header.SetFieldValue("ShopCD", 0, dr["ShopCD"]);
                header.SetFieldValue("PartyMonth", 0, pd.Month + "月");


                header.SetFieldValue("PartyDate", partyIndex, string.Format("{0:yyyy/MM/dd}", dr["PartyDate"]));
                header.SetFieldValue("BrideFamilyName", partyIndex, dr["BrideName"]);
                header.SetFieldValue("GroomFamilyName", partyIndex, dr["GroomName"]);
                header.SetFieldValue("TantoName", partyIndex, dr["StaffName"]);
                header.SetFieldValue("ShopName", partyIndex, dr["ShopName"]);
                header.SetFieldValue("HallType", partyIndex, dr["HallType"]);
                header.SetFieldValue("StartTime", partyIndex, DataUtil.CStr(dr["StartTime"]).Replace(":", "："));


                //挙式レポート
                var q1 = from dt1 in listReport.DataTable.AsEnumerable()//查询  
                         where DataUtil.CStr(dt1["PartyID"]) == DataUtil.CStr(dr["PartyID"])
                         select dt1;

                DataRow reportDr = null;
                foreach (var item in q1)//显示查询结果  
                {
                    reportDr = item;
                    break;
                }

                string key = "";

                key = "PartyDiv" + "_" + DataUtil.CStr(reportDr["PartyDiv"]);
                string partyDiv = optionSetDic.ContainsKey(key) ? optionSetDic[key] : "";

                key = "TableCloth" + "_" + DataUtil.CStr(reportDr["TableCross"]);
                string tableCross = optionSetDic.ContainsKey(key) ? optionSetDic[key] : "";

                key = "WeddingStyle" + "_" + DataUtil.CStr(reportDr["PartyStyleCD"]);
                string partyStyle = optionSetDic.ContainsKey(key) ? optionSetDic[key] : "";

                key = "FoodStyle" + "_" + DataUtil.CStr(reportDr["FoodStyleCD"]);
                string foodStyle = optionSetDic.ContainsKey(key) ? optionSetDic[key] : "";

                key = "WDrink" + "_" + DataUtil.CStr(reportDr["Wdrink"]);
                string wdrink = optionSetDic.ContainsKey(key) ? optionSetDic[key] : "";

                key = "Diesel" + "_" + DataUtil.CStr(reportDr["Desl"]);
                string desl = optionSetDic.ContainsKey(key) ? optionSetDic[key] : "";

                header.SetFieldValue("ReporterName", partyIndex, reportDr["ReporterCD"]);
                header.SetFieldValue("AdultCnt", partyIndex, reportDr["AdultCnt"]);
                header.SetFieldValue("HalfCnt", partyIndex, reportDr["HalfCnt"]);
                header.SetFieldValue("ChildrenCnt", partyIndex, reportDr["ChildrenCnt"]);
                header.SetFieldValue("SeatOnlyCnt", partyIndex, reportDr["SeatOnlyCnt"]);
                header.SetFieldValue("TableCnt", partyIndex, reportDr["TableCnt"]);
                header.SetFieldValue("PartyDiv", partyIndex, partyDiv);
                header.SetFieldValue("TableCross", partyIndex, tableCross);
                header.SetFieldValue("PartyStyleName", partyIndex, partyStyle);
                header.SetFieldValue("FoodStyleName", partyIndex, foodStyle);

                string foodPricce = DataUtil.IsNullOrEmpty(reportDr["FoodPricce"])?"":DataUtil.CInt(reportDr["FoodPricce"]).ToString("N");
                string drinkPrice = DataUtil.IsNullOrEmpty(reportDr["DrinkPrice"]) ? "" : DataUtil.CInt(reportDr["DrinkPrice"]).ToString("N");
                int index = -1;
                index = foodPricce.LastIndexOf('.');
                if (index >= 0)
                {
                    foodPricce = foodPricce.Substring(0, index);
                }
                index = drinkPrice.LastIndexOf('.');
                if (index >= 0)
                {
                    drinkPrice = drinkPrice.Substring(0, index);
                }

                header.SetFieldValue("FoodPricce", partyIndex, foodPricce);
                header.SetFieldValue("DrinkPrice", partyIndex, drinkPrice);
                header.SetFieldValue("Wdrink", partyIndex, wdrink);
                header.SetFieldValue("Desl", partyIndex, desl);


                header.SetFieldValue("RestRoomFlg", partyIndex, DataUtil.CStr(reportDr["RestRoomFlg"]) == "True" ? "○" : "×");
                header.SetFieldValue("AnketFlg", partyIndex, DataUtil.CStr(reportDr["AnketFlg"]) == "True" ? "○" : "×");
                header.SetFieldValue("SecondParty", partyIndex, DataUtil.CStr(reportDr["SecondParty"]) == "True" ? "○" : "×");
                header.SetFieldValue("OrderMemberCnt", partyIndex, reportDr["OrderMemberCnt"]);
                header.SetFieldValue("ActMemberCnt", partyIndex, reportDr["ActMemberCnt"]);
                header.SetFieldValue("LessMemberCnt", partyIndex, reportDr["LessMemberCnt"]);
                header.SetFieldValue("Coment1", partyIndex, reportDr["Coment1"]);
                header.SetFieldValue("Coment2", partyIndex, reportDr["Coment2"]);
                header.SetFieldValue("Coment3", partyIndex, reportDr["Coment3"]);
                header.SetFieldValue("ComentSkill", partyIndex, reportDr["ComentSkill"]);
                header.SetFieldValue("Memo", partyIndex, reportDr["Memo"]);

                string partyBeginTime = "";
                string partyEndTime = "";
                string partyBeginTimeTO = "";
                string partyEndTimeTO = "";

                //挙式タイム
                var qTime = from dt1 in listTime.DataTable.AsEnumerable()
                            where DataUtil.CStr(dt1["PartyID"]) == DataUtil.CStr(dr["PartyID"])
                            select dt1;
                foreach (var item in qTime)
                {
                    //switch (DataUtil.CStr(item["TimeCD"])) { 

                    //}    

                    switch (DataUtil.CStr(item["TimeCD"]))
                    {
                        case "01": //ロビー解放 

                            if (DataUtil.CStr(item["TimeType"]) == "2")
                            {
                                header.SetFieldValue("PartyTime_OrderTime_I", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":", "："));
                            }

                            break;
                        case "02": //挙式
                            if (DataUtil.CStr(item["TimeType"]) == "1")
                            {
                                header.SetFieldValue("PartyTime_OrderTime_II", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":", "："));
                                header.SetFieldValue("PartyTime_OrderTimeTO_II", partyIndex, DataUtil.CStr(item["EndTime"]).Replace(":", "："));


                                header.SetFieldValue("PartyTime_DelayTime_II", partyIndex, item["DelayTime"]);
                            }

                            if (DataUtil.CStr(item["TimeType"]) == "2")
                            {
                                header.SetFieldValue("PartyTime_ActTime_II", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":", "："));
                                header.SetFieldValue("PartyTime_ActTimeTO_II", partyIndex, DataUtil.CStr(item["EndTime"]).Replace(":", "："));


                                header.SetFieldValue("PartyTime_DelayTime_II", partyIndex, item["DelayTime"]);
                            }
                            break;
                        case "03": //披露宴
                            if (DataUtil.CStr(item["TimeType"]) == "1")
                            {
                                header.SetFieldValue("PartyTime_OrderTime_III", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":", "："));
                                header.SetFieldValue("PartyTime_OrderTimeTO_III", partyIndex, DataUtil.CStr(item["EndTime"]).Replace(":", "："));


                                header.SetFieldValue("PartyTime_DelayTime_III", partyIndex, item["DelayTime"]);
                                partyBeginTime = DataUtil.CStr(item["BeginTime"]);
                                partyEndTime = DataUtil.CStr(item["EndTime"]);
                            }

                            if (DataUtil.CStr(item["TimeType"]) == "2")
                            {
                                header.SetFieldValue("PartyTime_ActTime_III", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":", "："));
                                header.SetFieldValue("PartyTime_ActTimeTO_III", partyIndex, DataUtil.CStr(item["EndTime"]).Replace(":", "："));


                                header.SetFieldValue("PartyTime_DelayTime_III", partyIndex, item["DelayTime"]);
                                partyBeginTimeTO = DataUtil.CStr(item["BeginTime"]);
                                partyEndTimeTO = DataUtil.CStr(item["EndTime"]);
                            }
                            break;
                        case "04": //乾杯時間

                            if (DataUtil.CStr(item["TimeType"]) == "2")
                            {
                                header.SetFieldValue("PartyTime_OrderTime_IV", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":", "："));
                            }

                            break;
                        case "05": //パーティ時間 
                            break;
                        default:
                            break;
                    }
                }

                int rangeH = 0;
                int rangeM = 0;
                string rangeT = "";
                int partyOrdTime = DataUtil.CInt(reportDr["PartyOrdTime"]);
                int partyActTime = DataUtil.CInt(reportDr["PartyActTime"]);
                int partyActOrdTime = DataUtil.CInt(reportDr["PartyActOrdTime"]);

                rangeT = "";
                string partyOrdTimeRange = "";
                if (partyActOrdTime > 0)
                {
                    rangeH = partyActOrdTime / 60;
                    rangeM = partyActOrdTime % 60;
                    rangeT = string.Format("{0}時間 {1}分", DataUtil.CStr(rangeH).PadLeft(2, ' '), DataUtil.CStr(rangeM).PadLeft(2, ' '));
                    header.SetFieldValue("PartyTime_OrderTime_V", partyIndex, rangeT);

                    if ((partyActOrdTime - partyOrdTime) <= 0)
                    {
                        partyOrdTimeRange = "" + (partyActOrdTime - partyOrdTime);
                        partyOrdTimeRange += "分";
                    }
                    else
                    {
                        partyOrdTimeRange = "+" + (partyActOrdTime - partyOrdTime);
                        partyOrdTimeRange += "分";
                        partyOrdTimeRange = "&lt;span style='color: rgb(255, 0, 0);'&gt;" + partyOrdTimeRange + "&lt;/span&gt;";
                    }
                    header.SetFieldValue("PartyOrdTimeRange", partyIndex, partyOrdTimeRange);
                }
                else
                {
                    header.SetFieldValue("PartyTime_OrderTime_V", partyIndex, rangeT);
                    header.SetFieldValue("PartyOrdTimeRange", partyIndex, partyOrdTimeRange);
                }

                rangeT = "";
                string partyActTimeRange = "";
                if (partyActTime > 0)
                {
                    rangeH = partyActTime / 60;
                    rangeM = partyActTime % 60;
                    rangeT = string.Format("{0}時間 {1}分", DataUtil.CStr(rangeH).PadLeft(2, ' '), DataUtil.CStr(rangeM).PadLeft(2, ' '));
                    header.SetFieldValue("PartyTime_ActTime_V", partyIndex, rangeT);

                    if ((partyActTime - partyOrdTime) <= 0)
                    {
                        partyActTimeRange = "" + (partyActTime - partyOrdTime);
                        partyActTimeRange += "分";
                    }
                    else
                    {
                        partyActTimeRange = "+" + (partyActTime - partyOrdTime);
                        partyActTimeRange += "分";
                        partyActTimeRange = "&lt;span style='color: rgb(255, 0, 0);'&gt;" + partyActTimeRange + "&lt;/span&gt;";
                    }
                    header.SetFieldValue("PartyActTimeRange", partyIndex, partyActTimeRange);
                }
                else
                {
                    header.SetFieldValue("PartyTime_ActTime_V", partyIndex, rangeT);
                    header.SetFieldValue("PartyActTimeRange", partyIndex, partyActTimeRange);
                }

                int preTime = -1;
                int nextTime = -1;
                string timeBetweenFoods = "";

                //挙式料理
                var qFood = from dt1 in listFood.DataTable.AsEnumerable()
                            where DataUtil.CStr(dt1["PartyID"]) == DataUtil.CStr(dr["PartyID"]) && !DataUtil.IsNullOrEmpty(dt1["BeginTime"]) && DataUtil.CStr(dt1["FoodCD"]) != "09"
                            orderby DataUtil.CStr(dt1["BeginTime"])
                            select dt1;

                //カフェ
                var qFood09 = from dt1 in listFood.DataTable.AsEnumerable()
                            where DataUtil.CStr(dt1["FoodCD"]) == "09"
                            orderby DataUtil.CStr(dt1["BeginTime"])
                            select dt1;

                if (qFood.Count() > 0)
                {
                    DataRow item = qFood.ElementAt(0);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    nextTime = beginTime != null && beginTime.Length > 2 ? DataUtil.CInt(beginTime.Substring(0, 2)) * 60 + DataUtil.CInt(beginTime.Substring(2, 2)) : -1;
                    if (endTime != null && endTime.Length > 2)
                    {
                        preTime = DataUtil.CInt(endTime.Substring(0, 2)) * 60 + DataUtil.CInt(endTime.Substring(2, 2));
                    }

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_I", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_I", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_I", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_I", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_I", partyIndex, restRoomFlg);
                }
                if (qFood.Count() > 1)
                {
                    DataRow item = qFood.ElementAt(1);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    nextTime = beginTime != null && beginTime.Length > 2 ? DataUtil.CInt(beginTime.Substring(0, 2)) * 60 + DataUtil.CInt(beginTime.Substring(2, 2)) : -1;

                    if (preTime != -1 && nextTime != -1)
                    {
                        timeBetweenFoods = DataUtil.CStr(nextTime - preTime);
                    }
                    else
                    {
                        timeBetweenFoods = "";
                    }
                    header.SetFieldValue("TimeBetweenFoods_I", partyIndex, timeBetweenFoods);

                    if (endTime != null && endTime.Length > 2)
                    {
                        preTime = DataUtil.CInt(endTime.Substring(0, 2)) * 60 + DataUtil.CInt(endTime.Substring(2, 2));
                    }

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_II", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_II", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_II", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_II", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_II", partyIndex, restRoomFlg);
                }
                if (qFood.Count() > 2)
                {
                    DataRow item = qFood.ElementAt(2);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    nextTime = beginTime != null && beginTime.Length > 2 ? DataUtil.CInt(beginTime.Substring(0, 2)) * 60 + DataUtil.CInt(beginTime.Substring(2, 2)) : -1;

                    if (preTime != -1 && nextTime != -1)
                    {
                        timeBetweenFoods = DataUtil.CStr(nextTime - preTime);
                    }
                    else
                    {
                        timeBetweenFoods = "";
                    }
                    header.SetFieldValue("TimeBetweenFoods_II", partyIndex, timeBetweenFoods);

                    if (endTime != null && endTime.Length > 2)
                    {
                        preTime = DataUtil.CInt(endTime.Substring(0, 2)) * 60 + DataUtil.CInt(endTime.Substring(2, 2));
                    }

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_III", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_III", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_III", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_III", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_III", partyIndex, restRoomFlg);
                }
                if (qFood.Count() > 3)
                {
                    DataRow item = qFood.ElementAt(3);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    nextTime = beginTime != null && beginTime.Length > 2 ? DataUtil.CInt(beginTime.Substring(0, 2)) * 60 + DataUtil.CInt(beginTime.Substring(2, 2)) : -1;

                    if (preTime != -1 && nextTime != -1)
                    {
                        timeBetweenFoods = DataUtil.CStr(nextTime - preTime);
                    }
                    else
                    {
                        timeBetweenFoods = "";
                    }
                    header.SetFieldValue("TimeBetweenFoods_III", partyIndex, timeBetweenFoods);

                    if (endTime != null && endTime.Length > 2)
                    {
                        preTime = DataUtil.CInt(endTime.Substring(0, 2)) * 60 + DataUtil.CInt(endTime.Substring(2, 2));
                    }

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_IV", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_IV", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_IV", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_IV", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_IV", partyIndex, restRoomFlg);
                }
                if (qFood.Count() > 4)
                {
                    DataRow item = qFood.ElementAt(4);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    nextTime = beginTime != null && beginTime.Length > 2 ? DataUtil.CInt(beginTime.Substring(0, 2)) * 60 + DataUtil.CInt(beginTime.Substring(2, 2)) : -1;

                    if (preTime != -1 && nextTime != -1)
                    {
                        timeBetweenFoods = DataUtil.CStr(nextTime - preTime);
                    }
                    else
                    {
                        timeBetweenFoods = "";
                    }
                    header.SetFieldValue("TimeBetweenFoods_IV", partyIndex, timeBetweenFoods);

                    if (endTime != null && endTime.Length > 2)
                    {
                        preTime = DataUtil.CInt(endTime.Substring(0, 2)) * 60 + DataUtil.CInt(endTime.Substring(2, 2));
                    }

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_V", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_V", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_V", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_V", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_V", partyIndex, restRoomFlg);
                }
                if (qFood.Count() > 5)
                {
                    DataRow item = qFood.ElementAt(5);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    nextTime = beginTime != null && beginTime.Length > 2 ? DataUtil.CInt(beginTime.Substring(0, 2)) * 60 + DataUtil.CInt(beginTime.Substring(2, 2)) : -1;

                    if (preTime != -1 && nextTime != -1)
                    {
                        timeBetweenFoods = DataUtil.CStr(nextTime - preTime);
                    }
                    else
                    {
                        timeBetweenFoods = "";
                    }
                    header.SetFieldValue("TimeBetweenFoods_V", partyIndex, timeBetweenFoods);

                    if (endTime != null && endTime.Length > 2)
                    {
                        preTime = DataUtil.CInt(endTime.Substring(0, 2)) * 60 + DataUtil.CInt(endTime.Substring(2, 2));
                    }

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_VI", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_VI", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_VI", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_VI", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_VI", partyIndex, restRoomFlg);
                }
                if (qFood.Count() > 6)
                {
                    DataRow item = qFood.ElementAt(6);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    nextTime = beginTime != null && beginTime.Length > 2 ? DataUtil.CInt(beginTime.Substring(0, 2)) * 60 + DataUtil.CInt(beginTime.Substring(2, 2)) : -1;

                    if (preTime != -1 && nextTime != -1)
                    {
                        timeBetweenFoods = DataUtil.CStr(nextTime - preTime);
                    }
                    else
                    {
                        timeBetweenFoods = "";
                    }
                    header.SetFieldValue("TimeBetweenFoods_VI", partyIndex, timeBetweenFoods);

                    if (endTime != null && endTime.Length > 2)
                    {
                        preTime = DataUtil.CInt(endTime.Substring(0, 2)) * 60 + DataUtil.CInt(endTime.Substring(2, 2));
                    }

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_VII", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_VII", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_VII", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_VII", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_VII", partyIndex, restRoomFlg);
                }
                if (qFood.Count() > 7)
                {
                    DataRow item = qFood.ElementAt(7);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    nextTime = beginTime != null && beginTime.Length > 2 ? DataUtil.CInt(beginTime.Substring(0, 2)) * 60 + DataUtil.CInt(beginTime.Substring(2, 2)) : -1;

                    if (preTime != -1 && nextTime != -1)
                    {
                        timeBetweenFoods = DataUtil.CStr(nextTime - preTime);
                    }
                    else
                    {
                        timeBetweenFoods = "";
                    }
                    header.SetFieldValue("TimeBetweenFoods_VII", partyIndex, timeBetweenFoods);

                    if (endTime != null && endTime.Length > 2)
                    {
                        preTime = DataUtil.CInt(endTime.Substring(0, 2)) * 60 + DataUtil.CInt(endTime.Substring(2, 2));
                    }

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_VIII", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_VIII", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_VIII", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_VIII", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_VIII", partyIndex, restRoomFlg);
                }
                if (qFood.Count() > 8)
                {
                    DataRow item = qFood.ElementAt(8);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    nextTime = beginTime != null && beginTime.Length > 2 ? DataUtil.CInt(beginTime.Substring(0, 2)) * 60 + DataUtil.CInt(beginTime.Substring(2, 2)) : -1;

                    if (preTime != -1 && nextTime != -1)
                    {
                        timeBetweenFoods = DataUtil.CStr(nextTime - preTime);
                    }
                    else
                    {
                        timeBetweenFoods = "";
                    }
                    header.SetFieldValue("TimeBetweenFoods_VIII", partyIndex, timeBetweenFoods);

                    if (endTime != null && endTime.Length > 2)
                    {
                        preTime = DataUtil.CInt(endTime.Substring(0, 2)) * 60 + DataUtil.CInt(endTime.Substring(2, 2));
                    }

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_IX", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_IX", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_IX", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_IX", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_IX", partyIndex, restRoomFlg);
                }
                if (qFood.Count() > 9)
                {
                    DataRow item = qFood.ElementAt(9);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    nextTime = beginTime != null && beginTime.Length > 2 ? DataUtil.CInt(beginTime.Substring(0, 2)) * 60 + DataUtil.CInt(beginTime.Substring(2, 2)) : -1;

                    if (preTime != -1 && nextTime != -1)
                    {
                        timeBetweenFoods = DataUtil.CStr(nextTime - preTime);
                    }
                    else
                    {
                        timeBetweenFoods = "";
                    }
                    header.SetFieldValue("TimeBetweenFoods_IX", partyIndex, timeBetweenFoods);

                    if (endTime != null && endTime.Length > 2)
                    {
                        preTime = DataUtil.CInt(endTime.Substring(0, 2)) * 60 + DataUtil.CInt(endTime.Substring(2, 2));
                    }

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_X", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_X", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_X", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_X", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_X", partyIndex, restRoomFlg);
                }
                if (qFood.Count() > 10)
                {
                    DataRow item = qFood.ElementAt(10);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_XI", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_XI", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_XI", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_XI", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_XI", partyIndex, restRoomFlg);
                }
                if (qFood09.Count() > 0)
                {
                    DataRow item = qFood09.ElementAt(0);
                    string foodName = DataUtil.CStr(item["FoodName"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    header.SetFieldValue("PartyFood_FoodName_XI", partyIndex, foodName);
                    header.SetFieldValue("PartyFood_BeginTime_XI", partyIndex, beginTime);
                    header.SetFieldValue("PartyFood_EndTime_XI", partyIndex, endTime);
                    header.SetFieldValue("PartyFood_RestRoomTime_XI", partyIndex, restRoomTime);
                    header.SetFieldValue("PartyFood_RestRoomFlg_XI", partyIndex, restRoomFlg);
                }

                header.SetFieldValue("EndTimeA", partyIndex, reportDr["CutOutTimeA"]);
                header.SetFieldValue("EndTimeB", partyIndex, reportDr["CutOutTimeB"]);
                header.SetFieldValue("AllSupplyTime_I", partyIndex, reportDr["FullTime"]);
                header.SetFieldValue("AllSupplyTime_II", partyIndex, reportDr["CutOutTimeC"]);


                //挙式メンバー
                var qMember = from dt1 in listMember.DataTable.AsEnumerable()
                              where DataUtil.CStr(dt1["PartyID"]) == DataUtil.CStr(dr["PartyID"])
                              select dt1;
                foreach (var item in qMember)
                {
                    string roleCD = DataUtil.CStr(item["RoleCD"]);
                    string name = DataUtil.CStr(item["Name"]);

                    switch (roleCD)
                    {
                        case "01": //担当P
                            header.SetFieldValue("PartyMember_Name_I", partyIndex, name);
                            break;
                        case "02": //先導Cap
                            header.SetFieldValue("PartyMember_Name_II", partyIndex, name);
                            break;
                        case "03": //チャペルCap
                            header.SetFieldValue("PartyMember_Name_III", partyIndex, name);
                            break;
                        case "04": //バンケットCap
                            header.SetFieldValue("PartyMember_Name_IV", partyIndex, name);
                            break;
                        case "05": //コンシェルジュ
                            header.SetFieldValue("PartyMember_Name_V", partyIndex, name);
                            break;
                        case "06": //両親係新郎様側
                            header.SetFieldValue("PartyMember_Name_VI", partyIndex, name);
                            break;
                        case "07": //両親係新婦様側
                            header.SetFieldValue("PartyMember_Name_VII", partyIndex, name);
                            break;
                        case "08": //クロークA
                            header.SetFieldValue("PartyMember_Name_VIII", partyIndex, name);
                            break;
                        case "09": //クロークB
                            header.SetFieldValue("PartyMember_Name_IX", partyIndex, name);
                            break;
                        default:
                            break;
                    }
                }

                //挙式メンバータイム
                var qMemberTime = from dt1 in listMemberTime.DataTable.AsEnumerable()
                                  where DataUtil.CStr(dt1["PartyID"]) == DataUtil.CStr(dr["PartyID"])
                                  select dt1;
                foreach (var item in qMemberTime)
                {
                    string TimeCD = DataUtil.CStr(item["TimeCD"]);
                    string beginTime = DataUtil.CStr(item["BegTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string beginPeople = DataUtil.CStr(item["BegPeople"]);
                    string endPeople = DataUtil.CStr(item["EndPeople"]);

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    switch (TimeCD)
                    {
                        case "01": //早入/③
                            header.SetFieldValue("PartyMemberTime_BegTime_I", partyIndex, beginTime);
                            header.SetFieldValue("PartyMemberTime_BegPeople_I", partyIndex, beginPeople);
                            header.SetFieldValue("PartyMemberTime_EndTime_I", partyIndex, endTime);
                            header.SetFieldValue("PartyMemberTime_EndPeople_I", partyIndex, endPeople);
                            break;
                        case "02": //②/②
                            header.SetFieldValue("PartyMemberTime_BegTime_II", partyIndex, beginTime);
                            header.SetFieldValue("PartyMemberTime_BegPeople_II", partyIndex, beginPeople);
                            header.SetFieldValue("PartyMemberTime_EndTime_II", partyIndex, endTime);
                            header.SetFieldValue("PartyMemberTime_EndPeople_II", partyIndex, endPeople);
                            break;
                        case "03": //③/最終
                            header.SetFieldValue("PartyMemberTime_BegTime_III", partyIndex, beginTime);
                            header.SetFieldValue("PartyMemberTime_BegPeople_III", partyIndex, beginPeople);
                            header.SetFieldValue("PartyMemberTime_EndTime_III", partyIndex, endTime);
                            header.SetFieldValue("PartyMemberTime_EndPeople_III", partyIndex, endPeople);
                            break;
                        default:
                            break;
                    }
                }


                colCount += 9;
                partyIndex++;
            }

            datas.Add(header);

            report.Data = datas;

            //帳票生成
            report.CreateReport();
            listReportPath.Add(report.DownLoadPath());


            GC.Collect();

            if (listReportPath.Count > 1)
            {
                string date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

                string fileName = string.Format("パーティ報告書_{0}.zip", date);

                string zipFilePath = ZipFiles(listReportPath, fileName);
                return Json(new { Path = zipFilePath, ResultType = EnumResultType.Success });

            }
            else
            {
                return Json(new { Path = listReportPath[0], ResultType = EnumResultType.Success });
            }



        }

        public ActionResult ReportListExcel(DynamicsViewRequest request)
        {
            DyEntityLogic logic = new DyEntityLogic();
            request.pageNumber = 1;
            request.GetPageSize = 2000;

            PageViewResult list = logic.GetList(request);

            if (list.DataTable.Rows.Count == 0)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            //挙式レポート
            request.EntityName = "T_PartyReport";
            request.ViewName = "PartyReportIndex";
            PageViewResult listReport = logic.GetList(request);

            //挙式タイム
            request.EntityName = "T_PartyTime";
            request.ViewName = "PartyTimeIndex";
            PageViewResult listTime = logic.GetList(request);

            //挙式料理
            request.EntityName = "T_PartyFood";
            request.ViewName = "PartyFoodIndex";
            PageViewResult listFood = logic.GetList(request);

            //挙式メンバー
            request.EntityName = "T_PartyMember";
            request.ViewName = "PartyMemberIndex";
            PageViewResult listMember = logic.GetList(request);

            //挙式メンバータイム
            request.EntityName = "T_PartyMemberTime";
            request.ViewName = "PartyMemberTimeIndex";
            PageViewResult listMemberTime = logic.GetList(request);

            if (optionSetDic == null)
            {
                optionSetDic = new Dictionary<string, string>();
                DataTable optionSetDt = logic.GetOptionSet(request.ProjID, null, "ja-jp", "ja-jp");
                foreach (DataRow row in optionSetDt.Rows)
                {
                    string key = DataUtil.CStr(row["OptSetName"]) + "_" + DataUtil.CStr(row["CD"]);
                    if (optionSetDic.ContainsKey(key)) continue;

                    string value = DataUtil.CStr(row["Name"]);
                    optionSetDic.Add(key, value);
                }
            }

            ExcelReport report = null;

            //データ
            List<IReportData> datas = new List<IReportData>();

            //headerデータ
            PartyReportModel header = new PartyReportModel();

            int partyIndex = 1;
            int colCount = 5;
            string tempShopCD = "";
            int tempMonth = -1;
            List<string> listReportPath = new List<string>();

            foreach (DataRow dr in list.DataTable.Rows)
            {
                string ShopCD = DataUtil.CStr(dr["ShopCD"]);
                DateTime pd = DataUtil.CDate(dr["PartyDate"]);

                if (ShopCD != tempShopCD)
                {
                    if (report != null)
                    {
                        datas.Add(header);

                        (report.Handler as ReportListExcelHandler).index = colCount;

                        report.Data = datas;

                        //帳票生成
                        report.CreateReport();
                        listReportPath.Add(report.DownLoadPath());
                    }

                    report = new ExcelReport();

                    report.Handler = new ReportListExcelHandler();

                    //ReportType
                    report.ReportType = EnumReportType.ListReport;
                    //report.isAutoRowHeight = true;

                    report.Template = "パーティ報告書リスト.xls";

                    //Template
                    report.FileName = "パーティ報告書" + "_" + ShopCD;

                    //データ
                    datas = new List<IReportData>();

                    //headerデータ
                    header = new PartyReportModel();
                    partyIndex = 1;
                    colCount = 5;
                }
                else
                {
                    if (pd.Month != tempMonth)
                    {
                        datas.Add(header);
                        //headerデータ
                        header = new PartyReportModel();
                        partyIndex = 1;
                        colCount = 5;
                    }
                }

                tempShopCD = ShopCD;
                tempMonth = pd.Month;

                header.SetFieldValue("ShopCD", 0, dr["ShopCD"]);
                header.SetFieldValue("PartyMonth", 0, pd.Month + "月");


                header.SetFieldValue("PartyDate", partyIndex, string.Format("{0:yyyy/MM/dd}", dr["PartyDate"]));
                header.SetFieldValue("BrideFamilyName", partyIndex, dr["BrideName"]);
                header.SetFieldValue("GroomFamilyName", partyIndex, dr["GroomName"]);
                header.SetFieldValue("TantoName", partyIndex, dr["StaffName"]);
                header.SetFieldValue("StartTime", partyIndex, DataUtil.CStr(dr["StartTime"]).Replace(":", "："));
                

                //挙式レポート
                var q1 = from dt1 in listReport.DataTable.AsEnumerable()//查询  
                         where DataUtil.CStr(dt1["PartyID"]) == DataUtil.CStr(dr["PartyID"])
                         select dt1;

                DataRow reportDr = null;
                foreach (var item in q1)//显示查询结果  
                {
                    reportDr = item;
                    break;
                }

                string key = "";

                key = "PartyDiv" + "_" + DataUtil.CStr(reportDr["PartyDiv"]);
                string partyDiv = optionSetDic.ContainsKey(key) ? optionSetDic[key] : "";

                key = "TableCloth" + "_" + DataUtil.CStr(reportDr["TableCross"]);
                string tableCross = optionSetDic.ContainsKey(key)?optionSetDic[key]:"";

                key = "WeddingStyle" + "_" + DataUtil.CStr(reportDr["PartyStyleCD"]);
                string partyStyle = optionSetDic.ContainsKey(key) ? optionSetDic[key] : "";

                key = "FoodStyle" + "_" + DataUtil.CStr(reportDr["FoodStyleCD"]);
                string foodStyle = optionSetDic.ContainsKey(key) ? optionSetDic[key] : "";

                key = "WDrink" + "_" + DataUtil.CStr(reportDr["Wdrink"]);
                string wdrink = optionSetDic.ContainsKey(key) ? optionSetDic[key] : "";

                key = "Diesel" + "_" + DataUtil.CStr(reportDr["Desl"]);
                string desl = optionSetDic.ContainsKey(key) ? optionSetDic[key] : "";

                header.SetFieldValue("ReporterName", partyIndex, reportDr["StaffName"]);
                header.SetFieldValue("AdultCnt", partyIndex, reportDr["AdultCnt"]);
                header.SetFieldValue("HalfCnt", partyIndex, reportDr["HalfCnt"]);
                header.SetFieldValue("ChildrenCnt", partyIndex, reportDr["ChildrenCnt"]);
                header.SetFieldValue("SeatOnlyCnt", partyIndex, reportDr["SeatOnlyCnt"]);
                header.SetFieldValue("TableCnt", partyIndex, reportDr["TableCnt"]);
                header.SetFieldValue("PartyDiv", partyIndex, partyDiv);
                header.SetFieldValue("TableCross", partyIndex, tableCross);
                header.SetFieldValue("PartyStyleName", partyIndex, partyStyle);
                header.SetFieldValue("FoodStyleName", partyIndex, foodStyle);

                string foodPricce = DataUtil.IsNullOrEmpty(reportDr["FoodPricce"]) ? "" : DataUtil.CInt(reportDr["FoodPricce"]).ToString("N");
                string drinkPrice = DataUtil.IsNullOrEmpty(reportDr["DrinkPrice"]) ? "" : DataUtil.CInt(reportDr["DrinkPrice"]).ToString("N");
                int index = -1;
                index = foodPricce.LastIndexOf('.');
                if (index >= 0)
                {
                    foodPricce = foodPricce.Substring(0, index);
                }
                index = drinkPrice.LastIndexOf('.');
                if (index >= 0)
                {
                    drinkPrice = drinkPrice.Substring(0, index);
                }

                header.SetFieldValue("FoodPricce", partyIndex, foodPricce);
                header.SetFieldValue("DrinkPrice", partyIndex, drinkPrice);
                header.SetFieldValue("Wdrink", partyIndex, wdrink);
                header.SetFieldValue("Desl", partyIndex, desl);


                header.SetFieldValue("RestRoomFlg", partyIndex, DataUtil.CStr(reportDr["RestRoomFlg"]) == "True" ? "○" : "×");
                header.SetFieldValue("AnketFlg", partyIndex, DataUtil.CStr(reportDr["AnketFlg"]) == "True" ? "○" : "×");
                header.SetFieldValue("SecondParty", partyIndex, DataUtil.CStr(reportDr["SecondParty"]) == "True" ? "○" : "×");
                header.SetFieldValue("OrderMemberCnt", partyIndex, reportDr["OrderMemberCnt"]);
                header.SetFieldValue("ActMemberCnt", partyIndex, reportDr["ActMemberCnt"]);
                header.SetFieldValue("LessMemberCnt", partyIndex, reportDr["LessMemberCnt"]);
                header.SetFieldValue("Coment1", partyIndex, reportDr["Coment1"]);
                header.SetFieldValue("Coment2", partyIndex, reportDr["Coment2"]);
                header.SetFieldValue("Coment3", partyIndex, reportDr["Coment3"]);
                header.SetFieldValue("ComentSkill", partyIndex, reportDr["ComentSkill"]);
                header.SetFieldValue("Memo", partyIndex, reportDr["Memo"]);

                string partyBeginTime = "";
                string partyEndTime = "";
                string partyBeginTimeTO = "";
                string partyEndTimeTO = "";

                //挙式タイム
                var qTime = from dt1 in listTime.DataTable.AsEnumerable() 
                         where DataUtil.CStr(dt1["PartyID"]) == DataUtil.CStr(dr["PartyID"])
                         select dt1;

                foreach (var item in qTime)
                {
                    //switch (DataUtil.CStr(item["TimeCD"])) { 
                    
                    //}    

                    switch (DataUtil.CStr(item["TimeCD"]))
                    {
                        case "01": //ロビー解放 

                            if(DataUtil.CStr(item["TimeType"]) == "2"){
                                header.SetFieldValue("PartyTime_OrderTime_I", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":","："));
                            }

                            break;
                        case "02": //挙式
                            if (DataUtil.CStr(item["TimeType"]) == "1")
                            {
                                header.SetFieldValue("PartyTime_OrderTime_II", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":", "："));
                                header.SetFieldValue("PartyTime_OrderTimeTO_II", partyIndex, DataUtil.CStr(item["EndTime"]).Replace(":", "："));


                                //header.SetFieldValue("PartyTime_DelayTime_II", partyIndex, item["DelayTime"]);
                            }

                            if (DataUtil.CStr(item["TimeType"]) == "2")
                            {
                                header.SetFieldValue("PartyTime_ActTime_II", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":", "："));
                                header.SetFieldValue("PartyTime_ActTimeTO_II", partyIndex, DataUtil.CStr(item["EndTime"]).Replace(":", "："));

                                string delayTime = "";
                                if (DataUtil.CStr(item["DelayTime"]).IndexOf("ｲﾝﾀｲﾑ") >= 0)
                                {
                                    delayTime = "&lt;span style='color: rgb(255, 0, 0);'&gt;" + DataUtil.CStr(item["DelayTime"]) + "&lt;/span&gt;";
                                }
                                else
                                {
                                    delayTime = DataUtil.CStr(item["DelayTime"]);
                                }
                                header.SetFieldValue("PartyTime_DelayTime_II", partyIndex, delayTime);
                            }
                            break;
                        case "03": //披露宴
                             if (DataUtil.CStr(item["TimeType"]) == "1")
                            {
                                header.SetFieldValue("PartyTime_OrderTime_III", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":", "："));
                                header.SetFieldValue("PartyTime_OrderTimeTO_III", partyIndex, DataUtil.CStr(item["EndTime"]).Replace(":", "："));


                                //header.SetFieldValue("PartyTime_DelayTime_III", partyIndex, item["DelayTime"]);
                                partyBeginTime = DataUtil.CStr(item["BeginTime"]);
                                partyEndTime = DataUtil.CStr(item["EndTime"]);
                            }

                            if (DataUtil.CStr(item["TimeType"]) == "2")
                            {
                                header.SetFieldValue("PartyTime_ActTime_III", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":", "："));
                                header.SetFieldValue("PartyTime_ActTimeTO_III", partyIndex, DataUtil.CStr(item["EndTime"]).Replace(":", "："));

                                string delayTime = "";
                                if (DataUtil.CStr(item["DelayTime"]).IndexOf("ｲﾝﾀｲﾑ") >= 0)
                                {
                                    delayTime = "&lt;span style='color: rgb(255, 0, 0);'&gt;" + DataUtil.CStr(item["DelayTime"]) + "&lt;/span&gt;";
                                }
                                else
                                {
                                    delayTime = DataUtil.CStr(item["DelayTime"]);
                                }
                                header.SetFieldValue("PartyTime_DelayTime_III", partyIndex, delayTime);
                                partyBeginTimeTO = DataUtil.CStr(item["BeginTime"]);
                                partyEndTimeTO = DataUtil.CStr(item["EndTime"]);
                            }
                            break;
                        case "04": //乾杯時間

                            if (DataUtil.CStr(item["TimeType"]) == "2")
                            {
                                header.SetFieldValue("PartyTime_OrderTime_IV", partyIndex, DataUtil.CStr(item["BeginTime"]).Replace(":", "："));
                            }

                            break;
                        case "05": //パーティ時間 
                            break;
                        default:
                            break;
                    }
                }

                int rangeH = 0;
                int rangeM = 0;
                string rangeT = "";
                int partyOrdTime = DataUtil.CInt(reportDr["PartyOrdTime"]);
                int partyActTime = DataUtil.CInt(reportDr["PartyActTime"]);
                int partyActOrdTime = DataUtil.CInt(reportDr["PartyActOrdTime"]);
                
                rangeT = "";
                string partyOrdTimeRange = "";
                if (partyActOrdTime > 0)
                {
                    rangeH = partyActOrdTime / 60;
                    rangeM = partyActOrdTime % 60;
                    rangeT = string.Format("{0}時間 {1}分", DataUtil.CStr(rangeH).PadLeft(2, ' '), DataUtil.CStr(rangeM).PadLeft(2, ' '));
                    header.SetFieldValue("PartyTime_OrderTime_V", partyIndex, rangeT);

                    if ((partyActOrdTime - partyOrdTime) <= 0)
                    {
                        partyOrdTimeRange = "" + (partyActOrdTime - partyOrdTime);
                        partyOrdTimeRange += "分";
                    }
                    else
                    {
                        partyOrdTimeRange = "+" + (partyActOrdTime - partyOrdTime);
                        partyOrdTimeRange += "分";
                        partyOrdTimeRange = "&lt;span style='color: rgb(255, 0, 0);'&gt;" + partyOrdTimeRange + "&lt;/span&gt;";
                    }
                    header.SetFieldValue("PartyOrdTimeRange", partyIndex, partyOrdTimeRange);
                }
                else
                {
                    header.SetFieldValue("PartyTime_OrderTime_V", partyIndex, rangeT);
                    header.SetFieldValue("PartyOrdTimeRange", partyIndex, partyOrdTimeRange);
                }

                rangeT = "";
                string partyActTimeRange = "";
                if (partyActTime > 0)
                {
                    rangeH = partyActTime / 60;
                    rangeM = partyActTime % 60;
                    rangeT = string.Format("{0}時間 {1}分", DataUtil.CStr(rangeH).PadLeft(2, ' '), DataUtil.CStr(rangeM).PadLeft(2, ' '));
                    header.SetFieldValue("PartyTime_ActTime_V", partyIndex, rangeT);

                    if ((partyActTime - partyOrdTime) <= 0)
                    {
                        partyActTimeRange = "" + (partyActTime - partyOrdTime);
                        partyActTimeRange += "分";
                    }
                    else
                    {
                        partyActTimeRange = "+" + (partyActTime - partyOrdTime);
                        partyActTimeRange += "分";
                        partyActTimeRange = "&lt;span style='color: rgb(255, 0, 0);'&gt;" + partyActTimeRange + "&lt;/span&gt;";
                    }
                    header.SetFieldValue("PartyActTimeRange", partyIndex, partyActTimeRange);
                }
                else
                {
                    header.SetFieldValue("PartyTime_ActTime_V", partyIndex, rangeT);
                    header.SetFieldValue("PartyActTimeRange", partyIndex, partyActTimeRange);
                }


                //挙式料理
                var qFood = from dt1 in listFood.DataTable.AsEnumerable()
                            where DataUtil.CStr(dt1["PartyID"]) == DataUtil.CStr(dr["PartyID"])
                            select dt1;

                int preFoodCD = 0;
                int curFoodCD = 0;
                foreach (var item in qFood)
                {
                    string foodCD = DataUtil.CStr(item["FoodCD"]);
                    string beginTime = DataUtil.CStr(item["BeginTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string restRoomTime = DataUtil.CStr(item["RestRoomTime"]);
                    string restRoomFlg = DataUtil.CStr(item["RestRoomFlg"]) == "True" ? "●" : "";

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    switch (foodCD)
                    {
                        case "01": //アミューズ
                            header.SetFieldValue("PartyFood_BeginTime_I", partyIndex, beginTime);
                            header.SetFieldValue("PartyFood_EndTime_I", partyIndex, endTime);
                            header.SetFieldValue("PartyFood_RestRoomTime_I", partyIndex, restRoomTime);
                            header.SetFieldValue("PartyFood_RestRoomFlg_I", partyIndex, restRoomFlg);
                            break;
                        case "02": //1er
                            header.SetFieldValue("PartyFood_BeginTime_II", partyIndex, beginTime);
                            header.SetFieldValue("PartyFood_EndTime_II", partyIndex, endTime);
                            header.SetFieldValue("PartyFood_RestRoomTime_II", partyIndex, restRoomTime);
                            header.SetFieldValue("PartyFood_RestRoomFlg_II", partyIndex, restRoomFlg);
                            break;
                        case "03": //2em
                            header.SetFieldValue("PartyFood_BeginTime_III", partyIndex, beginTime);
                            header.SetFieldValue("PartyFood_EndTime_III", partyIndex, endTime);
                            header.SetFieldValue("PartyFood_RestRoomTime_III", partyIndex, restRoomTime);
                            header.SetFieldValue("PartyFood_RestRoomFlg_III", partyIndex, restRoomFlg);
                            break;
                        case "04": //ポタージュ
                            header.SetFieldValue("PartyFood_BeginTime_IV", partyIndex, beginTime);
                            header.SetFieldValue("PartyFood_EndTime_IV", partyIndex, endTime);
                            header.SetFieldValue("PartyFood_RestRoomTime_IV", partyIndex, restRoomTime);
                            header.SetFieldValue("PartyFood_RestRoomFlg_IV", partyIndex, restRoomFlg);
                            break;
                        case "05": //ポワソン 
                            header.SetFieldValue("PartyFood_BeginTime_V", partyIndex, beginTime);
                            header.SetFieldValue("PartyFood_EndTime_V", partyIndex, endTime);
                            header.SetFieldValue("PartyFood_RestRoomTime_V", partyIndex, restRoomTime);
                            header.SetFieldValue("PartyFood_RestRoomFlg_V", partyIndex, restRoomFlg);
                            break;
                        case "06": //お口直し
                            header.SetFieldValue("PartyFood_BeginTime_VI", partyIndex, beginTime);
                            header.SetFieldValue("PartyFood_EndTime_VI", partyIndex, endTime);
                            header.SetFieldValue("PartyFood_RestRoomTime_VI", partyIndex, restRoomTime);
                            header.SetFieldValue("PartyFood_RestRoomFlg_VI", partyIndex, restRoomFlg);
                            break;
                        case "07": //ヴィヤンド
                            header.SetFieldValue("PartyFood_BeginTime_VII", partyIndex, beginTime);
                            header.SetFieldValue("PartyFood_EndTime_VII", partyIndex, endTime);
                            header.SetFieldValue("PartyFood_RestRoomTime_VII", partyIndex, restRoomTime);
                            header.SetFieldValue("PartyFood_RestRoomFlg_VII", partyIndex, restRoomFlg);
                            break;
                        case "08": //デセール&マリアージュ
                            header.SetFieldValue("PartyFood_BeginTime_VIII", partyIndex, beginTime);
                            header.SetFieldValue("PartyFood_EndTime_VIII", partyIndex, endTime);
                            header.SetFieldValue("PartyFood_RestRoomTime_VIII", partyIndex, restRoomTime);
                            header.SetFieldValue("PartyFood_RestRoomFlg_VIII", partyIndex, restRoomFlg);
                            break;
                        case "09": //カフェ
                            header.SetFieldValue("PartyFood_BeginTime_IX", partyIndex, beginTime);
                            header.SetFieldValue("PartyFood_EndTime_IX", partyIndex, endTime);
                            header.SetFieldValue("PartyFood_RestRoomTime_IX", partyIndex, restRoomTime);
                            header.SetFieldValue("PartyFood_RestRoomFlg_IX", partyIndex, restRoomFlg);
                            break;
                        case "10": //サラダ
                            header.SetFieldValue("PartyFood_BeginTime_X", partyIndex, beginTime);
                            header.SetFieldValue("PartyFood_EndTime_X", partyIndex, endTime);
                            header.SetFieldValue("PartyFood_RestRoomTime_X", partyIndex, restRoomTime);
                            header.SetFieldValue("PartyFood_RestRoomFlg_X", partyIndex, restRoomFlg);
                            break;
                        case "11": //デザートブッフェ
                            header.SetFieldValue("PartyFood_BeginTime_XI", partyIndex, beginTime);
                            header.SetFieldValue("PartyFood_EndTime_XI", partyIndex, endTime);
                            header.SetFieldValue("PartyFood_RestRoomTime_XI", partyIndex, restRoomTime);
                            header.SetFieldValue("PartyFood_RestRoomFlg_XI", partyIndex, restRoomFlg);
                            break;
                        default:
                            break;
                    }

                    curFoodCD = DataUtil.CInt(foodCD);
                    if (preFoodCD < curFoodCD && curFoodCD < 8)
                    {
                        preFoodCD = curFoodCD;
                    }
                }

                string foodNoInput = "";
                switch (preFoodCD)
                {
                    case 1: //アミューズ
                        foodNoInput = "①";
                        break;
                    case 2: //1er
                        foodNoInput = "②";
                        break;
                    case 3: //2em
                        foodNoInput = "③";
                        break;
                    case 4: //ポタージュ
                        foodNoInput = "④";
                        break;
                    case 5: //ポワソン 
                        foodNoInput = "⑤";
                        break;
                    case 6: //お口直し
                        foodNoInput = "⑥";
                        break;
                    case 7: //ヴィヤンド
                        foodNoInput = "⑦";
                        break;
                    case 8: //デセール&マリアージュ
                        foodNoInput = "⑧";
                        break;
                    case 9: //カフェ
                        foodNoInput = "⑨";
                        break;
                    case 10: //サラダ
                        foodNoInput = "⑩";
                        break;
                    case 11: //デザートブッフェ
                        foodNoInput = "⑪";
                        break;
                    default:
                        break;
                }
                header.SetFieldValue("FoodNoInput", partyIndex, foodNoInput);

                string cutOutTimeA = "";
                string cutOutTimeB = "";
                string fullTime = "";
                string cutOutTimeC = "";

                if (DataUtil.CInt(reportDr["CutOutTimeA"]) > 90)
                {
                    cutOutTimeA = "&lt;span style='color: rgb(255, 0, 0);'&gt;" + DataUtil.CStr(reportDr["CutOutTimeA"]) + "&lt;/span&gt;";
                }
                else
                {
                    cutOutTimeA = DataUtil.CStr(reportDr["CutOutTimeA"]);
                }
                header.SetFieldValue("EndTimeA", partyIndex, cutOutTimeA);

                if (DataUtil.CInt(reportDr["CutOutTimeB"]) > 90)
                {
                    cutOutTimeB = "&lt;span style='color: rgb(255, 0, 0);'&gt;" + DataUtil.CStr(reportDr["CutOutTimeB"]) + "&lt;/span&gt;";
                }
                else
                {
                    cutOutTimeB = DataUtil.CStr(reportDr["CutOutTimeB"]);
                }
                header.SetFieldValue("EndTimeB", partyIndex, cutOutTimeB);

                //if (DataUtil.CInt(reportDr["FullTime"]) > 90)
                //{
                //    fullTime = "&lt;span style='color: rgb(255, 0, 0);'&gt;" + DataUtil.CStr(reportDr["FullTime"]) + "&lt;/span&gt;";
                //}
                //else
                {
                    fullTime = DataUtil.CStr(reportDr["FullTime"]);
                }
                header.SetFieldValue("AllSupplyTime_I", partyIndex, fullTime);

                //if (DataUtil.CInt(reportDr["CutOutTimeC"]) > 90)
                //{
                //    cutOutTimeC = "&lt;span style='color: rgb(255, 0, 0);'&gt;" + DataUtil.CStr(reportDr["CutOutTimeC"]) + "&lt;/span&gt;";
                //}
                //else
                {
                    cutOutTimeC = DataUtil.CStr(reportDr["CutOutTimeC"]);
                }
                header.SetFieldValue("AllSupplyTime_II", partyIndex, cutOutTimeC);

                //挙式メンバー
                var qMember = from dt1 in listMember.DataTable.AsEnumerable()
                            where DataUtil.CStr(dt1["PartyID"]) == DataUtil.CStr(dr["PartyID"])
                            select dt1;
                foreach (var item in qMember)
                {
                    string roleCD = DataUtil.CStr(item["RoleCD"]);
                    string name = DataUtil.CStr(item["Name"]);

                    switch (roleCD)
                    {
                        case "01": //担当P
                            header.SetFieldValue("PartyMember_Name_I", partyIndex, name);
                            break;
                        case "02": //先導Cap
                            header.SetFieldValue("PartyMember_Name_II", partyIndex, name);
                            break;
                        case "03": //チャペルCap
                            header.SetFieldValue("PartyMember_Name_III", partyIndex, name);
                            break;
                        case "04": //バンケットCap
                            header.SetFieldValue("PartyMember_Name_IV", partyIndex, name);
                            break;
                        case "05": //コンシェルジュ
                            header.SetFieldValue("PartyMember_Name_V", partyIndex, name);
                            break;
                        case "06": //両親係新郎様側
                            header.SetFieldValue("PartyMember_Name_VI", partyIndex, name);
                            break;
                        case "07": //両親係新婦様側
                            header.SetFieldValue("PartyMember_Name_VII", partyIndex, name);
                            break;
                        case "08": //クロークA
                            header.SetFieldValue("PartyMember_Name_VIII", partyIndex, name);
                            break;
                        case "09": //クロークB
                            header.SetFieldValue("PartyMember_Name_IX", partyIndex, name);
                            break;
                        default:
                            break;
                    }
                } 

                //挙式メンバータイム
                var qMemberTime = from dt1 in listMemberTime.DataTable.AsEnumerable()  
                            where DataUtil.CStr(dt1["PartyID"]) == DataUtil.CStr(dr["PartyID"])
                            select dt1;
                foreach (var item in qMemberTime)
                {
                    string TimeCD = DataUtil.CStr(item["TimeCD"]);
                    string beginTime = DataUtil.CStr(item["BegTime"]).Replace(":", "");
                    string endTime = DataUtil.CStr(item["EndTime"]).Replace(":", "");
                    string beginPeople = DataUtil.CStr(item["BegPeople"]);
                    string endPeople = DataUtil.CStr(item["EndPeople"]);

                    if (beginTime != null && beginTime.Length > 2)
                    {
                        beginTime = beginTime.Substring(0, 2) + "：" + beginTime.Substring(2, 2);
                    }
                    if (endTime != null && endTime.Length > 2)
                    {
                        endTime = endTime.Substring(0, 2) + "：" + endTime.Substring(2, 2);
                    }

                    switch (TimeCD)
                    {
                        case "01": //早入/③
                            header.SetFieldValue("PartyMemberTime_BegTime_I", partyIndex, beginTime);
                            header.SetFieldValue("PartyMemberTime_BegPeople_I", partyIndex, beginPeople);
                            header.SetFieldValue("PartyMemberTime_EndTime_I", partyIndex, endTime);
                            header.SetFieldValue("PartyMemberTime_EndPeople_I", partyIndex, endPeople);
                            break;
                        case "02": //②/②
                            header.SetFieldValue("PartyMemberTime_BegTime_II", partyIndex, beginTime);
                            header.SetFieldValue("PartyMemberTime_BegPeople_II", partyIndex, beginPeople);
                            header.SetFieldValue("PartyMemberTime_EndTime_II", partyIndex, endTime);
                            header.SetFieldValue("PartyMemberTime_EndPeople_II", partyIndex, endPeople);
                            break;
                        case "03": //③/最終
                            header.SetFieldValue("PartyMemberTime_BegTime_III", partyIndex, beginTime);
                            header.SetFieldValue("PartyMemberTime_BegPeople_III", partyIndex, beginPeople);
                            header.SetFieldValue("PartyMemberTime_EndTime_III", partyIndex, endTime);
                            header.SetFieldValue("PartyMemberTime_EndPeople_III", partyIndex, endPeople);
                            break;
                        default:
                            break;
                    }
                }

                
                colCount += 9;
                partyIndex++;
            }

            datas.Add(header);

            (report.Handler as ReportListExcelHandler).index = colCount;

            report.Data = datas;

            //帳票生成
            report.CreateReport();
            listReportPath.Add(report.DownLoadPath());


            GC.Collect();

            if (listReportPath.Count > 1)
            {
                string date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

                string fileName = string.Format("パーティ報告書_{0}.zip", date);

                string zipFilePath = ZipFiles(listReportPath, fileName);
                return Json(new { Path = zipFilePath, ResultType = EnumResultType.Success });

            }
            else 
            {
                return Json(new { Path = listReportPath[0], ResultType = EnumResultType.Success });
            }



        }

        private string ZipFiles(List<string> listPath, string zipFileName)
        {
            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string zipFilePath = "/temp/" + zipFileName;

            using (FileStream zipFileToOpen = new FileStream(basePath + zipFilePath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipFileToOpen, ZipArchiveMode.Create))
            {
                foreach (string path in listPath)
                {
                    string tempPath = basePath + path;
                    string filename = System.IO.Path.GetFileName(tempPath);

                    ZipArchiveEntry readMeEntry = archive.CreateEntry(filename);
                    using (System.IO.Stream stream = readMeEntry.Open())
                    {
                        byte[] bytes = System.IO.File.ReadAllBytes(tempPath);
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
            }

            return zipFilePath;
        }

        public string GetStaffName(string staffCD)
        {
            M_Staff staff = db.Staffs.Find(staffCD);
            if (null != staff)
            {
                return staff.StaffName;
            }
            else
            {
                return "";
            }
        }

        public ActionResult TelRptEvaByStaff(TelRptEvaViewModel model
            , int id
            , string PartyID
            , string ShopCD
            , string PartyDateFrom
            , string PartyDateTo
            , string StaffEva
            , string LeaderEva
            , string PartyUpdateTime
            , string ReportUpdateTime
            , string EvaByStaffUpdateTime
            , string UpdateUser
            , string UpdateUserID
            , string Range
            , string divisionCD
            , string StaffCD
            , string page
            , string actionType
            , string EmailTo
            , string EmailCc
            , string EmailBCc
            , string EmailHead, string isPartyReport)
        {
            ViewBag.ShopCD = ShopCD;
            ViewBag.PartyDateFrom = PartyDateFrom;
            ViewBag.PartyDateTo = PartyDateTo;
            ViewBag.StaffEva = StaffEva;
            ViewBag.LeaderEva = LeaderEva;
            ViewBag.PartyID = PartyID;
            ViewBag.page = page;
            ViewBag.isPartyReport = isPartyReport;
            

            ViewBag.Range = Range;
            ViewBag.divisionCD = divisionCD;
            ViewBag.StaffCD = StaffCD;

            bool haita = false;

            if (null != model.evaByStaff)
            {
                T_Party party = model.party;
                T_Report report = model.report;
                T_EvaByStaff evaByStaff = model.evaByStaff;

                T_Party _partyData = db.Partys.Find(int.Parse(PartyID));
                T_Report _reportData = db.Reports.Find(int.Parse(PartyID));
                T_EvaByStaff _evaByStaffData = db.EvaByStaffs.Find(int.Parse(PartyID));


                if (null != _partyData)
                {
                    //20141010　李梁　_partyData.UpdateTime.Value ⇒ _partyData.UpdateTime
                    if (PartyUpdateTime == (_partyData.UpdateTime == null ? "" : _partyData.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss")))
                    //20141010　李梁
                    {
                        if (_partyData.FinishFlag != (string.IsNullOrEmpty(evaByStaff.StatffEva) == false))
                        {
                            _partyData.FinishFlag = (string.IsNullOrEmpty(evaByStaff.StatffEva) == false);
                            _partyData.UpdateTime = CommonUtils.getLocalDateTime();
                            _partyData.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                        }

                        db.Entry(_partyData).State = EntityState.Modified;
                    }
                    else
                    {
                        haita = true;
                    }
                }

                if (_reportData != null)
                {
                    if (report.PartyID == 0)
                    {
                        haita = true;
                    }
                    else
                    {
                        //20141010　李梁　_reportData.UpdateTime.Value ⇒ _reportData.UpdateTime
                        if (ReportUpdateTime == (_reportData.UpdateTime == null ? "" : _reportData.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss")))
                        {
                            if (_reportData.TelFlg != report.TelFlg
                                || _reportData.Remark != report.Remark
                                || _reportData.Memo != report.Memo) {

                                    _reportData.TelFlg = report.TelFlg;
                                    _reportData.Remark = report.Remark;
                                    _reportData.Memo = report.Memo;
                                    _reportData.UpdateTime = CommonUtils.getLocalDateTime();
                                    _reportData.UpdateUserID = (Session["user"] as UserSession).StaffCD;

                                    db.Entry(_reportData).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            haita = true;
                        }
                    }
                }
                else
                {
                    report.PartyID = int.Parse(PartyID);
                    report.UpdateTime = CommonUtils.getLocalDateTime();
                    report.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                    db.Reports.Add(report);
                }

                if (_evaByStaffData != null)
                {
                    if (evaByStaff.PartyID == 0)
                    {
                        haita = true;
                    }
                    else
                    {
                        //20141010　李梁　_evaByStaffData.UpdateTime.Value ⇒ _evaByStaffData.UpdateTime
                        if (EvaByStaffUpdateTime == (_evaByStaffData.UpdateTime == null ? "" : _evaByStaffData.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss")))
                        //20141010　李梁
                        {
                            if (_evaByStaffData.CareFlg != evaByStaff.CareFlg
                                || _evaByStaffData.Record != evaByStaff.Record
                                || _evaByStaffData.Eva1Date != evaByStaff.Eva1Date
                                || _evaByStaffData.Eva1Result != evaByStaff.Eva1Result
                                || _evaByStaffData.Eva1Time != evaByStaff.Eva1Time
                                || _evaByStaffData.Eva1StaffCD != evaByStaff.Eva1StaffCD
                                || _evaByStaffData.Eva2Date != evaByStaff.Eva2Date
                                || _evaByStaffData.Eva2Result != evaByStaff.Eva2Result
                                || _evaByStaffData.Eva2Time != evaByStaff.Eva2Time
                                || _evaByStaffData.Eva2StaffCD != evaByStaff.Eva2StaffCD
                                || _evaByStaffData.Eva3Date != evaByStaff.Eva3Date
                                || _evaByStaffData.Eva3Result != evaByStaff.Eva3Result
                                || _evaByStaffData.Eva3Time != evaByStaff.Eva3Time
                                || _evaByStaffData.Eva3StaffCD != evaByStaff.Eva3StaffCD
                                || _evaByStaffData.StatffEva != evaByStaff.StatffEva)
                            {

                                    _evaByStaffData.CareFlg = evaByStaff.CareFlg;
                                    _evaByStaffData.Record = evaByStaff.Record;
                                    _evaByStaffData.Eva1Date = evaByStaff.Eva1Date;
                                    _evaByStaffData.Eva1Result = evaByStaff.Eva1Result;
                                    _evaByStaffData.Eva1Time = evaByStaff.Eva1Time;
                                    _evaByStaffData.Eva1StaffCD = evaByStaff.Eva1StaffCD;
                                    _evaByStaffData.Eva2Date = evaByStaff.Eva2Date;
                                    _evaByStaffData.Eva2Result = evaByStaff.Eva2Result;
                                    _evaByStaffData.Eva2Time = evaByStaff.Eva2Time;
                                    _evaByStaffData.Eva2StaffCD = evaByStaff.Eva2StaffCD;
                                    _evaByStaffData.Eva3Date = evaByStaff.Eva3Date;
                                    _evaByStaffData.Eva3Result = evaByStaff.Eva3Result;
                                    _evaByStaffData.Eva3Time = evaByStaff.Eva3Time;
                                    _evaByStaffData.Eva3StaffCD = evaByStaff.Eva3StaffCD;
                                    _evaByStaffData.StatffEva = evaByStaff.StatffEva;

                                    if (!string.IsNullOrEmpty(_evaByStaffData.Eva1Result) || !string.IsNullOrEmpty(_evaByStaffData.Eva2Result) || !string.IsNullOrEmpty(_evaByStaffData.Eva3Result))
                                    {
                                        if (_evaByStaffData.Eva1Result == "0" || _evaByStaffData.Eva2Result == "0" || _evaByStaffData.Eva3Result == "0")
                                        {
                                            _evaByStaffData.EvaResultFlag = "0";
                                        }
                                        else
                                        {
                                            _evaByStaffData.EvaResultFlag = "1";
                                        }
                                    }
                                    else
                                    {
                                        _evaByStaffData.EvaResultFlag = null;
                                    }

                                    _evaByStaffData.UpdateTime = CommonUtils.getLocalDateTime();
                                    _evaByStaffData.UpdateUserID = (Session["user"] as UserSession).StaffCD;

                                    if (String.IsNullOrEmpty(_evaByStaffData.StatffEva))
                                    {
                                        T_EvaByLeader _evaByLeaderData = db.EvaByLeaders.Find(int.Parse(PartyID));
                                        if (_evaByLeaderData != null)
                                        {
                                            _evaByLeaderData.LeaderEva = string.Empty;
                                            db.Entry(_evaByLeaderData).State = EntityState.Modified;
                                        }
                                    }

                                    db.Entry(_evaByStaffData).State = EntityState.Modified;

                            }
                        }
                        else
                        {
                            haita = true;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(evaByStaff.Eva1Result) || !string.IsNullOrEmpty(evaByStaff.Eva2Result) || !string.IsNullOrEmpty(evaByStaff.Eva3Result))
                    {
                        if (evaByStaff.Eva1Result == "0" || evaByStaff.Eva2Result == "0" || evaByStaff.Eva3Result == "0")
                        {
                            evaByStaff.EvaResultFlag = "0";
                        }
                        else
                        {
                            evaByStaff.EvaResultFlag = "1";
                        }
                    }
                    evaByStaff.PartyID = int.Parse(PartyID);
                    evaByStaff.UpdateTime = DateTime.Now;
                    evaByStaff.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                    db.EvaByStaffs.Add(evaByStaff);
                }

                if (!haita)
                {
                    db.SaveChanges();
                    ViewBag.msg = "データを保存しました。";
                    string resultMsg = "データを保存しました。";
                    if (actionType != "1") {

                        
                        //20151110
                        //「要確認」「要対応」フラグにチェックがつき、保存ボタンが押された際にメール送付。
                        string CareFlgStr = "";
                        if (evaByStaff.CareFlg == "0") {
                            CareFlgStr = "要確認";
                        }
                        else if (evaByStaff.CareFlg == "1")
                        {
                            CareFlgStr = "要対応";
                        }

                        string msgType = "";

                        if (string.IsNullOrEmpty(EmailTo)==false && CareFlgStr != "") { 
                            //Subject
                            string Subject = "【" + CareFlgStr + "・カスタマーセンター】テレレポ必ずご確認ください！！(" + _partyData.PartyNo + ") ※メールへの返信不可";
                            ////not reply
                            //Subject += "（返信しないでください）"; 
                            //内容
                            string Body = "";
                            //Head
                            Body += WebUtility.HtmlDecode(EmailHead) + "<br />";
                            //【店舗】：
                            Body += "【店舗】　　：" + _partyData.ShopCD + "<br />";
                            //【パーティ】：
                            //Body += "【パーティ】：" + evaByStaff.PartyID + "<br />";
                            //【挙式日】：
                            Body += "【挙式日】　：" + _partyData.PartyDate.ToString("yyyy/MM/dd") + "　" + _partyData.StartTime + "<br />";

                            //【担当者】：
                            Body += "【担当者】　：";
                            if (null != _partyData && _partyData.TantoCD.Length > 0)
                            {
                                M_Staff staff = db.Staffs.Find(_partyData.TantoCD);
                                if (null != staff)
                                {
                                    Body += staff.StaffName + "<br /><br />";
                                }
                            }

                            //【特記事項】：
                            Body += "【特記事項】：";
                            Body += "<div style='max-width:900px; border: 0; margin: 5px; padding: 0 40px;'>" + WebUtility.HtmlDecode(report.Memo) + "</div>";


                            //【電話内容】：
                            Body += "【電話内容】：";
                            Body += "<div style='max-width:900px; border: 0; margin: 5px; padding: 0 40px;'>" + WebUtility.HtmlDecode(evaByStaff.Record) + "</div><br />";

                            //To
                            List<string> sendTo = new List<string>();
                            string[] toStrs = EmailTo.Split(',');
                            foreach (string toStaffCD in toStrs) {
                                M_Staff staff = db.Staffs.Find(toStaffCD);
                                if (null != staff && string.IsNullOrEmpty(staff.Email) == false)
                                {
                                    sendTo.Add(staff.Email);
                                }
                            }

                            List<string> CCTo = new List<string>();
                            if (string.IsNullOrEmpty(EmailCc) == false) {
                                string[] ccStrs = EmailCc.Split(',');
                                foreach (string ccStaffCD in ccStrs)
                                {
                                    M_Staff staff = db.Staffs.Find(ccStaffCD);
                                    if (null != staff && string.IsNullOrEmpty(staff.Email) == false)
                                    {
                                        CCTo.Add(staff.Email);
                                    }
                                }
                            }

                            List<string> BCCTo = new List<string>();
                            BCCTo.Add(EmailBCc);

                            Body = "<div style='font-family: \"Meiryo UI\";'>" + Body + "</div>";

                            //送信
                            if (sendTo.Count > 0) {
                                //new Thread(() => {

                                //    EmailUtils.SendMailAsync(sendTo, CCTo,BCCTo, true, Subject, Body);
                                
                                //}).Start();


                                String emailResult = EmailUtils.SendMail(sendTo, CCTo, BCCTo, true, Subject, Body);
                                if (emailResult == "OK")
                                {
                                    resultMsg = "正常にメールを送信しました。データを保存しました。";
                                }
                                else 
                                {
                                    resultMsg = "データを保存しました。正常にメールを送信失敗しました。「" + emailResult + "」";
                                    msgType = "error";
                                }
                            }

                        }

                        return RedirectToAction("EvaByStaffIndex", "EvaByStaff", new { page = page, ShopCD = ShopCD, PartyDateFrom = PartyDateFrom, PartyDateTo = PartyDateTo, StaffEva = StaffEva, LeaderEva = LeaderEva, isPostBack = "1", Range = Range, divisionCD = divisionCD, StaffCD = StaffCD, msg = resultMsg, msgType = msgType });
                    }
                    else
                    {
                        return RedirectToAction("TelRptEvaByStaff", "TelRpt", new { page = page, PartyID = PartyID, ShopCD = ShopCD, PartyDateFrom = PartyDateFrom, PartyDateTo = PartyDateTo, isPartyReport = isPartyReport, StaffEva = StaffEva, LeaderEva = LeaderEva, id = id, Range = Range, divisionCD = divisionCD, StaffCD = StaffCD, msg = resultMsg });
                    }
                }
                else
                {
                    string updateUser = "";
                    M_Staff staff = db.Staffs.Find(_reportData.UpdateUserID);
                    if (staff != null)
                    {
                        updateUser = staff.StaffName;
                    }
                    ViewBag.msg = "編集中のデータは他の端末(" + updateUser + ")より更新されました。更新できません。一覧画面から、再度入力してください。";
                    ViewBag.msgType = "error";
                }
            }

            T_Party _party = db.Partys.Find(id);
            T_Report _report = db.Reports.Find(id);
            T_EvaByStaff _evaByStaff = db.EvaByStaffs.Find(id);
            T_EvaByLeader _evaByLeader = db.EvaByLeaders.Find(id);

            if (null != _party) 
            {
                //20141010　李梁　_party.UpdateTime.Value ⇒ _party.UpdateTime
                ViewBag.PartyUpdateTime = _party.UpdateTime == null ? "" : _party.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                //20141010　李梁
                if (haita && null != model.party)
                {
                    _party.FinishFlag = model.party.FinishFlag;
                    ViewBag.PartyUpdateTime = PartyUpdateTime;
                }
            }  

            if (null != _report)
            {
                //20141010   李梁　_report.UpdateTime.Value ⇒ _report.UpdateTime
                ViewBag.ReportUpdateTime = _report.UpdateTime == null ? "" : _report.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                //20141010   李梁
                if (haita && null != model.report)
                {
                    _report.TelFlg = model.report.TelFlg;
                    _report.Memo = model.report.Memo;
                    ViewBag.ReportUpdateTime = ReportUpdateTime;
                }
                ViewBag.telFlg = _report.TelFlg;
            }

            if (null != _evaByStaff)
            {
                //20141010　李梁　_evaByStaff.UpdateTime.Value ⇒  _evaByStaff.UpdateTime
                ViewBag.EvaByStaffUpdateTime = _evaByStaff.UpdateTime == null ? "" : _evaByStaff.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                ViewBag.UpdateTime = _evaByStaff.UpdateTime == null ? "" : _evaByStaff.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                //20141010　李梁
                ViewBag.UpdateUser = GetStaffName(_evaByStaff.UpdateUserID);
                ViewBag.UpdateUserID = _evaByStaff.UpdateUserID;

                if (haita && null != model.evaByStaff)
                {
                    _evaByStaff.CareFlg = model.evaByStaff.CareFlg;
                    _evaByStaff.Record = model.evaByStaff.Record;
                    _evaByStaff.Eva1Date = model.evaByStaff.Eva1Date;
                    _evaByStaff.Eva1Result = model.evaByStaff.Eva1Result;
                    _evaByStaff.Eva1Time = model.evaByStaff.Eva1Time;
                    _evaByStaff.Eva1StaffCD = model.evaByStaff.Eva1StaffCD;
                    _evaByStaff.Eva2Date = model.evaByStaff.Eva2Date;
                    _evaByStaff.Eva2Result = model.evaByStaff.Eva2Result;
                    _evaByStaff.Eva2Time = model.evaByStaff.Eva2Time;
                    _evaByStaff.Eva2StaffCD = model.evaByStaff.Eva2StaffCD;
                    _evaByStaff.Eva3Date = model.evaByStaff.Eva3Date;
                    _evaByStaff.Eva3Result = model.evaByStaff.Eva3Result;
                    _evaByStaff.Eva3Time = model.evaByStaff.Eva3Time;
                    _evaByStaff.Eva3StaffCD = model.evaByStaff.Eva3StaffCD;
                    _evaByStaff.StatffEva = model.evaByStaff.StatffEva;
                    ViewBag.EvaByStaffUpdateTime = EvaByStaffUpdateTime;
                    ViewBag.UpdateTime = EvaByStaffUpdateTime;
                    ViewBag.UpdateUser = UpdateUser;
                    ViewBag.UpdateUserID = UpdateUserID;
                }
            }

            if (null != _party && _party.TantoCD.Length > 0)
            {
                M_Staff staff = db.Staffs.Find(_party.TantoCD);
                if (null != staff)
                {
                    //_party.TantoCD = staff.StaffName;
                    ViewBag.StaffName = staff.StaffName;
                }
            }

            model = new TelRptEvaViewModel();

            model.party = _party == null ? new T_Party() : _party;
            model.report = _report == null ? new T_Report() : _report;
            model.evaByStaff = _evaByStaff == null ? new T_EvaByStaff() : _evaByStaff;
            model.evaByLeader = _evaByLeader == null ? new T_EvaByLeader() : _evaByLeader;

            GetDefaultEmailaddr(_party.ShopCD, _party.TantoCD);

            return View(model);
        }

        public ActionResult TelRptEvaByLeader(TelRptEvaViewModel model, int id, string PartyID, string ShopCD, string PartyDateFrom, string PartyDateTo, string EvaStatus, string StaffEva, string LeaderEva, string EvaByLeaderUpdateTime, string UpdateUser, string UpdateUserID, string Range, string divisionCD, string StaffCD, string page, string actionType, string isPartyReport)
        {
            ViewBag.ShopCD = ShopCD;
            ViewBag.PartyDateFrom = PartyDateFrom;
            ViewBag.PartyDateTo = PartyDateTo;
            ViewBag.EvaStatus = EvaStatus;
            ViewBag.StaffEva = StaffEva;
            ViewBag.LeaderEva = LeaderEva;
            ViewBag.PartyID = PartyID;
            ViewBag.page = page;
            ViewBag.isPartyReport = isPartyReport;

            ViewBag.Range = Range;
            ViewBag.divisionCD = divisionCD;
            ViewBag.StaffCD = StaffCD;

            bool haita = false;

            if (null != model.evaByLeader)
            {
                T_EvaByLeader evaByLeader = model.evaByLeader;

                T_EvaByLeader _evaByLeaderData = db.EvaByLeaders.Find(int.Parse(PartyID));

                if (null != _evaByLeaderData)
                {
                    //20141010　李梁　_evaByLeaderData.UpdateTime.Value ⇒ _evaByLeaderData.UpdateTime
                    if (EvaByLeaderUpdateTime == (_evaByLeaderData.UpdateTime == null ? "" : _evaByLeaderData.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss")))
                    //20141010　李梁
                    {
                        _evaByLeaderData.LeaderEva = evaByLeader.LeaderEva;
                        _evaByLeaderData.UpdateTime = CommonUtils.getLocalDateTime();
                        _evaByLeaderData.UpdateUserID = (Session["user"] as UserSession).StaffCD;

                        db.Entry(_evaByLeaderData).State = EntityState.Modified;
                    }
                    else
                    {
                        haita = true;
                    }
                }
                else
                {
                    evaByLeader.PartyID = int.Parse(PartyID);
                    evaByLeader.UpdateTime = CommonUtils.getLocalDateTime();
                    evaByLeader.UpdateUserID = (Session["user"] as UserSession).StaffCD;
                    db.EvaByLeaders.Add(evaByLeader);
                }

                if (!haita)
                {
                    db.SaveChanges();
                    ViewBag.msg = "データを保存しました。";
                    if (actionType != "1")
                    {
                        return RedirectToAction("EvaByLeaderIndex", "EvaByLeader", new { page = page, ShopCD = ShopCD, PartyDateFrom = PartyDateFrom, PartyDateTo = PartyDateTo, EvaStatus = EvaStatus, StaffEva = StaffEva, LeaderEva = LeaderEva, isPostBack = "1", Range = Range, divisionCD = divisionCD, StaffCD = StaffCD, msg = "データを保存しました。" });
                    }
                    else
                    {
                        return RedirectToAction("TelRptEvaByLeader", "TelRpt", new { page = page, PartyID = PartyID, ShopCD = ShopCD, PartyDateFrom = PartyDateFrom, PartyDateTo = PartyDateTo, EvaStatus = EvaStatus, StaffEva = StaffEva, LeaderEva = LeaderEva, isPartyReport = isPartyReport, id = id, Range = Range, divisionCD = divisionCD, StaffCD = StaffCD, msg = "データを保存しました。" });
                    }
                }
                else
                {
                    string updateUser = "";
                    M_Staff staff = db.Staffs.Find(_evaByLeaderData.UpdateUserID);
                    if (staff != null)
                    {
                        updateUser = staff.StaffName;
                    }
                    ViewBag.msg = "編集中のデータは他の端末(" + updateUser + ")より更新されました。更新できません。一覧画面から、再度入力してください。";
                    ViewBag.msgType = "error";
                }
            }

            T_Party _party = db.Partys.Find(id);
            T_Report _report = db.Reports.Find(id);
            T_EvaByStaff _evaByStaff = db.EvaByStaffs.Find(id);
            T_EvaByLeader _evaByLeader = db.EvaByLeaders.Find(id);

            if (null != _evaByLeader)
            {
                //20141010　李梁　_evaByLeader.UpdateTime.Value ⇒ _evaByLeader.UpdateTime
                ViewBag.EvaByLeaderUpdateTime =(  _evaByLeader.UpdateTime == null ? "" : _evaByLeader.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss") );
                ViewBag.UpdateTime = ( _evaByLeader.UpdateTime == null ? "" : _evaByLeader.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss") );
                //20141010　李梁
                ViewBag.UpdateUser = GetStaffName(_evaByLeader.UpdateUserID);
                ViewBag.UpdateUserID = _evaByLeader.UpdateUserID;
                if (haita && null != model.evaByLeader)
                {
                    _evaByLeader.LeaderEva = model.evaByLeader.LeaderEva;
                    ViewBag.EvaByLeaderUpdateTime = EvaByLeaderUpdateTime;
                    ViewBag.UpdateTime = EvaByLeaderUpdateTime;
                    ViewBag.UpdateUser = UpdateUser;
                    ViewBag.UpdateUserID = UpdateUserID;
                }
            }

            if (null != _party && _party.TantoCD.Length > 0)
            {
                _party.TantoCD = GetStaffName(_party.TantoCD);
            }

            if (null != _evaByStaff)
            {
                if (null != _evaByStaff.Eva1StaffCD && _evaByStaff.Eva1StaffCD.Length > 0)
                {
                    _evaByStaff.Eva1StaffCD = GetStaffName(_evaByStaff.Eva1StaffCD);
                }

                if (null != _evaByStaff.Eva2StaffCD && _evaByStaff.Eva2StaffCD.Length > 0)
                {
                    _evaByStaff.Eva2StaffCD = GetStaffName(_evaByStaff.Eva2StaffCD);
                }

                if (null != _evaByStaff.Eva3StaffCD && _evaByStaff.Eva3StaffCD.Length > 0)
                {
                    _evaByStaff.Eva3StaffCD = GetStaffName(_evaByStaff.Eva3StaffCD);
                }
            }

            model = new TelRptEvaViewModel();

            model.party = _party == null ? new T_Party() : _party;
            model.report = _report == null ? new T_Report() : _report;
            model.evaByStaff = _evaByStaff == null ? new T_EvaByStaff() : _evaByStaff;
            model.evaByLeader = _evaByLeader == null ? new T_EvaByLeader() : _evaByLeader;

            return View(model);  
        }
        /*テレフォンレポート検索-----2014-07-04-----李*/
        public ActionResult TelRptSearch(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string StaffCD, string StaffEva, string LeaderEva, string Sort, List<string> Tel,string SortRate, string GroupBy, string isPostBack, int? page)
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
            ViewBag.StaffEva = StaffEva;
            ViewBag.LeaderEva = LeaderEva;
            ViewBag.Sort = Sort;
            
            if (Sort == null) 
            {
                ViewBag.Sort = "partyDate";
            }

            
            ViewBag.GroupBy = GroupBy;
            ViewBag.SortRate = SortRate;
            if (GroupBy == null)
            {
                ViewBag.GroupBy = "shop";
            }
            if (SortRate == null)
            {
                ViewBag.SortRate = "unit";
            }
            
            ViewBag.Tel = Tel;
            

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

                int pageSize = 25;
                int pageNumber = (page ?? 1);

                int totalItemCount = 0;

                var models = GetModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, shopCD_Change, staffCD_Change, StaffEva, LeaderEva, Sort, Tel,pageSize, pageNumber, true, ref totalItemCount);

                if (models.ToList().Count == 0)
                {
                    ViewBag.msg = "データがありません。";
                    ViewBag.msgType = "info";
                }
                DyPagedList<TelRptViewModel> lit = new DyPagedList<TelRptViewModel>(models, pageNumber, pageSize, totalItemCount);


                GC.Collect();

                return View(lit);
            }
            else
            {
                if (Tel == null)
                {
                    ViewBag.Tel = new List<string> { "memo_special" };
                }
                ViewBag.isPostBack = "1";
                return View();
            }
        }
        
        public ActionResult TelRptIndex(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string StaffCD, string StaffEva, string LeaderEva, string Sort, List<string> Tel, int? page)
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
            ViewBag.StaffEva = StaffEva;
            ViewBag.LeaderEva = LeaderEva;
            ViewBag.Sort = Sort;
            ViewBag.Tel = Tel;
            if (Tel == null)
            {
                ViewBag.Tel = new List<string> { "null" };
            }

            int totalItemCount = 0;
            var models = GetModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, ShopCD, StaffCD, StaffEva, LeaderEva, Sort, Tel,2000, 0, false, ref totalItemCount);

            if (models.Count() == 0)
            {
                ViewBag.msg = "データがありません。";
                ViewBag.msgType = "info";
            }
            GC.Collect();
            return View(models);
        }


        public ActionResult TelRptIndexPreview(string Time, string Year, string Season, string Months, string DateFrom, string DateTo,string Range, string divisionCD, string ShopCD, string StaffCD, string StaffEva, string LeaderEva, string Sort, List<string> Tel)
        {
            ViewBag.Sort = Sort;


            int totalItemCount = 0;
            var models = GetModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, ShopCD, StaffCD, StaffEva, LeaderEva, Sort, Tel,2000, 1, true, ref totalItemCount);

            List<TelRptViewModel> modelDatas = models.ToList();

            if (modelDatas.Count == 0) {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            ExcelReport report = new ExcelReport();
            //ReportType
            report.ReportType = EnumReportType.ListReport;
            report.isAutoRowHeight = true;

            //Template
            report.FileName = "テレフォンレポート一覧";
            //李梁 2014.09.12
            if ((Session["user"] as WebEvaluation.DataModels.UserSession).RoleCD == "02" || (Session["user"] as WebEvaluation.DataModels.UserSession).RoleCD == "04" || (Session["user"] as WebEvaluation.DataModels.UserSession).RoleCD == "09")
            {
                if (Sort == "partyDate") 
                {
                    report.Template = "テレフォンレポート一覧.xls";
                }
                else if (Sort == "staff")
                {
                    report.Template = "テレフォンレポート一覧2.xls";
                }
                else if (Sort == "shop")
                {
                    report.Template = "テレフォンレポート一覧3.xls";
                }
            }
            else 
            {
                if (Sort == "partyDate")
                {
                    report.Template = "テレフォンレポート一覧_s1.xls";
                }
                else if (Sort == "staff")
                {
                    report.Template = "テレフォンレポート一覧_s2.xls";
                }
                else if (Sort == "shop")
                {
                    report.Template = "テレフォンレポート一覧_s3.xls";
                }
            }

            
            

            //データ
            List<IReportData> datas = new List<IReportData>();

            //headerデータ
            TelRptReportHeaderModel header = new TelRptReportHeaderModel();
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
            foreach (TelRptViewModel viewModel in modelDatas) {
                TelRptReportDetailModel detail = new TelRptReportDetailModel();

                detail.No = index;
                if(viewModel.HoldDate != null){
                    detail.HoldDate = viewModel.HoldDate.ToString("yyyy/MM/dd");
                }
                detail.StaffName = viewModel.StaffName;
                detail.ShopName = viewModel.ShopName;
                detail.StaffCD = viewModel.StaffCD;
                detail.HallType = viewModel.HallType;
                detail.StartTime = viewModel.StartTime;
                detail.TelMemo = "";
                detail.StaffEva = viewModel.StaffEva;
                detail.LeaderEva = viewModel.LeaderEva;

                if (null != Tel && Tel.Contains("memo_special") && !String.IsNullOrEmpty(viewModel.TelMemo_special))
                {
                    detail.TelMemo += "【特記事項】" + System.Environment.NewLine;
                    detail.TelMemo += viewModel.TelMemo_special +System.Environment.NewLine;

                }
                if (!String.IsNullOrEmpty(viewModel.TelMemo))
                {
                    detail.TelMemo += "【電話内容】" + System.Environment.NewLine;
                    detail.TelMemo += viewModel.TelMemo +System.Environment.NewLine;

                }
                

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

        public ActionResult Index(string Time, string Year, string Season, string Months, string DateFrom, string DateTo,string Range, string divisionCD, string ShopCD, string StaffCD, string StaffEva, string LeaderEva, string Sort, List<string> Tel, int? page)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            int totalItemCount = 0;

            var models = GetModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, ShopCD, StaffCD, StaffEva, LeaderEva, Sort, Tel,pageSize, pageNumber, true, ref totalItemCount);

            DyPagedList<TelRptViewModel> lit = new DyPagedList<TelRptViewModel>(models, pageNumber, pageSize, totalItemCount);
            GC.Collect();
            return View(lit);
        }

        //全データ取得
        private IEnumerable<TelRptViewModel> GetModels_old(string Time, string Year, string Season, string Months, string DateFrom, string DateTo,string Range, string divisionCD, string ShopCD, string StaffCD, string StaffEva, string LeaderEva, string Sort, List<string> Tel)
        {

            List<M_Division> divisions = db.Divisions.ToList();

            List<T_Report> reports = new List<T_Report>();
            List<T_Party> partys = new List<T_Party>();
            List<M_Shop> shops = new List<M_Shop>();
            List<T_EvaByLeader> evabyleaders = new List<T_EvaByLeader>();
            List<T_EvaByStaff> evabystaffs = new List<T_EvaByStaff>();
            List<M_Staff> staffs = new List<M_Staff>();
            List<M_Group> groups = new List<M_Group>();

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
                        partys = partyQuery.ToList();

                        reports = (from report in db.Reports

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
                        evabystaffs = (from staffEva in db.EvaByStaffs

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
                        evabyleaders = (from leaderEva in db.EvaByLeaders

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

                        shops = db.Shops.Where(s => s.GroupCD == divisionCD).ToList();
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
                        partys = partyQuery.ToList();

                        //reports
                        reports = (from report in db.Reports

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
                        evabystaffs = (from staffEva in db.EvaByStaffs

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
                        evabyleaders = (from leaderEva in db.EvaByLeaders

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
                    partys = partyQuery.ToList();

                    //reports
                    reports = (from report in db.Reports

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
                    evabystaffs = (from staffEva in db.EvaByStaffs

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
                    evabyleaders = (from leaderEva in db.EvaByLeaders

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

                    shops = db.Shops.Where(p => p.ShopCD == ShopCD).ToList();

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
                    partys = partyQuery.ToList();

                    //reports
                    reports = (from report in db.Reports

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
                    evabystaffs = (from staffEva in db.EvaByStaffs

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
                    evabyleaders = (from leaderEva in db.EvaByLeaders

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

                    staffs = db.Staffs.Where(p => p.StaffCD == StaffCD).ToList();

                    noCondition = false;
                }
            }


            //Partys = db.Partys.Where(p => p.PartyDate.CompareTo(dateFrom) >= 0 && p.PartyDate.CompareTo(dateTo) <= 0).ToList();
            if (partys.Count == 0 && noCondition) partys = db.Partys.Where(p => p.PartyDate.CompareTo(dateFrom) >= 0 && p.PartyDate.CompareTo(dateTo) <= 0).ToList();



            //reports
            if (reports.Count == 0 && noCondition)
            {
                reports = (from report in db.Reports

                           join party in db.Partys on report.PartyID equals party.PartyID into g_party_party
                           from p_party in g_party_party

                           where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

                           select report).ToList();
            }
            

            //StaffEva
            if (evabystaffs.Count == 0 && noCondition)
            {
                evabystaffs = (from staffEva in db.EvaByStaffs

                               join party in db.Partys on staffEva.PartyID equals party.PartyID into g_party_party
                               from p_party in g_party_party

                               where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

                               select staffEva).ToList();
            }
            

            //LeaderEva
            if (evabyleaders.Count == 0 && noCondition)
            {
                evabyleaders = (from leaderEva in db.EvaByLeaders

                                join party in db.Partys on leaderEva.PartyID equals party.PartyID into g_party_party
                                from p_party in g_party_party

                                where (p_party.PartyDate.CompareTo(dateFrom) >= 0 && p_party.PartyDate.CompareTo(dateTo) <= 0)

                                select leaderEva).ToList();
            }
            
            #endregion

            #region  findAll
            if (shops.Count == 0) shops = db.Shops.ToList();
            if (staffs.Count == 0) staffs = db.Staffs.ToList();
            if (groups.Count == 0) groups = db.Groups.ToList();
            #endregion

            var models = from party in partys
                         join shop in shops on party.ShopCD equals shop.ShopCD into g_party_shop
                         from p_shop in g_party_shop.DefaultIfEmpty(new M_Shop())
                         join report in reports on party.PartyID equals report.PartyID into g_party_report
                         from p_report in g_party_report.DefaultIfEmpty(new T_Report())
                         join staff in staffs on party.TantoCD equals staff.StaffCD into g_party_staff
                         from p_staff in g_party_staff.DefaultIfEmpty(new M_Staff())
                         join evaByLeader in evabyleaders on party.PartyID equals evaByLeader.PartyID into g_party_evaByLeader
                         from p_evaByLeader in g_party_evaByLeader.DefaultIfEmpty(new T_EvaByLeader())
                         join staffEva in evabystaffs on party.PartyID equals staffEva.PartyID into g_party_staffEva
                         from p_staffEva in g_party_staffEva.DefaultIfEmpty(new T_EvaByStaff())
                         join grp in groups on p_shop.GroupCD equals grp.GroupCD into g_shop_group
                         from s_group in g_shop_group.DefaultIfEmpty(new M_Group())
                         join div in divisions on s_group.DivCD equals div.DivCD into g_group_div
                         from g_div in  g_group_div.DefaultIfEmpty(new M_Division())
                         where(p_report.PartyID != 0 || p_evaByLeader.PartyID !=0 || p_staffEva.PartyID != 0)
                         select new TelRptViewModel
                         {
                             PartyNo = party.PartyNo,
                             ShopCD = p_shop.ShopCD,
                             ShopName = p_shop.ShopCD,
                             DivisionCD = g_div.DivCD,
                             GroupCD = p_shop.GroupCD,
                             HoldDate = party.PartyDate,
                             StaffCD = p_staff.StaffCD,
                             BrideName = party.BrideName,
                             GroomName = party.GroomName,
                             StaffName = p_staff.StaffName,
                             StaffKana = p_staff.StaffKana,
                             TelMemo = p_staffEva.Record,
                             StartTime = party.StartTime,
                             TelMemo_special = p_report.Memo,
                             HallType = party.HallType,
                             StaffEva = p_staffEva.StatffEva,
                             LeaderEva = p_evaByLeader.LeaderEva
                         };

            
            if (!string.IsNullOrEmpty(StaffEva))
            {
                if (StaffEva == "ZA")
                {
                    models = models.Where(p => String.IsNullOrEmpty(p.StaffEva));
                }
                else if (StaffEva == "ZB")
                {
                    models = models.Where(p => !String.IsNullOrEmpty(p.StaffEva));
                }
                else
                {
                    models = models.Where(p => p.StaffEva == StaffEva);
                }
            }

            if (!string.IsNullOrEmpty(LeaderEva))
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

            if (!string.IsNullOrEmpty(Sort))
            {
                if (Sort == "partyDate")
                {
                    models = models.OrderBy(p => p.HoldDate).ThenBy(p => p.ShopCD).ThenBy(p => p.StaffCD);
                }
                else if (Sort == "staff")
                {
                    models = models.OrderBy(p => p.StaffCD).ThenBy(p => p.ShopCD).ThenBy(p => p.HoldDate);
                }
                else if (Sort == "shop")
                {
                    models = models.OrderBy(p => p.ShopCD).ThenBy(p => p.HoldDate).ThenBy(p => p.StaffCD);
                }
            }

            return models;

        }

        //全データ取得
        private List<TelRptViewModel> GetModels(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string StaffCD, string StaffEva, string LeaderEva, string Sort, List<string> Tel,int pageSize, int pNumber, bool GetPageCount, ref int total)
        {
            DyEntityLogic logic = new DyEntityLogic();

            DynamicsViewRequest req = new DynamicsViewRequest();
            req.ProjID = 1;
            req.EntityName = "T_Party";
            req.ViewName = "PartyEvaIndex_OrderByPartyDate";
            req.pageNumber = pNumber;
            req.GetPageCount = GetPageCount;
            req.GetPageSize = pageSize;

            
            if (!string.IsNullOrEmpty(Sort))
            {
                if (Sort == "partyDate")
                {
                    req.ViewName = "PartyEvaIndex_OrderByPartyDate";
                }
                else if (Sort == "staff")
                {
                    req.ViewName = "PartyEvaIndex_OrderByStaff";
                }
                else if (Sort == "shop")
                {
                    req.ViewName = "PartyEvaIndex_OrderByShop";
                }
            }
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

            req.FilterDic = new Dictionary<string, string>();

            req.FilterDic.Add("PartyDateFormFilter", string.Format("{0:yyyy/MM/dd}", dateFrom));
            req.FilterDic.Add("PartyDateToFilter", string.Format("{0:yyyy/MM/dd}", dateTo));

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

            PageViewResult list = logic.GetList(req);

            List<TelRptViewModel> models = new List<TelRptViewModel>();

            if (list.DataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in list.DataTable.Rows)
                {
                    TelRptViewModel m = new TelRptViewModel();
                    m.PartyNo = DataUtil.CStr(dr["PartyNo"]);
                    m.ShopCD = DataUtil.CStr(dr["ShopCD"]);
                    m.ShopName = DataUtil.CStr(dr["ShopCD"]);
                    m.DivisionCD = DataUtil.CStr(dr["DivCD"]);
                    m.GroupCD = DataUtil.CStr(dr["GroupCD"]);
                    m.HoldDate = DataUtil.CDate(dr["PartyDate"]);
                    m.StaffCD = DataUtil.CStr(dr["StaffCD"]);
                    m.BrideName = DataUtil.CStr(dr["BrideName"]);
                    m.GroomName = DataUtil.CStr(dr["GroomName"]);
                    m.StaffName = DataUtil.CStr(dr["StaffName"]);
                    m.StaffKana = DataUtil.CStr(dr["StaffKana"]);
                    m.TelMemo = DataUtil.CStr(dr["Record"]);
                    m.StartTime = DataUtil.CStr(dr["StartTime"]);
                    m.TelMemo_special = DataUtil.CStr(dr["Memo"]);
                    m.HallType = DataUtil.CStr(dr["HallType"]);
                    m.StaffEva = DataUtil.CStr(dr["StatffEva"]);
                    m.LeaderEva = DataUtil.CStr(dr["LeaderEva"]);

                    models.Add(m);
                }
            }

            total = list.PageCount;

            return models;

        }

        public void GetDefaultEmailaddr(string shopCD,string staffCD)
        {
            string EmailToCD = "";
            string EmailToName = "";
            string EmailCcCD = "";
            string EmailCcName = "";

            var Staffs = db.Staffs.ToList();
            var Units = db.Units.ToList();

            var models = from staff in Staffs
                         join units in Units on staff.UnitCD equals units.UnitCD into g_staff_units
                         from s_unit in g_staff_units.DefaultIfEmpty(new S_Unit())

                         where ((s_unit.ShopCD == shopCD
                            && (staff.Yakusyoku == "営業（支配人）"
                                || staff.Yakusyoku == "営業(支配人)"
                                || staff.Yakusyoku == "料飲（シェフ）"
                                || staff.Yakusyoku == "料飲(シェフ)"
                                || staff.Yakusyoku == "FC（チーフ）"
                                || staff.Yakusyoku == "FC(チーフ)"
                                || staff.Yakusyoku == "PDチーフ"
                                || staff.Yakusyoku == "プロデューサー"
                                || staff.Yakusyoku == "ﾌﾟﾛﾃﾞｭｰｻｰ"
                                || staff.Yakusyoku == "営業（プランナー）"
                                || staff.Yakusyoku == "営業(プランナー)"
                                || staff.Yakusyoku == "WP")) || staff.StaffCD == staffCD)
                                && string.IsNullOrEmpty(staff.Email) == false
                         select new StaffViewModel
                         {
                             StaffCD = staff.StaffCD,
                             StaffName = staff.StaffName,
                             StaffKana = staff.StaffKana,
                             Sex = staff.Sex,
                             EnrollmentDate = staff.EnrollmentDate,
                             Duty = staff.Duty,
                             Yakusyoku = staff.Yakusyoku,
                             UnitCD = staff.UnitCD,
                             EMail = staff.Email,
                             UnitName = s_unit.UnitName
                         };

            //
            bool hasMaster = false;
            foreach (StaffViewModel staff in models) {
                if (staff.Yakusyoku == "営業（支配人）" || staff.Yakusyoku == "営業(支配人)")
                {
                    //if (hasMaster)
                    //{
                    //    EmailToCD = "";
                    //    EmailToName = "";
                    //}
                    //else
                    //{
                    //    hasMaster = true;
                    //    EmailToCD = staff.StaffCD;
                    //    EmailToName = staff.StaffName;
                    //}
                    if (EmailToCD == "")
                    {
                        EmailToCD = staff.StaffCD;
                        EmailToName = staff.StaffName;
                    }
                    else
                    {
                        EmailToCD += "," + staff.StaffCD;
                        EmailToName += "、　" + staff.StaffName;
                    }
                }
                else if ((staff.Yakusyoku != "営業（プランナー）" && staff.Yakusyoku != "営業(プランナー)") || staff.StaffCD == staffCD)
                {
                    if (EmailCcCD == "")
                    {
                        EmailCcCD = staff.StaffCD;
                        EmailCcName = staff.StaffName;
                    }
                    else {
                        EmailCcCD += ","+staff.StaffCD;
                        EmailCcName += "、　" + staff.StaffName;
                    }
                }
            }

            if (EmailToCD.Length == 0)
            {
                var planer = models.Where(m => m.Yakusyoku == "営業（プランナー）" || m.Yakusyoku == "営業(プランナー)");
                foreach (StaffViewModel staff in planer)
                {
                    if (EmailToCD == "")
                    {
                        EmailToCD = staff.StaffCD;
                        EmailToName = staff.StaffName;
                    }
                    else
                    {
                        EmailToCD += "," + staff.StaffCD;
                        EmailToName += "、　" + staff.StaffName;
                    }
                }
            }

            ViewBag.EmailToCD = EmailToCD;
            ViewBag.EmailToName = EmailToName;
            ViewBag.EmailCcCD = EmailCcCD;
            ViewBag.EmailCcName = EmailCcName;
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns></returns>
        public ActionResult csv(string Time, string Year, string Season, string Months, string DateFrom, string DateTo, string Range, string divisionCD, string ShopCD, string StaffCD, string StaffEva, string LeaderEva, string Sort, List<string> Tel)
        {

            int totalItemCount = 0;

            List<TelRptViewModel> models = GetModels(Time, Year, Season, Months, DateFrom, DateTo, Range, divisionCD, ShopCD, StaffCD, StaffEva, LeaderEva, Sort, Tel,2000, 0, false, ref totalItemCount);

            if (models.Count == 0)
            {
                return Json(new { ResultType = EnumResultType.NoData, Message = "データがありません。", });
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

            String fileName = string.Format("\\CSV\\電話内容_{0}.csv", date);
            //李梁    2014.09.12
            if ((Session["user"] as WebEvaluation.DataModels.UserSession).RoleCD == "02" || (Session["user"] as WebEvaluation.DataModels.UserSession).RoleCD == "04" || (Session["user"] as WebEvaluation.DataModels.UserSession).RoleCD == "09")
            {
                if (Sort == "partyDate")
                {
                    //明細データ
                    List<TelRptReportDetailModel> details = new List<TelRptReportDetailModel>();

                    int index = 1;
                    foreach (TelRptViewModel viewModel in models)
                    {
                        TelRptReportDetailModel detail = new TelRptReportDetailModel();

                        detail.No = index;
                        if (viewModel.HoldDate != null)
                        {
                            detail.HoldDate = viewModel.HoldDate.ToString("yyyy/MM/dd");
                        }
                        detail.StaffName = viewModel.StaffName;
                        detail.ShopName = viewModel.ShopName;
                        detail.StaffCD = viewModel.StaffCD;
                        detail.HallType = viewModel.HallType;
                        detail.StartTime = viewModel.StartTime;
                        detail.BrideName = viewModel.BrideName;
                        detail.GroomName = viewModel.GroomName;
                        detail.TelMemo = "";
                        detail.StaffEva = viewModel.StaffEva;
                        detail.LeaderEva = viewModel.LeaderEva;

                        if (null != Tel && Tel.Contains("memo_special") && !String.IsNullOrEmpty(viewModel.TelMemo_special))
                        {
                            detail.TelMemo += "【特記事項】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo_special + System.Environment.NewLine;

                        }
                        if (!String.IsNullOrEmpty(viewModel.TelMemo))
                        {
                            detail.TelMemo += "【電話内容】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo + System.Environment.NewLine;

                        }

                        //HtmlDecode and striphtml
                        detail.TelMemo = this.striphtml(WebUtility.HtmlDecode(detail.TelMemo));

                        details.Add(detail);

                        index++;
                    }

                    CsvUtils.ModlesToCsv<TelRptReportDetailModel>(basePath + fileName, details);
                }
                else if (Sort == "staff")
                {
                    //明細データ
                    List<TelRptReportDetailModel2> details = new List<TelRptReportDetailModel2>();

                    int index = 1;
                    foreach (TelRptViewModel viewModel in models)
                    {
                        TelRptReportDetailModel2 detail = new TelRptReportDetailModel2();

                        detail.No = index;
                        if (viewModel.HoldDate != null)
                        {
                            detail.HoldDate = viewModel.HoldDate.ToString("yyyy/MM/dd");
                        }
                        detail.BrideName = viewModel.BrideName;
                        detail.GroomName = viewModel.GroomName;
                        detail.StaffName = viewModel.StaffName;
                        detail.ShopName = viewModel.ShopName;
                        detail.StaffCD = viewModel.StaffCD;
                        detail.HallType = viewModel.HallType;
                        detail.StartTime = viewModel.StartTime;
                        detail.TelMemo = "";
                        detail.StaffEva = viewModel.StaffEva;
                        detail.LeaderEva = viewModel.LeaderEva;

                        if (null != Tel && Tel.Contains("memo_special") && !String.IsNullOrEmpty(viewModel.TelMemo_special))
                        {
                            detail.TelMemo += "【特記事項】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo_special + System.Environment.NewLine;

                        }
                        if (!String.IsNullOrEmpty(viewModel.TelMemo))
                        {
                            detail.TelMemo += "【電話内容】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo + System.Environment.NewLine;

                        }


                        details.Add(detail);

                        index++;
                    }

                    CsvUtils.ModlesToCsv<TelRptReportDetailModel2>(basePath + fileName, details);
                }
                else if (Sort == "shop")
                {
                    //明細データ
                    List<TelRptReportDetailModel3> details = new List<TelRptReportDetailModel3>();

                    int index = 1;
                    foreach (TelRptViewModel viewModel in models)
                    {
                        TelRptReportDetailModel3 detail = new TelRptReportDetailModel3();

                        detail.No = index;
                        if (viewModel.HoldDate != null)
                        {
                            detail.HoldDate = viewModel.HoldDate.ToString("yyyy/MM/dd");
                        }

                        detail.BrideName = viewModel.BrideName;
                        detail.GroomName = viewModel.GroomName;
                        detail.StaffName = viewModel.StaffName;
                        detail.ShopName = viewModel.ShopName;
                        detail.StaffCD = viewModel.StaffCD;
                        detail.HallType = viewModel.HallType;
                        detail.StartTime = viewModel.StartTime;
                        detail.TelMemo = "";
                        detail.StaffEva = viewModel.StaffEva;
                        detail.LeaderEva = viewModel.LeaderEva;

                        if (null != Tel && Tel.Contains("memo_special") && !String.IsNullOrEmpty(viewModel.TelMemo_special))
                        {
                            detail.TelMemo += "【特記事項】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo_special + System.Environment.NewLine;

                        }
                        if (!String.IsNullOrEmpty(viewModel.TelMemo))
                        {
                            detail.TelMemo += "【電話内容】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo + System.Environment.NewLine;

                        }


                        details.Add(detail);

                        index++;
                    }

                    CsvUtils.ModlesToCsv<TelRptReportDetailModel3>(basePath + fileName, details);
                }

                
            }
            else 
            {
                if (Sort == "partyDate")
                {
                    //明細データ
                    List<TelRptReportDetailModel_Normal> details = new List<TelRptReportDetailModel_Normal>();

                    int index = 1;
                    foreach (TelRptViewModel viewModel in models)
                    {
                        TelRptReportDetailModel_Normal detail = new TelRptReportDetailModel_Normal();

                        detail.No = index;
                        if (viewModel.HoldDate != null)
                        {
                            detail.HoldDate = viewModel.HoldDate.ToString("yyyy/MM/dd");
                        }
                        detail.StaffName = viewModel.StaffName;
                        detail.ShopName = viewModel.ShopName;
                        detail.StaffCD = viewModel.StaffCD;
                        detail.HallType = viewModel.HallType;
                        detail.StartTime = viewModel.StartTime;
                        detail.TelMemo = "";

                        if (null != Tel && Tel.Contains("memo_special") && !String.IsNullOrEmpty(viewModel.TelMemo_special))
                        {
                            detail.TelMemo += "【特記事項】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo_special + System.Environment.NewLine;

                        }
                        if (!String.IsNullOrEmpty(viewModel.TelMemo))
                        {
                            detail.TelMemo += "【電話内容】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo + System.Environment.NewLine;

                        }


                        details.Add(detail);

                        index++;
                    }

                    CsvUtils.ModlesToCsv<TelRptReportDetailModel_Normal>(basePath + fileName, details);
                }
                else if (Sort == "staff")
                {
                    //明細データ
                    List<TelRptReportDetailModel_Normal2> details = new List<TelRptReportDetailModel_Normal2>();

                    int index = 1;
                    foreach (TelRptViewModel viewModel in models)
                    {
                        TelRptReportDetailModel_Normal2 detail = new TelRptReportDetailModel_Normal2();

                        detail.No = index;
                        if (viewModel.HoldDate != null)
                        {
                            detail.HoldDate = viewModel.HoldDate.ToString("yyyy/MM/dd");
                        }
                        detail.StaffName = viewModel.StaffName;
                        detail.ShopName = viewModel.ShopName;
                        detail.TelMemo = "";
                        detail.StaffCD = viewModel.StaffCD;
                        detail.HallType = viewModel.HallType;
                        detail.StartTime = viewModel.StartTime;

                        if (null != Tel && Tel.Contains("memo_special") && !String.IsNullOrEmpty(viewModel.TelMemo_special))
                        {
                            detail.TelMemo += "【特記事項】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo_special + System.Environment.NewLine;

                        }
                        if (!String.IsNullOrEmpty(viewModel.TelMemo))
                        {
                            detail.TelMemo += "【電話内容】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo + System.Environment.NewLine;

                        }


                        details.Add(detail);

                        index++;
                    }

                    CsvUtils.ModlesToCsv<TelRptReportDetailModel_Normal2>(basePath + fileName, details);
                }
                else if (Sort == "shop")
                {
                    //明細データ
                    List<TelRptReportDetailModel_Normal3> details = new List<TelRptReportDetailModel_Normal3>();

                    int index = 1;
                    foreach (TelRptViewModel viewModel in models)
                    {
                        TelRptReportDetailModel_Normal3 detail = new TelRptReportDetailModel_Normal3();

                        detail.No = index;
                        if (viewModel.HoldDate != null)
                        {
                            detail.HoldDate = viewModel.HoldDate.ToString("yyyy/MM/dd");
                        }
                        detail.StaffName = viewModel.StaffName;
                        detail.ShopName = viewModel.ShopName;
                        detail.TelMemo = "";
                        detail.StaffCD = viewModel.StaffCD;
                        detail.HallType = viewModel.HallType;
                        detail.StartTime = viewModel.StartTime;

                        if (null != Tel && Tel.Contains("memo_special") && !String.IsNullOrEmpty(viewModel.TelMemo_special))
                        {
                            detail.TelMemo += "【特記事項】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo_special + System.Environment.NewLine;

                        }
                        if (!String.IsNullOrEmpty(viewModel.TelMemo))
                        {
                            detail.TelMemo += "【電話内容】" + System.Environment.NewLine;
                            detail.TelMemo += viewModel.TelMemo + System.Environment.NewLine;

                        }


                        details.Add(detail);

                        index++;
                    }

                    CsvUtils.ModlesToCsv<TelRptReportDetailModel_Normal3>(basePath + fileName, details);
                }

                
            }
            GC.Collect();
            return Json(new { Path = string.Format("/CSV/電話内容_{0}.csv", date), ResultType = EnumResultType.Success });
        }

        private string striphtml(string strhtml)
        {
            string stroutput = strhtml;
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
            stroutput = regex.Replace(stroutput, "");
            return stroutput;
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

    public class ReportListExcelHandler : IReportHandler
    {
        public int index { get; set; }

        public void AfterCreateReportSheet(HSSFWorkbook wb, ISheet sheet) {

            for (int rowNum = 0; rowNum <= sheet.LastRowNum; rowNum++)
            {
                IRow hssfRow = sheet.GetRow(rowNum);
                if (hssfRow == null)
                {
                    continue;
                }

                for (int i = this.index; i < 256; i++)
                {

                    
                    if (i == 255)
                    {
                        sheet.SetColumnWidth(i, 1);
                    }
                    else
                    {
                        ICell c = hssfRow.GetCell(i);
                        c.RemoveCellComment();
                        sheet.SetColumnWidth(i, 0);
                        hssfRow.RemoveCell(c);
                    }
                }


            }
        }
        public void AfterCreateReportBook(HSSFWorkbook wb) { 
        
        }

        public string GetSheetName(IReportData pageData, int index) {

            return ((PartyReportModel)pageData).PartyDate_01.Substring(0,7).Replace("/","年")+"月";
        }
    }

    public class ReportExcelHandler : IReportHandler
    {
        public int index { get; set; }

        public void AfterCreateReportSheet(HSSFWorkbook wb, ISheet sheet)
        {

        }
        public void AfterCreateReportBook(HSSFWorkbook wb)
        {

        }

        public string GetSheetName(IReportData pageData, int index)
        {

            return ""+index;
        }
    }
}
