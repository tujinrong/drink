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
    public class Y_ProjData
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Y_Proj[] GetData()
        {
            return new Y_Proj[] {
                new Y_Proj { ProjID =1, ProjDesc="DUSKINドリンクサービス"} 
            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.Projs.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}