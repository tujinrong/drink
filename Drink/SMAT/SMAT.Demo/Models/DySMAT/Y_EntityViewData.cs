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
using System.Data.Entity;
using System.Data.Entity.Migrations;


namespace DrinkService.Models
{
    /// <summary>
    /// システムテーブル
    /// </summary>
    public class Y_EntityViewData
    {

        public const string 補充集金指示リスト = "補充集金指示リスト";
        public const string 補充集金リスト = "補充集金リスト";
        public const string 担当者一覧 = "担当者一覧";
        public const string 店舗一覧 = "店舗一覧";

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static Y_EntityView[] GetData()
        {
            return new Y_EntityView[] {
                //補充集金指示一覧
                new Y_EntityView { ProjID =0, EntityName =typeof(T_HoOrderClient).Name, ViewName=補充集金指示リスト, ViewDesc="補充集金指示リスト"} ,
                //補充集金リスト
                new Y_EntityView { ProjID =0, EntityName =typeof(T_HoClient).Name, ViewName=補充集金リスト, ViewDesc="補充集金リスト"} ,
                //担当者一覧
                new Y_EntityView { ProjID =0, EntityName =typeof(M_Staff).Name, ViewName=担当者一覧, ViewDesc="担当者一覧"} ,
                //店舗一覧
                new Y_EntityView { ProjID =0, EntityName =typeof(M_Shop).Name, ViewName=店舗一覧, ViewDesc="店舗一覧"} ,
            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.EntityViews.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}