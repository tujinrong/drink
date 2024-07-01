//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  組織マスタ。
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
    /// 組織マスタ
    /// </summary>
    public class S_Unit : ModelBase
    {
        [Key]
        [Display(Name = "組織コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(UNITCD_LEN)]
        public string UnitCD { get; set; }

        [Display(Name = "組織名")]
        [StringLength(20)]
        public string UnitName { get; set; }

        [Display(Name = "店舗")]
        [Column(TypeName = "Varchar")]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        public bool IsCusCenter { get; set; }

        public bool IsShop { get; set; }

    }    
}