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
    public class T_HoOrderClient : ModelBase
    {

        public const string I店舗コード = "ShopCD";
        [Key]
        [Required]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar",Order=1)]
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


        public const string I顧客コード = "ClientCD";
        [Key]
        [Required]
        [Display(Name = "顧客コード")]
        [Column(TypeName = "Varchar", Order=4)]
        [StringLength(CLIENTCD_LEN)]
        public string ClientCD { get; set; }

        public const string Iルート = "";
        [Key]
        [Required]
        [Display(Name = "ルート")]
        [Column(TypeName = "Varchar", Order = 5)]
        [StringLength(ROUTE_LEN)]
        public string Route { get; set; }

        public const string I初回フラグ = "FirstFlag";
        [Display(Name = "初回フラグ")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
         public string FirstFlag { get; set; }

        public const string I済フラグ = "DoneFlag";
        [Display(Name = "済フラグ")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string DoneFlag { get; set; }

        public const string I補充SEQ = "Seq";
        //未使用
        [Display(Name = "補充SEQ")]
        public int Seq { get; set; }

        public const string I納品書伝票番号 = "SlipNO";
        [Display(Name = "納品書伝票番号")]
        public decimal? SlipNO { get; set; }

        public const string I後日今ｽﾄフラグ = "AfterStopFlag";
        [Display(Name = "後日今ｽﾄフラグ")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string AfterStopFlag { get; set; }

        public const string I後日 = "AfterDate";
        [Display(Name = "後日")]
        public DateTime? AfterDate { get; set; }

        public const string ILast後日今ｽﾄフラグ = "LastAfterStopFlag";
        [Display(Name = "Last後日今ｽﾄフラグ")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string LastAfterStopFlag { get; set; }
    }
}