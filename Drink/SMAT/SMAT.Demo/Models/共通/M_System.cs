//*****************************************************************************
// [システム]   ダスキン・配置ドリンク
// 
// [機能概要]  システムテーブル
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
    /// システムテーブル
    /// </summary>
    public class M_System : ModelBase
    {
        [Key]
        public int ID { get; set; }


        [Display(Name = "リスト表示行数")]
        public int PageRowCount { get; set; }

        [Display(Name = "社内カレンダー基準日")]
        public DateTime FirstWeekDate { get; set; }

        public short ProjNo { get; set; }
    }
}