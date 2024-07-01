//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  顧客マスタ。
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
    /// 店舗マスタ
    /// </summary>
    public class M_Client : ModelBase
    {
        public const string I顧客名 = "ClientName";
        public const string I住所 = "Address";

        [Key]
        [Required]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar", Order = 1)]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        [Key]
        [Display(Name = "顧客コード")]
        [Column(TypeName = "Varchar", Order = 2)]
        [StringLength(CLIENTCD_LEN)]
        public string ClientCD { get; set; }


        [Required]
        [Display(Name = "顧客名")]
        [StringLength(40)]
        public string ClientName { get; set; }

        [Display(Name = "顧客カナ")]
        [StringLength(40)]
        public string ClientKana { get; set; }

        [Display(Name = "郵便番号")]
        [Column(TypeName = "Varchar")]
        [StringLength(8)]
        public string PostCD { get; set; }

        [Display(Name = "住所")]
        [StringLength(80)]
        public string Address { get; set; }

        [Display(Name = "電話番号")]
        [Column(TypeName = "Varchar")]
        [StringLength(16)]
        public string Tel { get; set; }

        [Display(Name = "FAX番号")]
        [Column(TypeName = "Varchar")]
        [StringLength(16)]
        public string Fax { get; set; }


        [Display(Name = "顧客担当者")]
        [StringLength(20)]
        public string CustomerTanto { get; set; }

        [Display(Name = "従業員数")]
        [StringLength(10)]
        public string EmploeeNum { get; set; }

        [Display(Name = "担当者コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(STAFFCD_LEN)]
        public string TantoCD { get; set; }

        [Display(Name = "備考")]
        [StringLength(200)]
        public string Memo { get; set; }

        [Display(Name = "設置日")]
        public DateTime? FirstDate { get; set; }
        
        [Display(Name = "最後SEQ")]
        public int? LastSeq { get; set; }
     
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