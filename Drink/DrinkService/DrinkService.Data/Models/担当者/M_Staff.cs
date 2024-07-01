//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  担当者マスタ
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
    /// 社員マスタ
    /// </summary>
    public class M_Staff : ModelBase
    {

        public const string I店舗コード = "ShopCD";
        [Key]
        [Required]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar", Order = 1)]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        public const string I社員番号 = "StaffCD";
        [Key]
        [Required]
        [Display(Name = "社員番号")]
        [Column(TypeName = "Varchar", Order=2)]
        [StringLength(STAFFCD_LEN)]
        public string StaffCD { get; set; }

        public const string I氏名 = "StaffName";
        [Required]
        [Display(Name = "氏名")]
        [StringLength(10)]
        public string StaffName { get; set; }

        public const string Iパスワード = "Password";
        [Display(Name = "パスワード")]
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Password { get; set; }

        [Display(Name = "古いパスワード1")]
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string OldPassword1 { get; set; }

        [Display(Name = "古いパスワード2")]
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string OldPassword2 { get; set; }

        public const string I役割 = "RoleCD";
        [Required]
        [Display(Name = "役割")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string RoleCD { get; set; }

        public const string  I組織員コード="SosikinCD";
        [Display(Name = "組織員コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(8)]
        public string SosikinCD { get; set; }

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