using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace WebEvaluation.DataModels
{
    public class ShopData :Models.ModelBase
    {
        [Required]
        [Display(Name = "店舗略称")]
        [Column(TypeName = "Varchar")]
        [StringLength(SHOPCD_LEN)]
        public string ShopCD { get; set; }

        [Required]
        [Display(Name = "店舗名称")]
        [StringLength(30)]
        public string ShopName { get; set; }

        [Required]
        [Display(Name = "グループ")]
        public string GroupCD { get; set; }

    }
}