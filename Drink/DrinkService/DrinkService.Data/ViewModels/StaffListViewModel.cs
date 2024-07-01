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
    public class StaffListViewModel : ModelBase
    {
        [Display(Name = "所属店舗区分")]
        [CSV(Output = false)]
        [StringLength(1)]
        public string ShopTypeCD { get; set; }

        [Display(Name = "店舗コード")]
        [CSV(Output = false)]
        public string ShopCD { get; set; }

        [Display(Name = "部署店舗")]
        public string ShopName { get; set; }

        [Display(Name = "番号")]
        public string StaffCD { get; set; }

        [Display(Name = "担当者名")]
        public string StaffName { get; set; }

        [Display(Name = "役割")]
        public string RoleCD { get; set; }
    }
}
