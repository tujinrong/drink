using System;
using System.Text;

namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlMath
    {
        private SafeNeeds.DySmat.DB.EnumDatabaseType _DbType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType"></param>
        public SqlMath(SafeNeeds.DySmat.DB.EnumDatabaseType dbType)
        {
            _DbType = dbType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Floor(string value)
        {
            string sRet = string.Empty;

            if (_DbType == DB.EnumDatabaseType.ACCESS)
            {
                sRet = "int(" + value + ")";
            }
            else if (_DbType == DB.EnumDatabaseType.SQLSERVER)
            {
            }
            else if (_DbType == DB.EnumDatabaseType.ORACLE)
            {
                sRet = "floor(" + value + ")";
            }

            return sRet;
        }

        /// <summary>
        /// èËó]ÇéÊìæÇ∑ÇÈ
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="byNumber"></param>
        /// <returns></returns>
        public string myMod(string itemName, string byNumber)
        {
            string sRet = string.Empty;

            if (_DbType == DB.EnumDatabaseType.ACCESS)
            {
                sRet = itemName + " MOD " + byNumber;
            }
            else if (_DbType == DB.EnumDatabaseType.SQLSERVER)
            {
                sRet = "MOD(" + itemName + "," + byNumber + ")";
            }
            else if (_DbType == DB.EnumDatabaseType.ORACLE)
            {
                sRet = "MOD(" + itemName + "," + byNumber + ")";
            }

            return sRet;
        }
    }
}
