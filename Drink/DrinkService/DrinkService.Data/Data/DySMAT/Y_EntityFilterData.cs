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
    public class Y_EntityFilterData
    {

        public const string ShopFilter = "ShopFilter";
        public const string StaffFilter = "StaffFilter";
        public const string RouteFilter = "RouteFilter";
        public const string OrderTantoFilter = "OrderTantoFilter";
        public const string HoDateFilter = "HoDateFilter";
        public const string StaffNameFilter = "StaffNameFilter";
        public const string ShopNameFilter = "ShopNameFilter";
        public const string RegionFilter = "RegionFilter";
        public const string ShopTypeFilter = "ShopTypeFilter";
        public const string DoneFilter = "DoneFilter";
        public const string DoneStopFilter = "DoneStopFilter";
        public const string UndoFilter = "UndoFilter";

        public const string HoDateFromFilter = "HoDateFromFilter";
        public const string HoDateToFilter = "HoDateToFilter";
        public const string ClientFilter = "ClientFilter";
        public const string LastFilter = "LastFilter";
        public const string LastEmptyFilter = "LastEmptyFilter";

        public const string ReferClientFilter = "ReferClientFilter";
        public const string ItemIgnoreFilter = "ItemIgnoreFilter";
        public const string ReferItemFilter = "ReferItemFilter";
        public const string ReferShopTypeFilter = "ReferShopTypeFilter";
        public const string ReferShopKeyFilter = "ReferShopKeyFilter";
        public const string ItemMakerFilter = "ItemMakerFilter";

        public const string ClientNameFilter = "ClientNameFilter";
        public const string ItemCDFilter = "ItemCDFilter";
        public const string ItemNameFilter = "ItemNameFilter";
        public const string ItemKitFilter = "ItemKitFilter";
        public const string ItemKitNameFilter = "ItemKitNameFilter";
        public const string DoneFlagFilter = "DoneFlagFilter";

        public const string StaffNotNullFilter = "StaffNotNullFilter";
        
        public const string DownloadDateFilter = "DownloadDateFilter";
        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static Y_EntityFilterControl[] GetControlData()
        {
            return new Y_EntityFilterControl[] {
                //補充集金指示リスト
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(T_HoOrderClient).Name, 
                    FilterControlName =ShopFilter, FilterControlDesc ="店舗", FilterNames =ShopFilter } ,
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(T_HoOrderClient).Name,  
                    FilterControlName =StaffFilter, FilterControlDesc ="担当者",  FilterNames= StaffFilter }, 
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(T_HoOrderClient).Name,  
                    FilterControlName =RouteFilter, FilterControlDesc ="ルート",  FilterNames=RouteFilter}, 
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(T_HoOrderClient).Name,  
                    FilterControlName =HoDateFilter, FilterControlDesc ="補充日",  FilterNames=HoDateFilter},
                //補充集金リスト
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(T_HoClient).Name, 
                    FilterControlName =ShopFilter, FilterControlDesc ="店舗", FilterNames=ShopFilter}, 
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(T_HoClient).Name,  
                    FilterControlName =StaffFilter,FilterControlDesc ="担当者", FilterNames=StaffFilter},
                   
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(T_HoClient).Name,  
                    FilterControlName =RouteFilter, FilterControlDesc ="ルート", FilterNames=RouteFilter}, 
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(T_HoClient).Name,  
                    FilterControlName =HoDateFilter, FilterControlDesc ="補充日", FilterNames=HoDateFilter}, 

                 //担当者一覧
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(M_Staff).Name, 
                    FilterControlName =ShopFilter, FilterControlDesc ="店舗", FilterNames=ShopFilter}, 
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(M_Staff).Name, 
                    FilterControlName =StaffNameFilter, 
                    FilterControlDesc ="名前",  FilterNames=StaffNameFilter},

                    //店舗一覧
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(M_Shop).Name, 
                    FilterControlName =ShopNameFilter, 
                    FilterControlDesc ="店舗",  FilterNames=ShopNameFilter},

                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(M_Shop).Name, 
                    FilterControlName =RegionFilter, 
                    FilterControlDesc ="地域", FilterNames=RegionFilter}, 

                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(M_Shop).Name, 
                    FilterControlName =ShopTypeFilter, 
                    FilterControlDesc ="店舗区分", FilterNames=ShopTypeFilter}, 

                    
                    //顧客一覧
                new Y_EntityFilterControl { ProjID =1, EntityName =typeof(M_Client).Name, 
                    FilterControlName =ShopFilter, 
                    FilterControlDesc ="店舗",  FilterNames=ShopFilter},


            };
        }

        public static Y_EntityFilter[] GetFilterData()
        {

            return new Y_EntityFilter[] {
                //補充集金指示リスト
                new Y_EntityFilter { ProjID =1, EntityName =typeof(T_HoOrderClient).Name, 
                    FilterName =ShopFilter, 
                    FilterDesc ="店舗", 
                    Path ="", ItemEntityAliasName =typeof(T_HoOrderClient).Name,
                     FilterSql=typeof(T_HoOrderClient).Name + "." + T_HoOrderClient.I店舗コード + "='{0}'"},

                new Y_EntityFilter { ProjID =1, EntityName =typeof(T_HoOrderClient).Name,  
                    FilterName =StaffFilter, 
                    FilterDesc ="担当者", 
                    Path ="", ItemEntityAliasName = typeof(T_HoOrderClient).Name, 
                     FilterSql=typeof(T_HoOrderClient).Name + "." + T_HoOrderClient.I担当者コード + "='{0}'"},

                //new Y_EntityFilter { ProjID =1, EntityName =typeof(T_HoOrderClient).Name,  
                //    FilterName =RouteFilter, 
                //    FilterDesc ="ルート", 
                //    Path ="..",  ItemEntityAliasName=typeof(T_HoOrderTanto).Name,
                //     FilterSql=typeof(T_HoDay).Name + "," + T_HoDay.Iルート + "='{0}'"},

                //new Y_EntityFilter { ProjID =1, EntityName =typeof(T_HoOrderClient).Name,  
                //    FilterName =HoDateFilter, 
                //    FilterDesc ="補充日", 
                //    Path ="", ItemEntityAliasName = typeof(T_HoOrderTanto).Name,
                //    FilterSql=typeof(T_HoOrderClient).Name + "." + T_HoOrderClient.I補充日 + "='{0}'"},

                //補充集金リスト
                new Y_EntityFilter { ProjID =1, EntityName =typeof(T_HoClient).Name, 
                    FilterName =ShopFilter, 
                    FilterDesc ="店舗", 
                    Path ="", ItemEntityAliasName = typeof(T_HoOrderClient).Name, 
                     FilterSql= typeof(T_HoClient).Name + "." +T_HoClient.I店舗コード + "='{0}'"},

                //new Y_EntityFilter { ProjID =1, EntityName =typeof(T_HoClient).Name,  
                //    FilterName =StaffFilter, 
                //    FilterDesc ="担当者", 
                //    Path ="", ItemEntityAliasName = typeof(T_HoOrderTanto).Name, 
                //     FilterSql= typeof(T_HoClient).Name + "." + T_HoClient.I担当者 + "='{0}'"},

                //new Y_EntityFilter { ProjID =1, EntityName =typeof(T_HoClient).Name,  
                //    FilterName =RouteFilter, 
                //    FilterDesc ="ルート", 
                //    Path ="", ItemEntityAliasName = typeof(T_HoOrderTanto).Name, 
                //     FilterSql= typeof(T_HoClient).Name + "." + T_HoClient.Iルート +"='{0}'"},

                new Y_EntityFilter { ProjID =1, EntityName =typeof(T_HoClient).Name,  
                    FilterName =HoDateFilter, 
                    FilterDesc ="補充日", 
                    Path ="" , ItemEntityAliasName="",
                     FilterSql=typeof(T_HoClient).Name + T_HoClient.I補充日 +"='{0}"},

                 //担当者一覧
                new Y_EntityFilter { ProjID =1, EntityName =typeof(M_Staff).Name, 
                    FilterName =ShopFilter, 
                    FilterDesc ="店舗", 
                    Path ="", ItemEntityAliasName = typeof(M_Staff).Name, 
                     FilterSql=typeof(M_Staff).Name + "." +  M_Staff.I店舗コード + "='{0}"},

                new Y_EntityFilter { ProjID =1, EntityName =typeof(M_Staff).Name, 
                    FilterName =StaffNameFilter, 
                    FilterDesc ="名前", 
                    Path ="", ItemEntityAliasName = typeof(M_Staff).Name, 
                     FilterSql=typeof(M_Staff).Name + "." + M_Staff.I氏名 + " LIKE '%{0}%'"},

                    //店舗一覧
                new Y_EntityFilter { ProjID =1, EntityName =typeof(M_Shop).Name, 
                    FilterName =ShopNameFilter, 
                    FilterDesc ="店舗", 
                    Path ="", ItemEntityAliasName = typeof(M_Shop).Name, 
                     FilterSql= typeof(M_Shop).Name + "." + M_Shop.I店舗名 + " LIKE '%{0}%'"},

                new Y_EntityFilter { ProjID =1, EntityName =typeof(M_Shop).Name, 
                    FilterName =RegionFilter, 
                    FilterDesc ="地域", 
                    Path ="", ItemEntityAliasName = typeof(M_Shop).Name, 
                     FilterSql =typeof(M_Shop).Name + "." + M_Shop.I地域コード + "='{0}'"},

                new Y_EntityFilter { ProjID =1, EntityName =typeof(M_Shop).Name, 
                    FilterName =ShopTypeFilter, 
                    FilterDesc ="店舗区分", 
                    Path ="", ItemEntityAliasName = typeof(M_Shop).Name, 
                    FilterSql= typeof(M_Shop).Name + "." +  M_Shop.I所属店舗区分 + "='{0}'"},

                //店舗リスト
                new Y_EntityFilter { ProjID =1, EntityName =typeof(M_Client).Name, 
                    FilterName =ShopFilter, 
                    FilterDesc ="店舗", 
                    Path ="", ItemEntityAliasName = typeof(M_Client).Name, 
                     FilterSql= typeof(M_Client).Name + "." +M_Client.I店舗コード + "='{0}'"},


            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.EntityFilterControls.AddOrUpdate(GetControlData());
            context.EntityFilters.AddOrUpdate(GetFilterData());
            context.SaveChanges();
        }


    }
}