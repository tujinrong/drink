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

namespace DrinkService.Models
{
    /// <summary>
    /// 店舗マスタ
    /// </summary>
    public class CommonLogic : LogicBase
    {

        public static string GetRouteFromDate(DateTime date)
        {
            DateTime start = new DateTime(2014, 3, 30);
            int d =(int) date.Subtract(start).TotalDays;
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
    }
}