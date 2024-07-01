//*****************************************************************************
// [システム]  
// 
// [機能概要]  
//  
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace SafeNeeds.DySmat.Model
{
    /// <summary>
    /// ログテーブル
    /// </summary>
    public class Y_Log : DyModelBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogID { get; set; }

        public int ProjID { get; set; }

        [StringLength(20)]
        public string LogType { get; set; }

        [StringLength(40)]
        public string FunctionName { get; set; }

        [StringLength(80)]
        public string FunctionPath { get; set; }

        [StringLength(20)]
        public string FunctionType { get; set; }

        [StringLength(20)]
        public string LoginCD { get; set; }

        [StringLength(50)]
        public string RemoteAddr { get; set; }

        [StringLength(80)]
        public string HostName { get; set; }

        [StringLength(200)]
        public string Browser { get; set; }

        [StringLength(32)]
        public string SessionCD { get; set; }

        [StringLength(100)]
        public string Code1 { get; set; }

        [StringLength(100)]
        public string Code2 { get; set; }

        [StringLength(100)]
        public string Code3 { get; set; }

        [StringLength(100)]
        public string Code4 { get; set; }

        [StringLength(100)]
        public string Code5 { get; set; }

        public string Content { get; set; }

        //---------------------------------------------------------------------
        //システム共通項目
        //---------------------------------------------------------------------
        [Display(Name = "更新者")]
        [StringLength(20)]
        public string UpdateUser { get; set; }

        [Display(Name = "更新日時")]
        public DateTime? UpdateTime { get; set; }

        

    }

}