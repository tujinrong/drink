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

        [Key]
        [Required]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar",Order=1)]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        [Key]
        [Required]
        [Display(Name = "顧客コード")]
        [Column(TypeName = "Varchar",Order=2)]
        [StringLength(CLIENTCD_LEN)]
        public string ClientCD { get; set; }

        [Key]
        [Required]
        [Display(Name = "補充SEQ")]
        [Column(Order = 3)]
        public int Seq { get; set; }

        [Key]
        [Required]
        [Display(Name = "商品コード")]
        [Column(Order =4)]
        [StringLength(ITEMCD_LEN)]
        public string ItemCD { get; set; }

        [Display(Name = "棚")]
        [Column(TypeName = "numeric")]
        public decimal? ShelfNo { get; set; }

        [Display(Name = "順")]
        [Column(TypeName = "numeric")]
        public decimal? ShelfSubNo { get; set; }

        [Required]
        [Display(Name = "前回在庫")]
        [Column(TypeName = "numeric")]
        public decimal PrevNum { get; set; }

        [Required]
        [Display(Name = "今回在庫")]
        [Column(TypeName = "numeric")]
        public decimal ThisNum { get; set; }

        [Required]
        [Display(Name = "補充数")]
        [Column(TypeName = "numeric")]
        public decimal AddNum { get; set; }

        [Required]
        [Display(Name = "補充前")]
        [Column(TypeName = "numeric")]
        public decimal BeforeNum { get; set; }

        [Required]
        [Display(Name = "使用数")]
        [Column(TypeName = "numeric")]
        public decimal UsedNum { get; set; }

        [Required]
        [Display(Name = "補充後")]
        [Column(TypeName = "numeric")]
        public decimal AfterNum { get; set; }


        [Required]
        [Display(Name = "単価")]
        [Column(TypeName = "numeric")]
        public decimal Price { get; set; }


        [Display(Name = "金額")]
        [Column(TypeName = "numeric")]
        public decimal Money { get; set; }


        [Display(Name = "賞味期限")]
        public DateTime? FreshDate { get; set; }

        [Required]
        [Display(Name = "次回単価")]
        [Column(TypeName = "numeric")]
        public decimal NextPrice { get; set; }

        [Required]
        [Display(Name = "次回中止")]
        [Column(TypeName = "numeric")]
        public decimal NextStopFlag { get; set; }


    }
}