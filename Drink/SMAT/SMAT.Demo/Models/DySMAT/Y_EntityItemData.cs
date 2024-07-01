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
using System.Reflection;

using SafeNeeds.DySmat.Model;

namespace DrinkService.Models
{
    /// <summary>
    /// システムテーブル
    /// </summary>
    public class Y_EntityItemData
    {

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static Y_EntityField[] GetData()
        {
            List<Y_EntityField> list = new List<Y_EntityField>();

            list.AddRange(GetEntityItem(0, typeof(M_Client)));
            list.AddRange(GetEntityItem(0, typeof(M_ClientInitItems)));
            list.AddRange(GetEntityItem(0, typeof(M_ClientRoute)));
            list.AddRange(GetEntityItem(0, typeof(M_Code)));
            list.AddRange(GetEntityItem(0, typeof(M_Item)));
            list.AddRange(GetEntityItem(0, typeof(M_ItemKit)));
            list.AddRange(GetEntityItem(0, typeof(M_Shop)));
            list.AddRange(GetEntityItem(0, typeof(M_Staff)));
            list.AddRange(GetEntityItem(0, typeof(M_PostCode)));
            list.AddRange(GetEntityItem(0, typeof(M_System)));
            list.AddRange(GetEntityItem(0, typeof(T_HoClient)));
            list.AddRange(GetEntityItem(0, typeof(T_HoClientItem)));
            list.AddRange(GetEntityItem(0, typeof(T_HoOrder)));
            list.AddRange(GetEntityItem(0, typeof(T_HoOrderClient)));
            list.AddRange(GetEntityItem(0, typeof(T_HoDay)));

            return list.ToArray();
        }


        static Y_EntityField[] GetEntityItem(int proj, Type type)
        {
            List<Y_EntityField> list = new List<Y_EntityField>();

            PropertyInfo[] prop = type.GetProperties();
            short i = 0;
            foreach (PropertyInfo p in prop)
            {
                Y_EntityField item = new Y_EntityField();
                list.Add(item);

                item.Seq = i++;
                item.ProjID = (short)proj;
                item.EntityName = type.Name;
                item.FieldName = p.Name;
                item.FieldDesc = p.Name;
                item.IsKey = false;
                item.Length = 0;
                item.Precise = 0;
               // item.DataType = p.PropertyType.ToString(); 
            }

            return list.ToArray();
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.EntityFields.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}