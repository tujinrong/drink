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
    public class Y_EntityViewItemData
    {

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static Y_EntityViewItem[] GetData()
        {
            string cn="(SELECT CD,NAME FROM M_CODE WHERE KIND='{0}')";
            return new Y_EntityViewItem[] {
               // //補充集金指示リスト
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(T_HoOrderClient).Name, ViewName=Y_EntityViewData.補充集金指示リスト,
               //     ItemName ="状態", Seq =0, ItemDesc ="状態",
               //     Path= "CN:" + typeof(T_HoOrderClient).Name + "." + T_HoOrderClient.I済,
               //     ItemEntityName=string.Format(cn, ModelBase.CN済), 
               //     EntityAlias="CN"+ ModelBase.CN済,
               //     ItemSql="CN"+ ModelBase.CN済 + ".NAME", 
               //     Width ="10" } ,
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(T_HoOrderClient).Name, ViewName=Y_EntityViewData.補充集金指示リスト, 
               //     ItemName ="初回", Seq =1, ItemDesc ="初回", 
               //     Path= "CN:" + typeof(T_HoOrderClient).Name + "." + T_HoOrderClient.I初回,
               //     ItemEntityName=string.Format(cn, ModelBase.CN初回), 
               //     EntityAlias="CN"+ ModelBase.CN初回,
               //     ItemSql="CN"+ ModelBase.CN初回 + ".NAME",
               //     Width ="10" } ,
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(T_HoOrderClient).Name, ViewName=Y_EntityViewData.補充集金指示リスト, 
               //     ItemName ="担当者", Seq =2, ItemDesc ="担当者", 
               //     Path="N1:" + typeof(T_HoOrderClient).Name + "." + Y_EntityRelaN1Data.rM_STAFF ,
               //     ItemEntityName=typeof(M_Staff).Name, 
               //     EntityAlias=typeof(M_Staff).Name,
               //     ItemSql=typeof(M_Staff).Name + "." + M_Staff.I氏名, 
               //     Width ="20" } ,
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(T_HoOrderClient).Name, ViewName=Y_EntityViewData.補充集金指示リスト, 
               //     ItemName ="お客様", Seq =3, ItemDesc ="お客様", 
               //     Path="N1:" + typeof(T_HoOrderClient).Name + "." + Y_EntityRelaN1Data.rM_CLIENT ,
               //     ItemEntityName=typeof(M_Client).Name,
               //     EntityAlias=typeof(M_Client).Name,
               //     ItemSql=typeof(M_Client).Name + "." + M_Client.I顧客名, 
               //     Width ="100" } ,
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(T_HoOrderClient).Name, ViewName=Y_EntityViewData.補充集金指示リスト, 
               //     ItemName ="住所", Seq =4, ItemDesc ="住所", 
               //     Path = "N1:" + typeof(T_HoOrderClient).Name + "." + Y_EntityRelaN1Data.rM_CLIENT ,
               //     ItemEntityName=typeof(M_Client).Name, 
               //     EntityAlias=typeof(M_Client).Name,
               //     ItemSql=typeof(M_Client).Name + "." + M_Client.I住所, 
               //     Width ="*" } ,

               // //補充集金リスト
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(T_HoClient).Name, ViewName=Y_EntityViewData.補充集金リスト, 
               //     ItemName ="担当者", Seq =2, ItemDesc ="担当者", 
               //     Path="N1:" + typeof(T_HoClient).Name + "." + Y_EntityRelaN1Data.rM_STAFF ,
               //     ItemEntityName=typeof(M_Staff).Name, 
               //     EntityAlias=typeof(M_Staff).Name,
               //     ItemSql=typeof(M_Staff).Name + "." + M_Staff.I氏名, 
               //     Width ="20" } ,
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(T_HoClient).Name, ViewName=Y_EntityViewData.補充集金リスト, 
               //     ItemName ="お客様", Seq =3, ItemDesc ="お客様", 
               //     Path="N1:" + typeof(T_HoClient).Name + "." + Y_EntityRelaN1Data.rM_CLIENT ,
               //     ItemEntityName=typeof(M_Client).Name,
               //     EntityAlias=typeof(M_Client).Name,
               //     ItemSql=typeof(M_Client).Name + "." + M_Client.I顧客名, 
               //     Width ="100" } ,
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(T_HoClient).Name, ViewName=Y_EntityViewData.補充集金リスト, 
               //     ItemName ="補充日", Seq =4, ItemDesc ="補充日", 
               //     Path = "" ,
               //     ItemEntityName=typeof(T_HoClient).Name, 
               //     EntityAlias=typeof(T_HoClient).Name,
               //     ItemSql=typeof(T_HoClient).Name + "." + T_HoClient.I補充日, 
               //     Width ="10" } ,
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(T_HoClient).Name, ViewName=Y_EntityViewData.補充集金リスト, 
               //     ItemName ="売上額", Seq =4, ItemDesc ="売上額", 
               //     Path = "" ,
               //     ItemEntityName=typeof(T_HoClient).Name, 
               //     Alias=typeof(T_HoClient).Name,
               //     ItemSql=typeof(T_HoClient).Name + "." + T_HoClient.I売上, 
               //     Width ="10" } ,

               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(T_HoClient).Name, ViewName=Y_EntityViewData.補充集金リスト, 
               //     ItemName ="集金額", Seq =4, ItemDesc ="集金額", 
               //     Path = "" ,
               //     ItemEntityName=typeof(T_HoClient).Name, 
               //     Alias=typeof(T_HoClient).Name,
               //     ItemSql=typeof(T_HoClient).Name + "." + T_HoClient.I集金, 
               //     Width ="10" } ,

               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(T_HoClient).Name, ViewName=Y_EntityViewData.補充集金リスト, 
               //     ItemName ="過不足", Seq =4, ItemDesc ="過不足", 
               //     Path = "" ,
               //     ItemEntityName=typeof(T_HoClient).Name, 
               //     Alias=typeof(T_HoClient).Name,
               //     ItemSql=typeof(T_HoClient).Name + "." + T_HoClient.I過不足, 
               //     Width ="10" } ,

               ////担当者一覧
               //new Y_EntityViewItem {ProjID =0, EntityName =typeof(M_Staff).Name, ViewName=Y_EntityViewData.担当者一覧, 
               //     ItemName ="店舗", Seq =4, ItemDesc ="店舗", 
               //     Path="N1:" + typeof(M_Staff).Name + "." + Y_EntityRelaN1Data.rM_SHOP ,
               //     ItemEntityName=typeof(M_Shop).Name, 
               //     Alias=typeof(M_Shop).Name,
               //     ItemSql=typeof(M_Shop).Name + "." + M_Shop.I店舗名, 
               //     Width ="10" } ,

               //new Y_EntityViewItem {ProjID =0, EntityName =typeof(M_Staff).Name, ViewName=Y_EntityViewData.担当者一覧, 
               //     ItemName ="社員番号", Seq =4, ItemDesc ="番号", 
               //     Path="" ,
               //     ItemEntityName=typeof(M_Staff).Name, 
               //     Alias=typeof(M_Staff).Name,
               //     ItemSql=typeof(M_Staff).Name + "." + M_Staff.I社員番号, 
               //     Width ="10" } ,

               //new Y_EntityViewItem {ProjID =0, EntityName =typeof(M_Staff).Name, ViewName=Y_EntityViewData.担当者一覧, 
               //     ItemName ="名前", Seq =4, ItemDesc ="名前", 
               //     Path="" ,
               //     ItemEntityName=typeof(M_Staff).Name, 
               //     Alias=typeof(M_Staff).Name,
               //     ItemSql=typeof(M_Staff).Name + "." + M_Staff.I氏名, 
               //     Width ="10" } ,

               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(M_Staff).Name, ViewName=Y_EntityViewData.担当者一覧, 
               //     ItemName ="役割", Seq =4, ItemDesc ="役割", 
               //     Path="" ,
               //     ItemEntityName=typeof(M_Staff).Name, 
               //     Alias=typeof(M_Staff).Name,
               //     ItemSql=typeof(M_Staff).Name + "." + M_Staff.I役割, 
               //     Width ="10" } ,

               //     //店舗一覧
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(M_Shop).Name, ViewName=Y_EntityViewData.店舗一覧, 
               //     ItemName ="店舗コード", Seq =1, ItemDesc ="コード", 
               //     Path="",
               //     ItemEntityName=typeof(M_Shop).Name, 
               //     Alias=typeof(M_Shop).Name,
               //     ItemSql=typeof(M_Shop).Name + "." + M_Shop.I店舗コード, 
               //     Width ="10" } ,
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(M_Shop).Name, ViewName=Y_EntityViewData.店舗一覧, 
               //     ItemName ="店舗名", Seq =2, ItemDesc ="店舗名", 
               //     Path="",
               //     ItemEntityName=typeof(M_Shop).Name, 
               //     Alias=typeof(M_Shop).Name,
               //     ItemSql=typeof(M_Shop).Name + "." + M_Shop.I店舗名, 
               //     Width ="10" } ,
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(M_Shop).Name, ViewName=Y_EntityViewData.店舗一覧, 
               //     ItemName ="店舗区分", Seq =3, ItemDesc ="店舗区分", 
               //     Path="",
               //     ItemEntityName=typeof(M_Shop).Name, 
               //     Alias=typeof(M_Shop).Name,
               //     ItemSql=typeof(M_Shop).Name + "." + M_Shop.I所属店舗区分, 
               //     Format="=Name("+ModelBase.CN店舗区分 + ")",
               //     Width ="10" } ,
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(M_Shop).Name, ViewName=Y_EntityViewData.店舗一覧, 
               //     ItemName ="地域", Seq =4, ItemDesc ="地域", 
               //     Path="",
               //     ItemEntityName=typeof(M_Shop).Name, 
               //     Alias=typeof(M_Shop).Name,
               //     ItemSql=typeof(M_Shop).Name + "." + M_Shop.I地域コード, 
               //     Format="=Name("+ModelBase.CN地域 + ")",
               //     Width ="10" } ,
               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(M_Shop).Name, ViewName=Y_EntityViewData.店舗一覧, 
               //     ItemName ="業務区分", Seq =5, ItemDesc ="業務区分", 
               //     Path="",
               //     ItemEntityName=typeof(M_Shop).Name, 
               //     Alias=typeof(M_Shop).Name,
               //     ItemSql=typeof(M_Shop).Name + "." + M_Shop.I業務区分, 
               //     Format="=Name("+ModelBase.CN店舗業務区分 +")",
               //     Width ="10" } ,

               // new Y_EntityViewItem {ProjID =0, EntityName =typeof(M_Shop).Name, ViewName=Y_EntityViewData.店舗一覧, 
               //     ItemName ="更新時間", Seq =6, ItemDesc ="更新時間", 
               //     Path="",
               //     ItemEntityName=typeof(M_Shop).Name, 
               //     Alias=typeof(M_Shop).Name,
               //     ItemSql=typeof(M_Shop).Name + "." + M_Shop.I更新日時, 
               //     Format="{0:yyyy年MM月dd日}...",
               //     Width ="10" } ,

            }; 
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.EntityViewItems.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}