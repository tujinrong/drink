
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebEvaluation.Models;
using WebEvaluation.Utils;

namespace WebEvaluation.DataModels
{
    public class ShopDetailModel
    {

        [Display(Name = "事業部")]
        public string DivName { get; set; }

        [Display(Name = "グループ")]
        public string GroupName { get; set; }

        [Display(Name = "店舗略称")]
        public string ShopCD { get; set; }

        [Display(Name = "店舗名称")]
        public string ShopName { get; set; }

    }
}