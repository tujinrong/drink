using System;
using System.Text;

using SafeNeeds.DySmat.DB;
using SafeNeeds.DySmat.Util;

namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlBuilder
    {
        private EnumDatabaseType _DbType;
        private StringBuilder _Sql = new StringBuilder();
        private SqlDataTime _SqlDataTime;
        private SqlType _SqlType;
        private SqlString _SqlString;
        private SqlMath _SqlMath;
        private SqlLogic _SqlLogic;
        private SqlElse _SqlElse;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="dbType">�f�[�^�x�[�X�^�C�v</param>
        public SqlBuilder(EnumDatabaseType dbType)
        {
            _DbType = dbType;
            _SqlDataTime = new SqlDataTime(dbType);
            _SqlMath = new SqlMath(dbType);
            _SqlString = new SqlString(dbType);
            _SqlType = new SqlType(dbType);
            _SqlLogic = new SqlLogic(dbType);
            _SqlElse = new SqlElse(dbType);
        }
       
        #region " �v���p�e�B "
        
        /// <summary>
        /// 
        /// </summary>
        public SqlDataTime ���t�Ǝ����֐�
        {
            get
            {
                return _SqlDataTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlDataTime FncDataTime
        {
            get
            {
                return _SqlDataTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlMath ���w�֐�
        {
            get
            {
                return _SqlMath;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlMath FncMath
        {
            get
            {
                return _SqlMath;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlString ������֐�
        {
            get
            {
                return _SqlString;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlString FncString
        {
            get
            {
                return _SqlString;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlType �f�[�^�^�֐�
        {
            get
            {
                return _SqlType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlType FncType
        {
            get
            {
                return _SqlType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlLogic �_���֐�
        {
            get
            {
                return _SqlLogic;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlLogic FncLogic
        {
            get
            {
                return _SqlLogic;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlElse ���̑��֐�
        {
            get
            {
                return _SqlElse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlElse FncElse
        {
            get
            {
                return _SqlElse;
            }
        }
        #endregion

        #region " ���\�b�h "

        /// <summary>
        /// SQL����ǉ�����
        /// </summary>
        /// <param name="sql">SQL��</param>
        public void Append(string sql)
        {
            _Sql.Append(sql);
        }

        /// <summary>
        /// ���s�t����SQL����ǉ�����
        /// </summary>
        /// <param name="sql">SQL��</param>
        public void AppendLine(string sql)
        {
            _Sql.Append(sql + DBOption.SqlCrLf);
        }

        /// <summary>
        /// �e�[�u���ʖ���ǉ�����
        /// </summary>
        /// <param name="tableName">�e�[�u���ʖ�</param>
        public void AppendAlias(string tableName)
        {
            if (_DbType == DB.EnumDatabaseType.ORACLE)
            {
                _Sql.Append(" " + tableName);
            }
            else
            {
                _Sql.Append(" AS " + tableName);
            }
        }

        /// <summary>
        /// SQL��SELECT����ǉ�����
        /// </summary>
        public void AppendSelect()
        {
            _Sql.Append("SELECT ");
        }

        /// <summary>
        /// SQL��SELECT����ǉ�����
        /// </summary>
        /// <param name="itemName">���ږ�</param>
        public void AppendSelect(string itemName)
        {
            _Sql.Append("SELECT " + itemName);
        }

        /// <summary>
        /// SQL��SELECT����ǉ�����
        /// </summary>
        /// <param name="itemName">���ږ�</param>
        /// <param name="aliasName">�ʖ�</param>
        public void AppendSelect(string itemName, string aliasName)
        {
            _Sql.Append("SELECT " + itemName);

            if (_DbType == DB.EnumDatabaseType.ORACLE)
            {
                _Sql.Append(" " + aliasName);
            }
            else
            {
                _Sql.Append(" AS " + aliasName);
            }
        }

        /// <summary>
        /// DISTINCT�I�v�V�����t���̑I�����ڂ�ǉ�����
        /// </summary>
        public void AppendSelectDistinct()
        {
            _Sql.Append("SELECT DISTINCT ");
        }

        /// <summary>
        /// DISTINCT�I�v�V�����t���̑I�����ڂ�ǉ�����
        /// </summary>
        /// <param name="itemName">���ږ�</param>
        public void AppendSelectDistinct(string itemName)
        {
            _Sql.Append("SELECT DISTINCT " + itemName);
        }

        /// <summary>
        /// DISTINCT�I�v�V�����t���̑I�����ڂ�ǉ�����
        /// </summary>
        /// <param name="itemName">���ږ�</param>
        /// <param name="aliasName">�ʖ�</param>
        public void AppendSelectDistinct(string itemName, string aliasName)
        {
            _Sql.Append("SELECT DISTINCT " + itemName);
            if (_DbType == DB.EnumDatabaseType.ORACLE)
            {
                _Sql.Append(" " + aliasName);
            }
            else
            {
                _Sql.Append(" AS " + aliasName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        public void AppendItem(string itemName)
        {
            _Sql.Append(",");
            _Sql.Append(itemName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="aliasName"></param>
        public void AppendItem(string itemName, string aliasName)
        {
            this._Sql.Append(",");
            if (_DbType == DB.EnumDatabaseType.ORACLE)
            {
                _Sql.Append(itemName + " " + aliasName);
            }
            else
            {
                _Sql.Append(itemName + " AS " + aliasName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        public void AppendFrom(string tableName)
        {
            _Sql.Append(DBOption.SqlCrLf + "FROM " + tableName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        public void AppendWhere(string where)
        {
            _Sql.Append(DBOption.SqlCrLf + "WHERE " + where);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        public void AppendWhereAnd(string where)
        {
            _Sql.Append(DBOption.SqlCrLf + "AND " + where);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupBy"></param>
        public void AppendGroupBy(string groupBy)
        {
            _Sql.Append(DBOption.SqlCrLf + "GROUP BY " + groupBy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="word"></param>
        public void AppendLikeHead(string item, string word)
        {
            _Sql.Append(item + " LIKE '" + word + "%'");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="word"></param>
        public void AppendLikeMiddle(string item, string word)
        {
            _Sql.Append(item + " LIKE '%" + word + "%'");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        public void AppendInsert(string tableName)
        {
            _Sql.Append("INSERT INTO  " + tableName + " ");
        }

        /// <summary>
        /// 
        /// </summary>
        public void AppendUnion()
        {
            _Sql.Append(DBOption.SqlCrLf + "UNION" + DBOption.SqlCrLf);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _Sql = new StringBuilder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SqlBuilder.ModifySql(_Sql.ToString(), _DbType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static string ModifySql(string sql, EnumDatabaseType dbType)
        {
            if (DBOption.MultiDatabase == false)
            {
                return sql;
            }
            if (sql.IndexOf("&") > 0)
            {
                if (dbType == EnumDatabaseType.SQLSERVER)
                {
                    sql = sql.Replace("&", "+");
                }
                else if (dbType == EnumDatabaseType.ORACLE)
                {
                    sql = sql.Replace("&", "|");
                }
            }
            if (sql.IndexOf("#") > 0)
            {
                if (dbType == EnumDatabaseType.ORACLE || dbType == EnumDatabaseType.SQLSERVER)
                {
                    sql = ChangeSharp(sql);
                }
            }
            if (sql.IndexOf(" AS ") > 0)
            {
                if (dbType == EnumDatabaseType.ORACLE)
                {
                    sql = sql.Replace(" AS ", " ");
                }
            }
            if (sql.ToUpper().IndexOf("<>") > 0)
            {
                if (dbType == EnumDatabaseType.ACCESS)
                {
                }
                else if (dbType == EnumDatabaseType.ORACLE)
                {
                    sql = sql.Replace("<>", "!=");
                }
                else if (dbType == EnumDatabaseType.SQLSERVER)
                {
                    sql = sql.Replace("<>", "!=");
                }
            }

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string ChangeSharp(string sql)
        {
            string[] sTemps = sql.Split('#');

            if (sTemps.Length >= 3 && (sTemps.Length % 2) == 1)
            {
                if (sTemps[1].Length >= 8)
                {
                    if (StringUtil.IsDate(sTemps[1]))
                    {
                        sql = sql.Replace("#", "'");
                    }
                }
            }

            return sql;
        }

#endregion
    }

}
//namespace SafeNeeds.SMAT.Util
//{
//    public class StringUtil
//    {
//        public static bool IsDate(object o)
//        {
//            return true;
//        }

//        public static string DateToString(DateTime d)
//        {
//            return "";
//        }

//        public static bool IsDate(string s)
//        {
//            return true;
//        }

        
//    }
//    public class DateUtil
//    {
//        public static string DateToString(DateTime d)
//        {
//            return "";

//        }
    
//    }
//    public class DataUtil
//    {
//        public static int CInt(object o)
//        {
//            return 0;
//        }

//        public static string DateToString(DateTime d)
//        {
//            return "";

//        }
//        public static string DateToString(DateTime d, string fmt)
//        {
//            return "";
//        }
//        public static string CStr(object o)
//        {
//            return "";
//        }
//        public static DateTime StringToDate(string s)
//        {
//            return DateTime.Now;
//        }
//    }
//}
