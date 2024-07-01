//*****************************************************************************
// [システム]  
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

        public const string 田中担当者 = "TAN-1";
        public const string 伊藤管理者 = "KAN-1";
        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_Staff[] GetData()
        {
            return new M_Staff[] {
                new M_Staff { ShopCD=M_ShopData.本社育成室,StaffCD = 田中担当者, StaffName = "田中 担当者" , RoleCD = "1"} ,
                new M_Staff { ShopCD=M_ShopData.南森町店,StaffCD = 伊藤管理者, StaffName = "伊藤 管理者", RoleCD = "2"} ,
                new M_Staff { ShopCD=M_ShopData.南森町店, StaffCD = 田中担当者, StaffName = "田中 担当", RoleCD = "3"} ,

            };
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.Staffs.AddOrUpdate(GetData());
            context.SaveChanges();
        }
    }
}