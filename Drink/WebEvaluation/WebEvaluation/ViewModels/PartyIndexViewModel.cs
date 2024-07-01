using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebEvaluation.Models;
using WebEvaluation.Utils;

namespace WebEvaluation.ViewModels
{
    public class PartyIndexViewModel
    {

        [Display(Name = "パーティID")]
        [CSV(Output = false)]
        public int PartyID { get; set; }

        [Display(Name = "パーティID")]
        [CSV(Output = false)]
        public string PartyNo { get; set; }

        [Display(Name = "店舗")]
        public string ShopCD { get; set; }

        [Display(Name = "店舗")]
        [CSV(Output = false)]
        public string ShopName { get; set; }

        [Display(Name = "挙式日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime PartyDate { get; set; }

        [Display(Name = "会場種別")]
        public string HallType { get; set; }

        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [Display(Name = "担当者コード")]
        [CSV(Output = false)]
        public string StaffCD { get; set; }

        [Display(Name = "担当者")]
        public string TantoCD { get; set; }

        [Display(Name = "新郎氏名")]
        public string BrideName { get; set; }

        [Display(Name = "新婦氏名")]
        public string GroomName { get; set; }

        [Display(Name = "新郎かな")]
        [CSV(Output = false)]
        public string BrideKana { get; set; }

        [Display(Name = "新婦かな")]
        [CSV(Output = false)]
        public string GroomKana { get; set; }

        [Display(Name = "自宅電話番号（新郎）")]
        [CSV(Output = false)]
        public string BrideHomeTel { get; set; }

        [Display(Name = "携帯電話番号（新郎）")]
        [CSV(Output = false)]
        public string BrideMobile { get; set; }

        [Display(Name = "自宅電話番号（新婦）")]
        [CSV(Output = false)]
        public string GroomHomeTel { get; set; }

        [Display(Name = "携帯電話番号（新婦）")]
        [CSV(Output = false)]
        public string GroomMobile { get; set; }

        //0:未, 1:可, 2:否
        [Display(Name = "電話可否")]
        [StringLength(1)]
        public string TelFlg { get; set; }

        [Display(Name = "留意")]
        public string CareFlg { get; set; }

        [Display(Name = "評価")]
        public string StatffEva { get; set; }

        [Display(Name = "📞")]
        public bool FinishFlag { get; set; }

        [Display(Name = "🍴")]
        public string ReportState { get; set; }

        [Display(Name = "二次評価")]
        [CSV(Output = false)]
        public string LeaderEva { get; set; }
    }
}