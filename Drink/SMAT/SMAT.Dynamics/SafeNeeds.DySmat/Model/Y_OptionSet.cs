//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  名称テーブル
//  
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace SafeNeeds.DySmat.Model
{
    /// <summary>
    /// 名称テーブル
    /// </summary>
    public class Y_OptionSet : DyModelBase
    {
        [Key, Column(Order = 1)]
        public int ProjID { get; set; }

        [Display(Name = "カルチャー")]
        [Key, Column(TypeName = "Varchar", Order = 2)]
        [StringLength(20)]
        public string Culture { get; set; }

        [Display(Name = "コード種別")]
        [Key, Column(TypeName = "Varchar", Order = 3)]
        [StringLength(40)]
        public string OptSetName { get; set; }

        [Display(Name = "コード")]
        [Key, Column(TypeName = "Varchar", Order = 4)]
        [StringLength(40)]
        public string CD { get; set; }

        [Display(Name = "名称")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "関連数値")]
        [Column(TypeName = "numeric")]
        public decimal? RefNum { get; set; }

        [Display(Name = "関連コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(4)]
        public string RefCD { get; set; }

        [Display(Name = "備考")]
        [StringLength(20, ErrorMessage = "20文字まで入力してください。")]
        public string Memo { get; set; }

        public short? Seq { get; set; }

        public string LogicType { get; set; }

    }



}