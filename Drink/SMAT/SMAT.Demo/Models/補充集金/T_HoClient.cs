//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  補充
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
    public class T_HoClient : ModelBase
    {
        public const string I補充日 = "HoDate";
        public const string Iルート = "Route";
        public const string I売上 = "SoldMoney";
        public const string I集金 = "GetMoney";
        public const string I過不足 = "DiffMoney";
        public const string I備考 = "Memo";

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

        [Required]
        [Display(Name = "補充日")]
        public DateTime HoDate { get; set; }

        [Required]
        [Display(Name = "ルート")]
        [StringLength(ROUTE_LEN)]
        public string Route { get; set; }

        [Required]
        [Display(Name = "担当者コード")]
        [StringLength(STAFFCD_LEN)]
        public string TantoCD { get; set; }

        [Display(Name = "売上")]
        [Column(TypeName = "numeric")]
        public decimal SoldMoney { get; set; }

        [Display(Name = "集金")]
        [Column(TypeName = "numeric")]
        public decimal GetMoney { get; set; }

        [Display(Name = "過不足")]
        [Column(TypeName = "numeric")]
        public decimal DiffMoney { get; set; }

        [Display(Name = "備考")]
        [StringLength(200)]
        public string Memo { get; set; }

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