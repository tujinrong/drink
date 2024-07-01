//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  顧客ルート。
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
    /// 顧客ルート
    /// </summary>
    public class M_ClientRoute : ModelBase
    {

        [Key]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar", Order=1)]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        [Key]
        [Required]  
        [Display(Name = "顧客コード")]
        [Column(TypeName = "Varchar", Order=2)]
        [StringLength(CLIENTCD_LEN)]
        public string ClientCD { get; set; }

        [Key]
        [Required]
        [Display(Name = "週")]
        [Column( Order = 3)]
        public int WeekNo { get; set; }

        [Key]
        [Required]
        [Display(Name = "ルートNo")]
        [Column(Order = 4)]
        public int RouteNo { get; set; }

        [Display(Name = "ルート")]
        [Column(TypeName = "Varchar")]
        [StringLength(ROUTE_LEN)]
        public string Route { get; set; }

        public decimal WeekDayNo { get; set; }
    }
}