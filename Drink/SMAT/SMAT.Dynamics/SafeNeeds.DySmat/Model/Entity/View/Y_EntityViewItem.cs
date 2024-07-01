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
    public class Y_EntityViewItem　: DyModelBase
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
        [StringLength(NAME_LEN)]
        public string ItemName { get; set; }

    
        [StringLength(DESC_LEN)]
        public string ItemDesc { get; set; }

        public short Seq { get; set; }


        public const string ItemType_Item="IT";
        public const string ItemType_LEFTJOIN = "LJ";
        public const string ItemType_INNERJOIN = "IJ";
        public const string ItemType_CODENAME = "CN";
        [Column(TypeName = "Varchar")]

        //[StringLength(1)]

        //public string ItemType { get; set; }

        [StringLength(200)]
        public string Path { get; set; }

        public string ItemSql { get; set; }

        [StringLength(200)]
        public string ItemEntityName { get; set; }

        [StringLength(NAME_LEN)]
        public string EntityAlias { get; set; }

        //[StringLength(NAME_LEN)]
        //public string ItemParentEntityName { get; set; }

        [StringLength(100)]
        public string Format { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string Width { get; set; }

        public short OrderBy { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string Group { get; set; }

        public enum EnumGroup
        {
            None,
            GroupBy,
            Sum,
            Count,
            Max,
            Min,
            Avg
        }

        public EnumGroup GetGroup()
        {
            if (string.IsNullOrEmpty(Group)) return EnumGroup.None;

            return (EnumGroup)Enum.Parse(typeof(EnumGroup), Group);
        }


        [StringLength(GROUP_LEN)]
        public string ItemCategory { get; set; }

        [StringLength(NAME_LEN)]
        public string ItemFieldName { get; set; }

        public bool IsHideInView { get; set; }

        [StringLength(2)]
        public string SumType { get; set; }
    }



}