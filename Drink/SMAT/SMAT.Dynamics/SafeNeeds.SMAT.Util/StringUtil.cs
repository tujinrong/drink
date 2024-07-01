//*****************************************************************************
// ユーティリティのライブラリ
//
// 概要　　：　文字列関連のユーティリティクラス
//               
// 作成　　：屠
//            2006/05/12 
//
//*****************************************************************************
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SafeNeeds.DySmat.Util
{
    /// <summary>
    /// 文字列関連のユーティリティクラス
    /// </summary>
    public class StringUtil
    {
        /// <summary>
        /// 改行コード
        /// </summary>
        public const string CRLF = "\r\n";

        /// <summary>
        /// 
        /// </summary>
        public const string CR = "\r";

        /// <summary>
        /// 
        /// </summary>
        public const string LF = "\n";

        /// <summary>
        /// 
        /// </summary>
        public const string QUOT = "\"";

        /// <summary>
        /// 
        /// </summary>
        public const string SLASHR = "\\";

        /// <summary>
        /// スラシュー
        /// </summary>
        public const string SLASHL = "/";

        /// <summary>
        /// スペースコード
        /// </summary>
        public const string SPACE = " ";

        /// <summary>
        /// ハイフンコード
        /// </summary>
        public const string HYPHEN = "-";

        /// <summary>
        /// 
        /// </summary>
        public const string COMMA = ",";

        /// <summary>
        /// 
        /// </summary>
        public const string UNDERBAR = "_";

        /// <summary>
        /// セミコロンコード
        /// </summary>
        public const string SEMICOLON = ";";	// セミコロン

        #region 　文字列の変換メソッド

        /// <summary>
        /// 全角文字列を半角にすべで変換する
        /// </summary>
        /// <param name="value">変換する前の文字列</param>
        /// <returns></returns>
        public static string ToNarrow(string value)
        {
            return null;
            //return Strings.StrConv(value, VbStrConv.Narrow, 0);
        }

        /// <summary>
        /// 半角文字列を全角にすべで変換する
        /// </summary>
        /// <param name="value">変換する前の文字列</param>
        /// <returns></returns>
        public static string ToWide(string value)
        {
            return null;
            //return Strings.StrConv(value, VbStrConv.Wide, 0);
        }

        /// <summary>
        /// 文字列中の全角英字を半角英字に変換する
        /// </summary>
        /// <param name="value">変換する前の文字列</param>
        /// <returns></returns>
        public static string ToNarrowNL(string value)
        {
            char[] cs = value.ToCharArray();

            for (int i = 0; i <= cs.Length - 1; i++)
            {
                if (IsWideNumber(cs[i]) || IsWideLetter(cs[i]))
                {
                    cs[i] = ToNarrow(cs[i].ToString()).ToCharArray()[0];
                }
            }

            string sRet = string.Empty;
            foreach (char c in cs)
            {
                sRet += c.ToString();
            }

            return sRet;
        }

        /// <summary>
        /// 文字列中の半角英字を全角英字に変換する
        /// </summary>
        /// <param name="value">変換する前の文字列</param>
        /// <returns></returns>
        public static string ToWideNL(string value)
        {
            char[] cs = value.ToCharArray();

            string sRet = string.Empty;
            for (int i = 0; i <= cs.Length - 1; i++)
            {
                if (IsNarrowNumber(cs[i]) || IsNarrowLetter(cs[i]))
                {
                    sRet += ToWide(cs[i].ToString());
                }
                else
                {
                    sRet += cs[i].ToString();
                }
            }

            return sRet;
        }

        /// <summary>
        /// 文字列中の全角カタカナを半角カタカナに変換する
        /// </summary>
        /// <param name="value">変換する前の文字列</param>
        /// <returns></returns>
        public static string ToNarrowKatakana(string value)
        {
            char[] cs = value.ToCharArray();

            string sRet = string.Empty;

            for (int i = 0; i <= cs.Length - 1; i++)
            {
                if (IsWideKataKana(cs[i]))
                {
                    sRet += ToNarrow(cs[i].ToString());
                }
                else
                {
                    sRet += cs[i].ToString();
                }
            }

            return sRet;
        }

        /// <summary>
        /// 文字列中の半角カタカナを全角カタカナに変換する
        /// </summary>
        /// <param name="value">変換する前の文字列</param>
        /// <returns></returns>
        public static string ToWideKatakana(string value)
        {
            char[] cs = value.ToCharArray();

            string sRet = string.Empty;
            for (int i = 0; i <= cs.Length - 1; i++)
            {
                if (IsNarrowKatakana(cs[i]))
                {
                    if ((i != cs.Length - 1) && (IsNarrowKatakana(cs[i + 1])) && (cs[i + 1] == 'ﾟ' || cs[i + 1] == 'ﾞ'))
                    {
                        sRet += ToWide(cs[i].ToString() + cs[i + 1].ToString());

                        i++;
                    }
                    else
                    {
                        sRet += ToWide(cs[i].ToString());
                    }
                }
                else
                {
                    sRet += cs[i].ToString();
                }
            }

            return sRet;
        }

        /// <summary>
        /// 文字列中のひらがなをカタカナに変換する
        /// </summary>
        /// <param name="value">変換する前の文字列</param>
        /// <returns></returns>
        public static string ToKatakana(string value)
        {
            return null;
          //  return Strings.StrConv(value, VbStrConv.Katakana, 0);
        }

        /// <summary>
        /// 文字列中のカタカナをひらがなに変換する
        /// </summary>
        /// <param name="value">変換する前の文字列</param>
        /// <returns></returns>
        public static string ToHiragana(string value)
        {
            return null;
           // return Strings.StrConv(StringUtil.ToWideKatakana(value), VbStrConv.Hiragana, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSeachKatakana(string value)
        {
            // string s;
            if (value == null) return "";
            value = value.ToUpper();
            value = ToKatakana(value);
            value = ToWide(value);

            //value = ToWideKatakana(value);
            value = value.Replace("ヵ", "カ").Replace("ッ", "ツ").Replace("ァ", "ア").Replace("ャ", "ヤ").Replace("ゥ", "ウ").Replace("ェ", "エ").Replace("ュ", "ユ").Replace("ョ", "ヨ").Replace("ヶ", "ケ");

            return value;
        }

        #endregion

        #region 　文字列関連の判定メソッド

        /// <summary>
        /// 日付の判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsDate(string value)
        {
            value = StringUtil.ToNarrow(value);

            // 日付の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$");

            if (!r.IsMatch(value))
            {
                r = new Regex(@"^\d{2}(\-|\/|\.)\d{1,2}\1\d{1,2}$");
                if (!r.IsMatch(value))
                {
                    r = new Regex(@"^\d{1,2}(\-|\/|\.)\d{1,2}$");
                    if (!r.IsMatch(value))
                    {
                        return false;
                    }
                }
            }

            string[] sYMD = Regex.Split(value, @"\-|\/|\.");

            if (sYMD.Length > 3 || sYMD.Length < 2)
            {
                return false;
            }

            int iYear = 0;
            int iMon = 0;
            int iDay = 0;

            if (sYMD.Length == 3)
            {
                iYear = int.Parse(sYMD[0]);
                iMon = int.Parse(sYMD[1]);
                iDay = int.Parse(sYMD[2]);
            }

            if (sYMD.Length == 2)
            {
                iYear = DateTime.Now.Year;
                iMon = int.Parse(sYMD[0]);
                iDay = int.Parse(sYMD[1]);
            }

            if (iMon > 12 || iMon < 1)
            {
                return false;
            }

            if (iDay > 31 || iDay < 1)
            {
                return false;
            }

            int[] iDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if (iDay > iDays[iMon - 1])
            {
                if (iMon != 2)
                {
                    return false;
                }
                else
                {
                    bool bLeapYear = (iYear % 4 == 0 && (iYear % 100 != 0 || iYear % 400 == 0));

                    if (!bLeapYear)
                    {
                        return false;
                    }
                    else if (iDay > 29)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 年月の判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsMonth(string value)
        {
            value = StringUtil.ToNarrow(value);

            // 年月の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^\d{4}(\-|\/|\.)\d{1,2}$");

            if (!r.IsMatch(value))
            {
                r = new Regex(@"^\d{2}(\-|\/|\.)\d{1,2}$");
                if (!r.IsMatch(value))
                {
                    return false;
                }
            }

            string[] sYM = Regex.Split(value, @"\-|\/|\.");

            if (sYM.Length < 2)
            {
                return false;
            }

            int iYear = 0;
            int iMon = 0;

            if (sYM.Length == 2)
            {
                iYear = int.Parse(sYM[0]);
                iMon = int.Parse(sYM[1]);
            }

            if (iMon > 12 || iMon < 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 時刻の判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsTime(string value)
        {
            List<string> al = null;

            value = StringUtil.ToNarrow(value);

            if (value.IndexOf(":") < 0)
            {
                if (!IsNarrowNumber(value))
                {
                    return false;
                }

                if (value.Length != 4 && value.Length != 6)
                {
                    return false;
                }

                al = new List<string>();

                if (value.Length == 4)
                {
                    al.Add(value.Substring(0, 2));
                    al.Add(value.Substring(2, 2));
                }

                if (value.Length == 6)
                {
                    al.Add(value.Substring(0, 2));
                    al.Add(value.Substring(2, 2));
                    al.Add(value.Substring(4, 2));
                }
            }
            else
            {
                // 時刻の正規表現パターンを指定してRegexクラスのインスタンスを作成
                Regex r = new Regex(@"^\d{1,2}(\:)\d{1,2}\1\d{1,2}$");

                if (!r.IsMatch(value))
                {
                    r = new Regex(@"^\d{1,2}(\:)\d{1,2}$");
                    if (!r.IsMatch(value))
                    {
                        return false;
                    }
                }
            }

            string[] sHHmm;

            if (al != null)
            {
                sHHmm = al.ToArray();
            }
            else
            {
                sHHmm = Regex.Split(value, @"\:");
            }
            //string[] sHHmm = Regex.Split(value, @"\:");

            if (sHHmm.Length > 3 || sHHmm.Length < 2)
            {
                return false;
            }

            int iHour = 0;
            int iMin = 0;
            int iSec = 0;

            if (sHHmm.Length == 3)
            {
                iHour = int.Parse(sHHmm[0]);
                iMin = int.Parse(sHHmm[1]);
                iSec = int.Parse(sHHmm[2]);
            }

            if (sHHmm.Length == 2)
            {
                iHour = int.Parse(sHHmm[0]);
                iMin = int.Parse(sHHmm[1]);
            }

            if (iHour < 0 || iHour > 23)
            {
                return false;
            }

            if (iMin < 0 || iMin > 59)
            {
                return false;
            }

            if (iSec < 0 || iSec > 59)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 半角数値のか判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsNumeric(char value)
        {
            string sValue = value.ToString();

            // 半角数字の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[0-9]+$");

            return r.IsMatch(sValue);
        }

        /// <summary>
        /// 半角数字のか判定（小数有り、符号有り）
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            return IsNumeric(value, false, false);
        }

        /// <summary>
        /// 半角数値のか判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <param name="sign">符号有無（True:有り、False:無し）</param>
        /// <param name="dec">小数点有無（True:有り、False:無し）</param>
        /// <returns></returns>
        public static bool IsNumeric(string value, bool sign, bool dec)
        {
            Regex r;

            //-----------------------------------------------------------------
            // 半角数字の正規表現パターンを指定してRegexクラスのインスタンスを作成
            //-----------------------------------------------------------------
            if (sign && !dec)
            {
                // 整数のみ、符号有り
                r = new Regex(@"^(\+|\-)?([1-9]{1}[\d]*|0{1}){1}$");
            }
            else if (!sign && dec)
            {
                // 小数有り、符号無し
                r = new Regex(@"^([1-9]{1}[\d]*|0{1}){1}([\.]{1}[\d]+)?$");
            }
            else if (sign && dec)
            {
                // 小数有り、符号有り
                r = new Regex(@"^(\+|\-)?([1-9]{1}[\d]*|0{1}){1}([\.]{1}[\d]+)?$");
            }
            else
            {
                // 整数のみ、符号無し
                r = new Regex(@"^[0-9]+$");
            }

            return r.IsMatch(value);
        }

        /// <summary>
        /// 全角のみの判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsWide(string value)
        {
            Encoding jisEnc = Encoding.GetEncoding("Shift_JIS");

            bool ret = true;

            if (jisEnc.GetByteCount(value) != value.Length * 2)
            {
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// 半角のみの判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsNarrow(string value)
        {
            Encoding jisEnc = Encoding.GetEncoding("Shift_JIS");

            bool ret = true;

            if (jisEnc.GetByteCount(value) != value.Length)
            {
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// 半角英数の判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsNarrowNL(string value)
        {
            // 半角英数の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[A-Za-z0-9]+$");

            return r.IsMatch(value);
        }

        /// <summary>
        /// 全角英数の判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsWideNL(string value)
        {
            // 全角英数の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[Ａ-Ｚａ-ｚ０-９]+$");

            return r.IsMatch(value);
        }

        /// <summary>
        /// 半角小文の判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsLower(char value)
        {
            string sValue = value.ToString();

            // 半角小文字の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[a-z]+$");

            return r.IsMatch(sValue);
        }

        /// <summary>
        /// 半角小文の判定(全文字列の判定)
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsLower(string value)
        {
            bool bRet = true;

            foreach (char c in value)
            {
                if (IsLower(c) == false)
                {
                    bRet = false;
                    break;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 半角大文字の判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsUpper(char value)
        {
            string sValue = value.ToString();

            // 半角大文字の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[A-Z]+$");

            return r.IsMatch(sValue);
        }

        /// <summary>
        /// 半角大文字の判定(全文字列の判定)
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsUpper(string value)
        {
            bool bRet = true;

            foreach (char c in value)
            {
                if (IsUpper(c) == false)
                {
                    bRet = false;
                    break;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 半角記号の判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsSymbol(char value)
        {
            string sValue = value.ToString();
            bool bRet = false;

            // 半角記号の正規表現パターンを指定してRegexクラスのインスタンスを作成（ACSII以内）
            Regex r = new Regex(@"^[!-/:-@\[-`{-~]+$");

            if (r.IsMatch(sValue))
            {
                bRet = true;
            }

            // 半角記号の正規表現パターンを指定してRegexクラスのインスタンスを作成（ACSII以外）
            r = new Regex(@"^[｡-･]+$");

            if (r.IsMatch(sValue))
            {
                bRet = true;
            }

            return bRet;
        }

        /// <summary>
        /// 半角記号の判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsSymbol(string value)
        {
            bool bRet = false;

            foreach (char c in value)
            {
                if (IsSymbol(c) == true)
                {
                    bRet = true;
                    break;
                }
            }

            return bRet;
        }

        /// <summary>
        /// ひらがなの判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsHiragana(char value)
        {
            string sValue = value.ToString();

            // ひらがなの正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[あ-を]+$");

            return r.IsMatch(sValue);
        }

        /// <summary>
        /// 漢字とひらがなの判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsKanjiHiragana(char value)
        {
            System.Text.Encoding en = Encoding.GetEncoding("shift-jis");

            char[] chs = new char[] { value };

            byte[] byt = en.GetBytes(chs);

            if (byt.Length == 1)
            {
                return false;
            }

            bool bRet = false;

            if ((0x80 < byt[0] && byt[0] < 0xA0) || (0xDF < byt[0] && byt[0] < 0xF0))
            {
                if ((0x3F < byt[1] && byt[1] < 0x7F) || (0x7F < byt[1] && byt[1] < 0xFD))
                {
                    if (!IsWideKataKana(value))
                    {
                        bRet = true;
                    }
                }
            }

            return bRet;
        }

        /// <summary>
        /// シフト ＪＩＳの判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsShiftJIS(char value)
        {
            Encoding sjis = Encoding.GetEncoding("shift-jis");

            char[] chs = new char[] { value };

            byte[] bUcode = Encoding.Unicode.GetBytes(chs);

            byte[] bSjis = Encoding.Convert(Encoding.Unicode, sjis, bUcode);

            if (!sjis.GetString(bSjis).Equals(value.ToString()))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 全角カタカナの判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsWideKataKana(char value)
        {
            string sValue = value.ToString();

            // 全角カタカナの正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[ア-ヲァ-ォ]+$");

            return r.IsMatch(sValue);
        }

        /// <summary>
        /// 全角カタカナの判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsWideKataKana(string value)
        {
            bool bRet = true;

            foreach (char c in value)
            {
                if (IsWideKataKana(c) == false)
                {
                    bRet = false;
                    break;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 半角カタカナの判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsNarrowKatakana(char value)
        {
            string sValue = value.ToString();

            // 半角カタカナの正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[ｦ-ﾟ]+$");

            return r.IsMatch(sValue);
        }

        /// <summary>
        /// 半角カタカナの判定(全文字列の判定)
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsNarrowKatakana(string value)
        {
            bool bRet = true;

            foreach (char c in value)
            {
                if (IsNarrowKatakana(c) == false)
                {
                    bRet = false;
                    break;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 全角数字の判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsWideNumber(char value)
        {
            string sValue = value.ToString();

            // 全角数字の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[０-９]+$");

            return r.IsMatch(sValue);
        }

        /// <summary>
        /// 全角数字の判定(全文字列の判定)
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsWideNumber(string value)
        {
            bool bRet = true;

            foreach (char c in value)
            {
                if (IsWideNumber(c) == false)
                {
                    bRet = false;
                    break;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 半角数字のか判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsNarrowNumber(char value)
        {
            string sValue = value.ToString();

            // 半角数字の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[0-9]+$");

            return r.IsMatch(sValue);
        }

        /// <summary>
        /// 半角数字のか判定(全文字列の判定)
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsNarrowNumber(string value)
        {
            bool bRet = true;

            foreach (char c in value)
            {
                if (IsNarrowNumber(c) == false)
                {
                    bRet = false;
                    break;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 全角英字の判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsWideLetter(char value)
        {
            string sValue = value.ToString();

            // 全角英字の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[Ａ-Ｚａ-ｚ]+$");

            return r.IsMatch(sValue);
        }

        /// <summary>
        /// 全角英字の判定(全文字列の判定)
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsWideLetter(string value)
        {
            bool bRet = true;

            foreach (char c in value)
            {
                if (IsWideLetter(c) == false)
                {
                    bRet = false;
                    break;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 半角英字の判定
        /// </summary>
        /// <param name="value">文字</param>
        /// <returns></returns>
        public static bool IsNarrowLetter(char value)
        {
            string sValue = value.ToString();

            // 半角英字の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^[A-Za-z]+$");

            return r.IsMatch(sValue);
        }

        /// <summary>
        /// 半角英字の判定(全文字列の判定)
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsNarrowLetter(string value)
        {
            bool bRet = true;

            foreach (char c in value)
            {
                if (IsNarrowLetter(c) == false)
                {
                    bRet = false;
                    break;
                }
            }

            return bRet;
        }

        /// <summary>
        /// Eメールのフォーマットの判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsEmailString(string value)
        {
            // Eメールの正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"\b[-\w.]+@[-\w.]+\.[-\w]+\b");

            return r.IsMatch(value);
        }

        /// <summary>
        /// 電話番号のフォーマットの判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsPhone(string value)
        {
            // 電話番号の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"^(\+\d+ )?(\(\d+\) )?[\d ]+$");

            return r.IsMatch(value);
        }

        /// <summary>
        /// 郵便番号ののフォーマットの判定
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsPostCode(string value)
        {
            // 郵便番号の正規表現パターンを指定してRegexクラスのインスタンスを作成
            Regex r = new Regex(@"\d{3}-\d{4}");

            return r.IsMatch(value);
        }

        /// <summary>
        /// IPアドレスのチェック
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns></returns>
        public static bool IsIpAddress(string value)
        {
            string[] address = value.Split('.');

            if (address.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < 4; i++)
            {
                if (!IsNumeric(address[i], false, false))
                {
                    return false;
                }

                if (System.Int16.Parse(address[i]) > 255 || System.Int16.Parse(address[i]) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string JoinString(object[] obj, string delimiter)
        {
            List<string> al = new List<string>();

            for (int i = 0; i < obj.Length; i++)
            {
                al.Add(DataUtil.CStr(obj[i]));
            }

            string[] str = (string[])al.ToArray();

            return string.Join(delimiter, str);
        }

        /// <summary>
        ///     半角 1 バイト、全角 2 バイトとして、指定された文字列のバイト数を返します。</summary>
        /// <param name="stTarget">
        ///     バイト数取得の対象となる文字列。</param>
        /// <returns>
        ///     半角 1 バイト、全角 2 バイトでカウントされたバイト数。</returns>
        public static int LenB(string stTarget)
        {
            return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(stTarget);
        }

        /// <summary>
        ///     文字列の左端から指定したバイト数分の文字列を返します。
        /// </summary>
        /// <param name="stTarget">
        ///     取り出す元になる文字列。
        /// <param>
        /// <param name="iByteSize">
        ///     取り出すバイト数。
        /// </param>
        /// <returns>
        ///     左端から指定されたバイト数分の文字列。
        /// </returns>
        public static string LeftB(string stTarget, int iByteSize)
        {
            return LeftB(stTarget, iByteSize, " ");
        }

        /// <summary>
        ///     文字列の左端から指定したバイト数分の文字列を返します。
        /// </summary>
        /// <param name="stTarget">
        ///     取り出す元になる文字列。
        /// <param>
        /// <param name="iByteSize">
        ///     取り出すバイト数。
        /// </param>
        /// <returns>
        ///     左端から指定されたバイト数分の文字列。
        /// </returns>
        public static string LeftB(string stTarget, int iByteSize, string space)
        {

            System.Text.Encoding sjis = System.Text.Encoding.GetEncoding("Shift_JIS");
            int TempLen = sjis.GetByteCount(stTarget);
            if (((iByteSize < 1)
                        || (stTarget.Length < 1)))
            {
                return "";
            }
            if ((TempLen <= iByteSize))
            {

                return stTarget;
            }
            byte[] tempByt = sjis.GetBytes(stTarget);
            string strTemp = sjis.GetString(tempByt, 0, iByteSize);
            // 末尾が漢字分断されたら半角スペースと置き換え(VB2005="・" で.NET2003=NullChar になります）   
            if (strTemp.EndsWith("・"))
            {
                strTemp = sjis.GetString(tempByt, 0, (iByteSize - 1)) + space;
            }

            return strTemp;
        }

        /// <summary>
        ///     文字列の左端から指定したバイト数分の文字列を返します。
        /// </summary>
        /// <param name="stTarget">
        ///     取り出す元になる文字列。
        /// <param>
        /// <param name="iByteSize">
        ///     取り出すバイト数。
        /// </param>
        /// <returns>
        ///     左端から指定されたバイト数分の文字列。
        /// </returns>
        public static string LeftB(string stTarget, int iStart, int iByteSize, string space)
        {

            System.Text.Encoding sjis = System.Text.Encoding.GetEncoding("Shift_JIS");
            int TempLen = sjis.GetByteCount(stTarget);
            if (((iByteSize < 1)
                        || (stTarget.Length < 1)))
            {
                return "";
            }
            if ((TempLen <= iByteSize))
            {

                return stTarget;
            }
            byte[] tempByt = sjis.GetBytes(stTarget);
            string strTemp = sjis.GetString(tempByt, iStart, iByteSize);
            // 末尾が漢字分断されたら半角スペースと置き換え(VB2005="・" で.NET2003=NullChar になります）   
            if (strTemp.EndsWith("・"))
            {
                strTemp = sjis.GetString(tempByt, iStart, (iByteSize - 1)) + space;
            }

            return strTemp;
        }

        public static string[] GetByteArray(string stTarget, int iByteSize)
        {
            System.Text.Encoding sjis = System.Text.Encoding.GetEncoding("Shift_JIS");
            int TempLen = sjis.GetByteCount(stTarget);
            if (((iByteSize < 1)
                        || (stTarget.Length < 1)))
            {
                return new string[] { "" };
            }
            if ((TempLen <= iByteSize))
            {

                return new string[] { stTarget };
            }

            List<string> list = new List<string>();
            string text = stTarget;
            int count = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((double)TempLen / iByteSize)));
            for (int i = 0; i < count; i++)
            {
                // text = RightB(stTarget, TempLen - i * iByteSize);
                string subtext = LeftB(text, iByteSize, "");
                text = text.Substring(text.IndexOf(subtext) + 1);
                list.Add(subtext);
            }

            return list.ToArray();
        }

        /// <summary>
        ///     文字列の右端から指定したバイト数分の文字列を返します。
        /// </summary>
        /// <param name="stTarget">
        ///     取り出す元になる文字列。
        /// <param>
        /// <param name="iByteSize">
        ///     取り出すバイト数。
        /// </param>
        /// <returns>
        ///     右端から指定されたバイト数分の文字列。
        /// </returns>
        public static string RightB(string stTarget, int iByteSize)
        {
            return RightB(stTarget, iByteSize, "");
        }

        /// <summary>
        ///     文字列の右端から指定したバイト数分の文字列を返します。
        /// </summary>
        /// <param name="stTarget">
        ///     取り出す元になる文字列。
        /// <param>
        /// <param name="iByteSize">
        ///     取り出すバイト数。
        /// </param>
        /// <returns>
        ///     右端から指定されたバイト数分の文字列。
        /// </returns>
        public static string RightB(string stTarget, int iByteSize, string space)
        {

            System.Text.Encoding sjis = System.Text.Encoding.GetEncoding("Shift_JIS");
            int TempLen = sjis.GetByteCount(stTarget);
            if (((iByteSize < 1)
                        || (stTarget.Length < 1)))
            {
                return "";
            }
            if ((TempLen <= iByteSize))
            {

                return stTarget;
            }
            byte[] tempByt = sjis.GetBytes(stTarget);
            string strTemp = sjis.GetString(tempByt, tempByt.Length - iByteSize, iByteSize);
            // 末尾が漢字分断されたら半角スペースと置き換え(VB2005="・" で.NET2003=NullChar になります）   
            if (strTemp.StartsWith("・") || strTemp.StartsWith("") || strTemp.StartsWith("`"))
            {
                strTemp = sjis.GetString(tempByt, tempByt.Length - iByteSize - 1, iByteSize) + space;
            }
            if (strTemp.EndsWith("・") | strTemp.EndsWith("") || strTemp.EndsWith("`"))
            {
                strTemp = sjis.GetString(tempByt, tempByt.Length - iByteSize, (iByteSize - 1)) + space;
            }
            return strTemp;
        }

        /// <summary>
        ///     文字列の指定されたバイト位置以降のすべての文字列を返します。
        /// </summary>
        /// <param name="stTarget">
        ///     取り出す元になる文字列。
        /// </param>
        /// <param name="iStart">
        ///     取り出しを開始する位置。
        /// </param>
        /// <returns>
        ///     指定されたバイト位置以降のすべての文字列。
        /// </returns>
        public static string MidB(string stTarget, int iStart)
        {
            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] btBytes = hEncoding.GetBytes(stTarget);

            string strTemp = hEncoding.GetString(btBytes, 0, iStart - 1);
            if (strTemp.EndsWith("・"))
            {
                iStart = iStart - 1;
            }

            strTemp = hEncoding.GetString(btBytes, iStart - 1, btBytes.Length - iStart + 1);

            if (strTemp.EndsWith("・"))
            {
                strTemp = hEncoding.GetString(btBytes, iStart - 1, btBytes.Length - iStart) + " ";
            }
            return strTemp;
        }

        /// <summary>
        ///     文字列の指定されたバイト位置から、指定されたバイト数分の文字列を返します。
        /// </summary>
        /// <param name="stTarget">
        ///     取り出す元になる文字列。
        /// </param>
        /// <param name="iStart">
        ///     取り出しを開始する位置。
        /// </param>
        /// <param name="iByteSize">
        ///     取り出すバイト数。
        /// </param>
        /// <returns>
        ///     指定されたバイト位置から指定されたバイト数分の文字列。
        /// </returns>
        public static string MidB(string stTarget, int iStart, int iByteSize)
        {
            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] btBytes = hEncoding.GetBytes(stTarget);

            string strTemp = hEncoding.GetString(btBytes, 0, iStart - 1);
            if (strTemp.EndsWith("・"))
            {
                iStart = iStart - 1;
            }


            if (btBytes.Length <= iByteSize)
            {
                strTemp = hEncoding.GetString(btBytes, iStart - 1, btBytes.Length);

                if (strTemp.EndsWith("・"))
                {
                    strTemp = hEncoding.GetString(btBytes, iStart - 1, btBytes.Length - 1) + " ";
                }
            }
            else
            {
                strTemp = hEncoding.GetString(btBytes, iStart - 1, iByteSize);

                if (strTemp.EndsWith("・"))
                {
                    strTemp = hEncoding.GetString(btBytes, iStart - 1, iByteSize - 1) + " ";
                }

            }

            return strTemp;

        }

        /// <summary>
        /// MD5暗号化
        /// </summary>
        /// <param name="encryptStr">暗号化の文字列</param>
        /// <returns>暗号化したの文字列</returns>
        public static string Md5Encrypt(string encryptStr)
        {
            string str = string.Empty;
            MD5 md5 = MD5.Create();
            byte[] encryptBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(encryptStr));
            for (int i = 0; i < encryptBytes.Length; i++)
            {
                str += encryptBytes[i].ToString("x2");
            }
            return str;
        }
    }
}
