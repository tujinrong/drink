using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebEvaluation.Common;

namespace WebEvaluation.ReportModels
{

    public class TelRateRptReportHeaderModel : ReportData
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

    public class TelRateRptReportDetailModel : ReportData
    {

        [Display(Name = "事業部")]
        public string DivName { get; set; }

        [Display(Name = "グループ名")]
        public string GroupName { get; set; }

        [Display(Name = "店舗名")]
        public string ShopName { get; set; }

        [Display(Name = "番号")]
        public string StaffCD { get; set; }

        [Display(Name = "担当")]
        public string StaffName { get; set; }

        
        [Display(Name = "総数")]
        public int Count { get; set; }

        [Display(Name = "繋がった")]
        public int Connected { get; set; }

        [Display(Name = "繋がらず")]
        public int NoConnected { get; set; }

        [Display(Name = "通話率")]
        public string Rate { get; set; }
    }

    public class TelRateRptReportDetailModelShop 
    {

        [Display(Name = "事業部")]
        public string DivName { get; set; }

        [Display(Name = "グループ名")]
        public string GroupName { get; set; }

        [Display(Name = "店舗名")]
        public string ShopName { get; set; }

        [Display(Name = "総数")]
        public int Count { get; set; }

        [Display(Name = "繋がった")]
        public int Connected { get; set; }

        [Display(Name = "繋がらず")]
        public int NoConnected { get; set; }

        [Display(Name = "通話率")]
        public string Rate { get; set; }
    }

    public class TelRateRptReportDetailModelGroup
    {

        [Display(Name = "事業部")]
        public string DivName { get; set; }

        [Display(Name = "グループ名")]
        public string GroupName { get; set; }


        [Display(Name = "総数")]
        public int Count { get; set; }

        [Display(Name = "繋がった")]
        public int Connected { get; set; }

        [Display(Name = "繋がらず")]
        public int NoConnected { get; set; }

        [Display(Name = "通話率")]
        public string Rate { get; set; }
    }

    public class TelRateRptReportDetailModelDiv
    {

        [Display(Name = "事業部")]
        public string DivName { get; set; }

        [Display(Name = "総数")]
        public int Count { get; set; }

        [Display(Name = "繋がった")]
        public int Connected { get; set; }

        [Display(Name = "繋がらず")]
        public int NoConnected { get; set; }

        [Display(Name = "通話率")]
        public string Rate { get; set; }
    }
}