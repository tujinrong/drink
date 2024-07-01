using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebEvaluation.Common;

namespace WebEvaluation.ReportModels
{
    public class ValRptResultReportHeaderModel : ReportData
    {
        [Display(Name = "期間")]
        public string Time { get; set; }

        [Display(Name = "集計")]
        public string Range { get; set; }

        [Display(Name = "全国平均")]
        public string AvgJp { get; set; }

        [Display(Name = "期間Title")]
        public string Time_Title { get; set; }

        [Display(Name = "集計Title")]
        public string Range_Title { get; set; }
    }

    public class ValRptResultReportDetailModel : ReportData
    {
        public int No { get; set; }


        [Display(Name = "事業部")]
        public string DivName { get; set; }


        [Display(Name = "グループ")]
        public string GroupName { get; set; }

        [Display(Name = "店舗名")]
        public string ShopName { get; set; }

        [Display(Name = "SS")]
        public int EvaSS { get; set; }

        [Display(Name = "S")]
        public int EvaS { get; set; }

        [Display(Name = "A")]
        public int EvaA { get; set; }

        [Display(Name = "B")]
        public int EvaB { get; set; }

        [Display(Name = "C")]
        public int EvaC { get; set; }

        [Display(Name = "D")]
        public int EvaD { get; set; }

        [Display(Name = "E")]
        public int EvaE { get; set; }

        [Display(Name = "F")]
        public int EvaF { get; set; }

        [Display(Name = "評価件数")]
        public int CountAll { get; set; }

        [Display(Name = "総点数")]
        public int SumAll { get; set; }

        [Display(Name = "店舗平均点")]
        public string Avg { get; set; }

        [Display(Name = "グループ平均")]
        public string AvgGroup { get; set; }

        [Display(Name = "事業部平均")]
        public string AvgDiv { get; set; }

    }

    public class ValRptResultReportDetailModel2
    {
        public int No { get; set; }

        [Display(Name = "店舗名")]
        public string ShopName { get; set; }

        [Display(Name = "SS")]
        public int EvaSS { get; set; }

        [Display(Name = "S")]
        public int EvaS { get; set; }

        [Display(Name = "A")]
        public int EvaA { get; set; }

        [Display(Name = "B")]
        public int EvaB { get; set; }

        [Display(Name = "C")]
        public int EvaC { get; set; }

        [Display(Name = "D")]
        public int EvaD { get; set; }

        [Display(Name = "E")]
        public int EvaE { get; set; }

        [Display(Name = "F")]
        public int EvaF { get; set; }

        [Display(Name = "評価件数")]
        public int CountAll { get; set; }

        [Display(Name = "総点数")]
        public int SumAll { get; set; }

        [Display(Name = "店舗平均点")]
        public string Avg { get; set; }

    }

    public class ValRptResultReportDetailModel3 : ReportData
    {
        public int No { get; set; }


        [Display(Name = "事業部")]
        public string DivName { get; set; }

        [Display(Name = "グループ")]
        public string GroupName { get; set; }

        [Display(Name = "店舗名")]
        public string ShopName { get; set; }

        [Display(Name = "評価件数")]
        public int CountAll { get; set; }

        [Display(Name = "総点数")]
        public int SumAll { get; set; }

        [Display(Name = "店舗平均点")]
        public string Avg { get; set; }

        [Display(Name = "グループ平均")]
        public string AvgGroup { get; set; }

        [Display(Name = "事業部平均")]
        public string AvgDiv { get; set; }

    }

    public class ValRptResultReportDetailModel4 : ReportData
    {
        public int No { get; set; }


        [Display(Name = "店舗名")]
        public string ShopName { get; set; }

        [Display(Name = "評価件数")]
        public int CountAll { get; set; }

        [Display(Name = "総点数")]
        public int SumAll { get; set; }

        [Display(Name = "店舗平均点")]
        public string Avg { get; set; }

    }
}