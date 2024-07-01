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
        public const string I所属店舗 = "ShopCD";
        public const string I社員番号 = "StaffCD";
        public const string I氏名 = "StaffName";
        public const string I役割 = "RoleCD";

        [Key]
        [Required]
        [Display(Name = "所属店舗")]
        [Column(TypeName = "Varchar", Order = 1)]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        [Key]
        [Required]
        [Display(Name = "社員番号")]
        [Column(TypeName = "Varchar", Order=2)]
        [StringLength(STAFFCD_LEN)]
        public string StaffCD { get; set; }

        [Required]
        [Display(Name = "氏名")]
        [StringLength(10)]
        public string StaffName { get; set; }

        [Display(Name = "パスワード")]
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "役割")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string RoleCD { get; set; }

        [Display(Name = "組織員コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(8)]
        public string SosikinCD { get; set; }

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