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
    ///// <summary>
    ///// 店舗マスタ
    ///// </summary>
    //public class T_HoOrderData 
    //{
    //    /// <summary>
    //    /// テストデータ
    //    /// </summary>
    //    /// <returns></returns>
    //    public static T_HoOrderTanto[] GetData()
    //    {
    //        return new T_HoOrderTanto[] {
    //            new T_HoOrderTanto { ShopCD = M_ShopData.南森町店, HoDate=new DateTime(2015,1,1), TantoCD = M_StaffData.田中担当者, Route="1100"}, 
    //        };
    //    }

    //    public static T_HoOrderClient[] GetOrderClientData()
    //    {
    //        return new T_HoOrderClient[] {
    //            new T_HoOrderClient { ShopCD = M_ShopData.南森町店, HoDate=new DateTime(2015,1,1), TantoCD = M_StaffData.田中担当者, ClientCD=M_ClientData.ニッシンシステム, DoneFlag = "1", FirstFlag = "1"}, 
    //        };
    //    }

    //    public static void Seed(DrinkService.Models.DrinkServiceContext context)
    //    {
    //        context.HoOrders.AddOrUpdate(GetData());
    //        context.HoOrderClients.AddOrUpdate(GetOrderClientData());

    //        context.SaveChanges();
    //    }
    //}
}