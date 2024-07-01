//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  補充
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
    /// 初期キットマスタ
    /// </summary>
    public class T_HoClient : ModelBase
    {

        public const string I店舗コード = "ShopCD";
        [Key]
        [Required]
        [Display(Name = "店舗コード")]
        [Column(TypeName = "Varchar",Order=1)]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        public const string I顧客コード = "ClientCD";
        [Key]
        [Required]
        [Display(Name = "顧客コード")]
        [Column(TypeName = "Varchar",Order=2)]
        [StringLength(CLIENTCD_LEN)]
        public string ClientCD { get; set; }

        public const string I補充回数 = "Seq";
        [Key]
        [Required]
        [Display(Name = "補充回数")]
        [Column(Order = 3)]
        public int Seq { get; set; }

        public const string I補充日 = "HoDate";
        [Required]
        [Display(Name = "補充日")]
        public DateTime HoDate { get; set; }

        public const string Iルート = "Route";
        [Required]
        [Display(Name = "ルート")]
        [StringLength(ROUTE_LEN)]
        public string Route { get; set; }

        public const string I担当者 = "TantoCD";
        [Required]
        [Display(Name = "担当者")]
        [StringLength(STAFFCD_LEN)]
        public string TantoCD { get; set; }

        public const string I売上 = "SoldMoney";
        [Display(Name = "売上")]
        [Column(TypeName = "numeric")]
        public decimal? SoldMoney { get; set; }

        public const string I集金額 = "GetMoney";
        [Display(Name = "集金額")]
        [Column(TypeName = "numeric")]
        public decimal? GetMoney { get; set; }

        public const string I過不足 = "DiffMoney";
        [Display(Name = "過不足")]
        [Column(TypeName = "numeric")]
        public decimal? DiffMoney { get; set; }

        public const string I備考 = "Memo";
        [Display(Name = "備考")]
        [StringLength(200)]
        public string Memo { get; set; }

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

        public List<T_HoClientItem> Detail;

        public Key GetKey()
        {
            return new Key { ClientCD = ClientCD, ShopCD = ShopCD, Seq = Seq };
        }

        public class Key
        {
            public string ShopCD;
            public string ClientCD;
            public int Seq;
        }

    }
}