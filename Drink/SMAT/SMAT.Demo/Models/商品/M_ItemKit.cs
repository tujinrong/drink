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
    public class M_ItemKit : ModelBase
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

        [Required]
        [Display(Name = "キット名")]
        [StringLength(10)]
        public string KitName { get; set; }


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