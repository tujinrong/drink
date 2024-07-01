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

namespace DrinkService.Models
{
    /// <summary>
    /// 名称テーブル
    /// </summary>
    public class M_Code　: ModelBase
    {
        [Key]
        [Display(Name = "コード種別")]
        [Column(TypeName = "Varchar", Order = 1)]
        [StringLength(20)]
        public string Kind { get; set; }

        [Key]
        [Display(Name = "コード")]
        [Column(TypeName = "Varchar", Order = 2)]
        [StringLength(20)]
        public string CD { get; set; }

        [Display(Name = "名称")]
        [StringLength(20)]
        public string Name { get; set; }

        [Display(Name = "関連数値")]
        [Column(TypeName = "numeric")]
        public decimal? RefNo { get; set; }

        [Display(Name = "関連コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(4)]
        public string RefCD { get; set; }


        [Display(Name = "備考")]
        [StringLength(20, ErrorMessage = "20文字まで入力してください。")]
        public string Memo { get; set; }

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