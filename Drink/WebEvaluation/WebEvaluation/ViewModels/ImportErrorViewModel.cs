using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.ViewModels
{
    public class ImportErrorViewModel
    {

        [Display(Name = "エラーの種類")]
        public string ErrorType { get; set; }

        [Display(Name = "エラーフィールド")]
        public string ErrorField { get; set; }

        [Display(Name = "エラー詳細")]
        public string ErrorDetail { get; set; }

        [Display(Name = "エラー行番号")]
        public int ErrorRow { get; set; }
    }
}