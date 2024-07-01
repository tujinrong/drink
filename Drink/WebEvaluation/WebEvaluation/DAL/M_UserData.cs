//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  ユーザマスタ
//
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using WebEvaluation.Models;
using System.Data.Entity.Migrations;

namespace WebEvaluation.DAL
{
    /// <summary>
    /// ユーザマスタ
    /// </summary>
    public class M_UserData 
    {

        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_User[] GetData()
        {
            return new M_User[] {
                new M_User { UserID = "1", Password = "1" ,StaffCD="1", RoleCD="02"} ,
                new M_User { UserID = "2", Password = "2" ,StaffCD="2", RoleCD="03"} ,
                new M_User { UserID = "3", Password = "3" ,StaffCD="3", RoleCD="01"} ,
                new M_User { UserID = "4", Password = "4" ,StaffCD="4", RoleCD="01"} ,

            };
        }

        public static void Seed(WebEvaluation.DAL.EvaluationContext context)
        {
            context.Users.AddOrUpdate(GetData());
            context.SaveChanges();
        }

    }
}