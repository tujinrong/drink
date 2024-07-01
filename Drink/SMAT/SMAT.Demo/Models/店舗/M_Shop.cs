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
        public const string I所属店舗区分 = "ShopTypeCD";
        public const string I店舗名 = "ShopName";
        public const string I地域コード = "RegionCD";
        public const string I業務区分 = "SystemFlag";
        public const string I更新日時 = "UpdateTime";

        [Key]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        [Required]
        [Display(Name = "所属店舗区分")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string ShopTypeCD { get; set; }

        [Required]  
        [Display(Name = "店舗名")]
        [StringLength(30)]
        public string ShopName { get; set; }

        /*
        [Display(Name = "郵便番号")]
        [Column(TypeName = "Varchar")]
        [StringLength(8)]
        public string PostCD { get; set; }


        [Display(Name = "住所")]
        [StringLength(100)]
        public string Address { get; set; }
        */


        [Display(Name = "地域コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(3)]
        public string RegionCD { get; set; }

     
        [Display(Name = "エリアコード")]
        [Column(TypeName = "Varchar")]
        [StringLength(3)]
        public string AreaCD { get; set; }
        

        /*
             [Display(Name = "電話番号")]
             [Column(TypeName = "Varchar")]
             [StringLength(15)]
             public string Tel { get; set; }

             [Display(Name = "FAX")]
             [Column(TypeName = "Varchar")]
             [StringLength(15)]
             public string Fax { get; set; }
             */

        [Display(Name = "システム導入日")]
        public DateTime? SystemStartDay { get; set; }

        [Display(Name = "店舗業務区分")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string SystemFlag { get; set; }
        


        //---------------------------------------------------------------------
        //システム共通項目
        //---------------------------------------------------------------------
        [Display(Name = "更新者")]
        [Column(TypeName = "Varchar")]
        [StringLength(STAFFCD_LEN)]
        public string UpdateUserID { get; set; }

        [Display(Name = "更新日時")]
        public DateTime? UpdateTime { get; set; }



    }
}