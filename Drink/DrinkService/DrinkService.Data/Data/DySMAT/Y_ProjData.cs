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
using System.Reflection;

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
                new Y_Proj { ProjID =1,  PageRows=50,
                     ProjName = "DrinkService", ProjDesc="DUSKINドリンクサービス", 
                    ConnectionString="Data Source=192.168.1.91;Initial Catalog=Drink;Integrated Security=False;Connect Timeout=30;uid=sa;pwd=sa010203~",
                     DatabaseType= SafeNeeds.DySmat.DB.EnumDatabaseType.SQLSERVER,
                     ProviderType= SafeNeeds.DySmat.DB.EnumProviderType.MS_SQL_SQLSERVER,
                      UseDatabaseTime=false
                       //UpdateUserItem="UpdateUser",
                       // UpdateTimeItem="UpdateTime"
                 } 
            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.Projs.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}