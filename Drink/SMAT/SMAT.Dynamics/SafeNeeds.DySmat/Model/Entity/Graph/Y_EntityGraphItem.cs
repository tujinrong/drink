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
    public class Y_EntityGraphItem　: DyModelBase
    {
        [Key, Column(Order = 1)]
        public int ProjID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(NAME_LEN)]
        public string EntityName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(NAME_LEN)]
        public string ViewName { get; set; }

        [Key]
        [Column(Order = 4)]
        public short ItemID { get; set; }

 
        public short Seq { get; set; }

        [StringLength(DESC_LEN)]
        public string ItemDesc { get; set; }

        public string ItemSql { get; set; }

        [StringLength(NAME_LEN)]
        public string ItemEntityName { get; set; }

        [StringLength(NAME_LEN)]
        public string ItemRelaEntityName { get; set; }

        public short Width { get; set; }



    }



}