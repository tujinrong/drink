using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.Models
{
    public class UserSession : ModelBase
    {
        [Display(Name = "所属店舗区分")]
        [StringLength(1)]
        public string ShopTypeCD { get; set; }

        [Display(Name = "店舗コード")]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        [Display(Name = "店舗名")]
        [StringLength(30)]
        public string ShopName { get; set; }

        [Display(Name = "社員番号")]
        [StringLength(STAFFCD_LEN)]
        public string StaffCD { get; set; }

        [Required]
        [Display(Name = "社員氏名")]
        [StringLength(20)]
        public string StaffName { get; set; }

        [Display(Name = "権限")]
        [StringLength(2)]
        public string RoleCD { get; set; }

        public bool IsShopRole() {
            return RoleCD == ModelBase.CN役割_店舗管理者 || RoleCD == ModelBase.CN役割_店舗担当者 || RoleCD == ModelBase.CN役割_ドリンクマネジメント;
        }
    }
}
