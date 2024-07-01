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
    public class M_ItemData 
    {
                /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_Item[] GetData()
        {
            return new M_Item[] {
                new M_Item { ItemCD = "1001",  ItemName="コカ・コーラ", ShortName="コカ・コーラ", ShopPrice=100, StandardPrice =100, InNum =12}, 
                new M_Item { ItemCD = "1002",  ItemName="CCレモン", ShortName="CCレモン", ShopPrice=100, StandardPrice =100, InNum =12}, 
            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.Items.AddOrUpdate(GetData());
            context.SaveChanges();
        }
    }
}