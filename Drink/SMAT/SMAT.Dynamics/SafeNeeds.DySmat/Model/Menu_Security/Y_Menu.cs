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
    public class Y_Menu　: DyModelBase
    {
        [Key, Column(Order = 1)]
        public int ProjID { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(NAME_LEN)]
        public string MenuName { get; set; }

        [StringLength(DESC_LEN)]
        public string MenuDesc { get; set; }

        [StringLength(100)]
        public string MenuIcon { get; set; }

        [StringLength(200)]
        public string MenuUrl { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string ObjetType { get; set; }

    }

    public enum EnumObjectType
    {
        Form,
        View,
        Graph
    }

}