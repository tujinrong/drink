//*****************************************************************************
// [システム]  ダスキン・配置ドリンク
// 
// [機能概要]  メッセージ。
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
    /// メッセージ
    /// </summary>
    public class T_Message : ModelBase
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
        [StringLength(STAFFNAME_LEN)]
        public string UpdateUser { get; set; }

        [Display(Name = "更新日時")]
        public DateTime? UpdateTime { get; set; }



    }
}