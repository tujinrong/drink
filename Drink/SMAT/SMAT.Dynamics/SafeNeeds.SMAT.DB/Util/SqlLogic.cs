using System;
using System.Collections.Generic;

namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlLogic
    {
        private EnumDatabaseType _DbType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbType">データベースタイプ</param>
        public SqlLogic(EnumDatabaseType dbType)
        {
            _DbType = dbType;
        }

        /// <summary>
        /// SQL文のNullチェック
        /// </summary>
        /// <param name="itemName">項目名</param>
        /// <param name="itemThen">Nullの時に返す値</param>
        /// <returns>Nullの時はitemThen、それ以外の時はitemNameを返す</returns>
        public string Nvl(string itemName, string itemThen)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "IIF(" + itemName + " is null ," + itemThen + "," + itemName + ")";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "ISNULL(" + itemName + "," + itemThen + ")";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "NVL(" + itemName + "," + itemThen + ")";
            }

            return sRet;
        }

        /// <summary>
        /// 項目の評価
        /// </summary>
        /// <param name="itemName">項目名</param>
        /// <param name="itemCase">判定式</param>
        /// <param name="itemThen">判定された結果が真のときに返される値</param>
        /// <param name="itemElse">判定された結果が偽のときに返される値</param>
        /// <returns>判定結果によって選択された値</returns>
        public string IIF(string itemName, string itemCase, string itemThen, string itemElse)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                if (itemCase.ToUpper() == "NULL")
                {
                    sRet = "iif(" + itemName + " is " + itemCase + "," + itemThen + "," + itemElse + ")";
                }
                else if (itemCase.ToUpper() == "" && (itemName.IndexOf(">") > 0 || itemName.IndexOf("<") > 0))
                {
                    sRet = "iif(" + itemName + "," + itemThen + "," + itemElse + ")";
                }
                else if (itemCase.IndexOf(">") == 0 || itemCase.IndexOf("<") == 0)
                {
                    sRet = "iif(" + itemName + itemCase + "," + itemThen + "," + itemElse + ")";
                }
                else
                {
                    sRet = "iif(" + itemName + "=" + itemCase + "," + itemThen + "," + itemElse + ")";
                }
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "CASE " + itemName + " WHEN " + itemCase + " THEN " + itemThen + " ELSE " + itemElse + " END";
            }
            else if (_DbType == DB.EnumDatabaseType.ORACLE)
            {
                if (itemCase == "" && (itemName.IndexOf(">") > 0 || itemName.IndexOf("<") > 0))
                {
                    string op = string.Empty;
                    string s1 = string.Empty;
                    string s2 = string.Empty;
                    if (itemName.IndexOf(">=") > 0)
                    {
                        op = ">=";
                    }
                    else if (itemName.IndexOf("<=") > 0)
                    {
                        op = "<=";
                    }
                    else if (itemName.IndexOf(">") > 0)
                    {
                        op = ">";
                    }
                    else if (itemName.IndexOf("<") > 0)
                    {
                        op = "<";
                    }
                    s1 = itemName.Substring(0, itemName.IndexOf(op));
                    s2 = itemName.Substring(itemName.IndexOf(op) + op.Length);
                    if (op == ">=")
                    {
                        sRet = "DECODE(sign(" + s1 + "-" + s2 + "),1," + itemThen + ",0," + itemThen + "," + itemElse + ")";
                    }
                    else if (op == "<=")
                    {
                        sRet = "DECODE(sign(" + s1 + "-" + s2 + "),-1," + itemThen + ",0," + itemThen + "," + itemElse + ")";
                    }
                    else if (op == ">")
                    {
                        sRet = "DECODE(sign(" + s1 + "-" + s2 + "),1," + itemThen + "," + itemElse + ")";
                    }
                    else if (op == "<")
                    {
                        sRet = "DECODE(sign(" + s1 + "-" + s2 + "),-1," + itemThen + "," + itemElse + ")";
                    }
                }
                else if (itemCase.IndexOf(">") == 0 || itemCase.IndexOf("<") == 0)
                {
                    string op = string.Empty;
                    string s1 = string.Empty;
                    string s2 = string.Empty;
                    if (itemCase.IndexOf(">=") == 0)
                    {
                        op = ">=";
                    }
                    else if (itemCase.IndexOf("<=") == 0)
                    {
                        op = "<=";
                    }
                    else if (itemCase.IndexOf(">") == 0)
                    {
                        op = ">";
                    }
                    else if (itemCase.IndexOf("<") == 0)
                    {
                        op = "<";
                    }
                    s1 = itemName;
                    s2 = itemCase.Substring(op.Length).Trim();
                    if (op == ">=")
                    {
                        sRet = "DECODE(sign(" + s1 + "-" + s2 + "),1," + itemThen + ",0," + itemThen + "," + itemElse + ")";
                    }
                    else if (op == "<=")
                    {
                        sRet = "DECODE(sign(" + s1 + "-" + s2 + "),-1," + itemThen + ",0," + itemThen + "," + itemElse + ")";
                    }
                    else if (op == ">")
                    {
                        sRet = "DECODE(sign(" + s1 + "-" + s2 + "),1," + itemThen + "," + itemElse + ")";
                    }
                    else if (op == "<")
                    {
                        sRet = "DECODE(sign(" + s1 + "-" + s2 + "),-1," + itemThen + "," + itemElse + ")";
                    }
                }
                else
                {
                    sRet = "DECODE( " + itemName + "," + itemCase + "," + itemThen + "," + itemElse + ")";
                }
            }

            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="caseThen"></param>
        /// <param name="caseElse"></param>
        /// <returns></returns>
        public string SwitchCase(string itemName, List<string> caseThen, string caseElse)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "Switch(";
                foreach (string str in caseThen)
                {
                    if (sRet != "Switch(")
                    {
                        sRet += ",";
                    }
                    sRet += itemName + "=" + str[0];
                    sRet += "," + str[1];
                }
                if (caseElse != "")
                {
                    sRet += caseElse;
                }
                sRet += ")";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "CASE " + itemName;
                foreach (string str in caseThen)
                {
                    sRet += " WHEN " + str[0];
                    sRet += " THEN " + str[1];
                }
                if (caseElse != "")
                {
                    sRet += " ELSE " + caseElse;
                }
                sRet += " END";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "DECODE( " + itemName;
                foreach (string str in caseThen)
                {
                    sRet += "," + str[0];
                    sRet += " ," + str[1];
                }
                if (caseElse != "")
                {
                    sRet += "," + caseElse;
                }
                sRet += ")";
            }

            return sRet;
        }
    }
}
