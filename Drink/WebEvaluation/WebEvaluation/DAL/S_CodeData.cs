//*****************************************************************************
// [システム]  T&G テレフォンレポート管理
// 
// [機能概要]  名称テーブル
//             管理画面は不要            
//  
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using WebEvaluation.Models;
using System.Linq;
using System.Collections.Generic;


namespace WebEvaluation.DAL
{
    /// <summary>
    /// 名称テーブル
    /// </summary>
    public class S_CodeData
    {

        public static S_Code[] GetData()
        {
            return new S_Code[] {
                new S_Code {  Kind = "Sex",  CD = "1" ,  Name ="男"} ,
                new S_Code {  Kind = "Sex",  CD = "2" ,  Name ="女"} ,
                new S_Code {  Kind = "RoleCD",  CD = "01" ,  Name ="店舗"} ,
                new S_Code {  Kind = "RoleCD",  CD = "02" ,  Name ="カスタマセンター"} ,
                new S_Code {  Kind = "RoleCD",  CD = "03" ,  Name ="閲覧"} ,
                new S_Code {  Kind = "RoleCD",  CD = "04" ,  Name ="カスタマセンター上長"} ,
                new S_Code {  Kind = "RoleCD",  CD = "09" ,  Name ="システム管理者"} ,
                new S_Code {  Kind = "EvaStatus",  CD = "1" ,  Name ="レポート済"} ,
                new S_Code {  Kind = "EvaStatus",  CD = "2",  Name ="評価済"} ,
                new S_Code {  Kind = "EvaStatus",  CD = "3" ,  Name ="二次評価済"} ,

                new S_Code {  Kind = "EvaVal",  CD = "A" ,  Name ="01"} ,
                new S_Code {  Kind = "EvaVal",  CD = "B" ,  Name ="02"} ,
                new S_Code {  Kind = "EvaVal",  CD = "C" ,  Name ="03"} ,
                new S_Code {  Kind = "EvaVal",  CD = "D" ,  Name ="04"} ,
                new S_Code {  Kind = "EvaVal",  CD = "E" ,  Name ="05"} ,
                new S_Code {  Kind = "EvaVal",  CD = "F" ,  Name ="06"} ,
                new S_Code {  Kind = "EvaVal",  CD = "H" ,  Name ="07"} ,
                new S_Code {  Kind = "EvaVal",  CD = "X" ,  Name ="08"} ,
                new S_Code {  Kind = "EvaVal",  CD = "Y" ,  Name ="09"} ,
                new S_Code {  Kind = "EvaVal",  CD = "Z" ,  Name ="10"} ,

                new S_Code {  Kind = "EvaValCdt",  CD = "A" ,  Name ="A"} ,
                new S_Code {  Kind = "EvaValCdt",  CD = "B" ,  Name ="B"} ,
                new S_Code {  Kind = "EvaValCdt",  CD = "C" ,  Name ="C"} ,
                new S_Code {  Kind = "EvaValCdt",  CD = "D" ,  Name ="D"} ,
                new S_Code {  Kind = "EvaValCdt",  CD = "E" ,  Name ="E"} ,
                new S_Code {  Kind = "EvaValCdt",  CD = "F" ,  Name ="F"} ,
                new S_Code {  Kind = "EvaValCdt",  CD = "H" ,  Name ="H"} ,
                new S_Code {  Kind = "EvaValCdt",  CD = "X" ,  Name ="X"} ,
                new S_Code {  Kind = "EvaValCdt",  CD = "Y" ,  Name ="Y"} ,
                new S_Code {  Kind = "EvaValCdt",  CD = "Z" ,  Name ="Z"} ,
                new S_Code {  Kind = "EvaValCdt",  CD = "ZA" ,  Name ="未評価"} ,
                new S_Code {  Kind = "EvaValCdt",  CD = "ZB" ,  Name ="評価済"} ,

                new S_Code {  Kind = "EvaVal2",  CD = "SS" ,  Name ="01"} ,
                new S_Code {  Kind = "EvaVal2",  CD = "S" ,  Name ="02"} ,
                new S_Code {  Kind = "EvaVal2",  CD = "A" ,  Name ="03"} ,
                new S_Code {  Kind = "EvaVal2",  CD = "B" ,  Name ="04"} ,
                new S_Code {  Kind = "EvaVal2",  CD = "C" ,  Name ="05"} ,
                new S_Code {  Kind = "EvaVal2",  CD = "D" ,  Name ="06"} ,
                new S_Code {  Kind = "EvaVal2",  CD = "E" ,  Name ="07"} ,
                new S_Code {  Kind = "EvaVal2",  CD = "F" ,  Name ="08"} ,

                new S_Code {  Kind = "EvaVal2Cdt",  CD = "  SS" ,  Name ="SS"} ,
                new S_Code {  Kind = "EvaVal2Cdt",  CD = " S" ,  Name ="S"} ,
                new S_Code {  Kind = "EvaVal2Cdt",  CD = "A" ,  Name ="A"} ,
                new S_Code {  Kind = "EvaVal2Cdt",  CD = "B" ,  Name ="B"} ,
                new S_Code {  Kind = "EvaVal2Cdt",  CD = "C" ,  Name ="C"} ,
                new S_Code {  Kind = "EvaVal2Cdt",  CD = "D" ,  Name ="D"} ,
                new S_Code {  Kind = "EvaVal2Cdt",  CD = "E" ,  Name ="E"} ,
                new S_Code {  Kind = "EvaVal2Cdt",  CD = "F" ,  Name ="F"} ,
                new S_Code {  Kind = "EvaVal2Cdt",  CD = "ZA" ,  Name ="未評価"} ,
                new S_Code {  Kind = "EvaVal2Cdt",  CD = "ZB" ,  Name ="評価済"} ,
                new S_Code {  Kind = "EvaResult",  CD = "0" ,  Name ="繋がった"} ,
                new S_Code {  Kind = "EvaResult",  CD = "1" ,  Name ="繋がらず"} ,
                new S_Code {  Kind = "EvaResult",  CD = "2" ,  Name ="繋がったが話せず"} ,
                new S_Code {  Kind = "EvaResult",  CD = "3" ,  Name ="留守電"} ,
                new S_Code {  Kind = "EvaResult",  CD = "4" ,  Name ="書面アンケート"} ,
                new S_Code {  Kind = "Season",  CD = "1" ,  Name ="4～6月"} ,
                new S_Code {  Kind = "Season",  CD = "2" ,  Name ="7～9月"} ,
                new S_Code {  Kind = "Season",  CD = "3" ,  Name ="10～12月"} ,
                new S_Code {  Kind = "Season",  CD = "4" ,  Name ="1～3月"} ,
                new S_Code {  Kind = "ShopType",  CD = "1" ,  Name ="HW"} ,
                new S_Code {  Kind = "ShopType",  CD = "2" ,  Name ="PD"} ,
                new S_Code {  Kind = "ShopType",  CD = "3" ,  Name ="ﾚｽﾄﾗﾝ"} ,
                new S_Code {  Kind = "ShopType",  CD = "4" ,  Name ="ﾎﾃﾙ"} ,
                new S_Code {  Kind = "ShopType",  CD = "5" ,  Name ="BW"} ,

            };


        }

        public static void Seed(WebEvaluation.DAL.EvaluationContext context)
        {
            context.Codes.AddOrUpdate(GetData());
            context.SaveChanges();
        }

    }



}