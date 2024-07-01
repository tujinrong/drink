using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.ViewModels
{
    public class ValRptResultViewModel
    {
        public int No { get; set; }

        [Display(Name = "事業部コード")]
        public string DivCD { get; set; }

        [Display(Name = "事業部名")]
        public string DivName { get; set; }

        [Display(Name = "グループコード")]
        public string GroupCD { get; set; }

        [Display(Name = "グループ名")]
        public string GroupName { get; set; }

        [Display(Name = "店舗コード")]
        public string ShopCD { get; set; }

        [Display(Name = "店舗名")]
        public string ShopName { get; set; }

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

        [Display(Name = "評価件数")]
        public int CountAll { get; set; }

        [Display(Name = "総点数")]
        public int SumAll { get; set; }

        [Display(Name = "平均点")]
        public string Avg { get; set; }

        public double DblAvg { get; set; }

        [Display(Name = "グループ平均")]
        public string AvgGroup { get; set; }

        [Display(Name = "事業部平均")]
        public string AvgDiv { get; set; }
        
    }
}