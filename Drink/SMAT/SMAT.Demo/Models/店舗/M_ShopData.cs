//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
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

        public const string 南森町店 = "1001";
        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_Shop[] GetData()
        {
            return new M_Shop[] {
                new M_Shop { ShopCD = "9999", ShopName = "情報システム部",ShopTypeCD="1",RegionCD ="008", SystemFlag="0", UpdateTime=new DateTime(2015,3,11,1,1,1)},
                new M_Shop { ShopCD = "1000", ShopName = "本社育成室",ShopTypeCD="1",RegionCD="008", SystemFlag="1", UpdateTime=new DateTime(2015,3,12)}, 
                new M_Shop { ShopCD = 南森町店, ShopName = "大阪南森町店", ShopTypeCD = "2",RegionCD="008", SystemFlag="1", UpdateTime=new DateTime(2015,3,12)},
            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.Shops.AddOrUpdate(GetData());
            context.SaveChanges();
        }
    }
}