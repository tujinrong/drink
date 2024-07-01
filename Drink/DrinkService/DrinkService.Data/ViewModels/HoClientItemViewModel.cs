using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinkService.Models;

namespace DrinkService.Data.ViewModels
{
    public class HoClientItemViewModel  : ModelBase
    {
        [Display(Name = "売れるマーク")]
        public string SaleFlag { get; set; }

        [Display(Name = "商品名")]
        public string ItemsName { get; set; }

        [Display(Name = "店舗コード")]
        public string ShopCD { get; set; }

        [Display(Name = "顧客コード")]
        public string ClientCD { get; set; }

        [Display(Name = "補充回数")]
        public string Seq { get; set; }

        [Display(Name = "商品コード")]
        public string ItemCD { get; set; }

        [Display(Name = "棚")]
        public string ShelfNo { get; set; }

        [Display(Name = "順")]
        public string ShelfSubNo { get; set; }

        [Display(Name = "前回在庫数")]
        public string PrevNum { get; set; }
        
        [Display(Name = "今回在庫数")]
        public string ThisNum { get; set; }

        [Display(Name = "補充数")]
        public string AddNum { get; set; }

        [Display(Name = "補充前数")]
        public string BeforeNum { get; set; }

        [Display(Name = "使用数")]
        public string UsedNum { get; set; }

        [Display(Name = "補充後数")]
        public string AfterNum { get; set; }

        [Display(Name = "単価")]
        public string Price { get; set; }

        [Display(Name = "金額")]
        public string Money { get; set; }

        [Display(Name = "賞味期限")]
        public string FreshDate { get; set; }

        [Display(Name = "次回単価")]
        public string NextPrice { get; set; }

        [Display(Name = "次回中止")]
        public string NextStopFlag { get; set; }

        [Display(Name = "商品追加")]
        public string ItemAddFlag { get; set; }
    }
}
