using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.ViewModels
{
    public class TelRptSearchViewModel
    {

        [Display(Name = "店舗名")]
        public string ShopCD { get; set; }

    }
}