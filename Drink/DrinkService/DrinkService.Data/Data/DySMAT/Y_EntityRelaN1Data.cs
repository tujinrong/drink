////*****************************************************************************
//// [システム] 
//// 
//// [機能概要]  
////
//// [作成履歴]　2014/06/25  屠錦栄　初版 
////
//// [レビュー]　2014/07/17  屠錦栄　 
////*****************************************************************************

using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

using SafeNeeds.DySmat.Model;

namespace DrinkService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Y_EntityRelaN1Data
    {

        public const string rM_CLIENT = "M_Client";
        public const string rM_STAFF = "M_Staff";
        public const string rM_SHOP = "M_SHOP";

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static Y_EntityRelaN1[] GetData()
        {
            return new Y_EntityRelaN1[] {
                //補充集金指示リスト
                new Y_EntityRelaN1 {ProjID=1,  EntityName =typeof(T_HoOrderClient).Name,
                    RelaName=rM_CLIENT,RelaDesc = rM_CLIENT,
                    RelaEntityName=typeof(M_Client).Name, Alias=typeof(M_Client).Name, 
                    FieldNames="ShopCD,ClientCD", RelaIFieldNames="ShopCD,ClientCD"  } ,
                new Y_EntityRelaN1 {ProjID=1,  EntityName =typeof(T_HoOrderClient).Name, 
                    RelaName=rM_STAFF, RelaDesc = rM_STAFF,
                    RelaEntityName=typeof(M_Staff).Name,  Alias=typeof(M_Staff).Name,
                    FieldNames="ShopCD,TantoCD", RelaIFieldNames="ShopCD,StaffCD"  } ,

                //補充集金リスト
                new Y_EntityRelaN1 {ProjID=1,  EntityName =typeof(T_HoClient).Name,
                    RelaName=rM_CLIENT,RelaDesc = rM_CLIENT,
                    RelaEntityName=typeof(M_Client).Name, Alias=typeof(M_Client).Name, 
                    FieldNames="ShopCD,ClientCD", RelaIFieldNames="ShopCD,ClientCD"  } ,
             
                    new Y_EntityRelaN1 {ProjID=1,  EntityName =typeof(T_HoClient).Name, 
                    RelaName=rM_STAFF, RelaDesc = rM_STAFF,
                    RelaEntityName=typeof(M_Staff).Name,  Alias=typeof(M_Staff).Name,
                    FieldNames="ShopCD,TantoCD", RelaIFieldNames="ShopCD,StaffCD"  } ,

                    //担当者一覧
                new Y_EntityRelaN1 {ProjID=1,  EntityName =typeof(M_Staff).Name,
                    RelaName=rM_SHOP,RelaDesc = rM_SHOP,
                    RelaEntityName=typeof(M_Shop).Name, Alias=typeof(M_Shop).Name, 
                    FieldNames=M_Staff.I店舗コード, 
                    RelaIFieldNames=M_Shop.I店舗コード } ,

            };
        }
        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.EntityRelaN1.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}