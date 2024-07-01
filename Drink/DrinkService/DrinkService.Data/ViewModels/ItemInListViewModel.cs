using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class ItemInListViewModel : ModelBase
    {
        [Display(Name = "店舗コード")]
        public string ShopCD { get; set; }

        [Display(Name = "商品コード")]
        public string ItemCD { get; set; }

        [Display(Name = "商品名")]
        public string ItemName { get; set; }

        [Display(Name = "在庫数")]
        public decimal InNum { get; set; }
    }
}
