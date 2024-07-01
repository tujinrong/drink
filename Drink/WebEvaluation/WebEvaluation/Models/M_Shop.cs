//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  店舗マスタ。
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
    /// 店舗マスタ
    /// </summary>
    public class M_Shop : ModelBase
    {

        [Key]
        [Display(Name = "店舗略称")]
        [Column(TypeName = "Varchar")]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        [Required]  
        [Display(Name = "店舗名")]
        [StringLength(100)]
        public string ShopName { get; set; }

        [Display(Name = "グループ")]
        [Column(TypeName = "Varchar")]
        [StringLength(GROUPCD_LEN)]
        public string GroupCD { get; set; }

        [Display(Name = "形態")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string ShopType { get; set; }

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