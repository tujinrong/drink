using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string OldPassowrd { get; set; }

        public string NewPassWord { get; set; }

        public string NewPasswordConfirm { get; set; }
    }
}