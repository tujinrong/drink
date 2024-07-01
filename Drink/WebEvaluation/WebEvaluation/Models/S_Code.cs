//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  名称テーブル
//             管理画面は不要            
//  
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace WebEvaluation.Models
{
    /// <summary>
    /// 名称テーブル
    /// </summary>
    public class S_Code
    {
        [Key]
        [Display(Name = "コード種別")]
        [Column(TypeName = "Varchar", Order = 1)]
        [StringLength(10)]
        public string Kind { get; set; }

        [Key]
        [Display(Name = "コード")]
        [Column(TypeName = "Varchar", Order = 2)]
        [StringLength(4)]
        public string CD { get; set; }

        [Display(Name = "名称")]
        [StringLength(10)]
        public string Name { get; set; }

        [Display(Name = "備考")]
        [StringLength(20, ErrorMessage = "20文字まで入力してください。")]
        public string Memo { get; set; }

        /*
        public S_Code(string kind, string str)
        {
            Kind = kind;
            string[] ss = str.Split(':');
            CD = ss[0];
            Name = ss[1];
        }

        public S_Code(string kind, string cd, string name)
        {
            Kind = kind;
            CD = cd;
            Name = name;
        }
        */

    }



}