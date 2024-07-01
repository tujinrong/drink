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

    public enum EnumTableType
    {
        Transaction=0,
        Master,
        System,
        CodeTable
    }
    /// <summary>
    /// 名称テーブル
    /// </summary>
    public class Y_Entity : DyModelBase
    {
        [Key, Column(Order = 1)]
        public int ProjID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(NAME_LEN)]
        public String EntityName { get; set; }

        [StringLength(DESC_LEN)]
        public string EntityDesc { get; set; }

        [StringLength(NAME_LEN)]
        public String EntityType { get; set; }

        [StringLength(GROUP_LEN)]
        public String EntityGroup { get; set; }

        [StringLength(TYPE_LEN)]
        public String EntityState { get; set; }

        //テーブル種別
        public EnumTableType TableType;

        public List<Y_EntityField> FieldList;
        public List<Y_EntityRela1N> Rela1NList;
        public List<Y_EntityRelaN1> RelaN1List;

        public List<Y_EntityFilterControl> FilterControlList;

        public List<Y_EntityFilter> FilterList;
        public List<Y_EntityView> ViewList;

        //public List<Y_EntityViewFilter> ViewFilterList;
        //public List<Y_EntityViewItem> ViewItemList;

     }



}