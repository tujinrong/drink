using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using WebEvaluation.DAL;
using WebEvaluation.DataModels;
using WebEvaluation.Models;
using WebEvaluation.ViewModels;

namespace WebEvaluation.Utils
{
    public class CommonUtils
    {

        public static List<string> ipList = null;

        public static void AddError(ref List<ImportErrorViewModel> errorList, ImportErrorViewModel error)
        {
            bool errorFlag = false;
            foreach (ImportErrorViewModel model in errorList)
            {
                if (model.ErrorField == error.ErrorField && model.ErrorType == error.ErrorType)
                {
                    errorFlag = true;
                    break;
                }
            }

            if (!errorFlag)
            {
                errorList.Add(error);  
            }
        }

        public static List<string> getIpRules()
        {
            if (null == ipList)
            {
                ipList = new List<string>();
                Hashtable hRule = (Hashtable)ConfigurationManager.GetSection("ipRule");
                IDictionaryEnumerator myEnumerator = hRule.GetEnumerator();

                while (myEnumerator.MoveNext())
                {
                    ipList.Add(myEnumerator.Value.ToString());
                }
            }
            return ipList;
        }

        public static string ParseZero(string str, int len)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                str = str.PadLeft(len, '0');
            }
            return str;
        }

        public static List<string> GetFiledNames<T>() {
            List<string> names = new List<string>();
            PropertyInfo[] fields = typeof(T).GetProperties();
            for (int j = 0; j < fields.Length; j++)
            {
                names.Add(((DisplayAttribute)fields[j].GetCustomAttribute(typeof(DisplayAttribute), true)).Name);
            }
            return names;
        }
        
        /// <summary>
        /// 20140101 => 20140101
        /// 2014/01/01 => 20140101
        /// 2014/1/1 => 20140101
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CDateStr(string str)
        {
            Regex regex = new Regex("\\d{4}\\/\\d{1,2}\\/\\d{1,2}", RegexOptions.Singleline);
            Regex regex2 = new Regex("\\d{4}\\/\\d{2}\\/\\d{2}", RegexOptions.Singleline);

            if (regex.IsMatch(str) && str.Length <= 10)
            {
                if (regex2.IsMatch(str))
                {
                    str = str.Replace("/", "");
                }
                else
                {
                    string[] date = str.Split('/');
                    str = date[0] + date[1].PadLeft(2, '0') + date[2].PadLeft(2, '0');
                } 
            }
            return str;
        }


        public static DateTime getLocalDateTime()
        {
             DateTime date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local);
             return TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));
        }

        public static bool isContains(string sourceStr,string containsStr) {
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