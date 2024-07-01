//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  カスタマセンター担当者評価
//  
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.Models
{
    /// <summary>
    /// 挙式テーブル
    /// </summary>
    public class T_Party : ModelBase
    {
        public const int HALLTYPE_LEN = 10;
        //---------------------------------------------------------------------
        //基本情報
        //---------------------------------------------------------------------
        [Key]
        public int PartyID { get; set; }

        [Required]
        [Display(Name = "年度")]
        public int Year { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "パーティID")]
        [Column(TypeName="Varchar")]
        public string PartyNo { get; set; }

        [Display(Name = "店舗略称")]
        [StringLength(SHOPCD_LEN)]
        [Column(TypeName = "Varchar")]
        public string ShopCD { get; set; }

        //店舗種別　VH　WH　　漢字対応
        [Display(Name = "店舗種別")]
        //[Column(TypeName = "Varchar")]
        [StringLength(HALLTYPE_LEN)]
        public string HallType { get; set; }

        [Display(Name = "挙式日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime PartyDate { get; set; }

        //15:50 2014/8/18 追加
        [Display(Name = "開始時間")]
        [StringLength(5)]
        public string StartTime { get; set; }

        [Required]
        [StringLength(STAFFCD_LEN)]
        [Display(Name = "担当者")]
        [Column(TypeName = "Varchar")]
        public string TantoCD { get; set; }

        //---------------------------------------------------------------------
        //新郎、新婦
        //---------------------------------------------------------------------
        [StringLength(CUSNAME_LEN)]
        [Display(Name = "新郎氏名")]
        public string BrideName { get; set; }

        [StringLength(CUSNAME_LEN)]
        [Display(Name = "新郎かな")]
        public string BrideKana { get; set; }

        [StringLength(TEL_LEN)]
        [Display(Name = "新郎自宅TEL")]
        [DataType(DataType.PhoneNumber)]
        public string BrideHomeTel { get; set; }

        [StringLength(TEL_LEN)]
        [Display(Name = "新郎携帯TEL")]
        [DataType(DataType.PhoneNumber)]
        public string BrideMobile { get; set; }

        [Display(Name = "新婦氏名")]
        [StringLength(CUSNAME_LEN)]
        public string GroomName { get; set; }

        [Display(Name = "新婦カナ")]
        [StringLength(CUSNAME_LEN)]
        public string GroomKana { get; set; }

        [Display(Name = "新婦自宅TEL")]
        [StringLength(TEL_LEN)]
        [DataType(DataType.PhoneNumber)]
        public string GroomHomeTel { get; set; }

        [Display(Name = "新婦携帯TEL")]
        [StringLength(TEL_LEN)]
        [DataType(DataType.PhoneNumber)]
        public string GroomMobile { get; set; }

        //2014/8/18 削除       
        //[Display(Name = "状態")]
        //[StringLength(1)]
        //[Column(TypeName = "Varchar")]
        //public string EvaStatus { get; set; }


        [Display(Name = "完了")]
        public bool FinishFlag { get; set; }
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