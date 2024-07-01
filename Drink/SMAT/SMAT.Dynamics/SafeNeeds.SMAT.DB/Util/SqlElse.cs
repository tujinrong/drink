using System;
using System.Text;


namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// SQL文のその他クラス
    /// </summary>
    public class SqlElse
    {
        private EnumDatabaseType _DbType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbType">データベースタイプ</param>
        public SqlElse(EnumDatabaseType dbType)
        {
            this._DbType = dbType;
        }

        /// <summary>
        /// 期間を取得する
        /// </summary>
        /// <param name="dateItem">日付項目</param>
        /// <param name="startYear">開始年</param>
        /// <param name="startMonth">開始月</param>
        /// <returns>期間</returns>
        public string GetDays(string dateItem, int startYear, int startMonth)
        {
            StringBuilder sql = new StringBuilder();
            if (this._DbType == DB.EnumDatabaseType.ACCESS)
            {
                sql.Append("year(" + dateItem + ") - " + startYear);
                sql.Append("-iif(");
                sql.Append("cint(format(" + dateItem + ",'MM')) >= " + startMonth + ",0,1)");
            }
            else if (this._DbType == DB.EnumDatabaseType.ORACLE)
            {
            }
            else if (this._DbType == DB.EnumDatabaseType.SQLSERVER)
            {
                sql.Append("year(" + dateItem + ") - " + startYear);
                sql.Append("+ case sign(MONTH(" + dateItem + ")-" + startMonth + ") when -1 then -1 else 0 end");
            }
            return sql.ToString();
        }
    }
}
