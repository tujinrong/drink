using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class ClientInitItemViewModel : ModelBase
    {
        [Display(Name = "棚コード")]
        public string ShelfCD { get; set; }

        [Display(Name = "商品コード")]
        public string ItemCD { get; set; }

        [Display(Name = "商品名")]
        public string ShortName { get; set; }

        [Display(Name = "数量")]
        public decimal Num { get; set; }

        [Display(Name = "単価")]
        public decimal Price { get; set; }
    }
}
