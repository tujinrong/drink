//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  カスタマセンター担当者評価
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
    /// カスタマセンター担当者評価
    /// </summary>
    public class T_EvaByStaff : ModelBase
    {
        [Key]
        [Display(Name = "挙式ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PartyID { get; set; }

 
        [Display(Name = "日付")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Eva1Date { get; set; }

        //0:繋がず　1:繋がった
        [Display(Name = "通話結果")]
        [MaxLength(1)]
        public string Eva1Result { get; set; }

        [Display(Name = "時間")]
        public int? Eva1Time { get; set; }

        [Display(Name = "担当")]
        [StringLength(STAFFCD_LEN)]
        public string Eva1StaffCD { get; set; }


        [Display(Name = "日付")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Eva2Date { get; set; }

        //0:繋がず　1:繋がった
        [Display(Name = "通話結果")]
        [MaxLength(1)]
        public string Eva2Result { get; set; }

        [Display(Name = "時間")]
        public int? Eva2Time { get; set; }

        [Display(Name = "担当")]
        [StringLength(STAFFCD_LEN)]
        public string Eva2StaffCD { get; set; }

        

        [Display(Name = "日付")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Eva3Date { get; set; }

        //0:繋がず　1:繋がった
        [Display(Name = "通話結果")]
        [MaxLength(1)]
        public string Eva3Result { get; set; }

        [Display(Name = "時間")]
        public int? Eva3Time { get; set; }

        [Display(Name = "担当")]
        [StringLength(STAFFCD_LEN)]
        public string Eva3StaffCD { get; set; }

        //A:A  B:B  S:S  SS:SS
        [Display(Name = "担当評価")]
        [StringLength(2)]
        public string StatffEva { get; set; }

        //1:要確認 2:要対応
        [Display(Name = "留意フラグ	")]
        [StringLength(1)]
        public string CareFlg { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "電話内容")]
        public string Record { get; set; }

        [Display(Name = "通話フラグ")]
        [MaxLength(1)]
        public string EvaResultFlag { get; set; }

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