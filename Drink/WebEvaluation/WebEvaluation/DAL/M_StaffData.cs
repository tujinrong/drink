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
using WebEvaluation.Models;

namespace WebEvaluation.DAL
{
    /// <summary>
    /// 社員マスタ
    /// </summary>
    public class M_StaffData 
    {

        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static M_Staff[] GetData()
        {
            return new M_Staff[] {
                new M_Staff { StaffCD = "1", StaffName = "韓非", StaffKana = "ｲｼﾊﾗ ﾏｺﾄ", Sex = "1", UnitCD="TG"} ,
                new M_Staff { StaffCD = "2", StaffName = "屠錦栄", StaffKana = "ｲｼﾊﾗ ﾏｺﾄ", Sex = "1", UnitCD="AB"} ,
                new M_Staff { StaffCD = "3", StaffName = "上長さま", StaffKana = "ｲｼﾊﾗ ﾏｺﾄ", Sex = "1", UnitCD="AB"} ,
                new M_Staff { StaffCD = "4", StaffName = "管理者", StaffKana = "ｲｼﾊﾗ ﾏｺﾄ", Sex = "1", UnitCD="AB"} ,
                //new M_Staff { StaffCD = "3001", StaffName = "石原　誠", StaffKana = "ｲｼﾊﾗ ﾏｺﾄ", Sex = "1"} ,
                //new M_Staff { StaffCD = "10117", StaffName = "渋谷　健一", StaffKana = "ｼﾌﾞﾔ ｹﾝｲﾁ", Sex = "1"} ,
                //new M_Staff { StaffCD = "3128", StaffName = "菅井　利行", StaffKana = "ｽｶﾞｲ ﾄｼﾕｷ", Sex = "1"} ,
                //new M_Staff { StaffCD = "10108", StaffName = "大塚　崇浩", StaffKana = "ｵｵﾂｶ ﾀｶﾋﾛ", Sex = "1"} ,
                //new M_Staff { StaffCD = "18224", StaffName = "岩間　惇", StaffKana = "ｲﾜﾏ ｼﾞｭﾝ", Sex = "2"} ,
                //new M_Staff { StaffCD = "3003", StaffName = "森　理江", StaffKana = "ﾓﾘ ﾏｻｴ", Sex = "1"} ,
                //new M_Staff { StaffCD = "4246", StaffName = "江森　飛鳥", StaffKana = "ｴﾓﾘ ｱｽｶ", Sex = "1"} ,

            };
        }

        public static void Seed(WebEvaluation.DAL.EvaluationContext context)
        {
            context.Staffs.AddOrUpdate(GetData());
            context.SaveChanges();
        }
    }
}