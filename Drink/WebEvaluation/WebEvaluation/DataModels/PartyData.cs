using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;
using WebEvaluation.Models;
using WebEvaluation.Utils;

namespace WebEvaluation.DataModels
{
    public class PartyData :Models.ModelBase
    {
        [Required]
        [StringLength(10)]
        [Display(Name = "パーティID")]
        public string PartyNo { get; set; }

        [Required]
        [StringLength(SHOPCD_LEN)]
        [Display(Name = "店舗略称")]
        public string ShopCD { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "パーティ年月日")]
        public string PartyDate { get; set; }

        [Required]
        [StringLength(STAFFCD_LEN)]
        [Link(linkText = "社員登録", routeReg = "../Staff/IframeCreate?_staffCD=#TantoCD#&_staffName=#StaffName#&ShopCD=#ShopCD#")]
        [Display(Name = "担当者コード")]
        public string TantoCD { get; set; }

        [Required]
        [Display(Name = "担当者名")]
        [StringLength(10)]
        public string StaffName { get; set; }

        [Required]
        [StringLength(CUSNAME_LEN)]
        [Display(Name = "新郎氏名")]
        public string BrideName { get; set; }

        [Required]
        [StringLength(CUSNAME_LEN)]
        [Display(Name = "新婦氏名")]
        public string GroomName { get; set; }

        [Required]
        [StringLength(CUSNAME_LEN)]
        [Display(Name = "新郎かな")]
        public string BrideKana { get; set; }

        [Required]
        [StringLength(CUSNAME_LEN)]
        [Display(Name = "新婦かな")]
        public string GroomKana { get; set; }

        [Required]
        [StringLength(TEL_LEN)]
        [Display(Name = "自宅電話番号（新郎）")]
        public string BrideHomeTel { get; set; }

        [Required]
        [StringLength(TEL_LEN)]
        [Display(Name = "携帯電話番号（新郎）")]
        public string BrideMobile { get; set; }

        [Required]
        [StringLength(TEL_LEN)]
        [Display(Name = "自宅電話番号（新婦）")]
        public string GroomHomeTel { get; set; }

        [Required]
        [StringLength(TEL_LEN)]
        [Display(Name = "携帯電話番号（新婦）")]
        public string GroomMobile { get; set; }

        [Required]
        [StringLength(8)]
        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [Required]
        [StringLength(T_Party.HALLTYPE_LEN)]
        [Display(Name = "会場種類")]
        public string HallType { get; set; }
        
    }
}