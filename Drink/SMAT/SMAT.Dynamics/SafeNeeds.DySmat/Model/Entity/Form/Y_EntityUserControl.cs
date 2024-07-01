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
    public class Y_EntityUserControl : DyModelBase
    {
        [Key, Column(Order = 1)]
        public int ProjID { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(NAME_LEN)]
        public string EntityName { get; set; }

        [Key, Column(Order = 3)]
        [StringLength(NAME_LEN)]
        public string UserControlName { get; set; }

        [StringLength(DESC_LEN)]
        public string UserControlDesc { get; set; }

        [StringLength(NAME_LEN)]
        public string UserControlType { get; set; }

        public string UserControlOptions { get; set; }

        [StringLength(NAME_LEN)]
        public string Belong { get; set; }

        [StringLength(GROUP_LEN)]
        public string UserControlCategory { get; set; }

        [StringLength(2)]
        public string CreatedBy { get; set; }

        [StringLength(TYPE_LEN)]
        public String UserControlState { get; set; }

        [NotMapped]
        public List<Y_EntityUserControlItem> Controls{ get; set; }
    }


}