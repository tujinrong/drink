//*****************************************************************************
// [システム]  
// 
// [機能概要]  店舗マスタ。
//
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using DrinkService.Models;


namespace DrinkService.Models
{
    /// <summary>
    /// 店舗マスタ
    /// </summary>
    public class M_ShopData 
    {

        public const string 南森町店 = "SHOP-01";
        public const string 本社育成室 = "KANRI-1";
        public const string 情報システム部 = "KANRI-2";

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_Shop[] GetData()
        {
            return new M_Shop[] {
                new M_Shop { ShopCD = 情報システム部, ShopName = "情報システム部",ShopTypeCD="1",RegionCD ="008", SysTypeCD="1", UpdateTime=new DateTime(2015,3,11,1,1,1)},
                new M_Shop { ShopCD = 本社育成室, ShopName = "本社育成室",ShopTypeCD="1",RegionCD="008", SysTypeCD="2", UpdateTime=new DateTime(2015,3,12)}, 
                new M_Shop { ShopCD = 南森町店, ShopName = "大阪南森町店", ShopTypeCD = "2",RegionCD="008", SysTypeCD="1", UpdateTime=new DateTime(2015,3,12)},
            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.Shops.AddOrUpdate(GetData());
            context.SaveChanges();
        }
    }
}