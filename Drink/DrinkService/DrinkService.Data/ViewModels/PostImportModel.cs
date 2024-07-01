using DrinkService.Models;
using DrinkService.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class PostImportModel : ModelBase
    {

        [Display(Name = "郵便番号")]
        [CSV(Output = true, FieldIndex=2)]
        public string PostCD { get; set; }

        [Display(Name = "住所1")]
        [CSV(Output = true, FieldIndex = 6)]
        public string Adress1 { get; set; }

        [Display(Name = "住所2")]
        [CSV(Output = true, FieldIndex = 7)]
        public string Adress2 { get; set; }

        [Display(Name = "住所3")]
        [CSV(Output = true, FieldIndex = 8)]
        public string Adress3 { get; set; }
    }
}
