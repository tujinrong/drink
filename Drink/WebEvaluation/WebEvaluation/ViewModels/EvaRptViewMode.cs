using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.ViewModels
{
    public class ValRptViewModel
    {
        public int No { get; set; }

        public string AreaName { get; set; }

        public string GroupName { get; set; }

        [Display(Name = "店舗")]
        public string ShopCD { get; set; }



    }
}