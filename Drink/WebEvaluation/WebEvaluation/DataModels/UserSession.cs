using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace WebEvaluation.DataModels
{
    public class UserSession :Models.ModelBase
    {
        [Display(Name = "ユーザーID")]
        [StringLength(USERID_LEN)]
        public string UserID { get; set; }

        [Display(Name = "社員番号")]
        [StringLength(STAFFCD_LEN)]
        public string StaffCD { get; set; }

        [Required]
        [Display(Name = "社員氏名")]
        [StringLength(20)]
        public string StaffName { get; set; }

        [Display(Name = "権限")]
        [Column(TypeName = "Varchar")]
        [StringLength(2)]
        public string RoleCD { get; set; }

        [Display(Name = "組織コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(UNITCD_LEN)]
        public string UnitCD { get; set; }

        [Display(Name = "組織名")]
        [StringLength(20)]
        public string UnitName { get; set; }

        [Display(Name = "店舗名")]
        [Column(TypeName = "Varchar")]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        [Display(Name = "会場名")]
        [StringLength(30)]
        public string ShopName { get; set; }
    }
}