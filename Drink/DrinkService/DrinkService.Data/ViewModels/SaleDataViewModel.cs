using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class SaleDataViewModel : ModelBase
    {
        [Display(Name = "店舗コード")]
        public string ShopCD { get; set; }

        [Display(Name = "日付")]
        public string HoDate { get; set; }

        [Display(Name = "ダウンロード者")]
        public string DownloadStaffCD { get; set; }

        [Display(Name = "ダウンロード日時")]
        public string DownloadDate { get; set; }

    }
}
