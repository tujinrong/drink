using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebEvaluation.Common;

namespace WebEvaluation.ReportModels
{
    public class TelRptReportHeaderModel:ReportData
    {
        [Display(Name = "期間")]
        public string Time { get; set; }

        [Display(Name = "集計")]
        public string Range { get; set; }

        [Display(Name = "期間Title")]
        public string Time_Title { get; set; }

        [Display(Name = "集計Title")]
        public string Range_Title { get; set; }
    }

    public class TelRptReportDetailModel : ReportData
    {
        [Key]
        public int No { get; set; }

        [Display(Name = "挙式日")]
        public string HoldDate { get; set; }

        [Display(Name = "店舗")]
        public string ShopName { get; set; }

        [Display(Name = "新郎氏名")]
        public string BrideName { get; set; }

        [Display(Name = "新婦氏名")]
        public string GroomName { get; set; }


        [Display(Name = "番号")]
        public string StaffCD { get; set; }

        [Display(Name = "担当者")]
        public string StaffName { get; set; }

        [Display(Name = "会場種別")]
        public string HallType { get; set; }

        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [Display(Name = "電話内容")]
        public string TelMemo { get; set; }

        [Display(Name = "評価")]
        public string StaffEva { get; set; }

        [Display(Name = "二次評価")]
        public string LeaderEva { get; set; }
    }

    public class TelRptReportDetailModel2
    {
        [Key]
        public int No { get; set; }

        [Display(Name = "新郎氏名")]
        public string BrideName { get; set; }

        [Display(Name = "新婦氏名")]
        public string GroomName { get; set; }

        [Display(Name = "番号")]
        public string StaffCD { get; set; }

        [Display(Name = "担当者")]
        public string StaffName { get; set; }

        [Display(Name = "店舗")]
        public string ShopName { get; set; }

        [Display(Name = "挙式日")]
        public string HoldDate { get; set; }

        [Display(Name = "会場種別")]
        public string HallType { get; set; }

        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [Display(Name = "電話内容")]
        public string TelMemo { get; set; }

        [Display(Name = "評価")]
        public string StaffEva { get; set; }

        [Display(Name = "二次評価")]
        public string LeaderEva { get; set; }
    }

    public class TelRptReportDetailModel3
    {
        [Key]
        public int No { get; set; }

        [Display(Name = "新郎氏名")]
        public string BrideName { get; set; }

        [Display(Name = "新婦氏名")]
        public string GroomName { get; set; }

        [Display(Name = "店舗")]
        public string ShopName { get; set; }

        [Display(Name = "挙式日")]
        public string HoldDate { get; set; }

        [Display(Name = "番号")]
        public string StaffCD { get; set; }

        [Display(Name = "担当者")]
        public string StaffName { get; set; }

        [Display(Name = "会場種別")]
        public string HallType { get; set; }

        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [Display(Name = "電話内容")]
        public string TelMemo { get; set; }

        [Display(Name = "評価")]
        public string StaffEva { get; set; }

        [Display(Name = "二次評価")]
        public string LeaderEva { get; set; }
    }

    public class TelRptReportDetailModel_Normal 
    {
        [Key]
        public int No { get; set; }

        [Display(Name = "挙式日")]
        public string HoldDate { get; set; }

        [Display(Name = "店舗")]
        public string ShopName { get; set; }

        [Display(Name = "番号")]
        public string StaffCD { get; set; }

        [Display(Name = "担当者")]
        public string StaffName { get; set; }

        [Display(Name = "会場種別")]
        public string HallType { get; set; }

        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [Display(Name = "電話内容")]
        public string TelMemo { get; set; }

    }
    public class TelRptReportDetailModel_Normal2
    {
        [Key]
        public int No { get; set; }

        [Display(Name = "番号")]
        public string StaffCD { get; set; }

        [Display(Name = "担当者")]
        public string StaffName { get; set; }

        [Display(Name = "店舗")]
        public string ShopName { get; set; }

        [Display(Name = "挙式日")]
        public string HoldDate { get; set; }

        [Display(Name = "会場種別")]
        public string HallType { get; set; }

        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [Display(Name = "電話内容")]
        public string TelMemo { get; set; }

    }

    public class TelRptReportDetailModel_Normal3
    {
        [Key]
        public int No { get; set; }

        [Display(Name = "店舗")]
        public string ShopName { get; set; }

        [Display(Name = "挙式日")]
        public string HoldDate { get; set; }

        [Display(Name = "番号")]
        public string StaffCD { get; set; }

        [Display(Name = "担当者")]
        public string StaffName { get; set; }

        [Display(Name = "会場種別")]
        public string HallType { get; set; }

        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [Display(Name = "電話内容")]
        public string TelMemo { get; set; }

    }
}