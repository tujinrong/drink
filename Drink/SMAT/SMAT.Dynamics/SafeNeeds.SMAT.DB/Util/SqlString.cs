using System;

namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// SQL文の文字列操作クラス
    /// </summary>
    public class SqlString
    {
        private EnumDatabaseType _DbType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbType">データベースタイプ</param>
        public SqlString(EnumDatabaseType dbType)
        {
            _DbType = dbType;
        }

        /// <summary>
        /// SQL文のサブ文字列を取得する
        /// </summary>
        /// <param name="itemName">項目名</param>
        /// <param name="start">開始位置</param>
        /// <param name="len">文字数</param>
        /// <returns>文字列</returns>
        public string SubString(string itemName, int start, int len)
        {
            string sRet = string.Empty;

            if (_DbType == DB.EnumDatabaseType.ACCESS)
            {
                sRet = "mid(" + itemName + "," + start + 1 + "," + len + ")";
            }
            else if (_DbType == DB.EnumDatabaseType.SQLSERVER)
            {
                sRet = "SubString(" + itemName + "," + start + 1 + "," + len + ")";
            }
            else if (_DbType == DB.EnumDatabaseType.ORACLE)
            {
                sRet = "subStr(" + itemName + "," + start + 1 + "," + len + ")";
            }

            return sRet;
        }
    }
}
