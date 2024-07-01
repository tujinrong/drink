//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  メッセージ。
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
    /// メッセージ
    /// </summary>
    public class M_Message : ModelBase
    {

        [Key]
        public int MessageID { get; set; }

        [Display(Name = "メッセージ")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

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