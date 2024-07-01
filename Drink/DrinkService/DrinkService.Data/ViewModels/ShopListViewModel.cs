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
    public class ShopListViewModel : ModelBase
    {

        [Display(Name = "コード")]
        public string ShopCD { get; set; }

        [Display(Name = "店舗名")]
        public string ShopName { get; set; }

        [Display(Name = "区分")]
        [CSV(Output = false)]
        public string ShopTypeCD { get; set; }

        [Display(Name = "区分")]
        [CSV(Output = false)]
        public string ShopTypeCDName { get; set; }

        [Display(Name = "地域")]
        [CSV(Output = false)]
        public string RegionCD { get; set; }

        [Display(Name = "地域")]
        public string RegionCDName { get; set; }

        [Display(Name = "エリアコード")]
        [CSV(Output = false)]
        public string AreaCD { get; set; }

        [Display(Name = "エリア")]
        public string AreaCDName { get; set; }

        [Display(Name = "システム導入日")]
        [CSV(Output = false)]
        public DateTime? SystemStartDate { get; set; }

        [Display(Name = "店舗業務区分")]
        [CSV(Output = false)]
        public string SysTypeCD { get; set; }

        [Display(Name = "店舗業務区分")]
        public string SysTypeCDName { get; set; }
    }
}
