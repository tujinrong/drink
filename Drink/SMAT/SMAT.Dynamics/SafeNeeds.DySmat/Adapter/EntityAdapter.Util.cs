using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Reflection;

using SafeNeeds.DySmat.DB.Util;
using SafeNeeds.DySmat.Util;
//using SafeNeeds.SMAT.Core.Define;
using SafeNeeds.DySmat.DB;

using SafeNeeds.DySmat.Model;
using SafeNeeds.DySmat.DB.Exception;

namespace SafeNeeds.DySmat
{
    /// <summary>
    /// 単一のテーブルの処理を管理するクラス
    /// </summary>
    public partial class EntityAdapter
    {
        public void AddModelDataSet(Type modelType, ICollection data, ref DataSet ds)
        {
            bool isField = false;
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            dt.TableName = modelType.Name;

            FieldInfo[] fs = modelType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            //for field
            foreach (FieldInfo p in fs)
            {
                if (p.IsPublic　&& p.IsStatic==false)
                {
                    Type ptp = p.FieldType;
                    Type tp = GetDatasetType(ptp);

                    DataColumn dc = new DataColumn(p.Name, tp);
                    dt.Columns.Add(dc);
                    isField = true;
                }
            }

            PropertyInfo[] ps =null;
            //プロパティ対応
            if (isField==false)
            {
                ps = modelType.GetProperties();
                foreach (PropertyInfo p in ps)
                {
                        Type ptp = p.PropertyType;
                        Type tp = GetDatasetType(ptp);

                        DataColumn dc = new DataColumn(p.Name, tp);
                        dt.Columns.Add(dc);
                }
            }

            foreach (object o in data)
            {
                DataRow dr = dt.NewRow();

                foreach (FieldInfo p in fs)
                {
                    if (p.IsPublic && p.IsStatic == false)
                    {
                        object v = p.GetValue(o);
                        if (v != null)
                            dr[p.Name] = v;
                    }
                }

                //プロパティ対応
                if (isField == false)
                {
                    foreach (PropertyInfo p in ps)
                    {
                        object v = p.GetValue(o);
                        if (v != null)
                            dr[p.Name] = v;
                    }
                }

                dt.Rows.Add(dr);
            }

            dt.AcceptChanges();
        }

        private Type GetDatasetType(Type ptp)
        {
            Type tp;

            if (ptp == typeof(string)) tp = typeof(string);
            else if (ptp == typeof(int?)) tp = typeof(int);
            else if (ptp == typeof(short?)) tp = typeof(int);
            else if (ptp == typeof(byte?)) tp = typeof(int);
            else if (ptp == typeof(decimal?)) tp = typeof(decimal);
            else if (ptp == typeof(DateTime?)) tp = typeof(DateTime);
            else if (ptp == typeof(int)) tp = typeof(int);
            else if (ptp == typeof(short)) tp = typeof(int);
            else if (ptp == typeof(byte)) tp = typeof(int);
            else if (ptp == typeof(decimal)) tp = typeof(decimal);
            else if (ptp == typeof(bool)) tp = typeof(bool);
            else if (ptp == typeof(DateTime)) tp = typeof(DateTime);
            else throw new ApplicationException("type error");

            return tp;
        }

