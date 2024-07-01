using System;
using System.Text;


namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlDataTime
    {
        private EnumDatabaseType _DbType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbType">データベースタイプ</param>
        public SqlDataTime(EnumDatabaseType dbType)
        {
            _DbType = dbType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string ToYear(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "format(" + itemName + ",'yyyy')";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "datepart(year," + itemName + ")";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_Char(" + itemName + ",'YYYY')";
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string ToMonth(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "month(" + itemName + ")";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "month(" + itemName + ")";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_Number(to_Char(" + itemName + ",'MM'))";
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string ToDay(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "format(" + itemName + ",'dd')";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "SUBSTRING(CONVERT(CHAR(8), " + itemName + ", 112),5,2)";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_Char(" + itemName + ",'DD')";
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string ToYMD(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "format(" + itemName + ",'yyyyMMdd')";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "CONVERT(CHAR(8), " + itemName + ", 112)";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_Char(" + itemName + ",'YYYYMMDD')";
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string FormatYMD(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "format(" + itemName + ",'yyyy/MM/dd')";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "CONVERT(CHAR(10), " + itemName + ", 111)";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_Char(" + itemName + ",'YYYY/MM/DD')";
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string ToYM(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "format(" + itemName + ",'yyyyMM')";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "SUBSTRING(CONVERT(CHAR(8), " + itemName + ", 112),1,6)";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_Char(" + itemName + ",'YYYYMM')";
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string FormatYMDHMS_ODBC(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "format(" + itemName + ",'yyyy-MM-dd HH:mm:ss')";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "CONVERT(CHAR(19), " + itemName + ", 120)";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_Char(" + itemName + ",'YYYY-MM-DD HH24:MI:SS')";
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTime"></param>
        /// <returns></returns>
        public string ToHour(string strTime)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "hour(" + strTime + ")";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "DATEPART(hour, " + strTime + ")";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "TO_NUMBER(to_char(" + strTime + ",'HH24'))";
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string FormatHM(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "format(" + itemName + ",'HH:mm')";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "CONVERT(CHAR(5), " + itemName + ", 108)";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_Char(" + itemName + ",'HH24:MI')";
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public string ToWeekDayNo(string strDate)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "weekday(" + strDate + ")";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "DATEPART(weekday, " + strDate + ")";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_char(" + strDate + ",'D')";
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public string ToWeekDayName(string strDate)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "format(" + strDate + ",'aaa')";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "SubString(DATENAME(weekday, " + strDate + "),1,1)";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "subStr(to_char(" + strDate + ",'DAY'),1,1)";
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="birthdayItem"></param>
        /// <param name="referenceDay"></param>
        /// <returns></returns>
        public string Age(string birthdayItem, string referenceDay)
        {
            StringBuilder sbSql = new StringBuilder();

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sbSql.Append("year(#" + referenceDay + "#) - ");
                sbSql.Append("year(" + birthdayItem + ") ");
                sbSql.Append("-iif(");
                sbSql.Append("format(#" + referenceDay + "#,'MMdd') >= ");
                sbSql.Append("format(" + birthdayItem + ",'MMdd') ");
                sbSql.Append(",0");
                sbSql.Append(",1");
                sbSql.Append(")");
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sbSql.Append("datepart(year,'" + referenceDay + "') - ");
                sbSql.Append("datepart(year," + birthdayItem + ") ");
                sbSql.Append("-(CASE WHEN ");
                sbSql.Append("datepart(month," + birthdayItem + ") * 100 + datepart(day," + birthdayItem + ") ");
                sbSql.Append("-");
                sbSql.Append("datepart(month,'" + referenceDay + "') * 100 - datepart(day,'" + referenceDay + "') ");
                sbSql.Append(">0 THEN 1 ELSE 0 END)");
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sbSql.Append("to_number(to_char(to_Date('" + referenceDay + "','yyyy/mm/dd'),'yyyy')) - ");
                sbSql.Append("to_number(to_char(" + birthdayItem + ",'yyyy')) ");
                sbSql.Append("-decode(");
                sbSql.Append("sign(");
                sbSql.Append("to_number(to_char(" + birthdayItem + ",'mmdd'))");
                sbSql.Append("-to_number(to_char(to_Date('" + referenceDay + "','yyyy/mm/dd'),'mmdd')) ");
                sbSql.Append(")");
                sbSql.Append(",1");
                sbSql.Append(",1");
                sbSql.Append(",0");
                sbSql.Append(")");
            }

            return sbSql.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="birthdayItem"></param>
        /// <param name="referenceDay"></param>
        /// <returns></returns>
        public string AgeForMonth(string birthdayItem, string referenceDay)
        {
            StringBuilder sql = new StringBuilder();
            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sql.Append("cint(format(#" + referenceDay + "#,'yyyy')) - ");
                sql.Append("cint(format(" + birthdayItem + ",'yyyy')) ");
                sql.Append("-iif(");
                sql.Append("format(#" + referenceDay + "#,'MM') >= ");
                sql.Append("format(" + birthdayItem + ",'MM') ");
                sql.Append(",0");
                sql.Append(",1");
                sql.Append(")");
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sql.Append("datepart(year,'" + referenceDay + "') - ");
                sql.Append("datepart(year," + birthdayItem + ") ");
                sql.Append("-(CASE WHEN ");
                sql.Append("datepart(month," + birthdayItem + ") ");
                sql.Append("-");
                sql.Append("datepart(month,'" + referenceDay + "') ");
                sql.Append(">0 THEN 1 ELSE 0 END)");
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sql.Append("to_number(to_char(to_Date('" + referenceDay + "','yyyy/mm/dd'),'yyyy')) - ");
                sql.Append("to_number(to_char(" + birthdayItem + ",'yyyy')) ");
                sql.Append("-decode(");
                sql.Append("sign(");
                sql.Append("to_number(to_char(" + birthdayItem + ",'mm'))");
                sql.Append("-to_number(to_char(to_Date('" + referenceDay + "','yyyy/mm/dd'),'mm')) ");
                sql.Append(")");
                sql.Append(",1");
                sql.Append(",1");
                sql.Append(",0");
                sql.Append(")");
            }
            return sql.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OneDay"></param>
        /// <returns></returns>
        public string LastDayOfDay(string OneDay)
        {
            StringBuilder sql = new StringBuilder();
            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sql.Append("DateAdd('d',-1,");
                sql.Append("DateAdd('m', 1, ");
                sql.Append("Cdate(str(year(" + OneDay + ")) + '/' +str(month(" + OneDay + ")) + '/01')))");
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sql.Append("DateAdd(day,-1,");
                sql.Append("DateAdd(month, 1, ");
                sql.Append("cast(str(year(" + OneDay + ")) + '/' +str( month(" + OneDay + ")) + '/01' AS DATETIME )))");
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sql.Append("LAST_DAY(" + OneDay + ")");
            }
            return sql.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string LastDayOfMonth(string Month)
        {
            StringBuilder sql = new StringBuilder();
            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sql.Append("DateAdd('d',-1,");
                sql.Append("DateAdd('m', 1, ");
                sql.Append("Cdate(Mid(" + Month + ",1,4) + '/' +Mid(" + Month + ",5,2) + '/01'))");
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sql.Append("DateAdd(day,-1,");
                sql.Append("DateAdd(month, 1, ");
                sql.Append("cast(SubString(" + Month + ",1,4) + '/' +SubString(" + Month + ",5,2) + '/01' AS DATETIME )))");
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sql.Append("LAST_DAY(to_date(" + Month + "|| '01','yyyymmdd')");
            }
            return sql.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oneDay"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public string DateAdd(string oneDay, int n)
        {
            StringBuilder sql = new StringBuilder();

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sql.Append("DateAdd('d'," + n + "," + oneDay);
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sql.Append("DateAdd(day," + n + "," + oneDay);
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sql.Append(oneDay + "+" + n);
            }

            return sql.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public string MonthDayToDate(string month, string day)
        {
            StringBuilder sql = new StringBuilder();

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sql.Append("CDate(Mid(" + month + ",1,4) & '/' & Mid(" + month + ",5,2) & '/' & '" + day + "')");
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sql.Append("cast(SubString(" + month + ",1,4) + '/' + SubString(" + month + ",5,2) + '/' + '" + day + "' AS DATETIME)");
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sql.Append("to_date(" + month + "|| '" + day + "','yyyymmdd')");
            }

            return sql.ToString();
        }
    }
}
