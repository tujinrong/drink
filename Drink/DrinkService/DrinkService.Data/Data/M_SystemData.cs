////*****************************************************************************
//// [システム]  
//// 
//// [機能概要]  システムテーブル
////
//// [作成履歴]　2014/06/25  屠錦栄　初版 
////
//// [レビュー]　2014/07/17  屠錦栄　 
////*****************************************************************************

using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;


namespace DrinkService.Models
{
    /// <summary>
    /// システムテーブル
    /// </summary>
    public class M_SystemData
    {

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_System[] GetData()
        {
            return new M_System[] {
                new M_System {ID=0, FirstWeekDate = new System.DateTime(2014,1,3), ProjNo=0} 
            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.Systems.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}