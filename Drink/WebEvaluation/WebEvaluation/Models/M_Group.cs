//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  グループマスタ。
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
    /// グループ
    /// </summary>
    public class M_Group : ModelBase
    {



        [Key]
        [Display(Name = "グループコード")]
        [Column(TypeName = "Varchar")]
        [StringLength(GROUPCD_LEN)]
        public string GroupCD { get; set; }


        [Display(Name = "グループ名")]
        [StringLength(10)]
        public string GroupName { get; set; }

        ////この項目を削除 2014/07/28
        //[Display(Name = "エリア")]
        //[Column(TypeName = "Varchar")]
        //[StringLength(AREACD_LEN)]
        //public string AreaCD { get; set; }

        //
        [Display(Name = "事業部コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(DIVCD_LEN)]
        public string DivCD { get; set; }

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