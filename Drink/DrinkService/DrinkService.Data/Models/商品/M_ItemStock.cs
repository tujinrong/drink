//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  商品在庫マスタ。
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
    /// 商品在庫マスタ
    /// </summary>
    public class M_ItemStock : ModelBase
    {
        public const string I店舗コード = "ShopCD";
        [Display(Name = "店舗コード")]
        [Key, Required, Column(TypeName = "Varchar", Order = 1)]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        public const string I商品コード = "ItemCD";
        [Display(Name = "商品コード")]
        [Key, Required, Column(TypeName = "Varchar", Order = 2)]
        [StringLength(ITEMCD_LEN)]
        public string ItemCD { get; set; }

        public const string I在庫数 = "StockNum";
        [Display(Name = "在庫数")]
        [Column(TypeName = "numeric")]
        public decimal StockNum { get; set; }

      
        //---------------------------------------------------------------------
        //システム共通項目
        //---------------------------------------------------------------------
        public const string I更新者 = "UpdateUser";
        [Display(Name = "更新者")]
        [StringLength(STAFFNAME_LEN)]
        public string UpdateUser { get; set; }

        public const string I更新日時 = "UpdateTime";
        [Display(Name = "更新日時")]
        public DateTime? UpdateTime { get; set; }

    }
}