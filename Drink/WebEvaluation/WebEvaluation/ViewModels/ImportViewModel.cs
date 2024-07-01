using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.ViewModels
{
    public class ImportViewModel
    {
        [Display(Name = "種類")]
        public string Type { get; set; }

        [Display(Name = "フィールド")]
        public List<string> Fields { get; set; }
    }
}