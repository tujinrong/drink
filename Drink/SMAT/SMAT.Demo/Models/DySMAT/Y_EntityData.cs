////*****************************************************************************
//// [システム] 
//// 
//// [機能概要]  
////
//// [作成履歴]　2014/06/25  屠錦栄　初版 
////
//// [レビュー]　2014/07/17  屠錦栄　 
////*****************************************************************************

using SafeNeeds.DySmat.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Reflection;

namespace DrinkService.Models
{
    /// <summary>
    /// システムテーブル
    /// </summary>
    public class Y_EntityData
    {

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static Y_Entity[] GetData()
        {
            return new Y_Entity[] {
                GetEntity(0, typeof(M_Client)),
                GetEntity(0, typeof(M_ClientInitItems)),
                GetEntity(0, typeof(M_ClientRoute)),
                GetEntity(0, typeof(M_Code)),
                GetEntity(0, typeof(M_Item)),
                GetEntity(0, typeof(M_ItemKit)),
                GetEntity(0, typeof(M_Shop)),
                GetEntity(0, typeof(M_Staff)),
                GetEntity(0, typeof(M_PostCode)),
                GetEntity(0, typeof(M_System)),
                GetEntity(0, typeof(T_HoClient)),
                GetEntity(0, typeof(T_HoClientItem)),
                GetEntity(0, typeof(T_HoOrder)),
                GetEntity(0, typeof(T_HoOrderClient)),
                GetEntity(0, typeof(T_HoDay)),
            };

            //List<Y_Entity> list = new List<Y_Entity>();
            //list.Add(typeof(M_Client)

        }

        static Y_Entity GetEntity(int proj, Type type)
        {

            Y_Entity entity=new Y_Entity();

            entity.ProjID = proj;
            entity.EntityName = type.Name;
            entity.EntityDesc = type.Name;

            return entity;
        }



        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.Entities.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}