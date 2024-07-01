
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebEvaluation.Models;

namespace WebEvaluation.ViewModels
{
    public class HomeViewModel
    {
        [Display(Name = "社員名")]
        public string StaffName { get; set; }

        [Display(Name = "所属")]
        public string UnitName { get; set; }

        [Display(Name = "権限")]
        public string RoleName { get; set; }

        public int MessageID { get; set; }

        [Display(Name = "メッセージ")]
        public string Message { get; set; }

        [Display(Name = "更新者")]
        public string UpdateUser { get; set; }

        [Display(Name = "更新日時")]
        public string UpdateTime { get; set; }
    }
}