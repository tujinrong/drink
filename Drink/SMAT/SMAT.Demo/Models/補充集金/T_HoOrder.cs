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

namespace DrinkService.Models
{
    /// <summary>
    /// 初期キットマスタ
    /// </summary>
    public class T_HoOrder : ModelBase
    {
        public const string RELA1N_ITEMS="ShopCD,HoDate,TantoCD";


        [Key]
        [Required]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar",Order=1)]
        [StringLength(SHOPCD_LEN)]

        public string ShopCD { get; set; }

        [Key]
        [Required]
        [Display(Name = "補充日")]
        [Column(Order = 2)]
        public DateTime HoDate { get; set; }

        [Key]
        [Required]
        [Display(Name = "担当者コード")]
        [Column(TypeName = "Varchar", Order = 3)]
        [StringLength(STAFFCD_LEN)]
        public string TantoCD { get; set; }

        [Required]
        [Display(Name = "ルート")]
        [StringLength(ROUTE_LEN)]
        public string Route { get; set; }


        //[Display(Name = "済")]
        //[Column(TypeName = "Varchar")]
        //[StringLength(1)]
        //public string FinishFlag { get; set; }

        //[Display(Name = "初")]
        //[Column(TypeName = "Varchar")]
        //[StringLength(1)]
        //public string FirstFlag { get; set; }

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