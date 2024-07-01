using System;
using System.Collections.Generic;

using SafeNeeds.DySmat.Model;

using SafeNeeds.DySmat.DB;
using SafeNeeds.DySmat.Util;
//using SafeNeeds.SMAT.Core.Define;

namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class Common
    {
        private static DateTime _LastTime = new DateTime(1900, 1, 1);
        private static TimeSpan _TimeDiff;
        
        /// <summary>
        /// データベースとの接続状態をチェックする
        /// </summary>
        /// <param name="config">データベースの情報</param>
        /// <returns>True:接続状態、False：未接続状態</returns>
        //public static bool CheckConnection(DBConfig config)
        //{
        //    try
        //    {
        //        DateTime temp = GetSystemTime(config, true);
        //    }
        //    catch (System.Exception)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        /// <summary>
        /// 接続文字列を取得する
        /// </summary>
        /// <param name="provider">プロバイダーのタイプ</param>
        /// <param name="source">データソース名</param>
        /// <param name="db">データベース名</param>
        /// <param name="uid">ユーザーID</param>
        /// <param name="pwd">パスワード</param>
        /// <returns>接続文字列</returns>
        public static string GetConnnectionStr(EnumProviderType provider, string source, string db, string uid, string pwd)
        {
            string sConn = string.Empty;

            if (provider == EnumProviderType.MS_SQL_SQLSERVER)
            {
                
                if (uid == "Trusted_Connection" || uid=="" )
                {
                    sConn = "data source=" + source +
                            ";initial catalog=" + db +
                            ";persist security info=False" +
                            ";Trusted_Connection=Yes" +
                            ";workstation id=" + GetHostName() +
                            ";packet size=4096";
                }
                else
                {
                    sConn = "data source=" + source +
                            ";initial catalog=" + db +
                            ";persist security info=False" +
                            ";user id=" + uid +
                            ";password=" + pwd +
                            ";workstation id=" + GetHostName() +
                            ";packet size=4096";
                }
            }
            else if (provider == EnumProviderType.MS_OLEDB_Jet)
            {
                if (uid == "")
                {
                    uid = "Admin";
                }
                if (source.Substring(source.Length - 1, 1) != "\\")
                {
                    source += "\\";
                }
                
                sConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + 
                        "Password='" + pwd + "'" +
                        ";User ID=" + uid + 
                        ";Data Source=" + source + db + 
                        ";Mode=Share Deny None";
            }
            else if (provider == EnumProviderType.MS_OLEDB_ORACLE)
            {
                if (source == "")
                {
                    source = db;
                }
                sConn = "Provider=MSDAORA.1;data source=" + source + ";" + "user id=" + uid + ";" + "password=" + pwd;
            }
            else if (provider == EnumProviderType.ORA_ORACLE)
            {
                if (source == "")
                {
                    source = db;
                }
                sConn = "data source=" + source + ";" + "user id=" + uid + ";" + "password=" + pwd;
            }
            else if (provider == EnumProviderType.ORA_OLEDB_ORACLE)
            {
                if (source == "")
                {
                    source = db;
                }
                sConn = "Provider=OraOLEDB.Oracle.1;data source=" + source + ";" + "user id=" + uid + ";" + "password=" + pwd;
            }
            else if (provider == EnumProviderType.MS_OLEDB_SQLSERVER)
            {
                if (uid == "Trusted_Connection" || uid == "")
                {
                    sConn = "Trusted_Connection=Yes" +
                            ";Data Source=" + source +
                            ";Initial Catalog=" + db +
                            ";Use Procedure for Prepare=1" +
                            ";Auto Translate=True" +
                            ";Persist Security Info=False" +
                            ";Provider=\"SQLOLEDB.1\"" +
                            ";Use Encryption for Data=False" +
                            ";Packet Size=4096";
                }
                else
                {
                    sConn = "User ID=" + uid +
                            ";password=" + pwd +
                            ";Data Source=" + source +
                            ";Initial Catalog=" + db +
                            ";Use Procedure for Prepare=1" +
                            ";Auto Translate=True" +
                            ";Persist Security Info=False" +
                            ";Provider=\"SQLOLEDB.1\"" +
                            ";Use Encryption for Data=False" +
                            ";Packet Size=4096";
                }
            }
            else if (provider == EnumProviderType.MS_ODBC_SQLSERVER)
            {
                sConn = "Driver={SQL Server}" + 
                        ";Server=" + source + 
                        ";Trusted_Connection=yes" + 
                        ";Database=" + db + 
                        ";User ID=" + uid + 
                        ";password=" + pwd + 
                        ";";
            }
            else if (provider == EnumProviderType.MS_ODBC_ORACLE)
            {
                sConn = "Driver={Microsoft ODBC for Oracle}" + 
                        ";Server=" + source +
                        ";Persist Security Info=False" +
                        ";Trusted_Connection=yes" +
                        ";User ID=" + uid + 
                        ";password=" + pwd + 
                        ";";
            }
            else if (provider == EnumProviderType.MS_ODBC_ACCESS)
            {
                sConn = "Driver={Microsoft Access Driver (*.mdb)}" + 
                        ";DBQ=" + source + 
                        ";User ID=" + uid + 
                        ";password=" + pwd + 
                        ";";
            }

            return sConn;
        }

        ///// <summary>
        ///// サーバーの日時を取得する
        ///// </summary>
        ///// <param name="config">データベースの情報</param>
        ///// <returns>日時</returns>
        //public static DateTime GetSystemTime(DBConfig config)
        //{
        //    return GetSystemTime(config, false);
        //}

        /// <summary>
        /// サーバーの日時を取得する
        /// </summary>
        /// <param name="config">データベースの情報</param>
        /// <param name="fromDB">True：DBから取得、False：サーバーから取得</param>
        /// <returns>日時</returns>


        /// <summary>
        /// Stringの時刻をDate型にキャストする
        /// </summary>
        /// <param name="dbType">データベースの種類</param>
        /// <param name="time">文字列時刻</param>
        /// <returns>時刻</returns>
        public static DateTime CDBTime(EnumDatabaseType dbType, string time)
        {
            DateTime dateTime = new DateTime();

            if (time.IndexOf(" ") > 0)
            {
                time = time.Substring(time.IndexOf(" "));
            }
            if (dbType == EnumDatabaseType.SQLSERVER)
            {
                dateTime = System.Convert.ToDateTime("1899/12/30 " + time);
            }
            else
            {
                dateTime = Convert.ToDateTime(time);
            }

            return dateTime;
        }

        /// <summary>
        /// SQLのWhere分を取得する
        /// </summary>
        /// <param name="fldsDef"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string GetWhere(List<Y_EntityField> fldsDef, string[] name, object[] value, SqlConfig config)
        {
            List<string> al = new List<string>();
            int iLength;

            if (name.Length < value.Length)
            {
                iLength = name.Length;
            }
            else
            {
                iLength = value.Length;
            }

            for (int i = 0; i <= iLength - 1; i++)
            {
                string n = name[i];
                object v = value[i];
                if (!(v == null))
                {
                    Y_EntityField f = null; // fldsDef[n];
                    al.Add(GetWhere(f, v, config));
                }
            }
            if (al.Count == 0)
            {
                return "1=1";
            }
            else if (al.Count == 1)
            {
                return System.Convert.ToString(al[0]);
            }
            else
            {
                return "(" + string.Join(" AND ", (string[])al.ToArray()) + ")";
            }
        }

        /// <summary>
        /// SQLのWhere分を取得する
        /// </summary>
        /// <param name="fldDef"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        internal static string GetWhere(Y_EntityField fldDef, object value, SqlConfig config)
        {
            string sRet = string.Empty;

            if (value == null)
            {
                sRet = "1=1";
            }
            if (fldDef.DataType =="xxxxxxx")
            {
                if (value is System.DBNull)
                {
                    sRet = fldDef.FieldName + " is Null";
                }
                else
                {
                    sRet = fldDef.FieldName + "='" + Convert.ToString(value) + "'";
                }
            }
            else if (fldDef.DataType == "DataType.DECIMAL" ||
                    fldDef.DataType == "DataType.FLOAT" ||
                    fldDef.DataType == "DataType.NUMBER")
            {
                if (value is System.DBNull)
                {
                    sRet = fldDef.FieldName + " is Null";
                }
                else if (value is string)
                {
                    if (System.Convert.ToString(value) == "")
                    {
                        sRet = fldDef.FieldName + " is Null";
                    }
                    else
                    {
                        sRet = fldDef.FieldName + "=" + Convert.ToString(value);
                    }
                }
                else
                {
                    sRet = fldDef.FieldName + "=" + Convert.ToString(value);
                }
            }
            else if (fldDef.DataType == "DataType.DATE")
            {
                if (value is System.DBNull)
                {
                    sRet = fldDef.FieldName + " is Null";
                }

                DateTime d = Convert.ToDateTime(value);

                if (fldDef.DataType == "CharType.Date")
                {
                    if (config.Database == DB.EnumDatabaseType.ACCESS)
                    {
                        sRet = fldDef.FieldName + "=#" + DataUtil.DateToString(d) + "#";
                    }
                    else if (config.Database == DB.EnumDatabaseType.SQLSERVER)
                    {
                        sRet = fldDef.FieldName + "='" + DataUtil.DateToString(d) + "'";
                    }
                    else if (config.Database == DB.EnumDatabaseType.ORACLE)
                    {
                        sRet = fldDef.FieldName + "='" + DataUtil.DateToString(d) + "'";
                    }
                }
                else if (fldDef.DataType == "CharType.Time")
                {
                    if (config.Database == EnumDatabaseType.ACCESS)
                    {
                        sRet = "format(" + fldDef.FieldName + ",'HH:mm:ss')='" + d.ToString("HH:mm:ss") + "'";
                    }
                    else if (config.Database == EnumDatabaseType.SQLSERVER)
                    {
                        sRet = "CONVERT(CHAR(19), " + fldDef.FieldName + ", 108)='" + d.ToString("HH:mm:ss") + "'";
                    }
                    else if (config.Database == EnumDatabaseType.ORACLE)
                    {
                        sRet = "to_Char(" + fldDef.FieldName + ",'HH24:MI:SS')='" + d.ToString("HH:mm:ss") + "'";
                    }
                }
                //else if (fldDef.CharType == CharType.DateTime)
                //{
                //    if (config.Database == EnmDataBaseType.ACCESS)
                //    {
                //        sRet = "format(" + fldDef.FieldName + ",'yyyy-MM-dd HH:mm:ss')='" + DataUtil.DateToString(d, "yyyy-MM-dd HH:mm:ss") + "'";
                //    }
                //    else if (config.Database == EnmDataBaseType.SQLSERVER)
                //    {
                //        sRet = "CONVERT(CHAR(19), " + fldDef.FieldName + ", 120)='" + DataUtil.DateToString(d, "yyyy-MM-dd HH:mm:ss") + "'";
                //    }
                //    else if (config.Database == EnmDataBaseType.ORACLE)
                //    {
                //        sRet = "to_Char(" + fldDef.FieldName + ",'YYYY-MM-DD HH24:MI:SS')='" + DataUtil.DateToString(d, "yyyy-MM-dd HH:mm:ss") + "'";
                //    }
                //}
                else
                {
                    throw new ApplicationException("文字タイプ未定義です");
                    //if (d.Hour == 0 && d.Minute == 0 && d.Second == 0)
                    //{
                    //    sRet = f.FieldName + "=#" + DataUtil.DateToString(d) + "#";
                    //}
                    //else
                    //{
                    //    if (config.Database == DB.DataBaseType.ACCESS)
                    //    {
                    //        sRet = "format(" + f.FieldName + ",'yyyy-MM-dd HH:mm:ss')='" + d.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    //    }
                    //    else if (config.Database == DB.DataBaseType.SQLSERVER)
                    //    {
                    //        sRet = "CONVERT(CHAR(19), " + f.FieldName + ", 120)='" + d.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    //    }
                    //    else if (config.Database == DB.DataBaseType.ORACLE)
                    //    {
                    //        sRet = "to_Char(" + f.FieldName + ",'YYYY-MM-DD HH24:MI:SS')='" + d.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    //    }
                    //}
                }
            }
            //else if (fldDef.FieldType == DataType.BOOLEAN)
            //{
            //    sRet = fldDef.FieldName + "=" + System.Convert.ToString(value);
            //}
            //else if (fldDef.FieldType == DataType.LONGTEXT)
            //{
            //    sRet = fldDef.FieldName + "=" + Convert.ToString(value);
            //}

            return sRet;
        }

        private static string m_hostName=string.Empty;
        public static string GetHostName()
        {
            if (m_hostName == string.Empty)
            {
                m_hostName = System.Net.Dns.GetHostName();
            }
            return m_hostName;
        }
    }
}
