//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  店舗マスタ。
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace DrinkService.Models
{
    /// <summary>
    /// 店舗マスタ
    /// </summary>
    public class M_Shop : ModelBase
    {
        public const string I店舗コード = "ShopCD";

        [Key]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        public const string I所属店舗区分 = "ShopTypeCD";
        [Required]
        [Display(Name = "所属店舗区分")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string ShopTypeCD { get; set; }

        public const string I店舗名 = "ShopName";
        [Required]  
        [Display(Name = "店舗名")]
        [StringLength(30)]
        public string ShopName { get; set; }

        public const string I地域コード = "RegionCD";
        [Display(Name = "地域コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(3)]
        public string RegionCD { get; set; }


        public const string Iエリアコード = "AreaCD";
        [Display(Name = "エリアコード")]
        [Column(TypeName = "Varchar")]
        [StringLength(3)]
        public string AreaCD { get; set; }


        public const string Iシステム導入日 = "SystemStartDate";
        [Display(Name = "システム導入日")]
        public DateTime? SystemStartDate { get; set; }

        public const string I店舗業務区分 = "SysTypeCD";
        [Display(Name = "店舗業務区分")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string SysTypeCD { get; set; }

        public const string I納品書伝票番号 = "LastSlipNO";
        [Display(Name = "納品書伝票番号")]
        public decimal? LastSlipNO { get; set; }

        //---------------------------------------------------------------------
        //システム共通項目
        //---------------------------------------------------------------------
        public const string I更新者 = "UpdateUser";
        [Display(Name = "更新者")]
        [StringLength(STAFFNAME_LEN)]
        public string UpdateUser { get; set; }

        public const string I更新日時 = "UpdateTime";
        [Display(Name = "更新日時")]
        public DateTime? UpdateTime { get; set; }



    }
}