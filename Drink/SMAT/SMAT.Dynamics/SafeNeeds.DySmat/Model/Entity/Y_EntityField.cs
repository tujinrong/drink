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

using SafeNeeds.DySmat.DB;

namespace SafeNeeds.DySmat.Model
{
    /// <summary>
    /// 名称テーブル
    /// </summary>
    public class Y_EntityField:DyModelBase
    {
        public Y_EntityField(){
        }

        public Y_EntityField(int projID, 
            string entityName, string fieldName,string fieldDesc, bool isKey, bool isNullable, 
            string dataType, int length, int precise=0, string charSet="", EnumSystemField systemField= EnumSystemField.None )
        {
            this.ProjID = projID;
            this.EntityName = entityName;
            this.FieldName = fieldName;
            this.FieldDesc = fieldDesc;
            this.IsKey = isKey;
            this.IsNullable = isNullable;
            this.DataType = dataType;
            this.Length =(short) length;
            this.Precise =(short) precise;
            this.CharSet = charSet;
            this.SystemField = systemField;
        }

        [Key, Column(Order = 1)]
        public int ProjID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(NAME_LEN)]
        public string EntityName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(NAME_LEN)]
        public string FieldName { get; set; }

        public bool IsKey { get; set; }

        public bool HideInView { get; set; }


        public short Seq { get; set; }

        [StringLength(DESC_LEN)]
        public string FieldDesc { get; set; }

        [StringLength(DESC_LEN)]
        public string Title { get; set; }

        [StringLength(10)]
        public string DataType { get; set; }

        public short? Length { get; set; }

        public short? Precise { get; set; }

        public bool IsLogicItem { get; set; }

        [StringLength(20)]
        public string CharSet { get; set; }

        public bool IsVirtual { get; set; }

        [StringLength(100)]
        public string VirtualSql { get; set; }

        public bool IsSum {get; set;}

        public bool IsMax { get; set; }

        public bool IsMin { get; set; }

        public bool IsCount { get; set; }

        public bool IsAvg { get; set; }

        public bool IsGroupBy { get; set; }

        public bool IsFilter { get; set; }

        public bool IsNullable { get; set; }

        public bool IsIdentity { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string IdentitySql { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string ControlType { get; set; }

        public int FormX { get; set; }

        public int FormY { get; set; }

        [StringLength(GROUP_LEN)]
        public string FieldCategory { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string OptionSet { get; set; }

        public bool IsEncryption { get; set; }

        [StringLength(20)]
        public string EncryptionType { get; set; }

        public enum EnumSystemField
        {
            None = 0,
            InsertUser=11,
            InsertTime=12,
            InsertProgram=13,
            InsertAddress = 14,
            UpdateUser = 21,
            UpdateTime=22,
            UpdateProgram=23,
            UpdateAddress = 24,
            InsertUpdateUser = 31,
            InsertUpdateTime = 32,
            InsertUpdateProgram = 33,
            InsertUpdateAddress = 34
        }
        public EnumSystemField SystemField { get; set; }
        
        [StringLength(100)]
        public string Memo { get; set; }

        [StringLength(NAME_LEN)]
        public string DefaultValue { get; set; }

        public EnumDbDataType GetCommonDataType()
        {

                switch(DataType.ToUpper())
                {
                    case "NVARCHAR":
                    case "NVARCHAR2":
                        return EnumDbDataType.UNICODE;

                    case "VARCHAR":
                    case "VARCHAR2":
                        return EnumDbDataType.STRING;
                        
        
                    case "NUMBER":
                    case "NUMERIC":
                    case "DECIMAL":
                        return EnumDbDataType.NUMBER;
                    
                    case "BIGINT":
                        return EnumDbDataType.INT64;

                    case "INT":
                        return EnumDbDataType.INT32;

                    case "SMALLINT":
                        return EnumDbDataType.INT16;
                    
                    case "TYNYINT":
                        return EnumDbDataType.INT8;

                    case "DATETIME":
                    case "DATE":
                        return EnumDbDataType.DATE;

                    case "BIT":
                        return EnumDbDataType.BOOLEAN;
                    default:
                        throw new ApplicationException("type error");

                }
        }

            public int GetSize()
            {
                if (Length ==null)
                    return 0;
                else 
                    return (int)Length;
            }
       

     }



}