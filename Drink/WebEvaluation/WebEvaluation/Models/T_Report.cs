//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  テレフォンレポート
//  
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebEvaluation.Models
{
    /// <summary>
    /// テレフォンレポート
    /// </summary>
    public class T_Report : ModelBase
    {

        [Key]
        [Display(Name = "挙式ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PartyID { get; set; }

        [Display(Name = "特記事項")]
        [DataType(DataType.MultilineText)]
        public string Memo { get; set; }

        //0:未, 1:可, 2:否
        [Display(Name = "電話可否")]
        [StringLength(1)]
        public string TelFlg { get; set; }

        [Display(Name = "備考")]
        [StringLength(200)]
        public string Remark { get; set; }

        //新郎:新郎、新婦:新婦 --------- 不要
        [Display(Name = "電話相手")]
        [StringLength(2)]
        public string TelWho { get; set; }   
        
        //---------------------------------------------------------------------
        //システム共通項目
        //---------------------------------------------------------------------
        [Display(Name = "更新者")]
        [Column(TypeName = "Varchar")]
        [StringLength(USERID_LEN)]
        public string UpdateUserID { get; set; }

        [Display(Name = "更新日時")]
        public DateTime? UpdateTime { get; set; }


    }
}