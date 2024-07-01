using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class DeliveryRouteListViewModel : ModelBase
    {
        [Display(Name = "初回フラグ")]
        public string FirstFlag { get; set; }

        [Display(Name = "済フラグ")]
        public string DoneFlag { get; set; }

        [Display(Name = "担当者コード")]
        public string TantoCD { get; set; }

        [Display(Name = "担当者名")]
        public string StaffName { get; set; }

        [Display(Name = "店舗コード")]
        public string ShopCD { get; set; }

        [Display(Name = "顧客コード")]
        public string ClientCD { get; set; }

        [Display(Name = "顧客名")]
        public string ClientName { get; set; }

        [Display(Name = "住所")]
        public string Address { get; set; }

        [Display(Name = "補充日")]
        public DateTime HoDate { get; set; }
    }
}
