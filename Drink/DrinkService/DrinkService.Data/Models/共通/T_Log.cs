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
    /// Log
    /// </summary>
    public class T_Log : ModelBase
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogID { get; set; }

        [StringLength(20)]
        public string LogType { get; set; }

        [StringLength(40)]
        public string FunctionName { get; set; }

        [StringLength(80)]
        public string FunctionPath { get; set; }

        [StringLength(20)]
        public string FunctionType { get; set; }

        [StringLength(20)]
        public string Key1 { get; set; }

        [StringLength(20)]
        public string Key2 { get; set; }

        [StringLength(20)]
        public string Key3 { get; set; }

        [StringLength(20)]
        public string Key4 { get; set; }

        [StringLength(20)]
        public string Key5 { get; set; }

        public string Content { get; set; }

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