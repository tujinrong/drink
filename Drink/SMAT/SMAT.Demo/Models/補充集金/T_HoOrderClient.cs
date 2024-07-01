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
        public const string I初回 = "FirstFlag";
        public const string I済 = "DoneFlag"; 

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



        [Key]
        [Required]
        [Display(Name = "顧客コード")]
        [Column(TypeName = "Varchar", Order=4)]
        [StringLength(CLIENTCD_LEN)]
        public string ClientCD { get; set; }

        [Display(Name = "初回フラグ")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
         public string FirstFlag { get; set; }

        [Display(Name = "済フラグ")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string DoneFlag { get; set; }


        //未使用
        [Display(Name = "補充SEQ")]
        public int Seq { get; set; }

            
    }
}