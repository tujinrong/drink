using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class ClientRouteTantoViewModel : ModelBase
    {

        [Display(Name = "No")]
        public Int32 No { get; set; }

        [Display(Name = "顧客コード")]
        public string ClientCD { get; set; }

        [Display(Name = "顧客")]
        public string ClientName { get; set; }

        [Display(Name = "旧担当者CD")]
        public string TantoCDFrom { get; set; }

        [Display(Name = "新担当者CD")]
        public string TantoCDTo { get; set; }

        [Display(Name = "旧担当者")]
        public string TantoFrom { get; set; }

        [Display(Name = "新担当者")]
        public string TantoTo { get; set; }

        [Display(Name = "旧コース")]
        public string RouteFrom { get; set; }

        [Display(Name = "新コース")]
        public string RouteTo { get; set; }

        [Display(Name = "Msg")]
        public string Msg { get; set; }
    }
}
