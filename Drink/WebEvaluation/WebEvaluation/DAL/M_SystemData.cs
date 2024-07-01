//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  システムテーブル
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
    /// システムテーブル
    /// </summary>
    public class M_SystemData
    {

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_System[] GetData()
        {
            return new M_System[] {
                new M_System {ID=0, CusCenterUnitCD="A000000040", AccessIP = "127.0.0.*", AdminIP = "127.0.0.1", PageRowCount=100} 
            };
        }

        public static void Seed(WebEvaluation.DAL.EvaluationContext context)
        {
            context.Systems.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}