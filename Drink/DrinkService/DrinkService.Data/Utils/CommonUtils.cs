using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkService.Utils
{
    public class CommonUtils
    {
        public static bool isContains(string sourceStr, string containsStr)
        {
            if (sourceStr == null)
            {
                return false;
            }
            else
            {
                if (sourceStr.Contains(containsStr))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool StartsWith(string sourceStr, string containsStr)
        {
            if (sourceStr == null)
            {
                return false;
            }
            else
            {
                if (sourceStr.StartsWith(containsStr))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static DateTime GetDateTimeNow()
        {
            DateTime date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local);
            date = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));
            return date;
        }

        public static string FormatItemCD(string itemCD)
        {
            itemCD = itemCD.PadRight(8, '0');
            return itemCD.Substring(0, 2) + "-" + itemCD.Substring(2, 4) + "-" + itemCD.Substring(6, 2);
        }
    }
}