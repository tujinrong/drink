
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebEvaluation.Models;
using WebEvaluation.Utils;

namespace WebEvaluation.ViewModels
{
    public class StaffViewModel
    {
        [Display(Name = "現在組織")]
        public string UnitName { get; set; }

        [Display(Name = "社員番号")]
        public string StaffCD { get; set; }

        [Display(Name = "氏名漢字")]
        public string StaffName { get; set; }

        [Display(Name = "氏名カナ")]
        public string StaffKana { get; set; }

        //1:男　2:女
        [Display(Name = "性別")]
        [CSV(Output = false)]
        public string Sex { get; set; }

        [Display(Name = "性別")]
        public string SexName { get; set; }

        [Display(Name = "入社年月日")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? EnrollmentDate { get; set; }

        [Display(Name = "役職")]
        [StringLength(10)]
        public string Yakusyoku { get; set; }

        [Display(Name = "職種")]
        public string Duty { get; set; }

        //所属CD　→ 組織マスタ　→　店舗マスタ
        [Display(Name = "所属")]
        [CSV(Output = false)]
        public string UnitCD { get; set; }

        //01:店舗  02:カスタマセンター  03:上長  04:システム管理者
        [Display(Name = "権限")]
        public string RoleCD { get; set; }

        [Display(Name = "E-Mail")]
        public string EMail { get; set; }

       
    }
}