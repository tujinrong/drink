//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  システムテーブル
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
    /// システムテーブル
    /// </summary>
    public class M_System : ModelBase
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "カスタマセンター組織コード(A000000040)")]
        [StringLength(UNITCD_LEN)]
        [Column(TypeName = "Varchar")]
        public string CusCenterUnitCD { get; set; }

        //クライアントアクセス制限
        [Display(Name = "IPアドレス")]
        [MaxLength(15)]
        [Column(TypeName = "Varchar")]
        public string AccessIP { get; set; }

        //管理者アクセス制限
        [Display(Name = "IPアドレス")]
        [MaxLength(15)]
        [Column(TypeName = "Varchar")]
        public string AdminIP { get; set; }

        [Display(Name = "リスト表示行数")]
        public int PageRowCount { get; set; }

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