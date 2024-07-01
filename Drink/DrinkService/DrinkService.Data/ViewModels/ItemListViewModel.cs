using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class ItemListViewModel : ModelBase
    {
        [Display(Name = "商品コード")]
        public string ItemCD { get; set; }

        [Display(Name = "商品名")]
        public string ItemName { get; set; }

        [Display(Name = "略称")]
        public string ShortName { get; set; }

        [Display(Name = "標準単価")]
        public decimal StandardPrice { get; set; }

        [Display(Name = "店舗単価")]
        public decimal ShopPrice { get; set; }

        [Display(Name = "入数")]
        public decimal InNum { get; set; }

        [Display(Name = "適用開始日")]
        public string SaleStartDay { get; set; }


        [Display(Name = "販売終了日")]
        public string SaleEndDay { get; set; }


        [Display(Name = "凍結日")]
        public string FreezingDay { get; set; }


        [Display(Name = "研修資格コード")]
        public string QualifiedCD { get; set; }

        [Display(Name = "研修資格")]
        public string QualifiedName { get; set; }

        [Display(Name = "メーカーコード")]
        public string MakerCD { get; set; }

        [Display(Name = "メーカー")]
        public string MakerName { get; set; }


        [Display(Name = "種別コード")]
        public string ItemTypeCD { get; set; }

        [Display(Name = "種別")]
        public string ItemTypeName { get; set; }


        [Display(Name = "軽減税率区分")]
        public string TaxTypeCD { get; set; }

        [Display(Name = "軽減税率区分名")]
        public string TaxTypeName { get; set; }

    }
}
