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
    public class Y_EntityViewFilterData
    {

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static Y_EntityViewFilter[] GetData()
        {
            return new Y_EntityViewFilter[] {
                //補充集金指示一覧
                new Y_EntityViewFilter { ProjID =1, EntityName =typeof(T_HoOrderClient).Name, 
                    ViewName=Y_EntityViewData.補充集金指示リスト,   FilterControlName =Y_EntityFilterData.ShopFilter, Seq=1} ,
                new Y_EntityViewFilter { ProjID =1, EntityName =typeof(T_HoOrderClient).Name, 
                    ViewName=Y_EntityViewData.補充集金指示リスト,   FilterControlName =Y_EntityFilterData.StaffFilter,Seq=2} ,
                new Y_EntityViewFilter { ProjID =1, EntityName =typeof(T_HoOrderClient).Name, 
                    ViewName=Y_EntityViewData.補充集金指示リスト,   FilterControlName =Y_EntityFilterData.RouteFilter,Seq=3} ,
                new Y_EntityViewFilter { ProjID =1, EntityName =typeof(T_HoOrderClient).Name, 
                    ViewName=Y_EntityViewData.補充集金指示リスト,   FilterControlName =Y_EntityFilterData.HoDateFilter, Seq=4} ,
                //補充集金一覧
                new Y_EntityViewFilter { ProjID =1, EntityName =typeof(T_HoClient).Name,
                    ViewName=Y_EntityViewData.補充集金リスト,   FilterControlName =Y_EntityFilterData.ShopFilter, Seq=1} ,
                new Y_EntityViewFilter { ProjID =1, EntityName =typeof(T_HoClient).Name,
                    ViewName=Y_EntityViewData.補充集金リスト,   FilterControlName =Y_EntityFilterData.StaffFilter,Seq=2} ,
                new Y_EntityViewFilter { ProjID =1, EntityName =typeof(T_HoClient).Name,
                    ViewName=Y_EntityViewData.補充集金リスト,   FilterControlName =Y_EntityFilterData.RouteFilter,Seq=3} ,
                new Y_EntityViewFilter { ProjID =1, EntityName =typeof(T_HoClient).Name,
                    ViewName=Y_EntityViewData.補充集金リスト,   FilterControlName =Y_EntityFilterData.HoDateFilter, Seq=4} ,

            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.EntityViewFilters.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}