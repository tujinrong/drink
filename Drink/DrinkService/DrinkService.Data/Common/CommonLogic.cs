//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Data.Entity;
using System.Linq;
using SafeNeeds.DySmat;
using System.Collections.Generic;
using DrinkService.Data.Logics;

namespace DrinkService.Models
{
    /// <summary>
    /// 店舗マスタ
    /// </summary>
    public class CommonLogic : AdapterBase
    {
        public static List<M_Code> M_CodeKindList = null;

        public static string IsTestVersion = "false";

        public static string GetRouteFromDate(DateTime date)
        {
            DateTime start = new DateTime(2014, 3, 30);
            int d =(int) date.Subtract(start).TotalDays;

            if (d < 0)
            {
                d += ((Math.Abs(d / 28) + 1) * 28);
            }

            int week = (d % 28) / 7 + 1;
            int weekday = (d % 7);

            string route = week.ToString() + weekday.ToString();
            return route;

        }

        public static DateTime GetNextRouteDate(DateTime today, string nextroute)
        {
            for (int i=1;i<=28;i++)
            {
                DateTime date = today.AddDays(i);
                if (GetRouteFromDate(date)==nextroute)
                {
                    return date;
                }

            }
            return today;
        }

        public List<object> GetRouteList(string ShopCD,string date,bool isOnlyDate)
        {

            T_HoClientAdapter logic = new T_HoClientAdapter(new EntityRequest(1, "", ""));

            List<object> routeList = logic.GetShopRoute(ShopCD, date, isOnlyDate);
            return routeList;
        }

        public static List<M_Code> CodeKindList(string kind)
        {
            List<M_Code> codeList;
            if (M_CodeKindList == null) {
                CodeLogic logic = new CodeLogic(new EntityRequest(1, "", ""));
                M_CodeKindList = logic.GetCodeByCD(null);
            }

            if (!string.IsNullOrEmpty(kind))
            {
                codeList = M_CodeKindList.Where(c => c.Kind == kind).ToList();
            }
            else
            {
                codeList = M_CodeKindList;
            }
            return codeList;
        }
    }
}