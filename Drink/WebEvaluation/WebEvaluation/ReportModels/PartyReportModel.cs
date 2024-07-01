using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using WebEvaluation.Common;

namespace WebEvaluation.ReportModels
{

    public class PartyReportModel : ReportData
    {
        [Display(Name = "ShopCD")]
        public string ShopCD { get; set; }
        [Display(Name = "PartyMonth")]
        public string PartyMonth { get; set; }
        [Display(Name = "PartyDate_01")]
        public string PartyDate_01 { get; set; }
        [Display(Name = "BrideFamilyName_01")]
        public string BrideFamilyName_01 { get; set; }
        [Display(Name = "GroomFamilyName_01")]
        public string GroomFamilyName_01 { get; set; }
        [Display(Name = "TantoName_01")]
        public string TantoName_01 { get; set; }
        [Display(Name = "ReporterName_01")]
        public string ReporterName_01 { get; set; }
        [Display(Name = "AdultCnt_01")]
        public string AdultCnt_01 { get; set; }
        [Display(Name = "HalfCnt_01")]
        public string HalfCnt_01 { get; set; }
        [Display(Name = "ChildrenCnt_01")]
        public string ChildrenCnt_01 { get; set; }
        [Display(Name = "SeatOnlyCnt_01")]
        public string SeatOnlyCnt_01 { get; set; }
        [Display(Name = "TableCnt_01")]
        public string TableCnt_01 { get; set; }
        [Display(Name = "TableCross_01")]
        public string TableCross_01 { get; set; }
        [Display(Name = "PartyStyleName_01")]
        public string PartyStyleName_01 { get; set; }
        [Display(Name = "FoodStyleName_01")]
        public string FoodStyleName_01 { get; set; }
        [Display(Name = "FoodPricce_01")]
        public string FoodPricce_01 { get; set; }
        [Display(Name = "DrinkPrice_01")]
        public string DrinkPrice_01 { get; set; }
        [Display(Name = "Wdrink_01")]
        public string Wdrink_01 { get; set; }
        [Display(Name = "Desl_01")]
        public string Desl_01 { get; set; }
        [Display(Name = "RestRoomFlg_01")]
        public string RestRoomFlg_01 { get; set; }
        [Display(Name = "AnketFlg_01")]
        public string AnketFlg_01 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_01")]
        public string PartyTime_TimeName_I_01 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_01")]
        public string PartyTime_OrderTime_I_01 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_01")]
        public string PartyTime_ActTime_I_01 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_01")]
        public string PartyTime_DelayTime_I_01 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_01")]
        public string PartyTime_TimeName_II_01 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_01")]
        public string PartyTime_OrderTime_II_01 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_01")]
        public string PartyTime_ActTime_II_01 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_01")]
        public string PartyTime_DelayTime_II_01 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_01")]
        public string PartyTime_TimeName_III_01 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_01")]
        public string PartyTime_OrderTime_III_01 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_01")]
        public string PartyTime_ActTime_III_01 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_01")]
        public string PartyTime_DelayTime_III_01 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_01")]
        public string PartyTime_TimeName_IV_01 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_01")]
        public string PartyTime_OrderTime_IV_01 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_01")]
        public string PartyTime_ActTime_IV_01 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_01")]
        public string PartyTime_DelayTime_IV_01 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_01")]
        public string PartyTime_TimeName_V_01 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_01")]
        public string PartyTime_OrderTime_V_01 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_01")]
        public string PartyTime_ActTime_V_01 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_01")]
        public string PartyTime_DelayTime_V_01 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_01")]
        public string PartyFood_FoodName_I_01 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_01")]
        public string PartyFood_BeginTime_I_01 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_01")]
        public string PartyFood_EndTime_I_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_01")]
        public string PartyFood_RestRoomTime_I_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_01")]
        public string PartyFood_RestRoomFlg_I_01 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_01")]
        public string PartyFood_FoodName_II_01 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_01")]
        public string PartyFood_BeginTime_II_01 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_01")]
        public string PartyFood_EndTime_II_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_01")]
        public string PartyFood_RestRoomTime_II_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_01")]
        public string PartyFood_RestRoomFlg_II_01 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_01")]
        public string PartyFood_FoodName_III_01 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_01")]
        public string PartyFood_BeginTime_III_01 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_01")]
        public string PartyFood_EndTime_III_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_01")]
        public string PartyFood_RestRoomTime_III_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_01")]
        public string PartyFood_RestRoomFlg_III_01 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_01")]
        public string PartyFood_FoodName_IV_01 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_01")]
        public string PartyFood_BeginTime_IV_01 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_01")]
        public string PartyFood_EndTime_IV_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_01")]
        public string PartyFood_RestRoomTime_IV_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_01")]
        public string PartyFood_RestRoomFlg_IV_01 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_01")]
        public string PartyFood_FoodName_V_01 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_01")]
        public string PartyFood_BeginTime_V_01 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_01")]
        public string PartyFood_EndTime_V_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_01")]
        public string PartyFood_RestRoomTime_V_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_01")]
        public string PartyFood_RestRoomFlg_V_01 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_01")]
        public string PartyFood_FoodName_VI_01 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_01")]
        public string PartyFood_BeginTime_VI_01 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_01")]
        public string PartyFood_EndTime_VI_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_01")]
        public string PartyFood_RestRoomTime_VI_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_01")]
        public string PartyFood_RestRoomFlg_VI_01 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_01")]
        public string PartyFood_FoodName_VII_01 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_01")]
        public string PartyFood_BeginTime_VII_01 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_01")]
        public string PartyFood_EndTime_VII_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_01")]
        public string PartyFood_RestRoomTime_VII_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_01")]
        public string PartyFood_RestRoomFlg_VII_01 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_01")]
        public string PartyFood_FoodName_VIII_01 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_01")]
        public string PartyFood_BeginTime_VIII_01 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_01")]
        public string PartyFood_EndTime_VIII_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_01")]
        public string PartyFood_RestRoomTime_VIII_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_01")]
        public string PartyFood_RestRoomFlg_VIII_01 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_01")]
        public string PartyFood_FoodName_IX_01 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_01")]
        public string PartyFood_BeginTime_IX_01 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_01")]
        public string PartyFood_EndTime_IX_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_01")]
        public string PartyFood_RestRoomTime_IX_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_01")]
        public string PartyFood_RestRoomFlg_IX_01 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_01")]
        public string PartyFood_FoodName_X_01 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_01")]
        public string PartyFood_BeginTime_X_01 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_01")]
        public string PartyFood_EndTime_X_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_01")]
        public string PartyFood_RestRoomTime_X_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_01")]
        public string PartyFood_RestRoomFlg_X_01 { get; set; }
        [Display(Name = "PartyMember_Name_I_01")]
        public string PartyMember_Name_I_01 { get; set; }
        [Display(Name = "PartyMember_Name_II_01")]
        public string PartyMember_Name_II_01 { get; set; }
        [Display(Name = "PartyMember_Name_III_01")]
        public string PartyMember_Name_III_01 { get; set; }
        [Display(Name = "PartyMember_Name_IV_01")]
        public string PartyMember_Name_IV_01 { get; set; }
        [Display(Name = "PartyMember_Name_V_01")]
        public string PartyMember_Name_V_01 { get; set; }
        [Display(Name = "PartyMember_Name_VI_01")]
        public string PartyMember_Name_VI_01 { get; set; }
        [Display(Name = "PartyMember_Name_VII_01")]
        public string PartyMember_Name_VII_01 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_01")]
        public string PartyMember_Name_VIII_01 { get; set; }
        [Display(Name = "PartyMember_Name_IX_01")]
        public string PartyMember_Name_IX_01 { get; set; }
        [Display(Name = "PartyDate_02")]
        public string PartyDate_02 { get; set; }
        [Display(Name = "BrideFamilyName_02")]
        public string BrideFamilyName_02 { get; set; }
        [Display(Name = "GroomFamilyName_02")]
        public string GroomFamilyName_02 { get; set; }
        [Display(Name = "TantoName_02")]
        public string TantoName_02 { get; set; }
        [Display(Name = "ReporterName_02")]
        public string ReporterName_02 { get; set; }
        [Display(Name = "AdultCnt_02")]
        public string AdultCnt_02 { get; set; }
        [Display(Name = "HalfCnt_02")]
        public string HalfCnt_02 { get; set; }
        [Display(Name = "ChildrenCnt_02")]
        public string ChildrenCnt_02 { get; set; }
        [Display(Name = "SeatOnlyCnt_02")]
        public string SeatOnlyCnt_02 { get; set; }
        [Display(Name = "TableCnt_02")]
        public string TableCnt_02 { get; set; }
        [Display(Name = "TableCross_02")]
        public string TableCross_02 { get; set; }
        [Display(Name = "PartyStyleName_02")]
        public string PartyStyleName_02 { get; set; }
        [Display(Name = "FoodStyleName_02")]
        public string FoodStyleName_02 { get; set; }
        [Display(Name = "FoodPricce_02")]
        public string FoodPricce_02 { get; set; }
        [Display(Name = "DrinkPrice_02")]
        public string DrinkPrice_02 { get; set; }
        [Display(Name = "Wdrink_02")]
        public string Wdrink_02 { get; set; }
        [Display(Name = "Desl_02")]
        public string Desl_02 { get; set; }
        [Display(Name = "RestRoomFlg_02")]
        public string RestRoomFlg_02 { get; set; }
        [Display(Name = "AnketFlg_02")]
        public string AnketFlg_02 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_02")]
        public string PartyTime_TimeName_I_02 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_02")]
        public string PartyTime_OrderTime_I_02 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_02")]
        public string PartyTime_ActTime_I_02 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_02")]
        public string PartyTime_DelayTime_I_02 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_02")]
        public string PartyTime_TimeName_II_02 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_02")]
        public string PartyTime_OrderTime_II_02 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_02")]
        public string PartyTime_ActTime_II_02 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_02")]
        public string PartyTime_DelayTime_II_02 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_02")]
        public string PartyTime_TimeName_III_02 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_02")]
        public string PartyTime_OrderTime_III_02 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_02")]
        public string PartyTime_ActTime_III_02 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_02")]
        public string PartyTime_DelayTime_III_02 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_02")]
        public string PartyTime_TimeName_IV_02 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_02")]
        public string PartyTime_OrderTime_IV_02 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_02")]
        public string PartyTime_ActTime_IV_02 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_02")]
        public string PartyTime_DelayTime_IV_02 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_02")]
        public string PartyTime_TimeName_V_02 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_02")]
        public string PartyTime_OrderTime_V_02 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_02")]
        public string PartyTime_ActTime_V_02 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_02")]
        public string PartyTime_DelayTime_V_02 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_02")]
        public string PartyFood_FoodName_I_02 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_02")]
        public string PartyFood_BeginTime_I_02 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_02")]
        public string PartyFood_EndTime_I_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_02")]
        public string PartyFood_RestRoomTime_I_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_02")]
        public string PartyFood_RestRoomFlg_I_02 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_02")]
        public string PartyFood_FoodName_II_02 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_02")]
        public string PartyFood_BeginTime_II_02 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_02")]
        public string PartyFood_EndTime_II_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_02")]
        public string PartyFood_RestRoomTime_II_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_02")]
        public string PartyFood_RestRoomFlg_II_02 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_02")]
        public string PartyFood_FoodName_III_02 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_02")]
        public string PartyFood_BeginTime_III_02 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_02")]
        public string PartyFood_EndTime_III_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_02")]
        public string PartyFood_RestRoomTime_III_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_02")]
        public string PartyFood_RestRoomFlg_III_02 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_02")]
        public string PartyFood_FoodName_IV_02 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_02")]
        public string PartyFood_BeginTime_IV_02 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_02")]
        public string PartyFood_EndTime_IV_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_02")]
        public string PartyFood_RestRoomTime_IV_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_02")]
        public string PartyFood_RestRoomFlg_IV_02 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_02")]
        public string PartyFood_FoodName_V_02 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_02")]
        public string PartyFood_BeginTime_V_02 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_02")]
        public string PartyFood_EndTime_V_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_02")]
        public string PartyFood_RestRoomTime_V_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_02")]
        public string PartyFood_RestRoomFlg_V_02 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_02")]
        public string PartyFood_FoodName_VI_02 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_02")]
        public string PartyFood_BeginTime_VI_02 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_02")]
        public string PartyFood_EndTime_VI_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_02")]
        public string PartyFood_RestRoomTime_VI_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_02")]
        public string PartyFood_RestRoomFlg_VI_02 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_02")]
        public string PartyFood_FoodName_VII_02 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_02")]
        public string PartyFood_BeginTime_VII_02 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_02")]
        public string PartyFood_EndTime_VII_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_02")]
        public string PartyFood_RestRoomTime_VII_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_02")]
        public string PartyFood_RestRoomFlg_VII_02 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_02")]
        public string PartyFood_FoodName_VIII_02 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_02")]
        public string PartyFood_BeginTime_VIII_02 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_02")]
        public string PartyFood_EndTime_VIII_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_02")]
        public string PartyFood_RestRoomTime_VIII_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_02")]
        public string PartyFood_RestRoomFlg_VIII_02 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_02")]
        public string PartyFood_FoodName_IX_02 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_02")]
        public string PartyFood_BeginTime_IX_02 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_02")]
        public string PartyFood_EndTime_IX_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_02")]
        public string PartyFood_RestRoomTime_IX_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_02")]
        public string PartyFood_RestRoomFlg_IX_02 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_02")]
        public string PartyFood_FoodName_X_02 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_02")]
        public string PartyFood_BeginTime_X_02 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_02")]
        public string PartyFood_EndTime_X_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_02")]
        public string PartyFood_RestRoomTime_X_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_02")]
        public string PartyFood_RestRoomFlg_X_02 { get; set; }
        [Display(Name = "PartyMember_Name_I_02")]
        public string PartyMember_Name_I_02 { get; set; }
        [Display(Name = "PartyMember_Name_II_02")]
        public string PartyMember_Name_II_02 { get; set; }
        [Display(Name = "PartyMember_Name_III_02")]
        public string PartyMember_Name_III_02 { get; set; }
        [Display(Name = "PartyMember_Name_IV_02")]
        public string PartyMember_Name_IV_02 { get; set; }
        [Display(Name = "PartyMember_Name_V_02")]
        public string PartyMember_Name_V_02 { get; set; }
        [Display(Name = "PartyMember_Name_VI_02")]
        public string PartyMember_Name_VI_02 { get; set; }
        [Display(Name = "PartyMember_Name_VII_02")]
        public string PartyMember_Name_VII_02 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_02")]
        public string PartyMember_Name_VIII_02 { get; set; }
        [Display(Name = "PartyMember_Name_IX_02")]
        public string PartyMember_Name_IX_02 { get; set; }
        [Display(Name = "PartyDate_03")]
        public string PartyDate_03 { get; set; }
        [Display(Name = "BrideFamilyName_03")]
        public string BrideFamilyName_03 { get; set; }
        [Display(Name = "GroomFamilyName_03")]
        public string GroomFamilyName_03 { get; set; }
        [Display(Name = "TantoName_03")]
        public string TantoName_03 { get; set; }
        [Display(Name = "ReporterName_03")]
        public string ReporterName_03 { get; set; }
        [Display(Name = "AdultCnt_03")]
        public string AdultCnt_03 { get; set; }
        [Display(Name = "HalfCnt_03")]
        public string HalfCnt_03 { get; set; }
        [Display(Name = "ChildrenCnt_03")]
        public string ChildrenCnt_03 { get; set; }
        [Display(Name = "SeatOnlyCnt_03")]
        public string SeatOnlyCnt_03 { get; set; }
        [Display(Name = "TableCnt_03")]
        public string TableCnt_03 { get; set; }
        [Display(Name = "TableCross_03")]
        public string TableCross_03 { get; set; }
        [Display(Name = "PartyStyleName_03")]
        public string PartyStyleName_03 { get; set; }
        [Display(Name = "FoodStyleName_03")]
        public string FoodStyleName_03 { get; set; }
        [Display(Name = "FoodPricce_03")]
        public string FoodPricce_03 { get; set; }
        [Display(Name = "DrinkPrice_03")]
        public string DrinkPrice_03 { get; set; }
        [Display(Name = "Wdrink_03")]
        public string Wdrink_03 { get; set; }
        [Display(Name = "Desl_03")]
        public string Desl_03 { get; set; }
        [Display(Name = "RestRoomFlg_03")]
        public string RestRoomFlg_03 { get; set; }
        [Display(Name = "AnketFlg_03")]
        public string AnketFlg_03 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_03")]
        public string PartyTime_TimeName_I_03 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_03")]
        public string PartyTime_OrderTime_I_03 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_03")]
        public string PartyTime_ActTime_I_03 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_03")]
        public string PartyTime_DelayTime_I_03 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_03")]
        public string PartyTime_TimeName_II_03 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_03")]
        public string PartyTime_OrderTime_II_03 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_03")]
        public string PartyTime_ActTime_II_03 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_03")]
        public string PartyTime_DelayTime_II_03 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_03")]
        public string PartyTime_TimeName_III_03 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_03")]
        public string PartyTime_OrderTime_III_03 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_03")]
        public string PartyTime_ActTime_III_03 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_03")]
        public string PartyTime_DelayTime_III_03 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_03")]
        public string PartyTime_TimeName_IV_03 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_03")]
        public string PartyTime_OrderTime_IV_03 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_03")]
        public string PartyTime_ActTime_IV_03 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_03")]
        public string PartyTime_DelayTime_IV_03 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_03")]
        public string PartyTime_TimeName_V_03 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_03")]
        public string PartyTime_OrderTime_V_03 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_03")]
        public string PartyTime_ActTime_V_03 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_03")]
        public string PartyTime_DelayTime_V_03 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_03")]
        public string PartyFood_FoodName_I_03 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_03")]
        public string PartyFood_BeginTime_I_03 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_03")]
        public string PartyFood_EndTime_I_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_03")]
        public string PartyFood_RestRoomTime_I_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_03")]
        public string PartyFood_RestRoomFlg_I_03 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_03")]
        public string PartyFood_FoodName_II_03 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_03")]
        public string PartyFood_BeginTime_II_03 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_03")]
        public string PartyFood_EndTime_II_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_03")]
        public string PartyFood_RestRoomTime_II_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_03")]
        public string PartyFood_RestRoomFlg_II_03 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_03")]
        public string PartyFood_FoodName_III_03 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_03")]
        public string PartyFood_BeginTime_III_03 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_03")]
        public string PartyFood_EndTime_III_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_03")]
        public string PartyFood_RestRoomTime_III_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_03")]
        public string PartyFood_RestRoomFlg_III_03 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_03")]
        public string PartyFood_FoodName_IV_03 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_03")]
        public string PartyFood_BeginTime_IV_03 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_03")]
        public string PartyFood_EndTime_IV_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_03")]
        public string PartyFood_RestRoomTime_IV_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_03")]
        public string PartyFood_RestRoomFlg_IV_03 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_03")]
        public string PartyFood_FoodName_V_03 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_03")]
        public string PartyFood_BeginTime_V_03 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_03")]
        public string PartyFood_EndTime_V_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_03")]
        public string PartyFood_RestRoomTime_V_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_03")]
        public string PartyFood_RestRoomFlg_V_03 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_03")]
        public string PartyFood_FoodName_VI_03 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_03")]
        public string PartyFood_BeginTime_VI_03 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_03")]
        public string PartyFood_EndTime_VI_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_03")]
        public string PartyFood_RestRoomTime_VI_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_03")]
        public string PartyFood_RestRoomFlg_VI_03 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_03")]
        public string PartyFood_FoodName_VII_03 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_03")]
        public string PartyFood_BeginTime_VII_03 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_03")]
        public string PartyFood_EndTime_VII_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_03")]
        public string PartyFood_RestRoomTime_VII_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_03")]
        public string PartyFood_RestRoomFlg_VII_03 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_03")]
        public string PartyFood_FoodName_VIII_03 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_03")]
        public string PartyFood_BeginTime_VIII_03 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_03")]
        public string PartyFood_EndTime_VIII_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_03")]
        public string PartyFood_RestRoomTime_VIII_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_03")]
        public string PartyFood_RestRoomFlg_VIII_03 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_03")]
        public string PartyFood_FoodName_IX_03 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_03")]
        public string PartyFood_BeginTime_IX_03 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_03")]
        public string PartyFood_EndTime_IX_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_03")]
        public string PartyFood_RestRoomTime_IX_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_03")]
        public string PartyFood_RestRoomFlg_IX_03 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_03")]
        public string PartyFood_FoodName_X_03 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_03")]
        public string PartyFood_BeginTime_X_03 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_03")]
        public string PartyFood_EndTime_X_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_03")]
        public string PartyFood_RestRoomTime_X_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_03")]
        public string PartyFood_RestRoomFlg_X_03 { get; set; }
        [Display(Name = "PartyMember_Name_I_03")]
        public string PartyMember_Name_I_03 { get; set; }
        [Display(Name = "PartyMember_Name_II_03")]
        public string PartyMember_Name_II_03 { get; set; }
        [Display(Name = "PartyMember_Name_III_03")]
        public string PartyMember_Name_III_03 { get; set; }
        [Display(Name = "PartyMember_Name_IV_03")]
        public string PartyMember_Name_IV_03 { get; set; }
        [Display(Name = "PartyMember_Name_V_03")]
        public string PartyMember_Name_V_03 { get; set; }
        [Display(Name = "PartyMember_Name_VI_03")]
        public string PartyMember_Name_VI_03 { get; set; }
        [Display(Name = "PartyMember_Name_VII_03")]
        public string PartyMember_Name_VII_03 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_03")]
        public string PartyMember_Name_VIII_03 { get; set; }
        [Display(Name = "PartyMember_Name_IX_03")]
        public string PartyMember_Name_IX_03 { get; set; }
        [Display(Name = "PartyDate_04")]
        public string PartyDate_04 { get; set; }
        [Display(Name = "BrideFamilyName_04")]
        public string BrideFamilyName_04 { get; set; }
        [Display(Name = "GroomFamilyName_04")]
        public string GroomFamilyName_04 { get; set; }
        [Display(Name = "TantoName_04")]
        public string TantoName_04 { get; set; }
        [Display(Name = "ReporterName_04")]
        public string ReporterName_04 { get; set; }
        [Display(Name = "AdultCnt_04")]
        public string AdultCnt_04 { get; set; }
        [Display(Name = "HalfCnt_04")]
        public string HalfCnt_04 { get; set; }
        [Display(Name = "ChildrenCnt_04")]
        public string ChildrenCnt_04 { get; set; }
        [Display(Name = "SeatOnlyCnt_04")]
        public string SeatOnlyCnt_04 { get; set; }
        [Display(Name = "TableCnt_04")]
        public string TableCnt_04 { get; set; }
        [Display(Name = "TableCross_04")]
        public string TableCross_04 { get; set; }
        [Display(Name = "PartyStyleName_04")]
        public string PartyStyleName_04 { get; set; }
        [Display(Name = "FoodStyleName_04")]
        public string FoodStyleName_04 { get; set; }
        [Display(Name = "FoodPricce_04")]
        public string FoodPricce_04 { get; set; }
        [Display(Name = "DrinkPrice_04")]
        public string DrinkPrice_04 { get; set; }
        [Display(Name = "Wdrink_04")]
        public string Wdrink_04 { get; set; }
        [Display(Name = "Desl_04")]
        public string Desl_04 { get; set; }
        [Display(Name = "RestRoomFlg_04")]
        public string RestRoomFlg_04 { get; set; }
        [Display(Name = "AnketFlg_04")]
        public string AnketFlg_04 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_04")]
        public string PartyTime_TimeName_I_04 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_04")]
        public string PartyTime_OrderTime_I_04 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_04")]
        public string PartyTime_ActTime_I_04 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_04")]
        public string PartyTime_DelayTime_I_04 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_04")]
        public string PartyTime_TimeName_II_04 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_04")]
        public string PartyTime_OrderTime_II_04 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_04")]
        public string PartyTime_ActTime_II_04 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_04")]
        public string PartyTime_DelayTime_II_04 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_04")]
        public string PartyTime_TimeName_III_04 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_04")]
        public string PartyTime_OrderTime_III_04 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_04")]
        public string PartyTime_ActTime_III_04 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_04")]
        public string PartyTime_DelayTime_III_04 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_04")]
        public string PartyTime_TimeName_IV_04 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_04")]
        public string PartyTime_OrderTime_IV_04 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_04")]
        public string PartyTime_ActTime_IV_04 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_04")]
        public string PartyTime_DelayTime_IV_04 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_04")]
        public string PartyTime_TimeName_V_04 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_04")]
        public string PartyTime_OrderTime_V_04 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_04")]
        public string PartyTime_ActTime_V_04 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_04")]
        public string PartyTime_DelayTime_V_04 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_04")]
        public string PartyFood_FoodName_I_04 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_04")]
        public string PartyFood_BeginTime_I_04 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_04")]
        public string PartyFood_EndTime_I_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_04")]
        public string PartyFood_RestRoomTime_I_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_04")]
        public string PartyFood_RestRoomFlg_I_04 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_04")]
        public string PartyFood_FoodName_II_04 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_04")]
        public string PartyFood_BeginTime_II_04 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_04")]
        public string PartyFood_EndTime_II_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_04")]
        public string PartyFood_RestRoomTime_II_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_04")]
        public string PartyFood_RestRoomFlg_II_04 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_04")]
        public string PartyFood_FoodName_III_04 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_04")]
        public string PartyFood_BeginTime_III_04 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_04")]
        public string PartyFood_EndTime_III_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_04")]
        public string PartyFood_RestRoomTime_III_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_04")]
        public string PartyFood_RestRoomFlg_III_04 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_04")]
        public string PartyFood_FoodName_IV_04 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_04")]
        public string PartyFood_BeginTime_IV_04 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_04")]
        public string PartyFood_EndTime_IV_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_04")]
        public string PartyFood_RestRoomTime_IV_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_04")]
        public string PartyFood_RestRoomFlg_IV_04 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_04")]
        public string PartyFood_FoodName_V_04 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_04")]
        public string PartyFood_BeginTime_V_04 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_04")]
        public string PartyFood_EndTime_V_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_04")]
        public string PartyFood_RestRoomTime_V_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_04")]
        public string PartyFood_RestRoomFlg_V_04 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_04")]
        public string PartyFood_FoodName_VI_04 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_04")]
        public string PartyFood_BeginTime_VI_04 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_04")]
        public string PartyFood_EndTime_VI_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_04")]
        public string PartyFood_RestRoomTime_VI_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_04")]
        public string PartyFood_RestRoomFlg_VI_04 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_04")]
        public string PartyFood_FoodName_VII_04 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_04")]
        public string PartyFood_BeginTime_VII_04 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_04")]
        public string PartyFood_EndTime_VII_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_04")]
        public string PartyFood_RestRoomTime_VII_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_04")]
        public string PartyFood_RestRoomFlg_VII_04 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_04")]
        public string PartyFood_FoodName_VIII_04 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_04")]
        public string PartyFood_BeginTime_VIII_04 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_04")]
        public string PartyFood_EndTime_VIII_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_04")]
        public string PartyFood_RestRoomTime_VIII_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_04")]
        public string PartyFood_RestRoomFlg_VIII_04 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_04")]
        public string PartyFood_FoodName_IX_04 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_04")]
        public string PartyFood_BeginTime_IX_04 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_04")]
        public string PartyFood_EndTime_IX_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_04")]
        public string PartyFood_RestRoomTime_IX_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_04")]
        public string PartyFood_RestRoomFlg_IX_04 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_04")]
        public string PartyFood_FoodName_X_04 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_04")]
        public string PartyFood_BeginTime_X_04 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_04")]
        public string PartyFood_EndTime_X_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_04")]
        public string PartyFood_RestRoomTime_X_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_04")]
        public string PartyFood_RestRoomFlg_X_04 { get; set; }
        [Display(Name = "PartyMember_Name_I_04")]
        public string PartyMember_Name_I_04 { get; set; }
        [Display(Name = "PartyMember_Name_II_04")]
        public string PartyMember_Name_II_04 { get; set; }
        [Display(Name = "PartyMember_Name_III_04")]
        public string PartyMember_Name_III_04 { get; set; }
        [Display(Name = "PartyMember_Name_IV_04")]
        public string PartyMember_Name_IV_04 { get; set; }
        [Display(Name = "PartyMember_Name_V_04")]
        public string PartyMember_Name_V_04 { get; set; }
        [Display(Name = "PartyMember_Name_VI_04")]
        public string PartyMember_Name_VI_04 { get; set; }
        [Display(Name = "PartyMember_Name_VII_04")]
        public string PartyMember_Name_VII_04 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_04")]
        public string PartyMember_Name_VIII_04 { get; set; }
        [Display(Name = "PartyMember_Name_IX_04")]
        public string PartyMember_Name_IX_04 { get; set; }
        [Display(Name = "PartyDate_05")]
        public string PartyDate_05 { get; set; }
        [Display(Name = "BrideFamilyName_05")]
        public string BrideFamilyName_05 { get; set; }
        [Display(Name = "GroomFamilyName_05")]
        public string GroomFamilyName_05 { get; set; }
        [Display(Name = "TantoName_05")]
        public string TantoName_05 { get; set; }
        [Display(Name = "ReporterName_05")]
        public string ReporterName_05 { get; set; }
        [Display(Name = "AdultCnt_05")]
        public string AdultCnt_05 { get; set; }
        [Display(Name = "HalfCnt_05")]
        public string HalfCnt_05 { get; set; }
        [Display(Name = "ChildrenCnt_05")]
        public string ChildrenCnt_05 { get; set; }
        [Display(Name = "SeatOnlyCnt_05")]
        public string SeatOnlyCnt_05 { get; set; }
        [Display(Name = "TableCnt_05")]
        public string TableCnt_05 { get; set; }
        [Display(Name = "TableCross_05")]
        public string TableCross_05 { get; set; }
        [Display(Name = "PartyStyleName_05")]
        public string PartyStyleName_05 { get; set; }
        [Display(Name = "FoodStyleName_05")]
        public string FoodStyleName_05 { get; set; }
        [Display(Name = "FoodPricce_05")]
        public string FoodPricce_05 { get; set; }
        [Display(Name = "DrinkPrice_05")]
        public string DrinkPrice_05 { get; set; }
        [Display(Name = "Wdrink_05")]
        public string Wdrink_05 { get; set; }
        [Display(Name = "Desl_05")]
        public string Desl_05 { get; set; }
        [Display(Name = "RestRoomFlg_05")]
        public string RestRoomFlg_05 { get; set; }
        [Display(Name = "AnketFlg_05")]
        public string AnketFlg_05 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_05")]
        public string PartyTime_TimeName_I_05 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_05")]
        public string PartyTime_OrderTime_I_05 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_05")]
        public string PartyTime_ActTime_I_05 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_05")]
        public string PartyTime_DelayTime_I_05 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_05")]
        public string PartyTime_TimeName_II_05 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_05")]
        public string PartyTime_OrderTime_II_05 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_05")]
        public string PartyTime_ActTime_II_05 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_05")]
        public string PartyTime_DelayTime_II_05 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_05")]
        public string PartyTime_TimeName_III_05 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_05")]
        public string PartyTime_OrderTime_III_05 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_05")]
        public string PartyTime_ActTime_III_05 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_05")]
        public string PartyTime_DelayTime_III_05 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_05")]
        public string PartyTime_TimeName_IV_05 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_05")]
        public string PartyTime_OrderTime_IV_05 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_05")]
        public string PartyTime_ActTime_IV_05 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_05")]
        public string PartyTime_DelayTime_IV_05 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_05")]
        public string PartyTime_TimeName_V_05 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_05")]
        public string PartyTime_OrderTime_V_05 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_05")]
        public string PartyTime_ActTime_V_05 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_05")]
        public string PartyTime_DelayTime_V_05 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_05")]
        public string PartyFood_FoodName_I_05 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_05")]
        public string PartyFood_BeginTime_I_05 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_05")]
        public string PartyFood_EndTime_I_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_05")]
        public string PartyFood_RestRoomTime_I_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_05")]
        public string PartyFood_RestRoomFlg_I_05 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_05")]
        public string PartyFood_FoodName_II_05 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_05")]
        public string PartyFood_BeginTime_II_05 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_05")]
        public string PartyFood_EndTime_II_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_05")]
        public string PartyFood_RestRoomTime_II_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_05")]
        public string PartyFood_RestRoomFlg_II_05 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_05")]
        public string PartyFood_FoodName_III_05 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_05")]
        public string PartyFood_BeginTime_III_05 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_05")]
        public string PartyFood_EndTime_III_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_05")]
        public string PartyFood_RestRoomTime_III_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_05")]
        public string PartyFood_RestRoomFlg_III_05 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_05")]
        public string PartyFood_FoodName_IV_05 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_05")]
        public string PartyFood_BeginTime_IV_05 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_05")]
        public string PartyFood_EndTime_IV_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_05")]
        public string PartyFood_RestRoomTime_IV_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_05")]
        public string PartyFood_RestRoomFlg_IV_05 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_05")]
        public string PartyFood_FoodName_V_05 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_05")]
        public string PartyFood_BeginTime_V_05 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_05")]
        public string PartyFood_EndTime_V_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_05")]
        public string PartyFood_RestRoomTime_V_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_05")]
        public string PartyFood_RestRoomFlg_V_05 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_05")]
        public string PartyFood_FoodName_VI_05 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_05")]
        public string PartyFood_BeginTime_VI_05 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_05")]
        public string PartyFood_EndTime_VI_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_05")]
        public string PartyFood_RestRoomTime_VI_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_05")]
        public string PartyFood_RestRoomFlg_VI_05 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_05")]
        public string PartyFood_FoodName_VII_05 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_05")]
        public string PartyFood_BeginTime_VII_05 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_05")]
        public string PartyFood_EndTime_VII_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_05")]
        public string PartyFood_RestRoomTime_VII_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_05")]
        public string PartyFood_RestRoomFlg_VII_05 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_05")]
        public string PartyFood_FoodName_VIII_05 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_05")]
        public string PartyFood_BeginTime_VIII_05 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_05")]
        public string PartyFood_EndTime_VIII_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_05")]
        public string PartyFood_RestRoomTime_VIII_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_05")]
        public string PartyFood_RestRoomFlg_VIII_05 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_05")]
        public string PartyFood_FoodName_IX_05 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_05")]
        public string PartyFood_BeginTime_IX_05 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_05")]
        public string PartyFood_EndTime_IX_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_05")]
        public string PartyFood_RestRoomTime_IX_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_05")]
        public string PartyFood_RestRoomFlg_IX_05 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_05")]
        public string PartyFood_FoodName_X_05 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_05")]
        public string PartyFood_BeginTime_X_05 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_05")]
        public string PartyFood_EndTime_X_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_05")]
        public string PartyFood_RestRoomTime_X_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_05")]
        public string PartyFood_RestRoomFlg_X_05 { get; set; }
        [Display(Name = "PartyMember_Name_I_05")]
        public string PartyMember_Name_I_05 { get; set; }
        [Display(Name = "PartyMember_Name_II_05")]
        public string PartyMember_Name_II_05 { get; set; }
        [Display(Name = "PartyMember_Name_III_05")]
        public string PartyMember_Name_III_05 { get; set; }
        [Display(Name = "PartyMember_Name_IV_05")]
        public string PartyMember_Name_IV_05 { get; set; }
        [Display(Name = "PartyMember_Name_V_05")]
        public string PartyMember_Name_V_05 { get; set; }
        [Display(Name = "PartyMember_Name_VI_05")]
        public string PartyMember_Name_VI_05 { get; set; }
        [Display(Name = "PartyMember_Name_VII_05")]
        public string PartyMember_Name_VII_05 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_05")]
        public string PartyMember_Name_VIII_05 { get; set; }
        [Display(Name = "PartyMember_Name_IX_05")]
        public string PartyMember_Name_IX_05 { get; set; }
        [Display(Name = "PartyDate_06")]
        public string PartyDate_06 { get; set; }
        [Display(Name = "BrideFamilyName_06")]
        public string BrideFamilyName_06 { get; set; }
        [Display(Name = "GroomFamilyName_06")]
        public string GroomFamilyName_06 { get; set; }
        [Display(Name = "TantoName_06")]
        public string TantoName_06 { get; set; }
        [Display(Name = "ReporterName_06")]
        public string ReporterName_06 { get; set; }
        [Display(Name = "AdultCnt_06")]
        public string AdultCnt_06 { get; set; }
        [Display(Name = "HalfCnt_06")]
        public string HalfCnt_06 { get; set; }
        [Display(Name = "ChildrenCnt_06")]
        public string ChildrenCnt_06 { get; set; }
        [Display(Name = "SeatOnlyCnt_06")]
        public string SeatOnlyCnt_06 { get; set; }
        [Display(Name = "TableCnt_06")]
        public string TableCnt_06 { get; set; }
        [Display(Name = "TableCross_06")]
        public string TableCross_06 { get; set; }
        [Display(Name = "PartyStyleName_06")]
        public string PartyStyleName_06 { get; set; }
        [Display(Name = "FoodStyleName_06")]
        public string FoodStyleName_06 { get; set; }
        [Display(Name = "FoodPricce_06")]
        public string FoodPricce_06 { get; set; }
        [Display(Name = "DrinkPrice_06")]
        public string DrinkPrice_06 { get; set; }
        [Display(Name = "Wdrink_06")]
        public string Wdrink_06 { get; set; }
        [Display(Name = "Desl_06")]
        public string Desl_06 { get; set; }
        [Display(Name = "RestRoomFlg_06")]
        public string RestRoomFlg_06 { get; set; }
        [Display(Name = "AnketFlg_06")]
        public string AnketFlg_06 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_06")]
        public string PartyTime_TimeName_I_06 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_06")]
        public string PartyTime_OrderTime_I_06 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_06")]
        public string PartyTime_ActTime_I_06 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_06")]
        public string PartyTime_DelayTime_I_06 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_06")]
        public string PartyTime_TimeName_II_06 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_06")]
        public string PartyTime_OrderTime_II_06 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_06")]
        public string PartyTime_ActTime_II_06 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_06")]
        public string PartyTime_DelayTime_II_06 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_06")]
        public string PartyTime_TimeName_III_06 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_06")]
        public string PartyTime_OrderTime_III_06 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_06")]
        public string PartyTime_ActTime_III_06 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_06")]
        public string PartyTime_DelayTime_III_06 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_06")]
        public string PartyTime_TimeName_IV_06 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_06")]
        public string PartyTime_OrderTime_IV_06 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_06")]
        public string PartyTime_ActTime_IV_06 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_06")]
        public string PartyTime_DelayTime_IV_06 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_06")]
        public string PartyTime_TimeName_V_06 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_06")]
        public string PartyTime_OrderTime_V_06 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_06")]
        public string PartyTime_ActTime_V_06 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_06")]
        public string PartyTime_DelayTime_V_06 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_06")]
        public string PartyFood_FoodName_I_06 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_06")]
        public string PartyFood_BeginTime_I_06 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_06")]
        public string PartyFood_EndTime_I_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_06")]
        public string PartyFood_RestRoomTime_I_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_06")]
        public string PartyFood_RestRoomFlg_I_06 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_06")]
        public string PartyFood_FoodName_II_06 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_06")]
        public string PartyFood_BeginTime_II_06 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_06")]
        public string PartyFood_EndTime_II_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_06")]
        public string PartyFood_RestRoomTime_II_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_06")]
        public string PartyFood_RestRoomFlg_II_06 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_06")]
        public string PartyFood_FoodName_III_06 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_06")]
        public string PartyFood_BeginTime_III_06 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_06")]
        public string PartyFood_EndTime_III_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_06")]
        public string PartyFood_RestRoomTime_III_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_06")]
        public string PartyFood_RestRoomFlg_III_06 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_06")]
        public string PartyFood_FoodName_IV_06 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_06")]
        public string PartyFood_BeginTime_IV_06 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_06")]
        public string PartyFood_EndTime_IV_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_06")]
        public string PartyFood_RestRoomTime_IV_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_06")]
        public string PartyFood_RestRoomFlg_IV_06 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_06")]
        public string PartyFood_FoodName_V_06 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_06")]
        public string PartyFood_BeginTime_V_06 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_06")]
        public string PartyFood_EndTime_V_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_06")]
        public string PartyFood_RestRoomTime_V_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_06")]
        public string PartyFood_RestRoomFlg_V_06 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_06")]
        public string PartyFood_FoodName_VI_06 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_06")]
        public string PartyFood_BeginTime_VI_06 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_06")]
        public string PartyFood_EndTime_VI_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_06")]
        public string PartyFood_RestRoomTime_VI_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_06")]
        public string PartyFood_RestRoomFlg_VI_06 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_06")]
        public string PartyFood_FoodName_VII_06 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_06")]
        public string PartyFood_BeginTime_VII_06 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_06")]
        public string PartyFood_EndTime_VII_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_06")]
        public string PartyFood_RestRoomTime_VII_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_06")]
        public string PartyFood_RestRoomFlg_VII_06 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_06")]
        public string PartyFood_FoodName_VIII_06 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_06")]
        public string PartyFood_BeginTime_VIII_06 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_06")]
        public string PartyFood_EndTime_VIII_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_06")]
        public string PartyFood_RestRoomTime_VIII_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_06")]
        public string PartyFood_RestRoomFlg_VIII_06 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_06")]
        public string PartyFood_FoodName_IX_06 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_06")]
        public string PartyFood_BeginTime_IX_06 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_06")]
        public string PartyFood_EndTime_IX_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_06")]
        public string PartyFood_RestRoomTime_IX_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_06")]
        public string PartyFood_RestRoomFlg_IX_06 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_06")]
        public string PartyFood_FoodName_X_06 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_06")]
        public string PartyFood_BeginTime_X_06 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_06")]
        public string PartyFood_EndTime_X_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_06")]
        public string PartyFood_RestRoomTime_X_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_06")]
        public string PartyFood_RestRoomFlg_X_06 { get; set; }
        [Display(Name = "PartyMember_Name_I_06")]
        public string PartyMember_Name_I_06 { get; set; }
        [Display(Name = "PartyMember_Name_II_06")]
        public string PartyMember_Name_II_06 { get; set; }
        [Display(Name = "PartyMember_Name_III_06")]
        public string PartyMember_Name_III_06 { get; set; }
        [Display(Name = "PartyMember_Name_IV_06")]
        public string PartyMember_Name_IV_06 { get; set; }
        [Display(Name = "PartyMember_Name_V_06")]
        public string PartyMember_Name_V_06 { get; set; }
        [Display(Name = "PartyMember_Name_VI_06")]
        public string PartyMember_Name_VI_06 { get; set; }
        [Display(Name = "PartyMember_Name_VII_06")]
        public string PartyMember_Name_VII_06 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_06")]
        public string PartyMember_Name_VIII_06 { get; set; }
        [Display(Name = "PartyMember_Name_IX_06")]
        public string PartyMember_Name_IX_06 { get; set; }
        [Display(Name = "PartyDate_07")]
        public string PartyDate_07 { get; set; }
        [Display(Name = "BrideFamilyName_07")]
        public string BrideFamilyName_07 { get; set; }
        [Display(Name = "GroomFamilyName_07")]
        public string GroomFamilyName_07 { get; set; }
        [Display(Name = "TantoName_07")]
        public string TantoName_07 { get; set; }
        [Display(Name = "ReporterName_07")]
        public string ReporterName_07 { get; set; }
        [Display(Name = "AdultCnt_07")]
        public string AdultCnt_07 { get; set; }
        [Display(Name = "HalfCnt_07")]
        public string HalfCnt_07 { get; set; }
        [Display(Name = "ChildrenCnt_07")]
        public string ChildrenCnt_07 { get; set; }
        [Display(Name = "SeatOnlyCnt_07")]
        public string SeatOnlyCnt_07 { get; set; }
        [Display(Name = "TableCnt_07")]
        public string TableCnt_07 { get; set; }
        [Display(Name = "TableCross_07")]
        public string TableCross_07 { get; set; }
        [Display(Name = "PartyStyleName_07")]
        public string PartyStyleName_07 { get; set; }
        [Display(Name = "FoodStyleName_07")]
        public string FoodStyleName_07 { get; set; }
        [Display(Name = "FoodPricce_07")]
        public string FoodPricce_07 { get; set; }
        [Display(Name = "DrinkPrice_07")]
        public string DrinkPrice_07 { get; set; }
        [Display(Name = "Wdrink_07")]
        public string Wdrink_07 { get; set; }
        [Display(Name = "Desl_07")]
        public string Desl_07 { get; set; }
        [Display(Name = "RestRoomFlg_07")]
        public string RestRoomFlg_07 { get; set; }
        [Display(Name = "AnketFlg_07")]
        public string AnketFlg_07 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_07")]
        public string PartyTime_TimeName_I_07 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_07")]
        public string PartyTime_OrderTime_I_07 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_07")]
        public string PartyTime_ActTime_I_07 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_07")]
        public string PartyTime_DelayTime_I_07 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_07")]
        public string PartyTime_TimeName_II_07 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_07")]
        public string PartyTime_OrderTime_II_07 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_07")]
        public string PartyTime_ActTime_II_07 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_07")]
        public string PartyTime_DelayTime_II_07 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_07")]
        public string PartyTime_TimeName_III_07 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_07")]
        public string PartyTime_OrderTime_III_07 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_07")]
        public string PartyTime_ActTime_III_07 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_07")]
        public string PartyTime_DelayTime_III_07 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_07")]
        public string PartyTime_TimeName_IV_07 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_07")]
        public string PartyTime_OrderTime_IV_07 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_07")]
        public string PartyTime_ActTime_IV_07 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_07")]
        public string PartyTime_DelayTime_IV_07 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_07")]
        public string PartyTime_TimeName_V_07 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_07")]
        public string PartyTime_OrderTime_V_07 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_07")]
        public string PartyTime_ActTime_V_07 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_07")]
        public string PartyTime_DelayTime_V_07 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_07")]
        public string PartyFood_FoodName_I_07 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_07")]
        public string PartyFood_BeginTime_I_07 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_07")]
        public string PartyFood_EndTime_I_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_07")]
        public string PartyFood_RestRoomTime_I_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_07")]
        public string PartyFood_RestRoomFlg_I_07 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_07")]
        public string PartyFood_FoodName_II_07 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_07")]
        public string PartyFood_BeginTime_II_07 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_07")]
        public string PartyFood_EndTime_II_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_07")]
        public string PartyFood_RestRoomTime_II_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_07")]
        public string PartyFood_RestRoomFlg_II_07 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_07")]
        public string PartyFood_FoodName_III_07 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_07")]
        public string PartyFood_BeginTime_III_07 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_07")]
        public string PartyFood_EndTime_III_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_07")]
        public string PartyFood_RestRoomTime_III_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_07")]
        public string PartyFood_RestRoomFlg_III_07 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_07")]
        public string PartyFood_FoodName_IV_07 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_07")]
        public string PartyFood_BeginTime_IV_07 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_07")]
        public string PartyFood_EndTime_IV_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_07")]
        public string PartyFood_RestRoomTime_IV_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_07")]
        public string PartyFood_RestRoomFlg_IV_07 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_07")]
        public string PartyFood_FoodName_V_07 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_07")]
        public string PartyFood_BeginTime_V_07 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_07")]
        public string PartyFood_EndTime_V_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_07")]
        public string PartyFood_RestRoomTime_V_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_07")]
        public string PartyFood_RestRoomFlg_V_07 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_07")]
        public string PartyFood_FoodName_VI_07 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_07")]
        public string PartyFood_BeginTime_VI_07 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_07")]
        public string PartyFood_EndTime_VI_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_07")]
        public string PartyFood_RestRoomTime_VI_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_07")]
        public string PartyFood_RestRoomFlg_VI_07 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_07")]
        public string PartyFood_FoodName_VII_07 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_07")]
        public string PartyFood_BeginTime_VII_07 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_07")]
        public string PartyFood_EndTime_VII_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_07")]
        public string PartyFood_RestRoomTime_VII_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_07")]
        public string PartyFood_RestRoomFlg_VII_07 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_07")]
        public string PartyFood_FoodName_VIII_07 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_07")]
        public string PartyFood_BeginTime_VIII_07 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_07")]
        public string PartyFood_EndTime_VIII_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_07")]
        public string PartyFood_RestRoomTime_VIII_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_07")]
        public string PartyFood_RestRoomFlg_VIII_07 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_07")]
        public string PartyFood_FoodName_IX_07 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_07")]
        public string PartyFood_BeginTime_IX_07 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_07")]
        public string PartyFood_EndTime_IX_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_07")]
        public string PartyFood_RestRoomTime_IX_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_07")]
        public string PartyFood_RestRoomFlg_IX_07 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_07")]
        public string PartyFood_FoodName_X_07 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_07")]
        public string PartyFood_BeginTime_X_07 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_07")]
        public string PartyFood_EndTime_X_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_07")]
        public string PartyFood_RestRoomTime_X_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_07")]
        public string PartyFood_RestRoomFlg_X_07 { get; set; }
        [Display(Name = "PartyMember_Name_I_07")]
        public string PartyMember_Name_I_07 { get; set; }
        [Display(Name = "PartyMember_Name_II_07")]
        public string PartyMember_Name_II_07 { get; set; }
        [Display(Name = "PartyMember_Name_III_07")]
        public string PartyMember_Name_III_07 { get; set; }
        [Display(Name = "PartyMember_Name_IV_07")]
        public string PartyMember_Name_IV_07 { get; set; }
        [Display(Name = "PartyMember_Name_V_07")]
        public string PartyMember_Name_V_07 { get; set; }
        [Display(Name = "PartyMember_Name_VI_07")]
        public string PartyMember_Name_VI_07 { get; set; }
        [Display(Name = "PartyMember_Name_VII_07")]
        public string PartyMember_Name_VII_07 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_07")]
        public string PartyMember_Name_VIII_07 { get; set; }
        [Display(Name = "PartyMember_Name_IX_07")]
        public string PartyMember_Name_IX_07 { get; set; }
        [Display(Name = "PartyDate_08")]
        public string PartyDate_08 { get; set; }
        [Display(Name = "BrideFamilyName_08")]
        public string BrideFamilyName_08 { get; set; }
        [Display(Name = "GroomFamilyName_08")]
        public string GroomFamilyName_08 { get; set; }
        [Display(Name = "TantoName_08")]
        public string TantoName_08 { get; set; }
        [Display(Name = "ReporterName_08")]
        public string ReporterName_08 { get; set; }
        [Display(Name = "AdultCnt_08")]
        public string AdultCnt_08 { get; set; }
        [Display(Name = "HalfCnt_08")]
        public string HalfCnt_08 { get; set; }
        [Display(Name = "ChildrenCnt_08")]
        public string ChildrenCnt_08 { get; set; }
        [Display(Name = "SeatOnlyCnt_08")]
        public string SeatOnlyCnt_08 { get; set; }
        [Display(Name = "TableCnt_08")]
        public string TableCnt_08 { get; set; }
        [Display(Name = "TableCross_08")]
        public string TableCross_08 { get; set; }
        [Display(Name = "PartyStyleName_08")]
        public string PartyStyleName_08 { get; set; }
        [Display(Name = "FoodStyleName_08")]
        public string FoodStyleName_08 { get; set; }
        [Display(Name = "FoodPricce_08")]
        public string FoodPricce_08 { get; set; }
        [Display(Name = "DrinkPrice_08")]
        public string DrinkPrice_08 { get; set; }
        [Display(Name = "Wdrink_08")]
        public string Wdrink_08 { get; set; }
        [Display(Name = "Desl_08")]
        public string Desl_08 { get; set; }
        [Display(Name = "RestRoomFlg_08")]
        public string RestRoomFlg_08 { get; set; }
        [Display(Name = "AnketFlg_08")]
        public string AnketFlg_08 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_08")]
        public string PartyTime_TimeName_I_08 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_08")]
        public string PartyTime_OrderTime_I_08 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_08")]
        public string PartyTime_ActTime_I_08 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_08")]
        public string PartyTime_DelayTime_I_08 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_08")]
        public string PartyTime_TimeName_II_08 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_08")]
        public string PartyTime_OrderTime_II_08 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_08")]
        public string PartyTime_ActTime_II_08 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_08")]
        public string PartyTime_DelayTime_II_08 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_08")]
        public string PartyTime_TimeName_III_08 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_08")]
        public string PartyTime_OrderTime_III_08 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_08")]
        public string PartyTime_ActTime_III_08 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_08")]
        public string PartyTime_DelayTime_III_08 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_08")]
        public string PartyTime_TimeName_IV_08 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_08")]
        public string PartyTime_OrderTime_IV_08 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_08")]
        public string PartyTime_ActTime_IV_08 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_08")]
        public string PartyTime_DelayTime_IV_08 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_08")]
        public string PartyTime_TimeName_V_08 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_08")]
        public string PartyTime_OrderTime_V_08 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_08")]
        public string PartyTime_ActTime_V_08 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_08")]
        public string PartyTime_DelayTime_V_08 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_08")]
        public string PartyFood_FoodName_I_08 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_08")]
        public string PartyFood_BeginTime_I_08 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_08")]
        public string PartyFood_EndTime_I_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_08")]
        public string PartyFood_RestRoomTime_I_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_08")]
        public string PartyFood_RestRoomFlg_I_08 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_08")]
        public string PartyFood_FoodName_II_08 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_08")]
        public string PartyFood_BeginTime_II_08 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_08")]
        public string PartyFood_EndTime_II_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_08")]
        public string PartyFood_RestRoomTime_II_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_08")]
        public string PartyFood_RestRoomFlg_II_08 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_08")]
        public string PartyFood_FoodName_III_08 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_08")]
        public string PartyFood_BeginTime_III_08 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_08")]
        public string PartyFood_EndTime_III_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_08")]
        public string PartyFood_RestRoomTime_III_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_08")]
        public string PartyFood_RestRoomFlg_III_08 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_08")]
        public string PartyFood_FoodName_IV_08 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_08")]
        public string PartyFood_BeginTime_IV_08 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_08")]
        public string PartyFood_EndTime_IV_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_08")]
        public string PartyFood_RestRoomTime_IV_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_08")]
        public string PartyFood_RestRoomFlg_IV_08 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_08")]
        public string PartyFood_FoodName_V_08 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_08")]
        public string PartyFood_BeginTime_V_08 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_08")]
        public string PartyFood_EndTime_V_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_08")]
        public string PartyFood_RestRoomTime_V_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_08")]
        public string PartyFood_RestRoomFlg_V_08 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_08")]
        public string PartyFood_FoodName_VI_08 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_08")]
        public string PartyFood_BeginTime_VI_08 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_08")]
        public string PartyFood_EndTime_VI_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_08")]
        public string PartyFood_RestRoomTime_VI_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_08")]
        public string PartyFood_RestRoomFlg_VI_08 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_08")]
        public string PartyFood_FoodName_VII_08 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_08")]
        public string PartyFood_BeginTime_VII_08 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_08")]
        public string PartyFood_EndTime_VII_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_08")]
        public string PartyFood_RestRoomTime_VII_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_08")]
        public string PartyFood_RestRoomFlg_VII_08 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_08")]
        public string PartyFood_FoodName_VIII_08 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_08")]
        public string PartyFood_BeginTime_VIII_08 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_08")]
        public string PartyFood_EndTime_VIII_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_08")]
        public string PartyFood_RestRoomTime_VIII_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_08")]
        public string PartyFood_RestRoomFlg_VIII_08 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_08")]
        public string PartyFood_FoodName_IX_08 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_08")]
        public string PartyFood_BeginTime_IX_08 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_08")]
        public string PartyFood_EndTime_IX_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_08")]
        public string PartyFood_RestRoomTime_IX_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_08")]
        public string PartyFood_RestRoomFlg_IX_08 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_08")]
        public string PartyFood_FoodName_X_08 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_08")]
        public string PartyFood_BeginTime_X_08 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_08")]
        public string PartyFood_EndTime_X_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_08")]
        public string PartyFood_RestRoomTime_X_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_08")]
        public string PartyFood_RestRoomFlg_X_08 { get; set; }
        [Display(Name = "PartyMember_Name_I_08")]
        public string PartyMember_Name_I_08 { get; set; }
        [Display(Name = "PartyMember_Name_II_08")]
        public string PartyMember_Name_II_08 { get; set; }
        [Display(Name = "PartyMember_Name_III_08")]
        public string PartyMember_Name_III_08 { get; set; }
        [Display(Name = "PartyMember_Name_IV_08")]
        public string PartyMember_Name_IV_08 { get; set; }
        [Display(Name = "PartyMember_Name_V_08")]
        public string PartyMember_Name_V_08 { get; set; }
        [Display(Name = "PartyMember_Name_VI_08")]
        public string PartyMember_Name_VI_08 { get; set; }
        [Display(Name = "PartyMember_Name_VII_08")]
        public string PartyMember_Name_VII_08 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_08")]
        public string PartyMember_Name_VIII_08 { get; set; }
        [Display(Name = "PartyMember_Name_IX_08")]
        public string PartyMember_Name_IX_08 { get; set; }
        [Display(Name = "PartyDate_09")]
        public string PartyDate_09 { get; set; }
        [Display(Name = "BrideFamilyName_09")]
        public string BrideFamilyName_09 { get; set; }
        [Display(Name = "GroomFamilyName_09")]
        public string GroomFamilyName_09 { get; set; }
        [Display(Name = "TantoName_09")]
        public string TantoName_09 { get; set; }
        [Display(Name = "ReporterName_09")]
        public string ReporterName_09 { get; set; }
        [Display(Name = "AdultCnt_09")]
        public string AdultCnt_09 { get; set; }
        [Display(Name = "HalfCnt_09")]
        public string HalfCnt_09 { get; set; }
        [Display(Name = "ChildrenCnt_09")]
        public string ChildrenCnt_09 { get; set; }
        [Display(Name = "SeatOnlyCnt_09")]
        public string SeatOnlyCnt_09 { get; set; }
        [Display(Name = "TableCnt_09")]
        public string TableCnt_09 { get; set; }
        [Display(Name = "TableCross_09")]
        public string TableCross_09 { get; set; }
        [Display(Name = "PartyStyleName_09")]
        public string PartyStyleName_09 { get; set; }
        [Display(Name = "FoodStyleName_09")]
        public string FoodStyleName_09 { get; set; }
        [Display(Name = "FoodPricce_09")]
        public string FoodPricce_09 { get; set; }
        [Display(Name = "DrinkPrice_09")]
        public string DrinkPrice_09 { get; set; }
        [Display(Name = "Wdrink_09")]
        public string Wdrink_09 { get; set; }
        [Display(Name = "Desl_09")]
        public string Desl_09 { get; set; }
        [Display(Name = "RestRoomFlg_09")]
        public string RestRoomFlg_09 { get; set; }
        [Display(Name = "AnketFlg_09")]
        public string AnketFlg_09 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_09")]
        public string PartyTime_TimeName_I_09 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_09")]
        public string PartyTime_OrderTime_I_09 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_09")]
        public string PartyTime_ActTime_I_09 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_09")]
        public string PartyTime_DelayTime_I_09 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_09")]
        public string PartyTime_TimeName_II_09 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_09")]
        public string PartyTime_OrderTime_II_09 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_09")]
        public string PartyTime_ActTime_II_09 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_09")]
        public string PartyTime_DelayTime_II_09 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_09")]
        public string PartyTime_TimeName_III_09 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_09")]
        public string PartyTime_OrderTime_III_09 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_09")]
        public string PartyTime_ActTime_III_09 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_09")]
        public string PartyTime_DelayTime_III_09 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_09")]
        public string PartyTime_TimeName_IV_09 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_09")]
        public string PartyTime_OrderTime_IV_09 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_09")]
        public string PartyTime_ActTime_IV_09 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_09")]
        public string PartyTime_DelayTime_IV_09 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_09")]
        public string PartyTime_TimeName_V_09 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_09")]
        public string PartyTime_OrderTime_V_09 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_09")]
        public string PartyTime_ActTime_V_09 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_09")]
        public string PartyTime_DelayTime_V_09 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_09")]
        public string PartyFood_FoodName_I_09 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_09")]
        public string PartyFood_BeginTime_I_09 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_09")]
        public string PartyFood_EndTime_I_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_09")]
        public string PartyFood_RestRoomTime_I_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_09")]
        public string PartyFood_RestRoomFlg_I_09 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_09")]
        public string PartyFood_FoodName_II_09 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_09")]
        public string PartyFood_BeginTime_II_09 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_09")]
        public string PartyFood_EndTime_II_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_09")]
        public string PartyFood_RestRoomTime_II_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_09")]
        public string PartyFood_RestRoomFlg_II_09 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_09")]
        public string PartyFood_FoodName_III_09 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_09")]
        public string PartyFood_BeginTime_III_09 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_09")]
        public string PartyFood_EndTime_III_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_09")]
        public string PartyFood_RestRoomTime_III_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_09")]
        public string PartyFood_RestRoomFlg_III_09 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_09")]
        public string PartyFood_FoodName_IV_09 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_09")]
        public string PartyFood_BeginTime_IV_09 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_09")]
        public string PartyFood_EndTime_IV_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_09")]
        public string PartyFood_RestRoomTime_IV_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_09")]
        public string PartyFood_RestRoomFlg_IV_09 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_09")]
        public string PartyFood_FoodName_V_09 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_09")]
        public string PartyFood_BeginTime_V_09 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_09")]
        public string PartyFood_EndTime_V_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_09")]
        public string PartyFood_RestRoomTime_V_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_09")]
        public string PartyFood_RestRoomFlg_V_09 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_09")]
        public string PartyFood_FoodName_VI_09 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_09")]
        public string PartyFood_BeginTime_VI_09 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_09")]
        public string PartyFood_EndTime_VI_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_09")]
        public string PartyFood_RestRoomTime_VI_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_09")]
        public string PartyFood_RestRoomFlg_VI_09 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_09")]
        public string PartyFood_FoodName_VII_09 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_09")]
        public string PartyFood_BeginTime_VII_09 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_09")]
        public string PartyFood_EndTime_VII_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_09")]
        public string PartyFood_RestRoomTime_VII_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_09")]
        public string PartyFood_RestRoomFlg_VII_09 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_09")]
        public string PartyFood_FoodName_VIII_09 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_09")]
        public string PartyFood_BeginTime_VIII_09 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_09")]
        public string PartyFood_EndTime_VIII_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_09")]
        public string PartyFood_RestRoomTime_VIII_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_09")]
        public string PartyFood_RestRoomFlg_VIII_09 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_09")]
        public string PartyFood_FoodName_IX_09 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_09")]
        public string PartyFood_BeginTime_IX_09 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_09")]
        public string PartyFood_EndTime_IX_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_09")]
        public string PartyFood_RestRoomTime_IX_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_09")]
        public string PartyFood_RestRoomFlg_IX_09 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_09")]
        public string PartyFood_FoodName_X_09 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_09")]
        public string PartyFood_BeginTime_X_09 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_09")]
        public string PartyFood_EndTime_X_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_09")]
        public string PartyFood_RestRoomTime_X_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_09")]
        public string PartyFood_RestRoomFlg_X_09 { get; set; }
        [Display(Name = "PartyMember_Name_I_09")]
        public string PartyMember_Name_I_09 { get; set; }
        [Display(Name = "PartyMember_Name_II_09")]
        public string PartyMember_Name_II_09 { get; set; }
        [Display(Name = "PartyMember_Name_III_09")]
        public string PartyMember_Name_III_09 { get; set; }
        [Display(Name = "PartyMember_Name_IV_09")]
        public string PartyMember_Name_IV_09 { get; set; }
        [Display(Name = "PartyMember_Name_V_09")]
        public string PartyMember_Name_V_09 { get; set; }
        [Display(Name = "PartyMember_Name_VI_09")]
        public string PartyMember_Name_VI_09 { get; set; }
        [Display(Name = "PartyMember_Name_VII_09")]
        public string PartyMember_Name_VII_09 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_09")]
        public string PartyMember_Name_VIII_09 { get; set; }
        [Display(Name = "PartyMember_Name_IX_09")]
        public string PartyMember_Name_IX_09 { get; set; }
        [Display(Name = "PartyDate_10")]
        public string PartyDate_10 { get; set; }
        [Display(Name = "BrideFamilyName_10")]
        public string BrideFamilyName_10 { get; set; }
        [Display(Name = "GroomFamilyName_10")]
        public string GroomFamilyName_10 { get; set; }
        [Display(Name = "TantoName_10")]
        public string TantoName_10 { get; set; }
        [Display(Name = "ReporterName_10")]
        public string ReporterName_10 { get; set; }
        [Display(Name = "AdultCnt_10")]
        public string AdultCnt_10 { get; set; }
        [Display(Name = "HalfCnt_10")]
        public string HalfCnt_10 { get; set; }
        [Display(Name = "ChildrenCnt_10")]
        public string ChildrenCnt_10 { get; set; }
        [Display(Name = "SeatOnlyCnt_10")]
        public string SeatOnlyCnt_10 { get; set; }
        [Display(Name = "TableCnt_10")]
        public string TableCnt_10 { get; set; }
        [Display(Name = "TableCross_10")]
        public string TableCross_10 { get; set; }
        [Display(Name = "PartyStyleName_10")]
        public string PartyStyleName_10 { get; set; }
        [Display(Name = "FoodStyleName_10")]
        public string FoodStyleName_10 { get; set; }
        [Display(Name = "FoodPricce_10")]
        public string FoodPricce_10 { get; set; }
        [Display(Name = "DrinkPrice_10")]
        public string DrinkPrice_10 { get; set; }
        [Display(Name = "Wdrink_10")]
        public string Wdrink_10 { get; set; }
        [Display(Name = "Desl_10")]
        public string Desl_10 { get; set; }
        [Display(Name = "RestRoomFlg_10")]
        public string RestRoomFlg_10 { get; set; }
        [Display(Name = "AnketFlg_10")]
        public string AnketFlg_10 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_10")]
        public string PartyTime_TimeName_I_10 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_10")]
        public string PartyTime_OrderTime_I_10 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_10")]
        public string PartyTime_ActTime_I_10 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_10")]
        public string PartyTime_DelayTime_I_10 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_10")]
        public string PartyTime_TimeName_II_10 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_10")]
        public string PartyTime_OrderTime_II_10 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_10")]
        public string PartyTime_ActTime_II_10 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_10")]
        public string PartyTime_DelayTime_II_10 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_10")]
        public string PartyTime_TimeName_III_10 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_10")]
        public string PartyTime_OrderTime_III_10 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_10")]
        public string PartyTime_ActTime_III_10 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_10")]
        public string PartyTime_DelayTime_III_10 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_10")]
        public string PartyTime_TimeName_IV_10 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_10")]
        public string PartyTime_OrderTime_IV_10 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_10")]
        public string PartyTime_ActTime_IV_10 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_10")]
        public string PartyTime_DelayTime_IV_10 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_10")]
        public string PartyTime_TimeName_V_10 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_10")]
        public string PartyTime_OrderTime_V_10 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_10")]
        public string PartyTime_ActTime_V_10 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_10")]
        public string PartyTime_DelayTime_V_10 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_10")]
        public string PartyFood_FoodName_I_10 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_10")]
        public string PartyFood_BeginTime_I_10 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_10")]
        public string PartyFood_EndTime_I_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_10")]
        public string PartyFood_RestRoomTime_I_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_10")]
        public string PartyFood_RestRoomFlg_I_10 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_10")]
        public string PartyFood_FoodName_II_10 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_10")]
        public string PartyFood_BeginTime_II_10 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_10")]
        public string PartyFood_EndTime_II_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_10")]
        public string PartyFood_RestRoomTime_II_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_10")]
        public string PartyFood_RestRoomFlg_II_10 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_10")]
        public string PartyFood_FoodName_III_10 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_10")]
        public string PartyFood_BeginTime_III_10 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_10")]
        public string PartyFood_EndTime_III_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_10")]
        public string PartyFood_RestRoomTime_III_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_10")]
        public string PartyFood_RestRoomFlg_III_10 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_10")]
        public string PartyFood_FoodName_IV_10 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_10")]
        public string PartyFood_BeginTime_IV_10 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_10")]
        public string PartyFood_EndTime_IV_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_10")]
        public string PartyFood_RestRoomTime_IV_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_10")]
        public string PartyFood_RestRoomFlg_IV_10 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_10")]
        public string PartyFood_FoodName_V_10 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_10")]
        public string PartyFood_BeginTime_V_10 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_10")]
        public string PartyFood_EndTime_V_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_10")]
        public string PartyFood_RestRoomTime_V_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_10")]
        public string PartyFood_RestRoomFlg_V_10 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_10")]
        public string PartyFood_FoodName_VI_10 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_10")]
        public string PartyFood_BeginTime_VI_10 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_10")]
        public string PartyFood_EndTime_VI_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_10")]
        public string PartyFood_RestRoomTime_VI_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_10")]
        public string PartyFood_RestRoomFlg_VI_10 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_10")]
        public string PartyFood_FoodName_VII_10 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_10")]
        public string PartyFood_BeginTime_VII_10 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_10")]
        public string PartyFood_EndTime_VII_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_10")]
        public string PartyFood_RestRoomTime_VII_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_10")]
        public string PartyFood_RestRoomFlg_VII_10 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_10")]
        public string PartyFood_FoodName_VIII_10 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_10")]
        public string PartyFood_BeginTime_VIII_10 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_10")]
        public string PartyFood_EndTime_VIII_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_10")]
        public string PartyFood_RestRoomTime_VIII_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_10")]
        public string PartyFood_RestRoomFlg_VIII_10 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_10")]
        public string PartyFood_FoodName_IX_10 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_10")]
        public string PartyFood_BeginTime_IX_10 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_10")]
        public string PartyFood_EndTime_IX_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_10")]
        public string PartyFood_RestRoomTime_IX_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_10")]
        public string PartyFood_RestRoomFlg_IX_10 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_10")]
        public string PartyFood_FoodName_X_10 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_10")]
        public string PartyFood_BeginTime_X_10 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_10")]
        public string PartyFood_EndTime_X_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_10")]
        public string PartyFood_RestRoomTime_X_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_10")]
        public string PartyFood_RestRoomFlg_X_10 { get; set; }
        [Display(Name = "PartyMember_Name_I_10")]
        public string PartyMember_Name_I_10 { get; set; }
        [Display(Name = "PartyMember_Name_II_10")]
        public string PartyMember_Name_II_10 { get; set; }
        [Display(Name = "PartyMember_Name_III_10")]
        public string PartyMember_Name_III_10 { get; set; }
        [Display(Name = "PartyMember_Name_IV_10")]
        public string PartyMember_Name_IV_10 { get; set; }
        [Display(Name = "PartyMember_Name_V_10")]
        public string PartyMember_Name_V_10 { get; set; }
        [Display(Name = "PartyMember_Name_VI_10")]
        public string PartyMember_Name_VI_10 { get; set; }
        [Display(Name = "PartyMember_Name_VII_10")]
        public string PartyMember_Name_VII_10 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_10")]
        public string PartyMember_Name_VIII_10 { get; set; }
        [Display(Name = "PartyMember_Name_IX_10")]
        public string PartyMember_Name_IX_10 { get; set; }
        [Display(Name = "PartyDate_11")]
        public string PartyDate_11 { get; set; }
        [Display(Name = "BrideFamilyName_11")]
        public string BrideFamilyName_11 { get; set; }
        [Display(Name = "GroomFamilyName_11")]
        public string GroomFamilyName_11 { get; set; }
        [Display(Name = "TantoName_11")]
        public string TantoName_11 { get; set; }
        [Display(Name = "ReporterName_11")]
        public string ReporterName_11 { get; set; }
        [Display(Name = "AdultCnt_11")]
        public string AdultCnt_11 { get; set; }
        [Display(Name = "HalfCnt_11")]
        public string HalfCnt_11 { get; set; }
        [Display(Name = "ChildrenCnt_11")]
        public string ChildrenCnt_11 { get; set; }
        [Display(Name = "SeatOnlyCnt_11")]
        public string SeatOnlyCnt_11 { get; set; }
        [Display(Name = "TableCnt_11")]
        public string TableCnt_11 { get; set; }
        [Display(Name = "TableCross_11")]
        public string TableCross_11 { get; set; }
        [Display(Name = "PartyStyleName_11")]
        public string PartyStyleName_11 { get; set; }
        [Display(Name = "FoodStyleName_11")]
        public string FoodStyleName_11 { get; set; }
        [Display(Name = "FoodPricce_11")]
        public string FoodPricce_11 { get; set; }
        [Display(Name = "DrinkPrice_11")]
        public string DrinkPrice_11 { get; set; }
        [Display(Name = "Wdrink_11")]
        public string Wdrink_11 { get; set; }
        [Display(Name = "Desl_11")]
        public string Desl_11 { get; set; }
        [Display(Name = "RestRoomFlg_11")]
        public string RestRoomFlg_11 { get; set; }
        [Display(Name = "AnketFlg_11")]
        public string AnketFlg_11 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_11")]
        public string PartyTime_TimeName_I_11 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_11")]
        public string PartyTime_OrderTime_I_11 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_11")]
        public string PartyTime_ActTime_I_11 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_11")]
        public string PartyTime_DelayTime_I_11 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_11")]
        public string PartyTime_TimeName_II_11 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_11")]
        public string PartyTime_OrderTime_II_11 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_11")]
        public string PartyTime_ActTime_II_11 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_11")]
        public string PartyTime_DelayTime_II_11 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_11")]
        public string PartyTime_TimeName_III_11 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_11")]
        public string PartyTime_OrderTime_III_11 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_11")]
        public string PartyTime_ActTime_III_11 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_11")]
        public string PartyTime_DelayTime_III_11 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_11")]
        public string PartyTime_TimeName_IV_11 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_11")]
        public string PartyTime_OrderTime_IV_11 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_11")]
        public string PartyTime_ActTime_IV_11 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_11")]
        public string PartyTime_DelayTime_IV_11 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_11")]
        public string PartyTime_TimeName_V_11 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_11")]
        public string PartyTime_OrderTime_V_11 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_11")]
        public string PartyTime_ActTime_V_11 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_11")]
        public string PartyTime_DelayTime_V_11 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_11")]
        public string PartyFood_FoodName_I_11 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_11")]
        public string PartyFood_BeginTime_I_11 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_11")]
        public string PartyFood_EndTime_I_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_11")]
        public string PartyFood_RestRoomTime_I_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_11")]
        public string PartyFood_RestRoomFlg_I_11 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_11")]
        public string PartyFood_FoodName_II_11 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_11")]
        public string PartyFood_BeginTime_II_11 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_11")]
        public string PartyFood_EndTime_II_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_11")]
        public string PartyFood_RestRoomTime_II_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_11")]
        public string PartyFood_RestRoomFlg_II_11 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_11")]
        public string PartyFood_FoodName_III_11 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_11")]
        public string PartyFood_BeginTime_III_11 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_11")]
        public string PartyFood_EndTime_III_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_11")]
        public string PartyFood_RestRoomTime_III_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_11")]
        public string PartyFood_RestRoomFlg_III_11 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_11")]
        public string PartyFood_FoodName_IV_11 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_11")]
        public string PartyFood_BeginTime_IV_11 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_11")]
        public string PartyFood_EndTime_IV_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_11")]
        public string PartyFood_RestRoomTime_IV_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_11")]
        public string PartyFood_RestRoomFlg_IV_11 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_11")]
        public string PartyFood_FoodName_V_11 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_11")]
        public string PartyFood_BeginTime_V_11 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_11")]
        public string PartyFood_EndTime_V_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_11")]
        public string PartyFood_RestRoomTime_V_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_11")]
        public string PartyFood_RestRoomFlg_V_11 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_11")]
        public string PartyFood_FoodName_VI_11 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_11")]
        public string PartyFood_BeginTime_VI_11 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_11")]
        public string PartyFood_EndTime_VI_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_11")]
        public string PartyFood_RestRoomTime_VI_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_11")]
        public string PartyFood_RestRoomFlg_VI_11 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_11")]
        public string PartyFood_FoodName_VII_11 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_11")]
        public string PartyFood_BeginTime_VII_11 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_11")]
        public string PartyFood_EndTime_VII_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_11")]
        public string PartyFood_RestRoomTime_VII_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_11")]
        public string PartyFood_RestRoomFlg_VII_11 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_11")]
        public string PartyFood_FoodName_VIII_11 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_11")]
        public string PartyFood_BeginTime_VIII_11 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_11")]
        public string PartyFood_EndTime_VIII_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_11")]
        public string PartyFood_RestRoomTime_VIII_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_11")]
        public string PartyFood_RestRoomFlg_VIII_11 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_11")]
        public string PartyFood_FoodName_IX_11 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_11")]
        public string PartyFood_BeginTime_IX_11 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_11")]
        public string PartyFood_EndTime_IX_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_11")]
        public string PartyFood_RestRoomTime_IX_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_11")]
        public string PartyFood_RestRoomFlg_IX_11 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_11")]
        public string PartyFood_FoodName_X_11 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_11")]
        public string PartyFood_BeginTime_X_11 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_11")]
        public string PartyFood_EndTime_X_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_11")]
        public string PartyFood_RestRoomTime_X_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_11")]
        public string PartyFood_RestRoomFlg_X_11 { get; set; }
        [Display(Name = "PartyMember_Name_I_11")]
        public string PartyMember_Name_I_11 { get; set; }
        [Display(Name = "PartyMember_Name_II_11")]
        public string PartyMember_Name_II_11 { get; set; }
        [Display(Name = "PartyMember_Name_III_11")]
        public string PartyMember_Name_III_11 { get; set; }
        [Display(Name = "PartyMember_Name_IV_11")]
        public string PartyMember_Name_IV_11 { get; set; }
        [Display(Name = "PartyMember_Name_V_11")]
        public string PartyMember_Name_V_11 { get; set; }
        [Display(Name = "PartyMember_Name_VI_11")]
        public string PartyMember_Name_VI_11 { get; set; }
        [Display(Name = "PartyMember_Name_VII_11")]
        public string PartyMember_Name_VII_11 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_11")]
        public string PartyMember_Name_VIII_11 { get; set; }
        [Display(Name = "PartyMember_Name_IX_11")]
        public string PartyMember_Name_IX_11 { get; set; }
        [Display(Name = "PartyDate_12")]
        public string PartyDate_12 { get; set; }
        [Display(Name = "BrideFamilyName_12")]
        public string BrideFamilyName_12 { get; set; }
        [Display(Name = "GroomFamilyName_12")]
        public string GroomFamilyName_12 { get; set; }
        [Display(Name = "TantoName_12")]
        public string TantoName_12 { get; set; }
        [Display(Name = "ReporterName_12")]
        public string ReporterName_12 { get; set; }
        [Display(Name = "AdultCnt_12")]
        public string AdultCnt_12 { get; set; }
        [Display(Name = "HalfCnt_12")]
        public string HalfCnt_12 { get; set; }
        [Display(Name = "ChildrenCnt_12")]
        public string ChildrenCnt_12 { get; set; }
        [Display(Name = "SeatOnlyCnt_12")]
        public string SeatOnlyCnt_12 { get; set; }
        [Display(Name = "TableCnt_12")]
        public string TableCnt_12 { get; set; }
        [Display(Name = "TableCross_12")]
        public string TableCross_12 { get; set; }
        [Display(Name = "PartyStyleName_12")]
        public string PartyStyleName_12 { get; set; }
        [Display(Name = "FoodStyleName_12")]
        public string FoodStyleName_12 { get; set; }
        [Display(Name = "FoodPricce_12")]
        public string FoodPricce_12 { get; set; }
        [Display(Name = "DrinkPrice_12")]
        public string DrinkPrice_12 { get; set; }
        [Display(Name = "Wdrink_12")]
        public string Wdrink_12 { get; set; }
        [Display(Name = "Desl_12")]
        public string Desl_12 { get; set; }
        [Display(Name = "RestRoomFlg_12")]
        public string RestRoomFlg_12 { get; set; }
        [Display(Name = "AnketFlg_12")]
        public string AnketFlg_12 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_12")]
        public string PartyTime_TimeName_I_12 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_12")]
        public string PartyTime_OrderTime_I_12 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_12")]
        public string PartyTime_ActTime_I_12 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_12")]
        public string PartyTime_DelayTime_I_12 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_12")]
        public string PartyTime_TimeName_II_12 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_12")]
        public string PartyTime_OrderTime_II_12 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_12")]
        public string PartyTime_ActTime_II_12 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_12")]
        public string PartyTime_DelayTime_II_12 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_12")]
        public string PartyTime_TimeName_III_12 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_12")]
        public string PartyTime_OrderTime_III_12 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_12")]
        public string PartyTime_ActTime_III_12 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_12")]
        public string PartyTime_DelayTime_III_12 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_12")]
        public string PartyTime_TimeName_IV_12 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_12")]
        public string PartyTime_OrderTime_IV_12 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_12")]
        public string PartyTime_ActTime_IV_12 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_12")]
        public string PartyTime_DelayTime_IV_12 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_12")]
        public string PartyTime_TimeName_V_12 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_12")]
        public string PartyTime_OrderTime_V_12 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_12")]
        public string PartyTime_ActTime_V_12 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_12")]
        public string PartyTime_DelayTime_V_12 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_12")]
        public string PartyFood_FoodName_I_12 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_12")]
        public string PartyFood_BeginTime_I_12 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_12")]
        public string PartyFood_EndTime_I_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_12")]
        public string PartyFood_RestRoomTime_I_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_12")]
        public string PartyFood_RestRoomFlg_I_12 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_12")]
        public string PartyFood_FoodName_II_12 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_12")]
        public string PartyFood_BeginTime_II_12 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_12")]
        public string PartyFood_EndTime_II_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_12")]
        public string PartyFood_RestRoomTime_II_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_12")]
        public string PartyFood_RestRoomFlg_II_12 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_12")]
        public string PartyFood_FoodName_III_12 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_12")]
        public string PartyFood_BeginTime_III_12 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_12")]
        public string PartyFood_EndTime_III_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_12")]
        public string PartyFood_RestRoomTime_III_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_12")]
        public string PartyFood_RestRoomFlg_III_12 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_12")]
        public string PartyFood_FoodName_IV_12 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_12")]
        public string PartyFood_BeginTime_IV_12 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_12")]
        public string PartyFood_EndTime_IV_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_12")]
        public string PartyFood_RestRoomTime_IV_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_12")]
        public string PartyFood_RestRoomFlg_IV_12 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_12")]
        public string PartyFood_FoodName_V_12 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_12")]
        public string PartyFood_BeginTime_V_12 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_12")]
        public string PartyFood_EndTime_V_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_12")]
        public string PartyFood_RestRoomTime_V_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_12")]
        public string PartyFood_RestRoomFlg_V_12 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_12")]
        public string PartyFood_FoodName_VI_12 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_12")]
        public string PartyFood_BeginTime_VI_12 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_12")]
        public string PartyFood_EndTime_VI_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_12")]
        public string PartyFood_RestRoomTime_VI_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_12")]
        public string PartyFood_RestRoomFlg_VI_12 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_12")]
        public string PartyFood_FoodName_VII_12 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_12")]
        public string PartyFood_BeginTime_VII_12 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_12")]
        public string PartyFood_EndTime_VII_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_12")]
        public string PartyFood_RestRoomTime_VII_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_12")]
        public string PartyFood_RestRoomFlg_VII_12 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_12")]
        public string PartyFood_FoodName_VIII_12 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_12")]
        public string PartyFood_BeginTime_VIII_12 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_12")]
        public string PartyFood_EndTime_VIII_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_12")]
        public string PartyFood_RestRoomTime_VIII_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_12")]
        public string PartyFood_RestRoomFlg_VIII_12 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_12")]
        public string PartyFood_FoodName_IX_12 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_12")]
        public string PartyFood_BeginTime_IX_12 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_12")]
        public string PartyFood_EndTime_IX_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_12")]
        public string PartyFood_RestRoomTime_IX_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_12")]
        public string PartyFood_RestRoomFlg_IX_12 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_12")]
        public string PartyFood_FoodName_X_12 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_12")]
        public string PartyFood_BeginTime_X_12 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_12")]
        public string PartyFood_EndTime_X_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_12")]
        public string PartyFood_RestRoomTime_X_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_12")]
        public string PartyFood_RestRoomFlg_X_12 { get; set; }
        [Display(Name = "PartyMember_Name_I_12")]
        public string PartyMember_Name_I_12 { get; set; }
        [Display(Name = "PartyMember_Name_II_12")]
        public string PartyMember_Name_II_12 { get; set; }
        [Display(Name = "PartyMember_Name_III_12")]
        public string PartyMember_Name_III_12 { get; set; }
        [Display(Name = "PartyMember_Name_IV_12")]
        public string PartyMember_Name_IV_12 { get; set; }
        [Display(Name = "PartyMember_Name_V_12")]
        public string PartyMember_Name_V_12 { get; set; }
        [Display(Name = "PartyMember_Name_VI_12")]
        public string PartyMember_Name_VI_12 { get; set; }
        [Display(Name = "PartyMember_Name_VII_12")]
        public string PartyMember_Name_VII_12 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_12")]
        public string PartyMember_Name_VIII_12 { get; set; }
        [Display(Name = "PartyMember_Name_IX_12")]
        public string PartyMember_Name_IX_12 { get; set; }
        [Display(Name = "PartyDate_13")]
        public string PartyDate_13 { get; set; }
        [Display(Name = "BrideFamilyName_13")]
        public string BrideFamilyName_13 { get; set; }
        [Display(Name = "GroomFamilyName_13")]
        public string GroomFamilyName_13 { get; set; }
        [Display(Name = "TantoName_13")]
        public string TantoName_13 { get; set; }
        [Display(Name = "ReporterName_13")]
        public string ReporterName_13 { get; set; }
        [Display(Name = "AdultCnt_13")]
        public string AdultCnt_13 { get; set; }
        [Display(Name = "HalfCnt_13")]
        public string HalfCnt_13 { get; set; }
        [Display(Name = "ChildrenCnt_13")]
        public string ChildrenCnt_13 { get; set; }
        [Display(Name = "SeatOnlyCnt_13")]
        public string SeatOnlyCnt_13 { get; set; }
        [Display(Name = "TableCnt_13")]
        public string TableCnt_13 { get; set; }
        [Display(Name = "TableCross_13")]
        public string TableCross_13 { get; set; }
        [Display(Name = "PartyStyleName_13")]
        public string PartyStyleName_13 { get; set; }
        [Display(Name = "FoodStyleName_13")]
        public string FoodStyleName_13 { get; set; }
        [Display(Name = "FoodPricce_13")]
        public string FoodPricce_13 { get; set; }
        [Display(Name = "DrinkPrice_13")]
        public string DrinkPrice_13 { get; set; }
        [Display(Name = "Wdrink_13")]
        public string Wdrink_13 { get; set; }
        [Display(Name = "Desl_13")]
        public string Desl_13 { get; set; }
        [Display(Name = "RestRoomFlg_13")]
        public string RestRoomFlg_13 { get; set; }
        [Display(Name = "AnketFlg_13")]
        public string AnketFlg_13 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_13")]
        public string PartyTime_TimeName_I_13 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_13")]
        public string PartyTime_OrderTime_I_13 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_13")]
        public string PartyTime_ActTime_I_13 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_13")]
        public string PartyTime_DelayTime_I_13 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_13")]
        public string PartyTime_TimeName_II_13 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_13")]
        public string PartyTime_OrderTime_II_13 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_13")]
        public string PartyTime_ActTime_II_13 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_13")]
        public string PartyTime_DelayTime_II_13 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_13")]
        public string PartyTime_TimeName_III_13 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_13")]
        public string PartyTime_OrderTime_III_13 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_13")]
        public string PartyTime_ActTime_III_13 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_13")]
        public string PartyTime_DelayTime_III_13 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_13")]
        public string PartyTime_TimeName_IV_13 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_13")]
        public string PartyTime_OrderTime_IV_13 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_13")]
        public string PartyTime_ActTime_IV_13 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_13")]
        public string PartyTime_DelayTime_IV_13 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_13")]
        public string PartyTime_TimeName_V_13 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_13")]
        public string PartyTime_OrderTime_V_13 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_13")]
        public string PartyTime_ActTime_V_13 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_13")]
        public string PartyTime_DelayTime_V_13 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_13")]
        public string PartyFood_FoodName_I_13 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_13")]
        public string PartyFood_BeginTime_I_13 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_13")]
        public string PartyFood_EndTime_I_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_13")]
        public string PartyFood_RestRoomTime_I_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_13")]
        public string PartyFood_RestRoomFlg_I_13 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_13")]
        public string PartyFood_FoodName_II_13 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_13")]
        public string PartyFood_BeginTime_II_13 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_13")]
        public string PartyFood_EndTime_II_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_13")]
        public string PartyFood_RestRoomTime_II_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_13")]
        public string PartyFood_RestRoomFlg_II_13 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_13")]
        public string PartyFood_FoodName_III_13 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_13")]
        public string PartyFood_BeginTime_III_13 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_13")]
        public string PartyFood_EndTime_III_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_13")]
        public string PartyFood_RestRoomTime_III_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_13")]
        public string PartyFood_RestRoomFlg_III_13 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_13")]
        public string PartyFood_FoodName_IV_13 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_13")]
        public string PartyFood_BeginTime_IV_13 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_13")]
        public string PartyFood_EndTime_IV_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_13")]
        public string PartyFood_RestRoomTime_IV_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_13")]
        public string PartyFood_RestRoomFlg_IV_13 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_13")]
        public string PartyFood_FoodName_V_13 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_13")]
        public string PartyFood_BeginTime_V_13 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_13")]
        public string PartyFood_EndTime_V_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_13")]
        public string PartyFood_RestRoomTime_V_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_13")]
        public string PartyFood_RestRoomFlg_V_13 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_13")]
        public string PartyFood_FoodName_VI_13 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_13")]
        public string PartyFood_BeginTime_VI_13 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_13")]
        public string PartyFood_EndTime_VI_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_13")]
        public string PartyFood_RestRoomTime_VI_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_13")]
        public string PartyFood_RestRoomFlg_VI_13 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_13")]
        public string PartyFood_FoodName_VII_13 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_13")]
        public string PartyFood_BeginTime_VII_13 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_13")]
        public string PartyFood_EndTime_VII_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_13")]
        public string PartyFood_RestRoomTime_VII_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_13")]
        public string PartyFood_RestRoomFlg_VII_13 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_13")]
        public string PartyFood_FoodName_VIII_13 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_13")]
        public string PartyFood_BeginTime_VIII_13 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_13")]
        public string PartyFood_EndTime_VIII_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_13")]
        public string PartyFood_RestRoomTime_VIII_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_13")]
        public string PartyFood_RestRoomFlg_VIII_13 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_13")]
        public string PartyFood_FoodName_IX_13 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_13")]
        public string PartyFood_BeginTime_IX_13 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_13")]
        public string PartyFood_EndTime_IX_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_13")]
        public string PartyFood_RestRoomTime_IX_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_13")]
        public string PartyFood_RestRoomFlg_IX_13 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_13")]
        public string PartyFood_FoodName_X_13 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_13")]
        public string PartyFood_BeginTime_X_13 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_13")]
        public string PartyFood_EndTime_X_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_13")]
        public string PartyFood_RestRoomTime_X_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_13")]
        public string PartyFood_RestRoomFlg_X_13 { get; set; }
        [Display(Name = "PartyMember_Name_I_13")]
        public string PartyMember_Name_I_13 { get; set; }
        [Display(Name = "PartyMember_Name_II_13")]
        public string PartyMember_Name_II_13 { get; set; }
        [Display(Name = "PartyMember_Name_III_13")]
        public string PartyMember_Name_III_13 { get; set; }
        [Display(Name = "PartyMember_Name_IV_13")]
        public string PartyMember_Name_IV_13 { get; set; }
        [Display(Name = "PartyMember_Name_V_13")]
        public string PartyMember_Name_V_13 { get; set; }
        [Display(Name = "PartyMember_Name_VI_13")]
        public string PartyMember_Name_VI_13 { get; set; }
        [Display(Name = "PartyMember_Name_VII_13")]
        public string PartyMember_Name_VII_13 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_13")]
        public string PartyMember_Name_VIII_13 { get; set; }
        [Display(Name = "PartyMember_Name_IX_13")]
        public string PartyMember_Name_IX_13 { get; set; }
        [Display(Name = "PartyDate_14")]
        public string PartyDate_14 { get; set; }
        [Display(Name = "BrideFamilyName_14")]
        public string BrideFamilyName_14 { get; set; }
        [Display(Name = "GroomFamilyName_14")]
        public string GroomFamilyName_14 { get; set; }
        [Display(Name = "TantoName_14")]
        public string TantoName_14 { get; set; }
        [Display(Name = "ReporterName_14")]
        public string ReporterName_14 { get; set; }
        [Display(Name = "AdultCnt_14")]
        public string AdultCnt_14 { get; set; }
        [Display(Name = "HalfCnt_14")]
        public string HalfCnt_14 { get; set; }
        [Display(Name = "ChildrenCnt_14")]
        public string ChildrenCnt_14 { get; set; }
        [Display(Name = "SeatOnlyCnt_14")]
        public string SeatOnlyCnt_14 { get; set; }
        [Display(Name = "TableCnt_14")]
        public string TableCnt_14 { get; set; }
        [Display(Name = "TableCross_14")]
        public string TableCross_14 { get; set; }
        [Display(Name = "PartyStyleName_14")]
        public string PartyStyleName_14 { get; set; }
        [Display(Name = "FoodStyleName_14")]
        public string FoodStyleName_14 { get; set; }
        [Display(Name = "FoodPricce_14")]
        public string FoodPricce_14 { get; set; }
        [Display(Name = "DrinkPrice_14")]
        public string DrinkPrice_14 { get; set; }
        [Display(Name = "Wdrink_14")]
        public string Wdrink_14 { get; set; }
        [Display(Name = "Desl_14")]
        public string Desl_14 { get; set; }
        [Display(Name = "RestRoomFlg_14")]
        public string RestRoomFlg_14 { get; set; }
        [Display(Name = "AnketFlg_14")]
        public string AnketFlg_14 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_14")]
        public string PartyTime_TimeName_I_14 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_14")]
        public string PartyTime_OrderTime_I_14 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_14")]
        public string PartyTime_ActTime_I_14 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_14")]
        public string PartyTime_DelayTime_I_14 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_14")]
        public string PartyTime_TimeName_II_14 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_14")]
        public string PartyTime_OrderTime_II_14 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_14")]
        public string PartyTime_ActTime_II_14 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_14")]
        public string PartyTime_DelayTime_II_14 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_14")]
        public string PartyTime_TimeName_III_14 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_14")]
        public string PartyTime_OrderTime_III_14 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_14")]
        public string PartyTime_ActTime_III_14 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_14")]
        public string PartyTime_DelayTime_III_14 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_14")]
        public string PartyTime_TimeName_IV_14 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_14")]
        public string PartyTime_OrderTime_IV_14 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_14")]
        public string PartyTime_ActTime_IV_14 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_14")]
        public string PartyTime_DelayTime_IV_14 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_14")]
        public string PartyTime_TimeName_V_14 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_14")]
        public string PartyTime_OrderTime_V_14 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_14")]
        public string PartyTime_ActTime_V_14 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_14")]
        public string PartyTime_DelayTime_V_14 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_14")]
        public string PartyFood_FoodName_I_14 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_14")]
        public string PartyFood_BeginTime_I_14 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_14")]
        public string PartyFood_EndTime_I_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_14")]
        public string PartyFood_RestRoomTime_I_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_14")]
        public string PartyFood_RestRoomFlg_I_14 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_14")]
        public string PartyFood_FoodName_II_14 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_14")]
        public string PartyFood_BeginTime_II_14 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_14")]
        public string PartyFood_EndTime_II_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_14")]
        public string PartyFood_RestRoomTime_II_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_14")]
        public string PartyFood_RestRoomFlg_II_14 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_14")]
        public string PartyFood_FoodName_III_14 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_14")]
        public string PartyFood_BeginTime_III_14 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_14")]
        public string PartyFood_EndTime_III_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_14")]
        public string PartyFood_RestRoomTime_III_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_14")]
        public string PartyFood_RestRoomFlg_III_14 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_14")]
        public string PartyFood_FoodName_IV_14 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_14")]
        public string PartyFood_BeginTime_IV_14 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_14")]
        public string PartyFood_EndTime_IV_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_14")]
        public string PartyFood_RestRoomTime_IV_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_14")]
        public string PartyFood_RestRoomFlg_IV_14 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_14")]
        public string PartyFood_FoodName_V_14 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_14")]
        public string PartyFood_BeginTime_V_14 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_14")]
        public string PartyFood_EndTime_V_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_14")]
        public string PartyFood_RestRoomTime_V_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_14")]
        public string PartyFood_RestRoomFlg_V_14 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_14")]
        public string PartyFood_FoodName_VI_14 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_14")]
        public string PartyFood_BeginTime_VI_14 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_14")]
        public string PartyFood_EndTime_VI_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_14")]
        public string PartyFood_RestRoomTime_VI_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_14")]
        public string PartyFood_RestRoomFlg_VI_14 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_14")]
        public string PartyFood_FoodName_VII_14 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_14")]
        public string PartyFood_BeginTime_VII_14 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_14")]
        public string PartyFood_EndTime_VII_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_14")]
        public string PartyFood_RestRoomTime_VII_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_14")]
        public string PartyFood_RestRoomFlg_VII_14 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_14")]
        public string PartyFood_FoodName_VIII_14 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_14")]
        public string PartyFood_BeginTime_VIII_14 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_14")]
        public string PartyFood_EndTime_VIII_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_14")]
        public string PartyFood_RestRoomTime_VIII_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_14")]
        public string PartyFood_RestRoomFlg_VIII_14 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_14")]
        public string PartyFood_FoodName_IX_14 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_14")]
        public string PartyFood_BeginTime_IX_14 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_14")]
        public string PartyFood_EndTime_IX_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_14")]
        public string PartyFood_RestRoomTime_IX_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_14")]
        public string PartyFood_RestRoomFlg_IX_14 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_14")]
        public string PartyFood_FoodName_X_14 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_14")]
        public string PartyFood_BeginTime_X_14 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_14")]
        public string PartyFood_EndTime_X_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_14")]
        public string PartyFood_RestRoomTime_X_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_14")]
        public string PartyFood_RestRoomFlg_X_14 { get; set; }
        [Display(Name = "PartyMember_Name_I_14")]
        public string PartyMember_Name_I_14 { get; set; }
        [Display(Name = "PartyMember_Name_II_14")]
        public string PartyMember_Name_II_14 { get; set; }
        [Display(Name = "PartyMember_Name_III_14")]
        public string PartyMember_Name_III_14 { get; set; }
        [Display(Name = "PartyMember_Name_IV_14")]
        public string PartyMember_Name_IV_14 { get; set; }
        [Display(Name = "PartyMember_Name_V_14")]
        public string PartyMember_Name_V_14 { get; set; }
        [Display(Name = "PartyMember_Name_VI_14")]
        public string PartyMember_Name_VI_14 { get; set; }
        [Display(Name = "PartyMember_Name_VII_14")]
        public string PartyMember_Name_VII_14 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_14")]
        public string PartyMember_Name_VIII_14 { get; set; }
        [Display(Name = "PartyMember_Name_IX_14")]
        public string PartyMember_Name_IX_14 { get; set; }
        [Display(Name = "PartyDate_15")]
        public string PartyDate_15 { get; set; }
        [Display(Name = "BrideFamilyName_15")]
        public string BrideFamilyName_15 { get; set; }
        [Display(Name = "GroomFamilyName_15")]
        public string GroomFamilyName_15 { get; set; }
        [Display(Name = "TantoName_15")]
        public string TantoName_15 { get; set; }
        [Display(Name = "ReporterName_15")]
        public string ReporterName_15 { get; set; }
        [Display(Name = "AdultCnt_15")]
        public string AdultCnt_15 { get; set; }
        [Display(Name = "HalfCnt_15")]
        public string HalfCnt_15 { get; set; }
        [Display(Name = "ChildrenCnt_15")]
        public string ChildrenCnt_15 { get; set; }
        [Display(Name = "SeatOnlyCnt_15")]
        public string SeatOnlyCnt_15 { get; set; }
        [Display(Name = "TableCnt_15")]
        public string TableCnt_15 { get; set; }
        [Display(Name = "TableCross_15")]
        public string TableCross_15 { get; set; }
        [Display(Name = "PartyStyleName_15")]
        public string PartyStyleName_15 { get; set; }
        [Display(Name = "FoodStyleName_15")]
        public string FoodStyleName_15 { get; set; }
        [Display(Name = "FoodPricce_15")]
        public string FoodPricce_15 { get; set; }
        [Display(Name = "DrinkPrice_15")]
        public string DrinkPrice_15 { get; set; }
        [Display(Name = "Wdrink_15")]
        public string Wdrink_15 { get; set; }
        [Display(Name = "Desl_15")]
        public string Desl_15 { get; set; }
        [Display(Name = "RestRoomFlg_15")]
        public string RestRoomFlg_15 { get; set; }
        [Display(Name = "AnketFlg_15")]
        public string AnketFlg_15 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_15")]
        public string PartyTime_TimeName_I_15 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_15")]
        public string PartyTime_OrderTime_I_15 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_15")]
        public string PartyTime_ActTime_I_15 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_15")]
        public string PartyTime_DelayTime_I_15 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_15")]
        public string PartyTime_TimeName_II_15 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_15")]
        public string PartyTime_OrderTime_II_15 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_15")]
        public string PartyTime_ActTime_II_15 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_15")]
        public string PartyTime_DelayTime_II_15 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_15")]
        public string PartyTime_TimeName_III_15 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_15")]
        public string PartyTime_OrderTime_III_15 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_15")]
        public string PartyTime_ActTime_III_15 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_15")]
        public string PartyTime_DelayTime_III_15 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_15")]
        public string PartyTime_TimeName_IV_15 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_15")]
        public string PartyTime_OrderTime_IV_15 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_15")]
        public string PartyTime_ActTime_IV_15 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_15")]
        public string PartyTime_DelayTime_IV_15 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_15")]
        public string PartyTime_TimeName_V_15 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_15")]
        public string PartyTime_OrderTime_V_15 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_15")]
        public string PartyTime_ActTime_V_15 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_15")]
        public string PartyTime_DelayTime_V_15 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_15")]
        public string PartyFood_FoodName_I_15 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_15")]
        public string PartyFood_BeginTime_I_15 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_15")]
        public string PartyFood_EndTime_I_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_15")]
        public string PartyFood_RestRoomTime_I_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_15")]
        public string PartyFood_RestRoomFlg_I_15 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_15")]
        public string PartyFood_FoodName_II_15 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_15")]
        public string PartyFood_BeginTime_II_15 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_15")]
        public string PartyFood_EndTime_II_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_15")]
        public string PartyFood_RestRoomTime_II_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_15")]
        public string PartyFood_RestRoomFlg_II_15 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_15")]
        public string PartyFood_FoodName_III_15 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_15")]
        public string PartyFood_BeginTime_III_15 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_15")]
        public string PartyFood_EndTime_III_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_15")]
        public string PartyFood_RestRoomTime_III_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_15")]
        public string PartyFood_RestRoomFlg_III_15 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_15")]
        public string PartyFood_FoodName_IV_15 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_15")]
        public string PartyFood_BeginTime_IV_15 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_15")]
        public string PartyFood_EndTime_IV_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_15")]
        public string PartyFood_RestRoomTime_IV_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_15")]
        public string PartyFood_RestRoomFlg_IV_15 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_15")]
        public string PartyFood_FoodName_V_15 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_15")]
        public string PartyFood_BeginTime_V_15 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_15")]
        public string PartyFood_EndTime_V_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_15")]
        public string PartyFood_RestRoomTime_V_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_15")]
        public string PartyFood_RestRoomFlg_V_15 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_15")]
        public string PartyFood_FoodName_VI_15 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_15")]
        public string PartyFood_BeginTime_VI_15 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_15")]
        public string PartyFood_EndTime_VI_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_15")]
        public string PartyFood_RestRoomTime_VI_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_15")]
        public string PartyFood_RestRoomFlg_VI_15 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_15")]
        public string PartyFood_FoodName_VII_15 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_15")]
        public string PartyFood_BeginTime_VII_15 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_15")]
        public string PartyFood_EndTime_VII_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_15")]
        public string PartyFood_RestRoomTime_VII_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_15")]
        public string PartyFood_RestRoomFlg_VII_15 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_15")]
        public string PartyFood_FoodName_VIII_15 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_15")]
        public string PartyFood_BeginTime_VIII_15 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_15")]
        public string PartyFood_EndTime_VIII_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_15")]
        public string PartyFood_RestRoomTime_VIII_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_15")]
        public string PartyFood_RestRoomFlg_VIII_15 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_15")]
        public string PartyFood_FoodName_IX_15 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_15")]
        public string PartyFood_BeginTime_IX_15 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_15")]
        public string PartyFood_EndTime_IX_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_15")]
        public string PartyFood_RestRoomTime_IX_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_15")]
        public string PartyFood_RestRoomFlg_IX_15 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_15")]
        public string PartyFood_FoodName_X_15 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_15")]
        public string PartyFood_BeginTime_X_15 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_15")]
        public string PartyFood_EndTime_X_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_15")]
        public string PartyFood_RestRoomTime_X_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_15")]
        public string PartyFood_RestRoomFlg_X_15 { get; set; }
        [Display(Name = "PartyMember_Name_I_15")]
        public string PartyMember_Name_I_15 { get; set; }
        [Display(Name = "PartyMember_Name_II_15")]
        public string PartyMember_Name_II_15 { get; set; }
        [Display(Name = "PartyMember_Name_III_15")]
        public string PartyMember_Name_III_15 { get; set; }
        [Display(Name = "PartyMember_Name_IV_15")]
        public string PartyMember_Name_IV_15 { get; set; }
        [Display(Name = "PartyMember_Name_V_15")]
        public string PartyMember_Name_V_15 { get; set; }
        [Display(Name = "PartyMember_Name_VI_15")]
        public string PartyMember_Name_VI_15 { get; set; }
        [Display(Name = "PartyMember_Name_VII_15")]
        public string PartyMember_Name_VII_15 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_15")]
        public string PartyMember_Name_VIII_15 { get; set; }
        [Display(Name = "PartyMember_Name_IX_15")]
        public string PartyMember_Name_IX_15 { get; set; }
        [Display(Name = "PartyDate_16")]
        public string PartyDate_16 { get; set; }
        [Display(Name = "BrideFamilyName_16")]
        public string BrideFamilyName_16 { get; set; }
        [Display(Name = "GroomFamilyName_16")]
        public string GroomFamilyName_16 { get; set; }
        [Display(Name = "TantoName_16")]
        public string TantoName_16 { get; set; }
        [Display(Name = "ReporterName_16")]
        public string ReporterName_16 { get; set; }
        [Display(Name = "AdultCnt_16")]
        public string AdultCnt_16 { get; set; }
        [Display(Name = "HalfCnt_16")]
        public string HalfCnt_16 { get; set; }
        [Display(Name = "ChildrenCnt_16")]
        public string ChildrenCnt_16 { get; set; }
        [Display(Name = "SeatOnlyCnt_16")]
        public string SeatOnlyCnt_16 { get; set; }
        [Display(Name = "TableCnt_16")]
        public string TableCnt_16 { get; set; }
        [Display(Name = "TableCross_16")]
        public string TableCross_16 { get; set; }
        [Display(Name = "PartyStyleName_16")]
        public string PartyStyleName_16 { get; set; }
        [Display(Name = "FoodStyleName_16")]
        public string FoodStyleName_16 { get; set; }
        [Display(Name = "FoodPricce_16")]
        public string FoodPricce_16 { get; set; }
        [Display(Name = "DrinkPrice_16")]
        public string DrinkPrice_16 { get; set; }
        [Display(Name = "Wdrink_16")]
        public string Wdrink_16 { get; set; }
        [Display(Name = "Desl_16")]
        public string Desl_16 { get; set; }
        [Display(Name = "RestRoomFlg_16")]
        public string RestRoomFlg_16 { get; set; }
        [Display(Name = "AnketFlg_16")]
        public string AnketFlg_16 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_16")]
        public string PartyTime_TimeName_I_16 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_16")]
        public string PartyTime_OrderTime_I_16 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_16")]
        public string PartyTime_ActTime_I_16 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_16")]
        public string PartyTime_DelayTime_I_16 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_16")]
        public string PartyTime_TimeName_II_16 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_16")]
        public string PartyTime_OrderTime_II_16 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_16")]
        public string PartyTime_ActTime_II_16 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_16")]
        public string PartyTime_DelayTime_II_16 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_16")]
        public string PartyTime_TimeName_III_16 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_16")]
        public string PartyTime_OrderTime_III_16 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_16")]
        public string PartyTime_ActTime_III_16 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_16")]
        public string PartyTime_DelayTime_III_16 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_16")]
        public string PartyTime_TimeName_IV_16 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_16")]
        public string PartyTime_OrderTime_IV_16 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_16")]
        public string PartyTime_ActTime_IV_16 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_16")]
        public string PartyTime_DelayTime_IV_16 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_16")]
        public string PartyTime_TimeName_V_16 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_16")]
        public string PartyTime_OrderTime_V_16 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_16")]
        public string PartyTime_ActTime_V_16 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_16")]
        public string PartyTime_DelayTime_V_16 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_16")]
        public string PartyFood_FoodName_I_16 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_16")]
        public string PartyFood_BeginTime_I_16 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_16")]
        public string PartyFood_EndTime_I_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_16")]
        public string PartyFood_RestRoomTime_I_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_16")]
        public string PartyFood_RestRoomFlg_I_16 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_16")]
        public string PartyFood_FoodName_II_16 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_16")]
        public string PartyFood_BeginTime_II_16 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_16")]
        public string PartyFood_EndTime_II_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_16")]
        public string PartyFood_RestRoomTime_II_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_16")]
        public string PartyFood_RestRoomFlg_II_16 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_16")]
        public string PartyFood_FoodName_III_16 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_16")]
        public string PartyFood_BeginTime_III_16 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_16")]
        public string PartyFood_EndTime_III_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_16")]
        public string PartyFood_RestRoomTime_III_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_16")]
        public string PartyFood_RestRoomFlg_III_16 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_16")]
        public string PartyFood_FoodName_IV_16 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_16")]
        public string PartyFood_BeginTime_IV_16 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_16")]
        public string PartyFood_EndTime_IV_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_16")]
        public string PartyFood_RestRoomTime_IV_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_16")]
        public string PartyFood_RestRoomFlg_IV_16 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_16")]
        public string PartyFood_FoodName_V_16 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_16")]
        public string PartyFood_BeginTime_V_16 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_16")]
        public string PartyFood_EndTime_V_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_16")]
        public string PartyFood_RestRoomTime_V_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_16")]
        public string PartyFood_RestRoomFlg_V_16 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_16")]
        public string PartyFood_FoodName_VI_16 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_16")]
        public string PartyFood_BeginTime_VI_16 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_16")]
        public string PartyFood_EndTime_VI_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_16")]
        public string PartyFood_RestRoomTime_VI_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_16")]
        public string PartyFood_RestRoomFlg_VI_16 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_16")]
        public string PartyFood_FoodName_VII_16 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_16")]
        public string PartyFood_BeginTime_VII_16 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_16")]
        public string PartyFood_EndTime_VII_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_16")]
        public string PartyFood_RestRoomTime_VII_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_16")]
        public string PartyFood_RestRoomFlg_VII_16 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_16")]
        public string PartyFood_FoodName_VIII_16 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_16")]
        public string PartyFood_BeginTime_VIII_16 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_16")]
        public string PartyFood_EndTime_VIII_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_16")]
        public string PartyFood_RestRoomTime_VIII_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_16")]
        public string PartyFood_RestRoomFlg_VIII_16 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_16")]
        public string PartyFood_FoodName_IX_16 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_16")]
        public string PartyFood_BeginTime_IX_16 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_16")]
        public string PartyFood_EndTime_IX_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_16")]
        public string PartyFood_RestRoomTime_IX_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_16")]
        public string PartyFood_RestRoomFlg_IX_16 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_16")]
        public string PartyFood_FoodName_X_16 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_16")]
        public string PartyFood_BeginTime_X_16 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_16")]
        public string PartyFood_EndTime_X_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_16")]
        public string PartyFood_RestRoomTime_X_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_16")]
        public string PartyFood_RestRoomFlg_X_16 { get; set; }
        [Display(Name = "PartyMember_Name_I_16")]
        public string PartyMember_Name_I_16 { get; set; }
        [Display(Name = "PartyMember_Name_II_16")]
        public string PartyMember_Name_II_16 { get; set; }
        [Display(Name = "PartyMember_Name_III_16")]
        public string PartyMember_Name_III_16 { get; set; }
        [Display(Name = "PartyMember_Name_IV_16")]
        public string PartyMember_Name_IV_16 { get; set; }
        [Display(Name = "PartyMember_Name_V_16")]
        public string PartyMember_Name_V_16 { get; set; }
        [Display(Name = "PartyMember_Name_VI_16")]
        public string PartyMember_Name_VI_16 { get; set; }
        [Display(Name = "PartyMember_Name_VII_16")]
        public string PartyMember_Name_VII_16 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_16")]
        public string PartyMember_Name_VIII_16 { get; set; }
        [Display(Name = "PartyMember_Name_IX_16")]
        public string PartyMember_Name_IX_16 { get; set; }
        [Display(Name = "PartyDate_17")]
        public string PartyDate_17 { get; set; }
        [Display(Name = "BrideFamilyName_17")]
        public string BrideFamilyName_17 { get; set; }
        [Display(Name = "GroomFamilyName_17")]
        public string GroomFamilyName_17 { get; set; }
        [Display(Name = "TantoName_17")]
        public string TantoName_17 { get; set; }
        [Display(Name = "ReporterName_17")]
        public string ReporterName_17 { get; set; }
        [Display(Name = "AdultCnt_17")]
        public string AdultCnt_17 { get; set; }
        [Display(Name = "HalfCnt_17")]
        public string HalfCnt_17 { get; set; }
        [Display(Name = "ChildrenCnt_17")]
        public string ChildrenCnt_17 { get; set; }
        [Display(Name = "SeatOnlyCnt_17")]
        public string SeatOnlyCnt_17 { get; set; }
        [Display(Name = "TableCnt_17")]
        public string TableCnt_17 { get; set; }
        [Display(Name = "TableCross_17")]
        public string TableCross_17 { get; set; }
        [Display(Name = "PartyStyleName_17")]
        public string PartyStyleName_17 { get; set; }
        [Display(Name = "FoodStyleName_17")]
        public string FoodStyleName_17 { get; set; }
        [Display(Name = "FoodPricce_17")]
        public string FoodPricce_17 { get; set; }
        [Display(Name = "DrinkPrice_17")]
        public string DrinkPrice_17 { get; set; }
        [Display(Name = "Wdrink_17")]
        public string Wdrink_17 { get; set; }
        [Display(Name = "Desl_17")]
        public string Desl_17 { get; set; }
        [Display(Name = "RestRoomFlg_17")]
        public string RestRoomFlg_17 { get; set; }
        [Display(Name = "AnketFlg_17")]
        public string AnketFlg_17 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_17")]
        public string PartyTime_TimeName_I_17 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_17")]
        public string PartyTime_OrderTime_I_17 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_17")]
        public string PartyTime_ActTime_I_17 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_17")]
        public string PartyTime_DelayTime_I_17 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_17")]
        public string PartyTime_TimeName_II_17 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_17")]
        public string PartyTime_OrderTime_II_17 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_17")]
        public string PartyTime_ActTime_II_17 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_17")]
        public string PartyTime_DelayTime_II_17 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_17")]
        public string PartyTime_TimeName_III_17 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_17")]
        public string PartyTime_OrderTime_III_17 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_17")]
        public string PartyTime_ActTime_III_17 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_17")]
        public string PartyTime_DelayTime_III_17 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_17")]
        public string PartyTime_TimeName_IV_17 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_17")]
        public string PartyTime_OrderTime_IV_17 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_17")]
        public string PartyTime_ActTime_IV_17 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_17")]
        public string PartyTime_DelayTime_IV_17 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_17")]
        public string PartyTime_TimeName_V_17 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_17")]
        public string PartyTime_OrderTime_V_17 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_17")]
        public string PartyTime_ActTime_V_17 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_17")]
        public string PartyTime_DelayTime_V_17 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_17")]
        public string PartyFood_FoodName_I_17 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_17")]
        public string PartyFood_BeginTime_I_17 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_17")]
        public string PartyFood_EndTime_I_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_17")]
        public string PartyFood_RestRoomTime_I_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_17")]
        public string PartyFood_RestRoomFlg_I_17 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_17")]
        public string PartyFood_FoodName_II_17 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_17")]
        public string PartyFood_BeginTime_II_17 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_17")]
        public string PartyFood_EndTime_II_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_17")]
        public string PartyFood_RestRoomTime_II_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_17")]
        public string PartyFood_RestRoomFlg_II_17 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_17")]
        public string PartyFood_FoodName_III_17 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_17")]
        public string PartyFood_BeginTime_III_17 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_17")]
        public string PartyFood_EndTime_III_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_17")]
        public string PartyFood_RestRoomTime_III_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_17")]
        public string PartyFood_RestRoomFlg_III_17 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_17")]
        public string PartyFood_FoodName_IV_17 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_17")]
        public string PartyFood_BeginTime_IV_17 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_17")]
        public string PartyFood_EndTime_IV_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_17")]
        public string PartyFood_RestRoomTime_IV_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_17")]
        public string PartyFood_RestRoomFlg_IV_17 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_17")]
        public string PartyFood_FoodName_V_17 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_17")]
        public string PartyFood_BeginTime_V_17 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_17")]
        public string PartyFood_EndTime_V_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_17")]
        public string PartyFood_RestRoomTime_V_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_17")]
        public string PartyFood_RestRoomFlg_V_17 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_17")]
        public string PartyFood_FoodName_VI_17 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_17")]
        public string PartyFood_BeginTime_VI_17 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_17")]
        public string PartyFood_EndTime_VI_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_17")]
        public string PartyFood_RestRoomTime_VI_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_17")]
        public string PartyFood_RestRoomFlg_VI_17 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_17")]
        public string PartyFood_FoodName_VII_17 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_17")]
        public string PartyFood_BeginTime_VII_17 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_17")]
        public string PartyFood_EndTime_VII_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_17")]
        public string PartyFood_RestRoomTime_VII_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_17")]
        public string PartyFood_RestRoomFlg_VII_17 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_17")]
        public string PartyFood_FoodName_VIII_17 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_17")]
        public string PartyFood_BeginTime_VIII_17 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_17")]
        public string PartyFood_EndTime_VIII_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_17")]
        public string PartyFood_RestRoomTime_VIII_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_17")]
        public string PartyFood_RestRoomFlg_VIII_17 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_17")]
        public string PartyFood_FoodName_IX_17 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_17")]
        public string PartyFood_BeginTime_IX_17 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_17")]
        public string PartyFood_EndTime_IX_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_17")]
        public string PartyFood_RestRoomTime_IX_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_17")]
        public string PartyFood_RestRoomFlg_IX_17 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_17")]
        public string PartyFood_FoodName_X_17 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_17")]
        public string PartyFood_BeginTime_X_17 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_17")]
        public string PartyFood_EndTime_X_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_17")]
        public string PartyFood_RestRoomTime_X_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_17")]
        public string PartyFood_RestRoomFlg_X_17 { get; set; }
        [Display(Name = "PartyMember_Name_I_17")]
        public string PartyMember_Name_I_17 { get; set; }
        [Display(Name = "PartyMember_Name_II_17")]
        public string PartyMember_Name_II_17 { get; set; }
        [Display(Name = "PartyMember_Name_III_17")]
        public string PartyMember_Name_III_17 { get; set; }
        [Display(Name = "PartyMember_Name_IV_17")]
        public string PartyMember_Name_IV_17 { get; set; }
        [Display(Name = "PartyMember_Name_V_17")]
        public string PartyMember_Name_V_17 { get; set; }
        [Display(Name = "PartyMember_Name_VI_17")]
        public string PartyMember_Name_VI_17 { get; set; }
        [Display(Name = "PartyMember_Name_VII_17")]
        public string PartyMember_Name_VII_17 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_17")]
        public string PartyMember_Name_VIII_17 { get; set; }
        [Display(Name = "PartyMember_Name_IX_17")]
        public string PartyMember_Name_IX_17 { get; set; }
        [Display(Name = "PartyDate_18")]
        public string PartyDate_18 { get; set; }
        [Display(Name = "BrideFamilyName_18")]
        public string BrideFamilyName_18 { get; set; }
        [Display(Name = "GroomFamilyName_18")]
        public string GroomFamilyName_18 { get; set; }
        [Display(Name = "TantoName_18")]
        public string TantoName_18 { get; set; }
        [Display(Name = "ReporterName_18")]
        public string ReporterName_18 { get; set; }
        [Display(Name = "AdultCnt_18")]
        public string AdultCnt_18 { get; set; }
        [Display(Name = "HalfCnt_18")]
        public string HalfCnt_18 { get; set; }
        [Display(Name = "ChildrenCnt_18")]
        public string ChildrenCnt_18 { get; set; }
        [Display(Name = "SeatOnlyCnt_18")]
        public string SeatOnlyCnt_18 { get; set; }
        [Display(Name = "TableCnt_18")]
        public string TableCnt_18 { get; set; }
        [Display(Name = "TableCross_18")]
        public string TableCross_18 { get; set; }
        [Display(Name = "PartyStyleName_18")]
        public string PartyStyleName_18 { get; set; }
        [Display(Name = "FoodStyleName_18")]
        public string FoodStyleName_18 { get; set; }
        [Display(Name = "FoodPricce_18")]
        public string FoodPricce_18 { get; set; }
        [Display(Name = "DrinkPrice_18")]
        public string DrinkPrice_18 { get; set; }
        [Display(Name = "Wdrink_18")]
        public string Wdrink_18 { get; set; }
        [Display(Name = "Desl_18")]
        public string Desl_18 { get; set; }
        [Display(Name = "RestRoomFlg_18")]
        public string RestRoomFlg_18 { get; set; }
        [Display(Name = "AnketFlg_18")]
        public string AnketFlg_18 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_18")]
        public string PartyTime_TimeName_I_18 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_18")]
        public string PartyTime_OrderTime_I_18 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_18")]
        public string PartyTime_ActTime_I_18 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_18")]
        public string PartyTime_DelayTime_I_18 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_18")]
        public string PartyTime_TimeName_II_18 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_18")]
        public string PartyTime_OrderTime_II_18 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_18")]
        public string PartyTime_ActTime_II_18 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_18")]
        public string PartyTime_DelayTime_II_18 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_18")]
        public string PartyTime_TimeName_III_18 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_18")]
        public string PartyTime_OrderTime_III_18 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_18")]
        public string PartyTime_ActTime_III_18 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_18")]
        public string PartyTime_DelayTime_III_18 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_18")]
        public string PartyTime_TimeName_IV_18 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_18")]
        public string PartyTime_OrderTime_IV_18 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_18")]
        public string PartyTime_ActTime_IV_18 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_18")]
        public string PartyTime_DelayTime_IV_18 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_18")]
        public string PartyTime_TimeName_V_18 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_18")]
        public string PartyTime_OrderTime_V_18 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_18")]
        public string PartyTime_ActTime_V_18 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_18")]
        public string PartyTime_DelayTime_V_18 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_18")]
        public string PartyFood_FoodName_I_18 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_18")]
        public string PartyFood_BeginTime_I_18 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_18")]
        public string PartyFood_EndTime_I_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_18")]
        public string PartyFood_RestRoomTime_I_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_18")]
        public string PartyFood_RestRoomFlg_I_18 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_18")]
        public string PartyFood_FoodName_II_18 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_18")]
        public string PartyFood_BeginTime_II_18 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_18")]
        public string PartyFood_EndTime_II_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_18")]
        public string PartyFood_RestRoomTime_II_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_18")]
        public string PartyFood_RestRoomFlg_II_18 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_18")]
        public string PartyFood_FoodName_III_18 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_18")]
        public string PartyFood_BeginTime_III_18 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_18")]
        public string PartyFood_EndTime_III_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_18")]
        public string PartyFood_RestRoomTime_III_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_18")]
        public string PartyFood_RestRoomFlg_III_18 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_18")]
        public string PartyFood_FoodName_IV_18 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_18")]
        public string PartyFood_BeginTime_IV_18 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_18")]
        public string PartyFood_EndTime_IV_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_18")]
        public string PartyFood_RestRoomTime_IV_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_18")]
        public string PartyFood_RestRoomFlg_IV_18 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_18")]
        public string PartyFood_FoodName_V_18 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_18")]
        public string PartyFood_BeginTime_V_18 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_18")]
        public string PartyFood_EndTime_V_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_18")]
        public string PartyFood_RestRoomTime_V_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_18")]
        public string PartyFood_RestRoomFlg_V_18 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_18")]
        public string PartyFood_FoodName_VI_18 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_18")]
        public string PartyFood_BeginTime_VI_18 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_18")]
        public string PartyFood_EndTime_VI_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_18")]
        public string PartyFood_RestRoomTime_VI_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_18")]
        public string PartyFood_RestRoomFlg_VI_18 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_18")]
        public string PartyFood_FoodName_VII_18 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_18")]
        public string PartyFood_BeginTime_VII_18 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_18")]
        public string PartyFood_EndTime_VII_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_18")]
        public string PartyFood_RestRoomTime_VII_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_18")]
        public string PartyFood_RestRoomFlg_VII_18 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_18")]
        public string PartyFood_FoodName_VIII_18 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_18")]
        public string PartyFood_BeginTime_VIII_18 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_18")]
        public string PartyFood_EndTime_VIII_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_18")]
        public string PartyFood_RestRoomTime_VIII_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_18")]
        public string PartyFood_RestRoomFlg_VIII_18 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_18")]
        public string PartyFood_FoodName_IX_18 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_18")]
        public string PartyFood_BeginTime_IX_18 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_18")]
        public string PartyFood_EndTime_IX_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_18")]
        public string PartyFood_RestRoomTime_IX_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_18")]
        public string PartyFood_RestRoomFlg_IX_18 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_18")]
        public string PartyFood_FoodName_X_18 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_18")]
        public string PartyFood_BeginTime_X_18 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_18")]
        public string PartyFood_EndTime_X_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_18")]
        public string PartyFood_RestRoomTime_X_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_18")]
        public string PartyFood_RestRoomFlg_X_18 { get; set; }
        [Display(Name = "PartyMember_Name_I_18")]
        public string PartyMember_Name_I_18 { get; set; }
        [Display(Name = "PartyMember_Name_II_18")]
        public string PartyMember_Name_II_18 { get; set; }
        [Display(Name = "PartyMember_Name_III_18")]
        public string PartyMember_Name_III_18 { get; set; }
        [Display(Name = "PartyMember_Name_IV_18")]
        public string PartyMember_Name_IV_18 { get; set; }
        [Display(Name = "PartyMember_Name_V_18")]
        public string PartyMember_Name_V_18 { get; set; }
        [Display(Name = "PartyMember_Name_VI_18")]
        public string PartyMember_Name_VI_18 { get; set; }
        [Display(Name = "PartyMember_Name_VII_18")]
        public string PartyMember_Name_VII_18 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_18")]
        public string PartyMember_Name_VIII_18 { get; set; }
        [Display(Name = "PartyMember_Name_IX_18")]
        public string PartyMember_Name_IX_18 { get; set; }
        [Display(Name = "PartyDate_19")]
        public string PartyDate_19 { get; set; }
        [Display(Name = "BrideFamilyName_19")]
        public string BrideFamilyName_19 { get; set; }
        [Display(Name = "GroomFamilyName_19")]
        public string GroomFamilyName_19 { get; set; }
        [Display(Name = "TantoName_19")]
        public string TantoName_19 { get; set; }
        [Display(Name = "ReporterName_19")]
        public string ReporterName_19 { get; set; }
        [Display(Name = "AdultCnt_19")]
        public string AdultCnt_19 { get; set; }
        [Display(Name = "HalfCnt_19")]
        public string HalfCnt_19 { get; set; }
        [Display(Name = "ChildrenCnt_19")]
        public string ChildrenCnt_19 { get; set; }
        [Display(Name = "SeatOnlyCnt_19")]
        public string SeatOnlyCnt_19 { get; set; }
        [Display(Name = "TableCnt_19")]
        public string TableCnt_19 { get; set; }
        [Display(Name = "TableCross_19")]
        public string TableCross_19 { get; set; }
        [Display(Name = "PartyStyleName_19")]
        public string PartyStyleName_19 { get; set; }
        [Display(Name = "FoodStyleName_19")]
        public string FoodStyleName_19 { get; set; }
        [Display(Name = "FoodPricce_19")]
        public string FoodPricce_19 { get; set; }
        [Display(Name = "DrinkPrice_19")]
        public string DrinkPrice_19 { get; set; }
        [Display(Name = "Wdrink_19")]
        public string Wdrink_19 { get; set; }
        [Display(Name = "Desl_19")]
        public string Desl_19 { get; set; }
        [Display(Name = "RestRoomFlg_19")]
        public string RestRoomFlg_19 { get; set; }
        [Display(Name = "AnketFlg_19")]
        public string AnketFlg_19 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_19")]
        public string PartyTime_TimeName_I_19 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_19")]
        public string PartyTime_OrderTime_I_19 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_19")]
        public string PartyTime_ActTime_I_19 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_19")]
        public string PartyTime_DelayTime_I_19 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_19")]
        public string PartyTime_TimeName_II_19 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_19")]
        public string PartyTime_OrderTime_II_19 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_19")]
        public string PartyTime_ActTime_II_19 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_19")]
        public string PartyTime_DelayTime_II_19 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_19")]
        public string PartyTime_TimeName_III_19 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_19")]
        public string PartyTime_OrderTime_III_19 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_19")]
        public string PartyTime_ActTime_III_19 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_19")]
        public string PartyTime_DelayTime_III_19 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_19")]
        public string PartyTime_TimeName_IV_19 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_19")]
        public string PartyTime_OrderTime_IV_19 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_19")]
        public string PartyTime_ActTime_IV_19 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_19")]
        public string PartyTime_DelayTime_IV_19 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_19")]
        public string PartyTime_TimeName_V_19 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_19")]
        public string PartyTime_OrderTime_V_19 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_19")]
        public string PartyTime_ActTime_V_19 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_19")]
        public string PartyTime_DelayTime_V_19 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_19")]
        public string PartyFood_FoodName_I_19 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_19")]
        public string PartyFood_BeginTime_I_19 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_19")]
        public string PartyFood_EndTime_I_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_19")]
        public string PartyFood_RestRoomTime_I_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_19")]
        public string PartyFood_RestRoomFlg_I_19 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_19")]
        public string PartyFood_FoodName_II_19 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_19")]
        public string PartyFood_BeginTime_II_19 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_19")]
        public string PartyFood_EndTime_II_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_19")]
        public string PartyFood_RestRoomTime_II_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_19")]
        public string PartyFood_RestRoomFlg_II_19 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_19")]
        public string PartyFood_FoodName_III_19 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_19")]
        public string PartyFood_BeginTime_III_19 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_19")]
        public string PartyFood_EndTime_III_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_19")]
        public string PartyFood_RestRoomTime_III_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_19")]
        public string PartyFood_RestRoomFlg_III_19 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_19")]
        public string PartyFood_FoodName_IV_19 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_19")]
        public string PartyFood_BeginTime_IV_19 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_19")]
        public string PartyFood_EndTime_IV_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_19")]
        public string PartyFood_RestRoomTime_IV_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_19")]
        public string PartyFood_RestRoomFlg_IV_19 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_19")]
        public string PartyFood_FoodName_V_19 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_19")]
        public string PartyFood_BeginTime_V_19 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_19")]
        public string PartyFood_EndTime_V_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_19")]
        public string PartyFood_RestRoomTime_V_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_19")]
        public string PartyFood_RestRoomFlg_V_19 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_19")]
        public string PartyFood_FoodName_VI_19 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_19")]
        public string PartyFood_BeginTime_VI_19 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_19")]
        public string PartyFood_EndTime_VI_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_19")]
        public string PartyFood_RestRoomTime_VI_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_19")]
        public string PartyFood_RestRoomFlg_VI_19 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_19")]
        public string PartyFood_FoodName_VII_19 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_19")]
        public string PartyFood_BeginTime_VII_19 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_19")]
        public string PartyFood_EndTime_VII_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_19")]
        public string PartyFood_RestRoomTime_VII_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_19")]
        public string PartyFood_RestRoomFlg_VII_19 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_19")]
        public string PartyFood_FoodName_VIII_19 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_19")]
        public string PartyFood_BeginTime_VIII_19 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_19")]
        public string PartyFood_EndTime_VIII_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_19")]
        public string PartyFood_RestRoomTime_VIII_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_19")]
        public string PartyFood_RestRoomFlg_VIII_19 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_19")]
        public string PartyFood_FoodName_IX_19 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_19")]
        public string PartyFood_BeginTime_IX_19 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_19")]
        public string PartyFood_EndTime_IX_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_19")]
        public string PartyFood_RestRoomTime_IX_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_19")]
        public string PartyFood_RestRoomFlg_IX_19 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_19")]
        public string PartyFood_FoodName_X_19 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_19")]
        public string PartyFood_BeginTime_X_19 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_19")]
        public string PartyFood_EndTime_X_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_19")]
        public string PartyFood_RestRoomTime_X_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_19")]
        public string PartyFood_RestRoomFlg_X_19 { get; set; }
        [Display(Name = "PartyMember_Name_I_19")]
        public string PartyMember_Name_I_19 { get; set; }
        [Display(Name = "PartyMember_Name_II_19")]
        public string PartyMember_Name_II_19 { get; set; }
        [Display(Name = "PartyMember_Name_III_19")]
        public string PartyMember_Name_III_19 { get; set; }
        [Display(Name = "PartyMember_Name_IV_19")]
        public string PartyMember_Name_IV_19 { get; set; }
        [Display(Name = "PartyMember_Name_V_19")]
        public string PartyMember_Name_V_19 { get; set; }
        [Display(Name = "PartyMember_Name_VI_19")]
        public string PartyMember_Name_VI_19 { get; set; }
        [Display(Name = "PartyMember_Name_VII_19")]
        public string PartyMember_Name_VII_19 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_19")]
        public string PartyMember_Name_VIII_19 { get; set; }
        [Display(Name = "PartyMember_Name_IX_19")]
        public string PartyMember_Name_IX_19 { get; set; }
        [Display(Name = "PartyDate_20")]
        public string PartyDate_20 { get; set; }
        [Display(Name = "BrideFamilyName_20")]
        public string BrideFamilyName_20 { get; set; }
        [Display(Name = "GroomFamilyName_20")]
        public string GroomFamilyName_20 { get; set; }
        [Display(Name = "TantoName_20")]
        public string TantoName_20 { get; set; }
        [Display(Name = "ReporterName_20")]
        public string ReporterName_20 { get; set; }
        [Display(Name = "AdultCnt_20")]
        public string AdultCnt_20 { get; set; }
        [Display(Name = "HalfCnt_20")]
        public string HalfCnt_20 { get; set; }
        [Display(Name = "ChildrenCnt_20")]
        public string ChildrenCnt_20 { get; set; }
        [Display(Name = "SeatOnlyCnt_20")]
        public string SeatOnlyCnt_20 { get; set; }
        [Display(Name = "TableCnt_20")]
        public string TableCnt_20 { get; set; }
        [Display(Name = "TableCross_20")]
        public string TableCross_20 { get; set; }
        [Display(Name = "PartyStyleName_20")]
        public string PartyStyleName_20 { get; set; }
        [Display(Name = "FoodStyleName_20")]
        public string FoodStyleName_20 { get; set; }
        [Display(Name = "FoodPricce_20")]
        public string FoodPricce_20 { get; set; }
        [Display(Name = "DrinkPrice_20")]
        public string DrinkPrice_20 { get; set; }
        [Display(Name = "Wdrink_20")]
        public string Wdrink_20 { get; set; }
        [Display(Name = "Desl_20")]
        public string Desl_20 { get; set; }
        [Display(Name = "RestRoomFlg_20")]
        public string RestRoomFlg_20 { get; set; }
        [Display(Name = "AnketFlg_20")]
        public string AnketFlg_20 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_20")]
        public string PartyTime_TimeName_I_20 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_20")]
        public string PartyTime_OrderTime_I_20 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_20")]
        public string PartyTime_ActTime_I_20 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_20")]
        public string PartyTime_DelayTime_I_20 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_20")]
        public string PartyTime_TimeName_II_20 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_20")]
        public string PartyTime_OrderTime_II_20 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_20")]
        public string PartyTime_ActTime_II_20 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_20")]
        public string PartyTime_DelayTime_II_20 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_20")]
        public string PartyTime_TimeName_III_20 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_20")]
        public string PartyTime_OrderTime_III_20 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_20")]
        public string PartyTime_ActTime_III_20 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_20")]
        public string PartyTime_DelayTime_III_20 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_20")]
        public string PartyTime_TimeName_IV_20 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_20")]
        public string PartyTime_OrderTime_IV_20 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_20")]
        public string PartyTime_ActTime_IV_20 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_20")]
        public string PartyTime_DelayTime_IV_20 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_20")]
        public string PartyTime_TimeName_V_20 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_20")]
        public string PartyTime_OrderTime_V_20 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_20")]
        public string PartyTime_ActTime_V_20 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_20")]
        public string PartyTime_DelayTime_V_20 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_20")]
        public string PartyFood_FoodName_I_20 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_20")]
        public string PartyFood_BeginTime_I_20 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_20")]
        public string PartyFood_EndTime_I_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_20")]
        public string PartyFood_RestRoomTime_I_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_20")]
        public string PartyFood_RestRoomFlg_I_20 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_20")]
        public string PartyFood_FoodName_II_20 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_20")]
        public string PartyFood_BeginTime_II_20 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_20")]
        public string PartyFood_EndTime_II_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_20")]
        public string PartyFood_RestRoomTime_II_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_20")]
        public string PartyFood_RestRoomFlg_II_20 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_20")]
        public string PartyFood_FoodName_III_20 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_20")]
        public string PartyFood_BeginTime_III_20 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_20")]
        public string PartyFood_EndTime_III_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_20")]
        public string PartyFood_RestRoomTime_III_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_20")]
        public string PartyFood_RestRoomFlg_III_20 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_20")]
        public string PartyFood_FoodName_IV_20 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_20")]
        public string PartyFood_BeginTime_IV_20 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_20")]
        public string PartyFood_EndTime_IV_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_20")]
        public string PartyFood_RestRoomTime_IV_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_20")]
        public string PartyFood_RestRoomFlg_IV_20 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_20")]
        public string PartyFood_FoodName_V_20 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_20")]
        public string PartyFood_BeginTime_V_20 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_20")]
        public string PartyFood_EndTime_V_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_20")]
        public string PartyFood_RestRoomTime_V_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_20")]
        public string PartyFood_RestRoomFlg_V_20 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_20")]
        public string PartyFood_FoodName_VI_20 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_20")]
        public string PartyFood_BeginTime_VI_20 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_20")]
        public string PartyFood_EndTime_VI_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_20")]
        public string PartyFood_RestRoomTime_VI_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_20")]
        public string PartyFood_RestRoomFlg_VI_20 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_20")]
        public string PartyFood_FoodName_VII_20 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_20")]
        public string PartyFood_BeginTime_VII_20 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_20")]
        public string PartyFood_EndTime_VII_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_20")]
        public string PartyFood_RestRoomTime_VII_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_20")]
        public string PartyFood_RestRoomFlg_VII_20 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_20")]
        public string PartyFood_FoodName_VIII_20 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_20")]
        public string PartyFood_BeginTime_VIII_20 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_20")]
        public string PartyFood_EndTime_VIII_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_20")]
        public string PartyFood_RestRoomTime_VIII_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_20")]
        public string PartyFood_RestRoomFlg_VIII_20 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_20")]
        public string PartyFood_FoodName_IX_20 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_20")]
        public string PartyFood_BeginTime_IX_20 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_20")]
        public string PartyFood_EndTime_IX_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_20")]
        public string PartyFood_RestRoomTime_IX_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_20")]
        public string PartyFood_RestRoomFlg_IX_20 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_20")]
        public string PartyFood_FoodName_X_20 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_20")]
        public string PartyFood_BeginTime_X_20 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_20")]
        public string PartyFood_EndTime_X_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_20")]
        public string PartyFood_RestRoomTime_X_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_20")]
        public string PartyFood_RestRoomFlg_X_20 { get; set; }
        [Display(Name = "PartyMember_Name_I_20")]
        public string PartyMember_Name_I_20 { get; set; }
        [Display(Name = "PartyMember_Name_II_20")]
        public string PartyMember_Name_II_20 { get; set; }
        [Display(Name = "PartyMember_Name_III_20")]
        public string PartyMember_Name_III_20 { get; set; }
        [Display(Name = "PartyMember_Name_IV_20")]
        public string PartyMember_Name_IV_20 { get; set; }
        [Display(Name = "PartyMember_Name_V_20")]
        public string PartyMember_Name_V_20 { get; set; }
        [Display(Name = "PartyMember_Name_VI_20")]
        public string PartyMember_Name_VI_20 { get; set; }
        [Display(Name = "PartyMember_Name_VII_20")]
        public string PartyMember_Name_VII_20 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_20")]
        public string PartyMember_Name_VIII_20 { get; set; }
        [Display(Name = "PartyMember_Name_IX_20")]
        public string PartyMember_Name_IX_20 { get; set; }
        [Display(Name = "PartyDate_21")]
        public string PartyDate_21 { get; set; }
        [Display(Name = "BrideFamilyName_21")]
        public string BrideFamilyName_21 { get; set; }
        [Display(Name = "GroomFamilyName_21")]
        public string GroomFamilyName_21 { get; set; }
        [Display(Name = "TantoName_21")]
        public string TantoName_21 { get; set; }
        [Display(Name = "ReporterName_21")]
        public string ReporterName_21 { get; set; }
        [Display(Name = "AdultCnt_21")]
        public string AdultCnt_21 { get; set; }
        [Display(Name = "HalfCnt_21")]
        public string HalfCnt_21 { get; set; }
        [Display(Name = "ChildrenCnt_21")]
        public string ChildrenCnt_21 { get; set; }
        [Display(Name = "SeatOnlyCnt_21")]
        public string SeatOnlyCnt_21 { get; set; }
        [Display(Name = "TableCnt_21")]
        public string TableCnt_21 { get; set; }
        [Display(Name = "TableCross_21")]
        public string TableCross_21 { get; set; }
        [Display(Name = "PartyStyleName_21")]
        public string PartyStyleName_21 { get; set; }
        [Display(Name = "FoodStyleName_21")]
        public string FoodStyleName_21 { get; set; }
        [Display(Name = "FoodPricce_21")]
        public string FoodPricce_21 { get; set; }
        [Display(Name = "DrinkPrice_21")]
        public string DrinkPrice_21 { get; set; }
        [Display(Name = "Wdrink_21")]
        public string Wdrink_21 { get; set; }
        [Display(Name = "Desl_21")]
        public string Desl_21 { get; set; }
        [Display(Name = "RestRoomFlg_21")]
        public string RestRoomFlg_21 { get; set; }
        [Display(Name = "AnketFlg_21")]
        public string AnketFlg_21 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_21")]
        public string PartyTime_TimeName_I_21 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_21")]
        public string PartyTime_OrderTime_I_21 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_21")]
        public string PartyTime_ActTime_I_21 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_21")]
        public string PartyTime_DelayTime_I_21 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_21")]
        public string PartyTime_TimeName_II_21 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_21")]
        public string PartyTime_OrderTime_II_21 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_21")]
        public string PartyTime_ActTime_II_21 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_21")]
        public string PartyTime_DelayTime_II_21 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_21")]
        public string PartyTime_TimeName_III_21 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_21")]
        public string PartyTime_OrderTime_III_21 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_21")]
        public string PartyTime_ActTime_III_21 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_21")]
        public string PartyTime_DelayTime_III_21 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_21")]
        public string PartyTime_TimeName_IV_21 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_21")]
        public string PartyTime_OrderTime_IV_21 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_21")]
        public string PartyTime_ActTime_IV_21 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_21")]
        public string PartyTime_DelayTime_IV_21 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_21")]
        public string PartyTime_TimeName_V_21 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_21")]
        public string PartyTime_OrderTime_V_21 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_21")]
        public string PartyTime_ActTime_V_21 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_21")]
        public string PartyTime_DelayTime_V_21 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_21")]
        public string PartyFood_FoodName_I_21 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_21")]
        public string PartyFood_BeginTime_I_21 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_21")]
        public string PartyFood_EndTime_I_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_21")]
        public string PartyFood_RestRoomTime_I_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_21")]
        public string PartyFood_RestRoomFlg_I_21 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_21")]
        public string PartyFood_FoodName_II_21 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_21")]
        public string PartyFood_BeginTime_II_21 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_21")]
        public string PartyFood_EndTime_II_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_21")]
        public string PartyFood_RestRoomTime_II_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_21")]
        public string PartyFood_RestRoomFlg_II_21 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_21")]
        public string PartyFood_FoodName_III_21 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_21")]
        public string PartyFood_BeginTime_III_21 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_21")]
        public string PartyFood_EndTime_III_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_21")]
        public string PartyFood_RestRoomTime_III_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_21")]
        public string PartyFood_RestRoomFlg_III_21 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_21")]
        public string PartyFood_FoodName_IV_21 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_21")]
        public string PartyFood_BeginTime_IV_21 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_21")]
        public string PartyFood_EndTime_IV_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_21")]
        public string PartyFood_RestRoomTime_IV_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_21")]
        public string PartyFood_RestRoomFlg_IV_21 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_21")]
        public string PartyFood_FoodName_V_21 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_21")]
        public string PartyFood_BeginTime_V_21 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_21")]
        public string PartyFood_EndTime_V_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_21")]
        public string PartyFood_RestRoomTime_V_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_21")]
        public string PartyFood_RestRoomFlg_V_21 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_21")]
        public string PartyFood_FoodName_VI_21 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_21")]
        public string PartyFood_BeginTime_VI_21 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_21")]
        public string PartyFood_EndTime_VI_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_21")]
        public string PartyFood_RestRoomTime_VI_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_21")]
        public string PartyFood_RestRoomFlg_VI_21 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_21")]
        public string PartyFood_FoodName_VII_21 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_21")]
        public string PartyFood_BeginTime_VII_21 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_21")]
        public string PartyFood_EndTime_VII_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_21")]
        public string PartyFood_RestRoomTime_VII_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_21")]
        public string PartyFood_RestRoomFlg_VII_21 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_21")]
        public string PartyFood_FoodName_VIII_21 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_21")]
        public string PartyFood_BeginTime_VIII_21 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_21")]
        public string PartyFood_EndTime_VIII_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_21")]
        public string PartyFood_RestRoomTime_VIII_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_21")]
        public string PartyFood_RestRoomFlg_VIII_21 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_21")]
        public string PartyFood_FoodName_IX_21 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_21")]
        public string PartyFood_BeginTime_IX_21 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_21")]
        public string PartyFood_EndTime_IX_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_21")]
        public string PartyFood_RestRoomTime_IX_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_21")]
        public string PartyFood_RestRoomFlg_IX_21 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_21")]
        public string PartyFood_FoodName_X_21 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_21")]
        public string PartyFood_BeginTime_X_21 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_21")]
        public string PartyFood_EndTime_X_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_21")]
        public string PartyFood_RestRoomTime_X_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_21")]
        public string PartyFood_RestRoomFlg_X_21 { get; set; }
        [Display(Name = "PartyMember_Name_I_21")]
        public string PartyMember_Name_I_21 { get; set; }
        [Display(Name = "PartyMember_Name_II_21")]
        public string PartyMember_Name_II_21 { get; set; }
        [Display(Name = "PartyMember_Name_III_21")]
        public string PartyMember_Name_III_21 { get; set; }
        [Display(Name = "PartyMember_Name_IV_21")]
        public string PartyMember_Name_IV_21 { get; set; }
        [Display(Name = "PartyMember_Name_V_21")]
        public string PartyMember_Name_V_21 { get; set; }
        [Display(Name = "PartyMember_Name_VI_21")]
        public string PartyMember_Name_VI_21 { get; set; }
        [Display(Name = "PartyMember_Name_VII_21")]
        public string PartyMember_Name_VII_21 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_21")]
        public string PartyMember_Name_VIII_21 { get; set; }
        [Display(Name = "PartyMember_Name_IX_21")]
        public string PartyMember_Name_IX_21 { get; set; }
        [Display(Name = "PartyDate_22")]
        public string PartyDate_22 { get; set; }
        [Display(Name = "BrideFamilyName_22")]
        public string BrideFamilyName_22 { get; set; }
        [Display(Name = "GroomFamilyName_22")]
        public string GroomFamilyName_22 { get; set; }
        [Display(Name = "TantoName_22")]
        public string TantoName_22 { get; set; }
        [Display(Name = "ReporterName_22")]
        public string ReporterName_22 { get; set; }
        [Display(Name = "AdultCnt_22")]
        public string AdultCnt_22 { get; set; }
        [Display(Name = "HalfCnt_22")]
        public string HalfCnt_22 { get; set; }
        [Display(Name = "ChildrenCnt_22")]
        public string ChildrenCnt_22 { get; set; }
        [Display(Name = "SeatOnlyCnt_22")]
        public string SeatOnlyCnt_22 { get; set; }
        [Display(Name = "TableCnt_22")]
        public string TableCnt_22 { get; set; }
        [Display(Name = "TableCross_22")]
        public string TableCross_22 { get; set; }
        [Display(Name = "PartyStyleName_22")]
        public string PartyStyleName_22 { get; set; }
        [Display(Name = "FoodStyleName_22")]
        public string FoodStyleName_22 { get; set; }
        [Display(Name = "FoodPricce_22")]
        public string FoodPricce_22 { get; set; }
        [Display(Name = "DrinkPrice_22")]
        public string DrinkPrice_22 { get; set; }
        [Display(Name = "Wdrink_22")]
        public string Wdrink_22 { get; set; }
        [Display(Name = "Desl_22")]
        public string Desl_22 { get; set; }
        [Display(Name = "RestRoomFlg_22")]
        public string RestRoomFlg_22 { get; set; }
        [Display(Name = "AnketFlg_22")]
        public string AnketFlg_22 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_22")]
        public string PartyTime_TimeName_I_22 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_22")]
        public string PartyTime_OrderTime_I_22 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_22")]
        public string PartyTime_ActTime_I_22 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_22")]
        public string PartyTime_DelayTime_I_22 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_22")]
        public string PartyTime_TimeName_II_22 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_22")]
        public string PartyTime_OrderTime_II_22 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_22")]
        public string PartyTime_ActTime_II_22 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_22")]
        public string PartyTime_DelayTime_II_22 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_22")]
        public string PartyTime_TimeName_III_22 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_22")]
        public string PartyTime_OrderTime_III_22 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_22")]
        public string PartyTime_ActTime_III_22 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_22")]
        public string PartyTime_DelayTime_III_22 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_22")]
        public string PartyTime_TimeName_IV_22 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_22")]
        public string PartyTime_OrderTime_IV_22 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_22")]
        public string PartyTime_ActTime_IV_22 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_22")]
        public string PartyTime_DelayTime_IV_22 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_22")]
        public string PartyTime_TimeName_V_22 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_22")]
        public string PartyTime_OrderTime_V_22 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_22")]
        public string PartyTime_ActTime_V_22 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_22")]
        public string PartyTime_DelayTime_V_22 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_22")]
        public string PartyFood_FoodName_I_22 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_22")]
        public string PartyFood_BeginTime_I_22 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_22")]
        public string PartyFood_EndTime_I_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_22")]
        public string PartyFood_RestRoomTime_I_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_22")]
        public string PartyFood_RestRoomFlg_I_22 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_22")]
        public string PartyFood_FoodName_II_22 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_22")]
        public string PartyFood_BeginTime_II_22 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_22")]
        public string PartyFood_EndTime_II_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_22")]
        public string PartyFood_RestRoomTime_II_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_22")]
        public string PartyFood_RestRoomFlg_II_22 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_22")]
        public string PartyFood_FoodName_III_22 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_22")]
        public string PartyFood_BeginTime_III_22 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_22")]
        public string PartyFood_EndTime_III_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_22")]
        public string PartyFood_RestRoomTime_III_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_22")]
        public string PartyFood_RestRoomFlg_III_22 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_22")]
        public string PartyFood_FoodName_IV_22 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_22")]
        public string PartyFood_BeginTime_IV_22 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_22")]
        public string PartyFood_EndTime_IV_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_22")]
        public string PartyFood_RestRoomTime_IV_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_22")]
        public string PartyFood_RestRoomFlg_IV_22 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_22")]
        public string PartyFood_FoodName_V_22 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_22")]
        public string PartyFood_BeginTime_V_22 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_22")]
        public string PartyFood_EndTime_V_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_22")]
        public string PartyFood_RestRoomTime_V_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_22")]
        public string PartyFood_RestRoomFlg_V_22 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_22")]
        public string PartyFood_FoodName_VI_22 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_22")]
        public string PartyFood_BeginTime_VI_22 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_22")]
        public string PartyFood_EndTime_VI_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_22")]
        public string PartyFood_RestRoomTime_VI_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_22")]
        public string PartyFood_RestRoomFlg_VI_22 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_22")]
        public string PartyFood_FoodName_VII_22 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_22")]
        public string PartyFood_BeginTime_VII_22 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_22")]
        public string PartyFood_EndTime_VII_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_22")]
        public string PartyFood_RestRoomTime_VII_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_22")]
        public string PartyFood_RestRoomFlg_VII_22 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_22")]
        public string PartyFood_FoodName_VIII_22 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_22")]
        public string PartyFood_BeginTime_VIII_22 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_22")]
        public string PartyFood_EndTime_VIII_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_22")]
        public string PartyFood_RestRoomTime_VIII_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_22")]
        public string PartyFood_RestRoomFlg_VIII_22 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_22")]
        public string PartyFood_FoodName_IX_22 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_22")]
        public string PartyFood_BeginTime_IX_22 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_22")]
        public string PartyFood_EndTime_IX_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_22")]
        public string PartyFood_RestRoomTime_IX_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_22")]
        public string PartyFood_RestRoomFlg_IX_22 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_22")]
        public string PartyFood_FoodName_X_22 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_22")]
        public string PartyFood_BeginTime_X_22 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_22")]
        public string PartyFood_EndTime_X_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_22")]
        public string PartyFood_RestRoomTime_X_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_22")]
        public string PartyFood_RestRoomFlg_X_22 { get; set; }
        [Display(Name = "PartyMember_Name_I_22")]
        public string PartyMember_Name_I_22 { get; set; }
        [Display(Name = "PartyMember_Name_II_22")]
        public string PartyMember_Name_II_22 { get; set; }
        [Display(Name = "PartyMember_Name_III_22")]
        public string PartyMember_Name_III_22 { get; set; }
        [Display(Name = "PartyMember_Name_IV_22")]
        public string PartyMember_Name_IV_22 { get; set; }
        [Display(Name = "PartyMember_Name_V_22")]
        public string PartyMember_Name_V_22 { get; set; }
        [Display(Name = "PartyMember_Name_VI_22")]
        public string PartyMember_Name_VI_22 { get; set; }
        [Display(Name = "PartyMember_Name_VII_22")]
        public string PartyMember_Name_VII_22 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_22")]
        public string PartyMember_Name_VIII_22 { get; set; }
        [Display(Name = "PartyMember_Name_IX_22")]
        public string PartyMember_Name_IX_22 { get; set; }
        [Display(Name = "PartyDate_23")]
        public string PartyDate_23 { get; set; }
        [Display(Name = "BrideFamilyName_23")]
        public string BrideFamilyName_23 { get; set; }
        [Display(Name = "GroomFamilyName_23")]
        public string GroomFamilyName_23 { get; set; }
        [Display(Name = "TantoName_23")]
        public string TantoName_23 { get; set; }
        [Display(Name = "ReporterName_23")]
        public string ReporterName_23 { get; set; }
        [Display(Name = "AdultCnt_23")]
        public string AdultCnt_23 { get; set; }
        [Display(Name = "HalfCnt_23")]
        public string HalfCnt_23 { get; set; }
        [Display(Name = "ChildrenCnt_23")]
        public string ChildrenCnt_23 { get; set; }
        [Display(Name = "SeatOnlyCnt_23")]
        public string SeatOnlyCnt_23 { get; set; }
        [Display(Name = "TableCnt_23")]
        public string TableCnt_23 { get; set; }
        [Display(Name = "TableCross_23")]
        public string TableCross_23 { get; set; }
        [Display(Name = "PartyStyleName_23")]
        public string PartyStyleName_23 { get; set; }
        [Display(Name = "FoodStyleName_23")]
        public string FoodStyleName_23 { get; set; }
        [Display(Name = "FoodPricce_23")]
        public string FoodPricce_23 { get; set; }
        [Display(Name = "DrinkPrice_23")]
        public string DrinkPrice_23 { get; set; }
        [Display(Name = "Wdrink_23")]
        public string Wdrink_23 { get; set; }
        [Display(Name = "Desl_23")]
        public string Desl_23 { get; set; }
        [Display(Name = "RestRoomFlg_23")]
        public string RestRoomFlg_23 { get; set; }
        [Display(Name = "AnketFlg_23")]
        public string AnketFlg_23 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_23")]
        public string PartyTime_TimeName_I_23 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_23")]
        public string PartyTime_OrderTime_I_23 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_23")]
        public string PartyTime_ActTime_I_23 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_23")]
        public string PartyTime_DelayTime_I_23 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_23")]
        public string PartyTime_TimeName_II_23 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_23")]
        public string PartyTime_OrderTime_II_23 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_23")]
        public string PartyTime_ActTime_II_23 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_23")]
        public string PartyTime_DelayTime_II_23 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_23")]
        public string PartyTime_TimeName_III_23 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_23")]
        public string PartyTime_OrderTime_III_23 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_23")]
        public string PartyTime_ActTime_III_23 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_23")]
        public string PartyTime_DelayTime_III_23 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_23")]
        public string PartyTime_TimeName_IV_23 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_23")]
        public string PartyTime_OrderTime_IV_23 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_23")]
        public string PartyTime_ActTime_IV_23 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_23")]
        public string PartyTime_DelayTime_IV_23 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_23")]
        public string PartyTime_TimeName_V_23 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_23")]
        public string PartyTime_OrderTime_V_23 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_23")]
        public string PartyTime_ActTime_V_23 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_23")]
        public string PartyTime_DelayTime_V_23 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_23")]
        public string PartyFood_FoodName_I_23 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_23")]
        public string PartyFood_BeginTime_I_23 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_23")]
        public string PartyFood_EndTime_I_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_23")]
        public string PartyFood_RestRoomTime_I_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_23")]
        public string PartyFood_RestRoomFlg_I_23 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_23")]
        public string PartyFood_FoodName_II_23 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_23")]
        public string PartyFood_BeginTime_II_23 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_23")]
        public string PartyFood_EndTime_II_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_23")]
        public string PartyFood_RestRoomTime_II_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_23")]
        public string PartyFood_RestRoomFlg_II_23 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_23")]
        public string PartyFood_FoodName_III_23 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_23")]
        public string PartyFood_BeginTime_III_23 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_23")]
        public string PartyFood_EndTime_III_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_23")]
        public string PartyFood_RestRoomTime_III_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_23")]
        public string PartyFood_RestRoomFlg_III_23 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_23")]
        public string PartyFood_FoodName_IV_23 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_23")]
        public string PartyFood_BeginTime_IV_23 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_23")]
        public string PartyFood_EndTime_IV_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_23")]
        public string PartyFood_RestRoomTime_IV_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_23")]
        public string PartyFood_RestRoomFlg_IV_23 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_23")]
        public string PartyFood_FoodName_V_23 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_23")]
        public string PartyFood_BeginTime_V_23 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_23")]
        public string PartyFood_EndTime_V_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_23")]
        public string PartyFood_RestRoomTime_V_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_23")]
        public string PartyFood_RestRoomFlg_V_23 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_23")]
        public string PartyFood_FoodName_VI_23 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_23")]
        public string PartyFood_BeginTime_VI_23 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_23")]
        public string PartyFood_EndTime_VI_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_23")]
        public string PartyFood_RestRoomTime_VI_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_23")]
        public string PartyFood_RestRoomFlg_VI_23 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_23")]
        public string PartyFood_FoodName_VII_23 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_23")]
        public string PartyFood_BeginTime_VII_23 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_23")]
        public string PartyFood_EndTime_VII_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_23")]
        public string PartyFood_RestRoomTime_VII_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_23")]
        public string PartyFood_RestRoomFlg_VII_23 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_23")]
        public string PartyFood_FoodName_VIII_23 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_23")]
        public string PartyFood_BeginTime_VIII_23 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_23")]
        public string PartyFood_EndTime_VIII_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_23")]
        public string PartyFood_RestRoomTime_VIII_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_23")]
        public string PartyFood_RestRoomFlg_VIII_23 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_23")]
        public string PartyFood_FoodName_IX_23 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_23")]
        public string PartyFood_BeginTime_IX_23 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_23")]
        public string PartyFood_EndTime_IX_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_23")]
        public string PartyFood_RestRoomTime_IX_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_23")]
        public string PartyFood_RestRoomFlg_IX_23 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_23")]
        public string PartyFood_FoodName_X_23 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_23")]
        public string PartyFood_BeginTime_X_23 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_23")]
        public string PartyFood_EndTime_X_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_23")]
        public string PartyFood_RestRoomTime_X_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_23")]
        public string PartyFood_RestRoomFlg_X_23 { get; set; }
        [Display(Name = "PartyMember_Name_I_23")]
        public string PartyMember_Name_I_23 { get; set; }
        [Display(Name = "PartyMember_Name_II_23")]
        public string PartyMember_Name_II_23 { get; set; }
        [Display(Name = "PartyMember_Name_III_23")]
        public string PartyMember_Name_III_23 { get; set; }
        [Display(Name = "PartyMember_Name_IV_23")]
        public string PartyMember_Name_IV_23 { get; set; }
        [Display(Name = "PartyMember_Name_V_23")]
        public string PartyMember_Name_V_23 { get; set; }
        [Display(Name = "PartyMember_Name_VI_23")]
        public string PartyMember_Name_VI_23 { get; set; }
        [Display(Name = "PartyMember_Name_VII_23")]
        public string PartyMember_Name_VII_23 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_23")]
        public string PartyMember_Name_VIII_23 { get; set; }
        [Display(Name = "PartyMember_Name_IX_23")]
        public string PartyMember_Name_IX_23 { get; set; }
        [Display(Name = "PartyDate_24")]
        public string PartyDate_24 { get; set; }
        [Display(Name = "BrideFamilyName_24")]
        public string BrideFamilyName_24 { get; set; }
        [Display(Name = "GroomFamilyName_24")]
        public string GroomFamilyName_24 { get; set; }
        [Display(Name = "TantoName_24")]
        public string TantoName_24 { get; set; }
        [Display(Name = "ReporterName_24")]
        public string ReporterName_24 { get; set; }
        [Display(Name = "AdultCnt_24")]
        public string AdultCnt_24 { get; set; }
        [Display(Name = "HalfCnt_24")]
        public string HalfCnt_24 { get; set; }
        [Display(Name = "ChildrenCnt_24")]
        public string ChildrenCnt_24 { get; set; }
        [Display(Name = "SeatOnlyCnt_24")]
        public string SeatOnlyCnt_24 { get; set; }
        [Display(Name = "TableCnt_24")]
        public string TableCnt_24 { get; set; }
        [Display(Name = "TableCross_24")]
        public string TableCross_24 { get; set; }
        [Display(Name = "PartyStyleName_24")]
        public string PartyStyleName_24 { get; set; }
        [Display(Name = "FoodStyleName_24")]
        public string FoodStyleName_24 { get; set; }
        [Display(Name = "FoodPricce_24")]
        public string FoodPricce_24 { get; set; }
        [Display(Name = "DrinkPrice_24")]
        public string DrinkPrice_24 { get; set; }
        [Display(Name = "Wdrink_24")]
        public string Wdrink_24 { get; set; }
        [Display(Name = "Desl_24")]
        public string Desl_24 { get; set; }
        [Display(Name = "RestRoomFlg_24")]
        public string RestRoomFlg_24 { get; set; }
        [Display(Name = "AnketFlg_24")]
        public string AnketFlg_24 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_24")]
        public string PartyTime_TimeName_I_24 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_24")]
        public string PartyTime_OrderTime_I_24 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_24")]
        public string PartyTime_ActTime_I_24 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_24")]
        public string PartyTime_DelayTime_I_24 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_24")]
        public string PartyTime_TimeName_II_24 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_24")]
        public string PartyTime_OrderTime_II_24 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_24")]
        public string PartyTime_ActTime_II_24 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_24")]
        public string PartyTime_DelayTime_II_24 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_24")]
        public string PartyTime_TimeName_III_24 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_24")]
        public string PartyTime_OrderTime_III_24 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_24")]
        public string PartyTime_ActTime_III_24 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_24")]
        public string PartyTime_DelayTime_III_24 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_24")]
        public string PartyTime_TimeName_IV_24 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_24")]
        public string PartyTime_OrderTime_IV_24 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_24")]
        public string PartyTime_ActTime_IV_24 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_24")]
        public string PartyTime_DelayTime_IV_24 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_24")]
        public string PartyTime_TimeName_V_24 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_24")]
        public string PartyTime_OrderTime_V_24 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_24")]
        public string PartyTime_ActTime_V_24 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_24")]
        public string PartyTime_DelayTime_V_24 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_24")]
        public string PartyFood_FoodName_I_24 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_24")]
        public string PartyFood_BeginTime_I_24 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_24")]
        public string PartyFood_EndTime_I_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_24")]
        public string PartyFood_RestRoomTime_I_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_24")]
        public string PartyFood_RestRoomFlg_I_24 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_24")]
        public string PartyFood_FoodName_II_24 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_24")]
        public string PartyFood_BeginTime_II_24 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_24")]
        public string PartyFood_EndTime_II_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_24")]
        public string PartyFood_RestRoomTime_II_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_24")]
        public string PartyFood_RestRoomFlg_II_24 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_24")]
        public string PartyFood_FoodName_III_24 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_24")]
        public string PartyFood_BeginTime_III_24 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_24")]
        public string PartyFood_EndTime_III_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_24")]
        public string PartyFood_RestRoomTime_III_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_24")]
        public string PartyFood_RestRoomFlg_III_24 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_24")]
        public string PartyFood_FoodName_IV_24 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_24")]
        public string PartyFood_BeginTime_IV_24 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_24")]
        public string PartyFood_EndTime_IV_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_24")]
        public string PartyFood_RestRoomTime_IV_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_24")]
        public string PartyFood_RestRoomFlg_IV_24 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_24")]
        public string PartyFood_FoodName_V_24 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_24")]
        public string PartyFood_BeginTime_V_24 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_24")]
        public string PartyFood_EndTime_V_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_24")]
        public string PartyFood_RestRoomTime_V_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_24")]
        public string PartyFood_RestRoomFlg_V_24 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_24")]
        public string PartyFood_FoodName_VI_24 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_24")]
        public string PartyFood_BeginTime_VI_24 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_24")]
        public string PartyFood_EndTime_VI_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_24")]
        public string PartyFood_RestRoomTime_VI_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_24")]
        public string PartyFood_RestRoomFlg_VI_24 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_24")]
        public string PartyFood_FoodName_VII_24 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_24")]
        public string PartyFood_BeginTime_VII_24 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_24")]
        public string PartyFood_EndTime_VII_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_24")]
        public string PartyFood_RestRoomTime_VII_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_24")]
        public string PartyFood_RestRoomFlg_VII_24 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_24")]
        public string PartyFood_FoodName_VIII_24 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_24")]
        public string PartyFood_BeginTime_VIII_24 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_24")]
        public string PartyFood_EndTime_VIII_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_24")]
        public string PartyFood_RestRoomTime_VIII_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_24")]
        public string PartyFood_RestRoomFlg_VIII_24 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_24")]
        public string PartyFood_FoodName_IX_24 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_24")]
        public string PartyFood_BeginTime_IX_24 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_24")]
        public string PartyFood_EndTime_IX_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_24")]
        public string PartyFood_RestRoomTime_IX_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_24")]
        public string PartyFood_RestRoomFlg_IX_24 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_24")]
        public string PartyFood_FoodName_X_24 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_24")]
        public string PartyFood_BeginTime_X_24 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_24")]
        public string PartyFood_EndTime_X_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_24")]
        public string PartyFood_RestRoomTime_X_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_24")]
        public string PartyFood_RestRoomFlg_X_24 { get; set; }
        [Display(Name = "PartyMember_Name_I_24")]
        public string PartyMember_Name_I_24 { get; set; }
        [Display(Name = "PartyMember_Name_II_24")]
        public string PartyMember_Name_II_24 { get; set; }
        [Display(Name = "PartyMember_Name_III_24")]
        public string PartyMember_Name_III_24 { get; set; }
        [Display(Name = "PartyMember_Name_IV_24")]
        public string PartyMember_Name_IV_24 { get; set; }
        [Display(Name = "PartyMember_Name_V_24")]
        public string PartyMember_Name_V_24 { get; set; }
        [Display(Name = "PartyMember_Name_VI_24")]
        public string PartyMember_Name_VI_24 { get; set; }
        [Display(Name = "PartyMember_Name_VII_24")]
        public string PartyMember_Name_VII_24 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_24")]
        public string PartyMember_Name_VIII_24 { get; set; }
        [Display(Name = "PartyMember_Name_IX_24")]
        public string PartyMember_Name_IX_24 { get; set; }
        [Display(Name = "PartyDate_25")]
        public string PartyDate_25 { get; set; }
        [Display(Name = "BrideFamilyName_25")]
        public string BrideFamilyName_25 { get; set; }
        [Display(Name = "GroomFamilyName_25")]
        public string GroomFamilyName_25 { get; set; }
        [Display(Name = "TantoName_25")]
        public string TantoName_25 { get; set; }
        [Display(Name = "ReporterName_25")]
        public string ReporterName_25 { get; set; }
        [Display(Name = "AdultCnt_25")]
        public string AdultCnt_25 { get; set; }
        [Display(Name = "HalfCnt_25")]
        public string HalfCnt_25 { get; set; }
        [Display(Name = "ChildrenCnt_25")]
        public string ChildrenCnt_25 { get; set; }
        [Display(Name = "SeatOnlyCnt_25")]
        public string SeatOnlyCnt_25 { get; set; }
        [Display(Name = "TableCnt_25")]
        public string TableCnt_25 { get; set; }
        [Display(Name = "TableCross_25")]
        public string TableCross_25 { get; set; }
        [Display(Name = "PartyStyleName_25")]
        public string PartyStyleName_25 { get; set; }
        [Display(Name = "FoodStyleName_25")]
        public string FoodStyleName_25 { get; set; }
        [Display(Name = "FoodPricce_25")]
        public string FoodPricce_25 { get; set; }
        [Display(Name = "DrinkPrice_25")]
        public string DrinkPrice_25 { get; set; }
        [Display(Name = "Wdrink_25")]
        public string Wdrink_25 { get; set; }
        [Display(Name = "Desl_25")]
        public string Desl_25 { get; set; }
        [Display(Name = "RestRoomFlg_25")]
        public string RestRoomFlg_25 { get; set; }
        [Display(Name = "AnketFlg_25")]
        public string AnketFlg_25 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_25")]
        public string PartyTime_TimeName_I_25 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_25")]
        public string PartyTime_OrderTime_I_25 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_25")]
        public string PartyTime_ActTime_I_25 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_25")]
        public string PartyTime_DelayTime_I_25 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_25")]
        public string PartyTime_TimeName_II_25 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_25")]
        public string PartyTime_OrderTime_II_25 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_25")]
        public string PartyTime_ActTime_II_25 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_25")]
        public string PartyTime_DelayTime_II_25 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_25")]
        public string PartyTime_TimeName_III_25 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_25")]
        public string PartyTime_OrderTime_III_25 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_25")]
        public string PartyTime_ActTime_III_25 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_25")]
        public string PartyTime_DelayTime_III_25 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_25")]
        public string PartyTime_TimeName_IV_25 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_25")]
        public string PartyTime_OrderTime_IV_25 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_25")]
        public string PartyTime_ActTime_IV_25 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_25")]
        public string PartyTime_DelayTime_IV_25 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_25")]
        public string PartyTime_TimeName_V_25 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_25")]
        public string PartyTime_OrderTime_V_25 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_25")]
        public string PartyTime_ActTime_V_25 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_25")]
        public string PartyTime_DelayTime_V_25 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_25")]
        public string PartyFood_FoodName_I_25 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_25")]
        public string PartyFood_BeginTime_I_25 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_25")]
        public string PartyFood_EndTime_I_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_25")]
        public string PartyFood_RestRoomTime_I_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_25")]
        public string PartyFood_RestRoomFlg_I_25 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_25")]
        public string PartyFood_FoodName_II_25 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_25")]
        public string PartyFood_BeginTime_II_25 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_25")]
        public string PartyFood_EndTime_II_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_25")]
        public string PartyFood_RestRoomTime_II_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_25")]
        public string PartyFood_RestRoomFlg_II_25 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_25")]
        public string PartyFood_FoodName_III_25 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_25")]
        public string PartyFood_BeginTime_III_25 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_25")]
        public string PartyFood_EndTime_III_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_25")]
        public string PartyFood_RestRoomTime_III_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_25")]
        public string PartyFood_RestRoomFlg_III_25 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_25")]
        public string PartyFood_FoodName_IV_25 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_25")]
        public string PartyFood_BeginTime_IV_25 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_25")]
        public string PartyFood_EndTime_IV_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_25")]
        public string PartyFood_RestRoomTime_IV_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_25")]
        public string PartyFood_RestRoomFlg_IV_25 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_25")]
        public string PartyFood_FoodName_V_25 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_25")]
        public string PartyFood_BeginTime_V_25 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_25")]
        public string PartyFood_EndTime_V_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_25")]
        public string PartyFood_RestRoomTime_V_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_25")]
        public string PartyFood_RestRoomFlg_V_25 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_25")]
        public string PartyFood_FoodName_VI_25 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_25")]
        public string PartyFood_BeginTime_VI_25 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_25")]
        public string PartyFood_EndTime_VI_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_25")]
        public string PartyFood_RestRoomTime_VI_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_25")]
        public string PartyFood_RestRoomFlg_VI_25 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_25")]
        public string PartyFood_FoodName_VII_25 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_25")]
        public string PartyFood_BeginTime_VII_25 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_25")]
        public string PartyFood_EndTime_VII_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_25")]
        public string PartyFood_RestRoomTime_VII_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_25")]
        public string PartyFood_RestRoomFlg_VII_25 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_25")]
        public string PartyFood_FoodName_VIII_25 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_25")]
        public string PartyFood_BeginTime_VIII_25 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_25")]
        public string PartyFood_EndTime_VIII_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_25")]
        public string PartyFood_RestRoomTime_VIII_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_25")]
        public string PartyFood_RestRoomFlg_VIII_25 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_25")]
        public string PartyFood_FoodName_IX_25 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_25")]
        public string PartyFood_BeginTime_IX_25 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_25")]
        public string PartyFood_EndTime_IX_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_25")]
        public string PartyFood_RestRoomTime_IX_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_25")]
        public string PartyFood_RestRoomFlg_IX_25 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_25")]
        public string PartyFood_FoodName_X_25 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_25")]
        public string PartyFood_BeginTime_X_25 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_25")]
        public string PartyFood_EndTime_X_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_25")]
        public string PartyFood_RestRoomTime_X_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_25")]
        public string PartyFood_RestRoomFlg_X_25 { get; set; }
        [Display(Name = "PartyMember_Name_I_25")]
        public string PartyMember_Name_I_25 { get; set; }
        [Display(Name = "PartyMember_Name_II_25")]
        public string PartyMember_Name_II_25 { get; set; }
        [Display(Name = "PartyMember_Name_III_25")]
        public string PartyMember_Name_III_25 { get; set; }
        [Display(Name = "PartyMember_Name_IV_25")]
        public string PartyMember_Name_IV_25 { get; set; }
        [Display(Name = "PartyMember_Name_V_25")]
        public string PartyMember_Name_V_25 { get; set; }
        [Display(Name = "PartyMember_Name_VI_25")]
        public string PartyMember_Name_VI_25 { get; set; }
        [Display(Name = "PartyMember_Name_VII_25")]
        public string PartyMember_Name_VII_25 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_25")]
        public string PartyMember_Name_VIII_25 { get; set; }
        [Display(Name = "PartyMember_Name_IX_25")]
        public string PartyMember_Name_IX_25 { get; set; }
        [Display(Name = "PartyDate_26")]
        public string PartyDate_26 { get; set; }
        [Display(Name = "BrideFamilyName_26")]
        public string BrideFamilyName_26 { get; set; }
        [Display(Name = "GroomFamilyName_26")]
        public string GroomFamilyName_26 { get; set; }
        [Display(Name = "TantoName_26")]
        public string TantoName_26 { get; set; }
        [Display(Name = "ReporterName_26")]
        public string ReporterName_26 { get; set; }
        [Display(Name = "AdultCnt_26")]
        public string AdultCnt_26 { get; set; }
        [Display(Name = "HalfCnt_26")]
        public string HalfCnt_26 { get; set; }
        [Display(Name = "ChildrenCnt_26")]
        public string ChildrenCnt_26 { get; set; }
        [Display(Name = "SeatOnlyCnt_26")]
        public string SeatOnlyCnt_26 { get; set; }
        [Display(Name = "TableCnt_26")]
        public string TableCnt_26 { get; set; }
        [Display(Name = "TableCross_26")]
        public string TableCross_26 { get; set; }
        [Display(Name = "PartyStyleName_26")]
        public string PartyStyleName_26 { get; set; }
        [Display(Name = "FoodStyleName_26")]
        public string FoodStyleName_26 { get; set; }
        [Display(Name = "FoodPricce_26")]
        public string FoodPricce_26 { get; set; }
        [Display(Name = "DrinkPrice_26")]
        public string DrinkPrice_26 { get; set; }
        [Display(Name = "Wdrink_26")]
        public string Wdrink_26 { get; set; }
        [Display(Name = "Desl_26")]
        public string Desl_26 { get; set; }
        [Display(Name = "RestRoomFlg_26")]
        public string RestRoomFlg_26 { get; set; }
        [Display(Name = "AnketFlg_26")]
        public string AnketFlg_26 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_26")]
        public string PartyTime_TimeName_I_26 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_26")]
        public string PartyTime_OrderTime_I_26 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_26")]
        public string PartyTime_ActTime_I_26 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_26")]
        public string PartyTime_DelayTime_I_26 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_26")]
        public string PartyTime_TimeName_II_26 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_26")]
        public string PartyTime_OrderTime_II_26 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_26")]
        public string PartyTime_ActTime_II_26 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_26")]
        public string PartyTime_DelayTime_II_26 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_26")]
        public string PartyTime_TimeName_III_26 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_26")]
        public string PartyTime_OrderTime_III_26 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_26")]
        public string PartyTime_ActTime_III_26 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_26")]
        public string PartyTime_DelayTime_III_26 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_26")]
        public string PartyTime_TimeName_IV_26 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_26")]
        public string PartyTime_OrderTime_IV_26 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_26")]
        public string PartyTime_ActTime_IV_26 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_26")]
        public string PartyTime_DelayTime_IV_26 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_26")]
        public string PartyTime_TimeName_V_26 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_26")]
        public string PartyTime_OrderTime_V_26 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_26")]
        public string PartyTime_ActTime_V_26 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_26")]
        public string PartyTime_DelayTime_V_26 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_26")]
        public string PartyFood_FoodName_I_26 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_26")]
        public string PartyFood_BeginTime_I_26 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_26")]
        public string PartyFood_EndTime_I_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_26")]
        public string PartyFood_RestRoomTime_I_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_26")]
        public string PartyFood_RestRoomFlg_I_26 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_26")]
        public string PartyFood_FoodName_II_26 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_26")]
        public string PartyFood_BeginTime_II_26 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_26")]
        public string PartyFood_EndTime_II_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_26")]
        public string PartyFood_RestRoomTime_II_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_26")]
        public string PartyFood_RestRoomFlg_II_26 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_26")]
        public string PartyFood_FoodName_III_26 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_26")]
        public string PartyFood_BeginTime_III_26 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_26")]
        public string PartyFood_EndTime_III_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_26")]
        public string PartyFood_RestRoomTime_III_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_26")]
        public string PartyFood_RestRoomFlg_III_26 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_26")]
        public string PartyFood_FoodName_IV_26 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_26")]
        public string PartyFood_BeginTime_IV_26 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_26")]
        public string PartyFood_EndTime_IV_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_26")]
        public string PartyFood_RestRoomTime_IV_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_26")]
        public string PartyFood_RestRoomFlg_IV_26 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_26")]
        public string PartyFood_FoodName_V_26 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_26")]
        public string PartyFood_BeginTime_V_26 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_26")]
        public string PartyFood_EndTime_V_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_26")]
        public string PartyFood_RestRoomTime_V_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_26")]
        public string PartyFood_RestRoomFlg_V_26 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_26")]
        public string PartyFood_FoodName_VI_26 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_26")]
        public string PartyFood_BeginTime_VI_26 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_26")]
        public string PartyFood_EndTime_VI_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_26")]
        public string PartyFood_RestRoomTime_VI_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_26")]
        public string PartyFood_RestRoomFlg_VI_26 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_26")]
        public string PartyFood_FoodName_VII_26 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_26")]
        public string PartyFood_BeginTime_VII_26 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_26")]
        public string PartyFood_EndTime_VII_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_26")]
        public string PartyFood_RestRoomTime_VII_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_26")]
        public string PartyFood_RestRoomFlg_VII_26 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_26")]
        public string PartyFood_FoodName_VIII_26 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_26")]
        public string PartyFood_BeginTime_VIII_26 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_26")]
        public string PartyFood_EndTime_VIII_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_26")]
        public string PartyFood_RestRoomTime_VIII_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_26")]
        public string PartyFood_RestRoomFlg_VIII_26 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_26")]
        public string PartyFood_FoodName_IX_26 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_26")]
        public string PartyFood_BeginTime_IX_26 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_26")]
        public string PartyFood_EndTime_IX_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_26")]
        public string PartyFood_RestRoomTime_IX_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_26")]
        public string PartyFood_RestRoomFlg_IX_26 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_26")]
        public string PartyFood_FoodName_X_26 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_26")]
        public string PartyFood_BeginTime_X_26 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_26")]
        public string PartyFood_EndTime_X_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_26")]
        public string PartyFood_RestRoomTime_X_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_26")]
        public string PartyFood_RestRoomFlg_X_26 { get; set; }
        [Display(Name = "PartyMember_Name_I_26")]
        public string PartyMember_Name_I_26 { get; set; }
        [Display(Name = "PartyMember_Name_II_26")]
        public string PartyMember_Name_II_26 { get; set; }
        [Display(Name = "PartyMember_Name_III_26")]
        public string PartyMember_Name_III_26 { get; set; }
        [Display(Name = "PartyMember_Name_IV_26")]
        public string PartyMember_Name_IV_26 { get; set; }
        [Display(Name = "PartyMember_Name_V_26")]
        public string PartyMember_Name_V_26 { get; set; }
        [Display(Name = "PartyMember_Name_VI_26")]
        public string PartyMember_Name_VI_26 { get; set; }
        [Display(Name = "PartyMember_Name_VII_26")]
        public string PartyMember_Name_VII_26 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_26")]
        public string PartyMember_Name_VIII_26 { get; set; }
        [Display(Name = "PartyMember_Name_IX_26")]
        public string PartyMember_Name_IX_26 { get; set; }
        [Display(Name = "PartyDate_27")]
        public string PartyDate_27 { get; set; }
        [Display(Name = "BrideFamilyName_27")]
        public string BrideFamilyName_27 { get; set; }
        [Display(Name = "GroomFamilyName_27")]
        public string GroomFamilyName_27 { get; set; }
        [Display(Name = "TantoName_27")]
        public string TantoName_27 { get; set; }
        [Display(Name = "ReporterName_27")]
        public string ReporterName_27 { get; set; }
        [Display(Name = "AdultCnt_27")]
        public string AdultCnt_27 { get; set; }
        [Display(Name = "HalfCnt_27")]
        public string HalfCnt_27 { get; set; }
        [Display(Name = "ChildrenCnt_27")]
        public string ChildrenCnt_27 { get; set; }
        [Display(Name = "SeatOnlyCnt_27")]
        public string SeatOnlyCnt_27 { get; set; }
        [Display(Name = "TableCnt_27")]
        public string TableCnt_27 { get; set; }
        [Display(Name = "TableCross_27")]
        public string TableCross_27 { get; set; }
        [Display(Name = "PartyStyleName_27")]
        public string PartyStyleName_27 { get; set; }
        [Display(Name = "FoodStyleName_27")]
        public string FoodStyleName_27 { get; set; }
        [Display(Name = "FoodPricce_27")]
        public string FoodPricce_27 { get; set; }
        [Display(Name = "DrinkPrice_27")]
        public string DrinkPrice_27 { get; set; }
        [Display(Name = "Wdrink_27")]
        public string Wdrink_27 { get; set; }
        [Display(Name = "Desl_27")]
        public string Desl_27 { get; set; }
        [Display(Name = "RestRoomFlg_27")]
        public string RestRoomFlg_27 { get; set; }
        [Display(Name = "AnketFlg_27")]
        public string AnketFlg_27 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_27")]
        public string PartyTime_TimeName_I_27 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_27")]
        public string PartyTime_OrderTime_I_27 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_27")]
        public string PartyTime_ActTime_I_27 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_27")]
        public string PartyTime_DelayTime_I_27 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_27")]
        public string PartyTime_TimeName_II_27 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_27")]
        public string PartyTime_OrderTime_II_27 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_27")]
        public string PartyTime_ActTime_II_27 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_27")]
        public string PartyTime_DelayTime_II_27 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_27")]
        public string PartyTime_TimeName_III_27 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_27")]
        public string PartyTime_OrderTime_III_27 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_27")]
        public string PartyTime_ActTime_III_27 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_27")]
        public string PartyTime_DelayTime_III_27 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_27")]
        public string PartyTime_TimeName_IV_27 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_27")]
        public string PartyTime_OrderTime_IV_27 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_27")]
        public string PartyTime_ActTime_IV_27 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_27")]
        public string PartyTime_DelayTime_IV_27 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_27")]
        public string PartyTime_TimeName_V_27 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_27")]
        public string PartyTime_OrderTime_V_27 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_27")]
        public string PartyTime_ActTime_V_27 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_27")]
        public string PartyTime_DelayTime_V_27 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_27")]
        public string PartyFood_FoodName_I_27 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_27")]
        public string PartyFood_BeginTime_I_27 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_27")]
        public string PartyFood_EndTime_I_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_27")]
        public string PartyFood_RestRoomTime_I_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_27")]
        public string PartyFood_RestRoomFlg_I_27 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_27")]
        public string PartyFood_FoodName_II_27 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_27")]
        public string PartyFood_BeginTime_II_27 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_27")]
        public string PartyFood_EndTime_II_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_27")]
        public string PartyFood_RestRoomTime_II_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_27")]
        public string PartyFood_RestRoomFlg_II_27 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_27")]
        public string PartyFood_FoodName_III_27 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_27")]
        public string PartyFood_BeginTime_III_27 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_27")]
        public string PartyFood_EndTime_III_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_27")]
        public string PartyFood_RestRoomTime_III_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_27")]
        public string PartyFood_RestRoomFlg_III_27 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_27")]
        public string PartyFood_FoodName_IV_27 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_27")]
        public string PartyFood_BeginTime_IV_27 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_27")]
        public string PartyFood_EndTime_IV_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_27")]
        public string PartyFood_RestRoomTime_IV_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_27")]
        public string PartyFood_RestRoomFlg_IV_27 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_27")]
        public string PartyFood_FoodName_V_27 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_27")]
        public string PartyFood_BeginTime_V_27 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_27")]
        public string PartyFood_EndTime_V_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_27")]
        public string PartyFood_RestRoomTime_V_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_27")]
        public string PartyFood_RestRoomFlg_V_27 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_27")]
        public string PartyFood_FoodName_VI_27 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_27")]
        public string PartyFood_BeginTime_VI_27 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_27")]
        public string PartyFood_EndTime_VI_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_27")]
        public string PartyFood_RestRoomTime_VI_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_27")]
        public string PartyFood_RestRoomFlg_VI_27 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_27")]
        public string PartyFood_FoodName_VII_27 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_27")]
        public string PartyFood_BeginTime_VII_27 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_27")]
        public string PartyFood_EndTime_VII_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_27")]
        public string PartyFood_RestRoomTime_VII_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_27")]
        public string PartyFood_RestRoomFlg_VII_27 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_27")]
        public string PartyFood_FoodName_VIII_27 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_27")]
        public string PartyFood_BeginTime_VIII_27 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_27")]
        public string PartyFood_EndTime_VIII_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_27")]
        public string PartyFood_RestRoomTime_VIII_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_27")]
        public string PartyFood_RestRoomFlg_VIII_27 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_27")]
        public string PartyFood_FoodName_IX_27 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_27")]
        public string PartyFood_BeginTime_IX_27 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_27")]
        public string PartyFood_EndTime_IX_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_27")]
        public string PartyFood_RestRoomTime_IX_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_27")]
        public string PartyFood_RestRoomFlg_IX_27 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_27")]
        public string PartyFood_FoodName_X_27 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_27")]
        public string PartyFood_BeginTime_X_27 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_27")]
        public string PartyFood_EndTime_X_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_27")]
        public string PartyFood_RestRoomTime_X_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_27")]
        public string PartyFood_RestRoomFlg_X_27 { get; set; }
        [Display(Name = "PartyMember_Name_I_27")]
        public string PartyMember_Name_I_27 { get; set; }
        [Display(Name = "PartyMember_Name_II_27")]
        public string PartyMember_Name_II_27 { get; set; }
        [Display(Name = "PartyMember_Name_III_27")]
        public string PartyMember_Name_III_27 { get; set; }
        [Display(Name = "PartyMember_Name_IV_27")]
        public string PartyMember_Name_IV_27 { get; set; }
        [Display(Name = "PartyMember_Name_V_27")]
        public string PartyMember_Name_V_27 { get; set; }
        [Display(Name = "PartyMember_Name_VI_27")]
        public string PartyMember_Name_VI_27 { get; set; }
        [Display(Name = "PartyMember_Name_VII_27")]
        public string PartyMember_Name_VII_27 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_27")]
        public string PartyMember_Name_VIII_27 { get; set; }
        [Display(Name = "PartyMember_Name_IX_27")]
        public string PartyMember_Name_IX_27 { get; set; }
        [Display(Name = "PartyDate_28")]
        public string PartyDate_28 { get; set; }
        [Display(Name = "BrideFamilyName_28")]
        public string BrideFamilyName_28 { get; set; }
        [Display(Name = "GroomFamilyName_28")]
        public string GroomFamilyName_28 { get; set; }
        [Display(Name = "TantoName_28")]
        public string TantoName_28 { get; set; }
        [Display(Name = "ReporterName_28")]
        public string ReporterName_28 { get; set; }
        [Display(Name = "AdultCnt_28")]
        public string AdultCnt_28 { get; set; }
        [Display(Name = "HalfCnt_28")]
        public string HalfCnt_28 { get; set; }
        [Display(Name = "ChildrenCnt_28")]
        public string ChildrenCnt_28 { get; set; }
        [Display(Name = "SeatOnlyCnt_28")]
        public string SeatOnlyCnt_28 { get; set; }
        [Display(Name = "TableCnt_28")]
        public string TableCnt_28 { get; set; }
        [Display(Name = "TableCross_28")]
        public string TableCross_28 { get; set; }
        [Display(Name = "PartyStyleName_28")]
        public string PartyStyleName_28 { get; set; }
        [Display(Name = "FoodStyleName_28")]
        public string FoodStyleName_28 { get; set; }
        [Display(Name = "FoodPricce_28")]
        public string FoodPricce_28 { get; set; }
        [Display(Name = "DrinkPrice_28")]
        public string DrinkPrice_28 { get; set; }
        [Display(Name = "Wdrink_28")]
        public string Wdrink_28 { get; set; }
        [Display(Name = "Desl_28")]
        public string Desl_28 { get; set; }
        [Display(Name = "RestRoomFlg_28")]
        public string RestRoomFlg_28 { get; set; }
        [Display(Name = "AnketFlg_28")]
        public string AnketFlg_28 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_28")]
        public string PartyTime_TimeName_I_28 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_28")]
        public string PartyTime_OrderTime_I_28 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_28")]
        public string PartyTime_ActTime_I_28 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_28")]
        public string PartyTime_DelayTime_I_28 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_28")]
        public string PartyTime_TimeName_II_28 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_28")]
        public string PartyTime_OrderTime_II_28 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_28")]
        public string PartyTime_ActTime_II_28 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_28")]
        public string PartyTime_DelayTime_II_28 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_28")]
        public string PartyTime_TimeName_III_28 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_28")]
        public string PartyTime_OrderTime_III_28 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_28")]
        public string PartyTime_ActTime_III_28 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_28")]
        public string PartyTime_DelayTime_III_28 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_28")]
        public string PartyTime_TimeName_IV_28 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_28")]
        public string PartyTime_OrderTime_IV_28 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_28")]
        public string PartyTime_ActTime_IV_28 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_28")]
        public string PartyTime_DelayTime_IV_28 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_28")]
        public string PartyTime_TimeName_V_28 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_28")]
        public string PartyTime_OrderTime_V_28 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_28")]
        public string PartyTime_ActTime_V_28 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_28")]
        public string PartyTime_DelayTime_V_28 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_28")]
        public string PartyFood_FoodName_I_28 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_28")]
        public string PartyFood_BeginTime_I_28 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_28")]
        public string PartyFood_EndTime_I_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_28")]
        public string PartyFood_RestRoomTime_I_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_28")]
        public string PartyFood_RestRoomFlg_I_28 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_28")]
        public string PartyFood_FoodName_II_28 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_28")]
        public string PartyFood_BeginTime_II_28 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_28")]
        public string PartyFood_EndTime_II_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_28")]
        public string PartyFood_RestRoomTime_II_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_28")]
        public string PartyFood_RestRoomFlg_II_28 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_28")]
        public string PartyFood_FoodName_III_28 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_28")]
        public string PartyFood_BeginTime_III_28 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_28")]
        public string PartyFood_EndTime_III_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_28")]
        public string PartyFood_RestRoomTime_III_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_28")]
        public string PartyFood_RestRoomFlg_III_28 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_28")]
        public string PartyFood_FoodName_IV_28 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_28")]
        public string PartyFood_BeginTime_IV_28 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_28")]
        public string PartyFood_EndTime_IV_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_28")]
        public string PartyFood_RestRoomTime_IV_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_28")]
        public string PartyFood_RestRoomFlg_IV_28 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_28")]
        public string PartyFood_FoodName_V_28 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_28")]
        public string PartyFood_BeginTime_V_28 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_28")]
        public string PartyFood_EndTime_V_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_28")]
        public string PartyFood_RestRoomTime_V_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_28")]
        public string PartyFood_RestRoomFlg_V_28 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_28")]
        public string PartyFood_FoodName_VI_28 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_28")]
        public string PartyFood_BeginTime_VI_28 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_28")]
        public string PartyFood_EndTime_VI_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_28")]
        public string PartyFood_RestRoomTime_VI_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_28")]
        public string PartyFood_RestRoomFlg_VI_28 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_28")]
        public string PartyFood_FoodName_VII_28 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_28")]
        public string PartyFood_BeginTime_VII_28 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_28")]
        public string PartyFood_EndTime_VII_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_28")]
        public string PartyFood_RestRoomTime_VII_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_28")]
        public string PartyFood_RestRoomFlg_VII_28 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_28")]
        public string PartyFood_FoodName_VIII_28 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_28")]
        public string PartyFood_BeginTime_VIII_28 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_28")]
        public string PartyFood_EndTime_VIII_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_28")]
        public string PartyFood_RestRoomTime_VIII_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_28")]
        public string PartyFood_RestRoomFlg_VIII_28 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_28")]
        public string PartyFood_FoodName_IX_28 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_28")]
        public string PartyFood_BeginTime_IX_28 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_28")]
        public string PartyFood_EndTime_IX_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_28")]
        public string PartyFood_RestRoomTime_IX_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_28")]
        public string PartyFood_RestRoomFlg_IX_28 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_28")]
        public string PartyFood_FoodName_X_28 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_28")]
        public string PartyFood_BeginTime_X_28 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_28")]
        public string PartyFood_EndTime_X_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_28")]
        public string PartyFood_RestRoomTime_X_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_28")]
        public string PartyFood_RestRoomFlg_X_28 { get; set; }
        [Display(Name = "PartyMember_Name_I_28")]
        public string PartyMember_Name_I_28 { get; set; }
        [Display(Name = "PartyMember_Name_II_28")]
        public string PartyMember_Name_II_28 { get; set; }
        [Display(Name = "PartyMember_Name_III_28")]
        public string PartyMember_Name_III_28 { get; set; }
        [Display(Name = "PartyMember_Name_IV_28")]
        public string PartyMember_Name_IV_28 { get; set; }
        [Display(Name = "PartyMember_Name_V_28")]
        public string PartyMember_Name_V_28 { get; set; }
        [Display(Name = "PartyMember_Name_VI_28")]
        public string PartyMember_Name_VI_28 { get; set; }
        [Display(Name = "PartyMember_Name_VII_28")]
        public string PartyMember_Name_VII_28 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_28")]
        public string PartyMember_Name_VIII_28 { get; set; }
        [Display(Name = "PartyMember_Name_IX_28")]
        public string PartyMember_Name_IX_28 { get; set; }
        [Display(Name = "PartyDate_29")]
        public string PartyDate_29 { get; set; }
        [Display(Name = "BrideFamilyName_29")]
        public string BrideFamilyName_29 { get; set; }
        [Display(Name = "GroomFamilyName_29")]
        public string GroomFamilyName_29 { get; set; }
        [Display(Name = "TantoName_29")]
        public string TantoName_29 { get; set; }
        [Display(Name = "ReporterName_29")]
        public string ReporterName_29 { get; set; }
        [Display(Name = "AdultCnt_29")]
        public string AdultCnt_29 { get; set; }
        [Display(Name = "HalfCnt_29")]
        public string HalfCnt_29 { get; set; }
        [Display(Name = "ChildrenCnt_29")]
        public string ChildrenCnt_29 { get; set; }
        [Display(Name = "SeatOnlyCnt_29")]
        public string SeatOnlyCnt_29 { get; set; }
        [Display(Name = "TableCnt_29")]
        public string TableCnt_29 { get; set; }
        [Display(Name = "TableCross_29")]
        public string TableCross_29 { get; set; }
        [Display(Name = "PartyStyleName_29")]
        public string PartyStyleName_29 { get; set; }
        [Display(Name = "FoodStyleName_29")]
        public string FoodStyleName_29 { get; set; }
        [Display(Name = "FoodPricce_29")]
        public string FoodPricce_29 { get; set; }
        [Display(Name = "DrinkPrice_29")]
        public string DrinkPrice_29 { get; set; }
        [Display(Name = "Wdrink_29")]
        public string Wdrink_29 { get; set; }
        [Display(Name = "Desl_29")]
        public string Desl_29 { get; set; }
        [Display(Name = "RestRoomFlg_29")]
        public string RestRoomFlg_29 { get; set; }
        [Display(Name = "AnketFlg_29")]
        public string AnketFlg_29 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_29")]
        public string PartyTime_TimeName_I_29 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_29")]
        public string PartyTime_OrderTime_I_29 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_29")]
        public string PartyTime_ActTime_I_29 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_29")]
        public string PartyTime_DelayTime_I_29 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_29")]
        public string PartyTime_TimeName_II_29 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_29")]
        public string PartyTime_OrderTime_II_29 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_29")]
        public string PartyTime_ActTime_II_29 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_29")]
        public string PartyTime_DelayTime_II_29 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_29")]
        public string PartyTime_TimeName_III_29 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_29")]
        public string PartyTime_OrderTime_III_29 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_29")]
        public string PartyTime_ActTime_III_29 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_29")]
        public string PartyTime_DelayTime_III_29 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_29")]
        public string PartyTime_TimeName_IV_29 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_29")]
        public string PartyTime_OrderTime_IV_29 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_29")]
        public string PartyTime_ActTime_IV_29 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_29")]
        public string PartyTime_DelayTime_IV_29 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_29")]
        public string PartyTime_TimeName_V_29 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_29")]
        public string PartyTime_OrderTime_V_29 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_29")]
        public string PartyTime_ActTime_V_29 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_29")]
        public string PartyTime_DelayTime_V_29 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_29")]
        public string PartyFood_FoodName_I_29 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_29")]
        public string PartyFood_BeginTime_I_29 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_29")]
        public string PartyFood_EndTime_I_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_29")]
        public string PartyFood_RestRoomTime_I_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_29")]
        public string PartyFood_RestRoomFlg_I_29 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_29")]
        public string PartyFood_FoodName_II_29 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_29")]
        public string PartyFood_BeginTime_II_29 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_29")]
        public string PartyFood_EndTime_II_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_29")]
        public string PartyFood_RestRoomTime_II_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_29")]
        public string PartyFood_RestRoomFlg_II_29 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_29")]
        public string PartyFood_FoodName_III_29 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_29")]
        public string PartyFood_BeginTime_III_29 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_29")]
        public string PartyFood_EndTime_III_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_29")]
        public string PartyFood_RestRoomTime_III_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_29")]
        public string PartyFood_RestRoomFlg_III_29 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_29")]
        public string PartyFood_FoodName_IV_29 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_29")]
        public string PartyFood_BeginTime_IV_29 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_29")]
        public string PartyFood_EndTime_IV_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_29")]
        public string PartyFood_RestRoomTime_IV_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_29")]
        public string PartyFood_RestRoomFlg_IV_29 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_29")]
        public string PartyFood_FoodName_V_29 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_29")]
        public string PartyFood_BeginTime_V_29 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_29")]
        public string PartyFood_EndTime_V_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_29")]
        public string PartyFood_RestRoomTime_V_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_29")]
        public string PartyFood_RestRoomFlg_V_29 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_29")]
        public string PartyFood_FoodName_VI_29 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_29")]
        public string PartyFood_BeginTime_VI_29 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_29")]
        public string PartyFood_EndTime_VI_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_29")]
        public string PartyFood_RestRoomTime_VI_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_29")]
        public string PartyFood_RestRoomFlg_VI_29 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_29")]
        public string PartyFood_FoodName_VII_29 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_29")]
        public string PartyFood_BeginTime_VII_29 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_29")]
        public string PartyFood_EndTime_VII_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_29")]
        public string PartyFood_RestRoomTime_VII_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_29")]
        public string PartyFood_RestRoomFlg_VII_29 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_29")]
        public string PartyFood_FoodName_VIII_29 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_29")]
        public string PartyFood_BeginTime_VIII_29 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_29")]
        public string PartyFood_EndTime_VIII_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_29")]
        public string PartyFood_RestRoomTime_VIII_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_29")]
        public string PartyFood_RestRoomFlg_VIII_29 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_29")]
        public string PartyFood_FoodName_IX_29 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_29")]
        public string PartyFood_BeginTime_IX_29 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_29")]
        public string PartyFood_EndTime_IX_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_29")]
        public string PartyFood_RestRoomTime_IX_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_29")]
        public string PartyFood_RestRoomFlg_IX_29 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_29")]
        public string PartyFood_FoodName_X_29 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_29")]
        public string PartyFood_BeginTime_X_29 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_29")]
        public string PartyFood_EndTime_X_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_29")]
        public string PartyFood_RestRoomTime_X_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_29")]
        public string PartyFood_RestRoomFlg_X_29 { get; set; }
        [Display(Name = "PartyMember_Name_I_29")]
        public string PartyMember_Name_I_29 { get; set; }
        [Display(Name = "PartyMember_Name_II_29")]
        public string PartyMember_Name_II_29 { get; set; }
        [Display(Name = "PartyMember_Name_III_29")]
        public string PartyMember_Name_III_29 { get; set; }
        [Display(Name = "PartyMember_Name_IV_29")]
        public string PartyMember_Name_IV_29 { get; set; }
        [Display(Name = "PartyMember_Name_V_29")]
        public string PartyMember_Name_V_29 { get; set; }
        [Display(Name = "PartyMember_Name_VI_29")]
        public string PartyMember_Name_VI_29 { get; set; }
        [Display(Name = "PartyMember_Name_VII_29")]
        public string PartyMember_Name_VII_29 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_29")]
        public string PartyMember_Name_VIII_29 { get; set; }
        [Display(Name = "PartyMember_Name_IX_29")]
        public string PartyMember_Name_IX_29 { get; set; }
        [Display(Name = "PartyDate_30")]
        public string PartyDate_30 { get; set; }
        [Display(Name = "BrideFamilyName_30")]
        public string BrideFamilyName_30 { get; set; }
        [Display(Name = "GroomFamilyName_30")]
        public string GroomFamilyName_30 { get; set; }
        [Display(Name = "TantoName_30")]
        public string TantoName_30 { get; set; }
        [Display(Name = "ReporterName_30")]
        public string ReporterName_30 { get; set; }
        [Display(Name = "AdultCnt_30")]
        public string AdultCnt_30 { get; set; }
        [Display(Name = "HalfCnt_30")]
        public string HalfCnt_30 { get; set; }
        [Display(Name = "ChildrenCnt_30")]
        public string ChildrenCnt_30 { get; set; }
        [Display(Name = "SeatOnlyCnt_30")]
        public string SeatOnlyCnt_30 { get; set; }
        [Display(Name = "TableCnt_30")]
        public string TableCnt_30 { get; set; }
        [Display(Name = "TableCross_30")]
        public string TableCross_30 { get; set; }
        [Display(Name = "PartyStyleName_30")]
        public string PartyStyleName_30 { get; set; }
        [Display(Name = "FoodStyleName_30")]
        public string FoodStyleName_30 { get; set; }
        [Display(Name = "FoodPricce_30")]
        public string FoodPricce_30 { get; set; }
        [Display(Name = "DrinkPrice_30")]
        public string DrinkPrice_30 { get; set; }
        [Display(Name = "Wdrink_30")]
        public string Wdrink_30 { get; set; }
        [Display(Name = "Desl_30")]
        public string Desl_30 { get; set; }
        [Display(Name = "RestRoomFlg_30")]
        public string RestRoomFlg_30 { get; set; }
        [Display(Name = "AnketFlg_30")]
        public string AnketFlg_30 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_30")]
        public string PartyTime_TimeName_I_30 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_30")]
        public string PartyTime_OrderTime_I_30 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_30")]
        public string PartyTime_ActTime_I_30 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_30")]
        public string PartyTime_DelayTime_I_30 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_30")]
        public string PartyTime_TimeName_II_30 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_30")]
        public string PartyTime_OrderTime_II_30 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_30")]
        public string PartyTime_ActTime_II_30 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_30")]
        public string PartyTime_DelayTime_II_30 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_30")]
        public string PartyTime_TimeName_III_30 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_30")]
        public string PartyTime_OrderTime_III_30 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_30")]
        public string PartyTime_ActTime_III_30 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_30")]
        public string PartyTime_DelayTime_III_30 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_30")]
        public string PartyTime_TimeName_IV_30 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_30")]
        public string PartyTime_OrderTime_IV_30 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_30")]
        public string PartyTime_ActTime_IV_30 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_30")]
        public string PartyTime_DelayTime_IV_30 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_30")]
        public string PartyTime_TimeName_V_30 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_30")]
        public string PartyTime_OrderTime_V_30 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_30")]
        public string PartyTime_ActTime_V_30 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_30")]
        public string PartyTime_DelayTime_V_30 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_30")]
        public string PartyFood_FoodName_I_30 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_30")]
        public string PartyFood_BeginTime_I_30 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_30")]
        public string PartyFood_EndTime_I_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_30")]
        public string PartyFood_RestRoomTime_I_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_30")]
        public string PartyFood_RestRoomFlg_I_30 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_30")]
        public string PartyFood_FoodName_II_30 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_30")]
        public string PartyFood_BeginTime_II_30 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_30")]
        public string PartyFood_EndTime_II_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_30")]
        public string PartyFood_RestRoomTime_II_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_30")]
        public string PartyFood_RestRoomFlg_II_30 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_30")]
        public string PartyFood_FoodName_III_30 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_30")]
        public string PartyFood_BeginTime_III_30 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_30")]
        public string PartyFood_EndTime_III_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_30")]
        public string PartyFood_RestRoomTime_III_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_30")]
        public string PartyFood_RestRoomFlg_III_30 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_30")]
        public string PartyFood_FoodName_IV_30 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_30")]
        public string PartyFood_BeginTime_IV_30 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_30")]
        public string PartyFood_EndTime_IV_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_30")]
        public string PartyFood_RestRoomTime_IV_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_30")]
        public string PartyFood_RestRoomFlg_IV_30 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_30")]
        public string PartyFood_FoodName_V_30 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_30")]
        public string PartyFood_BeginTime_V_30 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_30")]
        public string PartyFood_EndTime_V_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_30")]
        public string PartyFood_RestRoomTime_V_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_30")]
        public string PartyFood_RestRoomFlg_V_30 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_30")]
        public string PartyFood_FoodName_VI_30 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_30")]
        public string PartyFood_BeginTime_VI_30 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_30")]
        public string PartyFood_EndTime_VI_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_30")]
        public string PartyFood_RestRoomTime_VI_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_30")]
        public string PartyFood_RestRoomFlg_VI_30 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_30")]
        public string PartyFood_FoodName_VII_30 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_30")]
        public string PartyFood_BeginTime_VII_30 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_30")]
        public string PartyFood_EndTime_VII_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_30")]
        public string PartyFood_RestRoomTime_VII_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_30")]
        public string PartyFood_RestRoomFlg_VII_30 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_30")]
        public string PartyFood_FoodName_VIII_30 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_30")]
        public string PartyFood_BeginTime_VIII_30 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_30")]
        public string PartyFood_EndTime_VIII_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_30")]
        public string PartyFood_RestRoomTime_VIII_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_30")]
        public string PartyFood_RestRoomFlg_VIII_30 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_30")]
        public string PartyFood_FoodName_IX_30 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_30")]
        public string PartyFood_BeginTime_IX_30 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_30")]
        public string PartyFood_EndTime_IX_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_30")]
        public string PartyFood_RestRoomTime_IX_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_30")]
        public string PartyFood_RestRoomFlg_IX_30 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_30")]
        public string PartyFood_FoodName_X_30 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_30")]
        public string PartyFood_BeginTime_X_30 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_30")]
        public string PartyFood_EndTime_X_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_30")]
        public string PartyFood_RestRoomTime_X_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_30")]
        public string PartyFood_RestRoomFlg_X_30 { get; set; }
        [Display(Name = "PartyMember_Name_I_30")]
        public string PartyMember_Name_I_30 { get; set; }
        [Display(Name = "PartyMember_Name_II_30")]
        public string PartyMember_Name_II_30 { get; set; }
        [Display(Name = "PartyMember_Name_III_30")]
        public string PartyMember_Name_III_30 { get; set; }
        [Display(Name = "PartyMember_Name_IV_30")]
        public string PartyMember_Name_IV_30 { get; set; }
        [Display(Name = "PartyMember_Name_V_30")]
        public string PartyMember_Name_V_30 { get; set; }
        [Display(Name = "PartyMember_Name_VI_30")]
        public string PartyMember_Name_VI_30 { get; set; }
        [Display(Name = "PartyMember_Name_VII_30")]
        public string PartyMember_Name_VII_30 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_30")]
        public string PartyMember_Name_VIII_30 { get; set; }
        [Display(Name = "PartyMember_Name_IX_30")]
        public string PartyMember_Name_IX_30 { get; set; }
        [Display(Name = "PartyDate_31")]
        public string PartyDate_31 { get; set; }
        [Display(Name = "BrideFamilyName_31")]
        public string BrideFamilyName_31 { get; set; }
        [Display(Name = "GroomFamilyName_31")]
        public string GroomFamilyName_31 { get; set; }
        [Display(Name = "TantoName_31")]
        public string TantoName_31 { get; set; }
        [Display(Name = "ReporterName_31")]
        public string ReporterName_31 { get; set; }
        [Display(Name = "AdultCnt_31")]
        public string AdultCnt_31 { get; set; }
        [Display(Name = "HalfCnt_31")]
        public string HalfCnt_31 { get; set; }
        [Display(Name = "ChildrenCnt_31")]
        public string ChildrenCnt_31 { get; set; }
        [Display(Name = "SeatOnlyCnt_31")]
        public string SeatOnlyCnt_31 { get; set; }
        [Display(Name = "TableCnt_31")]
        public string TableCnt_31 { get; set; }
        [Display(Name = "TableCross_31")]
        public string TableCross_31 { get; set; }
        [Display(Name = "PartyStyleName_31")]
        public string PartyStyleName_31 { get; set; }
        [Display(Name = "FoodStyleName_31")]
        public string FoodStyleName_31 { get; set; }
        [Display(Name = "FoodPricce_31")]
        public string FoodPricce_31 { get; set; }
        [Display(Name = "DrinkPrice_31")]
        public string DrinkPrice_31 { get; set; }
        [Display(Name = "Wdrink_31")]
        public string Wdrink_31 { get; set; }
        [Display(Name = "Desl_31")]
        public string Desl_31 { get; set; }
        [Display(Name = "RestRoomFlg_31")]
        public string RestRoomFlg_31 { get; set; }
        [Display(Name = "AnketFlg_31")]
        public string AnketFlg_31 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_31")]
        public string PartyTime_TimeName_I_31 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_31")]
        public string PartyTime_OrderTime_I_31 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_31")]
        public string PartyTime_ActTime_I_31 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_31")]
        public string PartyTime_DelayTime_I_31 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_31")]
        public string PartyTime_TimeName_II_31 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_31")]
        public string PartyTime_OrderTime_II_31 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_31")]
        public string PartyTime_ActTime_II_31 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_31")]
        public string PartyTime_DelayTime_II_31 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_31")]
        public string PartyTime_TimeName_III_31 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_31")]
        public string PartyTime_OrderTime_III_31 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_31")]
        public string PartyTime_ActTime_III_31 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_31")]
        public string PartyTime_DelayTime_III_31 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_31")]
        public string PartyTime_TimeName_IV_31 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_31")]
        public string PartyTime_OrderTime_IV_31 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_31")]
        public string PartyTime_ActTime_IV_31 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_31")]
        public string PartyTime_DelayTime_IV_31 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_31")]
        public string PartyTime_TimeName_V_31 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_31")]
        public string PartyTime_OrderTime_V_31 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_31")]
        public string PartyTime_ActTime_V_31 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_31")]
        public string PartyTime_DelayTime_V_31 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_31")]
        public string PartyFood_FoodName_I_31 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_31")]
        public string PartyFood_BeginTime_I_31 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_31")]
        public string PartyFood_EndTime_I_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_31")]
        public string PartyFood_RestRoomTime_I_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_31")]
        public string PartyFood_RestRoomFlg_I_31 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_31")]
        public string PartyFood_FoodName_II_31 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_31")]
        public string PartyFood_BeginTime_II_31 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_31")]
        public string PartyFood_EndTime_II_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_31")]
        public string PartyFood_RestRoomTime_II_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_31")]
        public string PartyFood_RestRoomFlg_II_31 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_31")]
        public string PartyFood_FoodName_III_31 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_31")]
        public string PartyFood_BeginTime_III_31 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_31")]
        public string PartyFood_EndTime_III_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_31")]
        public string PartyFood_RestRoomTime_III_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_31")]
        public string PartyFood_RestRoomFlg_III_31 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_31")]
        public string PartyFood_FoodName_IV_31 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_31")]
        public string PartyFood_BeginTime_IV_31 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_31")]
        public string PartyFood_EndTime_IV_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_31")]
        public string PartyFood_RestRoomTime_IV_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_31")]
        public string PartyFood_RestRoomFlg_IV_31 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_31")]
        public string PartyFood_FoodName_V_31 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_31")]
        public string PartyFood_BeginTime_V_31 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_31")]
        public string PartyFood_EndTime_V_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_31")]
        public string PartyFood_RestRoomTime_V_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_31")]
        public string PartyFood_RestRoomFlg_V_31 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_31")]
        public string PartyFood_FoodName_VI_31 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_31")]
        public string PartyFood_BeginTime_VI_31 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_31")]
        public string PartyFood_EndTime_VI_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_31")]
        public string PartyFood_RestRoomTime_VI_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_31")]
        public string PartyFood_RestRoomFlg_VI_31 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_31")]
        public string PartyFood_FoodName_VII_31 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_31")]
        public string PartyFood_BeginTime_VII_31 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_31")]
        public string PartyFood_EndTime_VII_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_31")]
        public string PartyFood_RestRoomTime_VII_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_31")]
        public string PartyFood_RestRoomFlg_VII_31 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_31")]
        public string PartyFood_FoodName_VIII_31 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_31")]
        public string PartyFood_BeginTime_VIII_31 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_31")]
        public string PartyFood_EndTime_VIII_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_31")]
        public string PartyFood_RestRoomTime_VIII_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_31")]
        public string PartyFood_RestRoomFlg_VIII_31 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_31")]
        public string PartyFood_FoodName_IX_31 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_31")]
        public string PartyFood_BeginTime_IX_31 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_31")]
        public string PartyFood_EndTime_IX_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_31")]
        public string PartyFood_RestRoomTime_IX_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_31")]
        public string PartyFood_RestRoomFlg_IX_31 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_31")]
        public string PartyFood_FoodName_X_31 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_31")]
        public string PartyFood_BeginTime_X_31 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_31")]
        public string PartyFood_EndTime_X_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_31")]
        public string PartyFood_RestRoomTime_X_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_31")]
        public string PartyFood_RestRoomFlg_X_31 { get; set; }
        [Display(Name = "PartyMember_Name_I_31")]
        public string PartyMember_Name_I_31 { get; set; }
        [Display(Name = "PartyMember_Name_II_31")]
        public string PartyMember_Name_II_31 { get; set; }
        [Display(Name = "PartyMember_Name_III_31")]
        public string PartyMember_Name_III_31 { get; set; }
        [Display(Name = "PartyMember_Name_IV_31")]
        public string PartyMember_Name_IV_31 { get; set; }
        [Display(Name = "PartyMember_Name_V_31")]
        public string PartyMember_Name_V_31 { get; set; }
        [Display(Name = "PartyMember_Name_VI_31")]
        public string PartyMember_Name_VI_31 { get; set; }
        [Display(Name = "PartyMember_Name_VII_31")]
        public string PartyMember_Name_VII_31 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_31")]
        public string PartyMember_Name_VIII_31 { get; set; }
        [Display(Name = "PartyMember_Name_IX_31")]
        public string PartyMember_Name_IX_31 { get; set; }
        [Display(Name = "PartyDate_32")]
        public string PartyDate_32 { get; set; }
        [Display(Name = "BrideFamilyName_32")]
        public string BrideFamilyName_32 { get; set; }
        [Display(Name = "GroomFamilyName_32")]
        public string GroomFamilyName_32 { get; set; }
        [Display(Name = "TantoName_32")]
        public string TantoName_32 { get; set; }
        [Display(Name = "ReporterName_32")]
        public string ReporterName_32 { get; set; }
        [Display(Name = "AdultCnt_32")]
        public string AdultCnt_32 { get; set; }
        [Display(Name = "HalfCnt_32")]
        public string HalfCnt_32 { get; set; }
        [Display(Name = "ChildrenCnt_32")]
        public string ChildrenCnt_32 { get; set; }
        [Display(Name = "SeatOnlyCnt_32")]
        public string SeatOnlyCnt_32 { get; set; }
        [Display(Name = "TableCnt_32")]
        public string TableCnt_32 { get; set; }
        [Display(Name = "TableCross_32")]
        public string TableCross_32 { get; set; }
        [Display(Name = "PartyStyleName_32")]
        public string PartyStyleName_32 { get; set; }
        [Display(Name = "FoodStyleName_32")]
        public string FoodStyleName_32 { get; set; }
        [Display(Name = "FoodPricce_32")]
        public string FoodPricce_32 { get; set; }
        [Display(Name = "DrinkPrice_32")]
        public string DrinkPrice_32 { get; set; }
        [Display(Name = "Wdrink_32")]
        public string Wdrink_32 { get; set; }
        [Display(Name = "Desl_32")]
        public string Desl_32 { get; set; }
        [Display(Name = "RestRoomFlg_32")]
        public string RestRoomFlg_32 { get; set; }
        [Display(Name = "AnketFlg_32")]
        public string AnketFlg_32 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_32")]
        public string PartyTime_TimeName_I_32 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_32")]
        public string PartyTime_OrderTime_I_32 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_32")]
        public string PartyTime_ActTime_I_32 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_32")]
        public string PartyTime_DelayTime_I_32 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_32")]
        public string PartyTime_TimeName_II_32 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_32")]
        public string PartyTime_OrderTime_II_32 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_32")]
        public string PartyTime_ActTime_II_32 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_32")]
        public string PartyTime_DelayTime_II_32 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_32")]
        public string PartyTime_TimeName_III_32 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_32")]
        public string PartyTime_OrderTime_III_32 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_32")]
        public string PartyTime_ActTime_III_32 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_32")]
        public string PartyTime_DelayTime_III_32 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_32")]
        public string PartyTime_TimeName_IV_32 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_32")]
        public string PartyTime_OrderTime_IV_32 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_32")]
        public string PartyTime_ActTime_IV_32 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_32")]
        public string PartyTime_DelayTime_IV_32 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_32")]
        public string PartyTime_TimeName_V_32 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_32")]
        public string PartyTime_OrderTime_V_32 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_32")]
        public string PartyTime_ActTime_V_32 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_32")]
        public string PartyTime_DelayTime_V_32 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_32")]
        public string PartyFood_FoodName_I_32 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_32")]
        public string PartyFood_BeginTime_I_32 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_32")]
        public string PartyFood_EndTime_I_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_32")]
        public string PartyFood_RestRoomTime_I_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_32")]
        public string PartyFood_RestRoomFlg_I_32 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_32")]
        public string PartyFood_FoodName_II_32 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_32")]
        public string PartyFood_BeginTime_II_32 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_32")]
        public string PartyFood_EndTime_II_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_32")]
        public string PartyFood_RestRoomTime_II_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_32")]
        public string PartyFood_RestRoomFlg_II_32 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_32")]
        public string PartyFood_FoodName_III_32 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_32")]
        public string PartyFood_BeginTime_III_32 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_32")]
        public string PartyFood_EndTime_III_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_32")]
        public string PartyFood_RestRoomTime_III_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_32")]
        public string PartyFood_RestRoomFlg_III_32 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_32")]
        public string PartyFood_FoodName_IV_32 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_32")]
        public string PartyFood_BeginTime_IV_32 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_32")]
        public string PartyFood_EndTime_IV_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_32")]
        public string PartyFood_RestRoomTime_IV_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_32")]
        public string PartyFood_RestRoomFlg_IV_32 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_32")]
        public string PartyFood_FoodName_V_32 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_32")]
        public string PartyFood_BeginTime_V_32 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_32")]
        public string PartyFood_EndTime_V_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_32")]
        public string PartyFood_RestRoomTime_V_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_32")]
        public string PartyFood_RestRoomFlg_V_32 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_32")]
        public string PartyFood_FoodName_VI_32 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_32")]
        public string PartyFood_BeginTime_VI_32 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_32")]
        public string PartyFood_EndTime_VI_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_32")]
        public string PartyFood_RestRoomTime_VI_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_32")]
        public string PartyFood_RestRoomFlg_VI_32 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_32")]
        public string PartyFood_FoodName_VII_32 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_32")]
        public string PartyFood_BeginTime_VII_32 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_32")]
        public string PartyFood_EndTime_VII_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_32")]
        public string PartyFood_RestRoomTime_VII_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_32")]
        public string PartyFood_RestRoomFlg_VII_32 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_32")]
        public string PartyFood_FoodName_VIII_32 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_32")]
        public string PartyFood_BeginTime_VIII_32 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_32")]
        public string PartyFood_EndTime_VIII_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_32")]
        public string PartyFood_RestRoomTime_VIII_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_32")]
        public string PartyFood_RestRoomFlg_VIII_32 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_32")]
        public string PartyFood_FoodName_IX_32 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_32")]
        public string PartyFood_BeginTime_IX_32 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_32")]
        public string PartyFood_EndTime_IX_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_32")]
        public string PartyFood_RestRoomTime_IX_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_32")]
        public string PartyFood_RestRoomFlg_IX_32 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_32")]
        public string PartyFood_FoodName_X_32 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_32")]
        public string PartyFood_BeginTime_X_32 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_32")]
        public string PartyFood_EndTime_X_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_32")]
        public string PartyFood_RestRoomTime_X_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_32")]
        public string PartyFood_RestRoomFlg_X_32 { get; set; }
        [Display(Name = "PartyMember_Name_I_32")]
        public string PartyMember_Name_I_32 { get; set; }
        [Display(Name = "PartyMember_Name_II_32")]
        public string PartyMember_Name_II_32 { get; set; }
        [Display(Name = "PartyMember_Name_III_32")]
        public string PartyMember_Name_III_32 { get; set; }
        [Display(Name = "PartyMember_Name_IV_32")]
        public string PartyMember_Name_IV_32 { get; set; }
        [Display(Name = "PartyMember_Name_V_32")]
        public string PartyMember_Name_V_32 { get; set; }
        [Display(Name = "PartyMember_Name_VI_32")]
        public string PartyMember_Name_VI_32 { get; set; }
        [Display(Name = "PartyMember_Name_VII_32")]
        public string PartyMember_Name_VII_32 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_32")]
        public string PartyMember_Name_VIII_32 { get; set; }
        [Display(Name = "PartyMember_Name_IX_32")]
        public string PartyMember_Name_IX_32 { get; set; }
        [Display(Name = "PartyDate_33")]
        public string PartyDate_33 { get; set; }
        [Display(Name = "BrideFamilyName_33")]
        public string BrideFamilyName_33 { get; set; }
        [Display(Name = "GroomFamilyName_33")]
        public string GroomFamilyName_33 { get; set; }
        [Display(Name = "TantoName_33")]
        public string TantoName_33 { get; set; }
        [Display(Name = "ReporterName_33")]
        public string ReporterName_33 { get; set; }
        [Display(Name = "AdultCnt_33")]
        public string AdultCnt_33 { get; set; }
        [Display(Name = "HalfCnt_33")]
        public string HalfCnt_33 { get; set; }
        [Display(Name = "ChildrenCnt_33")]
        public string ChildrenCnt_33 { get; set; }
        [Display(Name = "SeatOnlyCnt_33")]
        public string SeatOnlyCnt_33 { get; set; }
        [Display(Name = "TableCnt_33")]
        public string TableCnt_33 { get; set; }
        [Display(Name = "TableCross_33")]
        public string TableCross_33 { get; set; }
        [Display(Name = "PartyStyleName_33")]
        public string PartyStyleName_33 { get; set; }
        [Display(Name = "FoodStyleName_33")]
        public string FoodStyleName_33 { get; set; }
        [Display(Name = "FoodPricce_33")]
        public string FoodPricce_33 { get; set; }
        [Display(Name = "DrinkPrice_33")]
        public string DrinkPrice_33 { get; set; }
        [Display(Name = "Wdrink_33")]
        public string Wdrink_33 { get; set; }
        [Display(Name = "Desl_33")]
        public string Desl_33 { get; set; }
        [Display(Name = "RestRoomFlg_33")]
        public string RestRoomFlg_33 { get; set; }
        [Display(Name = "AnketFlg_33")]
        public string AnketFlg_33 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_33")]
        public string PartyTime_TimeName_I_33 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_33")]
        public string PartyTime_OrderTime_I_33 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_33")]
        public string PartyTime_ActTime_I_33 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_33")]
        public string PartyTime_DelayTime_I_33 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_33")]
        public string PartyTime_TimeName_II_33 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_33")]
        public string PartyTime_OrderTime_II_33 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_33")]
        public string PartyTime_ActTime_II_33 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_33")]
        public string PartyTime_DelayTime_II_33 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_33")]
        public string PartyTime_TimeName_III_33 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_33")]
        public string PartyTime_OrderTime_III_33 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_33")]
        public string PartyTime_ActTime_III_33 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_33")]
        public string PartyTime_DelayTime_III_33 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_33")]
        public string PartyTime_TimeName_IV_33 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_33")]
        public string PartyTime_OrderTime_IV_33 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_33")]
        public string PartyTime_ActTime_IV_33 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_33")]
        public string PartyTime_DelayTime_IV_33 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_33")]
        public string PartyTime_TimeName_V_33 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_33")]
        public string PartyTime_OrderTime_V_33 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_33")]
        public string PartyTime_ActTime_V_33 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_33")]
        public string PartyTime_DelayTime_V_33 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_33")]
        public string PartyFood_FoodName_I_33 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_33")]
        public string PartyFood_BeginTime_I_33 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_33")]
        public string PartyFood_EndTime_I_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_33")]
        public string PartyFood_RestRoomTime_I_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_33")]
        public string PartyFood_RestRoomFlg_I_33 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_33")]
        public string PartyFood_FoodName_II_33 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_33")]
        public string PartyFood_BeginTime_II_33 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_33")]
        public string PartyFood_EndTime_II_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_33")]
        public string PartyFood_RestRoomTime_II_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_33")]
        public string PartyFood_RestRoomFlg_II_33 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_33")]
        public string PartyFood_FoodName_III_33 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_33")]
        public string PartyFood_BeginTime_III_33 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_33")]
        public string PartyFood_EndTime_III_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_33")]
        public string PartyFood_RestRoomTime_III_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_33")]
        public string PartyFood_RestRoomFlg_III_33 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_33")]
        public string PartyFood_FoodName_IV_33 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_33")]
        public string PartyFood_BeginTime_IV_33 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_33")]
        public string PartyFood_EndTime_IV_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_33")]
        public string PartyFood_RestRoomTime_IV_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_33")]
        public string PartyFood_RestRoomFlg_IV_33 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_33")]
        public string PartyFood_FoodName_V_33 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_33")]
        public string PartyFood_BeginTime_V_33 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_33")]
        public string PartyFood_EndTime_V_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_33")]
        public string PartyFood_RestRoomTime_V_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_33")]
        public string PartyFood_RestRoomFlg_V_33 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_33")]
        public string PartyFood_FoodName_VI_33 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_33")]
        public string PartyFood_BeginTime_VI_33 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_33")]
        public string PartyFood_EndTime_VI_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_33")]
        public string PartyFood_RestRoomTime_VI_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_33")]
        public string PartyFood_RestRoomFlg_VI_33 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_33")]
        public string PartyFood_FoodName_VII_33 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_33")]
        public string PartyFood_BeginTime_VII_33 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_33")]
        public string PartyFood_EndTime_VII_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_33")]
        public string PartyFood_RestRoomTime_VII_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_33")]
        public string PartyFood_RestRoomFlg_VII_33 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_33")]
        public string PartyFood_FoodName_VIII_33 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_33")]
        public string PartyFood_BeginTime_VIII_33 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_33")]
        public string PartyFood_EndTime_VIII_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_33")]
        public string PartyFood_RestRoomTime_VIII_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_33")]
        public string PartyFood_RestRoomFlg_VIII_33 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_33")]
        public string PartyFood_FoodName_IX_33 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_33")]
        public string PartyFood_BeginTime_IX_33 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_33")]
        public string PartyFood_EndTime_IX_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_33")]
        public string PartyFood_RestRoomTime_IX_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_33")]
        public string PartyFood_RestRoomFlg_IX_33 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_33")]
        public string PartyFood_FoodName_X_33 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_33")]
        public string PartyFood_BeginTime_X_33 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_33")]
        public string PartyFood_EndTime_X_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_33")]
        public string PartyFood_RestRoomTime_X_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_33")]
        public string PartyFood_RestRoomFlg_X_33 { get; set; }
        [Display(Name = "PartyMember_Name_I_33")]
        public string PartyMember_Name_I_33 { get; set; }
        [Display(Name = "PartyMember_Name_II_33")]
        public string PartyMember_Name_II_33 { get; set; }
        [Display(Name = "PartyMember_Name_III_33")]
        public string PartyMember_Name_III_33 { get; set; }
        [Display(Name = "PartyMember_Name_IV_33")]
        public string PartyMember_Name_IV_33 { get; set; }
        [Display(Name = "PartyMember_Name_V_33")]
        public string PartyMember_Name_V_33 { get; set; }
        [Display(Name = "PartyMember_Name_VI_33")]
        public string PartyMember_Name_VI_33 { get; set; }
        [Display(Name = "PartyMember_Name_VII_33")]
        public string PartyMember_Name_VII_33 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_33")]
        public string PartyMember_Name_VIII_33 { get; set; }
        [Display(Name = "PartyMember_Name_IX_33")]
        public string PartyMember_Name_IX_33 { get; set; }
        [Display(Name = "PartyDate_34")]
        public string PartyDate_34 { get; set; }
        [Display(Name = "BrideFamilyName_34")]
        public string BrideFamilyName_34 { get; set; }
        [Display(Name = "GroomFamilyName_34")]
        public string GroomFamilyName_34 { get; set; }
        [Display(Name = "TantoName_34")]
        public string TantoName_34 { get; set; }
        [Display(Name = "ReporterName_34")]
        public string ReporterName_34 { get; set; }
        [Display(Name = "AdultCnt_34")]
        public string AdultCnt_34 { get; set; }
        [Display(Name = "HalfCnt_34")]
        public string HalfCnt_34 { get; set; }
        [Display(Name = "ChildrenCnt_34")]
        public string ChildrenCnt_34 { get; set; }
        [Display(Name = "SeatOnlyCnt_34")]
        public string SeatOnlyCnt_34 { get; set; }
        [Display(Name = "TableCnt_34")]
        public string TableCnt_34 { get; set; }
        [Display(Name = "TableCross_34")]
        public string TableCross_34 { get; set; }
        [Display(Name = "PartyStyleName_34")]
        public string PartyStyleName_34 { get; set; }
        [Display(Name = "FoodStyleName_34")]
        public string FoodStyleName_34 { get; set; }
        [Display(Name = "FoodPricce_34")]
        public string FoodPricce_34 { get; set; }
        [Display(Name = "DrinkPrice_34")]
        public string DrinkPrice_34 { get; set; }
        [Display(Name = "Wdrink_34")]
        public string Wdrink_34 { get; set; }
        [Display(Name = "Desl_34")]
        public string Desl_34 { get; set; }
        [Display(Name = "RestRoomFlg_34")]
        public string RestRoomFlg_34 { get; set; }
        [Display(Name = "AnketFlg_34")]
        public string AnketFlg_34 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_34")]
        public string PartyTime_TimeName_I_34 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_34")]
        public string PartyTime_OrderTime_I_34 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_34")]
        public string PartyTime_ActTime_I_34 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_34")]
        public string PartyTime_DelayTime_I_34 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_34")]
        public string PartyTime_TimeName_II_34 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_34")]
        public string PartyTime_OrderTime_II_34 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_34")]
        public string PartyTime_ActTime_II_34 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_34")]
        public string PartyTime_DelayTime_II_34 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_34")]
        public string PartyTime_TimeName_III_34 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_34")]
        public string PartyTime_OrderTime_III_34 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_34")]
        public string PartyTime_ActTime_III_34 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_34")]
        public string PartyTime_DelayTime_III_34 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_34")]
        public string PartyTime_TimeName_IV_34 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_34")]
        public string PartyTime_OrderTime_IV_34 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_34")]
        public string PartyTime_ActTime_IV_34 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_34")]
        public string PartyTime_DelayTime_IV_34 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_34")]
        public string PartyTime_TimeName_V_34 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_34")]
        public string PartyTime_OrderTime_V_34 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_34")]
        public string PartyTime_ActTime_V_34 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_34")]
        public string PartyTime_DelayTime_V_34 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_34")]
        public string PartyFood_FoodName_I_34 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_34")]
        public string PartyFood_BeginTime_I_34 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_34")]
        public string PartyFood_EndTime_I_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_34")]
        public string PartyFood_RestRoomTime_I_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_34")]
        public string PartyFood_RestRoomFlg_I_34 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_34")]
        public string PartyFood_FoodName_II_34 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_34")]
        public string PartyFood_BeginTime_II_34 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_34")]
        public string PartyFood_EndTime_II_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_34")]
        public string PartyFood_RestRoomTime_II_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_34")]
        public string PartyFood_RestRoomFlg_II_34 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_34")]
        public string PartyFood_FoodName_III_34 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_34")]
        public string PartyFood_BeginTime_III_34 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_34")]
        public string PartyFood_EndTime_III_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_34")]
        public string PartyFood_RestRoomTime_III_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_34")]
        public string PartyFood_RestRoomFlg_III_34 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_34")]
        public string PartyFood_FoodName_IV_34 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_34")]
        public string PartyFood_BeginTime_IV_34 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_34")]
        public string PartyFood_EndTime_IV_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_34")]
        public string PartyFood_RestRoomTime_IV_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_34")]
        public string PartyFood_RestRoomFlg_IV_34 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_34")]
        public string PartyFood_FoodName_V_34 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_34")]
        public string PartyFood_BeginTime_V_34 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_34")]
        public string PartyFood_EndTime_V_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_34")]
        public string PartyFood_RestRoomTime_V_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_34")]
        public string PartyFood_RestRoomFlg_V_34 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_34")]
        public string PartyFood_FoodName_VI_34 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_34")]
        public string PartyFood_BeginTime_VI_34 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_34")]
        public string PartyFood_EndTime_VI_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_34")]
        public string PartyFood_RestRoomTime_VI_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_34")]
        public string PartyFood_RestRoomFlg_VI_34 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_34")]
        public string PartyFood_FoodName_VII_34 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_34")]
        public string PartyFood_BeginTime_VII_34 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_34")]
        public string PartyFood_EndTime_VII_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_34")]
        public string PartyFood_RestRoomTime_VII_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_34")]
        public string PartyFood_RestRoomFlg_VII_34 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_34")]
        public string PartyFood_FoodName_VIII_34 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_34")]
        public string PartyFood_BeginTime_VIII_34 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_34")]
        public string PartyFood_EndTime_VIII_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_34")]
        public string PartyFood_RestRoomTime_VIII_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_34")]
        public string PartyFood_RestRoomFlg_VIII_34 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_34")]
        public string PartyFood_FoodName_IX_34 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_34")]
        public string PartyFood_BeginTime_IX_34 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_34")]
        public string PartyFood_EndTime_IX_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_34")]
        public string PartyFood_RestRoomTime_IX_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_34")]
        public string PartyFood_RestRoomFlg_IX_34 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_34")]
        public string PartyFood_FoodName_X_34 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_34")]
        public string PartyFood_BeginTime_X_34 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_34")]
        public string PartyFood_EndTime_X_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_34")]
        public string PartyFood_RestRoomTime_X_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_34")]
        public string PartyFood_RestRoomFlg_X_34 { get; set; }
        [Display(Name = "PartyMember_Name_I_34")]
        public string PartyMember_Name_I_34 { get; set; }
        [Display(Name = "PartyMember_Name_II_34")]
        public string PartyMember_Name_II_34 { get; set; }
        [Display(Name = "PartyMember_Name_III_34")]
        public string PartyMember_Name_III_34 { get; set; }
        [Display(Name = "PartyMember_Name_IV_34")]
        public string PartyMember_Name_IV_34 { get; set; }
        [Display(Name = "PartyMember_Name_V_34")]
        public string PartyMember_Name_V_34 { get; set; }
        [Display(Name = "PartyMember_Name_VI_34")]
        public string PartyMember_Name_VI_34 { get; set; }
        [Display(Name = "PartyMember_Name_VII_34")]
        public string PartyMember_Name_VII_34 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_34")]
        public string PartyMember_Name_VIII_34 { get; set; }
        [Display(Name = "PartyMember_Name_IX_34")]
        public string PartyMember_Name_IX_34 { get; set; }
        [Display(Name = "PartyDate_35")]
        public string PartyDate_35 { get; set; }
        [Display(Name = "BrideFamilyName_35")]
        public string BrideFamilyName_35 { get; set; }
        [Display(Name = "GroomFamilyName_35")]
        public string GroomFamilyName_35 { get; set; }
        [Display(Name = "TantoName_35")]
        public string TantoName_35 { get; set; }
        [Display(Name = "ReporterName_35")]
        public string ReporterName_35 { get; set; }
        [Display(Name = "AdultCnt_35")]
        public string AdultCnt_35 { get; set; }
        [Display(Name = "HalfCnt_35")]
        public string HalfCnt_35 { get; set; }
        [Display(Name = "ChildrenCnt_35")]
        public string ChildrenCnt_35 { get; set; }
        [Display(Name = "SeatOnlyCnt_35")]
        public string SeatOnlyCnt_35 { get; set; }
        [Display(Name = "TableCnt_35")]
        public string TableCnt_35 { get; set; }
        [Display(Name = "TableCross_35")]
        public string TableCross_35 { get; set; }
        [Display(Name = "PartyStyleName_35")]
        public string PartyStyleName_35 { get; set; }
        [Display(Name = "FoodStyleName_35")]
        public string FoodStyleName_35 { get; set; }
        [Display(Name = "FoodPricce_35")]
        public string FoodPricce_35 { get; set; }
        [Display(Name = "DrinkPrice_35")]
        public string DrinkPrice_35 { get; set; }
        [Display(Name = "Wdrink_35")]
        public string Wdrink_35 { get; set; }
        [Display(Name = "Desl_35")]
        public string Desl_35 { get; set; }
        [Display(Name = "RestRoomFlg_35")]
        public string RestRoomFlg_35 { get; set; }
        [Display(Name = "AnketFlg_35")]
        public string AnketFlg_35 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_35")]
        public string PartyTime_TimeName_I_35 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_35")]
        public string PartyTime_OrderTime_I_35 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_35")]
        public string PartyTime_ActTime_I_35 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_35")]
        public string PartyTime_DelayTime_I_35 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_35")]
        public string PartyTime_TimeName_II_35 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_35")]
        public string PartyTime_OrderTime_II_35 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_35")]
        public string PartyTime_ActTime_II_35 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_35")]
        public string PartyTime_DelayTime_II_35 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_35")]
        public string PartyTime_TimeName_III_35 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_35")]
        public string PartyTime_OrderTime_III_35 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_35")]
        public string PartyTime_ActTime_III_35 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_35")]
        public string PartyTime_DelayTime_III_35 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_35")]
        public string PartyTime_TimeName_IV_35 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_35")]
        public string PartyTime_OrderTime_IV_35 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_35")]
        public string PartyTime_ActTime_IV_35 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_35")]
        public string PartyTime_DelayTime_IV_35 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_35")]
        public string PartyTime_TimeName_V_35 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_35")]
        public string PartyTime_OrderTime_V_35 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_35")]
        public string PartyTime_ActTime_V_35 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_35")]
        public string PartyTime_DelayTime_V_35 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_35")]
        public string PartyFood_FoodName_I_35 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_35")]
        public string PartyFood_BeginTime_I_35 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_35")]
        public string PartyFood_EndTime_I_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_35")]
        public string PartyFood_RestRoomTime_I_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_35")]
        public string PartyFood_RestRoomFlg_I_35 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_35")]
        public string PartyFood_FoodName_II_35 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_35")]
        public string PartyFood_BeginTime_II_35 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_35")]
        public string PartyFood_EndTime_II_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_35")]
        public string PartyFood_RestRoomTime_II_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_35")]
        public string PartyFood_RestRoomFlg_II_35 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_35")]
        public string PartyFood_FoodName_III_35 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_35")]
        public string PartyFood_BeginTime_III_35 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_35")]
        public string PartyFood_EndTime_III_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_35")]
        public string PartyFood_RestRoomTime_III_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_35")]
        public string PartyFood_RestRoomFlg_III_35 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_35")]
        public string PartyFood_FoodName_IV_35 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_35")]
        public string PartyFood_BeginTime_IV_35 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_35")]
        public string PartyFood_EndTime_IV_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_35")]
        public string PartyFood_RestRoomTime_IV_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_35")]
        public string PartyFood_RestRoomFlg_IV_35 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_35")]
        public string PartyFood_FoodName_V_35 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_35")]
        public string PartyFood_BeginTime_V_35 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_35")]
        public string PartyFood_EndTime_V_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_35")]
        public string PartyFood_RestRoomTime_V_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_35")]
        public string PartyFood_RestRoomFlg_V_35 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_35")]
        public string PartyFood_FoodName_VI_35 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_35")]
        public string PartyFood_BeginTime_VI_35 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_35")]
        public string PartyFood_EndTime_VI_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_35")]
        public string PartyFood_RestRoomTime_VI_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_35")]
        public string PartyFood_RestRoomFlg_VI_35 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_35")]
        public string PartyFood_FoodName_VII_35 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_35")]
        public string PartyFood_BeginTime_VII_35 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_35")]
        public string PartyFood_EndTime_VII_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_35")]
        public string PartyFood_RestRoomTime_VII_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_35")]
        public string PartyFood_RestRoomFlg_VII_35 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_35")]
        public string PartyFood_FoodName_VIII_35 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_35")]
        public string PartyFood_BeginTime_VIII_35 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_35")]
        public string PartyFood_EndTime_VIII_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_35")]
        public string PartyFood_RestRoomTime_VIII_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_35")]
        public string PartyFood_RestRoomFlg_VIII_35 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_35")]
        public string PartyFood_FoodName_IX_35 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_35")]
        public string PartyFood_BeginTime_IX_35 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_35")]
        public string PartyFood_EndTime_IX_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_35")]
        public string PartyFood_RestRoomTime_IX_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_35")]
        public string PartyFood_RestRoomFlg_IX_35 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_35")]
        public string PartyFood_FoodName_X_35 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_35")]
        public string PartyFood_BeginTime_X_35 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_35")]
        public string PartyFood_EndTime_X_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_35")]
        public string PartyFood_RestRoomTime_X_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_35")]
        public string PartyFood_RestRoomFlg_X_35 { get; set; }
        [Display(Name = "PartyMember_Name_I_35")]
        public string PartyMember_Name_I_35 { get; set; }
        [Display(Name = "PartyMember_Name_II_35")]
        public string PartyMember_Name_II_35 { get; set; }
        [Display(Name = "PartyMember_Name_III_35")]
        public string PartyMember_Name_III_35 { get; set; }
        [Display(Name = "PartyMember_Name_IV_35")]
        public string PartyMember_Name_IV_35 { get; set; }
        [Display(Name = "PartyMember_Name_V_35")]
        public string PartyMember_Name_V_35 { get; set; }
        [Display(Name = "PartyMember_Name_VI_35")]
        public string PartyMember_Name_VI_35 { get; set; }
        [Display(Name = "PartyMember_Name_VII_35")]
        public string PartyMember_Name_VII_35 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_35")]
        public string PartyMember_Name_VIII_35 { get; set; }
        [Display(Name = "PartyMember_Name_IX_35")]
        public string PartyMember_Name_IX_35 { get; set; }
        [Display(Name = "PartyDate_36")]
        public string PartyDate_36 { get; set; }
        [Display(Name = "BrideFamilyName_36")]
        public string BrideFamilyName_36 { get; set; }
        [Display(Name = "GroomFamilyName_36")]
        public string GroomFamilyName_36 { get; set; }
        [Display(Name = "TantoName_36")]
        public string TantoName_36 { get; set; }
        [Display(Name = "ReporterName_36")]
        public string ReporterName_36 { get; set; }
        [Display(Name = "AdultCnt_36")]
        public string AdultCnt_36 { get; set; }
        [Display(Name = "HalfCnt_36")]
        public string HalfCnt_36 { get; set; }
        [Display(Name = "ChildrenCnt_36")]
        public string ChildrenCnt_36 { get; set; }
        [Display(Name = "SeatOnlyCnt_36")]
        public string SeatOnlyCnt_36 { get; set; }
        [Display(Name = "TableCnt_36")]
        public string TableCnt_36 { get; set; }
        [Display(Name = "TableCross_36")]
        public string TableCross_36 { get; set; }
        [Display(Name = "PartyStyleName_36")]
        public string PartyStyleName_36 { get; set; }
        [Display(Name = "FoodStyleName_36")]
        public string FoodStyleName_36 { get; set; }
        [Display(Name = "FoodPricce_36")]
        public string FoodPricce_36 { get; set; }
        [Display(Name = "DrinkPrice_36")]
        public string DrinkPrice_36 { get; set; }
        [Display(Name = "Wdrink_36")]
        public string Wdrink_36 { get; set; }
        [Display(Name = "Desl_36")]
        public string Desl_36 { get; set; }
        [Display(Name = "RestRoomFlg_36")]
        public string RestRoomFlg_36 { get; set; }
        [Display(Name = "AnketFlg_36")]
        public string AnketFlg_36 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_36")]
        public string PartyTime_TimeName_I_36 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_36")]
        public string PartyTime_OrderTime_I_36 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_36")]
        public string PartyTime_ActTime_I_36 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_36")]
        public string PartyTime_DelayTime_I_36 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_36")]
        public string PartyTime_TimeName_II_36 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_36")]
        public string PartyTime_OrderTime_II_36 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_36")]
        public string PartyTime_ActTime_II_36 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_36")]
        public string PartyTime_DelayTime_II_36 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_36")]
        public string PartyTime_TimeName_III_36 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_36")]
        public string PartyTime_OrderTime_III_36 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_36")]
        public string PartyTime_ActTime_III_36 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_36")]
        public string PartyTime_DelayTime_III_36 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_36")]
        public string PartyTime_TimeName_IV_36 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_36")]
        public string PartyTime_OrderTime_IV_36 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_36")]
        public string PartyTime_ActTime_IV_36 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_36")]
        public string PartyTime_DelayTime_IV_36 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_36")]
        public string PartyTime_TimeName_V_36 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_36")]
        public string PartyTime_OrderTime_V_36 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_36")]
        public string PartyTime_ActTime_V_36 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_36")]
        public string PartyTime_DelayTime_V_36 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_36")]
        public string PartyFood_FoodName_I_36 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_36")]
        public string PartyFood_BeginTime_I_36 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_36")]
        public string PartyFood_EndTime_I_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_36")]
        public string PartyFood_RestRoomTime_I_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_36")]
        public string PartyFood_RestRoomFlg_I_36 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_36")]
        public string PartyFood_FoodName_II_36 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_36")]
        public string PartyFood_BeginTime_II_36 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_36")]
        public string PartyFood_EndTime_II_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_36")]
        public string PartyFood_RestRoomTime_II_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_36")]
        public string PartyFood_RestRoomFlg_II_36 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_36")]
        public string PartyFood_FoodName_III_36 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_36")]
        public string PartyFood_BeginTime_III_36 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_36")]
        public string PartyFood_EndTime_III_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_36")]
        public string PartyFood_RestRoomTime_III_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_36")]
        public string PartyFood_RestRoomFlg_III_36 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_36")]
        public string PartyFood_FoodName_IV_36 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_36")]
        public string PartyFood_BeginTime_IV_36 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_36")]
        public string PartyFood_EndTime_IV_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_36")]
        public string PartyFood_RestRoomTime_IV_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_36")]
        public string PartyFood_RestRoomFlg_IV_36 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_36")]
        public string PartyFood_FoodName_V_36 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_36")]
        public string PartyFood_BeginTime_V_36 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_36")]
        public string PartyFood_EndTime_V_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_36")]
        public string PartyFood_RestRoomTime_V_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_36")]
        public string PartyFood_RestRoomFlg_V_36 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_36")]
        public string PartyFood_FoodName_VI_36 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_36")]
        public string PartyFood_BeginTime_VI_36 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_36")]
        public string PartyFood_EndTime_VI_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_36")]
        public string PartyFood_RestRoomTime_VI_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_36")]
        public string PartyFood_RestRoomFlg_VI_36 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_36")]
        public string PartyFood_FoodName_VII_36 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_36")]
        public string PartyFood_BeginTime_VII_36 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_36")]
        public string PartyFood_EndTime_VII_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_36")]
        public string PartyFood_RestRoomTime_VII_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_36")]
        public string PartyFood_RestRoomFlg_VII_36 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_36")]
        public string PartyFood_FoodName_VIII_36 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_36")]
        public string PartyFood_BeginTime_VIII_36 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_36")]
        public string PartyFood_EndTime_VIII_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_36")]
        public string PartyFood_RestRoomTime_VIII_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_36")]
        public string PartyFood_RestRoomFlg_VIII_36 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_36")]
        public string PartyFood_FoodName_IX_36 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_36")]
        public string PartyFood_BeginTime_IX_36 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_36")]
        public string PartyFood_EndTime_IX_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_36")]
        public string PartyFood_RestRoomTime_IX_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_36")]
        public string PartyFood_RestRoomFlg_IX_36 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_36")]
        public string PartyFood_FoodName_X_36 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_36")]
        public string PartyFood_BeginTime_X_36 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_36")]
        public string PartyFood_EndTime_X_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_36")]
        public string PartyFood_RestRoomTime_X_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_36")]
        public string PartyFood_RestRoomFlg_X_36 { get; set; }
        [Display(Name = "PartyMember_Name_I_36")]
        public string PartyMember_Name_I_36 { get; set; }
        [Display(Name = "PartyMember_Name_II_36")]
        public string PartyMember_Name_II_36 { get; set; }
        [Display(Name = "PartyMember_Name_III_36")]
        public string PartyMember_Name_III_36 { get; set; }
        [Display(Name = "PartyMember_Name_IV_36")]
        public string PartyMember_Name_IV_36 { get; set; }
        [Display(Name = "PartyMember_Name_V_36")]
        public string PartyMember_Name_V_36 { get; set; }
        [Display(Name = "PartyMember_Name_VI_36")]
        public string PartyMember_Name_VI_36 { get; set; }
        [Display(Name = "PartyMember_Name_VII_36")]
        public string PartyMember_Name_VII_36 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_36")]
        public string PartyMember_Name_VIII_36 { get; set; }
        [Display(Name = "PartyMember_Name_IX_36")]
        public string PartyMember_Name_IX_36 { get; set; }
        [Display(Name = "PartyDate_37")]
        public string PartyDate_37 { get; set; }
        [Display(Name = "BrideFamilyName_37")]
        public string BrideFamilyName_37 { get; set; }
        [Display(Name = "GroomFamilyName_37")]
        public string GroomFamilyName_37 { get; set; }
        [Display(Name = "TantoName_37")]
        public string TantoName_37 { get; set; }
        [Display(Name = "ReporterName_37")]
        public string ReporterName_37 { get; set; }
        [Display(Name = "AdultCnt_37")]
        public string AdultCnt_37 { get; set; }
        [Display(Name = "HalfCnt_37")]
        public string HalfCnt_37 { get; set; }
        [Display(Name = "ChildrenCnt_37")]
        public string ChildrenCnt_37 { get; set; }
        [Display(Name = "SeatOnlyCnt_37")]
        public string SeatOnlyCnt_37 { get; set; }
        [Display(Name = "TableCnt_37")]
        public string TableCnt_37 { get; set; }
        [Display(Name = "TableCross_37")]
        public string TableCross_37 { get; set; }
        [Display(Name = "PartyStyleName_37")]
        public string PartyStyleName_37 { get; set; }
        [Display(Name = "FoodStyleName_37")]
        public string FoodStyleName_37 { get; set; }
        [Display(Name = "FoodPricce_37")]
        public string FoodPricce_37 { get; set; }
        [Display(Name = "DrinkPrice_37")]
        public string DrinkPrice_37 { get; set; }
        [Display(Name = "Wdrink_37")]
        public string Wdrink_37 { get; set; }
        [Display(Name = "Desl_37")]
        public string Desl_37 { get; set; }
        [Display(Name = "RestRoomFlg_37")]
        public string RestRoomFlg_37 { get; set; }
        [Display(Name = "AnketFlg_37")]
        public string AnketFlg_37 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_37")]
        public string PartyTime_TimeName_I_37 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_37")]
        public string PartyTime_OrderTime_I_37 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_37")]
        public string PartyTime_ActTime_I_37 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_37")]
        public string PartyTime_DelayTime_I_37 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_37")]
        public string PartyTime_TimeName_II_37 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_37")]
        public string PartyTime_OrderTime_II_37 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_37")]
        public string PartyTime_ActTime_II_37 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_37")]
        public string PartyTime_DelayTime_II_37 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_37")]
        public string PartyTime_TimeName_III_37 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_37")]
        public string PartyTime_OrderTime_III_37 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_37")]
        public string PartyTime_ActTime_III_37 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_37")]
        public string PartyTime_DelayTime_III_37 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_37")]
        public string PartyTime_TimeName_IV_37 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_37")]
        public string PartyTime_OrderTime_IV_37 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_37")]
        public string PartyTime_ActTime_IV_37 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_37")]
        public string PartyTime_DelayTime_IV_37 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_37")]
        public string PartyTime_TimeName_V_37 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_37")]
        public string PartyTime_OrderTime_V_37 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_37")]
        public string PartyTime_ActTime_V_37 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_37")]
        public string PartyTime_DelayTime_V_37 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_37")]
        public string PartyFood_FoodName_I_37 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_37")]
        public string PartyFood_BeginTime_I_37 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_37")]
        public string PartyFood_EndTime_I_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_37")]
        public string PartyFood_RestRoomTime_I_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_37")]
        public string PartyFood_RestRoomFlg_I_37 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_37")]
        public string PartyFood_FoodName_II_37 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_37")]
        public string PartyFood_BeginTime_II_37 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_37")]
        public string PartyFood_EndTime_II_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_37")]
        public string PartyFood_RestRoomTime_II_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_37")]
        public string PartyFood_RestRoomFlg_II_37 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_37")]
        public string PartyFood_FoodName_III_37 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_37")]
        public string PartyFood_BeginTime_III_37 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_37")]
        public string PartyFood_EndTime_III_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_37")]
        public string PartyFood_RestRoomTime_III_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_37")]
        public string PartyFood_RestRoomFlg_III_37 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_37")]
        public string PartyFood_FoodName_IV_37 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_37")]
        public string PartyFood_BeginTime_IV_37 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_37")]
        public string PartyFood_EndTime_IV_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_37")]
        public string PartyFood_RestRoomTime_IV_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_37")]
        public string PartyFood_RestRoomFlg_IV_37 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_37")]
        public string PartyFood_FoodName_V_37 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_37")]
        public string PartyFood_BeginTime_V_37 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_37")]
        public string PartyFood_EndTime_V_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_37")]
        public string PartyFood_RestRoomTime_V_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_37")]
        public string PartyFood_RestRoomFlg_V_37 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_37")]
        public string PartyFood_FoodName_VI_37 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_37")]
        public string PartyFood_BeginTime_VI_37 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_37")]
        public string PartyFood_EndTime_VI_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_37")]
        public string PartyFood_RestRoomTime_VI_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_37")]
        public string PartyFood_RestRoomFlg_VI_37 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_37")]
        public string PartyFood_FoodName_VII_37 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_37")]
        public string PartyFood_BeginTime_VII_37 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_37")]
        public string PartyFood_EndTime_VII_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_37")]
        public string PartyFood_RestRoomTime_VII_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_37")]
        public string PartyFood_RestRoomFlg_VII_37 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_37")]
        public string PartyFood_FoodName_VIII_37 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_37")]
        public string PartyFood_BeginTime_VIII_37 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_37")]
        public string PartyFood_EndTime_VIII_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_37")]
        public string PartyFood_RestRoomTime_VIII_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_37")]
        public string PartyFood_RestRoomFlg_VIII_37 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_37")]
        public string PartyFood_FoodName_IX_37 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_37")]
        public string PartyFood_BeginTime_IX_37 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_37")]
        public string PartyFood_EndTime_IX_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_37")]
        public string PartyFood_RestRoomTime_IX_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_37")]
        public string PartyFood_RestRoomFlg_IX_37 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_37")]
        public string PartyFood_FoodName_X_37 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_37")]
        public string PartyFood_BeginTime_X_37 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_37")]
        public string PartyFood_EndTime_X_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_37")]
        public string PartyFood_RestRoomTime_X_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_37")]
        public string PartyFood_RestRoomFlg_X_37 { get; set; }
        [Display(Name = "PartyMember_Name_I_37")]
        public string PartyMember_Name_I_37 { get; set; }
        [Display(Name = "PartyMember_Name_II_37")]
        public string PartyMember_Name_II_37 { get; set; }
        [Display(Name = "PartyMember_Name_III_37")]
        public string PartyMember_Name_III_37 { get; set; }
        [Display(Name = "PartyMember_Name_IV_37")]
        public string PartyMember_Name_IV_37 { get; set; }
        [Display(Name = "PartyMember_Name_V_37")]
        public string PartyMember_Name_V_37 { get; set; }
        [Display(Name = "PartyMember_Name_VI_37")]
        public string PartyMember_Name_VI_37 { get; set; }
        [Display(Name = "PartyMember_Name_VII_37")]
        public string PartyMember_Name_VII_37 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_37")]
        public string PartyMember_Name_VIII_37 { get; set; }
        [Display(Name = "PartyMember_Name_IX_37")]
        public string PartyMember_Name_IX_37 { get; set; }
        [Display(Name = "PartyDate_38")]
        public string PartyDate_38 { get; set; }
        [Display(Name = "BrideFamilyName_38")]
        public string BrideFamilyName_38 { get; set; }
        [Display(Name = "GroomFamilyName_38")]
        public string GroomFamilyName_38 { get; set; }
        [Display(Name = "TantoName_38")]
        public string TantoName_38 { get; set; }
        [Display(Name = "ReporterName_38")]
        public string ReporterName_38 { get; set; }
        [Display(Name = "AdultCnt_38")]
        public string AdultCnt_38 { get; set; }
        [Display(Name = "HalfCnt_38")]
        public string HalfCnt_38 { get; set; }
        [Display(Name = "ChildrenCnt_38")]
        public string ChildrenCnt_38 { get; set; }
        [Display(Name = "SeatOnlyCnt_38")]
        public string SeatOnlyCnt_38 { get; set; }
        [Display(Name = "TableCnt_38")]
        public string TableCnt_38 { get; set; }
        [Display(Name = "TableCross_38")]
        public string TableCross_38 { get; set; }
        [Display(Name = "PartyStyleName_38")]
        public string PartyStyleName_38 { get; set; }
        [Display(Name = "FoodStyleName_38")]
        public string FoodStyleName_38 { get; set; }
        [Display(Name = "FoodPricce_38")]
        public string FoodPricce_38 { get; set; }
        [Display(Name = "DrinkPrice_38")]
        public string DrinkPrice_38 { get; set; }
        [Display(Name = "Wdrink_38")]
        public string Wdrink_38 { get; set; }
        [Display(Name = "Desl_38")]
        public string Desl_38 { get; set; }
        [Display(Name = "RestRoomFlg_38")]
        public string RestRoomFlg_38 { get; set; }
        [Display(Name = "AnketFlg_38")]
        public string AnketFlg_38 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_38")]
        public string PartyTime_TimeName_I_38 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_38")]
        public string PartyTime_OrderTime_I_38 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_38")]
        public string PartyTime_ActTime_I_38 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_38")]
        public string PartyTime_DelayTime_I_38 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_38")]
        public string PartyTime_TimeName_II_38 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_38")]
        public string PartyTime_OrderTime_II_38 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_38")]
        public string PartyTime_ActTime_II_38 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_38")]
        public string PartyTime_DelayTime_II_38 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_38")]
        public string PartyTime_TimeName_III_38 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_38")]
        public string PartyTime_OrderTime_III_38 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_38")]
        public string PartyTime_ActTime_III_38 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_38")]
        public string PartyTime_DelayTime_III_38 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_38")]
        public string PartyTime_TimeName_IV_38 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_38")]
        public string PartyTime_OrderTime_IV_38 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_38")]
        public string PartyTime_ActTime_IV_38 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_38")]
        public string PartyTime_DelayTime_IV_38 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_38")]
        public string PartyTime_TimeName_V_38 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_38")]
        public string PartyTime_OrderTime_V_38 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_38")]
        public string PartyTime_ActTime_V_38 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_38")]
        public string PartyTime_DelayTime_V_38 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_38")]
        public string PartyFood_FoodName_I_38 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_38")]
        public string PartyFood_BeginTime_I_38 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_38")]
        public string PartyFood_EndTime_I_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_38")]
        public string PartyFood_RestRoomTime_I_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_38")]
        public string PartyFood_RestRoomFlg_I_38 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_38")]
        public string PartyFood_FoodName_II_38 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_38")]
        public string PartyFood_BeginTime_II_38 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_38")]
        public string PartyFood_EndTime_II_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_38")]
        public string PartyFood_RestRoomTime_II_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_38")]
        public string PartyFood_RestRoomFlg_II_38 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_38")]
        public string PartyFood_FoodName_III_38 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_38")]
        public string PartyFood_BeginTime_III_38 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_38")]
        public string PartyFood_EndTime_III_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_38")]
        public string PartyFood_RestRoomTime_III_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_38")]
        public string PartyFood_RestRoomFlg_III_38 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_38")]
        public string PartyFood_FoodName_IV_38 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_38")]
        public string PartyFood_BeginTime_IV_38 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_38")]
        public string PartyFood_EndTime_IV_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_38")]
        public string PartyFood_RestRoomTime_IV_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_38")]
        public string PartyFood_RestRoomFlg_IV_38 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_38")]
        public string PartyFood_FoodName_V_38 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_38")]
        public string PartyFood_BeginTime_V_38 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_38")]
        public string PartyFood_EndTime_V_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_38")]
        public string PartyFood_RestRoomTime_V_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_38")]
        public string PartyFood_RestRoomFlg_V_38 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_38")]
        public string PartyFood_FoodName_VI_38 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_38")]
        public string PartyFood_BeginTime_VI_38 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_38")]
        public string PartyFood_EndTime_VI_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_38")]
        public string PartyFood_RestRoomTime_VI_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_38")]
        public string PartyFood_RestRoomFlg_VI_38 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_38")]
        public string PartyFood_FoodName_VII_38 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_38")]
        public string PartyFood_BeginTime_VII_38 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_38")]
        public string PartyFood_EndTime_VII_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_38")]
        public string PartyFood_RestRoomTime_VII_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_38")]
        public string PartyFood_RestRoomFlg_VII_38 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_38")]
        public string PartyFood_FoodName_VIII_38 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_38")]
        public string PartyFood_BeginTime_VIII_38 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_38")]
        public string PartyFood_EndTime_VIII_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_38")]
        public string PartyFood_RestRoomTime_VIII_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_38")]
        public string PartyFood_RestRoomFlg_VIII_38 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_38")]
        public string PartyFood_FoodName_IX_38 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_38")]
        public string PartyFood_BeginTime_IX_38 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_38")]
        public string PartyFood_EndTime_IX_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_38")]
        public string PartyFood_RestRoomTime_IX_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_38")]
        public string PartyFood_RestRoomFlg_IX_38 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_38")]
        public string PartyFood_FoodName_X_38 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_38")]
        public string PartyFood_BeginTime_X_38 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_38")]
        public string PartyFood_EndTime_X_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_38")]
        public string PartyFood_RestRoomTime_X_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_38")]
        public string PartyFood_RestRoomFlg_X_38 { get; set; }
        [Display(Name = "PartyMember_Name_I_38")]
        public string PartyMember_Name_I_38 { get; set; }
        [Display(Name = "PartyMember_Name_II_38")]
        public string PartyMember_Name_II_38 { get; set; }
        [Display(Name = "PartyMember_Name_III_38")]
        public string PartyMember_Name_III_38 { get; set; }
        [Display(Name = "PartyMember_Name_IV_38")]
        public string PartyMember_Name_IV_38 { get; set; }
        [Display(Name = "PartyMember_Name_V_38")]
        public string PartyMember_Name_V_38 { get; set; }
        [Display(Name = "PartyMember_Name_VI_38")]
        public string PartyMember_Name_VI_38 { get; set; }
        [Display(Name = "PartyMember_Name_VII_38")]
        public string PartyMember_Name_VII_38 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_38")]
        public string PartyMember_Name_VIII_38 { get; set; }
        [Display(Name = "PartyMember_Name_IX_38")]
        public string PartyMember_Name_IX_38 { get; set; }
        [Display(Name = "PartyDate_39")]
        public string PartyDate_39 { get; set; }
        [Display(Name = "BrideFamilyName_39")]
        public string BrideFamilyName_39 { get; set; }
        [Display(Name = "GroomFamilyName_39")]
        public string GroomFamilyName_39 { get; set; }
        [Display(Name = "TantoName_39")]
        public string TantoName_39 { get; set; }
        [Display(Name = "ReporterName_39")]
        public string ReporterName_39 { get; set; }
        [Display(Name = "AdultCnt_39")]
        public string AdultCnt_39 { get; set; }
        [Display(Name = "HalfCnt_39")]
        public string HalfCnt_39 { get; set; }
        [Display(Name = "ChildrenCnt_39")]
        public string ChildrenCnt_39 { get; set; }
        [Display(Name = "SeatOnlyCnt_39")]
        public string SeatOnlyCnt_39 { get; set; }
        [Display(Name = "TableCnt_39")]
        public string TableCnt_39 { get; set; }
        [Display(Name = "TableCross_39")]
        public string TableCross_39 { get; set; }
        [Display(Name = "PartyStyleName_39")]
        public string PartyStyleName_39 { get; set; }
        [Display(Name = "FoodStyleName_39")]
        public string FoodStyleName_39 { get; set; }
        [Display(Name = "FoodPricce_39")]
        public string FoodPricce_39 { get; set; }
        [Display(Name = "DrinkPrice_39")]
        public string DrinkPrice_39 { get; set; }
        [Display(Name = "Wdrink_39")]
        public string Wdrink_39 { get; set; }
        [Display(Name = "Desl_39")]
        public string Desl_39 { get; set; }
        [Display(Name = "RestRoomFlg_39")]
        public string RestRoomFlg_39 { get; set; }
        [Display(Name = "AnketFlg_39")]
        public string AnketFlg_39 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_39")]
        public string PartyTime_TimeName_I_39 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_39")]
        public string PartyTime_OrderTime_I_39 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_39")]
        public string PartyTime_ActTime_I_39 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_39")]
        public string PartyTime_DelayTime_I_39 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_39")]
        public string PartyTime_TimeName_II_39 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_39")]
        public string PartyTime_OrderTime_II_39 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_39")]
        public string PartyTime_ActTime_II_39 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_39")]
        public string PartyTime_DelayTime_II_39 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_39")]
        public string PartyTime_TimeName_III_39 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_39")]
        public string PartyTime_OrderTime_III_39 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_39")]
        public string PartyTime_ActTime_III_39 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_39")]
        public string PartyTime_DelayTime_III_39 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_39")]
        public string PartyTime_TimeName_IV_39 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_39")]
        public string PartyTime_OrderTime_IV_39 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_39")]
        public string PartyTime_ActTime_IV_39 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_39")]
        public string PartyTime_DelayTime_IV_39 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_39")]
        public string PartyTime_TimeName_V_39 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_39")]
        public string PartyTime_OrderTime_V_39 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_39")]
        public string PartyTime_ActTime_V_39 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_39")]
        public string PartyTime_DelayTime_V_39 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_39")]
        public string PartyFood_FoodName_I_39 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_39")]
        public string PartyFood_BeginTime_I_39 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_39")]
        public string PartyFood_EndTime_I_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_39")]
        public string PartyFood_RestRoomTime_I_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_39")]
        public string PartyFood_RestRoomFlg_I_39 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_39")]
        public string PartyFood_FoodName_II_39 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_39")]
        public string PartyFood_BeginTime_II_39 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_39")]
        public string PartyFood_EndTime_II_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_39")]
        public string PartyFood_RestRoomTime_II_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_39")]
        public string PartyFood_RestRoomFlg_II_39 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_39")]
        public string PartyFood_FoodName_III_39 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_39")]
        public string PartyFood_BeginTime_III_39 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_39")]
        public string PartyFood_EndTime_III_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_39")]
        public string PartyFood_RestRoomTime_III_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_39")]
        public string PartyFood_RestRoomFlg_III_39 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_39")]
        public string PartyFood_FoodName_IV_39 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_39")]
        public string PartyFood_BeginTime_IV_39 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_39")]
        public string PartyFood_EndTime_IV_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_39")]
        public string PartyFood_RestRoomTime_IV_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_39")]
        public string PartyFood_RestRoomFlg_IV_39 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_39")]
        public string PartyFood_FoodName_V_39 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_39")]
        public string PartyFood_BeginTime_V_39 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_39")]
        public string PartyFood_EndTime_V_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_39")]
        public string PartyFood_RestRoomTime_V_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_39")]
        public string PartyFood_RestRoomFlg_V_39 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_39")]
        public string PartyFood_FoodName_VI_39 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_39")]
        public string PartyFood_BeginTime_VI_39 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_39")]
        public string PartyFood_EndTime_VI_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_39")]
        public string PartyFood_RestRoomTime_VI_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_39")]
        public string PartyFood_RestRoomFlg_VI_39 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_39")]
        public string PartyFood_FoodName_VII_39 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_39")]
        public string PartyFood_BeginTime_VII_39 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_39")]
        public string PartyFood_EndTime_VII_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_39")]
        public string PartyFood_RestRoomTime_VII_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_39")]
        public string PartyFood_RestRoomFlg_VII_39 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_39")]
        public string PartyFood_FoodName_VIII_39 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_39")]
        public string PartyFood_BeginTime_VIII_39 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_39")]
        public string PartyFood_EndTime_VIII_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_39")]
        public string PartyFood_RestRoomTime_VIII_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_39")]
        public string PartyFood_RestRoomFlg_VIII_39 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_39")]
        public string PartyFood_FoodName_IX_39 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_39")]
        public string PartyFood_BeginTime_IX_39 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_39")]
        public string PartyFood_EndTime_IX_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_39")]
        public string PartyFood_RestRoomTime_IX_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_39")]
        public string PartyFood_RestRoomFlg_IX_39 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_39")]
        public string PartyFood_FoodName_X_39 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_39")]
        public string PartyFood_BeginTime_X_39 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_39")]
        public string PartyFood_EndTime_X_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_39")]
        public string PartyFood_RestRoomTime_X_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_39")]
        public string PartyFood_RestRoomFlg_X_39 { get; set; }
        [Display(Name = "PartyMember_Name_I_39")]
        public string PartyMember_Name_I_39 { get; set; }
        [Display(Name = "PartyMember_Name_II_39")]
        public string PartyMember_Name_II_39 { get; set; }
        [Display(Name = "PartyMember_Name_III_39")]
        public string PartyMember_Name_III_39 { get; set; }
        [Display(Name = "PartyMember_Name_IV_39")]
        public string PartyMember_Name_IV_39 { get; set; }
        [Display(Name = "PartyMember_Name_V_39")]
        public string PartyMember_Name_V_39 { get; set; }
        [Display(Name = "PartyMember_Name_VI_39")]
        public string PartyMember_Name_VI_39 { get; set; }
        [Display(Name = "PartyMember_Name_VII_39")]
        public string PartyMember_Name_VII_39 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_39")]
        public string PartyMember_Name_VIII_39 { get; set; }
        [Display(Name = "PartyMember_Name_IX_39")]
        public string PartyMember_Name_IX_39 { get; set; }
        [Display(Name = "PartyDate_40")]
        public string PartyDate_40 { get; set; }
        [Display(Name = "BrideFamilyName_40")]
        public string BrideFamilyName_40 { get; set; }
        [Display(Name = "GroomFamilyName_40")]
        public string GroomFamilyName_40 { get; set; }
        [Display(Name = "TantoName_40")]
        public string TantoName_40 { get; set; }
        [Display(Name = "ReporterName_40")]
        public string ReporterName_40 { get; set; }
        [Display(Name = "AdultCnt_40")]
        public string AdultCnt_40 { get; set; }
        [Display(Name = "HalfCnt_40")]
        public string HalfCnt_40 { get; set; }
        [Display(Name = "ChildrenCnt_40")]
        public string ChildrenCnt_40 { get; set; }
        [Display(Name = "SeatOnlyCnt_40")]
        public string SeatOnlyCnt_40 { get; set; }
        [Display(Name = "TableCnt_40")]
        public string TableCnt_40 { get; set; }
        [Display(Name = "TableCross_40")]
        public string TableCross_40 { get; set; }
        [Display(Name = "PartyStyleName_40")]
        public string PartyStyleName_40 { get; set; }
        [Display(Name = "FoodStyleName_40")]
        public string FoodStyleName_40 { get; set; }
        [Display(Name = "FoodPricce_40")]
        public string FoodPricce_40 { get; set; }
        [Display(Name = "DrinkPrice_40")]
        public string DrinkPrice_40 { get; set; }
        [Display(Name = "Wdrink_40")]
        public string Wdrink_40 { get; set; }
        [Display(Name = "Desl_40")]
        public string Desl_40 { get; set; }
        [Display(Name = "RestRoomFlg_40")]
        public string RestRoomFlg_40 { get; set; }
        [Display(Name = "AnketFlg_40")]
        public string AnketFlg_40 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_40")]
        public string PartyTime_TimeName_I_40 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_40")]
        public string PartyTime_OrderTime_I_40 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_40")]
        public string PartyTime_ActTime_I_40 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_40")]
        public string PartyTime_DelayTime_I_40 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_40")]
        public string PartyTime_TimeName_II_40 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_40")]
        public string PartyTime_OrderTime_II_40 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_40")]
        public string PartyTime_ActTime_II_40 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_40")]
        public string PartyTime_DelayTime_II_40 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_40")]
        public string PartyTime_TimeName_III_40 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_40")]
        public string PartyTime_OrderTime_III_40 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_40")]
        public string PartyTime_ActTime_III_40 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_40")]
        public string PartyTime_DelayTime_III_40 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_40")]
        public string PartyTime_TimeName_IV_40 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_40")]
        public string PartyTime_OrderTime_IV_40 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_40")]
        public string PartyTime_ActTime_IV_40 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_40")]
        public string PartyTime_DelayTime_IV_40 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_40")]
        public string PartyTime_TimeName_V_40 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_40")]
        public string PartyTime_OrderTime_V_40 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_40")]
        public string PartyTime_ActTime_V_40 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_40")]
        public string PartyTime_DelayTime_V_40 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_40")]
        public string PartyFood_FoodName_I_40 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_40")]
        public string PartyFood_BeginTime_I_40 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_40")]
        public string PartyFood_EndTime_I_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_40")]
        public string PartyFood_RestRoomTime_I_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_40")]
        public string PartyFood_RestRoomFlg_I_40 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_40")]
        public string PartyFood_FoodName_II_40 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_40")]
        public string PartyFood_BeginTime_II_40 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_40")]
        public string PartyFood_EndTime_II_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_40")]
        public string PartyFood_RestRoomTime_II_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_40")]
        public string PartyFood_RestRoomFlg_II_40 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_40")]
        public string PartyFood_FoodName_III_40 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_40")]
        public string PartyFood_BeginTime_III_40 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_40")]
        public string PartyFood_EndTime_III_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_40")]
        public string PartyFood_RestRoomTime_III_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_40")]
        public string PartyFood_RestRoomFlg_III_40 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_40")]
        public string PartyFood_FoodName_IV_40 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_40")]
        public string PartyFood_BeginTime_IV_40 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_40")]
        public string PartyFood_EndTime_IV_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_40")]
        public string PartyFood_RestRoomTime_IV_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_40")]
        public string PartyFood_RestRoomFlg_IV_40 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_40")]
        public string PartyFood_FoodName_V_40 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_40")]
        public string PartyFood_BeginTime_V_40 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_40")]
        public string PartyFood_EndTime_V_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_40")]
        public string PartyFood_RestRoomTime_V_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_40")]
        public string PartyFood_RestRoomFlg_V_40 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_40")]
        public string PartyFood_FoodName_VI_40 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_40")]
        public string PartyFood_BeginTime_VI_40 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_40")]
        public string PartyFood_EndTime_VI_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_40")]
        public string PartyFood_RestRoomTime_VI_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_40")]
        public string PartyFood_RestRoomFlg_VI_40 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_40")]
        public string PartyFood_FoodName_VII_40 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_40")]
        public string PartyFood_BeginTime_VII_40 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_40")]
        public string PartyFood_EndTime_VII_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_40")]
        public string PartyFood_RestRoomTime_VII_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_40")]
        public string PartyFood_RestRoomFlg_VII_40 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_40")]
        public string PartyFood_FoodName_VIII_40 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_40")]
        public string PartyFood_BeginTime_VIII_40 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_40")]
        public string PartyFood_EndTime_VIII_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_40")]
        public string PartyFood_RestRoomTime_VIII_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_40")]
        public string PartyFood_RestRoomFlg_VIII_40 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_40")]
        public string PartyFood_FoodName_IX_40 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_40")]
        public string PartyFood_BeginTime_IX_40 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_40")]
        public string PartyFood_EndTime_IX_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_40")]
        public string PartyFood_RestRoomTime_IX_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_40")]
        public string PartyFood_RestRoomFlg_IX_40 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_40")]
        public string PartyFood_FoodName_X_40 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_40")]
        public string PartyFood_BeginTime_X_40 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_40")]
        public string PartyFood_EndTime_X_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_40")]
        public string PartyFood_RestRoomTime_X_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_40")]
        public string PartyFood_RestRoomFlg_X_40 { get; set; }
        [Display(Name = "PartyMember_Name_I_40")]
        public string PartyMember_Name_I_40 { get; set; }
        [Display(Name = "PartyMember_Name_II_40")]
        public string PartyMember_Name_II_40 { get; set; }
        [Display(Name = "PartyMember_Name_III_40")]
        public string PartyMember_Name_III_40 { get; set; }
        [Display(Name = "PartyMember_Name_IV_40")]
        public string PartyMember_Name_IV_40 { get; set; }
        [Display(Name = "PartyMember_Name_V_40")]
        public string PartyMember_Name_V_40 { get; set; }
        [Display(Name = "PartyMember_Name_VI_40")]
        public string PartyMember_Name_VI_40 { get; set; }
        [Display(Name = "PartyMember_Name_VII_40")]
        public string PartyMember_Name_VII_40 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_40")]
        public string PartyMember_Name_VIII_40 { get; set; }
        [Display(Name = "PartyMember_Name_IX_40")]
        public string PartyMember_Name_IX_40 { get; set; }
        [Display(Name = "PartyDate_41")]
        public string PartyDate_41 { get; set; }
        [Display(Name = "BrideFamilyName_41")]
        public string BrideFamilyName_41 { get; set; }
        [Display(Name = "GroomFamilyName_41")]
        public string GroomFamilyName_41 { get; set; }
        [Display(Name = "TantoName_41")]
        public string TantoName_41 { get; set; }
        [Display(Name = "ReporterName_41")]
        public string ReporterName_41 { get; set; }
        [Display(Name = "AdultCnt_41")]
        public string AdultCnt_41 { get; set; }
        [Display(Name = "HalfCnt_41")]
        public string HalfCnt_41 { get; set; }
        [Display(Name = "ChildrenCnt_41")]
        public string ChildrenCnt_41 { get; set; }
        [Display(Name = "SeatOnlyCnt_41")]
        public string SeatOnlyCnt_41 { get; set; }
        [Display(Name = "TableCnt_41")]
        public string TableCnt_41 { get; set; }
        [Display(Name = "TableCross_41")]
        public string TableCross_41 { get; set; }
        [Display(Name = "PartyStyleName_41")]
        public string PartyStyleName_41 { get; set; }
        [Display(Name = "FoodStyleName_41")]
        public string FoodStyleName_41 { get; set; }
        [Display(Name = "FoodPricce_41")]
        public string FoodPricce_41 { get; set; }
        [Display(Name = "DrinkPrice_41")]
        public string DrinkPrice_41 { get; set; }
        [Display(Name = "Wdrink_41")]
        public string Wdrink_41 { get; set; }
        [Display(Name = "Desl_41")]
        public string Desl_41 { get; set; }
        [Display(Name = "RestRoomFlg_41")]
        public string RestRoomFlg_41 { get; set; }
        [Display(Name = "AnketFlg_41")]
        public string AnketFlg_41 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_41")]
        public string PartyTime_TimeName_I_41 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_41")]
        public string PartyTime_OrderTime_I_41 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_41")]
        public string PartyTime_ActTime_I_41 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_41")]
        public string PartyTime_DelayTime_I_41 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_41")]
        public string PartyTime_TimeName_II_41 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_41")]
        public string PartyTime_OrderTime_II_41 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_41")]
        public string PartyTime_ActTime_II_41 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_41")]
        public string PartyTime_DelayTime_II_41 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_41")]
        public string PartyTime_TimeName_III_41 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_41")]
        public string PartyTime_OrderTime_III_41 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_41")]
        public string PartyTime_ActTime_III_41 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_41")]
        public string PartyTime_DelayTime_III_41 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_41")]
        public string PartyTime_TimeName_IV_41 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_41")]
        public string PartyTime_OrderTime_IV_41 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_41")]
        public string PartyTime_ActTime_IV_41 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_41")]
        public string PartyTime_DelayTime_IV_41 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_41")]
        public string PartyTime_TimeName_V_41 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_41")]
        public string PartyTime_OrderTime_V_41 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_41")]
        public string PartyTime_ActTime_V_41 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_41")]
        public string PartyTime_DelayTime_V_41 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_41")]
        public string PartyFood_FoodName_I_41 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_41")]
        public string PartyFood_BeginTime_I_41 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_41")]
        public string PartyFood_EndTime_I_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_41")]
        public string PartyFood_RestRoomTime_I_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_41")]
        public string PartyFood_RestRoomFlg_I_41 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_41")]
        public string PartyFood_FoodName_II_41 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_41")]
        public string PartyFood_BeginTime_II_41 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_41")]
        public string PartyFood_EndTime_II_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_41")]
        public string PartyFood_RestRoomTime_II_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_41")]
        public string PartyFood_RestRoomFlg_II_41 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_41")]
        public string PartyFood_FoodName_III_41 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_41")]
        public string PartyFood_BeginTime_III_41 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_41")]
        public string PartyFood_EndTime_III_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_41")]
        public string PartyFood_RestRoomTime_III_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_41")]
        public string PartyFood_RestRoomFlg_III_41 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_41")]
        public string PartyFood_FoodName_IV_41 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_41")]
        public string PartyFood_BeginTime_IV_41 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_41")]
        public string PartyFood_EndTime_IV_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_41")]
        public string PartyFood_RestRoomTime_IV_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_41")]
        public string PartyFood_RestRoomFlg_IV_41 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_41")]
        public string PartyFood_FoodName_V_41 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_41")]
        public string PartyFood_BeginTime_V_41 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_41")]
        public string PartyFood_EndTime_V_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_41")]
        public string PartyFood_RestRoomTime_V_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_41")]
        public string PartyFood_RestRoomFlg_V_41 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_41")]
        public string PartyFood_FoodName_VI_41 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_41")]
        public string PartyFood_BeginTime_VI_41 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_41")]
        public string PartyFood_EndTime_VI_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_41")]
        public string PartyFood_RestRoomTime_VI_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_41")]
        public string PartyFood_RestRoomFlg_VI_41 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_41")]
        public string PartyFood_FoodName_VII_41 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_41")]
        public string PartyFood_BeginTime_VII_41 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_41")]
        public string PartyFood_EndTime_VII_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_41")]
        public string PartyFood_RestRoomTime_VII_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_41")]
        public string PartyFood_RestRoomFlg_VII_41 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_41")]
        public string PartyFood_FoodName_VIII_41 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_41")]
        public string PartyFood_BeginTime_VIII_41 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_41")]
        public string PartyFood_EndTime_VIII_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_41")]
        public string PartyFood_RestRoomTime_VIII_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_41")]
        public string PartyFood_RestRoomFlg_VIII_41 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_41")]
        public string PartyFood_FoodName_IX_41 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_41")]
        public string PartyFood_BeginTime_IX_41 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_41")]
        public string PartyFood_EndTime_IX_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_41")]
        public string PartyFood_RestRoomTime_IX_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_41")]
        public string PartyFood_RestRoomFlg_IX_41 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_41")]
        public string PartyFood_FoodName_X_41 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_41")]
        public string PartyFood_BeginTime_X_41 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_41")]
        public string PartyFood_EndTime_X_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_41")]
        public string PartyFood_RestRoomTime_X_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_41")]
        public string PartyFood_RestRoomFlg_X_41 { get; set; }
        [Display(Name = "PartyMember_Name_I_41")]
        public string PartyMember_Name_I_41 { get; set; }
        [Display(Name = "PartyMember_Name_II_41")]
        public string PartyMember_Name_II_41 { get; set; }
        [Display(Name = "PartyMember_Name_III_41")]
        public string PartyMember_Name_III_41 { get; set; }
        [Display(Name = "PartyMember_Name_IV_41")]
        public string PartyMember_Name_IV_41 { get; set; }
        [Display(Name = "PartyMember_Name_V_41")]
        public string PartyMember_Name_V_41 { get; set; }
        [Display(Name = "PartyMember_Name_VI_41")]
        public string PartyMember_Name_VI_41 { get; set; }
        [Display(Name = "PartyMember_Name_VII_41")]
        public string PartyMember_Name_VII_41 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_41")]
        public string PartyMember_Name_VIII_41 { get; set; }
        [Display(Name = "PartyMember_Name_IX_41")]
        public string PartyMember_Name_IX_41 { get; set; }
        [Display(Name = "PartyDate_42")]
        public string PartyDate_42 { get; set; }
        [Display(Name = "BrideFamilyName_42")]
        public string BrideFamilyName_42 { get; set; }
        [Display(Name = "GroomFamilyName_42")]
        public string GroomFamilyName_42 { get; set; }
        [Display(Name = "TantoName_42")]
        public string TantoName_42 { get; set; }
        [Display(Name = "ReporterName_42")]
        public string ReporterName_42 { get; set; }
        [Display(Name = "AdultCnt_42")]
        public string AdultCnt_42 { get; set; }
        [Display(Name = "HalfCnt_42")]
        public string HalfCnt_42 { get; set; }
        [Display(Name = "ChildrenCnt_42")]
        public string ChildrenCnt_42 { get; set; }
        [Display(Name = "SeatOnlyCnt_42")]
        public string SeatOnlyCnt_42 { get; set; }
        [Display(Name = "TableCnt_42")]
        public string TableCnt_42 { get; set; }
        [Display(Name = "TableCross_42")]
        public string TableCross_42 { get; set; }
        [Display(Name = "PartyStyleName_42")]
        public string PartyStyleName_42 { get; set; }
        [Display(Name = "FoodStyleName_42")]
        public string FoodStyleName_42 { get; set; }
        [Display(Name = "FoodPricce_42")]
        public string FoodPricce_42 { get; set; }
        [Display(Name = "DrinkPrice_42")]
        public string DrinkPrice_42 { get; set; }
        [Display(Name = "Wdrink_42")]
        public string Wdrink_42 { get; set; }
        [Display(Name = "Desl_42")]
        public string Desl_42 { get; set; }
        [Display(Name = "RestRoomFlg_42")]
        public string RestRoomFlg_42 { get; set; }
        [Display(Name = "AnketFlg_42")]
        public string AnketFlg_42 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_42")]
        public string PartyTime_TimeName_I_42 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_42")]
        public string PartyTime_OrderTime_I_42 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_42")]
        public string PartyTime_ActTime_I_42 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_42")]
        public string PartyTime_DelayTime_I_42 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_42")]
        public string PartyTime_TimeName_II_42 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_42")]
        public string PartyTime_OrderTime_II_42 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_42")]
        public string PartyTime_ActTime_II_42 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_42")]
        public string PartyTime_DelayTime_II_42 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_42")]
        public string PartyTime_TimeName_III_42 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_42")]
        public string PartyTime_OrderTime_III_42 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_42")]
        public string PartyTime_ActTime_III_42 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_42")]
        public string PartyTime_DelayTime_III_42 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_42")]
        public string PartyTime_TimeName_IV_42 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_42")]
        public string PartyTime_OrderTime_IV_42 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_42")]
        public string PartyTime_ActTime_IV_42 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_42")]
        public string PartyTime_DelayTime_IV_42 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_42")]
        public string PartyTime_TimeName_V_42 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_42")]
        public string PartyTime_OrderTime_V_42 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_42")]
        public string PartyTime_ActTime_V_42 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_42")]
        public string PartyTime_DelayTime_V_42 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_42")]
        public string PartyFood_FoodName_I_42 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_42")]
        public string PartyFood_BeginTime_I_42 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_42")]
        public string PartyFood_EndTime_I_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_42")]
        public string PartyFood_RestRoomTime_I_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_42")]
        public string PartyFood_RestRoomFlg_I_42 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_42")]
        public string PartyFood_FoodName_II_42 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_42")]
        public string PartyFood_BeginTime_II_42 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_42")]
        public string PartyFood_EndTime_II_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_42")]
        public string PartyFood_RestRoomTime_II_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_42")]
        public string PartyFood_RestRoomFlg_II_42 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_42")]
        public string PartyFood_FoodName_III_42 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_42")]
        public string PartyFood_BeginTime_III_42 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_42")]
        public string PartyFood_EndTime_III_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_42")]
        public string PartyFood_RestRoomTime_III_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_42")]
        public string PartyFood_RestRoomFlg_III_42 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_42")]
        public string PartyFood_FoodName_IV_42 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_42")]
        public string PartyFood_BeginTime_IV_42 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_42")]
        public string PartyFood_EndTime_IV_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_42")]
        public string PartyFood_RestRoomTime_IV_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_42")]
        public string PartyFood_RestRoomFlg_IV_42 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_42")]
        public string PartyFood_FoodName_V_42 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_42")]
        public string PartyFood_BeginTime_V_42 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_42")]
        public string PartyFood_EndTime_V_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_42")]
        public string PartyFood_RestRoomTime_V_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_42")]
        public string PartyFood_RestRoomFlg_V_42 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_42")]
        public string PartyFood_FoodName_VI_42 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_42")]
        public string PartyFood_BeginTime_VI_42 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_42")]
        public string PartyFood_EndTime_VI_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_42")]
        public string PartyFood_RestRoomTime_VI_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_42")]
        public string PartyFood_RestRoomFlg_VI_42 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_42")]
        public string PartyFood_FoodName_VII_42 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_42")]
        public string PartyFood_BeginTime_VII_42 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_42")]
        public string PartyFood_EndTime_VII_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_42")]
        public string PartyFood_RestRoomTime_VII_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_42")]
        public string PartyFood_RestRoomFlg_VII_42 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_42")]
        public string PartyFood_FoodName_VIII_42 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_42")]
        public string PartyFood_BeginTime_VIII_42 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_42")]
        public string PartyFood_EndTime_VIII_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_42")]
        public string PartyFood_RestRoomTime_VIII_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_42")]
        public string PartyFood_RestRoomFlg_VIII_42 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_42")]
        public string PartyFood_FoodName_IX_42 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_42")]
        public string PartyFood_BeginTime_IX_42 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_42")]
        public string PartyFood_EndTime_IX_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_42")]
        public string PartyFood_RestRoomTime_IX_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_42")]
        public string PartyFood_RestRoomFlg_IX_42 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_42")]
        public string PartyFood_FoodName_X_42 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_42")]
        public string PartyFood_BeginTime_X_42 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_42")]
        public string PartyFood_EndTime_X_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_42")]
        public string PartyFood_RestRoomTime_X_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_42")]
        public string PartyFood_RestRoomFlg_X_42 { get; set; }
        [Display(Name = "PartyMember_Name_I_42")]
        public string PartyMember_Name_I_42 { get; set; }
        [Display(Name = "PartyMember_Name_II_42")]
        public string PartyMember_Name_II_42 { get; set; }
        [Display(Name = "PartyMember_Name_III_42")]
        public string PartyMember_Name_III_42 { get; set; }
        [Display(Name = "PartyMember_Name_IV_42")]
        public string PartyMember_Name_IV_42 { get; set; }
        [Display(Name = "PartyMember_Name_V_42")]
        public string PartyMember_Name_V_42 { get; set; }
        [Display(Name = "PartyMember_Name_VI_42")]
        public string PartyMember_Name_VI_42 { get; set; }
        [Display(Name = "PartyMember_Name_VII_42")]
        public string PartyMember_Name_VII_42 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_42")]
        public string PartyMember_Name_VIII_42 { get; set; }
        [Display(Name = "PartyMember_Name_IX_42")]
        public string PartyMember_Name_IX_42 { get; set; }
        [Display(Name = "PartyDate_43")]
        public string PartyDate_43 { get; set; }
        [Display(Name = "BrideFamilyName_43")]
        public string BrideFamilyName_43 { get; set; }
        [Display(Name = "GroomFamilyName_43")]
        public string GroomFamilyName_43 { get; set; }
        [Display(Name = "TantoName_43")]
        public string TantoName_43 { get; set; }
        [Display(Name = "ReporterName_43")]
        public string ReporterName_43 { get; set; }
        [Display(Name = "AdultCnt_43")]
        public string AdultCnt_43 { get; set; }
        [Display(Name = "HalfCnt_43")]
        public string HalfCnt_43 { get; set; }
        [Display(Name = "ChildrenCnt_43")]
        public string ChildrenCnt_43 { get; set; }
        [Display(Name = "SeatOnlyCnt_43")]
        public string SeatOnlyCnt_43 { get; set; }
        [Display(Name = "TableCnt_43")]
        public string TableCnt_43 { get; set; }
        [Display(Name = "TableCross_43")]
        public string TableCross_43 { get; set; }
        [Display(Name = "PartyStyleName_43")]
        public string PartyStyleName_43 { get; set; }
        [Display(Name = "FoodStyleName_43")]
        public string FoodStyleName_43 { get; set; }
        [Display(Name = "FoodPricce_43")]
        public string FoodPricce_43 { get; set; }
        [Display(Name = "DrinkPrice_43")]
        public string DrinkPrice_43 { get; set; }
        [Display(Name = "Wdrink_43")]
        public string Wdrink_43 { get; set; }
        [Display(Name = "Desl_43")]
        public string Desl_43 { get; set; }
        [Display(Name = "RestRoomFlg_43")]
        public string RestRoomFlg_43 { get; set; }
        [Display(Name = "AnketFlg_43")]
        public string AnketFlg_43 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_43")]
        public string PartyTime_TimeName_I_43 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_43")]
        public string PartyTime_OrderTime_I_43 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_43")]
        public string PartyTime_ActTime_I_43 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_43")]
        public string PartyTime_DelayTime_I_43 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_43")]
        public string PartyTime_TimeName_II_43 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_43")]
        public string PartyTime_OrderTime_II_43 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_43")]
        public string PartyTime_ActTime_II_43 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_43")]
        public string PartyTime_DelayTime_II_43 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_43")]
        public string PartyTime_TimeName_III_43 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_43")]
        public string PartyTime_OrderTime_III_43 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_43")]
        public string PartyTime_ActTime_III_43 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_43")]
        public string PartyTime_DelayTime_III_43 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_43")]
        public string PartyTime_TimeName_IV_43 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_43")]
        public string PartyTime_OrderTime_IV_43 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_43")]
        public string PartyTime_ActTime_IV_43 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_43")]
        public string PartyTime_DelayTime_IV_43 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_43")]
        public string PartyTime_TimeName_V_43 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_43")]
        public string PartyTime_OrderTime_V_43 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_43")]
        public string PartyTime_ActTime_V_43 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_43")]
        public string PartyTime_DelayTime_V_43 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_43")]
        public string PartyFood_FoodName_I_43 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_43")]
        public string PartyFood_BeginTime_I_43 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_43")]
        public string PartyFood_EndTime_I_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_43")]
        public string PartyFood_RestRoomTime_I_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_43")]
        public string PartyFood_RestRoomFlg_I_43 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_43")]
        public string PartyFood_FoodName_II_43 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_43")]
        public string PartyFood_BeginTime_II_43 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_43")]
        public string PartyFood_EndTime_II_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_43")]
        public string PartyFood_RestRoomTime_II_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_43")]
        public string PartyFood_RestRoomFlg_II_43 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_43")]
        public string PartyFood_FoodName_III_43 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_43")]
        public string PartyFood_BeginTime_III_43 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_43")]
        public string PartyFood_EndTime_III_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_43")]
        public string PartyFood_RestRoomTime_III_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_43")]
        public string PartyFood_RestRoomFlg_III_43 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_43")]
        public string PartyFood_FoodName_IV_43 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_43")]
        public string PartyFood_BeginTime_IV_43 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_43")]
        public string PartyFood_EndTime_IV_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_43")]
        public string PartyFood_RestRoomTime_IV_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_43")]
        public string PartyFood_RestRoomFlg_IV_43 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_43")]
        public string PartyFood_FoodName_V_43 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_43")]
        public string PartyFood_BeginTime_V_43 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_43")]
        public string PartyFood_EndTime_V_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_43")]
        public string PartyFood_RestRoomTime_V_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_43")]
        public string PartyFood_RestRoomFlg_V_43 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_43")]
        public string PartyFood_FoodName_VI_43 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_43")]
        public string PartyFood_BeginTime_VI_43 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_43")]
        public string PartyFood_EndTime_VI_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_43")]
        public string PartyFood_RestRoomTime_VI_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_43")]
        public string PartyFood_RestRoomFlg_VI_43 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_43")]
        public string PartyFood_FoodName_VII_43 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_43")]
        public string PartyFood_BeginTime_VII_43 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_43")]
        public string PartyFood_EndTime_VII_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_43")]
        public string PartyFood_RestRoomTime_VII_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_43")]
        public string PartyFood_RestRoomFlg_VII_43 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_43")]
        public string PartyFood_FoodName_VIII_43 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_43")]
        public string PartyFood_BeginTime_VIII_43 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_43")]
        public string PartyFood_EndTime_VIII_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_43")]
        public string PartyFood_RestRoomTime_VIII_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_43")]
        public string PartyFood_RestRoomFlg_VIII_43 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_43")]
        public string PartyFood_FoodName_IX_43 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_43")]
        public string PartyFood_BeginTime_IX_43 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_43")]
        public string PartyFood_EndTime_IX_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_43")]
        public string PartyFood_RestRoomTime_IX_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_43")]
        public string PartyFood_RestRoomFlg_IX_43 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_43")]
        public string PartyFood_FoodName_X_43 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_43")]
        public string PartyFood_BeginTime_X_43 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_43")]
        public string PartyFood_EndTime_X_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_43")]
        public string PartyFood_RestRoomTime_X_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_43")]
        public string PartyFood_RestRoomFlg_X_43 { get; set; }
        [Display(Name = "PartyMember_Name_I_43")]
        public string PartyMember_Name_I_43 { get; set; }
        [Display(Name = "PartyMember_Name_II_43")]
        public string PartyMember_Name_II_43 { get; set; }
        [Display(Name = "PartyMember_Name_III_43")]
        public string PartyMember_Name_III_43 { get; set; }
        [Display(Name = "PartyMember_Name_IV_43")]
        public string PartyMember_Name_IV_43 { get; set; }
        [Display(Name = "PartyMember_Name_V_43")]
        public string PartyMember_Name_V_43 { get; set; }
        [Display(Name = "PartyMember_Name_VI_43")]
        public string PartyMember_Name_VI_43 { get; set; }
        [Display(Name = "PartyMember_Name_VII_43")]
        public string PartyMember_Name_VII_43 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_43")]
        public string PartyMember_Name_VIII_43 { get; set; }
        [Display(Name = "PartyMember_Name_IX_43")]
        public string PartyMember_Name_IX_43 { get; set; }
        [Display(Name = "PartyDate_44")]
        public string PartyDate_44 { get; set; }
        [Display(Name = "BrideFamilyName_44")]
        public string BrideFamilyName_44 { get; set; }
        [Display(Name = "GroomFamilyName_44")]
        public string GroomFamilyName_44 { get; set; }
        [Display(Name = "TantoName_44")]
        public string TantoName_44 { get; set; }
        [Display(Name = "ReporterName_44")]
        public string ReporterName_44 { get; set; }
        [Display(Name = "AdultCnt_44")]
        public string AdultCnt_44 { get; set; }
        [Display(Name = "HalfCnt_44")]
        public string HalfCnt_44 { get; set; }
        [Display(Name = "ChildrenCnt_44")]
        public string ChildrenCnt_44 { get; set; }
        [Display(Name = "SeatOnlyCnt_44")]
        public string SeatOnlyCnt_44 { get; set; }
        [Display(Name = "TableCnt_44")]
        public string TableCnt_44 { get; set; }
        [Display(Name = "TableCross_44")]
        public string TableCross_44 { get; set; }
        [Display(Name = "PartyStyleName_44")]
        public string PartyStyleName_44 { get; set; }
        [Display(Name = "FoodStyleName_44")]
        public string FoodStyleName_44 { get; set; }
        [Display(Name = "FoodPricce_44")]
        public string FoodPricce_44 { get; set; }
        [Display(Name = "DrinkPrice_44")]
        public string DrinkPrice_44 { get; set; }
        [Display(Name = "Wdrink_44")]
        public string Wdrink_44 { get; set; }
        [Display(Name = "Desl_44")]
        public string Desl_44 { get; set; }
        [Display(Name = "RestRoomFlg_44")]
        public string RestRoomFlg_44 { get; set; }
        [Display(Name = "AnketFlg_44")]
        public string AnketFlg_44 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_44")]
        public string PartyTime_TimeName_I_44 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_44")]
        public string PartyTime_OrderTime_I_44 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_44")]
        public string PartyTime_ActTime_I_44 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_44")]
        public string PartyTime_DelayTime_I_44 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_44")]
        public string PartyTime_TimeName_II_44 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_44")]
        public string PartyTime_OrderTime_II_44 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_44")]
        public string PartyTime_ActTime_II_44 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_44")]
        public string PartyTime_DelayTime_II_44 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_44")]
        public string PartyTime_TimeName_III_44 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_44")]
        public string PartyTime_OrderTime_III_44 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_44")]
        public string PartyTime_ActTime_III_44 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_44")]
        public string PartyTime_DelayTime_III_44 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_44")]
        public string PartyTime_TimeName_IV_44 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_44")]
        public string PartyTime_OrderTime_IV_44 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_44")]
        public string PartyTime_ActTime_IV_44 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_44")]
        public string PartyTime_DelayTime_IV_44 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_44")]
        public string PartyTime_TimeName_V_44 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_44")]
        public string PartyTime_OrderTime_V_44 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_44")]
        public string PartyTime_ActTime_V_44 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_44")]
        public string PartyTime_DelayTime_V_44 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_44")]
        public string PartyFood_FoodName_I_44 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_44")]
        public string PartyFood_BeginTime_I_44 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_44")]
        public string PartyFood_EndTime_I_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_44")]
        public string PartyFood_RestRoomTime_I_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_44")]
        public string PartyFood_RestRoomFlg_I_44 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_44")]
        public string PartyFood_FoodName_II_44 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_44")]
        public string PartyFood_BeginTime_II_44 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_44")]
        public string PartyFood_EndTime_II_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_44")]
        public string PartyFood_RestRoomTime_II_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_44")]
        public string PartyFood_RestRoomFlg_II_44 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_44")]
        public string PartyFood_FoodName_III_44 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_44")]
        public string PartyFood_BeginTime_III_44 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_44")]
        public string PartyFood_EndTime_III_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_44")]
        public string PartyFood_RestRoomTime_III_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_44")]
        public string PartyFood_RestRoomFlg_III_44 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_44")]
        public string PartyFood_FoodName_IV_44 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_44")]
        public string PartyFood_BeginTime_IV_44 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_44")]
        public string PartyFood_EndTime_IV_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_44")]
        public string PartyFood_RestRoomTime_IV_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_44")]
        public string PartyFood_RestRoomFlg_IV_44 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_44")]
        public string PartyFood_FoodName_V_44 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_44")]
        public string PartyFood_BeginTime_V_44 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_44")]
        public string PartyFood_EndTime_V_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_44")]
        public string PartyFood_RestRoomTime_V_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_44")]
        public string PartyFood_RestRoomFlg_V_44 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_44")]
        public string PartyFood_FoodName_VI_44 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_44")]
        public string PartyFood_BeginTime_VI_44 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_44")]
        public string PartyFood_EndTime_VI_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_44")]
        public string PartyFood_RestRoomTime_VI_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_44")]
        public string PartyFood_RestRoomFlg_VI_44 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_44")]
        public string PartyFood_FoodName_VII_44 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_44")]
        public string PartyFood_BeginTime_VII_44 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_44")]
        public string PartyFood_EndTime_VII_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_44")]
        public string PartyFood_RestRoomTime_VII_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_44")]
        public string PartyFood_RestRoomFlg_VII_44 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_44")]
        public string PartyFood_FoodName_VIII_44 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_44")]
        public string PartyFood_BeginTime_VIII_44 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_44")]
        public string PartyFood_EndTime_VIII_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_44")]
        public string PartyFood_RestRoomTime_VIII_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_44")]
        public string PartyFood_RestRoomFlg_VIII_44 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_44")]
        public string PartyFood_FoodName_IX_44 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_44")]
        public string PartyFood_BeginTime_IX_44 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_44")]
        public string PartyFood_EndTime_IX_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_44")]
        public string PartyFood_RestRoomTime_IX_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_44")]
        public string PartyFood_RestRoomFlg_IX_44 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_44")]
        public string PartyFood_FoodName_X_44 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_44")]
        public string PartyFood_BeginTime_X_44 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_44")]
        public string PartyFood_EndTime_X_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_44")]
        public string PartyFood_RestRoomTime_X_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_44")]
        public string PartyFood_RestRoomFlg_X_44 { get; set; }
        [Display(Name = "PartyMember_Name_I_44")]
        public string PartyMember_Name_I_44 { get; set; }
        [Display(Name = "PartyMember_Name_II_44")]
        public string PartyMember_Name_II_44 { get; set; }
        [Display(Name = "PartyMember_Name_III_44")]
        public string PartyMember_Name_III_44 { get; set; }
        [Display(Name = "PartyMember_Name_IV_44")]
        public string PartyMember_Name_IV_44 { get; set; }
        [Display(Name = "PartyMember_Name_V_44")]
        public string PartyMember_Name_V_44 { get; set; }
        [Display(Name = "PartyMember_Name_VI_44")]
        public string PartyMember_Name_VI_44 { get; set; }
        [Display(Name = "PartyMember_Name_VII_44")]
        public string PartyMember_Name_VII_44 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_44")]
        public string PartyMember_Name_VIII_44 { get; set; }
        [Display(Name = "PartyMember_Name_IX_44")]
        public string PartyMember_Name_IX_44 { get; set; }
        [Display(Name = "PartyDate_45")]
        public string PartyDate_45 { get; set; }
        [Display(Name = "BrideFamilyName_45")]
        public string BrideFamilyName_45 { get; set; }
        [Display(Name = "GroomFamilyName_45")]
        public string GroomFamilyName_45 { get; set; }
        [Display(Name = "TantoName_45")]
        public string TantoName_45 { get; set; }
        [Display(Name = "ReporterName_45")]
        public string ReporterName_45 { get; set; }
        [Display(Name = "AdultCnt_45")]
        public string AdultCnt_45 { get; set; }
        [Display(Name = "HalfCnt_45")]
        public string HalfCnt_45 { get; set; }
        [Display(Name = "ChildrenCnt_45")]
        public string ChildrenCnt_45 { get; set; }
        [Display(Name = "SeatOnlyCnt_45")]
        public string SeatOnlyCnt_45 { get; set; }
        [Display(Name = "TableCnt_45")]
        public string TableCnt_45 { get; set; }
        [Display(Name = "TableCross_45")]
        public string TableCross_45 { get; set; }
        [Display(Name = "PartyStyleName_45")]
        public string PartyStyleName_45 { get; set; }
        [Display(Name = "FoodStyleName_45")]
        public string FoodStyleName_45 { get; set; }
        [Display(Name = "FoodPricce_45")]
        public string FoodPricce_45 { get; set; }
        [Display(Name = "DrinkPrice_45")]
        public string DrinkPrice_45 { get; set; }
        [Display(Name = "Wdrink_45")]
        public string Wdrink_45 { get; set; }
        [Display(Name = "Desl_45")]
        public string Desl_45 { get; set; }
        [Display(Name = "RestRoomFlg_45")]
        public string RestRoomFlg_45 { get; set; }
        [Display(Name = "AnketFlg_45")]
        public string AnketFlg_45 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_45")]
        public string PartyTime_TimeName_I_45 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_45")]
        public string PartyTime_OrderTime_I_45 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_45")]
        public string PartyTime_ActTime_I_45 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_45")]
        public string PartyTime_DelayTime_I_45 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_45")]
        public string PartyTime_TimeName_II_45 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_45")]
        public string PartyTime_OrderTime_II_45 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_45")]
        public string PartyTime_ActTime_II_45 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_45")]
        public string PartyTime_DelayTime_II_45 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_45")]
        public string PartyTime_TimeName_III_45 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_45")]
        public string PartyTime_OrderTime_III_45 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_45")]
        public string PartyTime_ActTime_III_45 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_45")]
        public string PartyTime_DelayTime_III_45 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_45")]
        public string PartyTime_TimeName_IV_45 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_45")]
        public string PartyTime_OrderTime_IV_45 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_45")]
        public string PartyTime_ActTime_IV_45 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_45")]
        public string PartyTime_DelayTime_IV_45 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_45")]
        public string PartyTime_TimeName_V_45 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_45")]
        public string PartyTime_OrderTime_V_45 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_45")]
        public string PartyTime_ActTime_V_45 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_45")]
        public string PartyTime_DelayTime_V_45 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_45")]
        public string PartyFood_FoodName_I_45 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_45")]
        public string PartyFood_BeginTime_I_45 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_45")]
        public string PartyFood_EndTime_I_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_45")]
        public string PartyFood_RestRoomTime_I_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_45")]
        public string PartyFood_RestRoomFlg_I_45 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_45")]
        public string PartyFood_FoodName_II_45 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_45")]
        public string PartyFood_BeginTime_II_45 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_45")]
        public string PartyFood_EndTime_II_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_45")]
        public string PartyFood_RestRoomTime_II_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_45")]
        public string PartyFood_RestRoomFlg_II_45 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_45")]
        public string PartyFood_FoodName_III_45 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_45")]
        public string PartyFood_BeginTime_III_45 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_45")]
        public string PartyFood_EndTime_III_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_45")]
        public string PartyFood_RestRoomTime_III_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_45")]
        public string PartyFood_RestRoomFlg_III_45 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_45")]
        public string PartyFood_FoodName_IV_45 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_45")]
        public string PartyFood_BeginTime_IV_45 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_45")]
        public string PartyFood_EndTime_IV_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_45")]
        public string PartyFood_RestRoomTime_IV_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_45")]
        public string PartyFood_RestRoomFlg_IV_45 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_45")]
        public string PartyFood_FoodName_V_45 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_45")]
        public string PartyFood_BeginTime_V_45 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_45")]
        public string PartyFood_EndTime_V_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_45")]
        public string PartyFood_RestRoomTime_V_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_45")]
        public string PartyFood_RestRoomFlg_V_45 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_45")]
        public string PartyFood_FoodName_VI_45 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_45")]
        public string PartyFood_BeginTime_VI_45 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_45")]
        public string PartyFood_EndTime_VI_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_45")]
        public string PartyFood_RestRoomTime_VI_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_45")]
        public string PartyFood_RestRoomFlg_VI_45 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_45")]
        public string PartyFood_FoodName_VII_45 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_45")]
        public string PartyFood_BeginTime_VII_45 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_45")]
        public string PartyFood_EndTime_VII_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_45")]
        public string PartyFood_RestRoomTime_VII_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_45")]
        public string PartyFood_RestRoomFlg_VII_45 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_45")]
        public string PartyFood_FoodName_VIII_45 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_45")]
        public string PartyFood_BeginTime_VIII_45 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_45")]
        public string PartyFood_EndTime_VIII_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_45")]
        public string PartyFood_RestRoomTime_VIII_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_45")]
        public string PartyFood_RestRoomFlg_VIII_45 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_45")]
        public string PartyFood_FoodName_IX_45 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_45")]
        public string PartyFood_BeginTime_IX_45 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_45")]
        public string PartyFood_EndTime_IX_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_45")]
        public string PartyFood_RestRoomTime_IX_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_45")]
        public string PartyFood_RestRoomFlg_IX_45 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_45")]
        public string PartyFood_FoodName_X_45 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_45")]
        public string PartyFood_BeginTime_X_45 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_45")]
        public string PartyFood_EndTime_X_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_45")]
        public string PartyFood_RestRoomTime_X_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_45")]
        public string PartyFood_RestRoomFlg_X_45 { get; set; }
        [Display(Name = "PartyMember_Name_I_45")]
        public string PartyMember_Name_I_45 { get; set; }
        [Display(Name = "PartyMember_Name_II_45")]
        public string PartyMember_Name_II_45 { get; set; }
        [Display(Name = "PartyMember_Name_III_45")]
        public string PartyMember_Name_III_45 { get; set; }
        [Display(Name = "PartyMember_Name_IV_45")]
        public string PartyMember_Name_IV_45 { get; set; }
        [Display(Name = "PartyMember_Name_V_45")]
        public string PartyMember_Name_V_45 { get; set; }
        [Display(Name = "PartyMember_Name_VI_45")]
        public string PartyMember_Name_VI_45 { get; set; }
        [Display(Name = "PartyMember_Name_VII_45")]
        public string PartyMember_Name_VII_45 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_45")]
        public string PartyMember_Name_VIII_45 { get; set; }
        [Display(Name = "PartyMember_Name_IX_45")]
        public string PartyMember_Name_IX_45 { get; set; }
        [Display(Name = "PartyDate_46")]
        public string PartyDate_46 { get; set; }
        [Display(Name = "BrideFamilyName_46")]
        public string BrideFamilyName_46 { get; set; }
        [Display(Name = "GroomFamilyName_46")]
        public string GroomFamilyName_46 { get; set; }
        [Display(Name = "TantoName_46")]
        public string TantoName_46 { get; set; }
        [Display(Name = "ReporterName_46")]
        public string ReporterName_46 { get; set; }
        [Display(Name = "AdultCnt_46")]
        public string AdultCnt_46 { get; set; }
        [Display(Name = "HalfCnt_46")]
        public string HalfCnt_46 { get; set; }
        [Display(Name = "ChildrenCnt_46")]
        public string ChildrenCnt_46 { get; set; }
        [Display(Name = "SeatOnlyCnt_46")]
        public string SeatOnlyCnt_46 { get; set; }
        [Display(Name = "TableCnt_46")]
        public string TableCnt_46 { get; set; }
        [Display(Name = "TableCross_46")]
        public string TableCross_46 { get; set; }
        [Display(Name = "PartyStyleName_46")]
        public string PartyStyleName_46 { get; set; }
        [Display(Name = "FoodStyleName_46")]
        public string FoodStyleName_46 { get; set; }
        [Display(Name = "FoodPricce_46")]
        public string FoodPricce_46 { get; set; }
        [Display(Name = "DrinkPrice_46")]
        public string DrinkPrice_46 { get; set; }
        [Display(Name = "Wdrink_46")]
        public string Wdrink_46 { get; set; }
        [Display(Name = "Desl_46")]
        public string Desl_46 { get; set; }
        [Display(Name = "RestRoomFlg_46")]
        public string RestRoomFlg_46 { get; set; }
        [Display(Name = "AnketFlg_46")]
        public string AnketFlg_46 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_46")]
        public string PartyTime_TimeName_I_46 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_46")]
        public string PartyTime_OrderTime_I_46 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_46")]
        public string PartyTime_ActTime_I_46 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_46")]
        public string PartyTime_DelayTime_I_46 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_46")]
        public string PartyTime_TimeName_II_46 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_46")]
        public string PartyTime_OrderTime_II_46 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_46")]
        public string PartyTime_ActTime_II_46 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_46")]
        public string PartyTime_DelayTime_II_46 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_46")]
        public string PartyTime_TimeName_III_46 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_46")]
        public string PartyTime_OrderTime_III_46 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_46")]
        public string PartyTime_ActTime_III_46 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_46")]
        public string PartyTime_DelayTime_III_46 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_46")]
        public string PartyTime_TimeName_IV_46 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_46")]
        public string PartyTime_OrderTime_IV_46 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_46")]
        public string PartyTime_ActTime_IV_46 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_46")]
        public string PartyTime_DelayTime_IV_46 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_46")]
        public string PartyTime_TimeName_V_46 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_46")]
        public string PartyTime_OrderTime_V_46 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_46")]
        public string PartyTime_ActTime_V_46 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_46")]
        public string PartyTime_DelayTime_V_46 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_46")]
        public string PartyFood_FoodName_I_46 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_46")]
        public string PartyFood_BeginTime_I_46 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_46")]
        public string PartyFood_EndTime_I_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_46")]
        public string PartyFood_RestRoomTime_I_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_46")]
        public string PartyFood_RestRoomFlg_I_46 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_46")]
        public string PartyFood_FoodName_II_46 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_46")]
        public string PartyFood_BeginTime_II_46 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_46")]
        public string PartyFood_EndTime_II_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_46")]
        public string PartyFood_RestRoomTime_II_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_46")]
        public string PartyFood_RestRoomFlg_II_46 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_46")]
        public string PartyFood_FoodName_III_46 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_46")]
        public string PartyFood_BeginTime_III_46 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_46")]
        public string PartyFood_EndTime_III_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_46")]
        public string PartyFood_RestRoomTime_III_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_46")]
        public string PartyFood_RestRoomFlg_III_46 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_46")]
        public string PartyFood_FoodName_IV_46 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_46")]
        public string PartyFood_BeginTime_IV_46 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_46")]
        public string PartyFood_EndTime_IV_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_46")]
        public string PartyFood_RestRoomTime_IV_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_46")]
        public string PartyFood_RestRoomFlg_IV_46 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_46")]
        public string PartyFood_FoodName_V_46 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_46")]
        public string PartyFood_BeginTime_V_46 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_46")]
        public string PartyFood_EndTime_V_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_46")]
        public string PartyFood_RestRoomTime_V_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_46")]
        public string PartyFood_RestRoomFlg_V_46 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_46")]
        public string PartyFood_FoodName_VI_46 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_46")]
        public string PartyFood_BeginTime_VI_46 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_46")]
        public string PartyFood_EndTime_VI_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_46")]
        public string PartyFood_RestRoomTime_VI_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_46")]
        public string PartyFood_RestRoomFlg_VI_46 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_46")]
        public string PartyFood_FoodName_VII_46 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_46")]
        public string PartyFood_BeginTime_VII_46 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_46")]
        public string PartyFood_EndTime_VII_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_46")]
        public string PartyFood_RestRoomTime_VII_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_46")]
        public string PartyFood_RestRoomFlg_VII_46 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_46")]
        public string PartyFood_FoodName_VIII_46 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_46")]
        public string PartyFood_BeginTime_VIII_46 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_46")]
        public string PartyFood_EndTime_VIII_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_46")]
        public string PartyFood_RestRoomTime_VIII_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_46")]
        public string PartyFood_RestRoomFlg_VIII_46 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_46")]
        public string PartyFood_FoodName_IX_46 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_46")]
        public string PartyFood_BeginTime_IX_46 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_46")]
        public string PartyFood_EndTime_IX_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_46")]
        public string PartyFood_RestRoomTime_IX_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_46")]
        public string PartyFood_RestRoomFlg_IX_46 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_46")]
        public string PartyFood_FoodName_X_46 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_46")]
        public string PartyFood_BeginTime_X_46 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_46")]
        public string PartyFood_EndTime_X_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_46")]
        public string PartyFood_RestRoomTime_X_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_46")]
        public string PartyFood_RestRoomFlg_X_46 { get; set; }
        [Display(Name = "PartyMember_Name_I_46")]
        public string PartyMember_Name_I_46 { get; set; }
        [Display(Name = "PartyMember_Name_II_46")]
        public string PartyMember_Name_II_46 { get; set; }
        [Display(Name = "PartyMember_Name_III_46")]
        public string PartyMember_Name_III_46 { get; set; }
        [Display(Name = "PartyMember_Name_IV_46")]
        public string PartyMember_Name_IV_46 { get; set; }
        [Display(Name = "PartyMember_Name_V_46")]
        public string PartyMember_Name_V_46 { get; set; }
        [Display(Name = "PartyMember_Name_VI_46")]
        public string PartyMember_Name_VI_46 { get; set; }
        [Display(Name = "PartyMember_Name_VII_46")]
        public string PartyMember_Name_VII_46 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_46")]
        public string PartyMember_Name_VIII_46 { get; set; }
        [Display(Name = "PartyMember_Name_IX_46")]
        public string PartyMember_Name_IX_46 { get; set; }
        [Display(Name = "PartyDate_47")]
        public string PartyDate_47 { get; set; }
        [Display(Name = "BrideFamilyName_47")]
        public string BrideFamilyName_47 { get; set; }
        [Display(Name = "GroomFamilyName_47")]
        public string GroomFamilyName_47 { get; set; }
        [Display(Name = "TantoName_47")]
        public string TantoName_47 { get; set; }
        [Display(Name = "ReporterName_47")]
        public string ReporterName_47 { get; set; }
        [Display(Name = "AdultCnt_47")]
        public string AdultCnt_47 { get; set; }
        [Display(Name = "HalfCnt_47")]
        public string HalfCnt_47 { get; set; }
        [Display(Name = "ChildrenCnt_47")]
        public string ChildrenCnt_47 { get; set; }
        [Display(Name = "SeatOnlyCnt_47")]
        public string SeatOnlyCnt_47 { get; set; }
        [Display(Name = "TableCnt_47")]
        public string TableCnt_47 { get; set; }
        [Display(Name = "TableCross_47")]
        public string TableCross_47 { get; set; }
        [Display(Name = "PartyStyleName_47")]
        public string PartyStyleName_47 { get; set; }
        [Display(Name = "FoodStyleName_47")]
        public string FoodStyleName_47 { get; set; }
        [Display(Name = "FoodPricce_47")]
        public string FoodPricce_47 { get; set; }
        [Display(Name = "DrinkPrice_47")]
        public string DrinkPrice_47 { get; set; }
        [Display(Name = "Wdrink_47")]
        public string Wdrink_47 { get; set; }
        [Display(Name = "Desl_47")]
        public string Desl_47 { get; set; }
        [Display(Name = "RestRoomFlg_47")]
        public string RestRoomFlg_47 { get; set; }
        [Display(Name = "AnketFlg_47")]
        public string AnketFlg_47 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_47")]
        public string PartyTime_TimeName_I_47 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_47")]
        public string PartyTime_OrderTime_I_47 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_47")]
        public string PartyTime_ActTime_I_47 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_47")]
        public string PartyTime_DelayTime_I_47 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_47")]
        public string PartyTime_TimeName_II_47 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_47")]
        public string PartyTime_OrderTime_II_47 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_47")]
        public string PartyTime_ActTime_II_47 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_47")]
        public string PartyTime_DelayTime_II_47 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_47")]
        public string PartyTime_TimeName_III_47 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_47")]
        public string PartyTime_OrderTime_III_47 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_47")]
        public string PartyTime_ActTime_III_47 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_47")]
        public string PartyTime_DelayTime_III_47 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_47")]
        public string PartyTime_TimeName_IV_47 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_47")]
        public string PartyTime_OrderTime_IV_47 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_47")]
        public string PartyTime_ActTime_IV_47 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_47")]
        public string PartyTime_DelayTime_IV_47 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_47")]
        public string PartyTime_TimeName_V_47 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_47")]
        public string PartyTime_OrderTime_V_47 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_47")]
        public string PartyTime_ActTime_V_47 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_47")]
        public string PartyTime_DelayTime_V_47 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_47")]
        public string PartyFood_FoodName_I_47 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_47")]
        public string PartyFood_BeginTime_I_47 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_47")]
        public string PartyFood_EndTime_I_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_47")]
        public string PartyFood_RestRoomTime_I_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_47")]
        public string PartyFood_RestRoomFlg_I_47 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_47")]
        public string PartyFood_FoodName_II_47 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_47")]
        public string PartyFood_BeginTime_II_47 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_47")]
        public string PartyFood_EndTime_II_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_47")]
        public string PartyFood_RestRoomTime_II_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_47")]
        public string PartyFood_RestRoomFlg_II_47 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_47")]
        public string PartyFood_FoodName_III_47 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_47")]
        public string PartyFood_BeginTime_III_47 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_47")]
        public string PartyFood_EndTime_III_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_47")]
        public string PartyFood_RestRoomTime_III_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_47")]
        public string PartyFood_RestRoomFlg_III_47 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_47")]
        public string PartyFood_FoodName_IV_47 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_47")]
        public string PartyFood_BeginTime_IV_47 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_47")]
        public string PartyFood_EndTime_IV_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_47")]
        public string PartyFood_RestRoomTime_IV_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_47")]
        public string PartyFood_RestRoomFlg_IV_47 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_47")]
        public string PartyFood_FoodName_V_47 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_47")]
        public string PartyFood_BeginTime_V_47 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_47")]
        public string PartyFood_EndTime_V_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_47")]
        public string PartyFood_RestRoomTime_V_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_47")]
        public string PartyFood_RestRoomFlg_V_47 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_47")]
        public string PartyFood_FoodName_VI_47 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_47")]
        public string PartyFood_BeginTime_VI_47 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_47")]
        public string PartyFood_EndTime_VI_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_47")]
        public string PartyFood_RestRoomTime_VI_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_47")]
        public string PartyFood_RestRoomFlg_VI_47 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_47")]
        public string PartyFood_FoodName_VII_47 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_47")]
        public string PartyFood_BeginTime_VII_47 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_47")]
        public string PartyFood_EndTime_VII_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_47")]
        public string PartyFood_RestRoomTime_VII_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_47")]
        public string PartyFood_RestRoomFlg_VII_47 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_47")]
        public string PartyFood_FoodName_VIII_47 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_47")]
        public string PartyFood_BeginTime_VIII_47 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_47")]
        public string PartyFood_EndTime_VIII_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_47")]
        public string PartyFood_RestRoomTime_VIII_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_47")]
        public string PartyFood_RestRoomFlg_VIII_47 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_47")]
        public string PartyFood_FoodName_IX_47 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_47")]
        public string PartyFood_BeginTime_IX_47 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_47")]
        public string PartyFood_EndTime_IX_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_47")]
        public string PartyFood_RestRoomTime_IX_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_47")]
        public string PartyFood_RestRoomFlg_IX_47 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_47")]
        public string PartyFood_FoodName_X_47 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_47")]
        public string PartyFood_BeginTime_X_47 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_47")]
        public string PartyFood_EndTime_X_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_47")]
        public string PartyFood_RestRoomTime_X_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_47")]
        public string PartyFood_RestRoomFlg_X_47 { get; set; }
        [Display(Name = "PartyMember_Name_I_47")]
        public string PartyMember_Name_I_47 { get; set; }
        [Display(Name = "PartyMember_Name_II_47")]
        public string PartyMember_Name_II_47 { get; set; }
        [Display(Name = "PartyMember_Name_III_47")]
        public string PartyMember_Name_III_47 { get; set; }
        [Display(Name = "PartyMember_Name_IV_47")]
        public string PartyMember_Name_IV_47 { get; set; }
        [Display(Name = "PartyMember_Name_V_47")]
        public string PartyMember_Name_V_47 { get; set; }
        [Display(Name = "PartyMember_Name_VI_47")]
        public string PartyMember_Name_VI_47 { get; set; }
        [Display(Name = "PartyMember_Name_VII_47")]
        public string PartyMember_Name_VII_47 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_47")]
        public string PartyMember_Name_VIII_47 { get; set; }
        [Display(Name = "PartyMember_Name_IX_47")]
        public string PartyMember_Name_IX_47 { get; set; }
        [Display(Name = "PartyDate_48")]
        public string PartyDate_48 { get; set; }
        [Display(Name = "BrideFamilyName_48")]
        public string BrideFamilyName_48 { get; set; }
        [Display(Name = "GroomFamilyName_48")]
        public string GroomFamilyName_48 { get; set; }
        [Display(Name = "TantoName_48")]
        public string TantoName_48 { get; set; }
        [Display(Name = "ReporterName_48")]
        public string ReporterName_48 { get; set; }
        [Display(Name = "AdultCnt_48")]
        public string AdultCnt_48 { get; set; }
        [Display(Name = "HalfCnt_48")]
        public string HalfCnt_48 { get; set; }
        [Display(Name = "ChildrenCnt_48")]
        public string ChildrenCnt_48 { get; set; }
        [Display(Name = "SeatOnlyCnt_48")]
        public string SeatOnlyCnt_48 { get; set; }
        [Display(Name = "TableCnt_48")]
        public string TableCnt_48 { get; set; }
        [Display(Name = "TableCross_48")]
        public string TableCross_48 { get; set; }
        [Display(Name = "PartyStyleName_48")]
        public string PartyStyleName_48 { get; set; }
        [Display(Name = "FoodStyleName_48")]
        public string FoodStyleName_48 { get; set; }
        [Display(Name = "FoodPricce_48")]
        public string FoodPricce_48 { get; set; }
        [Display(Name = "DrinkPrice_48")]
        public string DrinkPrice_48 { get; set; }
        [Display(Name = "Wdrink_48")]
        public string Wdrink_48 { get; set; }
        [Display(Name = "Desl_48")]
        public string Desl_48 { get; set; }
        [Display(Name = "RestRoomFlg_48")]
        public string RestRoomFlg_48 { get; set; }
        [Display(Name = "AnketFlg_48")]
        public string AnketFlg_48 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_48")]
        public string PartyTime_TimeName_I_48 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_48")]
        public string PartyTime_OrderTime_I_48 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_48")]
        public string PartyTime_ActTime_I_48 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_48")]
        public string PartyTime_DelayTime_I_48 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_48")]
        public string PartyTime_TimeName_II_48 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_48")]
        public string PartyTime_OrderTime_II_48 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_48")]
        public string PartyTime_ActTime_II_48 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_48")]
        public string PartyTime_DelayTime_II_48 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_48")]
        public string PartyTime_TimeName_III_48 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_48")]
        public string PartyTime_OrderTime_III_48 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_48")]
        public string PartyTime_ActTime_III_48 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_48")]
        public string PartyTime_DelayTime_III_48 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_48")]
        public string PartyTime_TimeName_IV_48 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_48")]
        public string PartyTime_OrderTime_IV_48 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_48")]
        public string PartyTime_ActTime_IV_48 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_48")]
        public string PartyTime_DelayTime_IV_48 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_48")]
        public string PartyTime_TimeName_V_48 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_48")]
        public string PartyTime_OrderTime_V_48 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_48")]
        public string PartyTime_ActTime_V_48 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_48")]
        public string PartyTime_DelayTime_V_48 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_48")]
        public string PartyFood_FoodName_I_48 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_48")]
        public string PartyFood_BeginTime_I_48 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_48")]
        public string PartyFood_EndTime_I_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_48")]
        public string PartyFood_RestRoomTime_I_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_48")]
        public string PartyFood_RestRoomFlg_I_48 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_48")]
        public string PartyFood_FoodName_II_48 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_48")]
        public string PartyFood_BeginTime_II_48 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_48")]
        public string PartyFood_EndTime_II_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_48")]
        public string PartyFood_RestRoomTime_II_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_48")]
        public string PartyFood_RestRoomFlg_II_48 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_48")]
        public string PartyFood_FoodName_III_48 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_48")]
        public string PartyFood_BeginTime_III_48 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_48")]
        public string PartyFood_EndTime_III_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_48")]
        public string PartyFood_RestRoomTime_III_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_48")]
        public string PartyFood_RestRoomFlg_III_48 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_48")]
        public string PartyFood_FoodName_IV_48 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_48")]
        public string PartyFood_BeginTime_IV_48 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_48")]
        public string PartyFood_EndTime_IV_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_48")]
        public string PartyFood_RestRoomTime_IV_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_48")]
        public string PartyFood_RestRoomFlg_IV_48 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_48")]
        public string PartyFood_FoodName_V_48 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_48")]
        public string PartyFood_BeginTime_V_48 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_48")]
        public string PartyFood_EndTime_V_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_48")]
        public string PartyFood_RestRoomTime_V_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_48")]
        public string PartyFood_RestRoomFlg_V_48 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_48")]
        public string PartyFood_FoodName_VI_48 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_48")]
        public string PartyFood_BeginTime_VI_48 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_48")]
        public string PartyFood_EndTime_VI_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_48")]
        public string PartyFood_RestRoomTime_VI_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_48")]
        public string PartyFood_RestRoomFlg_VI_48 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_48")]
        public string PartyFood_FoodName_VII_48 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_48")]
        public string PartyFood_BeginTime_VII_48 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_48")]
        public string PartyFood_EndTime_VII_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_48")]
        public string PartyFood_RestRoomTime_VII_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_48")]
        public string PartyFood_RestRoomFlg_VII_48 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_48")]
        public string PartyFood_FoodName_VIII_48 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_48")]
        public string PartyFood_BeginTime_VIII_48 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_48")]
        public string PartyFood_EndTime_VIII_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_48")]
        public string PartyFood_RestRoomTime_VIII_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_48")]
        public string PartyFood_RestRoomFlg_VIII_48 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_48")]
        public string PartyFood_FoodName_IX_48 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_48")]
        public string PartyFood_BeginTime_IX_48 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_48")]
        public string PartyFood_EndTime_IX_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_48")]
        public string PartyFood_RestRoomTime_IX_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_48")]
        public string PartyFood_RestRoomFlg_IX_48 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_48")]
        public string PartyFood_FoodName_X_48 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_48")]
        public string PartyFood_BeginTime_X_48 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_48")]
        public string PartyFood_EndTime_X_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_48")]
        public string PartyFood_RestRoomTime_X_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_48")]
        public string PartyFood_RestRoomFlg_X_48 { get; set; }
        [Display(Name = "PartyMember_Name_I_48")]
        public string PartyMember_Name_I_48 { get; set; }
        [Display(Name = "PartyMember_Name_II_48")]
        public string PartyMember_Name_II_48 { get; set; }
        [Display(Name = "PartyMember_Name_III_48")]
        public string PartyMember_Name_III_48 { get; set; }
        [Display(Name = "PartyMember_Name_IV_48")]
        public string PartyMember_Name_IV_48 { get; set; }
        [Display(Name = "PartyMember_Name_V_48")]
        public string PartyMember_Name_V_48 { get; set; }
        [Display(Name = "PartyMember_Name_VI_48")]
        public string PartyMember_Name_VI_48 { get; set; }
        [Display(Name = "PartyMember_Name_VII_48")]
        public string PartyMember_Name_VII_48 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_48")]
        public string PartyMember_Name_VIII_48 { get; set; }
        [Display(Name = "PartyMember_Name_IX_48")]
        public string PartyMember_Name_IX_48 { get; set; }
        [Display(Name = "PartyDate_49")]
        public string PartyDate_49 { get; set; }
        [Display(Name = "BrideFamilyName_49")]
        public string BrideFamilyName_49 { get; set; }
        [Display(Name = "GroomFamilyName_49")]
        public string GroomFamilyName_49 { get; set; }
        [Display(Name = "TantoName_49")]
        public string TantoName_49 { get; set; }
        [Display(Name = "ReporterName_49")]
        public string ReporterName_49 { get; set; }
        [Display(Name = "AdultCnt_49")]
        public string AdultCnt_49 { get; set; }
        [Display(Name = "HalfCnt_49")]
        public string HalfCnt_49 { get; set; }
        [Display(Name = "ChildrenCnt_49")]
        public string ChildrenCnt_49 { get; set; }
        [Display(Name = "SeatOnlyCnt_49")]
        public string SeatOnlyCnt_49 { get; set; }
        [Display(Name = "TableCnt_49")]
        public string TableCnt_49 { get; set; }
        [Display(Name = "TableCross_49")]
        public string TableCross_49 { get; set; }
        [Display(Name = "PartyStyleName_49")]
        public string PartyStyleName_49 { get; set; }
        [Display(Name = "FoodStyleName_49")]
        public string FoodStyleName_49 { get; set; }
        [Display(Name = "FoodPricce_49")]
        public string FoodPricce_49 { get; set; }
        [Display(Name = "DrinkPrice_49")]
        public string DrinkPrice_49 { get; set; }
        [Display(Name = "Wdrink_49")]
        public string Wdrink_49 { get; set; }
        [Display(Name = "Desl_49")]
        public string Desl_49 { get; set; }
        [Display(Name = "RestRoomFlg_49")]
        public string RestRoomFlg_49 { get; set; }
        [Display(Name = "AnketFlg_49")]
        public string AnketFlg_49 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_49")]
        public string PartyTime_TimeName_I_49 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_49")]
        public string PartyTime_OrderTime_I_49 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_49")]
        public string PartyTime_ActTime_I_49 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_49")]
        public string PartyTime_DelayTime_I_49 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_49")]
        public string PartyTime_TimeName_II_49 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_49")]
        public string PartyTime_OrderTime_II_49 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_49")]
        public string PartyTime_ActTime_II_49 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_49")]
        public string PartyTime_DelayTime_II_49 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_49")]
        public string PartyTime_TimeName_III_49 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_49")]
        public string PartyTime_OrderTime_III_49 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_49")]
        public string PartyTime_ActTime_III_49 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_49")]
        public string PartyTime_DelayTime_III_49 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_49")]
        public string PartyTime_TimeName_IV_49 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_49")]
        public string PartyTime_OrderTime_IV_49 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_49")]
        public string PartyTime_ActTime_IV_49 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_49")]
        public string PartyTime_DelayTime_IV_49 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_49")]
        public string PartyTime_TimeName_V_49 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_49")]
        public string PartyTime_OrderTime_V_49 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_49")]
        public string PartyTime_ActTime_V_49 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_49")]
        public string PartyTime_DelayTime_V_49 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_49")]
        public string PartyFood_FoodName_I_49 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_49")]
        public string PartyFood_BeginTime_I_49 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_49")]
        public string PartyFood_EndTime_I_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_49")]
        public string PartyFood_RestRoomTime_I_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_49")]
        public string PartyFood_RestRoomFlg_I_49 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_49")]
        public string PartyFood_FoodName_II_49 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_49")]
        public string PartyFood_BeginTime_II_49 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_49")]
        public string PartyFood_EndTime_II_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_49")]
        public string PartyFood_RestRoomTime_II_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_49")]
        public string PartyFood_RestRoomFlg_II_49 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_49")]
        public string PartyFood_FoodName_III_49 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_49")]
        public string PartyFood_BeginTime_III_49 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_49")]
        public string PartyFood_EndTime_III_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_49")]
        public string PartyFood_RestRoomTime_III_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_49")]
        public string PartyFood_RestRoomFlg_III_49 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_49")]
        public string PartyFood_FoodName_IV_49 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_49")]
        public string PartyFood_BeginTime_IV_49 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_49")]
        public string PartyFood_EndTime_IV_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_49")]
        public string PartyFood_RestRoomTime_IV_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_49")]
        public string PartyFood_RestRoomFlg_IV_49 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_49")]
        public string PartyFood_FoodName_V_49 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_49")]
        public string PartyFood_BeginTime_V_49 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_49")]
        public string PartyFood_EndTime_V_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_49")]
        public string PartyFood_RestRoomTime_V_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_49")]
        public string PartyFood_RestRoomFlg_V_49 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_49")]
        public string PartyFood_FoodName_VI_49 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_49")]
        public string PartyFood_BeginTime_VI_49 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_49")]
        public string PartyFood_EndTime_VI_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_49")]
        public string PartyFood_RestRoomTime_VI_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_49")]
        public string PartyFood_RestRoomFlg_VI_49 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_49")]
        public string PartyFood_FoodName_VII_49 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_49")]
        public string PartyFood_BeginTime_VII_49 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_49")]
        public string PartyFood_EndTime_VII_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_49")]
        public string PartyFood_RestRoomTime_VII_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_49")]
        public string PartyFood_RestRoomFlg_VII_49 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_49")]
        public string PartyFood_FoodName_VIII_49 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_49")]
        public string PartyFood_BeginTime_VIII_49 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_49")]
        public string PartyFood_EndTime_VIII_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_49")]
        public string PartyFood_RestRoomTime_VIII_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_49")]
        public string PartyFood_RestRoomFlg_VIII_49 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_49")]
        public string PartyFood_FoodName_IX_49 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_49")]
        public string PartyFood_BeginTime_IX_49 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_49")]
        public string PartyFood_EndTime_IX_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_49")]
        public string PartyFood_RestRoomTime_IX_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_49")]
        public string PartyFood_RestRoomFlg_IX_49 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_49")]
        public string PartyFood_FoodName_X_49 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_49")]
        public string PartyFood_BeginTime_X_49 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_49")]
        public string PartyFood_EndTime_X_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_49")]
        public string PartyFood_RestRoomTime_X_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_49")]
        public string PartyFood_RestRoomFlg_X_49 { get; set; }
        [Display(Name = "PartyMember_Name_I_49")]
        public string PartyMember_Name_I_49 { get; set; }
        [Display(Name = "PartyMember_Name_II_49")]
        public string PartyMember_Name_II_49 { get; set; }
        [Display(Name = "PartyMember_Name_III_49")]
        public string PartyMember_Name_III_49 { get; set; }
        [Display(Name = "PartyMember_Name_IV_49")]
        public string PartyMember_Name_IV_49 { get; set; }
        [Display(Name = "PartyMember_Name_V_49")]
        public string PartyMember_Name_V_49 { get; set; }
        [Display(Name = "PartyMember_Name_VI_49")]
        public string PartyMember_Name_VI_49 { get; set; }
        [Display(Name = "PartyMember_Name_VII_49")]
        public string PartyMember_Name_VII_49 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_49")]
        public string PartyMember_Name_VIII_49 { get; set; }
        [Display(Name = "PartyMember_Name_IX_49")]
        public string PartyMember_Name_IX_49 { get; set; }
        [Display(Name = "PartyDate_50")]
        public string PartyDate_50 { get; set; }
        [Display(Name = "BrideFamilyName_50")]
        public string BrideFamilyName_50 { get; set; }
        [Display(Name = "GroomFamilyName_50")]
        public string GroomFamilyName_50 { get; set; }
        [Display(Name = "TantoName_50")]
        public string TantoName_50 { get; set; }
        [Display(Name = "ReporterName_50")]
        public string ReporterName_50 { get; set; }
        [Display(Name = "AdultCnt_50")]
        public string AdultCnt_50 { get; set; }
        [Display(Name = "HalfCnt_50")]
        public string HalfCnt_50 { get; set; }
        [Display(Name = "ChildrenCnt_50")]
        public string ChildrenCnt_50 { get; set; }
        [Display(Name = "SeatOnlyCnt_50")]
        public string SeatOnlyCnt_50 { get; set; }
        [Display(Name = "TableCnt_50")]
        public string TableCnt_50 { get; set; }
        [Display(Name = "TableCross_50")]
        public string TableCross_50 { get; set; }
        [Display(Name = "PartyStyleName_50")]
        public string PartyStyleName_50 { get; set; }
        [Display(Name = "FoodStyleName_50")]
        public string FoodStyleName_50 { get; set; }
        [Display(Name = "FoodPricce_50")]
        public string FoodPricce_50 { get; set; }
        [Display(Name = "DrinkPrice_50")]
        public string DrinkPrice_50 { get; set; }
        [Display(Name = "Wdrink_50")]
        public string Wdrink_50 { get; set; }
        [Display(Name = "Desl_50")]
        public string Desl_50 { get; set; }
        [Display(Name = "RestRoomFlg_50")]
        public string RestRoomFlg_50 { get; set; }
        [Display(Name = "AnketFlg_50")]
        public string AnketFlg_50 { get; set; }
        [Display(Name = "PartyTime_TimeName_I_50")]
        public string PartyTime_TimeName_I_50 { get; set; }
        [Display(Name = "PartyTime_OrderTime_I_50")]
        public string PartyTime_OrderTime_I_50 { get; set; }
        [Display(Name = "PartyTime_ActTime_I_50")]
        public string PartyTime_ActTime_I_50 { get; set; }
        [Display(Name = "PartyTime_DelayTime_I_50")]
        public string PartyTime_DelayTime_I_50 { get; set; }
        [Display(Name = "PartyTime_TimeName_II_50")]
        public string PartyTime_TimeName_II_50 { get; set; }
        [Display(Name = "PartyTime_OrderTime_II_50")]
        public string PartyTime_OrderTime_II_50 { get; set; }
        [Display(Name = "PartyTime_ActTime_II_50")]
        public string PartyTime_ActTime_II_50 { get; set; }
        [Display(Name = "PartyTime_DelayTime_II_50")]
        public string PartyTime_DelayTime_II_50 { get; set; }
        [Display(Name = "PartyTime_TimeName_III_50")]
        public string PartyTime_TimeName_III_50 { get; set; }
        [Display(Name = "PartyTime_OrderTime_III_50")]
        public string PartyTime_OrderTime_III_50 { get; set; }
        [Display(Name = "PartyTime_ActTime_III_50")]
        public string PartyTime_ActTime_III_50 { get; set; }
        [Display(Name = "PartyTime_DelayTime_III_50")]
        public string PartyTime_DelayTime_III_50 { get; set; }
        [Display(Name = "PartyTime_TimeName_IV_50")]
        public string PartyTime_TimeName_IV_50 { get; set; }
        [Display(Name = "PartyTime_OrderTime_IV_50")]
        public string PartyTime_OrderTime_IV_50 { get; set; }
        [Display(Name = "PartyTime_ActTime_IV_50")]
        public string PartyTime_ActTime_IV_50 { get; set; }
        [Display(Name = "PartyTime_DelayTime_IV_50")]
        public string PartyTime_DelayTime_IV_50 { get; set; }
        [Display(Name = "PartyTime_TimeName_V_50")]
        public string PartyTime_TimeName_V_50 { get; set; }
        [Display(Name = "PartyTime_OrderTime_V_50")]
        public string PartyTime_OrderTime_V_50 { get; set; }
        [Display(Name = "PartyTime_ActTime_V_50")]
        public string PartyTime_ActTime_V_50 { get; set; }
        [Display(Name = "PartyTime_DelayTime_V_50")]
        public string PartyTime_DelayTime_V_50 { get; set; }
        [Display(Name = "PartyFood_FoodName_I_50")]
        public string PartyFood_FoodName_I_50 { get; set; }
        [Display(Name = "PartyFood_BeginTime_I_50")]
        public string PartyFood_BeginTime_I_50 { get; set; }
        [Display(Name = "PartyFood_EndTime_I_50")]
        public string PartyFood_EndTime_I_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_I_50")]
        public string PartyFood_RestRoomTime_I_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_I_50")]
        public string PartyFood_RestRoomFlg_I_50 { get; set; }
        [Display(Name = "PartyFood_FoodName_II_50")]
        public string PartyFood_FoodName_II_50 { get; set; }
        [Display(Name = "PartyFood_BeginTime_II_50")]
        public string PartyFood_BeginTime_II_50 { get; set; }
        [Display(Name = "PartyFood_EndTime_II_50")]
        public string PartyFood_EndTime_II_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_II_50")]
        public string PartyFood_RestRoomTime_II_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_II_50")]
        public string PartyFood_RestRoomFlg_II_50 { get; set; }
        [Display(Name = "PartyFood_FoodName_III_50")]
        public string PartyFood_FoodName_III_50 { get; set; }
        [Display(Name = "PartyFood_BeginTime_III_50")]
        public string PartyFood_BeginTime_III_50 { get; set; }
        [Display(Name = "PartyFood_EndTime_III_50")]
        public string PartyFood_EndTime_III_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_III_50")]
        public string PartyFood_RestRoomTime_III_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_III_50")]
        public string PartyFood_RestRoomFlg_III_50 { get; set; }
        [Display(Name = "PartyFood_FoodName_IV_50")]
        public string PartyFood_FoodName_IV_50 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IV_50")]
        public string PartyFood_BeginTime_IV_50 { get; set; }
        [Display(Name = "PartyFood_EndTime_IV_50")]
        public string PartyFood_EndTime_IV_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IV_50")]
        public string PartyFood_RestRoomTime_IV_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IV_50")]
        public string PartyFood_RestRoomFlg_IV_50 { get; set; }
        [Display(Name = "PartyFood_FoodName_V_50")]
        public string PartyFood_FoodName_V_50 { get; set; }
        [Display(Name = "PartyFood_BeginTime_V_50")]
        public string PartyFood_BeginTime_V_50 { get; set; }
        [Display(Name = "PartyFood_EndTime_V_50")]
        public string PartyFood_EndTime_V_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_V_50")]
        public string PartyFood_RestRoomTime_V_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_V_50")]
        public string PartyFood_RestRoomFlg_V_50 { get; set; }
        [Display(Name = "PartyFood_FoodName_VI_50")]
        public string PartyFood_FoodName_VI_50 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VI_50")]
        public string PartyFood_BeginTime_VI_50 { get; set; }
        [Display(Name = "PartyFood_EndTime_VI_50")]
        public string PartyFood_EndTime_VI_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VI_50")]
        public string PartyFood_RestRoomTime_VI_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VI_50")]
        public string PartyFood_RestRoomFlg_VI_50 { get; set; }
        [Display(Name = "PartyFood_FoodName_VII_50")]
        public string PartyFood_FoodName_VII_50 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VII_50")]
        public string PartyFood_BeginTime_VII_50 { get; set; }
        [Display(Name = "PartyFood_EndTime_VII_50")]
        public string PartyFood_EndTime_VII_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VII_50")]
        public string PartyFood_RestRoomTime_VII_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VII_50")]
        public string PartyFood_RestRoomFlg_VII_50 { get; set; }
        [Display(Name = "PartyFood_FoodName_VIII_50")]
        public string PartyFood_FoodName_VIII_50 { get; set; }
        [Display(Name = "PartyFood_BeginTime_VIII_50")]
        public string PartyFood_BeginTime_VIII_50 { get; set; }
        [Display(Name = "PartyFood_EndTime_VIII_50")]
        public string PartyFood_EndTime_VIII_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_VIII_50")]
        public string PartyFood_RestRoomTime_VIII_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_VIII_50")]
        public string PartyFood_RestRoomFlg_VIII_50 { get; set; }
        [Display(Name = "PartyFood_FoodName_IX_50")]
        public string PartyFood_FoodName_IX_50 { get; set; }
        [Display(Name = "PartyFood_BeginTime_IX_50")]
        public string PartyFood_BeginTime_IX_50 { get; set; }
        [Display(Name = "PartyFood_EndTime_IX_50")]
        public string PartyFood_EndTime_IX_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_IX_50")]
        public string PartyFood_RestRoomTime_IX_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_IX_50")]
        public string PartyFood_RestRoomFlg_IX_50 { get; set; }
        [Display(Name = "PartyFood_FoodName_X_50")]
        public string PartyFood_FoodName_X_50 { get; set; }
        [Display(Name = "PartyFood_BeginTime_X_50")]
        public string PartyFood_BeginTime_X_50 { get; set; }
        [Display(Name = "PartyFood_EndTime_X_50")]
        public string PartyFood_EndTime_X_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_X_50")]
        public string PartyFood_RestRoomTime_X_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_X_50")]
        public string PartyFood_RestRoomFlg_X_50 { get; set; }
        [Display(Name = "PartyMember_Name_I_50")]
        public string PartyMember_Name_I_50 { get; set; }
        [Display(Name = "PartyMember_Name_II_50")]
        public string PartyMember_Name_II_50 { get; set; }
        [Display(Name = "PartyMember_Name_III_50")]
        public string PartyMember_Name_III_50 { get; set; }
        [Display(Name = "PartyMember_Name_IV_50")]
        public string PartyMember_Name_IV_50 { get; set; }
        [Display(Name = "PartyMember_Name_V_50")]
        public string PartyMember_Name_V_50 { get; set; }
        [Display(Name = "PartyMember_Name_VI_50")]
        public string PartyMember_Name_VI_50 { get; set; }
        [Display(Name = "PartyMember_Name_VII_50")]
        public string PartyMember_Name_VII_50 { get; set; }
        [Display(Name = "PartyMember_Name_VIII_50")]
        public string PartyMember_Name_VIII_50 { get; set; }
        [Display(Name = "PartyMember_Name_IX_50")]
        public string PartyMember_Name_IX_50 { get; set; }


        [Display(Name = "PartyTime_OrderTimeTO_III_01")]
        public string PartyTime_OrderTimeTO_III_01 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_01")]
        public string PartyTime_ActTimeTO_III_01 { get; set; }
        [Display(Name = "OrderMemberCnt_01")]
        public string OrderMemberCnt_01 { get; set; }
        [Display(Name = "ActMemberCnt_01")]
        public string ActMemberCnt_01 { get; set; }
        [Display(Name = "LessMemberCnt_01")]
        public string LessMemberCnt_01 { get; set; }
        [Display(Name = "HelpMemberCnt_01")]
        public string HelpMemberCnt_01 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_01")]
        public string PartyMemberTime_BegTime_I_01 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_01")]
        public string PartyMemberTime_BegPeople_I_01 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_01")]
        public string PartyMemberTime_EndTime_I_01 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_01")]
        public string PartyMemberTime_EndPeople_I_01 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_01")]
        public string PartyMemberTime_BegTime_II_01 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_01")]
        public string PartyMemberTime_BegPeople_II_01 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_01")]
        public string PartyMemberTime_EndTime_II_01 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_01")]
        public string PartyMemberTime_EndPeople_II_01 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_01")]
        public string PartyMemberTime_BegTime_III_01 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_01")]
        public string PartyMemberTime_BegPeople_III_01 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_01")]
        public string PartyMemberTime_EndTime_III_01 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_01")]
        public string PartyMemberTime_EndPeople_III_01 { get; set; }
        [Display(Name = "Coment1_01")]
        public string Coment1_01 { get; set; }
        [Display(Name = "Coment2_01")]
        public string Coment2_01 { get; set; }
        [Display(Name = "Coment3_01")]
        public string Coment3_01 { get; set; }
        [Display(Name = "ComentSkill_01")]
        public string ComentSkill_01 { get; set; }
        [Display(Name = "Memo_01")]
        public string Memo_01 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_02")]
        public string PartyTime_OrderTimeTO_III_02 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_02")]
        public string PartyTime_ActTimeTO_III_02 { get; set; }
        [Display(Name = "OrderMemberCnt_02")]
        public string OrderMemberCnt_02 { get; set; }
        [Display(Name = "ActMemberCnt_02")]
        public string ActMemberCnt_02 { get; set; }
        [Display(Name = "LessMemberCnt_02")]
        public string LessMemberCnt_02 { get; set; }
        [Display(Name = "HelpMemberCnt_02")]
        public string HelpMemberCnt_02 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_02")]
        public string PartyMemberTime_BegTime_I_02 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_02")]
        public string PartyMemberTime_BegPeople_I_02 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_02")]
        public string PartyMemberTime_EndTime_I_02 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_02")]
        public string PartyMemberTime_EndPeople_I_02 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_02")]
        public string PartyMemberTime_BegTime_II_02 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_02")]
        public string PartyMemberTime_BegPeople_II_02 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_02")]
        public string PartyMemberTime_EndTime_II_02 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_02")]
        public string PartyMemberTime_EndPeople_II_02 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_02")]
        public string PartyMemberTime_BegTime_III_02 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_02")]
        public string PartyMemberTime_BegPeople_III_02 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_02")]
        public string PartyMemberTime_EndTime_III_02 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_02")]
        public string PartyMemberTime_EndPeople_III_02 { get; set; }
        [Display(Name = "Coment1_02")]
        public string Coment1_02 { get; set; }
        [Display(Name = "Coment2_02")]
        public string Coment2_02 { get; set; }
        [Display(Name = "Coment3_02")]
        public string Coment3_02 { get; set; }
        [Display(Name = "ComentSkill_02")]
        public string ComentSkill_02 { get; set; }
        [Display(Name = "Memo_02")]
        public string Memo_02 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_03")]
        public string PartyTime_OrderTimeTO_III_03 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_03")]
        public string PartyTime_ActTimeTO_III_03 { get; set; }
        [Display(Name = "OrderMemberCnt_03")]
        public string OrderMemberCnt_03 { get; set; }
        [Display(Name = "ActMemberCnt_03")]
        public string ActMemberCnt_03 { get; set; }
        [Display(Name = "LessMemberCnt_03")]
        public string LessMemberCnt_03 { get; set; }
        [Display(Name = "HelpMemberCnt_03")]
        public string HelpMemberCnt_03 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_03")]
        public string PartyMemberTime_BegTime_I_03 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_03")]
        public string PartyMemberTime_BegPeople_I_03 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_03")]
        public string PartyMemberTime_EndTime_I_03 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_03")]
        public string PartyMemberTime_EndPeople_I_03 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_03")]
        public string PartyMemberTime_BegTime_II_03 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_03")]
        public string PartyMemberTime_BegPeople_II_03 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_03")]
        public string PartyMemberTime_EndTime_II_03 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_03")]
        public string PartyMemberTime_EndPeople_II_03 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_03")]
        public string PartyMemberTime_BegTime_III_03 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_03")]
        public string PartyMemberTime_BegPeople_III_03 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_03")]
        public string PartyMemberTime_EndTime_III_03 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_03")]
        public string PartyMemberTime_EndPeople_III_03 { get; set; }
        [Display(Name = "Coment1_03")]
        public string Coment1_03 { get; set; }
        [Display(Name = "Coment2_03")]
        public string Coment2_03 { get; set; }
        [Display(Name = "Coment3_03")]
        public string Coment3_03 { get; set; }
        [Display(Name = "ComentSkill_03")]
        public string ComentSkill_03 { get; set; }
        [Display(Name = "Memo_03")]
        public string Memo_03 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_04")]
        public string PartyTime_OrderTimeTO_III_04 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_04")]
        public string PartyTime_ActTimeTO_III_04 { get; set; }
        [Display(Name = "OrderMemberCnt_04")]
        public string OrderMemberCnt_04 { get; set; }
        [Display(Name = "ActMemberCnt_04")]
        public string ActMemberCnt_04 { get; set; }
        [Display(Name = "LessMemberCnt_04")]
        public string LessMemberCnt_04 { get; set; }
        [Display(Name = "HelpMemberCnt_04")]
        public string HelpMemberCnt_04 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_04")]
        public string PartyMemberTime_BegTime_I_04 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_04")]
        public string PartyMemberTime_BegPeople_I_04 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_04")]
        public string PartyMemberTime_EndTime_I_04 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_04")]
        public string PartyMemberTime_EndPeople_I_04 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_04")]
        public string PartyMemberTime_BegTime_II_04 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_04")]
        public string PartyMemberTime_BegPeople_II_04 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_04")]
        public string PartyMemberTime_EndTime_II_04 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_04")]
        public string PartyMemberTime_EndPeople_II_04 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_04")]
        public string PartyMemberTime_BegTime_III_04 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_04")]
        public string PartyMemberTime_BegPeople_III_04 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_04")]
        public string PartyMemberTime_EndTime_III_04 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_04")]
        public string PartyMemberTime_EndPeople_III_04 { get; set; }
        [Display(Name = "Coment1_04")]
        public string Coment1_04 { get; set; }
        [Display(Name = "Coment2_04")]
        public string Coment2_04 { get; set; }
        [Display(Name = "Coment3_04")]
        public string Coment3_04 { get; set; }
        [Display(Name = "ComentSkill_04")]
        public string ComentSkill_04 { get; set; }
        [Display(Name = "Memo_04")]
        public string Memo_04 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_05")]
        public string PartyTime_OrderTimeTO_III_05 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_05")]
        public string PartyTime_ActTimeTO_III_05 { get; set; }
        [Display(Name = "OrderMemberCnt_05")]
        public string OrderMemberCnt_05 { get; set; }
        [Display(Name = "ActMemberCnt_05")]
        public string ActMemberCnt_05 { get; set; }
        [Display(Name = "LessMemberCnt_05")]
        public string LessMemberCnt_05 { get; set; }
        [Display(Name = "HelpMemberCnt_05")]
        public string HelpMemberCnt_05 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_05")]
        public string PartyMemberTime_BegTime_I_05 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_05")]
        public string PartyMemberTime_BegPeople_I_05 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_05")]
        public string PartyMemberTime_EndTime_I_05 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_05")]
        public string PartyMemberTime_EndPeople_I_05 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_05")]
        public string PartyMemberTime_BegTime_II_05 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_05")]
        public string PartyMemberTime_BegPeople_II_05 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_05")]
        public string PartyMemberTime_EndTime_II_05 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_05")]
        public string PartyMemberTime_EndPeople_II_05 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_05")]
        public string PartyMemberTime_BegTime_III_05 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_05")]
        public string PartyMemberTime_BegPeople_III_05 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_05")]
        public string PartyMemberTime_EndTime_III_05 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_05")]
        public string PartyMemberTime_EndPeople_III_05 { get; set; }
        [Display(Name = "Coment1_05")]
        public string Coment1_05 { get; set; }
        [Display(Name = "Coment2_05")]
        public string Coment2_05 { get; set; }
        [Display(Name = "Coment3_05")]
        public string Coment3_05 { get; set; }
        [Display(Name = "ComentSkill_05")]
        public string ComentSkill_05 { get; set; }
        [Display(Name = "Memo_05")]
        public string Memo_05 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_06")]
        public string PartyTime_OrderTimeTO_III_06 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_06")]
        public string PartyTime_ActTimeTO_III_06 { get; set; }
        [Display(Name = "OrderMemberCnt_06")]
        public string OrderMemberCnt_06 { get; set; }
        [Display(Name = "ActMemberCnt_06")]
        public string ActMemberCnt_06 { get; set; }
        [Display(Name = "LessMemberCnt_06")]
        public string LessMemberCnt_06 { get; set; }
        [Display(Name = "HelpMemberCnt_06")]
        public string HelpMemberCnt_06 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_06")]
        public string PartyMemberTime_BegTime_I_06 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_06")]
        public string PartyMemberTime_BegPeople_I_06 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_06")]
        public string PartyMemberTime_EndTime_I_06 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_06")]
        public string PartyMemberTime_EndPeople_I_06 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_06")]
        public string PartyMemberTime_BegTime_II_06 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_06")]
        public string PartyMemberTime_BegPeople_II_06 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_06")]
        public string PartyMemberTime_EndTime_II_06 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_06")]
        public string PartyMemberTime_EndPeople_II_06 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_06")]
        public string PartyMemberTime_BegTime_III_06 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_06")]
        public string PartyMemberTime_BegPeople_III_06 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_06")]
        public string PartyMemberTime_EndTime_III_06 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_06")]
        public string PartyMemberTime_EndPeople_III_06 { get; set; }
        [Display(Name = "Coment1_06")]
        public string Coment1_06 { get; set; }
        [Display(Name = "Coment2_06")]
        public string Coment2_06 { get; set; }
        [Display(Name = "Coment3_06")]
        public string Coment3_06 { get; set; }
        [Display(Name = "ComentSkill_06")]
        public string ComentSkill_06 { get; set; }
        [Display(Name = "Memo_06")]
        public string Memo_06 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_07")]
        public string PartyTime_OrderTimeTO_III_07 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_07")]
        public string PartyTime_ActTimeTO_III_07 { get; set; }
        [Display(Name = "OrderMemberCnt_07")]
        public string OrderMemberCnt_07 { get; set; }
        [Display(Name = "ActMemberCnt_07")]
        public string ActMemberCnt_07 { get; set; }
        [Display(Name = "LessMemberCnt_07")]
        public string LessMemberCnt_07 { get; set; }
        [Display(Name = "HelpMemberCnt_07")]
        public string HelpMemberCnt_07 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_07")]
        public string PartyMemberTime_BegTime_I_07 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_07")]
        public string PartyMemberTime_BegPeople_I_07 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_07")]
        public string PartyMemberTime_EndTime_I_07 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_07")]
        public string PartyMemberTime_EndPeople_I_07 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_07")]
        public string PartyMemberTime_BegTime_II_07 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_07")]
        public string PartyMemberTime_BegPeople_II_07 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_07")]
        public string PartyMemberTime_EndTime_II_07 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_07")]
        public string PartyMemberTime_EndPeople_II_07 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_07")]
        public string PartyMemberTime_BegTime_III_07 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_07")]
        public string PartyMemberTime_BegPeople_III_07 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_07")]
        public string PartyMemberTime_EndTime_III_07 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_07")]
        public string PartyMemberTime_EndPeople_III_07 { get; set; }
        [Display(Name = "Coment1_07")]
        public string Coment1_07 { get; set; }
        [Display(Name = "Coment2_07")]
        public string Coment2_07 { get; set; }
        [Display(Name = "Coment3_07")]
        public string Coment3_07 { get; set; }
        [Display(Name = "ComentSkill_07")]
        public string ComentSkill_07 { get; set; }
        [Display(Name = "Memo_07")]
        public string Memo_07 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_08")]
        public string PartyTime_OrderTimeTO_III_08 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_08")]
        public string PartyTime_ActTimeTO_III_08 { get; set; }
        [Display(Name = "OrderMemberCnt_08")]
        public string OrderMemberCnt_08 { get; set; }
        [Display(Name = "ActMemberCnt_08")]
        public string ActMemberCnt_08 { get; set; }
        [Display(Name = "LessMemberCnt_08")]
        public string LessMemberCnt_08 { get; set; }
        [Display(Name = "HelpMemberCnt_08")]
        public string HelpMemberCnt_08 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_08")]
        public string PartyMemberTime_BegTime_I_08 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_08")]
        public string PartyMemberTime_BegPeople_I_08 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_08")]
        public string PartyMemberTime_EndTime_I_08 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_08")]
        public string PartyMemberTime_EndPeople_I_08 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_08")]
        public string PartyMemberTime_BegTime_II_08 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_08")]
        public string PartyMemberTime_BegPeople_II_08 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_08")]
        public string PartyMemberTime_EndTime_II_08 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_08")]
        public string PartyMemberTime_EndPeople_II_08 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_08")]
        public string PartyMemberTime_BegTime_III_08 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_08")]
        public string PartyMemberTime_BegPeople_III_08 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_08")]
        public string PartyMemberTime_EndTime_III_08 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_08")]
        public string PartyMemberTime_EndPeople_III_08 { get; set; }
        [Display(Name = "Coment1_08")]
        public string Coment1_08 { get; set; }
        [Display(Name = "Coment2_08")]
        public string Coment2_08 { get; set; }
        [Display(Name = "Coment3_08")]
        public string Coment3_08 { get; set; }
        [Display(Name = "ComentSkill_08")]
        public string ComentSkill_08 { get; set; }
        [Display(Name = "Memo_08")]
        public string Memo_08 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_09")]
        public string PartyTime_OrderTimeTO_III_09 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_09")]
        public string PartyTime_ActTimeTO_III_09 { get; set; }
        [Display(Name = "OrderMemberCnt_09")]
        public string OrderMemberCnt_09 { get; set; }
        [Display(Name = "ActMemberCnt_09")]
        public string ActMemberCnt_09 { get; set; }
        [Display(Name = "LessMemberCnt_09")]
        public string LessMemberCnt_09 { get; set; }
        [Display(Name = "HelpMemberCnt_09")]
        public string HelpMemberCnt_09 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_09")]
        public string PartyMemberTime_BegTime_I_09 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_09")]
        public string PartyMemberTime_BegPeople_I_09 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_09")]
        public string PartyMemberTime_EndTime_I_09 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_09")]
        public string PartyMemberTime_EndPeople_I_09 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_09")]
        public string PartyMemberTime_BegTime_II_09 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_09")]
        public string PartyMemberTime_BegPeople_II_09 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_09")]
        public string PartyMemberTime_EndTime_II_09 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_09")]
        public string PartyMemberTime_EndPeople_II_09 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_09")]
        public string PartyMemberTime_BegTime_III_09 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_09")]
        public string PartyMemberTime_BegPeople_III_09 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_09")]
        public string PartyMemberTime_EndTime_III_09 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_09")]
        public string PartyMemberTime_EndPeople_III_09 { get; set; }
        [Display(Name = "Coment1_09")]
        public string Coment1_09 { get; set; }
        [Display(Name = "Coment2_09")]
        public string Coment2_09 { get; set; }
        [Display(Name = "Coment3_09")]
        public string Coment3_09 { get; set; }
        [Display(Name = "ComentSkill_09")]
        public string ComentSkill_09 { get; set; }
        [Display(Name = "Memo_09")]
        public string Memo_09 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_10")]
        public string PartyTime_OrderTimeTO_III_10 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_10")]
        public string PartyTime_ActTimeTO_III_10 { get; set; }
        [Display(Name = "OrderMemberCnt_10")]
        public string OrderMemberCnt_10 { get; set; }
        [Display(Name = "ActMemberCnt_10")]
        public string ActMemberCnt_10 { get; set; }
        [Display(Name = "LessMemberCnt_10")]
        public string LessMemberCnt_10 { get; set; }
        [Display(Name = "HelpMemberCnt_10")]
        public string HelpMemberCnt_10 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_10")]
        public string PartyMemberTime_BegTime_I_10 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_10")]
        public string PartyMemberTime_BegPeople_I_10 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_10")]
        public string PartyMemberTime_EndTime_I_10 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_10")]
        public string PartyMemberTime_EndPeople_I_10 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_10")]
        public string PartyMemberTime_BegTime_II_10 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_10")]
        public string PartyMemberTime_BegPeople_II_10 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_10")]
        public string PartyMemberTime_EndTime_II_10 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_10")]
        public string PartyMemberTime_EndPeople_II_10 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_10")]
        public string PartyMemberTime_BegTime_III_10 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_10")]
        public string PartyMemberTime_BegPeople_III_10 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_10")]
        public string PartyMemberTime_EndTime_III_10 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_10")]
        public string PartyMemberTime_EndPeople_III_10 { get; set; }
        [Display(Name = "Coment1_10")]
        public string Coment1_10 { get; set; }
        [Display(Name = "Coment2_10")]
        public string Coment2_10 { get; set; }
        [Display(Name = "Coment3_10")]
        public string Coment3_10 { get; set; }
        [Display(Name = "ComentSkill_10")]
        public string ComentSkill_10 { get; set; }
        [Display(Name = "Memo_10")]
        public string Memo_10 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_11")]
        public string PartyTime_OrderTimeTO_III_11 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_11")]
        public string PartyTime_ActTimeTO_III_11 { get; set; }
        [Display(Name = "OrderMemberCnt_11")]
        public string OrderMemberCnt_11 { get; set; }
        [Display(Name = "ActMemberCnt_11")]
        public string ActMemberCnt_11 { get; set; }
        [Display(Name = "LessMemberCnt_11")]
        public string LessMemberCnt_11 { get; set; }
        [Display(Name = "HelpMemberCnt_11")]
        public string HelpMemberCnt_11 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_11")]
        public string PartyMemberTime_BegTime_I_11 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_11")]
        public string PartyMemberTime_BegPeople_I_11 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_11")]
        public string PartyMemberTime_EndTime_I_11 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_11")]
        public string PartyMemberTime_EndPeople_I_11 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_11")]
        public string PartyMemberTime_BegTime_II_11 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_11")]
        public string PartyMemberTime_BegPeople_II_11 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_11")]
        public string PartyMemberTime_EndTime_II_11 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_11")]
        public string PartyMemberTime_EndPeople_II_11 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_11")]
        public string PartyMemberTime_BegTime_III_11 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_11")]
        public string PartyMemberTime_BegPeople_III_11 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_11")]
        public string PartyMemberTime_EndTime_III_11 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_11")]
        public string PartyMemberTime_EndPeople_III_11 { get; set; }
        [Display(Name = "Coment1_11")]
        public string Coment1_11 { get; set; }
        [Display(Name = "Coment2_11")]
        public string Coment2_11 { get; set; }
        [Display(Name = "Coment3_11")]
        public string Coment3_11 { get; set; }
        [Display(Name = "ComentSkill_11")]
        public string ComentSkill_11 { get; set; }
        [Display(Name = "Memo_11")]
        public string Memo_11 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_12")]
        public string PartyTime_OrderTimeTO_III_12 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_12")]
        public string PartyTime_ActTimeTO_III_12 { get; set; }
        [Display(Name = "OrderMemberCnt_12")]
        public string OrderMemberCnt_12 { get; set; }
        [Display(Name = "ActMemberCnt_12")]
        public string ActMemberCnt_12 { get; set; }
        [Display(Name = "LessMemberCnt_12")]
        public string LessMemberCnt_12 { get; set; }
        [Display(Name = "HelpMemberCnt_12")]
        public string HelpMemberCnt_12 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_12")]
        public string PartyMemberTime_BegTime_I_12 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_12")]
        public string PartyMemberTime_BegPeople_I_12 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_12")]
        public string PartyMemberTime_EndTime_I_12 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_12")]
        public string PartyMemberTime_EndPeople_I_12 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_12")]
        public string PartyMemberTime_BegTime_II_12 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_12")]
        public string PartyMemberTime_BegPeople_II_12 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_12")]
        public string PartyMemberTime_EndTime_II_12 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_12")]
        public string PartyMemberTime_EndPeople_II_12 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_12")]
        public string PartyMemberTime_BegTime_III_12 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_12")]
        public string PartyMemberTime_BegPeople_III_12 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_12")]
        public string PartyMemberTime_EndTime_III_12 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_12")]
        public string PartyMemberTime_EndPeople_III_12 { get; set; }
        [Display(Name = "Coment1_12")]
        public string Coment1_12 { get; set; }
        [Display(Name = "Coment2_12")]
        public string Coment2_12 { get; set; }
        [Display(Name = "Coment3_12")]
        public string Coment3_12 { get; set; }
        [Display(Name = "ComentSkill_12")]
        public string ComentSkill_12 { get; set; }
        [Display(Name = "Memo_12")]
        public string Memo_12 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_13")]
        public string PartyTime_OrderTimeTO_III_13 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_13")]
        public string PartyTime_ActTimeTO_III_13 { get; set; }
        [Display(Name = "OrderMemberCnt_13")]
        public string OrderMemberCnt_13 { get; set; }
        [Display(Name = "ActMemberCnt_13")]
        public string ActMemberCnt_13 { get; set; }
        [Display(Name = "LessMemberCnt_13")]
        public string LessMemberCnt_13 { get; set; }
        [Display(Name = "HelpMemberCnt_13")]
        public string HelpMemberCnt_13 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_13")]
        public string PartyMemberTime_BegTime_I_13 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_13")]
        public string PartyMemberTime_BegPeople_I_13 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_13")]
        public string PartyMemberTime_EndTime_I_13 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_13")]
        public string PartyMemberTime_EndPeople_I_13 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_13")]
        public string PartyMemberTime_BegTime_II_13 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_13")]
        public string PartyMemberTime_BegPeople_II_13 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_13")]
        public string PartyMemberTime_EndTime_II_13 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_13")]
        public string PartyMemberTime_EndPeople_II_13 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_13")]
        public string PartyMemberTime_BegTime_III_13 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_13")]
        public string PartyMemberTime_BegPeople_III_13 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_13")]
        public string PartyMemberTime_EndTime_III_13 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_13")]
        public string PartyMemberTime_EndPeople_III_13 { get; set; }
        [Display(Name = "Coment1_13")]
        public string Coment1_13 { get; set; }
        [Display(Name = "Coment2_13")]
        public string Coment2_13 { get; set; }
        [Display(Name = "Coment3_13")]
        public string Coment3_13 { get; set; }
        [Display(Name = "ComentSkill_13")]
        public string ComentSkill_13 { get; set; }
        [Display(Name = "Memo_13")]
        public string Memo_13 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_14")]
        public string PartyTime_OrderTimeTO_III_14 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_14")]
        public string PartyTime_ActTimeTO_III_14 { get; set; }
        [Display(Name = "OrderMemberCnt_14")]
        public string OrderMemberCnt_14 { get; set; }
        [Display(Name = "ActMemberCnt_14")]
        public string ActMemberCnt_14 { get; set; }
        [Display(Name = "LessMemberCnt_14")]
        public string LessMemberCnt_14 { get; set; }
        [Display(Name = "HelpMemberCnt_14")]
        public string HelpMemberCnt_14 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_14")]
        public string PartyMemberTime_BegTime_I_14 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_14")]
        public string PartyMemberTime_BegPeople_I_14 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_14")]
        public string PartyMemberTime_EndTime_I_14 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_14")]
        public string PartyMemberTime_EndPeople_I_14 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_14")]
        public string PartyMemberTime_BegTime_II_14 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_14")]
        public string PartyMemberTime_BegPeople_II_14 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_14")]
        public string PartyMemberTime_EndTime_II_14 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_14")]
        public string PartyMemberTime_EndPeople_II_14 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_14")]
        public string PartyMemberTime_BegTime_III_14 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_14")]
        public string PartyMemberTime_BegPeople_III_14 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_14")]
        public string PartyMemberTime_EndTime_III_14 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_14")]
        public string PartyMemberTime_EndPeople_III_14 { get; set; }
        [Display(Name = "Coment1_14")]
        public string Coment1_14 { get; set; }
        [Display(Name = "Coment2_14")]
        public string Coment2_14 { get; set; }
        [Display(Name = "Coment3_14")]
        public string Coment3_14 { get; set; }
        [Display(Name = "ComentSkill_14")]
        public string ComentSkill_14 { get; set; }
        [Display(Name = "Memo_14")]
        public string Memo_14 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_15")]
        public string PartyTime_OrderTimeTO_III_15 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_15")]
        public string PartyTime_ActTimeTO_III_15 { get; set; }
        [Display(Name = "OrderMemberCnt_15")]
        public string OrderMemberCnt_15 { get; set; }
        [Display(Name = "ActMemberCnt_15")]
        public string ActMemberCnt_15 { get; set; }
        [Display(Name = "LessMemberCnt_15")]
        public string LessMemberCnt_15 { get; set; }
        [Display(Name = "HelpMemberCnt_15")]
        public string HelpMemberCnt_15 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_15")]
        public string PartyMemberTime_BegTime_I_15 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_15")]
        public string PartyMemberTime_BegPeople_I_15 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_15")]
        public string PartyMemberTime_EndTime_I_15 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_15")]
        public string PartyMemberTime_EndPeople_I_15 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_15")]
        public string PartyMemberTime_BegTime_II_15 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_15")]
        public string PartyMemberTime_BegPeople_II_15 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_15")]
        public string PartyMemberTime_EndTime_II_15 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_15")]
        public string PartyMemberTime_EndPeople_II_15 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_15")]
        public string PartyMemberTime_BegTime_III_15 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_15")]
        public string PartyMemberTime_BegPeople_III_15 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_15")]
        public string PartyMemberTime_EndTime_III_15 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_15")]
        public string PartyMemberTime_EndPeople_III_15 { get; set; }
        [Display(Name = "Coment1_15")]
        public string Coment1_15 { get; set; }
        [Display(Name = "Coment2_15")]
        public string Coment2_15 { get; set; }
        [Display(Name = "Coment3_15")]
        public string Coment3_15 { get; set; }
        [Display(Name = "ComentSkill_15")]
        public string ComentSkill_15 { get; set; }
        [Display(Name = "Memo_15")]
        public string Memo_15 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_16")]
        public string PartyTime_OrderTimeTO_III_16 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_16")]
        public string PartyTime_ActTimeTO_III_16 { get; set; }
        [Display(Name = "OrderMemberCnt_16")]
        public string OrderMemberCnt_16 { get; set; }
        [Display(Name = "ActMemberCnt_16")]
        public string ActMemberCnt_16 { get; set; }
        [Display(Name = "LessMemberCnt_16")]
        public string LessMemberCnt_16 { get; set; }
        [Display(Name = "HelpMemberCnt_16")]
        public string HelpMemberCnt_16 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_16")]
        public string PartyMemberTime_BegTime_I_16 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_16")]
        public string PartyMemberTime_BegPeople_I_16 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_16")]
        public string PartyMemberTime_EndTime_I_16 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_16")]
        public string PartyMemberTime_EndPeople_I_16 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_16")]
        public string PartyMemberTime_BegTime_II_16 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_16")]
        public string PartyMemberTime_BegPeople_II_16 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_16")]
        public string PartyMemberTime_EndTime_II_16 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_16")]
        public string PartyMemberTime_EndPeople_II_16 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_16")]
        public string PartyMemberTime_BegTime_III_16 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_16")]
        public string PartyMemberTime_BegPeople_III_16 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_16")]
        public string PartyMemberTime_EndTime_III_16 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_16")]
        public string PartyMemberTime_EndPeople_III_16 { get; set; }
        [Display(Name = "Coment1_16")]
        public string Coment1_16 { get; set; }
        [Display(Name = "Coment2_16")]
        public string Coment2_16 { get; set; }
        [Display(Name = "Coment3_16")]
        public string Coment3_16 { get; set; }
        [Display(Name = "ComentSkill_16")]
        public string ComentSkill_16 { get; set; }
        [Display(Name = "Memo_16")]
        public string Memo_16 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_17")]
        public string PartyTime_OrderTimeTO_III_17 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_17")]
        public string PartyTime_ActTimeTO_III_17 { get; set; }
        [Display(Name = "OrderMemberCnt_17")]
        public string OrderMemberCnt_17 { get; set; }
        [Display(Name = "ActMemberCnt_17")]
        public string ActMemberCnt_17 { get; set; }
        [Display(Name = "LessMemberCnt_17")]
        public string LessMemberCnt_17 { get; set; }
        [Display(Name = "HelpMemberCnt_17")]
        public string HelpMemberCnt_17 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_17")]
        public string PartyMemberTime_BegTime_I_17 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_17")]
        public string PartyMemberTime_BegPeople_I_17 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_17")]
        public string PartyMemberTime_EndTime_I_17 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_17")]
        public string PartyMemberTime_EndPeople_I_17 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_17")]
        public string PartyMemberTime_BegTime_II_17 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_17")]
        public string PartyMemberTime_BegPeople_II_17 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_17")]
        public string PartyMemberTime_EndTime_II_17 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_17")]
        public string PartyMemberTime_EndPeople_II_17 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_17")]
        public string PartyMemberTime_BegTime_III_17 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_17")]
        public string PartyMemberTime_BegPeople_III_17 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_17")]
        public string PartyMemberTime_EndTime_III_17 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_17")]
        public string PartyMemberTime_EndPeople_III_17 { get; set; }
        [Display(Name = "Coment1_17")]
        public string Coment1_17 { get; set; }
        [Display(Name = "Coment2_17")]
        public string Coment2_17 { get; set; }
        [Display(Name = "Coment3_17")]
        public string Coment3_17 { get; set; }
        [Display(Name = "ComentSkill_17")]
        public string ComentSkill_17 { get; set; }
        [Display(Name = "Memo_17")]
        public string Memo_17 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_18")]
        public string PartyTime_OrderTimeTO_III_18 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_18")]
        public string PartyTime_ActTimeTO_III_18 { get; set; }
        [Display(Name = "OrderMemberCnt_18")]
        public string OrderMemberCnt_18 { get; set; }
        [Display(Name = "ActMemberCnt_18")]
        public string ActMemberCnt_18 { get; set; }
        [Display(Name = "LessMemberCnt_18")]
        public string LessMemberCnt_18 { get; set; }
        [Display(Name = "HelpMemberCnt_18")]
        public string HelpMemberCnt_18 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_18")]
        public string PartyMemberTime_BegTime_I_18 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_18")]
        public string PartyMemberTime_BegPeople_I_18 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_18")]
        public string PartyMemberTime_EndTime_I_18 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_18")]
        public string PartyMemberTime_EndPeople_I_18 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_18")]
        public string PartyMemberTime_BegTime_II_18 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_18")]
        public string PartyMemberTime_BegPeople_II_18 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_18")]
        public string PartyMemberTime_EndTime_II_18 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_18")]
        public string PartyMemberTime_EndPeople_II_18 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_18")]
        public string PartyMemberTime_BegTime_III_18 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_18")]
        public string PartyMemberTime_BegPeople_III_18 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_18")]
        public string PartyMemberTime_EndTime_III_18 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_18")]
        public string PartyMemberTime_EndPeople_III_18 { get; set; }
        [Display(Name = "Coment1_18")]
        public string Coment1_18 { get; set; }
        [Display(Name = "Coment2_18")]
        public string Coment2_18 { get; set; }
        [Display(Name = "Coment3_18")]
        public string Coment3_18 { get; set; }
        [Display(Name = "ComentSkill_18")]
        public string ComentSkill_18 { get; set; }
        [Display(Name = "Memo_18")]
        public string Memo_18 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_19")]
        public string PartyTime_OrderTimeTO_III_19 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_19")]
        public string PartyTime_ActTimeTO_III_19 { get; set; }
        [Display(Name = "OrderMemberCnt_19")]
        public string OrderMemberCnt_19 { get; set; }
        [Display(Name = "ActMemberCnt_19")]
        public string ActMemberCnt_19 { get; set; }
        [Display(Name = "LessMemberCnt_19")]
        public string LessMemberCnt_19 { get; set; }
        [Display(Name = "HelpMemberCnt_19")]
        public string HelpMemberCnt_19 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_19")]
        public string PartyMemberTime_BegTime_I_19 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_19")]
        public string PartyMemberTime_BegPeople_I_19 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_19")]
        public string PartyMemberTime_EndTime_I_19 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_19")]
        public string PartyMemberTime_EndPeople_I_19 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_19")]
        public string PartyMemberTime_BegTime_II_19 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_19")]
        public string PartyMemberTime_BegPeople_II_19 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_19")]
        public string PartyMemberTime_EndTime_II_19 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_19")]
        public string PartyMemberTime_EndPeople_II_19 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_19")]
        public string PartyMemberTime_BegTime_III_19 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_19")]
        public string PartyMemberTime_BegPeople_III_19 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_19")]
        public string PartyMemberTime_EndTime_III_19 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_19")]
        public string PartyMemberTime_EndPeople_III_19 { get; set; }
        [Display(Name = "Coment1_19")]
        public string Coment1_19 { get; set; }
        [Display(Name = "Coment2_19")]
        public string Coment2_19 { get; set; }
        [Display(Name = "Coment3_19")]
        public string Coment3_19 { get; set; }
        [Display(Name = "ComentSkill_19")]
        public string ComentSkill_19 { get; set; }
        [Display(Name = "Memo_19")]
        public string Memo_19 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_20")]
        public string PartyTime_OrderTimeTO_III_20 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_20")]
        public string PartyTime_ActTimeTO_III_20 { get; set; }
        [Display(Name = "OrderMemberCnt_20")]
        public string OrderMemberCnt_20 { get; set; }
        [Display(Name = "ActMemberCnt_20")]
        public string ActMemberCnt_20 { get; set; }
        [Display(Name = "LessMemberCnt_20")]
        public string LessMemberCnt_20 { get; set; }
        [Display(Name = "HelpMemberCnt_20")]
        public string HelpMemberCnt_20 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_20")]
        public string PartyMemberTime_BegTime_I_20 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_20")]
        public string PartyMemberTime_BegPeople_I_20 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_20")]
        public string PartyMemberTime_EndTime_I_20 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_20")]
        public string PartyMemberTime_EndPeople_I_20 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_20")]
        public string PartyMemberTime_BegTime_II_20 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_20")]
        public string PartyMemberTime_BegPeople_II_20 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_20")]
        public string PartyMemberTime_EndTime_II_20 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_20")]
        public string PartyMemberTime_EndPeople_II_20 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_20")]
        public string PartyMemberTime_BegTime_III_20 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_20")]
        public string PartyMemberTime_BegPeople_III_20 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_20")]
        public string PartyMemberTime_EndTime_III_20 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_20")]
        public string PartyMemberTime_EndPeople_III_20 { get; set; }
        [Display(Name = "Coment1_20")]
        public string Coment1_20 { get; set; }
        [Display(Name = "Coment2_20")]
        public string Coment2_20 { get; set; }
        [Display(Name = "Coment3_20")]
        public string Coment3_20 { get; set; }
        [Display(Name = "ComentSkill_20")]
        public string ComentSkill_20 { get; set; }
        [Display(Name = "Memo_20")]
        public string Memo_20 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_21")]
        public string PartyTime_OrderTimeTO_III_21 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_21")]
        public string PartyTime_ActTimeTO_III_21 { get; set; }
        [Display(Name = "OrderMemberCnt_21")]
        public string OrderMemberCnt_21 { get; set; }
        [Display(Name = "ActMemberCnt_21")]
        public string ActMemberCnt_21 { get; set; }
        [Display(Name = "LessMemberCnt_21")]
        public string LessMemberCnt_21 { get; set; }
        [Display(Name = "HelpMemberCnt_21")]
        public string HelpMemberCnt_21 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_21")]
        public string PartyMemberTime_BegTime_I_21 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_21")]
        public string PartyMemberTime_BegPeople_I_21 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_21")]
        public string PartyMemberTime_EndTime_I_21 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_21")]
        public string PartyMemberTime_EndPeople_I_21 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_21")]
        public string PartyMemberTime_BegTime_II_21 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_21")]
        public string PartyMemberTime_BegPeople_II_21 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_21")]
        public string PartyMemberTime_EndTime_II_21 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_21")]
        public string PartyMemberTime_EndPeople_II_21 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_21")]
        public string PartyMemberTime_BegTime_III_21 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_21")]
        public string PartyMemberTime_BegPeople_III_21 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_21")]
        public string PartyMemberTime_EndTime_III_21 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_21")]
        public string PartyMemberTime_EndPeople_III_21 { get; set; }
        [Display(Name = "Coment1_21")]
        public string Coment1_21 { get; set; }
        [Display(Name = "Coment2_21")]
        public string Coment2_21 { get; set; }
        [Display(Name = "Coment3_21")]
        public string Coment3_21 { get; set; }
        [Display(Name = "ComentSkill_21")]
        public string ComentSkill_21 { get; set; }
        [Display(Name = "Memo_21")]
        public string Memo_21 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_22")]
        public string PartyTime_OrderTimeTO_III_22 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_22")]
        public string PartyTime_ActTimeTO_III_22 { get; set; }
        [Display(Name = "OrderMemberCnt_22")]
        public string OrderMemberCnt_22 { get; set; }
        [Display(Name = "ActMemberCnt_22")]
        public string ActMemberCnt_22 { get; set; }
        [Display(Name = "LessMemberCnt_22")]
        public string LessMemberCnt_22 { get; set; }
        [Display(Name = "HelpMemberCnt_22")]
        public string HelpMemberCnt_22 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_22")]
        public string PartyMemberTime_BegTime_I_22 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_22")]
        public string PartyMemberTime_BegPeople_I_22 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_22")]
        public string PartyMemberTime_EndTime_I_22 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_22")]
        public string PartyMemberTime_EndPeople_I_22 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_22")]
        public string PartyMemberTime_BegTime_II_22 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_22")]
        public string PartyMemberTime_BegPeople_II_22 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_22")]
        public string PartyMemberTime_EndTime_II_22 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_22")]
        public string PartyMemberTime_EndPeople_II_22 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_22")]
        public string PartyMemberTime_BegTime_III_22 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_22")]
        public string PartyMemberTime_BegPeople_III_22 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_22")]
        public string PartyMemberTime_EndTime_III_22 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_22")]
        public string PartyMemberTime_EndPeople_III_22 { get; set; }
        [Display(Name = "Coment1_22")]
        public string Coment1_22 { get; set; }
        [Display(Name = "Coment2_22")]
        public string Coment2_22 { get; set; }
        [Display(Name = "Coment3_22")]
        public string Coment3_22 { get; set; }
        [Display(Name = "ComentSkill_22")]
        public string ComentSkill_22 { get; set; }
        [Display(Name = "Memo_22")]
        public string Memo_22 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_23")]
        public string PartyTime_OrderTimeTO_III_23 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_23")]
        public string PartyTime_ActTimeTO_III_23 { get; set; }
        [Display(Name = "OrderMemberCnt_23")]
        public string OrderMemberCnt_23 { get; set; }
        [Display(Name = "ActMemberCnt_23")]
        public string ActMemberCnt_23 { get; set; }
        [Display(Name = "LessMemberCnt_23")]
        public string LessMemberCnt_23 { get; set; }
        [Display(Name = "HelpMemberCnt_23")]
        public string HelpMemberCnt_23 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_23")]
        public string PartyMemberTime_BegTime_I_23 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_23")]
        public string PartyMemberTime_BegPeople_I_23 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_23")]
        public string PartyMemberTime_EndTime_I_23 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_23")]
        public string PartyMemberTime_EndPeople_I_23 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_23")]
        public string PartyMemberTime_BegTime_II_23 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_23")]
        public string PartyMemberTime_BegPeople_II_23 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_23")]
        public string PartyMemberTime_EndTime_II_23 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_23")]
        public string PartyMemberTime_EndPeople_II_23 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_23")]
        public string PartyMemberTime_BegTime_III_23 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_23")]
        public string PartyMemberTime_BegPeople_III_23 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_23")]
        public string PartyMemberTime_EndTime_III_23 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_23")]
        public string PartyMemberTime_EndPeople_III_23 { get; set; }
        [Display(Name = "Coment1_23")]
        public string Coment1_23 { get; set; }
        [Display(Name = "Coment2_23")]
        public string Coment2_23 { get; set; }
        [Display(Name = "Coment3_23")]
        public string Coment3_23 { get; set; }
        [Display(Name = "ComentSkill_23")]
        public string ComentSkill_23 { get; set; }
        [Display(Name = "Memo_23")]
        public string Memo_23 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_24")]
        public string PartyTime_OrderTimeTO_III_24 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_24")]
        public string PartyTime_ActTimeTO_III_24 { get; set; }
        [Display(Name = "OrderMemberCnt_24")]
        public string OrderMemberCnt_24 { get; set; }
        [Display(Name = "ActMemberCnt_24")]
        public string ActMemberCnt_24 { get; set; }
        [Display(Name = "LessMemberCnt_24")]
        public string LessMemberCnt_24 { get; set; }
        [Display(Name = "HelpMemberCnt_24")]
        public string HelpMemberCnt_24 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_24")]
        public string PartyMemberTime_BegTime_I_24 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_24")]
        public string PartyMemberTime_BegPeople_I_24 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_24")]
        public string PartyMemberTime_EndTime_I_24 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_24")]
        public string PartyMemberTime_EndPeople_I_24 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_24")]
        public string PartyMemberTime_BegTime_II_24 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_24")]
        public string PartyMemberTime_BegPeople_II_24 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_24")]
        public string PartyMemberTime_EndTime_II_24 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_24")]
        public string PartyMemberTime_EndPeople_II_24 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_24")]
        public string PartyMemberTime_BegTime_III_24 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_24")]
        public string PartyMemberTime_BegPeople_III_24 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_24")]
        public string PartyMemberTime_EndTime_III_24 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_24")]
        public string PartyMemberTime_EndPeople_III_24 { get; set; }
        [Display(Name = "Coment1_24")]
        public string Coment1_24 { get; set; }
        [Display(Name = "Coment2_24")]
        public string Coment2_24 { get; set; }
        [Display(Name = "Coment3_24")]
        public string Coment3_24 { get; set; }
        [Display(Name = "ComentSkill_24")]
        public string ComentSkill_24 { get; set; }
        [Display(Name = "Memo_24")]
        public string Memo_24 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_25")]
        public string PartyTime_OrderTimeTO_III_25 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_25")]
        public string PartyTime_ActTimeTO_III_25 { get; set; }
        [Display(Name = "OrderMemberCnt_25")]
        public string OrderMemberCnt_25 { get; set; }
        [Display(Name = "ActMemberCnt_25")]
        public string ActMemberCnt_25 { get; set; }
        [Display(Name = "LessMemberCnt_25")]
        public string LessMemberCnt_25 { get; set; }
        [Display(Name = "HelpMemberCnt_25")]
        public string HelpMemberCnt_25 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_25")]
        public string PartyMemberTime_BegTime_I_25 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_25")]
        public string PartyMemberTime_BegPeople_I_25 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_25")]
        public string PartyMemberTime_EndTime_I_25 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_25")]
        public string PartyMemberTime_EndPeople_I_25 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_25")]
        public string PartyMemberTime_BegTime_II_25 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_25")]
        public string PartyMemberTime_BegPeople_II_25 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_25")]
        public string PartyMemberTime_EndTime_II_25 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_25")]
        public string PartyMemberTime_EndPeople_II_25 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_25")]
        public string PartyMemberTime_BegTime_III_25 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_25")]
        public string PartyMemberTime_BegPeople_III_25 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_25")]
        public string PartyMemberTime_EndTime_III_25 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_25")]
        public string PartyMemberTime_EndPeople_III_25 { get; set; }
        [Display(Name = "Coment1_25")]
        public string Coment1_25 { get; set; }
        [Display(Name = "Coment2_25")]
        public string Coment2_25 { get; set; }
        [Display(Name = "Coment3_25")]
        public string Coment3_25 { get; set; }
        [Display(Name = "ComentSkill_25")]
        public string ComentSkill_25 { get; set; }
        [Display(Name = "Memo_25")]
        public string Memo_25 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_26")]
        public string PartyTime_OrderTimeTO_III_26 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_26")]
        public string PartyTime_ActTimeTO_III_26 { get; set; }
        [Display(Name = "OrderMemberCnt_26")]
        public string OrderMemberCnt_26 { get; set; }
        [Display(Name = "ActMemberCnt_26")]
        public string ActMemberCnt_26 { get; set; }
        [Display(Name = "LessMemberCnt_26")]
        public string LessMemberCnt_26 { get; set; }
        [Display(Name = "HelpMemberCnt_26")]
        public string HelpMemberCnt_26 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_26")]
        public string PartyMemberTime_BegTime_I_26 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_26")]
        public string PartyMemberTime_BegPeople_I_26 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_26")]
        public string PartyMemberTime_EndTime_I_26 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_26")]
        public string PartyMemberTime_EndPeople_I_26 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_26")]
        public string PartyMemberTime_BegTime_II_26 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_26")]
        public string PartyMemberTime_BegPeople_II_26 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_26")]
        public string PartyMemberTime_EndTime_II_26 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_26")]
        public string PartyMemberTime_EndPeople_II_26 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_26")]
        public string PartyMemberTime_BegTime_III_26 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_26")]
        public string PartyMemberTime_BegPeople_III_26 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_26")]
        public string PartyMemberTime_EndTime_III_26 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_26")]
        public string PartyMemberTime_EndPeople_III_26 { get; set; }
        [Display(Name = "Coment1_26")]
        public string Coment1_26 { get; set; }
        [Display(Name = "Coment2_26")]
        public string Coment2_26 { get; set; }
        [Display(Name = "Coment3_26")]
        public string Coment3_26 { get; set; }
        [Display(Name = "ComentSkill_26")]
        public string ComentSkill_26 { get; set; }
        [Display(Name = "Memo_26")]
        public string Memo_26 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_27")]
        public string PartyTime_OrderTimeTO_III_27 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_27")]
        public string PartyTime_ActTimeTO_III_27 { get; set; }
        [Display(Name = "OrderMemberCnt_27")]
        public string OrderMemberCnt_27 { get; set; }
        [Display(Name = "ActMemberCnt_27")]
        public string ActMemberCnt_27 { get; set; }
        [Display(Name = "LessMemberCnt_27")]
        public string LessMemberCnt_27 { get; set; }
        [Display(Name = "HelpMemberCnt_27")]
        public string HelpMemberCnt_27 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_27")]
        public string PartyMemberTime_BegTime_I_27 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_27")]
        public string PartyMemberTime_BegPeople_I_27 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_27")]
        public string PartyMemberTime_EndTime_I_27 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_27")]
        public string PartyMemberTime_EndPeople_I_27 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_27")]
        public string PartyMemberTime_BegTime_II_27 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_27")]
        public string PartyMemberTime_BegPeople_II_27 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_27")]
        public string PartyMemberTime_EndTime_II_27 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_27")]
        public string PartyMemberTime_EndPeople_II_27 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_27")]
        public string PartyMemberTime_BegTime_III_27 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_27")]
        public string PartyMemberTime_BegPeople_III_27 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_27")]
        public string PartyMemberTime_EndTime_III_27 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_27")]
        public string PartyMemberTime_EndPeople_III_27 { get; set; }
        [Display(Name = "Coment1_27")]
        public string Coment1_27 { get; set; }
        [Display(Name = "Coment2_27")]
        public string Coment2_27 { get; set; }
        [Display(Name = "Coment3_27")]
        public string Coment3_27 { get; set; }
        [Display(Name = "ComentSkill_27")]
        public string ComentSkill_27 { get; set; }
        [Display(Name = "Memo_27")]
        public string Memo_27 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_28")]
        public string PartyTime_OrderTimeTO_III_28 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_28")]
        public string PartyTime_ActTimeTO_III_28 { get; set; }
        [Display(Name = "OrderMemberCnt_28")]
        public string OrderMemberCnt_28 { get; set; }
        [Display(Name = "ActMemberCnt_28")]
        public string ActMemberCnt_28 { get; set; }
        [Display(Name = "LessMemberCnt_28")]
        public string LessMemberCnt_28 { get; set; }
        [Display(Name = "HelpMemberCnt_28")]
        public string HelpMemberCnt_28 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_28")]
        public string PartyMemberTime_BegTime_I_28 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_28")]
        public string PartyMemberTime_BegPeople_I_28 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_28")]
        public string PartyMemberTime_EndTime_I_28 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_28")]
        public string PartyMemberTime_EndPeople_I_28 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_28")]
        public string PartyMemberTime_BegTime_II_28 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_28")]
        public string PartyMemberTime_BegPeople_II_28 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_28")]
        public string PartyMemberTime_EndTime_II_28 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_28")]
        public string PartyMemberTime_EndPeople_II_28 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_28")]
        public string PartyMemberTime_BegTime_III_28 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_28")]
        public string PartyMemberTime_BegPeople_III_28 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_28")]
        public string PartyMemberTime_EndTime_III_28 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_28")]
        public string PartyMemberTime_EndPeople_III_28 { get; set; }
        [Display(Name = "Coment1_28")]
        public string Coment1_28 { get; set; }
        [Display(Name = "Coment2_28")]
        public string Coment2_28 { get; set; }
        [Display(Name = "Coment3_28")]
        public string Coment3_28 { get; set; }
        [Display(Name = "ComentSkill_28")]
        public string ComentSkill_28 { get; set; }
        [Display(Name = "Memo_28")]
        public string Memo_28 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_29")]
        public string PartyTime_OrderTimeTO_III_29 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_29")]
        public string PartyTime_ActTimeTO_III_29 { get; set; }
        [Display(Name = "OrderMemberCnt_29")]
        public string OrderMemberCnt_29 { get; set; }
        [Display(Name = "ActMemberCnt_29")]
        public string ActMemberCnt_29 { get; set; }
        [Display(Name = "LessMemberCnt_29")]
        public string LessMemberCnt_29 { get; set; }
        [Display(Name = "HelpMemberCnt_29")]
        public string HelpMemberCnt_29 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_29")]
        public string PartyMemberTime_BegTime_I_29 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_29")]
        public string PartyMemberTime_BegPeople_I_29 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_29")]
        public string PartyMemberTime_EndTime_I_29 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_29")]
        public string PartyMemberTime_EndPeople_I_29 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_29")]
        public string PartyMemberTime_BegTime_II_29 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_29")]
        public string PartyMemberTime_BegPeople_II_29 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_29")]
        public string PartyMemberTime_EndTime_II_29 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_29")]
        public string PartyMemberTime_EndPeople_II_29 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_29")]
        public string PartyMemberTime_BegTime_III_29 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_29")]
        public string PartyMemberTime_BegPeople_III_29 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_29")]
        public string PartyMemberTime_EndTime_III_29 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_29")]
        public string PartyMemberTime_EndPeople_III_29 { get; set; }
        [Display(Name = "Coment1_29")]
        public string Coment1_29 { get; set; }
        [Display(Name = "Coment2_29")]
        public string Coment2_29 { get; set; }
        [Display(Name = "Coment3_29")]
        public string Coment3_29 { get; set; }
        [Display(Name = "ComentSkill_29")]
        public string ComentSkill_29 { get; set; }
        [Display(Name = "Memo_29")]
        public string Memo_29 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_30")]
        public string PartyTime_OrderTimeTO_III_30 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_30")]
        public string PartyTime_ActTimeTO_III_30 { get; set; }
        [Display(Name = "OrderMemberCnt_30")]
        public string OrderMemberCnt_30 { get; set; }
        [Display(Name = "ActMemberCnt_30")]
        public string ActMemberCnt_30 { get; set; }
        [Display(Name = "LessMemberCnt_30")]
        public string LessMemberCnt_30 { get; set; }
        [Display(Name = "HelpMemberCnt_30")]
        public string HelpMemberCnt_30 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_30")]
        public string PartyMemberTime_BegTime_I_30 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_30")]
        public string PartyMemberTime_BegPeople_I_30 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_30")]
        public string PartyMemberTime_EndTime_I_30 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_30")]
        public string PartyMemberTime_EndPeople_I_30 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_30")]
        public string PartyMemberTime_BegTime_II_30 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_30")]
        public string PartyMemberTime_BegPeople_II_30 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_30")]
        public string PartyMemberTime_EndTime_II_30 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_30")]
        public string PartyMemberTime_EndPeople_II_30 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_30")]
        public string PartyMemberTime_BegTime_III_30 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_30")]
        public string PartyMemberTime_BegPeople_III_30 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_30")]
        public string PartyMemberTime_EndTime_III_30 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_30")]
        public string PartyMemberTime_EndPeople_III_30 { get; set; }
        [Display(Name = "Coment1_30")]
        public string Coment1_30 { get; set; }
        [Display(Name = "Coment2_30")]
        public string Coment2_30 { get; set; }
        [Display(Name = "Coment3_30")]
        public string Coment3_30 { get; set; }
        [Display(Name = "ComentSkill_30")]
        public string ComentSkill_30 { get; set; }
        [Display(Name = "Memo_30")]
        public string Memo_30 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_31")]
        public string PartyTime_OrderTimeTO_III_31 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_31")]
        public string PartyTime_ActTimeTO_III_31 { get; set; }
        [Display(Name = "OrderMemberCnt_31")]
        public string OrderMemberCnt_31 { get; set; }
        [Display(Name = "ActMemberCnt_31")]
        public string ActMemberCnt_31 { get; set; }
        [Display(Name = "LessMemberCnt_31")]
        public string LessMemberCnt_31 { get; set; }
        [Display(Name = "HelpMemberCnt_31")]
        public string HelpMemberCnt_31 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_31")]
        public string PartyMemberTime_BegTime_I_31 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_31")]
        public string PartyMemberTime_BegPeople_I_31 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_31")]
        public string PartyMemberTime_EndTime_I_31 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_31")]
        public string PartyMemberTime_EndPeople_I_31 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_31")]
        public string PartyMemberTime_BegTime_II_31 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_31")]
        public string PartyMemberTime_BegPeople_II_31 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_31")]
        public string PartyMemberTime_EndTime_II_31 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_31")]
        public string PartyMemberTime_EndPeople_II_31 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_31")]
        public string PartyMemberTime_BegTime_III_31 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_31")]
        public string PartyMemberTime_BegPeople_III_31 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_31")]
        public string PartyMemberTime_EndTime_III_31 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_31")]
        public string PartyMemberTime_EndPeople_III_31 { get; set; }
        [Display(Name = "Coment1_31")]
        public string Coment1_31 { get; set; }
        [Display(Name = "Coment2_31")]
        public string Coment2_31 { get; set; }
        [Display(Name = "Coment3_31")]
        public string Coment3_31 { get; set; }
        [Display(Name = "ComentSkill_31")]
        public string ComentSkill_31 { get; set; }
        [Display(Name = "Memo_31")]
        public string Memo_31 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_32")]
        public string PartyTime_OrderTimeTO_III_32 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_32")]
        public string PartyTime_ActTimeTO_III_32 { get; set; }
        [Display(Name = "OrderMemberCnt_32")]
        public string OrderMemberCnt_32 { get; set; }
        [Display(Name = "ActMemberCnt_32")]
        public string ActMemberCnt_32 { get; set; }
        [Display(Name = "LessMemberCnt_32")]
        public string LessMemberCnt_32 { get; set; }
        [Display(Name = "HelpMemberCnt_32")]
        public string HelpMemberCnt_32 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_32")]
        public string PartyMemberTime_BegTime_I_32 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_32")]
        public string PartyMemberTime_BegPeople_I_32 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_32")]
        public string PartyMemberTime_EndTime_I_32 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_32")]
        public string PartyMemberTime_EndPeople_I_32 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_32")]
        public string PartyMemberTime_BegTime_II_32 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_32")]
        public string PartyMemberTime_BegPeople_II_32 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_32")]
        public string PartyMemberTime_EndTime_II_32 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_32")]
        public string PartyMemberTime_EndPeople_II_32 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_32")]
        public string PartyMemberTime_BegTime_III_32 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_32")]
        public string PartyMemberTime_BegPeople_III_32 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_32")]
        public string PartyMemberTime_EndTime_III_32 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_32")]
        public string PartyMemberTime_EndPeople_III_32 { get; set; }
        [Display(Name = "Coment1_32")]
        public string Coment1_32 { get; set; }
        [Display(Name = "Coment2_32")]
        public string Coment2_32 { get; set; }
        [Display(Name = "Coment3_32")]
        public string Coment3_32 { get; set; }
        [Display(Name = "ComentSkill_32")]
        public string ComentSkill_32 { get; set; }
        [Display(Name = "Memo_32")]
        public string Memo_32 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_33")]
        public string PartyTime_OrderTimeTO_III_33 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_33")]
        public string PartyTime_ActTimeTO_III_33 { get; set; }
        [Display(Name = "OrderMemberCnt_33")]
        public string OrderMemberCnt_33 { get; set; }
        [Display(Name = "ActMemberCnt_33")]
        public string ActMemberCnt_33 { get; set; }
        [Display(Name = "LessMemberCnt_33")]
        public string LessMemberCnt_33 { get; set; }
        [Display(Name = "HelpMemberCnt_33")]
        public string HelpMemberCnt_33 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_33")]
        public string PartyMemberTime_BegTime_I_33 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_33")]
        public string PartyMemberTime_BegPeople_I_33 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_33")]
        public string PartyMemberTime_EndTime_I_33 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_33")]
        public string PartyMemberTime_EndPeople_I_33 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_33")]
        public string PartyMemberTime_BegTime_II_33 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_33")]
        public string PartyMemberTime_BegPeople_II_33 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_33")]
        public string PartyMemberTime_EndTime_II_33 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_33")]
        public string PartyMemberTime_EndPeople_II_33 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_33")]
        public string PartyMemberTime_BegTime_III_33 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_33")]
        public string PartyMemberTime_BegPeople_III_33 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_33")]
        public string PartyMemberTime_EndTime_III_33 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_33")]
        public string PartyMemberTime_EndPeople_III_33 { get; set; }
        [Display(Name = "Coment1_33")]
        public string Coment1_33 { get; set; }
        [Display(Name = "Coment2_33")]
        public string Coment2_33 { get; set; }
        [Display(Name = "Coment3_33")]
        public string Coment3_33 { get; set; }
        [Display(Name = "ComentSkill_33")]
        public string ComentSkill_33 { get; set; }
        [Display(Name = "Memo_33")]
        public string Memo_33 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_34")]
        public string PartyTime_OrderTimeTO_III_34 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_34")]
        public string PartyTime_ActTimeTO_III_34 { get; set; }
        [Display(Name = "OrderMemberCnt_34")]
        public string OrderMemberCnt_34 { get; set; }
        [Display(Name = "ActMemberCnt_34")]
        public string ActMemberCnt_34 { get; set; }
        [Display(Name = "LessMemberCnt_34")]
        public string LessMemberCnt_34 { get; set; }
        [Display(Name = "HelpMemberCnt_34")]
        public string HelpMemberCnt_34 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_34")]
        public string PartyMemberTime_BegTime_I_34 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_34")]
        public string PartyMemberTime_BegPeople_I_34 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_34")]
        public string PartyMemberTime_EndTime_I_34 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_34")]
        public string PartyMemberTime_EndPeople_I_34 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_34")]
        public string PartyMemberTime_BegTime_II_34 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_34")]
        public string PartyMemberTime_BegPeople_II_34 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_34")]
        public string PartyMemberTime_EndTime_II_34 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_34")]
        public string PartyMemberTime_EndPeople_II_34 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_34")]
        public string PartyMemberTime_BegTime_III_34 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_34")]
        public string PartyMemberTime_BegPeople_III_34 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_34")]
        public string PartyMemberTime_EndTime_III_34 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_34")]
        public string PartyMemberTime_EndPeople_III_34 { get; set; }
        [Display(Name = "Coment1_34")]
        public string Coment1_34 { get; set; }
        [Display(Name = "Coment2_34")]
        public string Coment2_34 { get; set; }
        [Display(Name = "Coment3_34")]
        public string Coment3_34 { get; set; }
        [Display(Name = "ComentSkill_34")]
        public string ComentSkill_34 { get; set; }
        [Display(Name = "Memo_34")]
        public string Memo_34 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_35")]
        public string PartyTime_OrderTimeTO_III_35 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_35")]
        public string PartyTime_ActTimeTO_III_35 { get; set; }
        [Display(Name = "OrderMemberCnt_35")]
        public string OrderMemberCnt_35 { get; set; }
        [Display(Name = "ActMemberCnt_35")]
        public string ActMemberCnt_35 { get; set; }
        [Display(Name = "LessMemberCnt_35")]
        public string LessMemberCnt_35 { get; set; }
        [Display(Name = "HelpMemberCnt_35")]
        public string HelpMemberCnt_35 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_35")]
        public string PartyMemberTime_BegTime_I_35 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_35")]
        public string PartyMemberTime_BegPeople_I_35 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_35")]
        public string PartyMemberTime_EndTime_I_35 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_35")]
        public string PartyMemberTime_EndPeople_I_35 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_35")]
        public string PartyMemberTime_BegTime_II_35 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_35")]
        public string PartyMemberTime_BegPeople_II_35 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_35")]
        public string PartyMemberTime_EndTime_II_35 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_35")]
        public string PartyMemberTime_EndPeople_II_35 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_35")]
        public string PartyMemberTime_BegTime_III_35 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_35")]
        public string PartyMemberTime_BegPeople_III_35 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_35")]
        public string PartyMemberTime_EndTime_III_35 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_35")]
        public string PartyMemberTime_EndPeople_III_35 { get; set; }
        [Display(Name = "Coment1_35")]
        public string Coment1_35 { get; set; }
        [Display(Name = "Coment2_35")]
        public string Coment2_35 { get; set; }
        [Display(Name = "Coment3_35")]
        public string Coment3_35 { get; set; }
        [Display(Name = "ComentSkill_35")]
        public string ComentSkill_35 { get; set; }
        [Display(Name = "Memo_35")]
        public string Memo_35 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_36")]
        public string PartyTime_OrderTimeTO_III_36 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_36")]
        public string PartyTime_ActTimeTO_III_36 { get; set; }
        [Display(Name = "OrderMemberCnt_36")]
        public string OrderMemberCnt_36 { get; set; }
        [Display(Name = "ActMemberCnt_36")]
        public string ActMemberCnt_36 { get; set; }
        [Display(Name = "LessMemberCnt_36")]
        public string LessMemberCnt_36 { get; set; }
        [Display(Name = "HelpMemberCnt_36")]
        public string HelpMemberCnt_36 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_36")]
        public string PartyMemberTime_BegTime_I_36 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_36")]
        public string PartyMemberTime_BegPeople_I_36 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_36")]
        public string PartyMemberTime_EndTime_I_36 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_36")]
        public string PartyMemberTime_EndPeople_I_36 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_36")]
        public string PartyMemberTime_BegTime_II_36 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_36")]
        public string PartyMemberTime_BegPeople_II_36 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_36")]
        public string PartyMemberTime_EndTime_II_36 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_36")]
        public string PartyMemberTime_EndPeople_II_36 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_36")]
        public string PartyMemberTime_BegTime_III_36 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_36")]
        public string PartyMemberTime_BegPeople_III_36 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_36")]
        public string PartyMemberTime_EndTime_III_36 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_36")]
        public string PartyMemberTime_EndPeople_III_36 { get; set; }
        [Display(Name = "Coment1_36")]
        public string Coment1_36 { get; set; }
        [Display(Name = "Coment2_36")]
        public string Coment2_36 { get; set; }
        [Display(Name = "Coment3_36")]
        public string Coment3_36 { get; set; }
        [Display(Name = "ComentSkill_36")]
        public string ComentSkill_36 { get; set; }
        [Display(Name = "Memo_36")]
        public string Memo_36 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_37")]
        public string PartyTime_OrderTimeTO_III_37 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_37")]
        public string PartyTime_ActTimeTO_III_37 { get; set; }
        [Display(Name = "OrderMemberCnt_37")]
        public string OrderMemberCnt_37 { get; set; }
        [Display(Name = "ActMemberCnt_37")]
        public string ActMemberCnt_37 { get; set; }
        [Display(Name = "LessMemberCnt_37")]
        public string LessMemberCnt_37 { get; set; }
        [Display(Name = "HelpMemberCnt_37")]
        public string HelpMemberCnt_37 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_37")]
        public string PartyMemberTime_BegTime_I_37 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_37")]
        public string PartyMemberTime_BegPeople_I_37 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_37")]
        public string PartyMemberTime_EndTime_I_37 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_37")]
        public string PartyMemberTime_EndPeople_I_37 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_37")]
        public string PartyMemberTime_BegTime_II_37 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_37")]
        public string PartyMemberTime_BegPeople_II_37 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_37")]
        public string PartyMemberTime_EndTime_II_37 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_37")]
        public string PartyMemberTime_EndPeople_II_37 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_37")]
        public string PartyMemberTime_BegTime_III_37 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_37")]
        public string PartyMemberTime_BegPeople_III_37 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_37")]
        public string PartyMemberTime_EndTime_III_37 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_37")]
        public string PartyMemberTime_EndPeople_III_37 { get; set; }
        [Display(Name = "Coment1_37")]
        public string Coment1_37 { get; set; }
        [Display(Name = "Coment2_37")]
        public string Coment2_37 { get; set; }
        [Display(Name = "Coment3_37")]
        public string Coment3_37 { get; set; }
        [Display(Name = "ComentSkill_37")]
        public string ComentSkill_37 { get; set; }
        [Display(Name = "Memo_37")]
        public string Memo_37 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_38")]
        public string PartyTime_OrderTimeTO_III_38 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_38")]
        public string PartyTime_ActTimeTO_III_38 { get; set; }
        [Display(Name = "OrderMemberCnt_38")]
        public string OrderMemberCnt_38 { get; set; }
        [Display(Name = "ActMemberCnt_38")]
        public string ActMemberCnt_38 { get; set; }
        [Display(Name = "LessMemberCnt_38")]
        public string LessMemberCnt_38 { get; set; }
        [Display(Name = "HelpMemberCnt_38")]
        public string HelpMemberCnt_38 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_38")]
        public string PartyMemberTime_BegTime_I_38 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_38")]
        public string PartyMemberTime_BegPeople_I_38 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_38")]
        public string PartyMemberTime_EndTime_I_38 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_38")]
        public string PartyMemberTime_EndPeople_I_38 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_38")]
        public string PartyMemberTime_BegTime_II_38 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_38")]
        public string PartyMemberTime_BegPeople_II_38 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_38")]
        public string PartyMemberTime_EndTime_II_38 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_38")]
        public string PartyMemberTime_EndPeople_II_38 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_38")]
        public string PartyMemberTime_BegTime_III_38 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_38")]
        public string PartyMemberTime_BegPeople_III_38 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_38")]
        public string PartyMemberTime_EndTime_III_38 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_38")]
        public string PartyMemberTime_EndPeople_III_38 { get; set; }
        [Display(Name = "Coment1_38")]
        public string Coment1_38 { get; set; }
        [Display(Name = "Coment2_38")]
        public string Coment2_38 { get; set; }
        [Display(Name = "Coment3_38")]
        public string Coment3_38 { get; set; }
        [Display(Name = "ComentSkill_38")]
        public string ComentSkill_38 { get; set; }
        [Display(Name = "Memo_38")]
        public string Memo_38 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_39")]
        public string PartyTime_OrderTimeTO_III_39 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_39")]
        public string PartyTime_ActTimeTO_III_39 { get; set; }
        [Display(Name = "OrderMemberCnt_39")]
        public string OrderMemberCnt_39 { get; set; }
        [Display(Name = "ActMemberCnt_39")]
        public string ActMemberCnt_39 { get; set; }
        [Display(Name = "LessMemberCnt_39")]
        public string LessMemberCnt_39 { get; set; }
        [Display(Name = "HelpMemberCnt_39")]
        public string HelpMemberCnt_39 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_39")]
        public string PartyMemberTime_BegTime_I_39 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_39")]
        public string PartyMemberTime_BegPeople_I_39 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_39")]
        public string PartyMemberTime_EndTime_I_39 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_39")]
        public string PartyMemberTime_EndPeople_I_39 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_39")]
        public string PartyMemberTime_BegTime_II_39 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_39")]
        public string PartyMemberTime_BegPeople_II_39 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_39")]
        public string PartyMemberTime_EndTime_II_39 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_39")]
        public string PartyMemberTime_EndPeople_II_39 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_39")]
        public string PartyMemberTime_BegTime_III_39 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_39")]
        public string PartyMemberTime_BegPeople_III_39 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_39")]
        public string PartyMemberTime_EndTime_III_39 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_39")]
        public string PartyMemberTime_EndPeople_III_39 { get; set; }
        [Display(Name = "Coment1_39")]
        public string Coment1_39 { get; set; }
        [Display(Name = "Coment2_39")]
        public string Coment2_39 { get; set; }
        [Display(Name = "Coment3_39")]
        public string Coment3_39 { get; set; }
        [Display(Name = "ComentSkill_39")]
        public string ComentSkill_39 { get; set; }
        [Display(Name = "Memo_39")]
        public string Memo_39 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_40")]
        public string PartyTime_OrderTimeTO_III_40 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_40")]
        public string PartyTime_ActTimeTO_III_40 { get; set; }
        [Display(Name = "OrderMemberCnt_40")]
        public string OrderMemberCnt_40 { get; set; }
        [Display(Name = "ActMemberCnt_40")]
        public string ActMemberCnt_40 { get; set; }
        [Display(Name = "LessMemberCnt_40")]
        public string LessMemberCnt_40 { get; set; }
        [Display(Name = "HelpMemberCnt_40")]
        public string HelpMemberCnt_40 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_40")]
        public string PartyMemberTime_BegTime_I_40 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_40")]
        public string PartyMemberTime_BegPeople_I_40 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_40")]
        public string PartyMemberTime_EndTime_I_40 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_40")]
        public string PartyMemberTime_EndPeople_I_40 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_40")]
        public string PartyMemberTime_BegTime_II_40 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_40")]
        public string PartyMemberTime_BegPeople_II_40 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_40")]
        public string PartyMemberTime_EndTime_II_40 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_40")]
        public string PartyMemberTime_EndPeople_II_40 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_40")]
        public string PartyMemberTime_BegTime_III_40 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_40")]
        public string PartyMemberTime_BegPeople_III_40 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_40")]
        public string PartyMemberTime_EndTime_III_40 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_40")]
        public string PartyMemberTime_EndPeople_III_40 { get; set; }
        [Display(Name = "Coment1_40")]
        public string Coment1_40 { get; set; }
        [Display(Name = "Coment2_40")]
        public string Coment2_40 { get; set; }
        [Display(Name = "Coment3_40")]
        public string Coment3_40 { get; set; }
        [Display(Name = "ComentSkill_40")]
        public string ComentSkill_40 { get; set; }
        [Display(Name = "Memo_40")]
        public string Memo_40 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_41")]
        public string PartyTime_OrderTimeTO_III_41 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_41")]
        public string PartyTime_ActTimeTO_III_41 { get; set; }
        [Display(Name = "OrderMemberCnt_41")]
        public string OrderMemberCnt_41 { get; set; }
        [Display(Name = "ActMemberCnt_41")]
        public string ActMemberCnt_41 { get; set; }
        [Display(Name = "LessMemberCnt_41")]
        public string LessMemberCnt_41 { get; set; }
        [Display(Name = "HelpMemberCnt_41")]
        public string HelpMemberCnt_41 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_41")]
        public string PartyMemberTime_BegTime_I_41 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_41")]
        public string PartyMemberTime_BegPeople_I_41 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_41")]
        public string PartyMemberTime_EndTime_I_41 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_41")]
        public string PartyMemberTime_EndPeople_I_41 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_41")]
        public string PartyMemberTime_BegTime_II_41 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_41")]
        public string PartyMemberTime_BegPeople_II_41 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_41")]
        public string PartyMemberTime_EndTime_II_41 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_41")]
        public string PartyMemberTime_EndPeople_II_41 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_41")]
        public string PartyMemberTime_BegTime_III_41 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_41")]
        public string PartyMemberTime_BegPeople_III_41 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_41")]
        public string PartyMemberTime_EndTime_III_41 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_41")]
        public string PartyMemberTime_EndPeople_III_41 { get; set; }
        [Display(Name = "Coment1_41")]
        public string Coment1_41 { get; set; }
        [Display(Name = "Coment2_41")]
        public string Coment2_41 { get; set; }
        [Display(Name = "Coment3_41")]
        public string Coment3_41 { get; set; }
        [Display(Name = "ComentSkill_41")]
        public string ComentSkill_41 { get; set; }
        [Display(Name = "Memo_41")]
        public string Memo_41 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_42")]
        public string PartyTime_OrderTimeTO_III_42 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_42")]
        public string PartyTime_ActTimeTO_III_42 { get; set; }
        [Display(Name = "OrderMemberCnt_42")]
        public string OrderMemberCnt_42 { get; set; }
        [Display(Name = "ActMemberCnt_42")]
        public string ActMemberCnt_42 { get; set; }
        [Display(Name = "LessMemberCnt_42")]
        public string LessMemberCnt_42 { get; set; }
        [Display(Name = "HelpMemberCnt_42")]
        public string HelpMemberCnt_42 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_42")]
        public string PartyMemberTime_BegTime_I_42 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_42")]
        public string PartyMemberTime_BegPeople_I_42 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_42")]
        public string PartyMemberTime_EndTime_I_42 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_42")]
        public string PartyMemberTime_EndPeople_I_42 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_42")]
        public string PartyMemberTime_BegTime_II_42 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_42")]
        public string PartyMemberTime_BegPeople_II_42 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_42")]
        public string PartyMemberTime_EndTime_II_42 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_42")]
        public string PartyMemberTime_EndPeople_II_42 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_42")]
        public string PartyMemberTime_BegTime_III_42 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_42")]
        public string PartyMemberTime_BegPeople_III_42 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_42")]
        public string PartyMemberTime_EndTime_III_42 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_42")]
        public string PartyMemberTime_EndPeople_III_42 { get; set; }
        [Display(Name = "Coment1_42")]
        public string Coment1_42 { get; set; }
        [Display(Name = "Coment2_42")]
        public string Coment2_42 { get; set; }
        [Display(Name = "Coment3_42")]
        public string Coment3_42 { get; set; }
        [Display(Name = "ComentSkill_42")]
        public string ComentSkill_42 { get; set; }
        [Display(Name = "Memo_42")]
        public string Memo_42 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_43")]
        public string PartyTime_OrderTimeTO_III_43 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_43")]
        public string PartyTime_ActTimeTO_III_43 { get; set; }
        [Display(Name = "OrderMemberCnt_43")]
        public string OrderMemberCnt_43 { get; set; }
        [Display(Name = "ActMemberCnt_43")]
        public string ActMemberCnt_43 { get; set; }
        [Display(Name = "LessMemberCnt_43")]
        public string LessMemberCnt_43 { get; set; }
        [Display(Name = "HelpMemberCnt_43")]
        public string HelpMemberCnt_43 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_43")]
        public string PartyMemberTime_BegTime_I_43 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_43")]
        public string PartyMemberTime_BegPeople_I_43 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_43")]
        public string PartyMemberTime_EndTime_I_43 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_43")]
        public string PartyMemberTime_EndPeople_I_43 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_43")]
        public string PartyMemberTime_BegTime_II_43 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_43")]
        public string PartyMemberTime_BegPeople_II_43 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_43")]
        public string PartyMemberTime_EndTime_II_43 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_43")]
        public string PartyMemberTime_EndPeople_II_43 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_43")]
        public string PartyMemberTime_BegTime_III_43 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_43")]
        public string PartyMemberTime_BegPeople_III_43 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_43")]
        public string PartyMemberTime_EndTime_III_43 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_43")]
        public string PartyMemberTime_EndPeople_III_43 { get; set; }
        [Display(Name = "Coment1_43")]
        public string Coment1_43 { get; set; }
        [Display(Name = "Coment2_43")]
        public string Coment2_43 { get; set; }
        [Display(Name = "Coment3_43")]
        public string Coment3_43 { get; set; }
        [Display(Name = "ComentSkill_43")]
        public string ComentSkill_43 { get; set; }
        [Display(Name = "Memo_43")]
        public string Memo_43 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_44")]
        public string PartyTime_OrderTimeTO_III_44 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_44")]
        public string PartyTime_ActTimeTO_III_44 { get; set; }
        [Display(Name = "OrderMemberCnt_44")]
        public string OrderMemberCnt_44 { get; set; }
        [Display(Name = "ActMemberCnt_44")]
        public string ActMemberCnt_44 { get; set; }
        [Display(Name = "LessMemberCnt_44")]
        public string LessMemberCnt_44 { get; set; }
        [Display(Name = "HelpMemberCnt_44")]
        public string HelpMemberCnt_44 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_44")]
        public string PartyMemberTime_BegTime_I_44 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_44")]
        public string PartyMemberTime_BegPeople_I_44 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_44")]
        public string PartyMemberTime_EndTime_I_44 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_44")]
        public string PartyMemberTime_EndPeople_I_44 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_44")]
        public string PartyMemberTime_BegTime_II_44 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_44")]
        public string PartyMemberTime_BegPeople_II_44 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_44")]
        public string PartyMemberTime_EndTime_II_44 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_44")]
        public string PartyMemberTime_EndPeople_II_44 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_44")]
        public string PartyMemberTime_BegTime_III_44 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_44")]
        public string PartyMemberTime_BegPeople_III_44 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_44")]
        public string PartyMemberTime_EndTime_III_44 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_44")]
        public string PartyMemberTime_EndPeople_III_44 { get; set; }
        [Display(Name = "Coment1_44")]
        public string Coment1_44 { get; set; }
        [Display(Name = "Coment2_44")]
        public string Coment2_44 { get; set; }
        [Display(Name = "Coment3_44")]
        public string Coment3_44 { get; set; }
        [Display(Name = "ComentSkill_44")]
        public string ComentSkill_44 { get; set; }
        [Display(Name = "Memo_44")]
        public string Memo_44 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_45")]
        public string PartyTime_OrderTimeTO_III_45 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_45")]
        public string PartyTime_ActTimeTO_III_45 { get; set; }
        [Display(Name = "OrderMemberCnt_45")]
        public string OrderMemberCnt_45 { get; set; }
        [Display(Name = "ActMemberCnt_45")]
        public string ActMemberCnt_45 { get; set; }
        [Display(Name = "LessMemberCnt_45")]
        public string LessMemberCnt_45 { get; set; }
        [Display(Name = "HelpMemberCnt_45")]
        public string HelpMemberCnt_45 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_45")]
        public string PartyMemberTime_BegTime_I_45 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_45")]
        public string PartyMemberTime_BegPeople_I_45 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_45")]
        public string PartyMemberTime_EndTime_I_45 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_45")]
        public string PartyMemberTime_EndPeople_I_45 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_45")]
        public string PartyMemberTime_BegTime_II_45 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_45")]
        public string PartyMemberTime_BegPeople_II_45 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_45")]
        public string PartyMemberTime_EndTime_II_45 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_45")]
        public string PartyMemberTime_EndPeople_II_45 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_45")]
        public string PartyMemberTime_BegTime_III_45 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_45")]
        public string PartyMemberTime_BegPeople_III_45 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_45")]
        public string PartyMemberTime_EndTime_III_45 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_45")]
        public string PartyMemberTime_EndPeople_III_45 { get; set; }
        [Display(Name = "Coment1_45")]
        public string Coment1_45 { get; set; }
        [Display(Name = "Coment2_45")]
        public string Coment2_45 { get; set; }
        [Display(Name = "Coment3_45")]
        public string Coment3_45 { get; set; }
        [Display(Name = "ComentSkill_45")]
        public string ComentSkill_45 { get; set; }
        [Display(Name = "Memo_45")]
        public string Memo_45 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_46")]
        public string PartyTime_OrderTimeTO_III_46 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_46")]
        public string PartyTime_ActTimeTO_III_46 { get; set; }
        [Display(Name = "OrderMemberCnt_46")]
        public string OrderMemberCnt_46 { get; set; }
        [Display(Name = "ActMemberCnt_46")]
        public string ActMemberCnt_46 { get; set; }
        [Display(Name = "LessMemberCnt_46")]
        public string LessMemberCnt_46 { get; set; }
        [Display(Name = "HelpMemberCnt_46")]
        public string HelpMemberCnt_46 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_46")]
        public string PartyMemberTime_BegTime_I_46 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_46")]
        public string PartyMemberTime_BegPeople_I_46 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_46")]
        public string PartyMemberTime_EndTime_I_46 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_46")]
        public string PartyMemberTime_EndPeople_I_46 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_46")]
        public string PartyMemberTime_BegTime_II_46 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_46")]
        public string PartyMemberTime_BegPeople_II_46 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_46")]
        public string PartyMemberTime_EndTime_II_46 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_46")]
        public string PartyMemberTime_EndPeople_II_46 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_46")]
        public string PartyMemberTime_BegTime_III_46 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_46")]
        public string PartyMemberTime_BegPeople_III_46 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_46")]
        public string PartyMemberTime_EndTime_III_46 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_46")]
        public string PartyMemberTime_EndPeople_III_46 { get; set; }
        [Display(Name = "Coment1_46")]
        public string Coment1_46 { get; set; }
        [Display(Name = "Coment2_46")]
        public string Coment2_46 { get; set; }
        [Display(Name = "Coment3_46")]
        public string Coment3_46 { get; set; }
        [Display(Name = "ComentSkill_46")]
        public string ComentSkill_46 { get; set; }
        [Display(Name = "Memo_46")]
        public string Memo_46 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_47")]
        public string PartyTime_OrderTimeTO_III_47 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_47")]
        public string PartyTime_ActTimeTO_III_47 { get; set; }
        [Display(Name = "OrderMemberCnt_47")]
        public string OrderMemberCnt_47 { get; set; }
        [Display(Name = "ActMemberCnt_47")]
        public string ActMemberCnt_47 { get; set; }
        [Display(Name = "LessMemberCnt_47")]
        public string LessMemberCnt_47 { get; set; }
        [Display(Name = "HelpMemberCnt_47")]
        public string HelpMemberCnt_47 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_47")]
        public string PartyMemberTime_BegTime_I_47 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_47")]
        public string PartyMemberTime_BegPeople_I_47 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_47")]
        public string PartyMemberTime_EndTime_I_47 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_47")]
        public string PartyMemberTime_EndPeople_I_47 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_47")]
        public string PartyMemberTime_BegTime_II_47 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_47")]
        public string PartyMemberTime_BegPeople_II_47 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_47")]
        public string PartyMemberTime_EndTime_II_47 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_47")]
        public string PartyMemberTime_EndPeople_II_47 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_47")]
        public string PartyMemberTime_BegTime_III_47 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_47")]
        public string PartyMemberTime_BegPeople_III_47 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_47")]
        public string PartyMemberTime_EndTime_III_47 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_47")]
        public string PartyMemberTime_EndPeople_III_47 { get; set; }
        [Display(Name = "Coment1_47")]
        public string Coment1_47 { get; set; }
        [Display(Name = "Coment2_47")]
        public string Coment2_47 { get; set; }
        [Display(Name = "Coment3_47")]
        public string Coment3_47 { get; set; }
        [Display(Name = "ComentSkill_47")]
        public string ComentSkill_47 { get; set; }
        [Display(Name = "Memo_47")]
        public string Memo_47 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_48")]
        public string PartyTime_OrderTimeTO_III_48 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_48")]
        public string PartyTime_ActTimeTO_III_48 { get; set; }
        [Display(Name = "OrderMemberCnt_48")]
        public string OrderMemberCnt_48 { get; set; }
        [Display(Name = "ActMemberCnt_48")]
        public string ActMemberCnt_48 { get; set; }
        [Display(Name = "LessMemberCnt_48")]
        public string LessMemberCnt_48 { get; set; }
        [Display(Name = "HelpMemberCnt_48")]
        public string HelpMemberCnt_48 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_48")]
        public string PartyMemberTime_BegTime_I_48 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_48")]
        public string PartyMemberTime_BegPeople_I_48 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_48")]
        public string PartyMemberTime_EndTime_I_48 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_48")]
        public string PartyMemberTime_EndPeople_I_48 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_48")]
        public string PartyMemberTime_BegTime_II_48 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_48")]
        public string PartyMemberTime_BegPeople_II_48 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_48")]
        public string PartyMemberTime_EndTime_II_48 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_48")]
        public string PartyMemberTime_EndPeople_II_48 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_48")]
        public string PartyMemberTime_BegTime_III_48 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_48")]
        public string PartyMemberTime_BegPeople_III_48 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_48")]
        public string PartyMemberTime_EndTime_III_48 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_48")]
        public string PartyMemberTime_EndPeople_III_48 { get; set; }
        [Display(Name = "Coment1_48")]
        public string Coment1_48 { get; set; }
        [Display(Name = "Coment2_48")]
        public string Coment2_48 { get; set; }
        [Display(Name = "Coment3_48")]
        public string Coment3_48 { get; set; }
        [Display(Name = "ComentSkill_48")]
        public string ComentSkill_48 { get; set; }
        [Display(Name = "Memo_48")]
        public string Memo_48 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_49")]
        public string PartyTime_OrderTimeTO_III_49 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_49")]
        public string PartyTime_ActTimeTO_III_49 { get; set; }
        [Display(Name = "OrderMemberCnt_49")]
        public string OrderMemberCnt_49 { get; set; }
        [Display(Name = "ActMemberCnt_49")]
        public string ActMemberCnt_49 { get; set; }
        [Display(Name = "LessMemberCnt_49")]
        public string LessMemberCnt_49 { get; set; }
        [Display(Name = "HelpMemberCnt_49")]
        public string HelpMemberCnt_49 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_49")]
        public string PartyMemberTime_BegTime_I_49 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_49")]
        public string PartyMemberTime_BegPeople_I_49 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_49")]
        public string PartyMemberTime_EndTime_I_49 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_49")]
        public string PartyMemberTime_EndPeople_I_49 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_49")]
        public string PartyMemberTime_BegTime_II_49 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_49")]
        public string PartyMemberTime_BegPeople_II_49 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_49")]
        public string PartyMemberTime_EndTime_II_49 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_49")]
        public string PartyMemberTime_EndPeople_II_49 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_49")]
        public string PartyMemberTime_BegTime_III_49 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_49")]
        public string PartyMemberTime_BegPeople_III_49 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_49")]
        public string PartyMemberTime_EndTime_III_49 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_49")]
        public string PartyMemberTime_EndPeople_III_49 { get; set; }
        [Display(Name = "Coment1_49")]
        public string Coment1_49 { get; set; }
        [Display(Name = "Coment2_49")]
        public string Coment2_49 { get; set; }
        [Display(Name = "Coment3_49")]
        public string Coment3_49 { get; set; }
        [Display(Name = "ComentSkill_49")]
        public string ComentSkill_49 { get; set; }
        [Display(Name = "Memo_49")]
        public string Memo_49 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_III_50")]
        public string PartyTime_OrderTimeTO_III_50 { get; set; }
        [Display(Name = "PartyTime_ActTimeTO_III_50")]
        public string PartyTime_ActTimeTO_III_50 { get; set; }
        [Display(Name = "OrderMemberCnt_50")]
        public string OrderMemberCnt_50 { get; set; }
        [Display(Name = "ActMemberCnt_50")]
        public string ActMemberCnt_50 { get; set; }
        [Display(Name = "LessMemberCnt_50")]
        public string LessMemberCnt_50 { get; set; }
        [Display(Name = "HelpMemberCnt_50")]
        public string HelpMemberCnt_50 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_I_50")]
        public string PartyMemberTime_BegTime_I_50 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_I_50")]
        public string PartyMemberTime_BegPeople_I_50 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_I_50")]
        public string PartyMemberTime_EndTime_I_50 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_I_50")]
        public string PartyMemberTime_EndPeople_I_50 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_II_50")]
        public string PartyMemberTime_BegTime_II_50 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_II_50")]
        public string PartyMemberTime_BegPeople_II_50 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_II_50")]
        public string PartyMemberTime_EndTime_II_50 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_II_50")]
        public string PartyMemberTime_EndPeople_II_50 { get; set; }
        [Display(Name = "PartyMemberTime_BegTime_III_50")]
        public string PartyMemberTime_BegTime_III_50 { get; set; }
        [Display(Name = "PartyMemberTime_BegPeople_III_50")]
        public string PartyMemberTime_BegPeople_III_50 { get; set; }
        [Display(Name = "PartyMemberTime_EndTime_III_50")]
        public string PartyMemberTime_EndTime_III_50 { get; set; }
        [Display(Name = "PartyMemberTime_EndPeople_III_50")]
        public string PartyMemberTime_EndPeople_III_50 { get; set; }
        [Display(Name = "Coment1_50")]
        public string Coment1_50 { get; set; }
        [Display(Name = "Coment2_50")]
        public string Coment2_50 { get; set; }
        [Display(Name = "Coment3_50")]
        public string Coment3_50 { get; set; }
        [Display(Name = "ComentSkill_50")]
        public string ComentSkill_50 { get; set; }
        [Display(Name = "Memo_50")]
        public string Memo_50 { get; set; }

        [Display(Name = "PartyFood_FoodName_XI_01")]
        public string PartyFood_FoodName_XI_01 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_01")]
        public string PartyFood_BeginTime_XI_01 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_01")]
        public string PartyFood_EndTime_XI_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_01")]
        public string PartyFood_RestRoomTime_XI_01 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_01")]
        public string PartyFood_RestRoomFlg_XI_01 { get; set; }
        [Display(Name = "EndTimeA_01")]
        public string EndTimeA_01 { get; set; }
        [Display(Name = "EndTimeB_01")]
        public string EndTimeB_01 { get; set; }
        [Display(Name = "AllSupplyTime_I_01")]
        public string AllSupplyTime_I_01 { get; set; }
        [Display(Name = "AllSupplyTime_II_01")]
        public string AllSupplyTime_II_01 { get; set; }
        [Display(Name = "FoodNoInput_01")]
        public string FoodNoInput_01 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_02")]
        public string PartyFood_FoodName_XI_02 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_02")]
        public string PartyFood_BeginTime_XI_02 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_02")]
        public string PartyFood_EndTime_XI_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_02")]
        public string PartyFood_RestRoomTime_XI_02 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_02")]
        public string PartyFood_RestRoomFlg_XI_02 { get; set; }
        [Display(Name = "EndTimeA_02")]
        public string EndTimeA_02 { get; set; }
        [Display(Name = "EndTimeB_02")]
        public string EndTimeB_02 { get; set; }
        [Display(Name = "AllSupplyTime_I_02")]
        public string AllSupplyTime_I_02 { get; set; }
        [Display(Name = "AllSupplyTime_II_02")]
        public string AllSupplyTime_II_02 { get; set; }
        [Display(Name = "FoodNoInput_02")]
        public string FoodNoInput_02 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_03")]
        public string PartyFood_FoodName_XI_03 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_03")]
        public string PartyFood_BeginTime_XI_03 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_03")]
        public string PartyFood_EndTime_XI_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_03")]
        public string PartyFood_RestRoomTime_XI_03 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_03")]
        public string PartyFood_RestRoomFlg_XI_03 { get; set; }
        [Display(Name = "EndTimeA_03")]
        public string EndTimeA_03 { get; set; }
        [Display(Name = "EndTimeB_03")]
        public string EndTimeB_03 { get; set; }
        [Display(Name = "AllSupplyTime_I_03")]
        public string AllSupplyTime_I_03 { get; set; }
        [Display(Name = "AllSupplyTime_II_03")]
        public string AllSupplyTime_II_03 { get; set; }
        [Display(Name = "FoodNoInput_03")]
        public string FoodNoInput_03 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_04")]
        public string PartyFood_FoodName_XI_04 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_04")]
        public string PartyFood_BeginTime_XI_04 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_04")]
        public string PartyFood_EndTime_XI_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_04")]
        public string PartyFood_RestRoomTime_XI_04 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_04")]
        public string PartyFood_RestRoomFlg_XI_04 { get; set; }
        [Display(Name = "EndTimeA_04")]
        public string EndTimeA_04 { get; set; }
        [Display(Name = "EndTimeB_04")]
        public string EndTimeB_04 { get; set; }
        [Display(Name = "AllSupplyTime_I_04")]
        public string AllSupplyTime_I_04 { get; set; }
        [Display(Name = "AllSupplyTime_II_04")]
        public string AllSupplyTime_II_04 { get; set; }
        [Display(Name = "FoodNoInput_04")]
        public string FoodNoInput_04 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_05")]
        public string PartyFood_FoodName_XI_05 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_05")]
        public string PartyFood_BeginTime_XI_05 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_05")]
        public string PartyFood_EndTime_XI_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_05")]
        public string PartyFood_RestRoomTime_XI_05 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_05")]
        public string PartyFood_RestRoomFlg_XI_05 { get; set; }
        [Display(Name = "EndTimeA_05")]
        public string EndTimeA_05 { get; set; }
        [Display(Name = "EndTimeB_05")]
        public string EndTimeB_05 { get; set; }
        [Display(Name = "AllSupplyTime_I_05")]
        public string AllSupplyTime_I_05 { get; set; }
        [Display(Name = "AllSupplyTime_II_05")]
        public string AllSupplyTime_II_05 { get; set; }
        [Display(Name = "FoodNoInput_05")]
        public string FoodNoInput_05 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_06")]
        public string PartyFood_FoodName_XI_06 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_06")]
        public string PartyFood_BeginTime_XI_06 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_06")]
        public string PartyFood_EndTime_XI_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_06")]
        public string PartyFood_RestRoomTime_XI_06 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_06")]
        public string PartyFood_RestRoomFlg_XI_06 { get; set; }
        [Display(Name = "EndTimeA_06")]
        public string EndTimeA_06 { get; set; }
        [Display(Name = "EndTimeB_06")]
        public string EndTimeB_06 { get; set; }
        [Display(Name = "AllSupplyTime_I_06")]
        public string AllSupplyTime_I_06 { get; set; }
        [Display(Name = "AllSupplyTime_II_06")]
        public string AllSupplyTime_II_06 { get; set; }
        [Display(Name = "FoodNoInput_06")]
        public string FoodNoInput_06 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_07")]
        public string PartyFood_FoodName_XI_07 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_07")]
        public string PartyFood_BeginTime_XI_07 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_07")]
        public string PartyFood_EndTime_XI_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_07")]
        public string PartyFood_RestRoomTime_XI_07 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_07")]
        public string PartyFood_RestRoomFlg_XI_07 { get; set; }
        [Display(Name = "EndTimeA_07")]
        public string EndTimeA_07 { get; set; }
        [Display(Name = "EndTimeB_07")]
        public string EndTimeB_07 { get; set; }
        [Display(Name = "AllSupplyTime_I_07")]
        public string AllSupplyTime_I_07 { get; set; }
        [Display(Name = "AllSupplyTime_II_07")]
        public string AllSupplyTime_II_07 { get; set; }
        [Display(Name = "FoodNoInput_07")]
        public string FoodNoInput_07 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_08")]
        public string PartyFood_FoodName_XI_08 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_08")]
        public string PartyFood_BeginTime_XI_08 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_08")]
        public string PartyFood_EndTime_XI_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_08")]
        public string PartyFood_RestRoomTime_XI_08 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_08")]
        public string PartyFood_RestRoomFlg_XI_08 { get; set; }
        [Display(Name = "EndTimeA_08")]
        public string EndTimeA_08 { get; set; }
        [Display(Name = "EndTimeB_08")]
        public string EndTimeB_08 { get; set; }
        [Display(Name = "AllSupplyTime_I_08")]
        public string AllSupplyTime_I_08 { get; set; }
        [Display(Name = "AllSupplyTime_II_08")]
        public string AllSupplyTime_II_08 { get; set; }
        [Display(Name = "FoodNoInput_08")]
        public string FoodNoInput_08 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_09")]
        public string PartyFood_FoodName_XI_09 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_09")]
        public string PartyFood_BeginTime_XI_09 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_09")]
        public string PartyFood_EndTime_XI_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_09")]
        public string PartyFood_RestRoomTime_XI_09 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_09")]
        public string PartyFood_RestRoomFlg_XI_09 { get; set; }
        [Display(Name = "EndTimeA_09")]
        public string EndTimeA_09 { get; set; }
        [Display(Name = "EndTimeB_09")]
        public string EndTimeB_09 { get; set; }
        [Display(Name = "AllSupplyTime_I_09")]
        public string AllSupplyTime_I_09 { get; set; }
        [Display(Name = "AllSupplyTime_II_09")]
        public string AllSupplyTime_II_09 { get; set; }
        [Display(Name = "FoodNoInput_09")]
        public string FoodNoInput_09 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_10")]
        public string PartyFood_FoodName_XI_10 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_10")]
        public string PartyFood_BeginTime_XI_10 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_10")]
        public string PartyFood_EndTime_XI_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_10")]
        public string PartyFood_RestRoomTime_XI_10 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_10")]
        public string PartyFood_RestRoomFlg_XI_10 { get; set; }
        [Display(Name = "EndTimeA_10")]
        public string EndTimeA_10 { get; set; }
        [Display(Name = "EndTimeB_10")]
        public string EndTimeB_10 { get; set; }
        [Display(Name = "AllSupplyTime_I_10")]
        public string AllSupplyTime_I_10 { get; set; }
        [Display(Name = "AllSupplyTime_II_10")]
        public string AllSupplyTime_II_10 { get; set; }
        [Display(Name = "FoodNoInput_10")]
        public string FoodNoInput_10 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_11")]
        public string PartyFood_FoodName_XI_11 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_11")]
        public string PartyFood_BeginTime_XI_11 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_11")]
        public string PartyFood_EndTime_XI_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_11")]
        public string PartyFood_RestRoomTime_XI_11 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_11")]
        public string PartyFood_RestRoomFlg_XI_11 { get; set; }
        [Display(Name = "EndTimeA_11")]
        public string EndTimeA_11 { get; set; }
        [Display(Name = "EndTimeB_11")]
        public string EndTimeB_11 { get; set; }
        [Display(Name = "AllSupplyTime_I_11")]
        public string AllSupplyTime_I_11 { get; set; }
        [Display(Name = "AllSupplyTime_II_11")]
        public string AllSupplyTime_II_11 { get; set; }
        [Display(Name = "FoodNoInput_11")]
        public string FoodNoInput_11 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_12")]
        public string PartyFood_FoodName_XI_12 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_12")]
        public string PartyFood_BeginTime_XI_12 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_12")]
        public string PartyFood_EndTime_XI_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_12")]
        public string PartyFood_RestRoomTime_XI_12 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_12")]
        public string PartyFood_RestRoomFlg_XI_12 { get; set; }
        [Display(Name = "EndTimeA_12")]
        public string EndTimeA_12 { get; set; }
        [Display(Name = "EndTimeB_12")]
        public string EndTimeB_12 { get; set; }
        [Display(Name = "AllSupplyTime_I_12")]
        public string AllSupplyTime_I_12 { get; set; }
        [Display(Name = "AllSupplyTime_II_12")]
        public string AllSupplyTime_II_12 { get; set; }
        [Display(Name = "FoodNoInput_12")]
        public string FoodNoInput_12 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_13")]
        public string PartyFood_FoodName_XI_13 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_13")]
        public string PartyFood_BeginTime_XI_13 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_13")]
        public string PartyFood_EndTime_XI_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_13")]
        public string PartyFood_RestRoomTime_XI_13 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_13")]
        public string PartyFood_RestRoomFlg_XI_13 { get; set; }
        [Display(Name = "EndTimeA_13")]
        public string EndTimeA_13 { get; set; }
        [Display(Name = "EndTimeB_13")]
        public string EndTimeB_13 { get; set; }
        [Display(Name = "AllSupplyTime_I_13")]
        public string AllSupplyTime_I_13 { get; set; }
        [Display(Name = "AllSupplyTime_II_13")]
        public string AllSupplyTime_II_13 { get; set; }
        [Display(Name = "FoodNoInput_13")]
        public string FoodNoInput_13 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_14")]
        public string PartyFood_FoodName_XI_14 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_14")]
        public string PartyFood_BeginTime_XI_14 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_14")]
        public string PartyFood_EndTime_XI_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_14")]
        public string PartyFood_RestRoomTime_XI_14 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_14")]
        public string PartyFood_RestRoomFlg_XI_14 { get; set; }
        [Display(Name = "EndTimeA_14")]
        public string EndTimeA_14 { get; set; }
        [Display(Name = "EndTimeB_14")]
        public string EndTimeB_14 { get; set; }
        [Display(Name = "AllSupplyTime_I_14")]
        public string AllSupplyTime_I_14 { get; set; }
        [Display(Name = "AllSupplyTime_II_14")]
        public string AllSupplyTime_II_14 { get; set; }
        [Display(Name = "FoodNoInput_14")]
        public string FoodNoInput_14 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_15")]
        public string PartyFood_FoodName_XI_15 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_15")]
        public string PartyFood_BeginTime_XI_15 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_15")]
        public string PartyFood_EndTime_XI_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_15")]
        public string PartyFood_RestRoomTime_XI_15 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_15")]
        public string PartyFood_RestRoomFlg_XI_15 { get; set; }
        [Display(Name = "EndTimeA_15")]
        public string EndTimeA_15 { get; set; }
        [Display(Name = "EndTimeB_15")]
        public string EndTimeB_15 { get; set; }
        [Display(Name = "AllSupplyTime_I_15")]
        public string AllSupplyTime_I_15 { get; set; }
        [Display(Name = "AllSupplyTime_II_15")]
        public string AllSupplyTime_II_15 { get; set; }
        [Display(Name = "FoodNoInput_15")]
        public string FoodNoInput_15 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_16")]
        public string PartyFood_FoodName_XI_16 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_16")]
        public string PartyFood_BeginTime_XI_16 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_16")]
        public string PartyFood_EndTime_XI_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_16")]
        public string PartyFood_RestRoomTime_XI_16 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_16")]
        public string PartyFood_RestRoomFlg_XI_16 { get; set; }
        [Display(Name = "EndTimeA_16")]
        public string EndTimeA_16 { get; set; }
        [Display(Name = "EndTimeB_16")]
        public string EndTimeB_16 { get; set; }
        [Display(Name = "AllSupplyTime_I_16")]
        public string AllSupplyTime_I_16 { get; set; }
        [Display(Name = "AllSupplyTime_II_16")]
        public string AllSupplyTime_II_16 { get; set; }
        [Display(Name = "FoodNoInput_16")]
        public string FoodNoInput_16 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_17")]
        public string PartyFood_FoodName_XI_17 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_17")]
        public string PartyFood_BeginTime_XI_17 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_17")]
        public string PartyFood_EndTime_XI_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_17")]
        public string PartyFood_RestRoomTime_XI_17 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_17")]
        public string PartyFood_RestRoomFlg_XI_17 { get; set; }
        [Display(Name = "EndTimeA_17")]
        public string EndTimeA_17 { get; set; }
        [Display(Name = "EndTimeB_17")]
        public string EndTimeB_17 { get; set; }
        [Display(Name = "AllSupplyTime_I_17")]
        public string AllSupplyTime_I_17 { get; set; }
        [Display(Name = "AllSupplyTime_II_17")]
        public string AllSupplyTime_II_17 { get; set; }
        [Display(Name = "FoodNoInput_17")]
        public string FoodNoInput_17 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_18")]
        public string PartyFood_FoodName_XI_18 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_18")]
        public string PartyFood_BeginTime_XI_18 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_18")]
        public string PartyFood_EndTime_XI_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_18")]
        public string PartyFood_RestRoomTime_XI_18 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_18")]
        public string PartyFood_RestRoomFlg_XI_18 { get; set; }
        [Display(Name = "EndTimeA_18")]
        public string EndTimeA_18 { get; set; }
        [Display(Name = "EndTimeB_18")]
        public string EndTimeB_18 { get; set; }
        [Display(Name = "AllSupplyTime_I_18")]
        public string AllSupplyTime_I_18 { get; set; }
        [Display(Name = "AllSupplyTime_II_18")]
        public string AllSupplyTime_II_18 { get; set; }
        [Display(Name = "FoodNoInput_18")]
        public string FoodNoInput_18 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_19")]
        public string PartyFood_FoodName_XI_19 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_19")]
        public string PartyFood_BeginTime_XI_19 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_19")]
        public string PartyFood_EndTime_XI_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_19")]
        public string PartyFood_RestRoomTime_XI_19 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_19")]
        public string PartyFood_RestRoomFlg_XI_19 { get; set; }
        [Display(Name = "EndTimeA_19")]
        public string EndTimeA_19 { get; set; }
        [Display(Name = "EndTimeB_19")]
        public string EndTimeB_19 { get; set; }
        [Display(Name = "AllSupplyTime_I_19")]
        public string AllSupplyTime_I_19 { get; set; }
        [Display(Name = "AllSupplyTime_II_19")]
        public string AllSupplyTime_II_19 { get; set; }
        [Display(Name = "FoodNoInput_19")]
        public string FoodNoInput_19 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_20")]
        public string PartyFood_FoodName_XI_20 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_20")]
        public string PartyFood_BeginTime_XI_20 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_20")]
        public string PartyFood_EndTime_XI_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_20")]
        public string PartyFood_RestRoomTime_XI_20 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_20")]
        public string PartyFood_RestRoomFlg_XI_20 { get; set; }
        [Display(Name = "EndTimeA_20")]
        public string EndTimeA_20 { get; set; }
        [Display(Name = "EndTimeB_20")]
        public string EndTimeB_20 { get; set; }
        [Display(Name = "AllSupplyTime_I_20")]
        public string AllSupplyTime_I_20 { get; set; }
        [Display(Name = "AllSupplyTime_II_20")]
        public string AllSupplyTime_II_20 { get; set; }
        [Display(Name = "FoodNoInput_20")]
        public string FoodNoInput_20 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_21")]
        public string PartyFood_FoodName_XI_21 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_21")]
        public string PartyFood_BeginTime_XI_21 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_21")]
        public string PartyFood_EndTime_XI_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_21")]
        public string PartyFood_RestRoomTime_XI_21 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_21")]
        public string PartyFood_RestRoomFlg_XI_21 { get; set; }
        [Display(Name = "EndTimeA_21")]
        public string EndTimeA_21 { get; set; }
        [Display(Name = "EndTimeB_21")]
        public string EndTimeB_21 { get; set; }
        [Display(Name = "AllSupplyTime_I_21")]
        public string AllSupplyTime_I_21 { get; set; }
        [Display(Name = "AllSupplyTime_II_21")]
        public string AllSupplyTime_II_21 { get; set; }
        [Display(Name = "FoodNoInput_21")]
        public string FoodNoInput_21 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_22")]
        public string PartyFood_FoodName_XI_22 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_22")]
        public string PartyFood_BeginTime_XI_22 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_22")]
        public string PartyFood_EndTime_XI_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_22")]
        public string PartyFood_RestRoomTime_XI_22 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_22")]
        public string PartyFood_RestRoomFlg_XI_22 { get; set; }
        [Display(Name = "EndTimeA_22")]
        public string EndTimeA_22 { get; set; }
        [Display(Name = "EndTimeB_22")]
        public string EndTimeB_22 { get; set; }
        [Display(Name = "AllSupplyTime_I_22")]
        public string AllSupplyTime_I_22 { get; set; }
        [Display(Name = "AllSupplyTime_II_22")]
        public string AllSupplyTime_II_22 { get; set; }
        [Display(Name = "FoodNoInput_22")]
        public string FoodNoInput_22 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_23")]
        public string PartyFood_FoodName_XI_23 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_23")]
        public string PartyFood_BeginTime_XI_23 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_23")]
        public string PartyFood_EndTime_XI_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_23")]
        public string PartyFood_RestRoomTime_XI_23 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_23")]
        public string PartyFood_RestRoomFlg_XI_23 { get; set; }
        [Display(Name = "EndTimeA_23")]
        public string EndTimeA_23 { get; set; }
        [Display(Name = "EndTimeB_23")]
        public string EndTimeB_23 { get; set; }
        [Display(Name = "AllSupplyTime_I_23")]
        public string AllSupplyTime_I_23 { get; set; }
        [Display(Name = "AllSupplyTime_II_23")]
        public string AllSupplyTime_II_23 { get; set; }
        [Display(Name = "FoodNoInput_23")]
        public string FoodNoInput_23 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_24")]
        public string PartyFood_FoodName_XI_24 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_24")]
        public string PartyFood_BeginTime_XI_24 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_24")]
        public string PartyFood_EndTime_XI_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_24")]
        public string PartyFood_RestRoomTime_XI_24 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_24")]
        public string PartyFood_RestRoomFlg_XI_24 { get; set; }
        [Display(Name = "EndTimeA_24")]
        public string EndTimeA_24 { get; set; }
        [Display(Name = "EndTimeB_24")]
        public string EndTimeB_24 { get; set; }
        [Display(Name = "AllSupplyTime_I_24")]
        public string AllSupplyTime_I_24 { get; set; }
        [Display(Name = "AllSupplyTime_II_24")]
        public string AllSupplyTime_II_24 { get; set; }
        [Display(Name = "FoodNoInput_24")]
        public string FoodNoInput_24 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_25")]
        public string PartyFood_FoodName_XI_25 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_25")]
        public string PartyFood_BeginTime_XI_25 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_25")]
        public string PartyFood_EndTime_XI_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_25")]
        public string PartyFood_RestRoomTime_XI_25 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_25")]
        public string PartyFood_RestRoomFlg_XI_25 { get; set; }
        [Display(Name = "EndTimeA_25")]
        public string EndTimeA_25 { get; set; }
        [Display(Name = "EndTimeB_25")]
        public string EndTimeB_25 { get; set; }
        [Display(Name = "AllSupplyTime_I_25")]
        public string AllSupplyTime_I_25 { get; set; }
        [Display(Name = "AllSupplyTime_II_25")]
        public string AllSupplyTime_II_25 { get; set; }
        [Display(Name = "FoodNoInput_25")]
        public string FoodNoInput_25 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_26")]
        public string PartyFood_FoodName_XI_26 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_26")]
        public string PartyFood_BeginTime_XI_26 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_26")]
        public string PartyFood_EndTime_XI_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_26")]
        public string PartyFood_RestRoomTime_XI_26 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_26")]
        public string PartyFood_RestRoomFlg_XI_26 { get; set; }
        [Display(Name = "EndTimeA_26")]
        public string EndTimeA_26 { get; set; }
        [Display(Name = "EndTimeB_26")]
        public string EndTimeB_26 { get; set; }
        [Display(Name = "AllSupplyTime_I_26")]
        public string AllSupplyTime_I_26 { get; set; }
        [Display(Name = "AllSupplyTime_II_26")]
        public string AllSupplyTime_II_26 { get; set; }
        [Display(Name = "FoodNoInput_26")]
        public string FoodNoInput_26 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_27")]
        public string PartyFood_FoodName_XI_27 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_27")]
        public string PartyFood_BeginTime_XI_27 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_27")]
        public string PartyFood_EndTime_XI_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_27")]
        public string PartyFood_RestRoomTime_XI_27 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_27")]
        public string PartyFood_RestRoomFlg_XI_27 { get; set; }
        [Display(Name = "EndTimeA_27")]
        public string EndTimeA_27 { get; set; }
        [Display(Name = "EndTimeB_27")]
        public string EndTimeB_27 { get; set; }
        [Display(Name = "AllSupplyTime_I_27")]
        public string AllSupplyTime_I_27 { get; set; }
        [Display(Name = "AllSupplyTime_II_27")]
        public string AllSupplyTime_II_27 { get; set; }
        [Display(Name = "FoodNoInput_27")]
        public string FoodNoInput_27 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_28")]
        public string PartyFood_FoodName_XI_28 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_28")]
        public string PartyFood_BeginTime_XI_28 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_28")]
        public string PartyFood_EndTime_XI_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_28")]
        public string PartyFood_RestRoomTime_XI_28 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_28")]
        public string PartyFood_RestRoomFlg_XI_28 { get; set; }
        [Display(Name = "EndTimeA_28")]
        public string EndTimeA_28 { get; set; }
        [Display(Name = "EndTimeB_28")]
        public string EndTimeB_28 { get; set; }
        [Display(Name = "AllSupplyTime_I_28")]
        public string AllSupplyTime_I_28 { get; set; }
        [Display(Name = "AllSupplyTime_II_28")]
        public string AllSupplyTime_II_28 { get; set; }
        [Display(Name = "FoodNoInput_28")]
        public string FoodNoInput_28 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_29")]
        public string PartyFood_FoodName_XI_29 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_29")]
        public string PartyFood_BeginTime_XI_29 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_29")]
        public string PartyFood_EndTime_XI_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_29")]
        public string PartyFood_RestRoomTime_XI_29 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_29")]
        public string PartyFood_RestRoomFlg_XI_29 { get; set; }
        [Display(Name = "EndTimeA_29")]
        public string EndTimeA_29 { get; set; }
        [Display(Name = "EndTimeB_29")]
        public string EndTimeB_29 { get; set; }
        [Display(Name = "AllSupplyTime_I_29")]
        public string AllSupplyTime_I_29 { get; set; }
        [Display(Name = "AllSupplyTime_II_29")]
        public string AllSupplyTime_II_29 { get; set; }
        [Display(Name = "FoodNoInput_29")]
        public string FoodNoInput_29 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_30")]
        public string PartyFood_FoodName_XI_30 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_30")]
        public string PartyFood_BeginTime_XI_30 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_30")]
        public string PartyFood_EndTime_XI_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_30")]
        public string PartyFood_RestRoomTime_XI_30 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_30")]
        public string PartyFood_RestRoomFlg_XI_30 { get; set; }
        [Display(Name = "EndTimeA_30")]
        public string EndTimeA_30 { get; set; }
        [Display(Name = "EndTimeB_30")]
        public string EndTimeB_30 { get; set; }
        [Display(Name = "AllSupplyTime_I_30")]
        public string AllSupplyTime_I_30 { get; set; }
        [Display(Name = "AllSupplyTime_II_30")]
        public string AllSupplyTime_II_30 { get; set; }
        [Display(Name = "FoodNoInput_30")]
        public string FoodNoInput_30 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_31")]
        public string PartyFood_FoodName_XI_31 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_31")]
        public string PartyFood_BeginTime_XI_31 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_31")]
        public string PartyFood_EndTime_XI_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_31")]
        public string PartyFood_RestRoomTime_XI_31 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_31")]
        public string PartyFood_RestRoomFlg_XI_31 { get; set; }
        [Display(Name = "EndTimeA_31")]
        public string EndTimeA_31 { get; set; }
        [Display(Name = "EndTimeB_31")]
        public string EndTimeB_31 { get; set; }
        [Display(Name = "AllSupplyTime_I_31")]
        public string AllSupplyTime_I_31 { get; set; }
        [Display(Name = "AllSupplyTime_II_31")]
        public string AllSupplyTime_II_31 { get; set; }
        [Display(Name = "FoodNoInput_31")]
        public string FoodNoInput_31 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_32")]
        public string PartyFood_FoodName_XI_32 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_32")]
        public string PartyFood_BeginTime_XI_32 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_32")]
        public string PartyFood_EndTime_XI_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_32")]
        public string PartyFood_RestRoomTime_XI_32 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_32")]
        public string PartyFood_RestRoomFlg_XI_32 { get; set; }
        [Display(Name = "EndTimeA_32")]
        public string EndTimeA_32 { get; set; }
        [Display(Name = "EndTimeB_32")]
        public string EndTimeB_32 { get; set; }
        [Display(Name = "AllSupplyTime_I_32")]
        public string AllSupplyTime_I_32 { get; set; }
        [Display(Name = "AllSupplyTime_II_32")]
        public string AllSupplyTime_II_32 { get; set; }
        [Display(Name = "FoodNoInput_32")]
        public string FoodNoInput_32 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_33")]
        public string PartyFood_FoodName_XI_33 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_33")]
        public string PartyFood_BeginTime_XI_33 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_33")]
        public string PartyFood_EndTime_XI_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_33")]
        public string PartyFood_RestRoomTime_XI_33 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_33")]
        public string PartyFood_RestRoomFlg_XI_33 { get; set; }
        [Display(Name = "EndTimeA_33")]
        public string EndTimeA_33 { get; set; }
        [Display(Name = "EndTimeB_33")]
        public string EndTimeB_33 { get; set; }
        [Display(Name = "AllSupplyTime_I_33")]
        public string AllSupplyTime_I_33 { get; set; }
        [Display(Name = "AllSupplyTime_II_33")]
        public string AllSupplyTime_II_33 { get; set; }
        [Display(Name = "FoodNoInput_33")]
        public string FoodNoInput_33 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_34")]
        public string PartyFood_FoodName_XI_34 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_34")]
        public string PartyFood_BeginTime_XI_34 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_34")]
        public string PartyFood_EndTime_XI_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_34")]
        public string PartyFood_RestRoomTime_XI_34 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_34")]
        public string PartyFood_RestRoomFlg_XI_34 { get; set; }
        [Display(Name = "EndTimeA_34")]
        public string EndTimeA_34 { get; set; }
        [Display(Name = "EndTimeB_34")]
        public string EndTimeB_34 { get; set; }
        [Display(Name = "AllSupplyTime_I_34")]
        public string AllSupplyTime_I_34 { get; set; }
        [Display(Name = "AllSupplyTime_II_34")]
        public string AllSupplyTime_II_34 { get; set; }
        [Display(Name = "FoodNoInput_34")]
        public string FoodNoInput_34 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_35")]
        public string PartyFood_FoodName_XI_35 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_35")]
        public string PartyFood_BeginTime_XI_35 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_35")]
        public string PartyFood_EndTime_XI_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_35")]
        public string PartyFood_RestRoomTime_XI_35 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_35")]
        public string PartyFood_RestRoomFlg_XI_35 { get; set; }
        [Display(Name = "EndTimeA_35")]
        public string EndTimeA_35 { get; set; }
        [Display(Name = "EndTimeB_35")]
        public string EndTimeB_35 { get; set; }
        [Display(Name = "AllSupplyTime_I_35")]
        public string AllSupplyTime_I_35 { get; set; }
        [Display(Name = "AllSupplyTime_II_35")]
        public string AllSupplyTime_II_35 { get; set; }
        [Display(Name = "FoodNoInput_35")]
        public string FoodNoInput_35 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_36")]
        public string PartyFood_FoodName_XI_36 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_36")]
        public string PartyFood_BeginTime_XI_36 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_36")]
        public string PartyFood_EndTime_XI_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_36")]
        public string PartyFood_RestRoomTime_XI_36 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_36")]
        public string PartyFood_RestRoomFlg_XI_36 { get; set; }
        [Display(Name = "EndTimeA_36")]
        public string EndTimeA_36 { get; set; }
        [Display(Name = "EndTimeB_36")]
        public string EndTimeB_36 { get; set; }
        [Display(Name = "AllSupplyTime_I_36")]
        public string AllSupplyTime_I_36 { get; set; }
        [Display(Name = "AllSupplyTime_II_36")]
        public string AllSupplyTime_II_36 { get; set; }
        [Display(Name = "FoodNoInput_36")]
        public string FoodNoInput_36 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_37")]
        public string PartyFood_FoodName_XI_37 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_37")]
        public string PartyFood_BeginTime_XI_37 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_37")]
        public string PartyFood_EndTime_XI_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_37")]
        public string PartyFood_RestRoomTime_XI_37 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_37")]
        public string PartyFood_RestRoomFlg_XI_37 { get; set; }
        [Display(Name = "EndTimeA_37")]
        public string EndTimeA_37 { get; set; }
        [Display(Name = "EndTimeB_37")]
        public string EndTimeB_37 { get; set; }
        [Display(Name = "AllSupplyTime_I_37")]
        public string AllSupplyTime_I_37 { get; set; }
        [Display(Name = "AllSupplyTime_II_37")]
        public string AllSupplyTime_II_37 { get; set; }
        [Display(Name = "FoodNoInput_37")]
        public string FoodNoInput_37 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_38")]
        public string PartyFood_FoodName_XI_38 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_38")]
        public string PartyFood_BeginTime_XI_38 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_38")]
        public string PartyFood_EndTime_XI_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_38")]
        public string PartyFood_RestRoomTime_XI_38 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_38")]
        public string PartyFood_RestRoomFlg_XI_38 { get; set; }
        [Display(Name = "EndTimeA_38")]
        public string EndTimeA_38 { get; set; }
        [Display(Name = "EndTimeB_38")]
        public string EndTimeB_38 { get; set; }
        [Display(Name = "AllSupplyTime_I_38")]
        public string AllSupplyTime_I_38 { get; set; }
        [Display(Name = "AllSupplyTime_II_38")]
        public string AllSupplyTime_II_38 { get; set; }
        [Display(Name = "FoodNoInput_38")]
        public string FoodNoInput_38 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_39")]
        public string PartyFood_FoodName_XI_39 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_39")]
        public string PartyFood_BeginTime_XI_39 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_39")]
        public string PartyFood_EndTime_XI_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_39")]
        public string PartyFood_RestRoomTime_XI_39 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_39")]
        public string PartyFood_RestRoomFlg_XI_39 { get; set; }
        [Display(Name = "EndTimeA_39")]
        public string EndTimeA_39 { get; set; }
        [Display(Name = "EndTimeB_39")]
        public string EndTimeB_39 { get; set; }
        [Display(Name = "AllSupplyTime_I_39")]
        public string AllSupplyTime_I_39 { get; set; }
        [Display(Name = "AllSupplyTime_II_39")]
        public string AllSupplyTime_II_39 { get; set; }
        [Display(Name = "FoodNoInput_39")]
        public string FoodNoInput_39 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_40")]
        public string PartyFood_FoodName_XI_40 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_40")]
        public string PartyFood_BeginTime_XI_40 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_40")]
        public string PartyFood_EndTime_XI_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_40")]
        public string PartyFood_RestRoomTime_XI_40 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_40")]
        public string PartyFood_RestRoomFlg_XI_40 { get; set; }
        [Display(Name = "EndTimeA_40")]
        public string EndTimeA_40 { get; set; }
        [Display(Name = "EndTimeB_40")]
        public string EndTimeB_40 { get; set; }
        [Display(Name = "AllSupplyTime_I_40")]
        public string AllSupplyTime_I_40 { get; set; }
        [Display(Name = "AllSupplyTime_II_40")]
        public string AllSupplyTime_II_40 { get; set; }
        [Display(Name = "FoodNoInput_40")]
        public string FoodNoInput_40 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_41")]
        public string PartyFood_FoodName_XI_41 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_41")]
        public string PartyFood_BeginTime_XI_41 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_41")]
        public string PartyFood_EndTime_XI_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_41")]
        public string PartyFood_RestRoomTime_XI_41 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_41")]
        public string PartyFood_RestRoomFlg_XI_41 { get; set; }
        [Display(Name = "EndTimeA_41")]
        public string EndTimeA_41 { get; set; }
        [Display(Name = "EndTimeB_41")]
        public string EndTimeB_41 { get; set; }
        [Display(Name = "AllSupplyTime_I_41")]
        public string AllSupplyTime_I_41 { get; set; }
        [Display(Name = "AllSupplyTime_II_41")]
        public string AllSupplyTime_II_41 { get; set; }
        [Display(Name = "FoodNoInput_41")]
        public string FoodNoInput_41 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_42")]
        public string PartyFood_FoodName_XI_42 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_42")]
        public string PartyFood_BeginTime_XI_42 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_42")]
        public string PartyFood_EndTime_XI_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_42")]
        public string PartyFood_RestRoomTime_XI_42 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_42")]
        public string PartyFood_RestRoomFlg_XI_42 { get; set; }
        [Display(Name = "EndTimeA_42")]
        public string EndTimeA_42 { get; set; }
        [Display(Name = "EndTimeB_42")]
        public string EndTimeB_42 { get; set; }
        [Display(Name = "AllSupplyTime_I_42")]
        public string AllSupplyTime_I_42 { get; set; }
        [Display(Name = "AllSupplyTime_II_42")]
        public string AllSupplyTime_II_42 { get; set; }
        [Display(Name = "FoodNoInput_42")]
        public string FoodNoInput_42 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_43")]
        public string PartyFood_FoodName_XI_43 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_43")]
        public string PartyFood_BeginTime_XI_43 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_43")]
        public string PartyFood_EndTime_XI_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_43")]
        public string PartyFood_RestRoomTime_XI_43 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_43")]
        public string PartyFood_RestRoomFlg_XI_43 { get; set; }
        [Display(Name = "EndTimeA_43")]
        public string EndTimeA_43 { get; set; }
        [Display(Name = "EndTimeB_43")]
        public string EndTimeB_43 { get; set; }
        [Display(Name = "AllSupplyTime_I_43")]
        public string AllSupplyTime_I_43 { get; set; }
        [Display(Name = "AllSupplyTime_II_43")]
        public string AllSupplyTime_II_43 { get; set; }
        [Display(Name = "FoodNoInput_43")]
        public string FoodNoInput_43 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_44")]
        public string PartyFood_FoodName_XI_44 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_44")]
        public string PartyFood_BeginTime_XI_44 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_44")]
        public string PartyFood_EndTime_XI_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_44")]
        public string PartyFood_RestRoomTime_XI_44 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_44")]
        public string PartyFood_RestRoomFlg_XI_44 { get; set; }
        [Display(Name = "EndTimeA_44")]
        public string EndTimeA_44 { get; set; }
        [Display(Name = "EndTimeB_44")]
        public string EndTimeB_44 { get; set; }
        [Display(Name = "AllSupplyTime_I_44")]
        public string AllSupplyTime_I_44 { get; set; }
        [Display(Name = "AllSupplyTime_II_44")]
        public string AllSupplyTime_II_44 { get; set; }
        [Display(Name = "FoodNoInput_44")]
        public string FoodNoInput_44 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_45")]
        public string PartyFood_FoodName_XI_45 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_45")]
        public string PartyFood_BeginTime_XI_45 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_45")]
        public string PartyFood_EndTime_XI_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_45")]
        public string PartyFood_RestRoomTime_XI_45 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_45")]
        public string PartyFood_RestRoomFlg_XI_45 { get; set; }
        [Display(Name = "EndTimeA_45")]
        public string EndTimeA_45 { get; set; }
        [Display(Name = "EndTimeB_45")]
        public string EndTimeB_45 { get; set; }
        [Display(Name = "AllSupplyTime_I_45")]
        public string AllSupplyTime_I_45 { get; set; }
        [Display(Name = "AllSupplyTime_II_45")]
        public string AllSupplyTime_II_45 { get; set; }
        [Display(Name = "FoodNoInput_45")]
        public string FoodNoInput_45 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_46")]
        public string PartyFood_FoodName_XI_46 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_46")]
        public string PartyFood_BeginTime_XI_46 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_46")]
        public string PartyFood_EndTime_XI_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_46")]
        public string PartyFood_RestRoomTime_XI_46 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_46")]
        public string PartyFood_RestRoomFlg_XI_46 { get; set; }
        [Display(Name = "EndTimeA_46")]
        public string EndTimeA_46 { get; set; }
        [Display(Name = "EndTimeB_46")]
        public string EndTimeB_46 { get; set; }
        [Display(Name = "AllSupplyTime_I_46")]
        public string AllSupplyTime_I_46 { get; set; }
        [Display(Name = "AllSupplyTime_II_46")]
        public string AllSupplyTime_II_46 { get; set; }
        [Display(Name = "FoodNoInput_46")]
        public string FoodNoInput_46 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_47")]
        public string PartyFood_FoodName_XI_47 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_47")]
        public string PartyFood_BeginTime_XI_47 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_47")]
        public string PartyFood_EndTime_XI_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_47")]
        public string PartyFood_RestRoomTime_XI_47 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_47")]
        public string PartyFood_RestRoomFlg_XI_47 { get; set; }
        [Display(Name = "EndTimeA_47")]
        public string EndTimeA_47 { get; set; }
        [Display(Name = "EndTimeB_47")]
        public string EndTimeB_47 { get; set; }
        [Display(Name = "AllSupplyTime_I_47")]
        public string AllSupplyTime_I_47 { get; set; }
        [Display(Name = "AllSupplyTime_II_47")]
        public string AllSupplyTime_II_47 { get; set; }
        [Display(Name = "FoodNoInput_47")]
        public string FoodNoInput_47 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_48")]
        public string PartyFood_FoodName_XI_48 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_48")]
        public string PartyFood_BeginTime_XI_48 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_48")]
        public string PartyFood_EndTime_XI_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_48")]
        public string PartyFood_RestRoomTime_XI_48 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_48")]
        public string PartyFood_RestRoomFlg_XI_48 { get; set; }
        [Display(Name = "EndTimeA_48")]
        public string EndTimeA_48 { get; set; }
        [Display(Name = "EndTimeB_48")]
        public string EndTimeB_48 { get; set; }
        [Display(Name = "AllSupplyTime_I_48")]
        public string AllSupplyTime_I_48 { get; set; }
        [Display(Name = "AllSupplyTime_II_48")]
        public string AllSupplyTime_II_48 { get; set; }
        [Display(Name = "FoodNoInput_48")]
        public string FoodNoInput_48 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_49")]
        public string PartyFood_FoodName_XI_49 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_49")]
        public string PartyFood_BeginTime_XI_49 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_49")]
        public string PartyFood_EndTime_XI_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_49")]
        public string PartyFood_RestRoomTime_XI_49 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_49")]
        public string PartyFood_RestRoomFlg_XI_49 { get; set; }
        [Display(Name = "EndTimeA_49")]
        public string EndTimeA_49 { get; set; }
        [Display(Name = "EndTimeB_49")]
        public string EndTimeB_49 { get; set; }
        [Display(Name = "AllSupplyTime_I_49")]
        public string AllSupplyTime_I_49 { get; set; }
        [Display(Name = "AllSupplyTime_II_49")]
        public string AllSupplyTime_II_49 { get; set; }
        [Display(Name = "FoodNoInput_49")]
        public string FoodNoInput_49 { get; set; }
        [Display(Name = "PartyFood_FoodName_XI_50")]
        public string PartyFood_FoodName_XI_50 { get; set; }
        [Display(Name = "PartyFood_BeginTime_XI_50")]
        public string PartyFood_BeginTime_XI_50 { get; set; }
        [Display(Name = "PartyFood_EndTime_XI_50")]
        public string PartyFood_EndTime_XI_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomTime_XI_50")]
        public string PartyFood_RestRoomTime_XI_50 { get; set; }
        [Display(Name = "PartyFood_RestRoomFlg_XI_50")]
        public string PartyFood_RestRoomFlg_XI_50 { get; set; }
        [Display(Name = "EndTimeA_50")]
        public string EndTimeA_50 { get; set; }
        [Display(Name = "EndTimeB_50")]
        public string EndTimeB_50 { get; set; }
        [Display(Name = "AllSupplyTime_I_50")]
        public string AllSupplyTime_I_50 { get; set; }
        [Display(Name = "AllSupplyTime_II_50")]
        public string AllSupplyTime_II_50 { get; set; }
        [Display(Name = "FoodNoInput_50")]
        public string FoodNoInput_50 { get; set; }

        [Display(Name = "PartyTime_OrderTimeTO_II_01")]
        public string PartyTime_OrderTimeTO_II_01 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_01")]
        public string PartyTime_ActTimeTO_II_01 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_02")]
        public string PartyTime_OrderTimeTO_II_02 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_02")]
        public string PartyTime_ActTimeTO_II_02 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_03")]
        public string PartyTime_OrderTimeTO_II_03 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_03")]
        public string PartyTime_ActTimeTO_II_03 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_04")]
        public string PartyTime_OrderTimeTO_II_04 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_04")]
        public string PartyTime_ActTimeTO_II_04 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_05")]
        public string PartyTime_OrderTimeTO_II_05 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_05")]
        public string PartyTime_ActTimeTO_II_05 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_06")]
        public string PartyTime_OrderTimeTO_II_06 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_06")]
        public string PartyTime_ActTimeTO_II_06 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_07")]
        public string PartyTime_OrderTimeTO_II_07 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_07")]
        public string PartyTime_ActTimeTO_II_07 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_08")]
        public string PartyTime_OrderTimeTO_II_08 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_08")]
        public string PartyTime_ActTimeTO_II_08 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_09")]
        public string PartyTime_OrderTimeTO_II_09 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_09")]
        public string PartyTime_ActTimeTO_II_09 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_10")]
        public string PartyTime_OrderTimeTO_II_10 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_10")]
        public string PartyTime_ActTimeTO_II_10 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_11")]
        public string PartyTime_OrderTimeTO_II_11 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_11")]
        public string PartyTime_ActTimeTO_II_11 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_12")]
        public string PartyTime_OrderTimeTO_II_12 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_12")]
        public string PartyTime_ActTimeTO_II_12 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_13")]
        public string PartyTime_OrderTimeTO_II_13 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_13")]
        public string PartyTime_ActTimeTO_II_13 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_14")]
        public string PartyTime_OrderTimeTO_II_14 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_14")]
        public string PartyTime_ActTimeTO_II_14 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_15")]
        public string PartyTime_OrderTimeTO_II_15 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_15")]
        public string PartyTime_ActTimeTO_II_15 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_16")]
        public string PartyTime_OrderTimeTO_II_16 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_16")]
        public string PartyTime_ActTimeTO_II_16 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_17")]
        public string PartyTime_OrderTimeTO_II_17 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_17")]
        public string PartyTime_ActTimeTO_II_17 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_18")]
        public string PartyTime_OrderTimeTO_II_18 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_18")]
        public string PartyTime_ActTimeTO_II_18 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_19")]
        public string PartyTime_OrderTimeTO_II_19 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_19")]
        public string PartyTime_ActTimeTO_II_19 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_20")]
        public string PartyTime_OrderTimeTO_II_20 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_20")]
        public string PartyTime_ActTimeTO_II_20 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_21")]
        public string PartyTime_OrderTimeTO_II_21 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_21")]
        public string PartyTime_ActTimeTO_II_21 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_22")]
        public string PartyTime_OrderTimeTO_II_22 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_22")]
        public string PartyTime_ActTimeTO_II_22 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_23")]
        public string PartyTime_OrderTimeTO_II_23 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_23")]
        public string PartyTime_ActTimeTO_II_23 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_24")]
        public string PartyTime_OrderTimeTO_II_24 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_24")]
        public string PartyTime_ActTimeTO_II_24 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_25")]
        public string PartyTime_OrderTimeTO_II_25 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_25")]
        public string PartyTime_ActTimeTO_II_25 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_26")]
        public string PartyTime_OrderTimeTO_II_26 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_26")]
        public string PartyTime_ActTimeTO_II_26 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_27")]
        public string PartyTime_OrderTimeTO_II_27 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_27")]
        public string PartyTime_ActTimeTO_II_27 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_28")]
        public string PartyTime_OrderTimeTO_II_28 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_28")]
        public string PartyTime_ActTimeTO_II_28 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_29")]
        public string PartyTime_OrderTimeTO_II_29 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_29")]
        public string PartyTime_ActTimeTO_II_29 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_30")]
        public string PartyTime_OrderTimeTO_II_30 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_30")]
        public string PartyTime_ActTimeTO_II_30 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_31")]
        public string PartyTime_OrderTimeTO_II_31 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_31")]
        public string PartyTime_ActTimeTO_II_31 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_32")]
        public string PartyTime_OrderTimeTO_II_32 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_32")]
        public string PartyTime_ActTimeTO_II_32 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_33")]
        public string PartyTime_OrderTimeTO_II_33 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_33")]
        public string PartyTime_ActTimeTO_II_33 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_34")]
        public string PartyTime_OrderTimeTO_II_34 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_34")]
        public string PartyTime_ActTimeTO_II_34 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_35")]
        public string PartyTime_OrderTimeTO_II_35 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_35")]
        public string PartyTime_ActTimeTO_II_35 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_36")]
        public string PartyTime_OrderTimeTO_II_36 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_36")]
        public string PartyTime_ActTimeTO_II_36 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_37")]
        public string PartyTime_OrderTimeTO_II_37 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_37")]
        public string PartyTime_ActTimeTO_II_37 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_38")]
        public string PartyTime_OrderTimeTO_II_38 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_38")]
        public string PartyTime_ActTimeTO_II_38 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_39")]
        public string PartyTime_OrderTimeTO_II_39 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_39")]
        public string PartyTime_ActTimeTO_II_39 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_40")]
        public string PartyTime_OrderTimeTO_II_40 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_40")]
        public string PartyTime_ActTimeTO_II_40 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_41")]
        public string PartyTime_OrderTimeTO_II_41 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_41")]
        public string PartyTime_ActTimeTO_II_41 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_42")]
        public string PartyTime_OrderTimeTO_II_42 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_42")]
        public string PartyTime_ActTimeTO_II_42 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_43")]
        public string PartyTime_OrderTimeTO_II_43 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_43")]
        public string PartyTime_ActTimeTO_II_43 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_44")]
        public string PartyTime_OrderTimeTO_II_44 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_44")]
        public string PartyTime_ActTimeTO_II_44 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_45")]
        public string PartyTime_OrderTimeTO_II_45 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_45")]
        public string PartyTime_ActTimeTO_II_45 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_46")]
        public string PartyTime_OrderTimeTO_II_46 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_46")]
        public string PartyTime_ActTimeTO_II_46 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_47")]
        public string PartyTime_OrderTimeTO_II_47 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_47")]
        public string PartyTime_ActTimeTO_II_47 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_48")]
        public string PartyTime_OrderTimeTO_II_48 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_48")]
        public string PartyTime_ActTimeTO_II_48 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_49")]
        public string PartyTime_OrderTimeTO_II_49 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_49")]
        public string PartyTime_ActTimeTO_II_49 { get; set; }
        [Display(Name = "PartyTime_OrderTimeTO_II_50")]
        public string PartyTime_OrderTimeTO_II_50 { get; set; }

        [Display(Name = "PartyTime_ActTimeTO_II_50")]
        public string PartyTime_ActTimeTO_II_50 { get; set; }
        [Display(Name = "ShopName_01")]
        public string ShopName_01 { get; set; }
        [Display(Name = "ShopName_02")]
        public string ShopName_02 { get; set; }
        [Display(Name = "ShopName_03")]
        public string ShopName_03 { get; set; }
        [Display(Name = "ShopName_04")]
        public string ShopName_04 { get; set; }
        [Display(Name = "ShopName_05")]
        public string ShopName_05 { get; set; }
        [Display(Name = "ShopName_06")]
        public string ShopName_06 { get; set; }
        [Display(Name = "ShopName_07")]
        public string ShopName_07 { get; set; }
        [Display(Name = "ShopName_08")]
        public string ShopName_08 { get; set; }
        [Display(Name = "ShopName_09")]
        public string ShopName_09 { get; set; }
        [Display(Name = "ShopName_10")]
        public string ShopName_10 { get; set; }
        [Display(Name = "ShopName_11")]
        public string ShopName_11 { get; set; }
        [Display(Name = "ShopName_12")]
        public string ShopName_12 { get; set; }
        [Display(Name = "ShopName_13")]
        public string ShopName_13 { get; set; }
        [Display(Name = "ShopName_14")]
        public string ShopName_14 { get; set; }
        [Display(Name = "ShopName_15")]
        public string ShopName_15 { get; set; }
        [Display(Name = "ShopName_16")]
        public string ShopName_16 { get; set; }
        [Display(Name = "ShopName_17")]
        public string ShopName_17 { get; set; }
        [Display(Name = "ShopName_18")]
        public string ShopName_18 { get; set; }
        [Display(Name = "ShopName_19")]
        public string ShopName_19 { get; set; }
        [Display(Name = "ShopName_20")]
        public string ShopName_20 { get; set; }
        [Display(Name = "ShopName_21")]
        public string ShopName_21 { get; set; }
        [Display(Name = "ShopName_22")]
        public string ShopName_22 { get; set; }
        [Display(Name = "ShopName_23")]
        public string ShopName_23 { get; set; }
        [Display(Name = "ShopName_24")]
        public string ShopName_24 { get; set; }
        [Display(Name = "ShopName_25")]
        public string ShopName_25 { get; set; }
        [Display(Name = "ShopName_26")]
        public string ShopName_26 { get; set; }
        [Display(Name = "ShopName_27")]
        public string ShopName_27 { get; set; }
        [Display(Name = "ShopName_28")]
        public string ShopName_28 { get; set; }
        [Display(Name = "ShopName_29")]
        public string ShopName_29 { get; set; }
        [Display(Name = "ShopName_30")]
        public string ShopName_30 { get; set; }
        [Display(Name = "ShopName_31")]
        public string ShopName_31 { get; set; }
        [Display(Name = "ShopName_32")]
        public string ShopName_32 { get; set; }
        [Display(Name = "ShopName_33")]
        public string ShopName_33 { get; set; }
        [Display(Name = "ShopName_34")]
        public string ShopName_34 { get; set; }
        [Display(Name = "ShopName_35")]
        public string ShopName_35 { get; set; }
        [Display(Name = "ShopName_36")]
        public string ShopName_36 { get; set; }
        [Display(Name = "ShopName_37")]
        public string ShopName_37 { get; set; }
        [Display(Name = "ShopName_38")]
        public string ShopName_38 { get; set; }
        [Display(Name = "ShopName_39")]
        public string ShopName_39 { get; set; }
        [Display(Name = "ShopName_40")]
        public string ShopName_40 { get; set; }
        [Display(Name = "ShopName_41")]
        public string ShopName_41 { get; set; }
        [Display(Name = "ShopName_42")]
        public string ShopName_42 { get; set; }
        [Display(Name = "ShopName_43")]
        public string ShopName_43 { get; set; }
        [Display(Name = "ShopName_44")]
        public string ShopName_44 { get; set; }
        [Display(Name = "ShopName_45")]
        public string ShopName_45 { get; set; }
        [Display(Name = "ShopName_46")]
        public string ShopName_46 { get; set; }
        [Display(Name = "ShopName_47")]
        public string ShopName_47 { get; set; }
        [Display(Name = "ShopName_48")]
        public string ShopName_48 { get; set; }
        [Display(Name = "ShopName_49")]
        public string ShopName_49 { get; set; }
        [Display(Name = "ShopName_50")]
        public string ShopName_50 { get; set; }
        [Display(Name = "HallType_01")]
        public string HallType_01 { get; set; }
        [Display(Name = "HallType_02")]
        public string HallType_02 { get; set; }
        [Display(Name = "HallType_03")]
        public string HallType_03 { get; set; }
        [Display(Name = "HallType_04")]
        public string HallType_04 { get; set; }
        [Display(Name = "HallType_05")]
        public string HallType_05 { get; set; }
        [Display(Name = "HallType_06")]
        public string HallType_06 { get; set; }
        [Display(Name = "HallType_07")]
        public string HallType_07 { get; set; }
        [Display(Name = "HallType_08")]
        public string HallType_08 { get; set; }
        [Display(Name = "HallType_09")]
        public string HallType_09 { get; set; }
        [Display(Name = "HallType_10")]
        public string HallType_10 { get; set; }
        [Display(Name = "HallType_11")]
        public string HallType_11 { get; set; }
        [Display(Name = "HallType_12")]
        public string HallType_12 { get; set; }
        [Display(Name = "HallType_13")]
        public string HallType_13 { get; set; }
        [Display(Name = "HallType_14")]
        public string HallType_14 { get; set; }
        [Display(Name = "HallType_15")]
        public string HallType_15 { get; set; }
        [Display(Name = "HallType_16")]
        public string HallType_16 { get; set; }
        [Display(Name = "HallType_17")]
        public string HallType_17 { get; set; }
        [Display(Name = "HallType_18")]
        public string HallType_18 { get; set; }
        [Display(Name = "HallType_19")]
        public string HallType_19 { get; set; }
        [Display(Name = "HallType_20")]
        public string HallType_20 { get; set; }
        [Display(Name = "HallType_21")]
        public string HallType_21 { get; set; }
        [Display(Name = "HallType_22")]
        public string HallType_22 { get; set; }
        [Display(Name = "HallType_23")]
        public string HallType_23 { get; set; }
        [Display(Name = "HallType_24")]
        public string HallType_24 { get; set; }
        [Display(Name = "HallType_25")]
        public string HallType_25 { get; set; }
        [Display(Name = "HallType_26")]
        public string HallType_26 { get; set; }
        [Display(Name = "HallType_27")]
        public string HallType_27 { get; set; }
        [Display(Name = "HallType_28")]
        public string HallType_28 { get; set; }
        [Display(Name = "HallType_29")]
        public string HallType_29 { get; set; }
        [Display(Name = "HallType_30")]
        public string HallType_30 { get; set; }
        [Display(Name = "HallType_31")]
        public string HallType_31 { get; set; }
        [Display(Name = "HallType_32")]
        public string HallType_32 { get; set; }
        [Display(Name = "HallType_33")]
        public string HallType_33 { get; set; }
        [Display(Name = "HallType_34")]
        public string HallType_34 { get; set; }
        [Display(Name = "HallType_35")]
        public string HallType_35 { get; set; }
        [Display(Name = "HallType_36")]
        public string HallType_36 { get; set; }
        [Display(Name = "HallType_37")]
        public string HallType_37 { get; set; }
        [Display(Name = "HallType_38")]
        public string HallType_38 { get; set; }
        [Display(Name = "HallType_39")]
        public string HallType_39 { get; set; }
        [Display(Name = "HallType_40")]
        public string HallType_40 { get; set; }
        [Display(Name = "HallType_41")]
        public string HallType_41 { get; set; }
        [Display(Name = "HallType_42")]
        public string HallType_42 { get; set; }
        [Display(Name = "HallType_43")]
        public string HallType_43 { get; set; }
        [Display(Name = "HallType_44")]
        public string HallType_44 { get; set; }
        [Display(Name = "HallType_45")]
        public string HallType_45 { get; set; }
        [Display(Name = "HallType_46")]
        public string HallType_46 { get; set; }
        [Display(Name = "HallType_47")]
        public string HallType_47 { get; set; }
        [Display(Name = "HallType_48")]
        public string HallType_48 { get; set; }
        [Display(Name = "HallType_49")]
        public string HallType_49 { get; set; }
        [Display(Name = "HallType_50")]
        public string HallType_50 { get; set; }
        [Display(Name = "StartTime_01")]
        public string StartTime_01 { get; set; }
        [Display(Name = "StartTime_02")]
        public string StartTime_02 { get; set; }
        [Display(Name = "StartTime_03")]
        public string StartTime_03 { get; set; }
        [Display(Name = "StartTime_04")]
        public string StartTime_04 { get; set; }
        [Display(Name = "StartTime_05")]
        public string StartTime_05 { get; set; }
        [Display(Name = "StartTime_06")]
        public string StartTime_06 { get; set; }
        [Display(Name = "StartTime_07")]
        public string StartTime_07 { get; set; }
        [Display(Name = "StartTime_08")]
        public string StartTime_08 { get; set; }
        [Display(Name = "StartTime_09")]
        public string StartTime_09 { get; set; }
        [Display(Name = "StartTime_10")]
        public string StartTime_10 { get; set; }
        [Display(Name = "StartTime_11")]
        public string StartTime_11 { get; set; }
        [Display(Name = "StartTime_12")]
        public string StartTime_12 { get; set; }
        [Display(Name = "StartTime_13")]
        public string StartTime_13 { get; set; }
        [Display(Name = "StartTime_14")]
        public string StartTime_14 { get; set; }
        [Display(Name = "StartTime_15")]
        public string StartTime_15 { get; set; }
        [Display(Name = "StartTime_16")]
        public string StartTime_16 { get; set; }
        [Display(Name = "StartTime_17")]
        public string StartTime_17 { get; set; }
        [Display(Name = "StartTime_18")]
        public string StartTime_18 { get; set; }
        [Display(Name = "StartTime_19")]
        public string StartTime_19 { get; set; }
        [Display(Name = "StartTime_20")]
        public string StartTime_20 { get; set; }
        [Display(Name = "StartTime_21")]
        public string StartTime_21 { get; set; }
        [Display(Name = "StartTime_22")]
        public string StartTime_22 { get; set; }
        [Display(Name = "StartTime_23")]
        public string StartTime_23 { get; set; }
        [Display(Name = "StartTime_24")]
        public string StartTime_24 { get; set; }
        [Display(Name = "StartTime_25")]
        public string StartTime_25 { get; set; }
        [Display(Name = "StartTime_26")]
        public string StartTime_26 { get; set; }
        [Display(Name = "StartTime_27")]
        public string StartTime_27 { get; set; }
        [Display(Name = "StartTime_28")]
        public string StartTime_28 { get; set; }
        [Display(Name = "StartTime_29")]
        public string StartTime_29 { get; set; }
        [Display(Name = "StartTime_30")]
        public string StartTime_30 { get; set; }
        [Display(Name = "StartTime_31")]
        public string StartTime_31 { get; set; }
        [Display(Name = "StartTime_32")]
        public string StartTime_32 { get; set; }
        [Display(Name = "StartTime_33")]
        public string StartTime_33 { get; set; }
        [Display(Name = "StartTime_34")]
        public string StartTime_34 { get; set; }
        [Display(Name = "StartTime_35")]
        public string StartTime_35 { get; set; }
        [Display(Name = "StartTime_36")]
        public string StartTime_36 { get; set; }
        [Display(Name = "StartTime_37")]
        public string StartTime_37 { get; set; }
        [Display(Name = "StartTime_38")]
        public string StartTime_38 { get; set; }
        [Display(Name = "StartTime_39")]
        public string StartTime_39 { get; set; }
        [Display(Name = "StartTime_40")]
        public string StartTime_40 { get; set; }
        [Display(Name = "StartTime_41")]
        public string StartTime_41 { get; set; }
        [Display(Name = "StartTime_42")]
        public string StartTime_42 { get; set; }
        [Display(Name = "StartTime_43")]
        public string StartTime_43 { get; set; }
        [Display(Name = "StartTime_44")]
        public string StartTime_44 { get; set; }
        [Display(Name = "StartTime_45")]
        public string StartTime_45 { get; set; }
        [Display(Name = "StartTime_46")]
        public string StartTime_46 { get; set; }
        [Display(Name = "StartTime_47")]
        public string StartTime_47 { get; set; }
        [Display(Name = "StartTime_48")]
        public string StartTime_48 { get; set; }
        [Display(Name = "StartTime_49")]
        public string StartTime_49 { get; set; }
        [Display(Name = "StartTime_50")]
        public string StartTime_50 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_01")]
        public string TimeBetweenFoods_I_01 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_01")]
        public string TimeBetweenFoods_II_01 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_01")]
        public string TimeBetweenFoods_III_01 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_01")]
        public string TimeBetweenFoods_IV_01 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_01")]
        public string TimeBetweenFoods_V_01 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_01")]
        public string TimeBetweenFoods_VI_01 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_01")]
        public string TimeBetweenFoods_VII_01 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_01")]
        public string TimeBetweenFoods_VIII_01 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_01")]
        public string TimeBetweenFoods_IX_01 { get; set; }
        [Display(Name = "PartyDiv_01")]
        public string PartyDiv_01 { get; set; }
        [Display(Name = "SecondParty_01")]
        public string SecondParty_01 { get; set; }
        [Display(Name = "PartyOrdTimeRange_01")]
        public string PartyOrdTimeRange_01 { get; set; }
        [Display(Name = "PartyActTimeRange_01")]
        public string PartyActTimeRange_01 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_02")]
        public string TimeBetweenFoods_I_02 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_02")]
        public string TimeBetweenFoods_II_02 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_02")]
        public string TimeBetweenFoods_III_02 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_02")]
        public string TimeBetweenFoods_IV_02 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_02")]
        public string TimeBetweenFoods_V_02 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_02")]
        public string TimeBetweenFoods_VI_02 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_02")]
        public string TimeBetweenFoods_VII_02 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_02")]
        public string TimeBetweenFoods_VIII_02 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_02")]
        public string TimeBetweenFoods_IX_02 { get; set; }
        [Display(Name = "PartyDiv_02")]
        public string PartyDiv_02 { get; set; }
        [Display(Name = "SecondParty_02")]
        public string SecondParty_02 { get; set; }
        [Display(Name = "PartyOrdTimeRange_02")]
        public string PartyOrdTimeRange_02 { get; set; }
        [Display(Name = "PartyActTimeRange_02")]
        public string PartyActTimeRange_02 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_03")]
        public string TimeBetweenFoods_I_03 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_03")]
        public string TimeBetweenFoods_II_03 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_03")]
        public string TimeBetweenFoods_III_03 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_03")]
        public string TimeBetweenFoods_IV_03 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_03")]
        public string TimeBetweenFoods_V_03 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_03")]
        public string TimeBetweenFoods_VI_03 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_03")]
        public string TimeBetweenFoods_VII_03 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_03")]
        public string TimeBetweenFoods_VIII_03 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_03")]
        public string TimeBetweenFoods_IX_03 { get; set; }
        [Display(Name = "PartyDiv_03")]
        public string PartyDiv_03 { get; set; }
        [Display(Name = "SecondParty_03")]
        public string SecondParty_03 { get; set; }
        [Display(Name = "PartyOrdTimeRange_03")]
        public string PartyOrdTimeRange_03 { get; set; }
        [Display(Name = "PartyActTimeRange_03")]
        public string PartyActTimeRange_03 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_04")]
        public string TimeBetweenFoods_I_04 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_04")]
        public string TimeBetweenFoods_II_04 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_04")]
        public string TimeBetweenFoods_III_04 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_04")]
        public string TimeBetweenFoods_IV_04 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_04")]
        public string TimeBetweenFoods_V_04 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_04")]
        public string TimeBetweenFoods_VI_04 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_04")]
        public string TimeBetweenFoods_VII_04 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_04")]
        public string TimeBetweenFoods_VIII_04 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_04")]
        public string TimeBetweenFoods_IX_04 { get; set; }
        [Display(Name = "PartyDiv_04")]
        public string PartyDiv_04 { get; set; }
        [Display(Name = "SecondParty_04")]
        public string SecondParty_04 { get; set; }
        [Display(Name = "PartyOrdTimeRange_04")]
        public string PartyOrdTimeRange_04 { get; set; }
        [Display(Name = "PartyActTimeRange_04")]
        public string PartyActTimeRange_04 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_05")]
        public string TimeBetweenFoods_I_05 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_05")]
        public string TimeBetweenFoods_II_05 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_05")]
        public string TimeBetweenFoods_III_05 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_05")]
        public string TimeBetweenFoods_IV_05 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_05")]
        public string TimeBetweenFoods_V_05 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_05")]
        public string TimeBetweenFoods_VI_05 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_05")]
        public string TimeBetweenFoods_VII_05 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_05")]
        public string TimeBetweenFoods_VIII_05 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_05")]
        public string TimeBetweenFoods_IX_05 { get; set; }
        [Display(Name = "PartyDiv_05")]
        public string PartyDiv_05 { get; set; }
        [Display(Name = "SecondParty_05")]
        public string SecondParty_05 { get; set; }
        [Display(Name = "PartyOrdTimeRange_05")]
        public string PartyOrdTimeRange_05 { get; set; }
        [Display(Name = "PartyActTimeRange_05")]
        public string PartyActTimeRange_05 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_06")]
        public string TimeBetweenFoods_I_06 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_06")]
        public string TimeBetweenFoods_II_06 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_06")]
        public string TimeBetweenFoods_III_06 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_06")]
        public string TimeBetweenFoods_IV_06 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_06")]
        public string TimeBetweenFoods_V_06 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_06")]
        public string TimeBetweenFoods_VI_06 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_06")]
        public string TimeBetweenFoods_VII_06 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_06")]
        public string TimeBetweenFoods_VIII_06 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_06")]
        public string TimeBetweenFoods_IX_06 { get; set; }
        [Display(Name = "PartyDiv_06")]
        public string PartyDiv_06 { get; set; }
        [Display(Name = "SecondParty_06")]
        public string SecondParty_06 { get; set; }
        [Display(Name = "PartyOrdTimeRange_06")]
        public string PartyOrdTimeRange_06 { get; set; }
        [Display(Name = "PartyActTimeRange_06")]
        public string PartyActTimeRange_06 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_07")]
        public string TimeBetweenFoods_I_07 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_07")]
        public string TimeBetweenFoods_II_07 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_07")]
        public string TimeBetweenFoods_III_07 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_07")]
        public string TimeBetweenFoods_IV_07 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_07")]
        public string TimeBetweenFoods_V_07 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_07")]
        public string TimeBetweenFoods_VI_07 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_07")]
        public string TimeBetweenFoods_VII_07 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_07")]
        public string TimeBetweenFoods_VIII_07 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_07")]
        public string TimeBetweenFoods_IX_07 { get; set; }
        [Display(Name = "PartyDiv_07")]
        public string PartyDiv_07 { get; set; }
        [Display(Name = "SecondParty_07")]
        public string SecondParty_07 { get; set; }
        [Display(Name = "PartyOrdTimeRange_07")]
        public string PartyOrdTimeRange_07 { get; set; }
        [Display(Name = "PartyActTimeRange_07")]
        public string PartyActTimeRange_07 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_08")]
        public string TimeBetweenFoods_I_08 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_08")]
        public string TimeBetweenFoods_II_08 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_08")]
        public string TimeBetweenFoods_III_08 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_08")]
        public string TimeBetweenFoods_IV_08 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_08")]
        public string TimeBetweenFoods_V_08 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_08")]
        public string TimeBetweenFoods_VI_08 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_08")]
        public string TimeBetweenFoods_VII_08 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_08")]
        public string TimeBetweenFoods_VIII_08 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_08")]
        public string TimeBetweenFoods_IX_08 { get; set; }
        [Display(Name = "PartyDiv_08")]
        public string PartyDiv_08 { get; set; }
        [Display(Name = "SecondParty_08")]
        public string SecondParty_08 { get; set; }
        [Display(Name = "PartyOrdTimeRange_08")]
        public string PartyOrdTimeRange_08 { get; set; }
        [Display(Name = "PartyActTimeRange_08")]
        public string PartyActTimeRange_08 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_09")]
        public string TimeBetweenFoods_I_09 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_09")]
        public string TimeBetweenFoods_II_09 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_09")]
        public string TimeBetweenFoods_III_09 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_09")]
        public string TimeBetweenFoods_IV_09 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_09")]
        public string TimeBetweenFoods_V_09 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_09")]
        public string TimeBetweenFoods_VI_09 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_09")]
        public string TimeBetweenFoods_VII_09 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_09")]
        public string TimeBetweenFoods_VIII_09 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_09")]
        public string TimeBetweenFoods_IX_09 { get; set; }
        [Display(Name = "PartyDiv_09")]
        public string PartyDiv_09 { get; set; }
        [Display(Name = "SecondParty_09")]
        public string SecondParty_09 { get; set; }
        [Display(Name = "PartyOrdTimeRange_09")]
        public string PartyOrdTimeRange_09 { get; set; }
        [Display(Name = "PartyActTimeRange_09")]
        public string PartyActTimeRange_09 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_10")]
        public string TimeBetweenFoods_I_10 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_10")]
        public string TimeBetweenFoods_II_10 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_10")]
        public string TimeBetweenFoods_III_10 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_10")]
        public string TimeBetweenFoods_IV_10 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_10")]
        public string TimeBetweenFoods_V_10 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_10")]
        public string TimeBetweenFoods_VI_10 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_10")]
        public string TimeBetweenFoods_VII_10 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_10")]
        public string TimeBetweenFoods_VIII_10 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_10")]
        public string TimeBetweenFoods_IX_10 { get; set; }
        [Display(Name = "PartyDiv_10")]
        public string PartyDiv_10 { get; set; }
        [Display(Name = "SecondParty_10")]
        public string SecondParty_10 { get; set; }
        [Display(Name = "PartyOrdTimeRange_10")]
        public string PartyOrdTimeRange_10 { get; set; }
        [Display(Name = "PartyActTimeRange_10")]
        public string PartyActTimeRange_10 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_11")]
        public string TimeBetweenFoods_I_11 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_11")]
        public string TimeBetweenFoods_II_11 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_11")]
        public string TimeBetweenFoods_III_11 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_11")]
        public string TimeBetweenFoods_IV_11 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_11")]
        public string TimeBetweenFoods_V_11 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_11")]
        public string TimeBetweenFoods_VI_11 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_11")]
        public string TimeBetweenFoods_VII_11 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_11")]
        public string TimeBetweenFoods_VIII_11 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_11")]
        public string TimeBetweenFoods_IX_11 { get; set; }
        [Display(Name = "PartyDiv_11")]
        public string PartyDiv_11 { get; set; }
        [Display(Name = "SecondParty_11")]
        public string SecondParty_11 { get; set; }
        [Display(Name = "PartyOrdTimeRange_11")]
        public string PartyOrdTimeRange_11 { get; set; }
        [Display(Name = "PartyActTimeRange_11")]
        public string PartyActTimeRange_11 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_12")]
        public string TimeBetweenFoods_I_12 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_12")]
        public string TimeBetweenFoods_II_12 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_12")]
        public string TimeBetweenFoods_III_12 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_12")]
        public string TimeBetweenFoods_IV_12 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_12")]
        public string TimeBetweenFoods_V_12 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_12")]
        public string TimeBetweenFoods_VI_12 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_12")]
        public string TimeBetweenFoods_VII_12 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_12")]
        public string TimeBetweenFoods_VIII_12 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_12")]
        public string TimeBetweenFoods_IX_12 { get; set; }
        [Display(Name = "PartyDiv_12")]
        public string PartyDiv_12 { get; set; }
        [Display(Name = "SecondParty_12")]
        public string SecondParty_12 { get; set; }
        [Display(Name = "PartyOrdTimeRange_12")]
        public string PartyOrdTimeRange_12 { get; set; }
        [Display(Name = "PartyActTimeRange_12")]
        public string PartyActTimeRange_12 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_13")]
        public string TimeBetweenFoods_I_13 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_13")]
        public string TimeBetweenFoods_II_13 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_13")]
        public string TimeBetweenFoods_III_13 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_13")]
        public string TimeBetweenFoods_IV_13 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_13")]
        public string TimeBetweenFoods_V_13 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_13")]
        public string TimeBetweenFoods_VI_13 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_13")]
        public string TimeBetweenFoods_VII_13 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_13")]
        public string TimeBetweenFoods_VIII_13 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_13")]
        public string TimeBetweenFoods_IX_13 { get; set; }
        [Display(Name = "PartyDiv_13")]
        public string PartyDiv_13 { get; set; }
        [Display(Name = "SecondParty_13")]
        public string SecondParty_13 { get; set; }
        [Display(Name = "PartyOrdTimeRange_13")]
        public string PartyOrdTimeRange_13 { get; set; }
        [Display(Name = "PartyActTimeRange_13")]
        public string PartyActTimeRange_13 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_14")]
        public string TimeBetweenFoods_I_14 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_14")]
        public string TimeBetweenFoods_II_14 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_14")]
        public string TimeBetweenFoods_III_14 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_14")]
        public string TimeBetweenFoods_IV_14 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_14")]
        public string TimeBetweenFoods_V_14 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_14")]
        public string TimeBetweenFoods_VI_14 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_14")]
        public string TimeBetweenFoods_VII_14 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_14")]
        public string TimeBetweenFoods_VIII_14 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_14")]
        public string TimeBetweenFoods_IX_14 { get; set; }
        [Display(Name = "PartyDiv_14")]
        public string PartyDiv_14 { get; set; }
        [Display(Name = "SecondParty_14")]
        public string SecondParty_14 { get; set; }
        [Display(Name = "PartyOrdTimeRange_14")]
        public string PartyOrdTimeRange_14 { get; set; }
        [Display(Name = "PartyActTimeRange_14")]
        public string PartyActTimeRange_14 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_15")]
        public string TimeBetweenFoods_I_15 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_15")]
        public string TimeBetweenFoods_II_15 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_15")]
        public string TimeBetweenFoods_III_15 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_15")]
        public string TimeBetweenFoods_IV_15 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_15")]
        public string TimeBetweenFoods_V_15 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_15")]
        public string TimeBetweenFoods_VI_15 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_15")]
        public string TimeBetweenFoods_VII_15 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_15")]
        public string TimeBetweenFoods_VIII_15 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_15")]
        public string TimeBetweenFoods_IX_15 { get; set; }
        [Display(Name = "PartyDiv_15")]
        public string PartyDiv_15 { get; set; }
        [Display(Name = "SecondParty_15")]
        public string SecondParty_15 { get; set; }
        [Display(Name = "PartyOrdTimeRange_15")]
        public string PartyOrdTimeRange_15 { get; set; }
        [Display(Name = "PartyActTimeRange_15")]
        public string PartyActTimeRange_15 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_16")]
        public string TimeBetweenFoods_I_16 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_16")]
        public string TimeBetweenFoods_II_16 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_16")]
        public string TimeBetweenFoods_III_16 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_16")]
        public string TimeBetweenFoods_IV_16 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_16")]
        public string TimeBetweenFoods_V_16 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_16")]
        public string TimeBetweenFoods_VI_16 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_16")]
        public string TimeBetweenFoods_VII_16 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_16")]
        public string TimeBetweenFoods_VIII_16 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_16")]
        public string TimeBetweenFoods_IX_16 { get; set; }
        [Display(Name = "PartyDiv_16")]
        public string PartyDiv_16 { get; set; }
        [Display(Name = "SecondParty_16")]
        public string SecondParty_16 { get; set; }
        [Display(Name = "PartyOrdTimeRange_16")]
        public string PartyOrdTimeRange_16 { get; set; }
        [Display(Name = "PartyActTimeRange_16")]
        public string PartyActTimeRange_16 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_17")]
        public string TimeBetweenFoods_I_17 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_17")]
        public string TimeBetweenFoods_II_17 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_17")]
        public string TimeBetweenFoods_III_17 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_17")]
        public string TimeBetweenFoods_IV_17 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_17")]
        public string TimeBetweenFoods_V_17 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_17")]
        public string TimeBetweenFoods_VI_17 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_17")]
        public string TimeBetweenFoods_VII_17 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_17")]
        public string TimeBetweenFoods_VIII_17 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_17")]
        public string TimeBetweenFoods_IX_17 { get; set; }
        [Display(Name = "PartyDiv_17")]
        public string PartyDiv_17 { get; set; }
        [Display(Name = "SecondParty_17")]
        public string SecondParty_17 { get; set; }
        [Display(Name = "PartyOrdTimeRange_17")]
        public string PartyOrdTimeRange_17 { get; set; }
        [Display(Name = "PartyActTimeRange_17")]
        public string PartyActTimeRange_17 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_18")]
        public string TimeBetweenFoods_I_18 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_18")]
        public string TimeBetweenFoods_II_18 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_18")]
        public string TimeBetweenFoods_III_18 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_18")]
        public string TimeBetweenFoods_IV_18 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_18")]
        public string TimeBetweenFoods_V_18 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_18")]
        public string TimeBetweenFoods_VI_18 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_18")]
        public string TimeBetweenFoods_VII_18 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_18")]
        public string TimeBetweenFoods_VIII_18 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_18")]
        public string TimeBetweenFoods_IX_18 { get; set; }
        [Display(Name = "PartyDiv_18")]
        public string PartyDiv_18 { get; set; }
        [Display(Name = "SecondParty_18")]
        public string SecondParty_18 { get; set; }
        [Display(Name = "PartyOrdTimeRange_18")]
        public string PartyOrdTimeRange_18 { get; set; }
        [Display(Name = "PartyActTimeRange_18")]
        public string PartyActTimeRange_18 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_19")]
        public string TimeBetweenFoods_I_19 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_19")]
        public string TimeBetweenFoods_II_19 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_19")]
        public string TimeBetweenFoods_III_19 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_19")]
        public string TimeBetweenFoods_IV_19 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_19")]
        public string TimeBetweenFoods_V_19 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_19")]
        public string TimeBetweenFoods_VI_19 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_19")]
        public string TimeBetweenFoods_VII_19 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_19")]
        public string TimeBetweenFoods_VIII_19 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_19")]
        public string TimeBetweenFoods_IX_19 { get; set; }
        [Display(Name = "PartyDiv_19")]
        public string PartyDiv_19 { get; set; }
        [Display(Name = "SecondParty_19")]
        public string SecondParty_19 { get; set; }
        [Display(Name = "PartyOrdTimeRange_19")]
        public string PartyOrdTimeRange_19 { get; set; }
        [Display(Name = "PartyActTimeRange_19")]
        public string PartyActTimeRange_19 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_20")]
        public string TimeBetweenFoods_I_20 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_20")]
        public string TimeBetweenFoods_II_20 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_20")]
        public string TimeBetweenFoods_III_20 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_20")]
        public string TimeBetweenFoods_IV_20 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_20")]
        public string TimeBetweenFoods_V_20 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_20")]
        public string TimeBetweenFoods_VI_20 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_20")]
        public string TimeBetweenFoods_VII_20 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_20")]
        public string TimeBetweenFoods_VIII_20 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_20")]
        public string TimeBetweenFoods_IX_20 { get; set; }
        [Display(Name = "PartyDiv_20")]
        public string PartyDiv_20 { get; set; }
        [Display(Name = "SecondParty_20")]
        public string SecondParty_20 { get; set; }
        [Display(Name = "PartyOrdTimeRange_20")]
        public string PartyOrdTimeRange_20 { get; set; }
        [Display(Name = "PartyActTimeRange_20")]
        public string PartyActTimeRange_20 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_21")]
        public string TimeBetweenFoods_I_21 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_21")]
        public string TimeBetweenFoods_II_21 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_21")]
        public string TimeBetweenFoods_III_21 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_21")]
        public string TimeBetweenFoods_IV_21 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_21")]
        public string TimeBetweenFoods_V_21 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_21")]
        public string TimeBetweenFoods_VI_21 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_21")]
        public string TimeBetweenFoods_VII_21 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_21")]
        public string TimeBetweenFoods_VIII_21 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_21")]
        public string TimeBetweenFoods_IX_21 { get; set; }
        [Display(Name = "PartyDiv_21")]
        public string PartyDiv_21 { get; set; }
        [Display(Name = "SecondParty_21")]
        public string SecondParty_21 { get; set; }
        [Display(Name = "PartyOrdTimeRange_21")]
        public string PartyOrdTimeRange_21 { get; set; }
        [Display(Name = "PartyActTimeRange_21")]
        public string PartyActTimeRange_21 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_22")]
        public string TimeBetweenFoods_I_22 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_22")]
        public string TimeBetweenFoods_II_22 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_22")]
        public string TimeBetweenFoods_III_22 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_22")]
        public string TimeBetweenFoods_IV_22 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_22")]
        public string TimeBetweenFoods_V_22 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_22")]
        public string TimeBetweenFoods_VI_22 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_22")]
        public string TimeBetweenFoods_VII_22 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_22")]
        public string TimeBetweenFoods_VIII_22 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_22")]
        public string TimeBetweenFoods_IX_22 { get; set; }
        [Display(Name = "PartyDiv_22")]
        public string PartyDiv_22 { get; set; }
        [Display(Name = "SecondParty_22")]
        public string SecondParty_22 { get; set; }
        [Display(Name = "PartyOrdTimeRange_22")]
        public string PartyOrdTimeRange_22 { get; set; }
        [Display(Name = "PartyActTimeRange_22")]
        public string PartyActTimeRange_22 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_23")]
        public string TimeBetweenFoods_I_23 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_23")]
        public string TimeBetweenFoods_II_23 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_23")]
        public string TimeBetweenFoods_III_23 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_23")]
        public string TimeBetweenFoods_IV_23 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_23")]
        public string TimeBetweenFoods_V_23 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_23")]
        public string TimeBetweenFoods_VI_23 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_23")]
        public string TimeBetweenFoods_VII_23 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_23")]
        public string TimeBetweenFoods_VIII_23 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_23")]
        public string TimeBetweenFoods_IX_23 { get; set; }
        [Display(Name = "PartyDiv_23")]
        public string PartyDiv_23 { get; set; }
        [Display(Name = "SecondParty_23")]
        public string SecondParty_23 { get; set; }
        [Display(Name = "PartyOrdTimeRange_23")]
        public string PartyOrdTimeRange_23 { get; set; }
        [Display(Name = "PartyActTimeRange_23")]
        public string PartyActTimeRange_23 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_24")]
        public string TimeBetweenFoods_I_24 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_24")]
        public string TimeBetweenFoods_II_24 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_24")]
        public string TimeBetweenFoods_III_24 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_24")]
        public string TimeBetweenFoods_IV_24 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_24")]
        public string TimeBetweenFoods_V_24 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_24")]
        public string TimeBetweenFoods_VI_24 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_24")]
        public string TimeBetweenFoods_VII_24 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_24")]
        public string TimeBetweenFoods_VIII_24 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_24")]
        public string TimeBetweenFoods_IX_24 { get; set; }
        [Display(Name = "PartyDiv_24")]
        public string PartyDiv_24 { get; set; }
        [Display(Name = "SecondParty_24")]
        public string SecondParty_24 { get; set; }
        [Display(Name = "PartyOrdTimeRange_24")]
        public string PartyOrdTimeRange_24 { get; set; }
        [Display(Name = "PartyActTimeRange_24")]
        public string PartyActTimeRange_24 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_25")]
        public string TimeBetweenFoods_I_25 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_25")]
        public string TimeBetweenFoods_II_25 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_25")]
        public string TimeBetweenFoods_III_25 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_25")]
        public string TimeBetweenFoods_IV_25 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_25")]
        public string TimeBetweenFoods_V_25 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_25")]
        public string TimeBetweenFoods_VI_25 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_25")]
        public string TimeBetweenFoods_VII_25 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_25")]
        public string TimeBetweenFoods_VIII_25 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_25")]
        public string TimeBetweenFoods_IX_25 { get; set; }
        [Display(Name = "PartyDiv_25")]
        public string PartyDiv_25 { get; set; }
        [Display(Name = "SecondParty_25")]
        public string SecondParty_25 { get; set; }
        [Display(Name = "PartyOrdTimeRange_25")]
        public string PartyOrdTimeRange_25 { get; set; }
        [Display(Name = "PartyActTimeRange_25")]
        public string PartyActTimeRange_25 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_26")]
        public string TimeBetweenFoods_I_26 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_26")]
        public string TimeBetweenFoods_II_26 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_26")]
        public string TimeBetweenFoods_III_26 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_26")]
        public string TimeBetweenFoods_IV_26 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_26")]
        public string TimeBetweenFoods_V_26 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_26")]
        public string TimeBetweenFoods_VI_26 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_26")]
        public string TimeBetweenFoods_VII_26 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_26")]
        public string TimeBetweenFoods_VIII_26 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_26")]
        public string TimeBetweenFoods_IX_26 { get; set; }
        [Display(Name = "PartyDiv_26")]
        public string PartyDiv_26 { get; set; }
        [Display(Name = "SecondParty_26")]
        public string SecondParty_26 { get; set; }
        [Display(Name = "PartyOrdTimeRange_26")]
        public string PartyOrdTimeRange_26 { get; set; }
        [Display(Name = "PartyActTimeRange_26")]
        public string PartyActTimeRange_26 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_27")]
        public string TimeBetweenFoods_I_27 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_27")]
        public string TimeBetweenFoods_II_27 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_27")]
        public string TimeBetweenFoods_III_27 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_27")]
        public string TimeBetweenFoods_IV_27 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_27")]
        public string TimeBetweenFoods_V_27 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_27")]
        public string TimeBetweenFoods_VI_27 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_27")]
        public string TimeBetweenFoods_VII_27 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_27")]
        public string TimeBetweenFoods_VIII_27 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_27")]
        public string TimeBetweenFoods_IX_27 { get; set; }
        [Display(Name = "PartyDiv_27")]
        public string PartyDiv_27 { get; set; }
        [Display(Name = "SecondParty_27")]
        public string SecondParty_27 { get; set; }
        [Display(Name = "PartyOrdTimeRange_27")]
        public string PartyOrdTimeRange_27 { get; set; }
        [Display(Name = "PartyActTimeRange_27")]
        public string PartyActTimeRange_27 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_28")]
        public string TimeBetweenFoods_I_28 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_28")]
        public string TimeBetweenFoods_II_28 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_28")]
        public string TimeBetweenFoods_III_28 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_28")]
        public string TimeBetweenFoods_IV_28 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_28")]
        public string TimeBetweenFoods_V_28 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_28")]
        public string TimeBetweenFoods_VI_28 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_28")]
        public string TimeBetweenFoods_VII_28 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_28")]
        public string TimeBetweenFoods_VIII_28 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_28")]
        public string TimeBetweenFoods_IX_28 { get; set; }
        [Display(Name = "PartyDiv_28")]
        public string PartyDiv_28 { get; set; }
        [Display(Name = "SecondParty_28")]
        public string SecondParty_28 { get; set; }
        [Display(Name = "PartyOrdTimeRange_28")]
        public string PartyOrdTimeRange_28 { get; set; }
        [Display(Name = "PartyActTimeRange_28")]
        public string PartyActTimeRange_28 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_29")]
        public string TimeBetweenFoods_I_29 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_29")]
        public string TimeBetweenFoods_II_29 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_29")]
        public string TimeBetweenFoods_III_29 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_29")]
        public string TimeBetweenFoods_IV_29 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_29")]
        public string TimeBetweenFoods_V_29 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_29")]
        public string TimeBetweenFoods_VI_29 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_29")]
        public string TimeBetweenFoods_VII_29 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_29")]
        public string TimeBetweenFoods_VIII_29 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_29")]
        public string TimeBetweenFoods_IX_29 { get; set; }
        [Display(Name = "PartyDiv_29")]
        public string PartyDiv_29 { get; set; }
        [Display(Name = "SecondParty_29")]
        public string SecondParty_29 { get; set; }
        [Display(Name = "PartyOrdTimeRange_29")]
        public string PartyOrdTimeRange_29 { get; set; }
        [Display(Name = "PartyActTimeRange_29")]
        public string PartyActTimeRange_29 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_30")]
        public string TimeBetweenFoods_I_30 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_30")]
        public string TimeBetweenFoods_II_30 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_30")]
        public string TimeBetweenFoods_III_30 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_30")]
        public string TimeBetweenFoods_IV_30 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_30")]
        public string TimeBetweenFoods_V_30 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_30")]
        public string TimeBetweenFoods_VI_30 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_30")]
        public string TimeBetweenFoods_VII_30 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_30")]
        public string TimeBetweenFoods_VIII_30 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_30")]
        public string TimeBetweenFoods_IX_30 { get; set; }
        [Display(Name = "PartyDiv_30")]
        public string PartyDiv_30 { get; set; }
        [Display(Name = "SecondParty_30")]
        public string SecondParty_30 { get; set; }
        [Display(Name = "PartyOrdTimeRange_30")]
        public string PartyOrdTimeRange_30 { get; set; }
        [Display(Name = "PartyActTimeRange_30")]
        public string PartyActTimeRange_30 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_31")]
        public string TimeBetweenFoods_I_31 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_31")]
        public string TimeBetweenFoods_II_31 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_31")]
        public string TimeBetweenFoods_III_31 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_31")]
        public string TimeBetweenFoods_IV_31 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_31")]
        public string TimeBetweenFoods_V_31 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_31")]
        public string TimeBetweenFoods_VI_31 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_31")]
        public string TimeBetweenFoods_VII_31 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_31")]
        public string TimeBetweenFoods_VIII_31 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_31")]
        public string TimeBetweenFoods_IX_31 { get; set; }
        [Display(Name = "PartyDiv_31")]
        public string PartyDiv_31 { get; set; }
        [Display(Name = "SecondParty_31")]
        public string SecondParty_31 { get; set; }
        [Display(Name = "PartyOrdTimeRange_31")]
        public string PartyOrdTimeRange_31 { get; set; }
        [Display(Name = "PartyActTimeRange_31")]
        public string PartyActTimeRange_31 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_32")]
        public string TimeBetweenFoods_I_32 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_32")]
        public string TimeBetweenFoods_II_32 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_32")]
        public string TimeBetweenFoods_III_32 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_32")]
        public string TimeBetweenFoods_IV_32 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_32")]
        public string TimeBetweenFoods_V_32 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_32")]
        public string TimeBetweenFoods_VI_32 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_32")]
        public string TimeBetweenFoods_VII_32 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_32")]
        public string TimeBetweenFoods_VIII_32 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_32")]
        public string TimeBetweenFoods_IX_32 { get; set; }
        [Display(Name = "PartyDiv_32")]
        public string PartyDiv_32 { get; set; }
        [Display(Name = "SecondParty_32")]
        public string SecondParty_32 { get; set; }
        [Display(Name = "PartyOrdTimeRange_32")]
        public string PartyOrdTimeRange_32 { get; set; }
        [Display(Name = "PartyActTimeRange_32")]
        public string PartyActTimeRange_32 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_33")]
        public string TimeBetweenFoods_I_33 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_33")]
        public string TimeBetweenFoods_II_33 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_33")]
        public string TimeBetweenFoods_III_33 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_33")]
        public string TimeBetweenFoods_IV_33 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_33")]
        public string TimeBetweenFoods_V_33 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_33")]
        public string TimeBetweenFoods_VI_33 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_33")]
        public string TimeBetweenFoods_VII_33 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_33")]
        public string TimeBetweenFoods_VIII_33 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_33")]
        public string TimeBetweenFoods_IX_33 { get; set; }
        [Display(Name = "PartyDiv_33")]
        public string PartyDiv_33 { get; set; }
        [Display(Name = "SecondParty_33")]
        public string SecondParty_33 { get; set; }
        [Display(Name = "PartyOrdTimeRange_33")]
        public string PartyOrdTimeRange_33 { get; set; }
        [Display(Name = "PartyActTimeRange_33")]
        public string PartyActTimeRange_33 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_34")]
        public string TimeBetweenFoods_I_34 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_34")]
        public string TimeBetweenFoods_II_34 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_34")]
        public string TimeBetweenFoods_III_34 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_34")]
        public string TimeBetweenFoods_IV_34 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_34")]
        public string TimeBetweenFoods_V_34 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_34")]
        public string TimeBetweenFoods_VI_34 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_34")]
        public string TimeBetweenFoods_VII_34 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_34")]
        public string TimeBetweenFoods_VIII_34 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_34")]
        public string TimeBetweenFoods_IX_34 { get; set; }
        [Display(Name = "PartyDiv_34")]
        public string PartyDiv_34 { get; set; }
        [Display(Name = "SecondParty_34")]
        public string SecondParty_34 { get; set; }
        [Display(Name = "PartyOrdTimeRange_34")]
        public string PartyOrdTimeRange_34 { get; set; }
        [Display(Name = "PartyActTimeRange_34")]
        public string PartyActTimeRange_34 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_35")]
        public string TimeBetweenFoods_I_35 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_35")]
        public string TimeBetweenFoods_II_35 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_35")]
        public string TimeBetweenFoods_III_35 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_35")]
        public string TimeBetweenFoods_IV_35 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_35")]
        public string TimeBetweenFoods_V_35 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_35")]
        public string TimeBetweenFoods_VI_35 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_35")]
        public string TimeBetweenFoods_VII_35 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_35")]
        public string TimeBetweenFoods_VIII_35 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_35")]
        public string TimeBetweenFoods_IX_35 { get; set; }
        [Display(Name = "PartyDiv_35")]
        public string PartyDiv_35 { get; set; }
        [Display(Name = "SecondParty_35")]
        public string SecondParty_35 { get; set; }
        [Display(Name = "PartyOrdTimeRange_35")]
        public string PartyOrdTimeRange_35 { get; set; }
        [Display(Name = "PartyActTimeRange_35")]
        public string PartyActTimeRange_35 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_36")]
        public string TimeBetweenFoods_I_36 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_36")]
        public string TimeBetweenFoods_II_36 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_36")]
        public string TimeBetweenFoods_III_36 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_36")]
        public string TimeBetweenFoods_IV_36 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_36")]
        public string TimeBetweenFoods_V_36 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_36")]
        public string TimeBetweenFoods_VI_36 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_36")]
        public string TimeBetweenFoods_VII_36 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_36")]
        public string TimeBetweenFoods_VIII_36 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_36")]
        public string TimeBetweenFoods_IX_36 { get; set; }
        [Display(Name = "PartyDiv_36")]
        public string PartyDiv_36 { get; set; }
        [Display(Name = "SecondParty_36")]
        public string SecondParty_36 { get; set; }
        [Display(Name = "PartyOrdTimeRange_36")]
        public string PartyOrdTimeRange_36 { get; set; }
        [Display(Name = "PartyActTimeRange_36")]
        public string PartyActTimeRange_36 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_37")]
        public string TimeBetweenFoods_I_37 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_37")]
        public string TimeBetweenFoods_II_37 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_37")]
        public string TimeBetweenFoods_III_37 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_37")]
        public string TimeBetweenFoods_IV_37 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_37")]
        public string TimeBetweenFoods_V_37 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_37")]
        public string TimeBetweenFoods_VI_37 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_37")]
        public string TimeBetweenFoods_VII_37 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_37")]
        public string TimeBetweenFoods_VIII_37 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_37")]
        public string TimeBetweenFoods_IX_37 { get; set; }
        [Display(Name = "PartyDiv_37")]
        public string PartyDiv_37 { get; set; }
        [Display(Name = "SecondParty_37")]
        public string SecondParty_37 { get; set; }
        [Display(Name = "PartyOrdTimeRange_37")]
        public string PartyOrdTimeRange_37 { get; set; }
        [Display(Name = "PartyActTimeRange_37")]
        public string PartyActTimeRange_37 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_38")]
        public string TimeBetweenFoods_I_38 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_38")]
        public string TimeBetweenFoods_II_38 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_38")]
        public string TimeBetweenFoods_III_38 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_38")]
        public string TimeBetweenFoods_IV_38 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_38")]
        public string TimeBetweenFoods_V_38 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_38")]
        public string TimeBetweenFoods_VI_38 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_38")]
        public string TimeBetweenFoods_VII_38 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_38")]
        public string TimeBetweenFoods_VIII_38 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_38")]
        public string TimeBetweenFoods_IX_38 { get; set; }
        [Display(Name = "PartyDiv_38")]
        public string PartyDiv_38 { get; set; }
        [Display(Name = "SecondParty_38")]
        public string SecondParty_38 { get; set; }
        [Display(Name = "PartyOrdTimeRange_38")]
        public string PartyOrdTimeRange_38 { get; set; }
        [Display(Name = "PartyActTimeRange_38")]
        public string PartyActTimeRange_38 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_39")]
        public string TimeBetweenFoods_I_39 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_39")]
        public string TimeBetweenFoods_II_39 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_39")]
        public string TimeBetweenFoods_III_39 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_39")]
        public string TimeBetweenFoods_IV_39 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_39")]
        public string TimeBetweenFoods_V_39 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_39")]
        public string TimeBetweenFoods_VI_39 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_39")]
        public string TimeBetweenFoods_VII_39 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_39")]
        public string TimeBetweenFoods_VIII_39 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_39")]
        public string TimeBetweenFoods_IX_39 { get; set; }
        [Display(Name = "PartyDiv_39")]
        public string PartyDiv_39 { get; set; }
        [Display(Name = "SecondParty_39")]
        public string SecondParty_39 { get; set; }
        [Display(Name = "PartyOrdTimeRange_39")]
        public string PartyOrdTimeRange_39 { get; set; }
        [Display(Name = "PartyActTimeRange_39")]
        public string PartyActTimeRange_39 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_40")]
        public string TimeBetweenFoods_I_40 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_40")]
        public string TimeBetweenFoods_II_40 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_40")]
        public string TimeBetweenFoods_III_40 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_40")]
        public string TimeBetweenFoods_IV_40 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_40")]
        public string TimeBetweenFoods_V_40 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_40")]
        public string TimeBetweenFoods_VI_40 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_40")]
        public string TimeBetweenFoods_VII_40 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_40")]
        public string TimeBetweenFoods_VIII_40 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_40")]
        public string TimeBetweenFoods_IX_40 { get; set; }
        [Display(Name = "PartyDiv_40")]
        public string PartyDiv_40 { get; set; }
        [Display(Name = "SecondParty_40")]
        public string SecondParty_40 { get; set; }
        [Display(Name = "PartyOrdTimeRange_40")]
        public string PartyOrdTimeRange_40 { get; set; }
        [Display(Name = "PartyActTimeRange_40")]
        public string PartyActTimeRange_40 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_41")]
        public string TimeBetweenFoods_I_41 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_41")]
        public string TimeBetweenFoods_II_41 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_41")]
        public string TimeBetweenFoods_III_41 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_41")]
        public string TimeBetweenFoods_IV_41 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_41")]
        public string TimeBetweenFoods_V_41 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_41")]
        public string TimeBetweenFoods_VI_41 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_41")]
        public string TimeBetweenFoods_VII_41 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_41")]
        public string TimeBetweenFoods_VIII_41 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_41")]
        public string TimeBetweenFoods_IX_41 { get; set; }
        [Display(Name = "PartyDiv_41")]
        public string PartyDiv_41 { get; set; }
        [Display(Name = "SecondParty_41")]
        public string SecondParty_41 { get; set; }
        [Display(Name = "PartyOrdTimeRange_41")]
        public string PartyOrdTimeRange_41 { get; set; }
        [Display(Name = "PartyActTimeRange_41")]
        public string PartyActTimeRange_41 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_42")]
        public string TimeBetweenFoods_I_42 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_42")]
        public string TimeBetweenFoods_II_42 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_42")]
        public string TimeBetweenFoods_III_42 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_42")]
        public string TimeBetweenFoods_IV_42 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_42")]
        public string TimeBetweenFoods_V_42 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_42")]
        public string TimeBetweenFoods_VI_42 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_42")]
        public string TimeBetweenFoods_VII_42 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_42")]
        public string TimeBetweenFoods_VIII_42 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_42")]
        public string TimeBetweenFoods_IX_42 { get; set; }
        [Display(Name = "PartyDiv_42")]
        public string PartyDiv_42 { get; set; }
        [Display(Name = "SecondParty_42")]
        public string SecondParty_42 { get; set; }
        [Display(Name = "PartyOrdTimeRange_42")]
        public string PartyOrdTimeRange_42 { get; set; }
        [Display(Name = "PartyActTimeRange_42")]
        public string PartyActTimeRange_42 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_43")]
        public string TimeBetweenFoods_I_43 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_43")]
        public string TimeBetweenFoods_II_43 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_43")]
        public string TimeBetweenFoods_III_43 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_43")]
        public string TimeBetweenFoods_IV_43 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_43")]
        public string TimeBetweenFoods_V_43 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_43")]
        public string TimeBetweenFoods_VI_43 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_43")]
        public string TimeBetweenFoods_VII_43 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_43")]
        public string TimeBetweenFoods_VIII_43 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_43")]
        public string TimeBetweenFoods_IX_43 { get; set; }
        [Display(Name = "PartyDiv_43")]
        public string PartyDiv_43 { get; set; }
        [Display(Name = "SecondParty_43")]
        public string SecondParty_43 { get; set; }
        [Display(Name = "PartyOrdTimeRange_43")]
        public string PartyOrdTimeRange_43 { get; set; }
        [Display(Name = "PartyActTimeRange_43")]
        public string PartyActTimeRange_43 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_44")]
        public string TimeBetweenFoods_I_44 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_44")]
        public string TimeBetweenFoods_II_44 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_44")]
        public string TimeBetweenFoods_III_44 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_44")]
        public string TimeBetweenFoods_IV_44 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_44")]
        public string TimeBetweenFoods_V_44 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_44")]
        public string TimeBetweenFoods_VI_44 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_44")]
        public string TimeBetweenFoods_VII_44 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_44")]
        public string TimeBetweenFoods_VIII_44 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_44")]
        public string TimeBetweenFoods_IX_44 { get; set; }
        [Display(Name = "PartyDiv_44")]
        public string PartyDiv_44 { get; set; }
        [Display(Name = "SecondParty_44")]
        public string SecondParty_44 { get; set; }
        [Display(Name = "PartyOrdTimeRange_44")]
        public string PartyOrdTimeRange_44 { get; set; }
        [Display(Name = "PartyActTimeRange_44")]
        public string PartyActTimeRange_44 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_45")]
        public string TimeBetweenFoods_I_45 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_45")]
        public string TimeBetweenFoods_II_45 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_45")]
        public string TimeBetweenFoods_III_45 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_45")]
        public string TimeBetweenFoods_IV_45 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_45")]
        public string TimeBetweenFoods_V_45 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_45")]
        public string TimeBetweenFoods_VI_45 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_45")]
        public string TimeBetweenFoods_VII_45 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_45")]
        public string TimeBetweenFoods_VIII_45 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_45")]
        public string TimeBetweenFoods_IX_45 { get; set; }
        [Display(Name = "PartyDiv_45")]
        public string PartyDiv_45 { get; set; }
        [Display(Name = "SecondParty_45")]
        public string SecondParty_45 { get; set; }
        [Display(Name = "PartyOrdTimeRange_45")]
        public string PartyOrdTimeRange_45 { get; set; }
        [Display(Name = "PartyActTimeRange_45")]
        public string PartyActTimeRange_45 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_46")]
        public string TimeBetweenFoods_I_46 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_46")]
        public string TimeBetweenFoods_II_46 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_46")]
        public string TimeBetweenFoods_III_46 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_46")]
        public string TimeBetweenFoods_IV_46 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_46")]
        public string TimeBetweenFoods_V_46 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_46")]
        public string TimeBetweenFoods_VI_46 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_46")]
        public string TimeBetweenFoods_VII_46 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_46")]
        public string TimeBetweenFoods_VIII_46 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_46")]
        public string TimeBetweenFoods_IX_46 { get; set; }
        [Display(Name = "PartyDiv_46")]
        public string PartyDiv_46 { get; set; }
        [Display(Name = "SecondParty_46")]
        public string SecondParty_46 { get; set; }
        [Display(Name = "PartyOrdTimeRange_46")]
        public string PartyOrdTimeRange_46 { get; set; }
        [Display(Name = "PartyActTimeRange_46")]
        public string PartyActTimeRange_46 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_47")]
        public string TimeBetweenFoods_I_47 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_47")]
        public string TimeBetweenFoods_II_47 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_47")]
        public string TimeBetweenFoods_III_47 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_47")]
        public string TimeBetweenFoods_IV_47 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_47")]
        public string TimeBetweenFoods_V_47 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_47")]
        public string TimeBetweenFoods_VI_47 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_47")]
        public string TimeBetweenFoods_VII_47 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_47")]
        public string TimeBetweenFoods_VIII_47 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_47")]
        public string TimeBetweenFoods_IX_47 { get; set; }
        [Display(Name = "PartyDiv_47")]
        public string PartyDiv_47 { get; set; }
        [Display(Name = "SecondParty_47")]
        public string SecondParty_47 { get; set; }
        [Display(Name = "PartyOrdTimeRange_47")]
        public string PartyOrdTimeRange_47 { get; set; }
        [Display(Name = "PartyActTimeRange_47")]
        public string PartyActTimeRange_47 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_48")]
        public string TimeBetweenFoods_I_48 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_48")]
        public string TimeBetweenFoods_II_48 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_48")]
        public string TimeBetweenFoods_III_48 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_48")]
        public string TimeBetweenFoods_IV_48 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_48")]
        public string TimeBetweenFoods_V_48 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_48")]
        public string TimeBetweenFoods_VI_48 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_48")]
        public string TimeBetweenFoods_VII_48 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_48")]
        public string TimeBetweenFoods_VIII_48 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_48")]
        public string TimeBetweenFoods_IX_48 { get; set; }
        [Display(Name = "PartyDiv_48")]
        public string PartyDiv_48 { get; set; }
        [Display(Name = "SecondParty_48")]
        public string SecondParty_48 { get; set; }
        [Display(Name = "PartyOrdTimeRange_48")]
        public string PartyOrdTimeRange_48 { get; set; }
        [Display(Name = "PartyActTimeRange_48")]
        public string PartyActTimeRange_48 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_49")]
        public string TimeBetweenFoods_I_49 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_49")]
        public string TimeBetweenFoods_II_49 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_49")]
        public string TimeBetweenFoods_III_49 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_49")]
        public string TimeBetweenFoods_IV_49 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_49")]
        public string TimeBetweenFoods_V_49 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_49")]
        public string TimeBetweenFoods_VI_49 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_49")]
        public string TimeBetweenFoods_VII_49 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_49")]
        public string TimeBetweenFoods_VIII_49 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_49")]
        public string TimeBetweenFoods_IX_49 { get; set; }
        [Display(Name = "PartyDiv_49")]
        public string PartyDiv_49 { get; set; }
        [Display(Name = "SecondParty_49")]
        public string SecondParty_49 { get; set; }
        [Display(Name = "PartyOrdTimeRange_49")]
        public string PartyOrdTimeRange_49 { get; set; }
        [Display(Name = "PartyActTimeRange_49")]
        public string PartyActTimeRange_49 { get; set; }
        [Display(Name = "TimeBetweenFoods_I_50")]
        public string TimeBetweenFoods_I_50 { get; set; }
        [Display(Name = "TimeBetweenFoods_II_50")]
        public string TimeBetweenFoods_II_50 { get; set; }
        [Display(Name = "TimeBetweenFoods_III_50")]
        public string TimeBetweenFoods_III_50 { get; set; }
        [Display(Name = "TimeBetweenFoods_IV_50")]
        public string TimeBetweenFoods_IV_50 { get; set; }
        [Display(Name = "TimeBetweenFoods_V_50")]
        public string TimeBetweenFoods_V_50 { get; set; }
        [Display(Name = "TimeBetweenFoods_VI_50")]
        public string TimeBetweenFoods_VI_50 { get; set; }
        [Display(Name = "TimeBetweenFoods_VII_50")]
        public string TimeBetweenFoods_VII_50 { get; set; }
        [Display(Name = "TimeBetweenFoods_VIII_50")]
        public string TimeBetweenFoods_VIII_50 { get; set; }
        [Display(Name = "TimeBetweenFoods_IX_50")]
        public string TimeBetweenFoods_IX_50 { get; set; }
        [Display(Name = "PartyDiv_50")]
        public string PartyDiv_50 { get; set; }
        [Display(Name = "SecondParty_50")]
        public string SecondParty_50 { get; set; }
        [Display(Name = "PartyOrdTimeRange_50")]
        public string PartyOrdTimeRange_50 { get; set; }
        [Display(Name = "PartyActTimeRange_50")]
        public string PartyActTimeRange_50 { get; set; }


        public String GetFieldValue(String fieldName,int index)
        {
            if (index>0)
            {
                fieldName += "_" + index.ToString().PadLeft(2, '0');
            }

            PropertyInfo p = this.GetType().GetProperty(fieldName);
            if (p != null)
            {
                return p.GetValue(this).ToString();
            }

            return "";
        }

        public void SetFieldValue(String fieldName, int index, object value)
        {
            if (index>0)
            {
                fieldName += "_" + index.ToString().PadLeft(2, '0');
            }

            PropertyInfo p = this.GetType().GetProperty(fieldName);
            if (p != null)
            {
                p.SetValue(this, DataUtil.CStr(value));
            }
        }
    }

}