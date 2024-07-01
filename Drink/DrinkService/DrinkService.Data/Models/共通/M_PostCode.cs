//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  郵便番号マスタ。
//
// [作成履歴]　2015/02/25  屠錦栄　初版 
//
// [レビュー]　2015/03/17  屠錦栄　 
//*****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace DrinkService.Models
{
    /// <summary>
    /// 郵便番号マスタ
    /// </summary>
    public class M_PostCode : ModelBase
    {

        [Key]
        [Display(Name = "郵便番号")]
        [Column(TypeName = "Varchar")]
        [StringLength(8)]
        public string PostCD { get; set; }

        [Required]  
        [Display(Name = "住所")]
        [StringLength(100)]
        public string Adress { get; set; }



    }
}