using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.ViewModels
{
    public class ClientEditRouteViewModel
    {
        [Display(Name = "週No")]
        public string WeekNo { get; set; }

        [Display(Name = "週")]
        public string WeekName { get; set; }

        [Display(Name = "ルール１")]
        public string Rule1 { get; set; }

        [Display(Name = "ルール2")]
        public string Rule2 { get; set; }

        [Display(Name = "ルール3")]
        public string Rule3 { get; set; }
    }
}
