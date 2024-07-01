//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  事業部マスタ。
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
    /// 事業部
    /// </summary>
    public class M_DivisionData
    {


        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_Division[] GetData()
        {
            return new M_Division[] {
                new M_Division { DivCD = "1", DivName = "東日本" } ,
                new M_Division { DivCD = "2", DivName = "関東" } ,
                new M_Division { DivCD = "3", DivName = "東海" } ,
                new M_Division { DivCD = "4", DivName = "関西" } ,
                new M_Division { DivCD = "5", DivName = "西日本" },
                new M_Division { DivCD = "7", DivName = "PD" } ,
                new M_Division { DivCD = "8", DivName = "事業推進部" } ,
                new M_Division { DivCD = "9", DivName = "‐" } 
                
            };
        }

        public static void Seed(WebEvaluation.DAL.EvaluationContext context)
        {
            context.Divisions.AddOrUpdate(GetData());
            context.SaveChanges();
        }
    }
}