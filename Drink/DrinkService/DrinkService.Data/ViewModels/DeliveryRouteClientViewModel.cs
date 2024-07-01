using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinkService.Models;

namespace DrinkService.Data.ViewModels
{
    public class DeliveryRouteClientViewModel : ModelBase
    {
        [Display(Name = "初回フラグ")]
        public string FirstFlag { get; set; }

        [Display(Name = "担当者コード")]
        public string StaffCD { get; set; }

        [Display(Name = "担当者名")]
        public string StaffName { get; set; }

        [Display(Name = "顧客コード")]
        public string ClientCD { get; set; }

        [Display(Name = "顧客名")]
        public string ClientName { get; set; }

        [Display(Name = "店舗コード")]
        public string ShopCD { get; set; }

        [Display(Name = "ルート")]
        public string Route { get; set; }

        [Display(Name = "設置日")]
        public string FirstDate { get; set; }

        [Display(Name = "設置日")]
        public string LastDate { get; set; }
        

        [Display(Name = "DoneFlag")]
        public string DoneFlag { get; set; }

        [Display(Name = "DoneFlagSaved")]
        public string DoneFlagSaved { get; set; }

        [Display(Name = "担当者コード")]
        public string StaffCDPlan { get; set; }

        [Display(Name = "担当者名")]
        public string StaffNamePlan { get; set; }

        [Display(Name = "後日今ｽﾄフラグ")]
        public string AfterStopFlag { get; set; }

        public string AfterDate { get; set; }   
    }
}
