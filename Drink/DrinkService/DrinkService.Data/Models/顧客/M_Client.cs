//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  顧客マスタ。
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
    /// 店舗マスタ
    /// </summary>
    public class M_Client : ModelBase
    {
        public const string I店舗コード = "ShopCD";
        [Display(Name = "店舗コード")]
        [Key, Required, Column(TypeName = "Varchar", Order = 1)]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        public const string I顧客コード = "ClientCD";
        [Display(Name = "顧客コード")]
        [Key, Required, Column(TypeName = "Varchar", Order = 2)]
        [StringLength(CLIENTCD_LEN)]
        public string ClientCD { get; set; }


        public const string I顧客名 = "ClientName";
        [Display(Name = "顧客名")]
        [Required, StringLength(40)]
        public string ClientName { get; set; }

        public const string I顧客カナ = "ClientKana";
        [Display(Name = "顧客カナ")]
        [StringLength(40)]
        public string ClientKana { get; set; }

        public const string I郵便番号 = "PostCD";
        [Display(Name = "郵便番号")]
        [Column(TypeName = "Varchar")]
        [StringLength(8)]
        public string PostCD { get; set; }

        public const string I住所 = "Address";
        [Display(Name = "住所")]
        [StringLength(80)]
        public string Address { get; set; }

        public const string I電話番号 = "Tel";
        [Display(Name = "電話番号")]
        [Column(TypeName = "Varchar")]
        [StringLength(16)]
        public string Tel { get; set; }

        public const string IFAX番号 = "Fax";
        [Display(Name = "FAX番号")]
        [Column(TypeName = "Varchar")]
        [StringLength(16)]
        public string Fax { get; set; }

        public const string I顧客担当者 = "CustomerTanto";
        [Display(Name = "顧客担当者")]
        [StringLength(20)]
        public string CustomerTanto { get; set; }

        public const string I従業員数 = "EmploeeNum";
        [Display(Name = "従業員数")]
        [StringLength(10)]
        public string EmploeeNum { get; set; }

        public const string I担当者コード = "TantoCD";
        [Display(Name = "担当者コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(STAFFCD_LEN)]
        public string TantoCD { get; set; }

        public const string I備考 = "Memo";
        [Display(Name = "備考")]
        [StringLength(200)]
        public string Memo { get; set; }

        public const string I設置日 = "FirstDate";
        [Display(Name = "設置日")]
        public DateTime? FirstDate { get; set; }

        public const string I回数 = "LastSeq";
        [Display(Name = "回数")]
        public int LastSeq { get; set; }

        public const string IキットID = "KitID";
        [Display(Name = "キットID")]
        public int? KitID { get; set; }
     
        public const string I販売管理顧客コード = "KanriClientCD";
        [Display(Name = "販売管理顧客コード")]
        [StringLength(CLIENTCD_LEN)]
        public string KanriClientCD { get; set; }

        public const string I後日 = "AfterDate";
        [Display(Name = "後日")]
        public DateTime? AfterDate { get; set; }

        public const string I取引種別 = "TransactionType";
        public string TransactionType { get; set; }


        public const string I後日今ｽﾄフラグ = "LastAfterStopFlag";
        [Display(Name = "後日今ｽﾄフラグ")]
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string LastAfterStopFlag { get; set; }

        public const string I後日今ｽﾄ日 = "AfterDate";
        [Display(Name = "後日今ｽﾄ日")]
        public DateTime? AfterStopVisitDate { get; set; }

        public const string I解約フラグ = "CancelFlg";
        [Display(Name = "解約フラグ")]
        public Boolean? CancelFlg { get; set; }
     
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


        public List<M_ClientInitItems> InitItemDetail;
        public List<M_ClientInitItems> clientInitItemDetail;
        public List<M_ClientRoute> RouteDetail;
        public List<T_HoClientItem> HoDetail;
        public T_HoClient HoClient;

        public Key GetKey()
        {
            return new Key { ShopCD = ShopCD, ClientCD = ClientCD };
        }

        public class Key
        {
            public string ShopCD;

            public string ClientCD;

        }
    }
}