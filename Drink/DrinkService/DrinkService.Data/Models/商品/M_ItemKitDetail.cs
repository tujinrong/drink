//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  初期キットマスタ
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
    /// 初期キットマスタ
    /// </summary>
    public class M_ItemKitDetail : ModelBase
    {

        [Key]
        [Required]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar", Order=1)]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        [Key]
        [Required]
        [Display(Name = "キットID")]
        [Column(Order=2)]
        public int KitID { get; set; }

        [Key]
        [Required]
        [Display(Name = "棚コード")]
        [Column(TypeName = "Varchar", Order = 3)]
        [StringLength(1)]
        public string ShelfCD { get; set; }

        [Key]
        [Required]
        [Display(Name = "商品コード")]
        [Column(TypeName = "Varchar", Order = 4)]
        [StringLength(ITEMCD_LEN)]
        public string ItemCD { get; set; }

        [Required]
        [Display(Name = "数量")]
        [Column(TypeName = "numeric")]
        public decimal Num { get; set; }

        [Display(Name = "価格")]
        public decimal Price { get; set; }

    
    }
}