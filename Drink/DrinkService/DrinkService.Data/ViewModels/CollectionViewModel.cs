using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class CollectionViewModel : ModelBase
    {
        [Display(Name = "店舗コード")]
        public string ShopCD { get; set; }

        [Display(Name = "日付")]
        public string HoDate { get; set; }
        public DateTime date { get; set; }

        public string HoDateStr { get; set; }

        [Display(Name = "担当者コード")]
        public string StaffCD { get; set; }

        [Display(Name = "担当者名")]
        public string StaffName { get; set; }

        [Display(Name = "集金額")]
        public string GetMoney { get; set; }
    }
}