        /// <summary>
        /// DataRowの状態を変更します。
        /// </summary>
        /// <param name="dtOld">変更前のDataTable</param>
        /// <param name="dtNew">変更後のDataTable</param>
        /// <param name="keyNames"></param>
        private DataTable GetDiffDT(DataTable dtOld, DataTable dtNew, string[] keyNames, bool updateSystemItem)
        {
            //Local時間、暫定
            DateTime nowTime = DateTime.Now;

            int iInsertProgram = _Entity.FieldList.FindIndex(e => e.SystemField == Y_EntityField.EnumSystemField.InsertProgram);
            int iInsertTime = _Entity.FieldList.FindIndex(e => e.SystemField == Y_EntityField.EnumSystemField.InsertTime);
            int iInsertUser = _Entity.FieldList.FindIndex(e => e.SystemField == Y_EntityField.EnumSystemField.InsertUser);
            int iUpdateProgram = _Entity.FieldList.FindIndex(e => e.SystemField == Y_EntityField.EnumSystemField.UpdateProgram);
            int iUpdateTime = _Entity.FieldList.FindIndex(e => e.SystemField == Y_EntityField.EnumSystemField.UpdateTime);
            int iUpdateUser = _Entity.FieldList.FindIndex(e => e.SystemField == Y_EntityField.EnumSystemField.UpdateUser);
            int iInsertUpdateProgram = _Entity.FieldList.FindIndex(e => e.SystemField == Y_EntityField.EnumSystemField.InsertUpdateProgram);
            int iInsertUpdateTime = _Entity.FieldList.FindIndex(e => e.SystemField == Y_EntityField.EnumSystemField.InsertUpdateTime);
            int iInsertUpdateUser = _Entity.FieldList.FindIndex(e => e.SystemField == Y_EntityField.EnumSystemField.InsertUpdateUser);

            Dictionary<string, DataRow> dicOld = new Dictionary<string, DataRow>();
            Dictionary<string, DataRow> dicNew = new Dictionary<string, DataRow>();

            //旧DataSetの辞書を作成
            foreach (DataRow dr in dtOld.Rows)
            {
                dicOld.Add(ToKeyString(dr, keyNames), dr);
            }

            //新Datasetの辞書を作成
            foreach (DataRow dr in dtNew.Rows)
            {
                dicNew.Add(ToKeyString(dr, keyNames), dr);
            }

            foreach (string key in dicNew.Keys)
            {

                if (!dicOld.ContainsKey(key))
                {
                    //Ｏｒｇに存在しない場合、追加する
                    //dtOld.Rows.Add(dicNew[key].ItemArray);
                    
                    DataRow dr = dtOld.NewRow();

                    for (int i = 0; i < dtNew.Columns.Count; i++)
                    {
                        var columnName = dtNew.Columns[i].ColumnName;
                        dr[columnName] = dicNew[key].ItemArray[i];
                    }

                    if (iInsertProgram >= 0)
                    {
                        if (updateSystemItem)
                        {
                            dr[iInsertProgram] = _entityRequest.Program;
                        }
                        else if (DataUtil.CStr(dr[iInsertProgram]) == string.Empty)
                        {
                            dr[iInsertProgram] = _entityRequest.Program;
                        }
                    }
                    if (iInsertTime >= 0)
                    {
                        if (updateSystemItem)
                        {
                            dr[iInsertTime] = nowTime;
                        }
                        else
                        {
                            string str = DataUtil.CStr(dr[iInsertTime]);
                            if (str == string.Empty || str == "0001/01/01 0:00:00")
                            {
                                dr[iInsertTime] = nowTime;
                            }
                        }
                    }
                    if (iInsertUser >= 0)
                    {
                        if (updateSystemItem)
                        {
                            dr[iInsertUser] = _entityRequest.User;
                        }
                        else if (DataUtil.CStr(dr[iInsertUser]) == string.Empty)
                        {
                            dr[iInsertUser] = _entityRequest.User;
                        }
                    }
                    if (iInsertUpdateProgram >= 0)
                    {
                        if (updateSystemItem)
                        {
                            dr[iInsertUpdateProgram] = _entityRequest.Program;
                        }
                        else if (DataUtil.CStr(dr[iInsertUpdateProgram]) == string.Empty)
                        {
                            dr[iInsertUpdateProgram] = _entityRequest.Program;
                        }
                    }
                    if (iInsertUpdateTime >= 0)
                    {
                        if (updateSystemItem)
                        {
                            dr[iInsertUpdateTime] = nowTime;
                        }
                        else
                        {
                            string str = DataUtil.CStr(dr[iInsertUpdateTime]);
                            if (str == string.Empty || str == "0001/01/01 0:00:00")
                            {
                                dr[iInsertUpdateTime] = nowTime;
                            }
                        }
                    }
                    if (iInsertUpdateUser >= 0)
                    {
                        if (updateSystemItem)
                        {
                            dr[iInsertUpdateUser] = _entityRequest.User;
                        }
                        else if (DataUtil.CStr(dr[iInsertUpdateUser]) == string.Empty)
                        {
                            dr[iInsertUpdateUser] = _entityRequest.User;
                        }
                    }
                    dtOld.Rows.Add(dr);
                }
                else
                {
                    //存在する場合、Ｏｒｇを更新にする
                    //int irow = (int)OrgKeyNoDic[sKeyValue];
                    DataRow rowOrg = dicOld[key];
                    DataRow rowDes = dicNew[key];

                    //
                    //if (IsDataRowEquals(rowOrg, rowDes, iUpdateUser, iUpdateTime))
                    if (IsDataRowEqualsByColName(rowOrg, rowDes))    
                    {
                        //内容が一致する場合、更新しない
                        rowOrg.AcceptChanges();
                    }
                    else
                    {
                        //一致しない場合、新DataSet内容をセット
                        if (rowOrg.RowState == DataRowState.Added)
                        {
                            rowOrg.AcceptChanges();
                        }
                 
                        rowOrg.BeginEdit();
                        for (int i = 0; i < dtNew.Columns.Count; i++)
                        {
                            //2016/04/19 Haita
                            var columnName = dtNew.Columns[i].ColumnName;
                            var field = _Entity.FieldList.Where(e => e.FieldName == columnName);
                            if (field.Count() == 0) {
                                continue;
                            }

                            if (field.First().SystemField == Y_EntityField.EnumSystemField.UpdateTime)
                            {
                                if (!rowOrg[columnName].Equals(rowDes[columnName]))
                                {
                                    throw new HaitaException("");
                                }
                            }
                            //2016/04/19 Haita

                            if (field.First().SystemField == Y_EntityField.EnumSystemField.None)
                            {
                                rowOrg[columnName] = rowDes[columnName];
                            }
                        }

                        if (iUpdateProgram >= 0)
                        {
                            if (updateSystemItem)
                            {
                                rowOrg[iUpdateProgram] = _entityRequest.Program;
                            }
                            else
                            {
                                if (DataUtil.CStr(rowDes[iUpdateProgram]) == string.Empty)
                                {
                                    rowOrg[iUpdateProgram] = _entityRequest.Program;
                                }
                                else
                                {
                                    rowOrg[iUpdateProgram] = rowDes[iUpdateProgram];
                                }
                            }
                        }
                        if (iUpdateTime >= 0)
                        {
                            if (updateSystemItem)
                            {
                                rowOrg[iUpdateTime] = nowTime;
                            }
                            else
                            {
                                string str = DataUtil.CStr(rowDes[iUpdateTime]);
                                if (str == string.Empty || str == "0001/01/01 0:00:00")
                                {
                                    rowOrg[iUpdateTime] = nowTime;
                                }
                                else
                                {
                                    rowOrg[iUpdateTime] = rowDes[iUpdateTime];
                                }
                            }
                        }
                        if (iUpdateUser >= 0)
                        {
                            if (updateSystemItem)
                            {
                                rowOrg[iUpdateUser] = _entityRequest.User;
                            }
                            else
                            {
                                if (DataUtil.CStr(rowDes[iUpdateUser]) == string.Empty)
                                {
                                    rowOrg[iUpdateUser] = _entityRequest.User;
                                }
                                else
                                {
                                    rowOrg[iUpdateUser] = rowDes[iUpdateUser];
                                }
                            }
                        }
                        if (iInsertUpdateProgram >= 0)
                        {
                            if (updateSystemItem)
                            {
                                rowOrg[iInsertUpdateProgram] = _entityRequest.Program;
                            }
                            else
                            {
                                if (DataUtil.CStr(rowDes[iInsertUpdateProgram]) == string.Empty)
                                {
                                    rowOrg[iInsertUpdateProgram] = _entityRequest.Program;
                                }
                                else
                                {
                                    rowOrg[iInsertUpdateProgram] = rowDes[iInsertUpdateProgram];
                                }
                            }
                        }
                        if (iInsertUpdateTime >= 0)
                        {
                            if (updateSystemItem)
                            {
                                rowOrg[iInsertUpdateTime] = nowTime;
                            }
                            else
                            {
                                string str = DataUtil.CStr(rowDes[iInsertUpdateTime]);
                                if (str == string.Empty || str == "0001/01/01 0:00:00")
                                {
                                    rowOrg[iInsertUpdateTime] = nowTime;
                                }
                                else
                                {
                                    rowOrg[iInsertUpdateTime] = rowDes[iInsertUpdateTime];
                                }
                            }
                        }
                        if (iInsertUpdateUser >= 0)
                        {
                            if (updateSystemItem)
                            {
                                rowOrg[iInsertUpdateUser] = _entityRequest.User;
                            }
                            else
                            {
                                if (DataUtil.CStr(rowDes[iInsertUpdateUser]) == string.Empty)
                                {
                                    rowOrg[iInsertUpdateUser] = _entityRequest.User;
                                }
                                else
                                {
                                    rowOrg[iInsertUpdateUser] = rowDes[iInsertUpdateUser];
                                }
                            }
                        }
                        rowOrg.EndEdit();
                    }

                }
            }

            //旧DataSetを遍歴
            foreach (string key in dicOld.Keys)
            {
                if (!dicNew.ContainsKey(key))
                {
                    //新DataSetに存在しない場合、削除
                    dicOld[key].Delete();
                }
            }

            return dtOld.GetChanges();

        }
        private bool IsDataRowEqualsxxxxxx(DataRow dr1, DataRow dr2, int iUpdateUser, int iUpdateTime)
        {
            for (int i = 0; i < dr1.ItemArray.Length; i++)
            {
                //更新時間、更新者を比較しない
                if (i == iUpdateTime || i == iUpdateUser) continue;

                object o1 = dr1[i];
                object o2 = dr2[i];
                if (o1.GetType() != o2.GetType()) return false;
                if (o1.ToString() != o2.ToString()) return false;
            }
            return true;
        }

