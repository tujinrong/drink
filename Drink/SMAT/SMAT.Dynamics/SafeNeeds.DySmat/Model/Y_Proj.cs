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
    public class Y_Proj : DyModelBase
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 1)]
        public int ProjID { get; set; }

        [StringLength(NAME_LEN)]
        public string ProjName { get; set; }

        [StringLength(DESC_LEN)]
        public string ProjDesc { get; set; }

        public short PageRows { get; set; }

        [StringLength(500)]
        public string ConnectionString { get; set; }


        public EnumDatabaseType DatabaseType { get; set; }

        public EnumProviderType ProviderType { get; set; }


        public bool UseDatabaseTime{get; set;}

        public string GetConnectionString()
        {
            if (!string.IsNullOrEmpty(Global.ConnectString ))
            {
                return Global.ConnectString;
            }
            return ConnectionString; 

        }

        //屠 2015.10.25
        //[StringLength(20)]
        //public string InsertUserItem { get; set; }

        //[StringLength(20)]
        //public string InsertTimeItem { get; set; }

        //[StringLength(50)]
        //public string InsertProgItem { get; set; }

        //夏 2016.09.01
        [StringLength(20)]
        public string UpdateUserItem { get; set; }

        [StringLength(20)]
        public string UpdateTimeItem { get; set; }

        //[StringLength(50)]
        //public string UpdateProgItem { get; set; }

        public List<Y_Entity> EntityList;
        public Dictionary<string, Dictionary<string, string>> OptionSet;
        public List<Y_Menu> MenuList;
        public List<Y_MenuGroup> MenuGroupList;
        public List<Y_MenuGroupDetail> MenuGroupDetailList;
        public List<Y_Role> RoleList;
        public List<Y_RoleMenu> RoleMenuList;
        public List<Y_RoleMenuAuth> RoleMenuAuthList;
        public Dictionary<string, Y_EntityForm> FormDic;
    }




}