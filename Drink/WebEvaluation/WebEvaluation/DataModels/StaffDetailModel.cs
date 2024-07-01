using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace WebEvaluation.DataModels
{
    public class StaffDetailModel
    {
        [Display(Name = "社員コード")]
        public string StaffCD { get; set; }

        [Display(Name = "氏名漢字")]
        public string StaffName { get; set; }

        [Display(Name = "氏名カナ")]
        public string StaffKana { get; set; }

        [Display(Name = "性別")]
        public string Sex { get; set; }

        [Display(Name = "入社年月日")]
        public string EnrollmentDate { get; set; }

        [Display(Name = "現在組織コード")]
        public string SosikiCD { get; set; }

        [Display(Name = "現在組織名称")]
        public string SosikiName { get; set; }

        [Display(Name = "現在役職正式名称")]
        public string Yakusyoku { get; set; }

        [Display(Name = "現在職種名称")]
        public string Duty { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}