        private bool IsDataRowEqualsByColName(DataRow dr1, DataRow dr2)
        {
            for (int i = 0; i < dr2.ItemArray.Length; i++)
            {
                string colName = dr2.Table.Columns[i].ColumnName;

                //更新時間、更新者を比較しない
                //夏 2016.09.01
                if (colName == _Proj.UpdateUserItem || colName == _Proj.UpdateTimeItem
                        || dr1.Table.Columns.Contains(colName) == false)
                    continue;

                //2016.02.22
                if (DataUtil.CStr(dr1[colName]) != DataUtil.CStr(dr2[colName])) return false;

                //if (o1.GetType() != o2.GetType()) return false;
                //if (o1.ToString() != o2.ToString()) return false;
            }
            return true;
        }

        private int GetUpdateUserItemIndex(DataTable dt)
        {

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (_Entity.FieldList[i].SystemField ==  Y_EntityField.EnumSystemField.UpdateUser)
                    return i;
            }
            return -1;
        }

        private int GetUpdateTimeItemIndex(DataTable dt)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (_Entity.FieldList[i].SystemField == Y_EntityField.EnumSystemField.UpdateTime)
                    return i;
            }
            return -1;
        }



        //private int GetUpdateUserItemIndexx()
        //{
        //    string userItem = _Proj.UpdateUserItem;
        //    if (userItem == string.Empty) return -1;

        //    Y_EntityField f = _Entity.FieldList.Find(e => e.FieldName == userItem);
        //    if (f == null) return -1;
        //    return _Entity.FieldList.IndexOf(f);
        //}

        //private int GetUpdateTimeItemIndexx()
        //{
        //    string Item = _Proj.UpdateTimeItem;
        //    if (Item == string.Empty) return -1;

        //    Y_EntityField f = _Entity.FieldList.Find(e => e.FieldName == Item);
        //    if (f == null) return -1;
        //    return _Entity.FieldList.IndexOf(f);
        //}

        private  string ToKeyString(DataRow dr, string[] keyNames)
        {
            string sKeyValue = string.Empty;
            for (int i = 0; i < keyNames.Length; i++)
            {
                if (i > 0)
                {
                    sKeyValue += "|";
                }

                Y_EntityField field = _Entity.FieldList.Find(e => e.FieldName == keyNames[i]);

                if (field.IsIdentity && Convert.IsDBNull(dr[keyNames[i]]))
                {
                    sKeyValue += ("IdentityNewKey" + dr.Table.Rows.IndexOf(dr));
                    continue;
                }

                sKeyValue += dr[keyNames[i]].ToString();
            }
            return sKeyValue;

        }




        /// <summary>
        /// SQL文を取得する
        /// </summary>
        /// <param name="strSelect">SQL文</param>
        /// <param name="strOrderBy">ソート文</param>
        /// <param name="strFilter">フィルター</param>
        /// <param name="rowFrom">開始行番号</param>
        /// <param name="rowTo">終了行番号</param>
        /// <param name="isDistinct">Distinctフラグ</param>
        /// <returns>SQL文</returns>
        private string GetSelectSQL(string strSelect, string strFilter, string strOrderBy, int rowFrom, int rowTo, bool isDistinct)
        {
            //マルチDB対応
            SqlConstructor sql = new SqlConstructor(this.Config.Database);

            sql.Distinct = isDistinct;
            sql.RowNoFrom = rowFrom;
            sql.RowNoTo = rowTo;
            sql.Select = strSelect;
            sql.Where = strFilter;
            sql.OrderBy = strOrderBy;
            sql.MainTable = _Entity.EntityName;

            return sql.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        private string GetSQLFromKey(object[] keyValues)
        {
            //FieldDefs flds = _TableDefine.Fields;
            return this.GetSQLFromItem(GetKeys(), keyValues);
        }
        
        private string GetSQLFromItem(string[] f, object[] v)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < v.Length; i++)
            {

                string s = f[i] ;
                object o = v[i];

                if (s.StartsWith("'"))
                {
                    continue;
                }

                if (o == null)
                {
                    s += " IS NULL";
                }
                else
                {
                    string sv = DataUtil.SQLConvert(o.ToString());
                    Y_EntityField field = _Entity.FieldList.Find(e => e.FieldName == s);
                    switch (field.GetCommonDataType())
                    {
                        case EnumDbDataType.STRING:
                        case EnumDbDataType.UNICODE:
                        case EnumDbDataType.LONGTEXT:
                            s += "=N'" + sv + "'";
                            break;

                        case EnumDbDataType.DATE:
                            if (base.Config.Database== EnumDatabaseType.ORACLE)
                            {
                                if (sv.IndexOf(':')>0)
                                {
                                    s += "=to_date('" + sv + "', 'YYYY/MM/DD HH24:MI:SS')";
                                }
                                else
                                {
                                    s += "='" + sv + "'";
                                }

                            }
                            else if (base.Config.Database== EnumDatabaseType.SQLSERVER)
                            {
                                s += "='" + sv + "'";
                            }
                            else
                            {
                                throw new ApplicationException("Type not surported");
                            }
                            break;


                        default:
                            s += "=" + sv;
                            break;
                    }
                }
                list.Add(s);
            }
            return string.Join(" AND " , list.ToArray());
        }


        private string GetSQLFromItemNoEQ(string[] f, object[] v)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < v.Length; i++)
            {

                string s = f[i];
                object o = v[i];

                if (s.StartsWith("'"))
                {
                    continue;
                }

                if (o == null)
                {
                    s += " IS NULL";
                }
                else
                {
                    string sv = DataUtil.SQLConvert(o.ToString());
                    Y_EntityField field = _Entity.FieldList.Find(e => e.FieldName == s);
                    switch (field.GetCommonDataType())
                    {
                        case EnumDbDataType.STRING:
                        case EnumDbDataType.UNICODE:
                        case EnumDbDataType.LONGTEXT:
                        case EnumDbDataType.DATE:
                            s += "!=N'" + sv + "'";
                            break;

                        default:
                            s += "!=" + sv;
                            break;
                    }
                }
                list.Add(s);
            }
            return string.Join(" OR ", list.ToArray());
        }

        public string[] GetKeys()
        {
            return _Entity.FieldList.Where(e => e.IsKey == true).OrderBy(e=>e.Seq).Select(e=>e.FieldName).ToArray(); 
        }
        /// <summary>
        /// キー項目の取得
        /// </summary>
        /// <returns>キー項目</returns>
        public List<Y_EntityField> GetKeyFields()
        {
            return _Entity.FieldList.Where(e => e.IsKey == true).OrderBy(e => e.Seq).ToList();
        }


        /// <summary>
        /// キー項目の取得
        /// </summary>
        /// <returns>キー項目</returns>
        private List<Y_EntityField> GetNotKeyFields()
        {
            return _Entity.FieldList.Where(e => e.IsKey == false).OrderBy(e => e.Seq).ToList();
        }

        /// <summary>
        /// データセットからリストに変更する
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <returns></returns>
        private List<object> DsToList(DataSet ds)
        {
            //ArrayList al = new ArrayList();
            List<object> al = new List<object>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                al.Add(row.ItemArray);
            }
            return al;
        }
        

        /// <summary>
        /// テーブルのデータセットからキーのデータを取り出す。
        /// </summary>
        /// <param name="ds">テーブルのデータセット</param>
        /// <returns>キーデータの配列</returns>
        private List<object> GetKeyArray(DataSet ds)
        {
            string[] alkeyName = GetKeys();
            List<object> al = new List<object>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                List<object> d = new List<object>();
                foreach (string n in alkeyName)
                {
                    d.Add(row[n]);
                }
                al.Add(d.ToArray());
            }
            return al;
        }

        /// <summary>
        /// サーバ時間を取得する
        /// /summary>
        /// <returns></returns>
        public DateTime GetSysDateTime()
        {
            if (_Proj.UseDatabaseTime == false) return DateTime.Now;

            string sql = string.Empty;

            if (base.Config.Database == EnumDatabaseType.ACCESS)
            {
                sql = "select now";
            }
            else if (base.Config.Database == EnumDatabaseType.ORACLE)
            {
                sql = "select SysDate from dual";
            }
            else if (base.Config.Database == EnumDatabaseType.SQLSERVER)
            {
                sql = "select GETDATE()";
            }

            try
            {
                DataSet ds = base.GetDataSetBySQL(sql, "dummy");
                return Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray[0]);
            }
            catch (System.Exception ex)
            {
                //時間を取得失敗する場合、接続エラーを投げる
                throw new ApplicationException("time");
            }
        }
        private static DateTime _LastTime = new DateTime(1900, 1, 1);
        private static TimeSpan _TimeDiff;
        
        public  DateTime GetSystemTime(DBConfig config, bool fromDB)
        {
            DateTime dateTime = DateTime.Now;

            if (dateTime.Subtract(_LastTime).Ticks > (long)300 * 1000 * 10000 || fromDB)
            {
                
                DateTime time =GetSysDateTime();

                dateTime = DateTime.Now;
                _TimeDiff = time.Subtract(dateTime);
                _LastTime = dateTime;

                dateTime = time;
            }
            else
            {
                dateTime = dateTime.AddTicks(_TimeDiff.Ticks);
            }

            return dateTime;
        }
    }
}
