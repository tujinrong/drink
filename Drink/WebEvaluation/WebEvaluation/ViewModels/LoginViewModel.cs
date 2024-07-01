using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.ViewModels
{
    public class LoginViewModel
    {

        [Display(Name = "ログインID")]
        [StringLength(5)]
        public string LoginName { get; set; }

        [StringLength(20)]
        [Display(Name = "パスワード")]
        public string PassWord { get; set; }

    }
}