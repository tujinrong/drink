using System;
using System.Globalization;

using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;

namespace SafeNeeds.DySmat.Util
{
    /// <summary>
    /// データ処理のユーティリティクラス
    /// </summary>
    public class DataUtil
    {
        #region 　データ型の判定メソッド

        /// <summary>
        /// 数字の判定
        /// </summary>
        /// <param name="objValue">オブジェクト</param>
        /// <returns></returns>
        public static bool IsNumeric(object objValue)
        {
            return true;// Information.IsNumeric(objValue);
        }

        #endregion

        #region 　データ型の変換メソッド

        #region ■Nz____________________________________________________________________


        ///// <summary>
        ///// オブジェクトを文字列に変換する
        ///// </summary>
        ///// <param name="obj">オブジェクト</param>
        ///// <returns></returns>
        //public static string CStr(object obj)
        //{
        //    if (obj is System.DBNull || obj == null)
        //    {
        //        return string.Empty;
        //    }
        //    else
        //    {
        //        return Convert.ToString(obj);
        //    }
        //}


        /// <summary>
        /// オブジェクトを小数(Double)に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static double CDbl(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return 0;
            }
            else if (Convert.ToString(obj) == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

        /// <summary>
        /// オブジェクトを整数(Long)に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static float CFloat(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return 0f;
            }
            else if (Convert.ToString(obj) == "")
            {
                return 0f;
            }
            else
            {
                return Convert.ToSingle(obj);
            }
        }


        ///// <summary>
        ///// オブジェクトを整数(Int)に変換する
        ///// </summary>
        ///// <param name="obj">オブジェクト</param>
        ///// <returns></returns>
        //public static int CInt(object obj)
        //{
        //    if (obj is System.DBNull || obj == null)
        //    {
        //        return 0;
        //    }
        //    else if (Convert.ToString(obj) == "")
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return Convert.ToInt32(obj);
        //    }
        //}


        /// <summary>
        /// オブジェクトを整数(Long)に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static long CLong(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return 0;
            }
            else if (Convert.ToString(obj) == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }


        ///// <summary>
        ///// オブジェクトを数字(Decimal)に変換する
        ///// </summary>
        ///// <param name="obj">オブジェクト</param>
        ///// <returns></returns>
        //public static decimal CDec(object obj)
        //{
        //    if (obj is System.DBNull || obj == null)
        //    {
        //        return 0;
        //    }
        //    else if (Convert.ToString(obj) == "")
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return Convert.ToDecimal(obj);
        //    }
        //}


        /// <summary>
        /// オブジェクトを日付の文字列に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static string NzDate(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return string.Empty;
            }
            else if (obj is System.DateTime)
            {
                return DateToString(Convert.ToDateTime(obj));
            }
            else if (obj is string)
            {
                return System.Convert.ToString(obj);
            }
            else
            {
                throw new ApplicationException("Type error");
            }
        }

        //public static object N(object obj)
        //{
        //    if (obj is System.DBNull || obj == null || CStr(obj) == string.Empty)
        //    {
        //        return System.DBNull.Value;
        //    }
        //    else
        //    {
        //        return obj;
        //    }
        //}

        public static bool CBool(object obj)
        {
            if (obj is System.DBNull || obj == null || CStr(obj) == string.Empty)
            {
                return false;
            }
            else if (obj == System.DBNull.Value)
            {
                return false;
            }
            else if (CStr(obj).ToUpper() == "TRUE")
            {
                return true;
            }
            else if (CStr(obj).ToUpper() == "FALSE")
            {
                return false;
            }
            else if (CStr(obj) == "1")
            {
                return true;
            }
            else if (CStr(obj) == "0")
            {
                return false;
            }

            return false;
        }

        #endregion



        /// <summary>
        /// 日付の文字列を日付に変換する
        /// </summary>
        /// <param name="sDate">日付の文字列</param>
        /// <returns></returns>
        public static DateTime StringToDate(string sDate)
        {
            if (sDate == null)
            {
                return DateTime.Now;
            }

            if (sDate.Trim().Length == 0)
            {
                return DateTime.Now;
            }

            CultureInfo cul = new CultureInfo("en-US", true);

            int iCount = sDate.Length - sDate.Replace("/", "").Length;

            if (iCount == 2)
            {
                if (sDate.Split('/')[0].Length == 2)
                {
                    sDate = "20" + sDate;
                }
                else
                {
                    if (int.Parse(sDate.Split('/')[0]) < 1900)
                    {
                        sDate = sDate.Replace(sDate.Substring(0, 4), "1900");
                    }
                }
            }

            return DateTime.Parse(sDate, cul, DateTimeStyles.NoCurrentDateDefault);
        }


        /// <summary>
        /// 日付を日付の文字列に変換する（固定するフォーマット：yyyy/mm/dd）
        /// </summary>
        /// <param name="date">日付</param>
        /// <returns></returns>
        public static string DateToString(DateTime date)
        {
            CultureInfo cul = new CultureInfo("en-US", true);

            return date.ToString("yyyy\\/MM\\/dd", cul);
        }


        /// <summary>
        /// 日付を日付の文字列に変換する（指定されるフォーマット）
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="sFormat">フォーマット</param>
        /// <returns></returns>
        public static string DateToString(DateTime date, string sFormat)
        {
            CultureInfo cul = new CultureInfo("en-US", true);

            return date.ToString(sFormat, cul);
        }

        /// <summary> 
        /// 完全一致SQLを取得 
        /// </summary> 
        /// <param name="i_value">入力データ</param> 
        /// <returns>完全一致SQL</returns> 
        public static string AllAgreeSql(string i_value)
        {
            string sql = i_value;

            sql = SQLConvert(sql);
            sql = "'" + sql + "'";

            return sql;
        }

        /// <summary> 
        /// 完全一致SQLを取得 
        /// </summary> 
        /// <param name="i_value">入力データ</param> 
        /// <returns>完全一致SQL</returns> 
        public static string AllAgreeSql(object i_value)
        {
            string sql = CStr(i_value);

            sql = SQLConvert(sql);
            sql = "'" + sql + "'";

            return sql;
        }

        /// <summary> 
        /// 部分一致SQLを取得 
        /// </summary> 
        /// <param name="i_value">入力データ</param> 
        /// <returns>部分一致SQL</returns> 
        public static string PartlyAgreeSql(string i_value)
        {
            string sql = i_value;

            sql = ConvertSQL(sql);

            sql = "%" + sql + "%";
            sql = "'" + sql + "' ";

            //sql += SetEscapeSQL();

            return sql;
        }

        /// <summary> 
        /// 前方一致SQLを取得 
        /// </summary> 
        /// <param name="i_value">入力データ</param> 
        /// <returns>前方一致SQL</returns> 
        public static string FirstAgreeSql(string i_value)
        {
            string sql = i_value;

            sql = ConvertSQL(sql);
            sql = sql + "%";
            sql = "'" + sql + "' ";

            sql += SetEscapeSQL();

            return sql;
        }

        /// <summary> 
        /// 前方一致SQLを取得 
        /// </summary> 
        /// <param name="i_value">入力データ</param> 
        /// <returns>前方一致SQL</returns> 
        public static string FirstAgreeSpreadSql(string i_value)
        {
            string sql = i_value;

            sql = ConvertSQL(sql);
            sql = sql + "%";
            sql = "'" + sql + "' ";

            return sql;
        }


        /// <summary> 
        /// SQLキャスト処理 
        /// </summary> 
        /// <param name="i_sql">SQL文</param> 
        /// <returns>キャスト</returns> 
        public static string SQLConvert(string i_sql)
        {
            string sql = i_sql;

            sql = sql.Replace("'", "''");

            return sql;
        }


        /// <summary> 
        /// SQLキャスト処理 
        /// </summary> 
        /// <param name="i_sql">SQL文</param> 
        /// <returns>キャスト</returns> 
        public static string ConvertSQL(string i_sql)
        {
            string sql = i_sql;

            sql = sql.Replace("'", "''");
            sql = sql.Replace("/", "//");
            sql = sql.Replace("%", "/%");
            //sql = sql.Replace("[", "/[");
            sql = sql.Replace("_", "/_");

            return sql;
        }

        public static string FilterSql(string i_str)
        {
            i_str = i_str.Replace("'", "''");
            i_str = i_str.Replace("/", "//");
            i_str = i_str.Replace("%", "/%");
            i_str = i_str.Replace("[", "/[");
            i_str = i_str.Replace("_", "/_");

            return i_str;
        }

        /// <summary> 
        /// ESCAPE文を追加する 
        /// </summary> 
        /// <returns>ESCAPEを追加した文</returns> 
        public static string SetEscapeSQL()
        {
            return " ESCAPE '/' ";
        }

        public static bool IsNullOrEmpty(object v)
        {
            return DataUtil.CStr(v) == string.Empty;
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNumber(object obj)
        {
            string strNumber = CStr(obj);
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }
        #endregion


        /// <summary>
        /// オブジェクトを文字列に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static string CStr(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(obj);
            }
        }


        /// <summary>
        /// オブジェクトを整数(Int)に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static int CInt(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return 0;
            }
            else if (Convert.ToString(obj) == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public static int? CIntN(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return null;
            }
            else if (Convert.ToString(obj) == "")
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        /// <summary>
        /// オブジェクトを整数(Long)に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static long CLng(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return 0;
            }
            else if (Convert.ToString(obj) == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        /// <summary>
        /// オブジェクトを数字(Decimal)に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static decimal CDec(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return 0;
            }
            else if (Convert.ToString(obj) == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }


        public static decimal? CDecN(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return null;
            }
            else if (Convert.ToString(obj) == "")
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }


        /// <summary>
        /// オブジェクトを日付の文字列に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static DateTime CDate(object obj)
        {
            if (obj is System.DBNull || obj == null || CStr(obj).Trim() == string.Empty)
            {
                return DateTime.MinValue;
            }
            else if (obj is System.DateTime)
            {
                return (DateTime)obj;
            }
            else if (obj is string)
            {
                return DateTime.Parse(obj.ToString());
            }
            else
            {
                throw new ApplicationException("Type error");
            }
        }

        public static DateTime? CDateN(object obj)
        {
            if (obj is System.DBNull || obj == null || CStr(obj).Trim() == string.Empty)
            {
                return null;
            }
            else if (obj is System.DateTime)
            {
                return (DateTime)obj;
            }
            else if (obj is string)
            {
                return DateTime.Parse(obj.ToString());
            }
            else
            {
                throw new ApplicationException("Type error");
            }
        }




        /// <summary>
        /// オブジェクトを文字列に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static object DStr(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(obj);
            }
        }




        /// <summary>
        /// オブジェクトを数字(Decimal)に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static object DDec(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return System.Convert.DBNull;
            }
            else if (obj is decimal)
            {
                return (decimal)obj;
            }
            else if (Convert.ToString(obj) == "")
            {
                return System.Convert.DBNull;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }


        /// <summary>
        /// オブジェクトを日付の文字列に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static object DDate(object obj)
        {
            if (CStr(obj) == string.Empty)
            {
                return System.Convert.DBNull;
            }
            else if (obj is System.DateTime)
            {
                return (DateTime)obj;
            }
            else if (obj is string)
            {
                return System.Convert.ToDateTime(obj);
            }
            else
            {
                throw new ApplicationException("Type error");
            }
        }




        /// <summary>
        /// オブジェクトを文字列に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static string VStr(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return string.Empty;
            }
            else if (obj is string)
            {
                return (string)obj;
            }
            else
            {
                return Convert.ToString(obj);
            }
        }



        /// <summary>
        /// オブジェクトを数字(Decimal)に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static decimal? VDec(object obj)
        {
            if (obj is System.DBNull || obj == null)
            {
                return null;
            }
            else if (obj is decimal)
            {
                return (decimal)obj;
            }
            else if (Convert.ToString(obj) == "")
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }


        /// <summary>
        /// オブジェクトを日付の文字列に変換する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns></returns>
        public static DateTime? VDate(object obj)
        {
            if (CStr(obj) == string.Empty)
            {
                return null;
            }
            else if (obj is System.DateTime)
            {
                return (DateTime)obj;
            }
            else if (obj is string)
            {
                return DateTime.Parse(obj.ToString());
            }
            else
            {
                throw new ApplicationException("Type error");
            }
        }

        public static object ConvertDBValue(object value)
        {
            if (value == null)
            {
                return System.DBNull.Value;
            }

            if (DataUtil.IsNullOrEmpty(value))
            {
                return System.DBNull.Value;
            }

            return value;
        }

        public static object GetObjectValue(object obj, string name)
        {
            if (name != string.Empty)
            {
                PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection.Find(name, false);

                if (propertyDescriptor != null)
                {
                    return propertyDescriptor.GetValue(obj);
                }
                else
                {
                    Type type = obj.GetType();
                    FieldInfo fileInfo = type.GetField(name, BindingFlags.Public | BindingFlags.Instance);
                    if (fileInfo != null)
                    {
                        return fileInfo.GetValue(obj);
                    }
                }
            }
            return null;
        }

        public static void SetModelValue(object obj, string propertyName, object value)
        {
            if (!DataUtil.IsNullOrEmpty(propertyName))
            {
                if (value == System.DBNull.Value) return;

                Type type = obj.GetType();

                FieldInfo[] fileInfos = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
                foreach (FieldInfo info in fileInfos)
                {
                    if (info.Name == propertyName)
                    {
                        SetFieldValue(obj, info, value);

                        break;
                    }
                }
            }
        }

        public static void SetFieldValue(object obj, FieldInfo field, object value)
        {
            Type itemType = field.FieldType;
            if (itemType.Name == "Nullable`1")
            {
                if (itemType.FullName.IndexOf("System.Decimal") > 0)
                {
                    field.SetValue(obj, DataUtil.VDec(value));
                }
                else if (itemType.FullName.IndexOf("System.DateTime") > 0)
                {
                    field.SetValue(obj, DataUtil.VDate(value));
                }
            }
            else if (itemType.Name == "DateTime")
            {
                field.SetValue(obj, DataUtil.CDate(value));
            }
            else if (itemType.Name == "Decimal")
            {
                field.SetValue(obj, DataUtil.CDec(value));
            }
            else if (itemType.Name == "Int32")
            {
                field.SetValue(obj, DataUtil.CInt(value));
            }
            else
            {
                field.SetValue(obj, DataUtil.CStr(value));
            }
        }

        #region 加密解密

        private static string key = "safeneeds123456789***///--++,,..??!!$$5%&&*##22~`";
        //DES默认密钥向量
        private static byte[] DES_IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //AES默认密钥向量   
        public static readonly byte[] AES_IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 唐 加密数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="keyFieldList"></param>
        /// <returns></returns>
        public static string Encrypt(string str, string encryptionType)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            string decStr = "";
            switch (encryptionType)
            {
                    
                case "SHA1":
                    decStr = EncryptBySHA1(str);
                    break;
                case "MD5":
                    decStr = EncryptByMD5(str);
                    break;
                case "DES":
                    decStr = EncryptByDES(str, GetEncryptKey());
                    break;
                case "AES":
                default:
                    decStr = EncryptByAES(str, GetEncryptKey());
                    break;
            }
            return decStr;
        }

        /// <summary>
        /// 唐 解密数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="keyFieldList"></param>
        /// <returns></returns>
        public static string Decrypt(string str, string encryptionType)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            string decStr = "";
            switch (encryptionType)
            {
                case "SHA1":
                case "MD5":
                    break;
                case "DES":
                    decStr = DecryptByDES(str, GetEncryptKey());
                    break;
                case "AES":
                default:
                    decStr = DecryptByAES(str, GetEncryptKey());
                    break;
            }
            return decStr;
        }

        public static string GetEncryptKey()
        {
            return EncryptByMD5(key);
        }



        #region MD5
        /// <summary>
        /// MD5加密为32字符长度的16进制字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptByMD5(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();
            //将每个字节转为16进制
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        #endregion

        #region SHA1
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptBySHA1(string input)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] bytes = Encoding.Unicode.GetBytes(input);
            byte[] result = sha.ComputeHash(bytes);
            return BitConverter.ToString(result);
        }
        #endregion

        #region DES
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptByDES(string input, string key)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input); //Encoding.UTF8.GetBytes(input);
            byte[] keyBytes = ASCIIEncoding.UTF8.GetBytes(key);
            byte[] encryptBytes = EncryptByDES(inputBytes, keyBytes, keyBytes);
            //string result = Encoding.UTF8.GetString(encryptBytes); //无法解码,其加密结果中文出现乱码：d\"�e����(��uπ�W��-��,_�\nJn7 
            //原因：如果明文为中文，UTF8编码两个字节标识一个中文字符，但是加密后，两个字节密文，不一定还是中文字符。
            using (DES des = new DESCryptoServiceProvider())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cs))
                        {
                            writer.Write(inputBytes);
                        }
                    }
                }
            }

            string result = Convert.ToBase64String(encryptBytes);

            return result;
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="inputBytes">输入byte数组</param>
        /// <param name="key">密钥，只能是英文字母或数字</param>
        /// <param name="IV">偏移向量</param>
        /// <returns></returns>
        public static byte[] EncryptByDES(byte[] inputBytes, byte[] key, byte[] IV)
        {
            DES des = new DESCryptoServiceProvider();
            //建立加密对象的密钥和偏移量
            des.Key = key;
            des.IV = IV;
            string result = string.Empty;

            //1、如果通过CryptoStreamMode.Write方式进行加密，然后CryptoStreamMode.Read方式进行解密，解密成功。
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputBytes, 0, inputBytes.Length);
                }
                return ms.ToArray();
            }
            //2、如果通过CryptoStreamMode.Write方式进行加密，然后再用CryptoStreamMode.Write方式进行解密，可以得到正确结果
            //3、如果通过CryptoStreamMode.Read方式进行加密，然后再用CryptoStreamMode.Read方式进行解密，无法解密，Error：要解密的数据的长度无效。
            //4、如果通过CryptoStreamMode.Read方式进行加密，然后再用CryptoStreamMode.Write方式进行解密,无法解密，Error：要解密的数据的长度无效。
            //using (MemoryStream ms = new MemoryStream(inputBytes))
            //{
            //    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Read))
            //    {
            //        using (StreamReader reader = new StreamReader(cs))
            //        {
            //            result = reader.ReadToEnd();
            //            return Encoding.UTF8.GetBytes(result);
            //        }
            //    }
            //}
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptByDES(string input, string key)
        {
            //UTF8无法解密，Error: 要解密的数据的长度无效。
            //byte[] inputBytes = Encoding.UTF8.GetBytes(input);//UTF8乱码，见加密算法
            byte[] inputBytes = Convert.FromBase64String(input);

            byte[] keyBytes = ASCIIEncoding.UTF8.GetBytes(key);
            byte[] resultBytes = DecryptByDES(inputBytes, keyBytes, keyBytes);

            string result = Encoding.UTF8.GetString(resultBytes);

            return result;
        }
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] DecryptByDES(byte[] inputBytes, byte[] key, byte[] iv)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //建立加密对象的密钥和偏移量，此值重要，不能修改
            des.Key = key;
            des.IV = iv;

            //通过write方式解密
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
            //    {
            //        cs.Write(inputBytes, 0, inputBytes.Length);
            //    }
            //    return ms.ToArray();
            //}

            //通过read方式解密
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        string result = reader.ReadToEnd();
                        return Encoding.UTF8.GetBytes(result);
                    }
                }
            }

            //错误写法,注意哪个是输出流的位置，如果范围ms，与原文不一致。
            //using (MemoryStream ms = new MemoryStream(inputBytes))
            //{
            //    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
            //    {
            //        cs.Read(inputBytes, 0, inputBytes.Length);
            //    }
            //    return ms.ToArray();
            //}
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string EncryptString(string input, string sKey)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = des.CreateEncryptor();
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return BitConverter.ToString(result);
            }
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DecryptString(string input, string sKey)
        {
            string[] sInput = input.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = des.CreateDecryptor();
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return Encoding.UTF8.GetString(result);
            }
        }
        #endregion

        #region AES
        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="input">明文字符串</param>  
        /// <param name="key">密钥</param>  
        /// <returns>字符串</returns>  
        public static string EncryptByAES(string input, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 32));
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = AES_IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        byte[] bytes = msEncrypt.ToArray();
                        //return Convert.ToBase64String(bytes);//此方法不可用
                        return BitConverter.ToString(bytes);
                    }
                }
            }
        }
        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="input">密文字节数组</param>  
        /// <param name="key">密钥</param>  
        /// <returns>返回解密后的字符串</returns>  
        public static string DecryptByAES(string input, string key)
        {
            //byte[] inputBytes = Convert.FromBase64String(input); //Encoding.UTF8.GetBytes(input);
            string[] sInput = input.Split("-".ToCharArray());
            byte[] inputBytes = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                inputBytes[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 32));
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = AES_IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream(inputBytes))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srEncrypt = new StreamReader(csEncrypt))
                        {
                            return srEncrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        /// <summary> 
        /// AES加密        
        /// </summary> 
        /// <param name="inputdata">输入的数据</param>         
        /// <param name="iv">向量128位</param>         
        /// <param name="strKey">加密密钥</param>         
        /// <returns></returns> 
        public static byte[] EncryptByAES(byte[] inputdata, byte[] key, byte[] iv)
        {
            ////分组加密算法 
            //Aes aes = new AesCryptoServiceProvider();          
            ////设置密钥及密钥向量 
            //aes.Key = key;
            //aes.IV = iv;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            //    {
            //        using (StreamWriter writer = new StreamWriter(cs))
            //        {
            //            writer.Write(inputdata);
            //        }
            //        return ms.ToArray(); 
            //    }               
            //}

            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(inputdata);
                        }
                        byte[] encrypted = msEncrypt.ToArray();
                        return encrypted;
                    }
                }
            }
        }
        /// <summary>         
        /// AES解密         
        /// </summary> 
        /// <param name="inputdata">输入的数据</param>                
        /// <param name="key">key</param>         
        /// <param name="iv">向量128</param> 
        /// <returns></returns> 
        public static byte[] DecryptByAES(byte[] inputBytes, byte[] key, byte[] iv)
        {
            Aes aes = new AesCryptoServiceProvider();
            aes.Key = key;
            aes.IV = iv;
            byte[] decryptBytes;
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        string result = reader.ReadToEnd();
                        decryptBytes = Encoding.UTF8.GetBytes(result);
                    }
                }
            }

            return decryptBytes;
        }
        #endregion

        #region DSA


        #endregion

        #region RSA
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>密文字符串</returns>
        public static string EncryptByRSA(string plaintext, string publicKey)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = ByteConverter.GetBytes(plaintext);
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(publicKey);
                byte[] encryptedData = RSA.Encrypt(dataToEncrypt, false);
                return Convert.ToBase64String(encryptedData);
            }
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>明文字符串</returns>
        public static string DecryptByRSA(string ciphertext, string privateKey)
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(privateKey);
                byte[] encryptedData = Convert.FromBase64String(ciphertext);
                byte[] decryptedData = RSA.Decrypt(encryptedData, false);
                return byteConverter.GetString(decryptedData);
            }
        }

        //public static string signByRSA(string plaintext, string privateKey)
        //{
        //    UnicodeEncoding ByteConverter = new UnicodeEncoding();
        //    byte[] dataToEncrypt = ByteConverter.GetBytes(plaintext);
        //    using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
        //    {
        //        RSA.FromXmlString(privateKey);
        //        byte[] encryptedData = RSA.SignData(dataToEncrypt,);
        //        return Convert.ToBase64String(encryptedData);
        //    }
        //}
        /// <summary>
        /// 数字签名
        /// </summary>
        /// <param name="plaintext">原文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>签名</returns>
        public static string HashAndSignString(string plaintext, string privateKey)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = ByteConverter.GetBytes(plaintext);

            using (RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider())
            {
                RSAalg.FromXmlString(privateKey);
                //使用SHA1进行摘要算法，生成签名
                byte[] encryptedData = RSAalg.SignData(dataToEncrypt, new SHA1CryptoServiceProvider());
                return Convert.ToBase64String(encryptedData);
            }
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="plaintext">原文</param>
        /// <param name="SignedData">签名</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static bool VerifySigned(string plaintext, string SignedData, string publicKey)
        {
            using (RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider())
            {
                RSAalg.FromXmlString(publicKey);
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] dataToVerifyBytes = ByteConverter.GetBytes(plaintext);
                byte[] signedDataBytes = Convert.FromBase64String(SignedData);
                return RSAalg.VerifyData(dataToVerifyBytes, new SHA1CryptoServiceProvider(), signedDataBytes);
            }
        }
        /// <summary>
        /// 获取Key
        /// 键为公钥，值为私钥
        /// </summary>
        /// <returns></returns>
        public static KeyValuePair<string, string> CreateRSAKey()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            string privateKey = RSA.ToXmlString(true);
            string publicKey = RSA.ToXmlString(false);

            return new KeyValuePair<string, string>(publicKey, privateKey);
        }

        #endregion

        #region 十六进制转换

        /// <summary>
        /// 加密 十六进制 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encryption16(String str)
        {
            string result = string.Empty;

            byte[] arrByte = System.Text.Encoding.Default.GetBytes(str);

            for (int i = 0; i < arrByte.Length; i++)
            {
                result += System.Convert.ToString(arrByte[i], 16);        //Convert.ToString(byte, 16)把byte转化成十六进制 
            }

            return result;
        }

        /// <summary>
        /// 解密 十六进制 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decryption16(String str)
        {

            string result = string.Empty;
            byte[] arrByte = new byte[str.Length / 2];
            int index = 0;
            for (int i = 0; i < str.Length; i += 2)
            {
                arrByte[index++] = Convert.ToByte(str.Substring(i, 2), 16);        //Convert.ToByte(string,16)把十六进制string转化成byte 
            }
            result = System.Text.Encoding.Default.GetString(arrByte);

            return result;
        }

        #endregion

        #endregion

    }
}
