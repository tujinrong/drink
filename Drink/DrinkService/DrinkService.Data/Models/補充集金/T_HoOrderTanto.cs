//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  補充一覧明細
//
// [作成履歴]　2015/02/25  屠錦栄　初版 
//
// [レビュー]　2015/03/17  屠錦栄　 
//*****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

using System.Collections.Generic;

namespace DrinkService.Models
{
    /// <summary>
    /// 初期キットマスタ
    /// </summary>
    public class T_HoOrderTanto : ModelBase
    {
        public const string RELA1N_ITEMS = "ShopCD,HoDate,TantoCD";

        public const string I店舗コード = "ShopCD";
        [Key]
        [Required]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar", Order = 1)]
        [StringLength(SHOPCD_LEN)]

        public string ShopCD { get; set; }

        public const string I補充日 = "HoDate";
        [Key]
        [Required]
        [Display(Name = "補充日")]
        [Column(Order = 2)]
        public DateTime HoDate { get; set; }

        public const string I担当者コード = "TantoCD";
        [Key]
        [Required]
        [Display(Name = "担当者コード")]
        [Column(TypeName = "Varchar", Order = 3)]
        [StringLength(STAFFCD_LEN)]
        public string TantoCD { get; set; }

        public const string Iルート = "";
        [Key]
        [Required]
        [Display(Name = "ルート")]
        [Column(TypeName = "Varchar", Order = 4)]
        [StringLength(ROUTE_LEN)]
        public string Route { get; set; }


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