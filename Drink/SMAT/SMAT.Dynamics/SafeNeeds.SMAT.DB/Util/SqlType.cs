using System;

namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// SQL文の型変換クラス
    /// </summary>
    public class SqlType
    {
        private EnumDatabaseType _DbType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbType">データベースタイプ</param>
        public SqlType(EnumDatabaseType dbType)
        {
            _DbType = dbType;
        }

        /// <summary>
        /// Varchar型に変換する
        /// </summary>
        /// <param name="itemName">項目名</param>
        /// <returns>型変換された値</returns>
        public string ToVarchar(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "CSTR(" + itemName + ")";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "CAST(" + itemName + " AS Varchar)";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_Char(" + itemName + ")";
            }

            return sRet;
        }

        /// <summary>
        /// Int型に変換する
        /// </summary>
        /// <param name="itemName">項目名</param>
        /// <returns>型変換された値</returns>
        public string ToInt(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "cint(" + itemName + ")";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "CAST(" + itemName + " AS INT)";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_number(" + itemName + ")";
            }

            return sRet;
        }

        /// <summary>
        /// SQLで使用する型に変換する
        /// </summary>
        /// <param name="date">日付</param>
        /// <returns>型変換された値</returns>
        public string ToSQLDate(string date)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "#" + date + "#";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "'" + date + "'";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_date('" + date + "','yyyy/mm/dd')";
            }

            return sRet;
        }

        /// <summary>
        /// 数字の先頭に「0」を埋める
        /// </summary>
        /// <param name="itemName">項目名</param>
        /// <param name="digit">「0」の個数</param>
        /// <returns>変換された値</returns>
        public string FillZero(string itemName, int digit)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "Format(" + itemName + ",'" + new string('0', digit) + "')";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = itemName;
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "LPAD(" + itemName + "," + digit + ",'0')";
            }

            return sRet;
        }
    }
}
