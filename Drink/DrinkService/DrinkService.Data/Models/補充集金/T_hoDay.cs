//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  
//
// [作成履歴]　2015/02/25  屠錦栄　初版 
//
// [レビュー]　2015/03/17  屠錦栄　 
//*****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

using System.Collections.Generic;

namespace DrinkService.Models
{
    /// <summary>
    /// 日別処理
    /// </summary>
    public class T_HoDay : ModelBase
    {

        [Key]
        [Required]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar", Order=1)]
        [StringLength(SHOPCD_LEN)]

        public string ShopCD { get; set; }

        [Key]
        [Required]
        [Display(Name = "日付")]
        [Column(Order = 2)]
        public DateTime HoDate { get; set; }

        public const string Iルート = "Route";
        [Display(Name = "ルート")]
        [Column(TypeName = "Varchar")]
        [StringLength(4)]
        public string Route { get; set; }

        
        [Display(Name = "ダウンロード者")]
        [StringLength(STAFFNAME_LEN)]
        public string DownloadStaffName { get; set; }

        [Display(Name = "ダウンロード日時")]
        public DateTime? DownloadDate { get; set; }

        //---------------------------------------------------------------------
        //システム共通項目
        //---------------------------------------------------------------------
        [Display(Name = "更新者")]
        [StringLength(STAFFNAME_LEN)]
        public string UpdateUser { get; set; }

        [Display(Name = "更新日時")]
        public DateTime? UpdateTime { get; set; }


        public List<T_HoOrderClient> Detail;

        public List<T_HoOrderTanto> DetailTanto;



        public Key GetKey()
        {
            Key key=new Key();
            
            key.HoDate=HoDate;
            key.ShopCD=ShopCD;

            return key;
        }

        public class Key
        {
            public string ShopCD;
            public DateTime HoDate; 
        }
    }
}