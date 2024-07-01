//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  上長評価テーブル
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
    /// 上長評価テーブル
    /// </summary>
    public class T_EvaByLeader : ModelBase
    {
        [Key]
        [Display(Name = "挙式ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PartyID { get; set; }

        //A:A  B:B  S:S  SS:SS
        [Display(Name = "上長評価")]
        [Column(TypeName = "Varchar")]
        [StringLength(2)]
        public string LeaderEva { get; set; }

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