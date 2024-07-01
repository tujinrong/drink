using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace WebEvaluation.DataModels
{
    public class UnitData : Models.ModelBase
    {
        [Required]
        [Display(Name = "組織コード")]
        [Column(TypeName = "Varchar")]
        [StringLength(UNITCD_LEN)]
        public string UnitCD { get; set; }

        [Required]
        [Display(Name = "組織名")]
        [StringLength(20)]
        public string UnitName { get; set; }

        [Required]
        [Display(Name = "店舗")]
        [Column(TypeName = "Varchar")]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        [Required]
        [Display(Name = "IsCusCenter")]
        [StringLength(5)]
        public string IsCusCenter { get; set; }

        [Required]
        [Display(Name = "IsShop")]
        [StringLength(5)]
        public string IsShop { get; set; }
    }
}