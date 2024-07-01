//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  社員マスタ
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
    /// 社員マスタ
    /// </summary>
    public class M_StaffData 
    {

        public const string 田中 = "3333";
        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_Staff[] GetData()
        {
            return new M_Staff[] {
                new M_Staff { StaffCD = "1111", StaffName = "韓非", ShopCD="1000", RoleCD = "1"} ,
                new M_Staff { StaffCD = "2222", StaffName = "伊藤 管理者", ShopCD="1000", RoleCD = "2"} ,
                new M_Staff { ShopCD=M_ShopData.南森町店, StaffCD = 田中, StaffName = "田中 担当", RoleCD = "3"} ,

            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.Staffs.AddOrUpdate(GetData());
            context.SaveChanges();
        }
    }
}