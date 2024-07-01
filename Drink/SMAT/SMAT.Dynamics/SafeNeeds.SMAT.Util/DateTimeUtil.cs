//*****************************************************************************
// ユーティリティのライブラリ
//
// 概要　　：　日付時刻関連のユーティリティクラス
//               
// 作成　　：屠
//            2006/05/12 
//
//*****************************************************************************
using System;
using System.Globalization;


namespace SafeNeeds.DySmat.Util
{
    /// <summary>
    /// 日付時刻関連のユーティリティクラス
    /// </summary>
    public class DateTimeUtil
    {
        /// <summary>
        /// 
        /// </summary>
        public class 年号
        {
            /// <summary>
            /// 
            /// </summary>
            public class 漢字
            {
                /// <summary>
                /// 
                /// </summary>
                public const string 明治 = "明治";

                /// <summary>
                /// 
                /// </summary>
                public const string 大正 = "大正";

                /// <summary>
                /// 
                /// </summary>
                public const string 昭和 = "昭和";

                /// <summary>
                /// 
                /// </summary>
                public const string 平成 = "平成";
            }

            /// <summary>
            /// 
            /// </summary>
            public class 英字
            {
                /// <summary>
                /// 
                /// </summary>
                public const string 明治 = "M";

                /// <summary>
                /// 
                /// </summary>
                public const string 大正 = "T";

                /// <summary>
                /// 
                /// </summary>
                public const string 昭和 = "S";

                /// <summary>
                /// 
                /// </summary>
                public const string 平成 = "H";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public enum 表示種類
        {
            /// <summary>
            /// 
            /// </summary>
            漢字 = 0,

            /// <summary>
            /// 
            /// </summary>
            英字
        }

        /// <summary>
        /// 年齢の取得
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static int GetAge(DateTime fromDate, DateTime toDate)
        {
            int iAge = toDate.Year - fromDate.Year;

            string sNewDate = toDate.Year.ToString() + "/" + fromDate.Month.ToString() + "/" + fromDate.Day.ToString();

            if (DateTime.Compare(toDate, DataUtil.StringToDate(sNewDate)) < 0)
            {
                iAge -= 1;
            }

            return iAge;
        }


        /// <summary>
        /// 和暦年号の取得
        /// </summary>
        /// <param name="datetime">日付時刻</param>
        /// <param name="kind">年号の表示種類</param>
        /// <returns></returns>
        public static string Get和暦年号(DateTime datetime, 表示種類 kind)
        {
            JapaneseCalendar jpCal = new JapaneseCalendar();

            string s年号 = string.Empty;
            if (jpCal.GetEra(datetime) == 1)
            {
                if (kind == 表示種類.英字)
                {
                    s年号 = DateTimeUtil.年号.英字.明治;
                }
                else
                {
                    s年号 = DateTimeUtil.年号.漢字.明治;
                }
            }
            else if (jpCal.GetEra(datetime) == 2)
            {
                if (kind == 表示種類.英字)
                {
                    s年号 = DateTimeUtil.年号.英字.大正;
                }
                else
                {
                    s年号 = DateTimeUtil.年号.漢字.大正;
                }
            }
            else if (jpCal.GetEra(datetime) == 3)
            {
                if (kind == 表示種類.英字)
                {
                    s年号 = DateTimeUtil.年号.英字.昭和;
                }
                else
                {
                    s年号 = DateTimeUtil.年号.漢字.昭和;
                }
            }
            else if (jpCal.GetEra(datetime) == 4)
            {
                if (kind == 表示種類.英字)
                {
                    s年号 = DateTimeUtil.年号.英字.平成;
                }
                else
                {
                    s年号 = DateTimeUtil.年号.漢字.平成;
                }
            }

            return s年号;
        }


        /// <summary>
        /// 和暦年の取得
        /// </summary>
        /// <param name="datetime">日付時刻</param>
        /// <param name="kind">年号の表示種類</param>
        /// <returns></returns>
        public static string Get和暦年(DateTime datetime, 表示種類 kind)
        {
            JapaneseCalendar jpCal = new JapaneseCalendar();

            string s年号 = Get和暦年号(datetime, kind);

            string sYear = string.Empty;
            if (kind == 表示種類.英字)
            {
                sYear = s年号 + jpCal.GetYear(datetime);
            }
            else
            {
                sYear = s年号 + jpCal.GetYear(datetime) + "年";
            }

            return sYear;
        }


