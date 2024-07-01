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
    public class Y_RoleMenuAuth : DyModelBase
    {
        [Key, Column(Order = 1)]
        public int ProjID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(NAME_LEN)]
        public string RoleName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(NAME_LEN)]
        public string MenuName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(NAME_LEN*2)]
        public string AuthName { get; set; }

        [StringLength(TYPE_LEN)]
        public string AuthType { get; set; }

        [StringLength(NAME_LEN)]
        public string EntityName { get; set; }

        [StringLength(NAME_LEN)]
        public string FormName { get; set; }

        [StringLength(NAME_LEN)]
        public string FieldName { get; set; }

        [StringLength(1)]
        public string AuthValue { get; set; }
        

        
    }



}