//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  ユーザマスタ
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
    /// ユーザマスタ
    /// </summary>
    public class M_User : ModelBase
    {
        [Key]
        [Display(Name = "ユーザーID")]
        [StringLength(USERID_LEN)]
        public string UserID { get; set; }

        [Display(Name = "パスワード")]
        [DataType(DataType.Password)]
        [StringLength(20)]
        public string Password { get; set; }

        [Display(Name = "社員番号")]
        [StringLength(STAFFCD_LEN)]
        public string StaffCD { get; set; }

        //01:店舗  02:カスタマセンター 03:閲覧  09:システム管理者
        [Display(Name = "権限")]
        [Column(TypeName = "Varchar")]
        [StringLength(2)]
        public string RoleCD { get; set; }

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