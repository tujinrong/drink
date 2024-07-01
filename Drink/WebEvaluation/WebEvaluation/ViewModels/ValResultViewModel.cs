using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.ViewModels
{
    public class ValResultViewModel
    {
       
        [Display(Name = "")]
        public string Type { get; set; }

        [Display(Name = "SS")]
        public string EvaSS { get; set; }

        [Display(Name = "S")]
        public string EvaS { get; set; }

        [Display(Name = "A")]
        public string EvaA { get; set; }

        [Display(Name = "B")]
        public string EvaB { get; set; }

        [Display(Name = "C")]
        public string EvaC { get; set; }

        [Display(Name = "D")]
        public string EvaD { get; set; }

        [Display(Name = "E")]
        public string EvaE { get; set; }

        [Display(Name = "F")]
        public string EvaF { get; set; }

        [Display(Name = "統計")]
        public string SumAll { get; set; }
        
    }

    public class ValResultStaffViewModel
    {
        [Display(Name = "店舗")]
        public string ShopName { get; set; }

        [Display(Name = "担当者")]
        public string StaffName { get; set; }

        [Display(Name = "")]
        public string Type { get; set; }

        [Display(Name = "SS")]
        public string EvaSS { get; set; }

        [Display(Name = "S")]
        public string EvaS { get; set; }

        [Display(Name = "A")]
        public string EvaA { get; set; }

        [Display(Name = "B")]
        public string EvaB { get; set; }

        [Display(Name = "C")]
        public string EvaC { get; set; }

        [Display(Name = "D")]
        public string EvaD { get; set; }

        [Display(Name = "E")]
        public string EvaE { get; set; }

        [Display(Name = "F")]
        public string EvaF { get; set; }

        [Display(Name = "統計")]
        public string SumAll { get; set; }

    }
}