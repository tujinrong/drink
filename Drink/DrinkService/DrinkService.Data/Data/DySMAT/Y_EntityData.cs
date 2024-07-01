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
using System.Collections.Generic;

using System.Data.Entity;
using System.Data.Entity.Migrations;
using SafeNeeds.DySmat.Model;
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
                GetEntity(1, typeof(M_Client)),
                GetEntity(1, typeof(M_ClientInitItems)),
                GetEntity(1, typeof(M_ClientRoute)),
                GetEntity(1, typeof(M_Item)),
                GetEntity(1, typeof(M_ItemKit)),
                GetEntity(1, typeof(M_Shop)),
                GetEntity(1, typeof(M_Staff)),
                GetEntity(1, typeof(M_PostCode)),
                GetEntity(1, typeof(M_System)),
                GetEntity(1, typeof(T_HoClient)),
                GetEntity(1, typeof(T_HoClientItem)),
                //GetEntity(1, typeof(T_HoOrderTanto)),
                GetEntity(1, typeof(T_HoOrderClient)),
                GetEntity(1, typeof(T_HoDay)),
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