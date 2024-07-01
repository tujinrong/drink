using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class CodeListViewModel : ModelBase
    {
        [Display(Name = "種別")]
        public string Kind { get; set; }

        [Display(Name = "コード")]
        public string CD { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "関連数値")]
        public decimal? RefNo { get; set; }

        [Display(Name = "関連コード")]
        public string RefCD { get; set; }
    }
}
