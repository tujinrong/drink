using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebEvaluation.Common;

namespace WebEvaluation.ReportModels
{
    public class ValResultReportHeaderModel:ReportData
    {

        [Display(Name = "期間")]
        public string Time { get; set; }

        [Display(Name = "集計")]
        public string Range { get; set; }

        [Display(Name = "期間Title")]
        public string Time_Title { get; set; }

        [Display(Name = "集計Title")]
        public string Range_Title { get; set; }

        [Display(Name = "評価SS")]
        public int EvaSS { get; set; }

        [Display(Name = "評価S")]
        public int EvaS { get; set; }

        [Display(Name = "評価A")]
        public int EvaA { get; set; }

        [Display(Name = "評価B")]
        public int EvaB { get; set; }

        [Display(Name = "評価C")]
        public int EvaC { get; set; }

        [Display(Name = "評価D")]
        public int EvaD { get; set; }

        [Display(Name = "評価E")]
        public int EvaE { get; set; }

        [Display(Name = "評価F")]
        public int EvaF { get; set; }


        [Display(Name = "構成SS")]
        public string CpsSS { get; set; }

        [Display(Name = "構成S")]
        public string CpsS { get; set; }

        [Display(Name = "構成A")]
        public string CpsA { get; set; }

        [Display(Name = "構成B")]
        public string CpsB { get; set; }

        [Display(Name = "構成C")]
        public string CpsC { get; set; }

        [Display(Name = "構成D")]
        public string CpsD { get; set; }

        [Display(Name = "構成E")]
        public string CpsE { get; set; }

        [Display(Name = "構成F")]
        public string CpsF { get; set; }

    }

    public class ValResultStaffReportHeaderModel : ReportData
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

    public class ValResultStaffReportDetailModel : ReportData
    {
        [Display(Name = "店舗")]
        public string ShopName { get; set; }

        [Display(Name = "担当者")]
        public string StaffName { get; set; }

        [Display(Name = "評価SS")]
        public int EvaSS { get; set; }

        [Display(Name = "評価S")]
        public int EvaS { get; set; }

        [Display(Name = "評価A")]
        public int EvaA { get; set; }

        [Display(Name = "評価B")]
        public int EvaB { get; set; }

        [Display(Name = "評価C")]
        public int EvaC { get; set; }

        [Display(Name = "評価D")]
        public int EvaD { get; set; }

        [Display(Name = "評価E")]
        public int EvaE { get; set; }

        [Display(Name = "評価F")]
        public int EvaF { get; set; }


        [Display(Name = "構成SS")]
        public string CpsSS { get; set; }

        [Display(Name = "構成S")]
        public string CpsS { get; set; }

        [Display(Name = "構成A")]
        public string CpsA { get; set; }

        [Display(Name = "構成B")]
        public string CpsB { get; set; }

        [Display(Name = "構成C")]
        public string CpsC { get; set; }

        [Display(Name = "構成D")]
        public string CpsD { get; set; }

        [Display(Name = "構成E")]
        public string CpsE { get; set; }

        [Display(Name = "構成F")]
        public string CpsF { get; set; }

    }

}