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
    public class ItemKitListViewModel : ModelBase
    {
        
        [Display(Name = "店舗コード")]
        [CSV(Output = false)]
        public string ShopCD { get; set; }

        [Display(Name = "店舗名")]
        public string ShopName { get; set; }

        [Display(Name = "キットID")]
        [CSV(Output = false)]
        public int KitID { get; set; }

        [Display(Name = "キット名")]
        public string KitName { get; set; }
    }
}
