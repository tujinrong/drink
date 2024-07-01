//*****************************************************************************
// ユーティリティのライブラリ
//
// 概要　　：　INIファイル関連のユーティリティクラス
//               
// 作成　　：屠
//            2006/05/23
//
//*****************************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace SafeNeeds.DySmat.Util
{
    /// <summary>
    /// ini文件操作类
    /// </summary>
    public class IniFileUtil
    {
        #region ■私有的成员变量__________________________________________________
        private string m_fileName = string.Empty;
        private string m_fileContent = string.Empty;
        private const string FILE_NOT_EXSIT = "指定されたINIファイルが存在していません。";
        #endregion

        #region ■初始化__________________________________________________________
        /// <summary>
        ///  コンストラクター
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        public IniFileUtil(string fileName)
        {
            if (File.Exists(fileName))
            {
                StreamReader sr = new StreamReader(fileName, Encoding.Default);

                m_fileContent = sr.ReadToEnd();

                sr.Close();

                m_fileName = fileName;
            }
            else
            {
                throw new Exception(FILE_NOT_EXSIT);
            }
        }
        #endregion

        #region ■属性____________________________________________________________
        /// <summary>
        /// セクション名を取得する
        /// </summary>
        public string[] SectionNames
        {
            get
            {
                //-------------------------------------------------------------
                // 正規表現を利用し、セクション名を取得する
                //-------------------------------------------------------------
                // 正規表現パターンを設定する
                string sRegexPattern = @"\[(?<SectionName>\w*)\]";

                Regex r = new Regex(sRegexPattern);

                // パターンに一致したセクション名を取得する
                MatchCollection matches = r.Matches(m_fileContent);

                //-------------------------------------------------------------
                // 全部のセクション名を文字列配列に格納する
                //-------------------------------------------------------------
                string[] results = new string[matches.Count];

                for (int i = 0; i < matches.Count; i++)
                {
                    results[i] = matches[i].Result("${SectionName}");
                }

                return results;
            }
        }
        #endregion

        #region ■公开方法________________________________________________________
        /// <summary>
        /// 指定したセクションに該当するキー名を取得する
        /// </summary>
        /// <param name="sectionName">セクション名</param>
        /// <returns></returns>
        public string[] GetKeyNames(string sectionName)
        {
            //-------------------------------------------------------------
            // 正規表現を利用し、キー名を取得する処理
            //-------------------------------------------------------------
            // 正規表現パターンを設定する
            string sRegexPattern = @"\[" + sectionName + @"\]" + @"(?<Section>.*)";

            Regex r = new Regex(sRegexPattern, RegexOptions.Singleline);

            // パターンに一致した文字列を取得する
            if (r.IsMatch(m_fileContent))
            {
                string sMatch = r.Match(m_fileContent).Result("${Section}");

                int iPos = sMatch.IndexOf(StringUtil.CRLF + "[");
                if (iPos > 0)
                {
                    sMatch = sMatch.Substring(0, iPos + StringUtil.CRLF.Length);
                }
                else if (iPos == 0)
                {
                    return null;
                }

                iPos = sMatch.IndexOf(StringUtil.CRLF);
                List<string> al = new List<string>();
                while (iPos >= 0)
                {
                    if (iPos == 0)
                    {
                        sMatch = sMatch.Substring(2, sMatch.Length - 2);
                        iPos = sMatch.IndexOf(StringUtil.CRLF);
                        continue;
                    }

                    string sKey = sMatch.Substring(0, iPos);

                    string sKeyName = sKey.Substring(0, sKey.IndexOf("="));

                    al.Add(sKeyName);

                    sMatch = sMatch.Substring(iPos + 2, sMatch.Length - (iPos + 2));

                    iPos = sMatch.IndexOf(StringUtil.CRLF);
                }

                string[] sKeyNames = (string[])al.ToArray();

                return sKeyNames;
            }

            return null;
        }

        /// <summary>
        /// 指定したキーに該当する値を取得する
        /// </summary>
        /// <param name="sectionName">セクション名</param>
        /// <param name="keyName">キー名</param>
        /// <returns></returns>
        public string GetKeyValue(string sectionName, string keyName)
        {
            string sSectionString = this.GetSectionString(sectionName);

            string regexPattern = @"(" + keyName + @"=(?<value>.*)\r\n)";

            Regex r = new Regex(regexPattern);

            if (r.IsMatch(sSectionString))
            {
                string sValue = r.Match(sSectionString).Result("${value}");

                int iPos = sValue.IndexOf(";");
                if (iPos > 0)
                {
                    return sValue.Substring(0, iPos);
                }
                else if (iPos == 0)
                {
                    return string.Empty;
                }

                return sValue;
            }

            return string.Empty;
        }

        /// <summary>
        /// 指定したキーに該当する値を設定する
        /// </summary>
        /// <param name="sectionName">セクション名</param>
        /// <param name="keyName">キー名</param>
        /// <param name="keyValue">キー値</param>
        public void WriteKeyValue(string sectionName, string keyName, string keyValue)
        {
            string sSectionString = this.GetSectionString(sectionName);

            string regexPattern = @"(" + keyName + @"=(?<value>.*)\r\n)";

            Regex r = new Regex(regexPattern);

            if (r.IsMatch(sSectionString))
            {
                string sNewContents = m_fileContent.Replace(r.Match(sSectionString).Result("${value}"), keyValue);

                StreamWriter sw = new StreamWriter(m_fileName, false, Encoding.Default);

                sw.Write(sNewContents);

                sw.Close();

                sw = null;
            }
        }
        #endregion

        #region ■私有方法________________________________________________________
        /// <summary>
        /// 指定したセクションに該当する文字列を取得する
        /// </summary>
        /// <param name="sSectionName"></param>
        /// <returns></returns>
        private string GetSectionString(string sSectionName)
        {
            string sRegexPattern = @"\[" + sSectionName + @"\]" + @"(?<Section>.*)";

            Regex r = new Regex(sRegexPattern, RegexOptions.Singleline);

            if (r.IsMatch(m_fileContent))
            {
                string sMatch = r.Match(m_fileContent).Result("${Section}");

                int iPos = sMatch.IndexOf(StringUtil.CRLF + "[");
                if (iPos > 0)
                {
                    sMatch = sMatch.Substring(0, iPos + StringUtil.CRLF.Length);
                }
                else if (iPos == 0)
                {
                    return null;
                }

                return sMatch;
            }

            return null;
        }
        #endregion

        #region ■静态方法________________________________________________________
        /// <summary>
        /// 指定したセクションに該当する文字列を取得する
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sSectionName"></param>
        /// <returns></returns>
        internal static string GetSectionString(string fileName, string sSectionName)
        {
            string FileContents = IniFileUtil.GetFileContent(fileName);

            string sRegexPattern = @"\[" + sSectionName + @"\]" + @"(?<Section>.*)";
            Regex r = new Regex(sRegexPattern, RegexOptions.Singleline);

            if (r.IsMatch(FileContents))
            {
                string sMatch = r.Match(FileContents).Result("${Section}");

                int iPos = sMatch.IndexOf(StringUtil.CRLF + "[");
                if (iPos > 0)
                {
                    sMatch = sMatch.Substring(0, iPos + StringUtil.CRLF.Length);
                }
                else if (iPos == 0)
                {
                    return null;
                }

                return sMatch;
            }

            return null;
        }

        /// <summary>
        /// 指定したキーに該当する値を取得する
        /// </summary>
        /// <param name="sectionName">セクション名</param>
        /// <param name="keyName">キー名</param>
        /// <returns></returns>
        public static string GetKeyValue(string fileName, string sectionName, string keyName)
        {
            string sSectionString = IniFileUtil.GetSectionString(fileName, sectionName);

            string regexPattern = @"(" + keyName + @"=(?<value>.*)\r\n)";

            Regex r = new Regex(regexPattern);

            if (r.IsMatch(sSectionString))
            {
                string sValue = r.Match(sSectionString).Result("${value}");

                int iPos = sValue.IndexOf(";");
                if (iPos > 0)
                {
                    return sValue.Substring(0, iPos);
                }
                else if (iPos == 0)
                {
                    return string.Empty;
                }

                return sValue;
            }

            return string.Empty;
        }

        /// <summary>
        /// 指定したキーに該当する値を設定する
        /// </summary>
        /// <param name="sectionName">セクション名</param>
        /// <param name="keyName">キー名</param>
        /// <param name="keyValue">キー値</param>
        public static void WriteKeyValue(string fileName, string sectionName, string keyName, string keyValue)
        {
            string sSectionString = IniFileUtil.GetSectionString(fileName, sectionName);
            string strFileContent = IniFileUtil.GetFileContent(fileName);

            string regexPattern = @"(" + keyName + @"=(?<value>.*)\r\n)";

            Regex r = new Regex(regexPattern);

            if (r.IsMatch(sSectionString))
            {
                string sNewContents = strFileContent.Replace(r.Match(sSectionString).Result("${value}"), keyValue);

                StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default);
                sw.Write(sNewContents);
                sw.Close();

                sw = null;
            }
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        internal static string GetFileContent(string fileName)
        {
            string strFileContents = string.Empty;
            if (File.Exists(fileName))
            {
                StreamReader sr = new StreamReader(fileName, Encoding.Default);
                strFileContents = sr.ReadToEnd();
                sr.Close();
            }
            else
            {
                throw new Exception(FILE_NOT_EXSIT);
            }

            return strFileContents;
        }
        #endregion

        #region ■INIファイル処理________________________________________________

        #region ■API____________________________________________________________

        [DllImport("kernel32")]
        public extern static int GetPrivateProfileStringA(
            string segName,
            string keyName,
            string sDefault,
            byte[] buffer,
            int iLen,
            string fileName);


        /// <summary>
        /// ini ファイルの読み込み用の関数(GetPrivateProfileString)
        /// </summary>
        /// <param name="lpAppName">セクション名</param>
        /// <param name="lpKeyName">ｷｰ名</param>
        /// <param name="lpDefault">ｷｰが見つからなかった場合のﾃﾞﾌｫﾙﾄの文字列</param>
        /// <param name="lpReturnedString">取得した文字列</param>
        /// <param name="nSize">取得した文字列数</param>
        /// <param name="lpFileName">ＩＮＩファイル名</param>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
        string lpAppName,
        string lpKeyName,
        string lpDefault,
        StringBuilder lpReturnedString,
        int nSize,
        string lpFileName
        );

        /// <summary>
        /// ini ファイルの書き込み用の関数(WritePrivateProfileString)
        /// </summary>
        /// <param name="lpAppName">セクション名</param>
        /// <param name="lpKeyName">ｷｰ名</param>
        /// <param name="lpString">書きこむ文字列</param>
        /// <param name="lpFileName">ＩＮＩファイル名</param>	
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(
        string lpAppName,
        string lpKeyName,
        string lpString,
        string lpFileName
        );

        /// <summary>
        /// ini ファイルの読み込み用の関数 byte取得版 (GetPrivateProfileStringByteArray)
        /// </summary>
        /// <param name="lpAppName">セクション名</param>
        /// <param name="lpKeyName">ｷｰ名</param>
        /// <param name="lpDefault">ｷｰが見つからなかった場合のﾃﾞﾌｫﾙﾄの文字列</param>
        /// <param name="lpReturnedString">取得したbyte配列</param>
        /// <param name="nSize">取得した文字列数</param>
        /// <param name="inifilename">ＩＮＩファイル名</param>
        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileString")]
        public static extern uint GetPrivateProfileStringByByteArray(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            byte[] lpReturnedString,
            uint nSize,
            string inifilename);

        #endregion

        #region ■処理関数_______________________________________________________

        /// <summary>
        /// ini ファイルの読み込み
        /// </summary>
        /// <param name="i_InEntryName">エントリー名</param>
        /// <param name="i_InEntryString">エントリー文字列</param>
        /// <param name="i_FilePath">ファイルパス</param> 
        /// <returns>取得値</returns>
        public static string ReadIniF(string i_InEntryName,
                                    string i_InEntryString,
                                    string i_FilePath,
                                    string i_DefautValue)
        {
            if (File.Exists(i_FilePath) == false)
            {
                return i_DefautValue;
            }

            int entryLength;
            string strEntryStringValue;

            // 読み込むためのバッファ
            StringBuilder strEntryString = new StringBuilder(4096);

            entryLength = GetPrivateProfileString(i_InEntryName,
                                                i_InEntryString,
                                                i_DefautValue,
                                                strEntryString,
                                                4096,
                                                i_FilePath);

            strEntryStringValue = strEntryString.ToString();
            return strEntryStringValue;

        }

        /// <summary>
        /// ini ファイルの書き込み
        /// </summary>
        /// <param name="i_InEntryName">エントリー名</param>
        /// <param name="i_InEntryString">エントリー文字列</param>
        /// <param name="i_Value">書きこむ文字列</param>
        /// <param name="i_FilePath">ＩＮＩファイル名</param>
        public static void WriteIniF(string i_InEntryName,
                                    string i_InEntryString,
                                    string i_Value,
                                    string i_FilePath)
        {
            WritePrivateProfileString(i_InEntryName, i_InEntryString, i_Value, i_FilePath);
        }


        /// <summary>
        /// ini ファイル中の指定セクションのキー数を取得
        /// </summary>
        /// <param name="i_EntryName">エントリー名</param>
        /// <returns>キー数</returns>
        public static int ReadIniSecKeyCnt(string i_EntryName, string i_FilePath)
        {
            if (File.Exists(i_FilePath) == false)
            {
                return 0;
            }

            //セクション中の文字列取得用byte配列
            byte[] bytarr = new byte[4096];

            //セクション中の全文字列を取得
            uint resultSize = GetPrivateProfileStringByByteArray(
                i_EntryName, null, "", bytarr,
                (uint)bytarr.Length, i_FilePath);
            if (resultSize <= 0)
            {
                return 0;
            }
            //byte配列を文字列に変換
            string result = System.Text.Encoding.Default.GetString(bytarr, 0, (int)resultSize - 1);

            //キー毎に分解
            string[] keys = result.Split('\0');

            //1セクション中のキー数を取得
            int keyCnt = keys.Length;

            return keyCnt;
        }

        /// <summary>
        /// ini ファイル中の指定セクションのキーを取得
        /// </summary>
        /// <param name="i_EntryName">エントリー名</param>
        /// <returns>キーの配列</returns>
        public static string[] ReadIniSecKey(string i_EntryName, string i_FilePath)
        {
            if (File.Exists(i_FilePath) == false)
            {
                return new string[] { };
            }
  
            //セクション中の文字列取得用byte配列
            byte[] bytarr = new byte[4096];

            //セクション中の全文字列を取得
            uint resultSize = GetPrivateProfileStringByByteArray(
                i_EntryName, null, "", bytarr,
                (uint)bytarr.Length, i_FilePath);
            if (resultSize <= 0)
            {
                return new string[] { };
            }
            //byte配列を文字列に変換
            string result = System.Text.Encoding.Default.GetString(bytarr, 0, (int)resultSize - 1);

            //キー毎に分解
            string[] keys = result.Split('\0');

            return keys;
        }

        /// <summary>
        /// ini ファイル中の指定セクションのキーの値を取得
        /// </summary>
        /// <param name="i_EntryName">エントリー名</param>
        /// <returns>キーの配列</returns>
        public static Dictionary<string, string> ReadIniSecAllValue(string i_EntryName, string i_FilePath)
        {
            //Add by 2010.10.12 Start
            if (File.Exists(i_FilePath) == false)
            {
                return new Dictionary<string, string>();
            }
            //Add by 2010.10.12 End

            Dictionary<string, string> dicResult = new Dictionary<string, string>();

            string[] keys = ReadIniSecKey(i_EntryName, i_FilePath);

            foreach (string key in keys)
            {
                //セクション中の文字列取得用byte配列
                byte[] bytarr = new byte[4096];

                //セクション中のキーの全文字列を取得
                uint resultSize = GetPrivateProfileStringByByteArray(
                    i_EntryName, key, "", bytarr,
                    (uint)bytarr.Length, i_FilePath);
                if (resultSize < 0)
                {
                    continue;
                }
                //byte配列を文字列に変換
                string result = System.Text.Encoding.Default.GetString(bytarr, 0, (int)resultSize);

                dicResult[key] = result;
            }

            return dicResult;
        }

        #endregion
        #endregion
    }

}
