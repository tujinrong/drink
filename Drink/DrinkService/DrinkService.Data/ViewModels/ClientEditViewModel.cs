using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class ClientEditViewModel : ModelBase
    {
        [Display(Name = "店舗")]
        public string ShopCD { get; set; }

        [Display(Name = "顧客コード")]
        public string ClientCD { get; set; }

        [Display(Name = "顧客")]
        public string ClientName { get; set; }

        [Display(Name = "電話番号")]
        public string Tel { get; set; }

        [Display(Name = "担当者")]
        public string TantoCD { get; set; }
    }
}