        /// <summary>
        /// 和暦年月の取得
        /// </summary>
        /// <param name="datetime">日付時刻</param>
        /// <param name="kind">年号の表示種類</param>
        /// <returns></returns>
        public static string Get和暦年月(DateTime datetime, 表示種類 kind)
        {
            JapaneseCalendar jpCal = new JapaneseCalendar();

            string s年号 = Get和暦年号(datetime, kind);

            string sYM = string.Empty;
            if (kind == 表示種類.英字)
            {
                sYM = s年号 + jpCal.GetYear(datetime) + "/" + datetime.Month.ToString("00");
            }
            else
            {
                sYM = s年号 + jpCal.GetYear(datetime) + "年" + datetime.Month.ToString("00") + "月";
            }

            return sYM;
        }


        /// <summary>
        /// 和暦年月日の取得
        /// </summary>
        /// <param name="datetime">日付時刻</param>
        /// <param name="kind">年号の表示種類</param>
        /// <returns></returns>
        public static string Get和暦年月日(DateTime datetime, 表示種類 kind)
        {
            if (datetime.Year == 1) return "";

            JapaneseCalendar jpCal = new JapaneseCalendar();

            string s年号 = Get和暦年号(datetime, kind);

            string sYMD = string.Empty;
            if (kind == 表示種類.英字)
            {
                sYMD = s年号 + jpCal.GetYear(datetime).ToString("00") + "/" +
                        datetime.Month.ToString("00") + "/" +
                        datetime.Day.ToString("00");
            }
            else
            {
                sYMD = s年号 + jpCal.GetYear(datetime).ToString("00") + "年" +
                    datetime.Month.ToString("00") + "月" +
                    datetime.Day.ToString("00") + "日";
            }

            return sYMD;
        }

        /// <summary>
        /// 和暦年月日の取得
        /// </summary>
        /// <param name="datetime">日付時刻 ex. 20101118</param>
        /// <param name="kind">年号の表示種類</param>
        /// <returns></returns>
        public static string Get和暦年月日(string datetime, 表示種類 kind)
        {
            string sYMD = string.Empty;
            DateTime dt = new DateTime();
            if (DateTime.TryParseExact(datetime, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dt) == true)
            {
                JapaneseCalendar jpCal = new JapaneseCalendar();

                string s年号 = Get和暦年号(dt, kind);

                if (kind == 表示種類.英字)
                {
                    sYMD = s年号 + jpCal.GetYear(dt).ToString("00") + "/" +
                            dt.Month.ToString("00") + "/" +
                            dt.Day.ToString("00");
                }
                else
                {
                    sYMD = s年号 + jpCal.GetYear(dt).ToString("00") + "年" +
                        dt.Month.ToString("00") + "月" +
                        dt.Day.ToString("00") + "日";
                }
            }

            return sYMD;
        }

        /// <summary>
        /// 8位数值类型转换成日期格式,如：20101026  -> 2010.10.26
        /// </summary>
        /// <param name="date">需要转换的字符</param>
        /// <param name="format">转换用的格式化字符串</param>
        /// <returns></returns>
        public static string Convert(object date, string format)
        {
            try
            {
                DateTime dt = new DateTime();
                if (DateTime.TryParseExact(DataUtil.CStr(date), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dt) == true)
                {
                    return dt.ToString(format);
                }

                return DataUtil.CStr(date);
            }
            catch
            {
                return DataUtil.CStr(date);
            }
        }

        /// <summary>
        /// 8位数值类型转换成日期类型,如：20101026  -> 2010/10/26
        /// </summary>
        /// <param name="value"></param>
        /// <returns>DateTime</returns>
        public static DateTime ParseToDate(object value)
        {
            DateTime dt = new DateTime();
            if (DateTime.TryParseExact(DataUtil.CStr(value), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dt) == true)
            {
                return dt;
            }
            else
            {
                throw new System.ArgumentException();
            }
        }

        public static bool IsDate(object value)
        {
            DateTime dt = new DateTime();
            if (DateTime.TryParseExact(DataUtil.CStr(value), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dt) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
