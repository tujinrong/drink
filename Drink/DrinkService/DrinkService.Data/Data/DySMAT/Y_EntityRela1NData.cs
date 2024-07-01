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
    /// システムテーブル
    /// </summary>
    public class Y_EntityRela1NData
    {

        public const string T_HoOrder__T_HoOrderClient = "T_HoOrder->T_HoOrderClient";
        public const string M_Client__M_ClientInitItems = "M_Client->M_ClientInitItems";
        public const string M_Client__M_ClientRoute = "M_Client->M_ClientRoute";
        public const string M_Client__T_HoClient = "M_Client->T_HoClient";
        public const string T_HoClient__T_HoClientItem = "T_HoClient->T_HoClientItem";

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static Y_EntityRela1N[] GetData()
        {
            return new Y_EntityRela1N[] {
                //new Y_EntityRela1N { ProjID=1,  EntityName =typeof(T_HoOrderTanto).Name, RelaName =T_HoOrder__T_HoOrderClient, RelaDesc= T_HoOrder__T_HoOrderClient, RelaEntityName =typeof(T_HoOrderClient).Name, FieldNames =T_HoOrderTanto.RELA1N_ITEMS, RelaFieldNames=T_HoOrderTanto.RELA1N_ITEMS },

                new Y_EntityRela1N { ProjID=1,  EntityName =typeof(T_HoClient).Name, 
                    RelaName =T_HoClient__T_HoClientItem,
                    RelaDesc= T_HoClient__T_HoClientItem,
                    RelaEntityName =typeof(T_HoClientItem).Name, 
                    FieldNames = T_HoClient.I店舗コード + "," + T_HoClient.I顧客コード+ "," + T_HoClient.I補充回数,
                    RelaFieldNames=T_HoClientItem.I店舗コード + "," + T_HoClientItem.I顧客コード+ "," + T_HoClientItem.I補充回数,
                      IsSubTable=true,
                      CheckDelete=false,
                },
                new Y_EntityRela1N { ProjID=1,  EntityName =typeof(M_Client).Name, 
                    RelaName =M_Client__M_ClientInitItems, 
                    RelaDesc= M_Client__M_ClientInitItems,
                    RelaEntityName =typeof(M_ClientInitItems).Name,
                    FieldNames = M_Client.I店舗コード + "," + M_Client.I顧客コード,
                    RelaFieldNames=M_Client.I店舗コード + "," + M_Client.I顧客コード,
                     IsSubTable=true,
                      CheckDelete=false
                    } ,
                new Y_EntityRela1N { ProjID=1,  EntityName =typeof(M_Client).Name, 
                    RelaName =M_Client__M_ClientRoute,
                    RelaDesc= M_Client__M_ClientRoute,
                    RelaEntityName =typeof(M_ClientRoute).Name, 
                    FieldNames = M_Client.I店舗コード + "," + M_Client.I顧客コード,
                    RelaFieldNames=M_Client.I店舗コード + "," + M_Client.I顧客コード,
                      IsSubTable=true,
                      CheckDelete=false,
                } ,

                   new Y_EntityRela1N { ProjID=1,  EntityName =typeof(M_Client).Name, 
                    RelaName =M_Client__T_HoClient,
                    RelaDesc= M_Client__T_HoClient,
                    RelaEntityName =typeof(T_HoClient).Name, 
                    FieldNames = M_Client.I店舗コード + "," + M_Client.I顧客コード,
                    RelaFieldNames=M_Client.I店舗コード + "," + M_Client.I顧客コード,
                      IsSubTable=false,
                      CheckDelete=true,
                } ,
            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.EntityRela1N.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}