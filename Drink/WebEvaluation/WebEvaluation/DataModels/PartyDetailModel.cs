using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;
using WebEvaluation.Models;
using WebEvaluation.Utils;

namespace WebEvaluation.DataModels
{
    public class PartyDetailModel
    {
        [Display(Name = "店舗略称")]
        public string ShopCD { get; set; }

        [Display(Name = "店舗名")]
        public string ShopName { get; set; }

        [Display(Name = "会場種類")]
        public string HallType { get; set; }

        [Display(Name = "パーティID")]
        public string PartyNo { get; set; }

        [Display(Name = "パーティ年月日")]
        public string PartyDate { get; set; }

        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [Display(Name = "担当者コード")]
        public string TantoCD { get; set; }

        [Display(Name = "担当者名")]
        public string StaffName { get; set; }

        [Display(Name = "新郎氏名")]
        public string BrideName { get; set; }

        [Display(Name = "新郎かな")]
        public string BrideKana { get; set; }

        [Display(Name = "自宅電話番号（新郎）")]
        public string BrideHomeTel { get; set; }

        [Display(Name = "携帯電話番号（新郎）")]
        public string BrideMobile { get; set; }

        [Display(Name = "新婦氏名")]
        public string GroomName { get; set; }

        [Display(Name = "新婦かな")]
        public string GroomKana { get; set; }

        [Display(Name = "自宅電話番号（新婦）")]
        public string GroomHomeTel { get; set; }

        [Display(Name = "携帯電話番号（新婦）")]
        public string GroomMobile { get; set; }
        
    }
}