using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace WebEvaluation.DataModels
{
    public class StaffData :Models.ModelBase
    {
        [Required]
        [Display(Name = "社員コード")]
        [StringLength(STAFFCD_LEN)]
        public string StaffCD { get; set; }

        [Required]
        [Display(Name = "氏名漢字")]
        [StringLength(10)]
        public string StaffName { get; set; }

        [Required]
        [Display(Name = "氏名カナ")]
        [StringLength(20)]
        public string StaffKana { get; set; }

        //1:男　2:女
        [Required]
        [Display(Name = "性別")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "入社年月日")]
        public string EnrollmentDate { get; set; }

        [Required]
        [Display(Name = "現在組織コード")]
        [StringLength(10)]
        public string SosikiCD { get; set; }

        [Required]
        [Display(Name = "現在組織名称")]
        [StringLength(20)]
        public string SosikiName { get; set; }

        [Required]
        [Display(Name = "現在役職正式名称")]
        [StringLength(30)]
        public string Yakusyoku { get; set; }

        [Required]
        [Display(Name = "現在職種名称")]
        [StringLength(30)]
        public string Duty { get; set; }


        [Display(Name = "Email")]
        [StringLength(200)]
        public string Email { get; set; }

    }
}