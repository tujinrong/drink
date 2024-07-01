//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  社員マスタ
//
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace WebEvaluation.Models
{
    /// <summary>
    /// 社員マスタ
    /// </summary>
    public class M_Staff : ModelBase
    {
        public const int NAME_LEN = 10;
        public const int KANA_LEN = 20;
        public const int YAKU_LEN = 30;
        public const int DUTY_LEN = 30;

        public const int EMAIL_LEN = 200;

        [Key]
        [Required]
        [Display(Name = "社員番号")]
        [Column(TypeName = "Varchar")]
        [StringLength(STAFFCD_LEN)]
        public string StaffCD { get; set; }

        [Required]
        [Display(Name = "氏名")]
        [StringLength(NAME_LEN)]
        public string StaffName { get; set; }

        [Display(Name = "カナ")]
        [StringLength(KANA_LEN)]
        public string StaffKana { get; set; }

        //1:男　2:女
        [Required]
        [Display(Name = "性別")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string Sex { get; set; }

        [Display(Name = "入社日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EnrollmentDate { get; set; }

        [Display(Name = "役職")]
        [StringLength(YAKU_LEN)]
        public string Yakusyoku { get; set; }

        [Display(Name = "職種")]
        [StringLength(DUTY_LEN)]
        public string Duty { get; set; }

        //所属CD　→ 組織マスタ　→　店舗マスタ
        [Required]
        [Display(Name = "所属")]
        [StringLength(UNITCD_LEN)]
        public string UnitCD { get; set; }

        [Display(Name = "Email")]
        [StringLength(EMAIL_LEN)]
        public string Email { get; set; }

        //---------------------------------------------------------------------
        //システム共通項目
        //---------------------------------------------------------------------
        [Display(Name = "更新者")]
        [Column(TypeName = "Varchar")]
        [StringLength(USERID_LEN)]
        public string UpdateUserID { get; set; }

        [Display(Name = "更新日時")]
        public DateTime? UpdateTime { get; set; }


    }
}