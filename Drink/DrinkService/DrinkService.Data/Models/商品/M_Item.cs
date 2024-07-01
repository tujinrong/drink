//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  商品マスタ。
//
// [作成履歴]　2015/02/25  屠錦栄　初版 
//
// [レビュー]　2015/03/17  屠錦栄　 
//*****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace DrinkService.Models
{
    /// <summary>
    /// 商品マスタ
    /// </summary>
    public class M_Item : ModelBase
    {

        public const string I商品コード = "ItemCD";
        [Key]
        [Display(Name = "商品コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(ITEMCD_LEN)]
        public string ItemCD { get; set; }

        public const string I商品名 = "ItemName";
        [Required]  
        [Display(Name = "商品名")]
        [StringLength(40)]
        public string ItemName { get; set; }

        public const string I商品略称 = "ShortName";
        [Required]
        [Display(Name = "商品略称")]
        [StringLength(20)]
        public string ShortName { get; set; }

        public const string I商品種別 = "ItemTypeCD";
        [Display(Name = "商品種別")]
        [Column(TypeName = "Varchar")]
        [StringLength(2)]
        public string ItemTypeCD { get; set; }

        public const string I標準単価 = "StandardPrice";
        [Display(Name = "標準単価")]
        [Column(TypeName = "numeric")]
        public decimal StandardPrice { get; set; }

        public const string I店舗単価 = "ShopPrice";
        [Display(Name = "店舗単価")]
        [Column(TypeName = "numeric")]
        public decimal ShopPrice { get; set; }

        public const string I入数 = "InNum";
        [Display(Name = "入数")]
        [Column(TypeName = "numeric")]
        public decimal InNum { get; set; }

        public const string I適用開始日 = "SaleStartDay";
        [Display(Name = "適用開始日")]
        public DateTime? SaleStartDay { get; set; }

        public const string I販売終了日 = "SaleEndDay";
        [Display(Name = "販売終了日")]
        public DateTime? SaleEndDay { get; set; }

        public const string I凍結日 = "FreezingDay";
        [Display(Name = "凍結日")]
        public DateTime? FreezingDay { get; set; }

        //---------------------------------------------------------------------
        //システム共通項目
        //---------------------------------------------------------------------
        public const string I更新者 = "UpdateTime";
        [Display(Name = "更新者")]
        [StringLength(STAFFNAME_LEN)]
        public string UpdateUser { get; set; }

        public const string I更新日時 = "UpdateTime";
        [Display(Name = "更新日時")]
        public DateTime? UpdateTime { get; set; }

        public const string I資格コード = "QualifiedCD";
        [Display(Name = "資格コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(7)]
        public string QualifiedCD { get; set; }

        public const string Iメーカーコード = "MakerCD";
        [Display(Name = "メーカーコード")]
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string MakerCD { get; set; }

        public const string I軽減税率区分 = "TaxTypeCD";
        [Display(Name = "軽減税率区分")]
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string TaxTypeCD { get; set; }

    }
}