using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.ViewModels
{
    public class TelRptViewModel
    {
        [Key]
        public int No { get; set; }

        [Display(Name = "店舗")]
        public string ShopCD { get; set; }

        [Display(Name = "店舗名")]
        public string ShopName { get; set; }

        [Display(Name = "グループコード")]
        public string GroupCD { get; set; }

        [Display(Name = "事業部コード")]
        public string DivisionCD { get; set; }

        [Display(Name = "パーティID")]
        public string PartyNo { get; set; }

        [Display(Name = "挙式日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime HoldDate { get; set; }

        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [Display(Name = "新郎氏名")]
        public string BrideName { get; set; }

        [Display(Name = "新婦氏名")]
        public string GroomName { get; set; }

        [Display(Name = "番号")]
        public string StaffCD { get; set; }

        [Display(Name = "担当者")]
        public string StaffName { get; set; }

        [Display(Name = "カナ")]
        public string StaffKana { get; set; }

        [Display(Name = "電話内容")]
        public string TelMemo { get; set; }

        [Display(Name = "特記事項")]
        public string TelMemo_special { get; set; }

        [Display(Name = "担当者評価")]
        public string StaffEva { get; set; }

        [Display(Name = "上長評価")]
        public string LeaderEva { get; set; }

        [Display(Name = "会場種別")]
        public string HallType { get; set; }

    }
}