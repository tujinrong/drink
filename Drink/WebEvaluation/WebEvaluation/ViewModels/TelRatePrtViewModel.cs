using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.ViewModels
{
    public class TelRateRptViewModel
    {

        [Display(Name = "事業部コード")]
        public string DivCD { get; set; }

        [Display(Name = "事業部名")]
        public string DivName { get; set; }

        [Display(Name = "グループコード")]
        public string GroupCD { get; set; }

        [Display(Name = "グループ名")]
        public string GroupName { get; set; }

        [Display(Name = "店舗コード")]
        public string ShopCD { get; set; }

        [Display(Name = "社員番号")]
        public string StaffCD { get; set; }

        [Display(Name = "氏名")]
        public string StaffName { get; set; }

        [Display(Name = "挙式日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime PartyDate { get; set; }

        [Display(Name = "総数")]
        public int Count { get; set; }

        [Display(Name = "繋がった")]
        public int Connected { get; set; }

        [Display(Name = "繋がらず")]
        public int NoConnected { get; set; }

        [Display(Name = "通話率")]
        public string Rate { get; set; }

        [Display(Name = "通話率")]
        public double RateForOrder { get; set; }


        //[Display(Name = "一回目総数")]
        //public int FirstCount { get; set; }

        //[Display(Name = "一回目繋がった")]
        //public int FirstConnected { get; set; }

        //[Display(Name = "一回目繋がらず")]
        //public int FirstNoConnected { get; set; }

        //[Display(Name = "一回目通話率")]
        //public string FirstRate { get; set; }

        //[Display(Name = "二回目総数")]
        //public int SecondCount { get; set; }

        //[Display(Name = "二回目繋がった")]
        //public int SecondConnected { get; set; }

        //[Display(Name = "二回目繋がらず")]
        //public int SecondNoConnected { get; set; }

        //[Display(Name = "二回目通話率")]
        //public string SecondRate { get; set; }

        //[Display(Name = "三回目総数")]
        //public int ThirdCount { get; set; }

        //[Display(Name = "三回目繋がった")]
        //public int ThirdConnected { get; set; }

        //[Display(Name = "三回目繋がらず")]
        //public int ThirdNoConnected { get; set; }

        //[Display(Name = "三回目通話率")]
        //public string ThirdRate { get; set; }
    }
}