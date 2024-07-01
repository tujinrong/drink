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
    public class Y_EntityRela1N　: DyModelBase
    {
        [Key, Column(Order = 1)]
        public int ProjID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(NAME_LEN)]
        public string EntityName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(NAME_LEN*2)]
        public string RelaName { get; set; }

        [StringLength(DESC_LEN)]
        public string RelaDesc { get; set; }

        [StringLength(NAME_LEN)]
        public string RelaEntityName { get; set; }

        [StringLength(NAME_LEN*2)]
        public string FieldNames { get; set; }

        [StringLength(NAME_LEN*2)]
        public string RelaFieldNames { get; set; }

        public bool CheckDelete { get; set; }
        public bool IsSubTable { get; set; }


        public Y_Entity RelaEntity;
    }

}