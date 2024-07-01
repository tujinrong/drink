//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  グループマスタ。
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
    /// グループ
    /// </summary>
    public class M_GroupData 
    {

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_Group[] GetData()
        {
            return new M_Group[] {
                new M_Group { GroupCD = "01", DivCD="1", GroupName="1G"}, 
                new M_Group { GroupCD = "02", DivCD="1", GroupName="2G"}, 
                new M_Group { GroupCD = "03", DivCD="1", GroupName="3G"}, 
                new M_Group { GroupCD = "04", DivCD="2", GroupName="4G"}, 
                new M_Group { GroupCD = "05", DivCD="2", GroupName="5G"}, 
                new M_Group { GroupCD = "06", DivCD="2", GroupName="6G"}, 
                new M_Group { GroupCD = "07", DivCD="3", GroupName="7G"}, 
                new M_Group { GroupCD = "08", DivCD="3", GroupName="8G"}, 
                new M_Group { GroupCD = "09", DivCD="4", GroupName="9G"}, 
                new M_Group { GroupCD = "10", DivCD="4", GroupName="10G"}, 
                new M_Group { GroupCD = "11", DivCD="5", GroupName="11G"}, 
                new M_Group { GroupCD = "12", DivCD="5", GroupName="12G"}, 
                new M_Group { GroupCD = "13", DivCD="8", GroupName="事業推進部"},
                new M_Group { GroupCD = "20", DivCD="7", GroupName="PD"},
                new M_Group { GroupCD = "99", DivCD="9", GroupName="‐"}
            };
   
            /*
    エリア	グループ番号	グループ
    1:東日本	01	1G
    1:東日本	02	2G
    1:東日本	03  3G
    2:関東	04	4G
    2:関東	05	5G
    2:関東	06	6G
    3:東海	07	7G
    3:東海	08	8G
    4:関西	09	9G
    4:関西	10	10G
    5:西日本	11	11G
    5:西日本	12	12G
    PD	    99	PD
            */
        }

        public static void Seed(WebEvaluation.DAL.EvaluationContext context)
        {
            context.Groups.AddOrUpdate(GetData());
            context.SaveChanges();
        }
    }
}