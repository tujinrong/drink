using System;

using System.Collections.Generic;

using SafeNeeds.DySmat.DB;

using SafeNeeds.DySmat.Model;

namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// SQL文の生成クラス
    /// </summary>
    public class SqlConstructor
    {
        private EnumDatabaseType _DbType;
       // private JoinCollection _TableAndJoins = new JoinCollection();

        public string MainTable;
        public List<Y_EntityRelaN1> RelaList;

        private int _RowNoFrom = 0;
        private int _RowNoTo = 0;
        private bool _Distinct = false;
        private string _Select = string.Empty;
        private string _Where = string.Empty;
        private string _OrderBy = string.Empty;
        private string _GroupBy = string.Empty;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbType">データベースタイプ</param>
        public SqlConstructor(EnumDatabaseType dbType)
        {
            _DbType = dbType;
        }

        #region " プロパティ "

        /// <summary>
        /// 開始行番号
        /// </summary>
        public int RowNoFrom
        {
            get
            {
                return _RowNoFrom;
            }
            set
            {
                _RowNoFrom = value;
            }
        }

        /// <summary>
        /// 終了行番号
        /// </summary>
        public int RowNoTo
        {
            get
            {
                return _RowNoTo;
            }
            set
            {
                _RowNoTo = value;
            }
        }

        /// <summary>
        /// Distinctオプション
        /// </summary>
        public bool Distinct
        {
            get
            {
                return _Distinct;
            }
            set
            {
                _Distinct = value;
            }
        }

        /// <summary>
        /// 抽出項目
        /// </summary>
        public string Select
        {
            get
            {
                return _Select;
            }
            set
            {
                _Select = value;
            }
        }

        /// <summary>
        /// Group By文
        /// </summary>
        public string GroupBy
        {
            get
            {
                return _GroupBy;
            }
            set
            {
                _GroupBy = value;
            }
        }

        /// <summary>
        /// 条件文(Where)
        /// </summary>
        public string Where
        {
            get
            {
                return _Where;
            }
            set
            {
                _Where = value;
            }
        }

        /// <summary>
        /// Order By文
        /// </summary>
        public string OrderBy
        {
            get
            {
                return _OrderBy;
            }
            set
            {
                _OrderBy = value;
            }
        }


        #endregion
        
        #region " メソッド "

        /// <summary>
        /// SQL文を生成する
        /// </summary>
        /// <returns>SQL文</returns>
        public override string ToString()
        {
            const int NON_ROWNO = 0;  // rownumなし
            const int ORA_ROWNO = 1;  // oracleのrownum
            const int SRV_ROWNO = 2;　// sql server 2005のrownum

            SqlBuilder sql = new SqlBuilder(_DbType);
            int RowNoType = NON_ROWNO;
            bool hasSubQuery = false;

            if (_RowNoTo > 0)
            {
                if (_DbType == EnumDatabaseType.ACCESS || _DbType == EnumDatabaseType.SQLSERVER)
                {
                    RowNoType = SRV_ROWNO;
                }
                else if (_DbType == EnumDatabaseType.ORACLE)
                {
                    RowNoType = ORA_ROWNO;
                }
            }

            //-----------------------------------------------------------------
            // SELECT分
            //-----------------------------------------------------------------
            sql.Append("SELECT ");

            // rownumある場合
            if (RowNoType == SRV_ROWNO)
            {
                if (_RowNoFrom == 0)
                {
                    sql.Append("TOP " + _RowNoTo + " ");
                }
                else if (DBOption.SqlServer2005)
                {
                    sql.Append("ROW_NUMBER() OVER(ORDER BY " + _OrderBy + ") AS RowNo,");
                }

                hasSubQuery = true;
            }
            else if (RowNoType == ORA_ROWNO)
            {
                //sql.Append("ROWNUM RowNo,");

                hasSubQuery = true;
            }

            // distinctオプションの場合
            if (_Distinct && ((RowNoType == NON_ROWNO) || (RowNoType == ORA_ROWNO)))
            {
                sql.Append(" DISTINCT ");
            }

            sql.Append(_Select);

            //-----------------------------------------------------------------
            // FROM分
            //-----------------------------------------------------------------

            if (_DbType == EnumDatabaseType.ORACLE)
            {
                sql.Append(DBOption.SqlCrLf + "\nFROM " + MainTable);// + this.TableAndJoins.ToOracleString());
            }
            else
            {
                sql.Append(DBOption.SqlCrLf + "\nFROM " + MainTable); //+ this.TableAndJoins.ToSQLSERVERString());
            }

            //-----------------------------------------------------------------
            // WHERE分
            //-----------------------------------------------------------------
            string sWhere = string.Empty;
            if (_DbType == EnumDatabaseType.ORACLE)
            {
                sWhere = "";// this.TableAndJoins.GetOracleWhere();
                if (_Where != string.Empty)
                {
                    sWhere += _Where;
                }
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER || _DbType == EnumDatabaseType.ACCESS)
            {
                sWhere = _Where;
            }
            
            if (sWhere != string.Empty)
            {
                sql.Append(DBOption.SqlCrLf + "\nWHERE " + sWhere);
            }

            //-----------------------------------------------------------------
            // GROUP BY分
            //-----------------------------------------------------------------
            if (this.GroupBy != "")
            {
                sql.Append(DBOption.SqlCrLf + "\nGROUP BY " + _GroupBy);
            }

            //-----------------------------------------------------------------
            // ORDER BY分
            //-----------------------------------------------------------------
            if (this.OrderBy != string.Empty)
            {
                sql.Append(DBOption.SqlCrLf + "\nORDER BY " + _OrderBy);
            }

            string sqlText = string.Empty;
            if (hasSubQuery)
            {
                sqlText = "SELECT ";
                //if (_Distinct)
                //{
                //    sqlText = "DISTINCT ";
                //}
                
                sqlText += _Select;
                if (RowNoType == ORA_ROWNO)
                {
                    sqlText += " FROM (SELECT ROWNUM RowNo,";
                    sqlText += _Select;
                    sqlText += " FROM (";
                }else
                {
                    sqlText += " FROM (";
                }
                sqlText += sql.ToString();
                if (RowNoType == ORA_ROWNO)
                {
                    sqlText += "))";
                }
                else
                {
                    sqlText += ")";
                }
                sqlText += " WHERE RowNo >= ";
                sqlText += _RowNoFrom;
                sqlText += " AND RowNo <= ";
                sqlText += _RowNoTo;
            }
            else
            {
                sqlText = sql.ToString();
            }

            return sqlText;
        }

        #endregion
    }
}
