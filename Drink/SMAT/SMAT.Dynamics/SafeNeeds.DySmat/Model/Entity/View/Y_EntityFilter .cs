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
    /// 
    /// </summary>
    public class Y_EntityFilter : DyModelBase
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
        public string FilterName { get; set; }

        [StringLength(DESC_LEN)]
        public string FilterDesc { get; set; }

        [StringLength(200)]
        public string Path { get; set; }

        [StringLength(NAME_LEN)]
        public string ItemEntityAliasName { get; set; }


        [StringLength(200)]
        public string FilterSql { get; set; }



        [StringLength(NAME_LEN)]
        public string Belong { get; set; }

        [StringLength(GROUP_LEN)]
        public string FilterCategory { get; set; }

        [StringLength(2)]
        public string CreatedBy { get; set; }

        [StringLength(TYPE_LEN)]
        public String FilterState { get; set; }

        
        public bool IsHaving { get; set; }

        /*
        [StringLength(NAME_LEN)]
        public string ItemEntityName { get; set; }


        [StringLength(NAME_LEN)]
        public string FieldName { get; set; }


        //1,2,N
        [StringLength(1)]
        public string ItemNumCD { get; set; }


        public string ToSql(string value)
        {
            string sql=string.Empty;
            switch (ItemNumCD)
            {
                case NUM_1:
                    string item = EntityName + "." + FieldName;
                        switch(Operator)
                        {
                            case OPR_LH:      
                                return item + " LIKE '" + value +"%'";
                            case OPR_LK:      
                                return item + " LIKE '%" + value +"%'";
                            case OPR_EQ:
                                if (DataType==TYPE_STRING || DataType==TYPE_DATE)
                                {
                                    value = "'" + value + "'";
                                }
                                return item + "=" + value;
                            case OPR_GE:
                                if (DataType == TYPE_STRING || DataType == TYPE_DATE)
                                {
                                    value = "'" + value + "'";
                                }
                                return item + ">=" + value;
                            case OPR_LE:
                                if (DataType == TYPE_STRING || DataType == TYPE_DATE)
                                {
                                    value = "'" + value + "'";
                                }
                                return item + "<=" + value;
                            default:
                                return "";
                        }
                        
                default:
                    return "";
            }
            
        */
            
        
    }

}