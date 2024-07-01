using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebEvaluation.Models;
using WebEvaluation.Utils;

namespace WebEvaluation.ViewModels
{
    public class EvaBystaffIndexViewModel
    {

        [Display(Name = "パーティID")]
        [CSV(Output = false)]
        public int PartyID { get; set; }

        [Display(Name = "店舗")]
        public string ShopCD { get; set; }

        [Display(Name = "グループコード")]
        [CSV(Output = false)]
        public string GroupCD { get; set; }

        [Display(Name = "事業部コード")]
        [CSV(Output = false)]
        public string DivisionCD { get; set; }
        
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

        [Display(Name = "番号")]
        public string StaffCD { get; set; }

        [Display(Name = "担当者")]
        public string TantoCD { get; set; }

        [Display(Name = "新郎氏名")]
        public string BrideName { get; set; }

        [Display(Name = "新婦氏名")]
        public string GroomName { get; set; }

        //0:未, 1:可, 2:否
        [Display(Name = "電話可否")]
        [StringLength(1)]
        public string TelFlg { get; set; }

        [Display(Name = "評価")]
        public string StatffEva { get; set; }

        [Display(Name = "二次評価")]
        public string LeaderEva { get; set; }

        [Display(Name = "完了")]
        public bool FinishFlag { get; set; }


    }
}