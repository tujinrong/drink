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
    public class T_HoData 
    {
        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>

        public static T_HoClient[] GetData()
        {
            return new T_HoClient[] {
                new T_HoClient { ShopCD = M_ShopData.南森町店, HoDate=new DateTime(2015,1,1), TantoCD = M_StaffData.田中担当者,
                    ClientCD=M_ClientData.ニッシンシステム, Route="1101", Seq =1, 
                    SoldMoney=1000, GetMoney=999, 
                    DiffMoney=1, Memo="備考"}, 
            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.HoClients.AddOrUpdate(GetData());

            context.SaveChanges();
        }
    }
}