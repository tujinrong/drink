//*****************************************************************************
// [システム]  ダスキン・配置ドリンク
// 
// [機能概要]  補充明細
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
    /// 補充明細
    /// </summary>
    public class T_HoClientItem : ModelBase
    {
        public const string I店舗コード = "ShopCD";
        [Key]
        [Required]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar",Order=1)]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        public const string I顧客コード = "ClientCD";
        [Key]
        [Required]
        [Display(Name = "顧客コード")]
        [Column(TypeName = "Varchar",Order=2)]
        [StringLength(CLIENTCD_LEN)]
        public string ClientCD { get; set; }

        public const string I補充回数 = "Seq";
        [Key]
        [Required]
        [Display(Name = "補充回数")]
        [Column(Order = 3)]
        public int Seq { get; set; }

        public const string I商品コード = "ItemCD";
        [Key]
        [Required]
        [Display(Name = "商品コード")]
        [Column(Order =4)]
        [StringLength(ITEMCD_LEN)]
        public string ItemCD { get; set; }

        public const string I棚 = "ShelfNo";
        [Display(Name = "棚")]
        [Column(TypeName = "numeric")]
        public decimal? ShelfNo { get; set; }

        public const string I順 = "";
        [Display(Name = "順")]
        [Column(TypeName = "numeric")]
        public decimal? ShelfSubNo { get; set; }

        public const string I前回在庫数 = "PrevNum";
        [Display(Name = "前回在庫数")]
        [Column(TypeName = "numeric")]
        public decimal? PrevNum { get; set; }

        public const string I今回在庫数 = "ThisNum";
        [Display(Name = "今回在庫数")]
        [Column(TypeName = "numeric")]
        public decimal? ThisNum { get; set; }

        public const string I補充数 = "AddNum";
        [Display(Name = "補充数")]
        [Column(TypeName = "numeric")]
        public decimal? AddNum { get; set; }

        public const string I補充前数 = "BeforeNum";
        [Display(Name = "補充前数")]
        [Column(TypeName = "numeric")]
        public decimal? BeforeNum { get; set; }

        public const string I使用数 = "UsedNum";
        [Display(Name = "使用数")]
        [Column(TypeName = "numeric")]
        public decimal? UsedNum { get; set; }

        public const string I補充後数 = "";
        [Display(Name = "補充後数")]
        [Column(TypeName = "numeric")]
        public decimal? AfterNum { get; set; }

        public const string I単価 = "Price";
        [Display(Name = "単価")]
        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        public const string I金額 = "Money";
        [Display(Name = "金額")]
        [Column(TypeName = "numeric")]
        public decimal? Money { get; set; }

        public const string I賞味期限 = "FreshDate";
        [Display(Name = "賞味期限")]
        public DateTime? FreshDate { get; set; }

        public const string I次回単価 = "NextPrice";
        [Display(Name = "次回単価")]
        [Column(TypeName = "numeric")]
        public decimal? NextPrice { get; set; }

        public const string I次回中止 = "NextStopFlag";
        [Required]
        [Display(Name = "次回中止")]
        [Column(TypeName = "numeric")]
        public decimal NextStopFlag { get; set; }

        public const string I売れるマーク = "SaleFlag";
        [Display(Name = "売れるマーク")]
        [Column(TypeName = "numeric")]
        public decimal? SaleFlag { get; set; }

        public const string I商品追加 = "ItemAddFlag";
        [Display(Name = "商品追加")]
        [Column(TypeName = "numeric")]
        public decimal? ItemAddFlag { get; set; }
    }
}