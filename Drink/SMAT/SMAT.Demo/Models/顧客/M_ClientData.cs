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
    public class M_ClientData 
    {

        public const string ニッシンシステム = "200";
                /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_Client[] GetData()
        {
            return new M_Client[] {
                new M_Client { ShopCD = M_ShopData.南森町店, ClientCD =ニッシンシステム, ClientName="ニッシンシステム"}, 
            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.Clients.AddOrUpdate(GetData());
            context.SaveChanges();
        }
    }
}