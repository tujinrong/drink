using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class KitDetailViewModel : ModelBase
    {
        [Display(Name = "店舗")]
        public string ShopCD { get; set; }

        [Display(Name = "キットID")]
        public int KitID { get; set; }

        [Display(Name = "棚コード")]
        [StringLength(1)]
        public string ShelfCD { get; set; }

        [Display(Name = "商品コード")]
        public string ItemCD { get; set; }

        [Display(Name = "商品")]
        public string ItemName { get; set; }

        [Display(Name = "商品")]
        public string ShortName { get; set; }

        [Display(Name = "数量")]
        public decimal Num { get; set; }

        [Display(Name = "価格")]
        public decimal Price { get; set; }

        public int No { get; set; }
    }
}
