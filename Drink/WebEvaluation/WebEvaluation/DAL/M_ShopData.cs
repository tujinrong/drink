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
using WebEvaluation.Models;


namespace WebEvaluation.DAL
{
    /// <summary>
    /// 店舗マスタ
    /// </summary>
    public class M_ShopData 
    {


        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_Shop[] GetData()
        {
            return new M_Shop[] {
                //new M_Shop { ShopCD = "AB", ShopName = "アンティコ・ブッテロ（東京/広尾）",GroupCD=""}, 
                //new M_Shop { ShopCD = "ACF", ShopName = "アーククラブ迎賓館（福山）", GroupCD="11"}
            };
        }
 
        public static void Seed(WebEvaluation.DAL.EvaluationContext context)
        {
            context.Shops.AddOrUpdate(GetData());
            context.SaveChanges();
        }
    }
}