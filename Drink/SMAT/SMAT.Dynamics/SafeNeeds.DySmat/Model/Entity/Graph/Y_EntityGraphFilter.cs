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
    public class Y_EntityGraphFilter : DyModelBase
    {
        [Key, Column(Order = 1)]
        public int ProjID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(NAME_LEN)]
        public int EntityName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(NAME_LEN)]
        public int ViewName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(NAME_LEN)]
        public string FilterName { get; set; }

        [StringLength(DESC_LEN)]
        public string FilterDesc { get; set; }

        [StringLength(NAME_LEN)]
        public string ItemEntityName { get; set; }

        [StringLength(NAME_LEN)]
        public string ItemName { get; set; }

        [StringLength(TYPE_LEN)]
        public string DataType { get; set; }

        //LikeHead, LikeMiddle, =,>=,<= in, Between
        [StringLength(OPR_LEN)]
        public string Operator { get; set; }

        //1,2,N
        [StringLength(1)]
        public string ItemNum { get; set; }
    }

}