
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebEvaluation.Models;
using WebEvaluation.Utils;

namespace WebEvaluation.ViewModels
{
    public class ShopViewModel
    {

        public int No { get; set; }

        [Display(Name = "状態")]
        [CSV(Output = false)]
        public string ShopType { get; set; }

        [Display(Name = "事業部")]
        [CSV(Output = false)]
        public string DivCD { get; set; }

        [Display(Name = "事業部")]
        public string DivName { get; set; }

        [Display(Name = "グループコード")]
        [CSV(Output = false)]
        public string GroupCD { get; set; }

        [Display(Name = "グループ")]
        public string GroupName { get; set; }

        [Display(Name = "店舗名")]
        public string ShopCD { get; set; }

        [Display(Name = "会場")]
        public string ShopName { get; set; }

    }
}