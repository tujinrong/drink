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

        [Key]
        [Display(Name = "商品コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(ITEMCD_LEN)]
        public string ItemCD { get; set; }

        [Required]  
        [Display(Name = "商品名")]
        [StringLength(40)]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "商品略称")]
        [StringLength(20)]
        public string ShortName { get; set; }

        [Display(Name = "種別")]
        [Column(TypeName = "Varchar")]
        [StringLength(2)]
        public string ItemTypeCD { get; set; }

        [Display(Name = "標準単価")]
        [Column(TypeName = "numeric")]
        public decimal StandardPrice { get; set; }

        [Display(Name = "店舗単価")]
        [Column(TypeName = "numeric")]
        public decimal ShopPrice { get; set; }

        [Display(Name = "入数")]
        [Column(TypeName = "numeric")]
        public decimal InNum { get; set; }

        //---------------------------------------------------------------------
        //システム共通項目
        //---------------------------------------------------------------------
        [Display(Name = "更新者")]
        [Column(TypeName = "Varchar")]
        [StringLength(STAFFCD_LEN)]
        public string UpdateUserID { get; set; }

        [Display(Name = "更新日時")]
        public DateTime? UpdateTime { get; set; }



    }
}