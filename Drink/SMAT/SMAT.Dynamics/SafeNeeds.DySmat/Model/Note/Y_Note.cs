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
    /// 名称テーブル
    /// </summary>
    public class Y_Note　: DyModelBase
    {
        [Key]
        [StringLength(32)]
        public string NoteCD { get; set; }

        public int ProjID { get; set; }

        [StringLength(20)]
        public string NoteUserCD { get; set; }

        [StringLength(255)]
        public string NoteContent { get; set; }

        [StringLength(20)]
        public string SendUserCD { get; set; }

        [StringLength(255)]
        public string SendUserName { get; set; }

        public DateTime? SendTime { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [StringLength(20)]
        public string Action { get; set; }

        [StringLength(255)]
        public string ActionPath { get; set; }

        [StringLength(4000)]
        public string ActionParam { get; set; }

        public DateTime? UpdateTime { get; set; }

        [StringLength(20)]
        public string UpdateUser { get; set; }

        

    }

}