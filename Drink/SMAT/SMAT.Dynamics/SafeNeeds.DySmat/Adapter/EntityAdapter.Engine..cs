using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Transactions;

using SafeNeeds.DySmat.DB.Util;
using SafeNeeds.DySmat.Util;

using SafeNeeds.DySmat.DB;

using SafeNeeds.DySmat.Model;

namespace SafeNeeds.DySmat
{
    /// <summary>
    /// 単一のテーブルの処理を管理するクラス
    /// </summary>
    public partial class EntityAdapter
    {
        private const int MAX_OR = 50;

        private string _UpdateUser;
        private bool _UseUpdateControlTable;

        public string CreateTimeFieldName;
        public string UpdateTimeFieldName;
        public string UpdateUserFieldName;


        /// <summary>
        /// 更新コントロールテーブルを取得または設定する
        /// </summary>
        public bool UseUpdateControlTable
        {
            get
            {
                return _UseUpdateControlTable;
            }
            set
            {
                _UseUpdateControlTable = value;
            }
        }

        /// <summary>
        /// 更新ユーザの設定
        /// </summary>
        /// <param name="user">ユーザＩＤ</param>
        private void SetUpdateUser(string user)
        {
            _UpdateUser = user;
        }


        private Result SaveData_Binding(TableSaveRequest req, DataSet ds)
        {
            string[] names = _Entity.Rela1NList.Where(e => e.IsSubTable).Select(e => e.RelaEntityName).ToArray();
            //using (TransactionScope ts = new TransactionScope())
            //{
                try
                {

                    //システム時間を更新
                    //updateUserAndTime(ds.Tables[0]);
                    //直接更新
                    UpdateDataTable(ds.Tables[0]);
                    foreach (string t in names)
                    {
                        EntityAdapter ea = new EntityAdapter(_entityRequest, t);
                        //時間を更新
                        //ea.updateUserAndTime(ds.Tables[t]);
                        ea.UpdateDataTable(ds.Tables[t]);
                    }
                    //ts.Complete();
                }
                catch (Exception ex)
                {
                    return new Result(ex);
                }

            //}


            return new Result();

        }

        private Result SaveData_Change(TableSaveRequest req, DataSet ds, bool updateSystemItem)
        {
            string[] names = _Entity.Rela1NList.Where(e => e.IsSubTable).Select(e => e.RelaEntityName).ToArray();

            //差分更新
            string[] keys = GetKeys();
            DataSet dsDB = new DataSet();

            DataTable dsDT = ds.Tables[_Entity.EntityName];

            //dsDB.Tables.Add(dsDT.Clone());

            foreach (DataRow dr in dsDT.Rows)
            {
                List<object> keyValueList = new List<object>();

                bool identityNew = false;

                foreach (string k in keys)
                {
                    Y_EntityField field = _Entity.FieldList.Find(e => e.FieldName == k);

                    if (field.IsIdentity && Convert.IsDBNull(dr[k]))
                    {
                        identityNew = true;
                        break;
                    }

                    keyValueList.Add(dr[k]);
                }

                if (identityNew) continue;

                object[] keyValues = keyValueList.ToArray();
                FillDataSetByKey(dsDB, keyValues);

                foreach (string t in names)
                {
                    if (dsDB.Tables.Contains(t))
                    {
                        EntityAdapter ea = new EntityAdapter(_entityRequest, t);
                        ea.FillDataSetByKey(dsDB, keyValues);
                    }
                }
            }


           
            //システム時間を更新
            //updateUserAndTime(ds.Tables[0]);

            //データを更新
            UpdateDataTableDiff(ds.Tables[0], dsDB.Tables[0], updateSystemItem);

            //子テーブルの更新
            foreach (string t in names)
            {
                if (ds.Tables.Contains(t) == false) continue;
                
                EntityAdapter ea = new EntityAdapter(_entityRequest, t);

                //ユーザ、時間を更新
                //ea.updateUserAndTime(ds.Tables[t]);

                ea.UpdateDataTableDiff(ds.Tables[t], dsDB.Tables[t], updateSystemItem);
            }
         
            return new Result();
        }

        private Result SaveData_Change(string filter, DataTable dsDT, bool updateSystemItem)
        {
            DataSet dsDB = new DataSet();
            string sql = "SELECT * FROM " + _Entity.EntityName;
            if (filter != string.Empty) sql += " WHERE " + filter;
            base.FillDataSetBySQL(dsDB, sql);

            //データを更新
            UpdateDataTableDiff(dsDT, dsDB.Tables[0], updateSystemItem);

            return new Result();
        }


        private int UpdateDataSet(DataSet ds)
        {
            return UpdateDataSet(ds, null);
        }

        /// <summary>
        /// Datasetの更新処理
        /// </summary>
        /// <param name="ds">データテーブル</param>
        /// <param name="updateFields"></param>
        /// <returns>更新結果</returns>
        private int UpdateDataSet(DataSet ds, List<Y_EntityField> updateFields)
        {
            this.UpdateDataSetInfo(ds);

            IDbDataAdapter adapter = InitAdpater(this.m_DBMachine.Provider);

            int ret = base.m_DBMachine.UpdateDataSet(adapter, ds, "this._TableDefine.sTableName");

            return ret;
        }

        /// <summary>
        /// Datasetの更新処理
        /// </summary>
        /// <param name="dt">データセット</param>
        /// <returns></returns>
        private int UpdateDataTable(DataTable dt)
        {
            return this.UpdateDataTable(dt, null);
        }

        private int UpdateDataTableDiff(DataTable dt, DataTable oldDt, bool updateSystemItem)
        {


            DataTable difDt = GetDiffDT(oldDt, dt, GetKeys(), updateSystemItem);
            
            if (difDt == null) return 0;


            return UpdateDataTable(difDt, null);
        }
        /// <summary>
        /// Datasetの更新処理
        /// </summary>
        /// <param name="dt">データテーブル</param>
        /// <param name="updateFields"></param>
        /// <returns>更新結果</returns>
        private int UpdateDataTable(DataTable dt, List<Y_EntityField> updateFields)
        {
            this.UpdateDataTableInfo(dt);

            //Adpaterの作成及び初期化、ＳＱＬ、パラメータの作成
            IDbDataAdapter adapter = InitAdpater(this.m_DBMachine.Provider);
            //更新処理の実行
            int ret = base.m_DBMachine.UpdateDataTable(adapter, dt);

            return ret;
        }




        /// <summary>
        /// データセットを取得する
        /// </summary>
        /// <returns>データセット</returns>
        private DataSet GetDataSet()
        {
            return this.GetDataSet("*", "", "", 0, 0, false);
        }

        /// <summary>
        /// データセットを取得する
        /// </summary>
        /// <param name="select">SelectのSQL文</param>
        /// <returns>データセット</returns>
        private DataSet GetDataSet(string select)
        {
            return this.GetDataSet(select, "", "", 0, 0, false);
        }

        /// <summary>
        /// データセットを取得する
        /// </summary>
        /// <param name="select">SelectのSQL文</param>
        /// <param name="filter">フィルター</param>
        /// <returns>データセット</returns>
        private DataSet GetDataSet(string select, string filter)
        {
            return this.GetDataSet(select, filter, "", 0, 0, false);
        }

        /// <summary>
        /// データセットを取得する
        /// </summary>
        /// <param name="select">SelectのSQL文</param>
        /// <param name="filter">フィルター</param>
        /// <param name="orderBy">ソート文</param>
        /// <returns>データセット</returns>
        private DataSet GetDataSet(string select, string filter, string orderBy)
        {
            return this.GetDataSet(select, filter, orderBy, 0, 0, false);
        }

        /// <summary>
        /// データセットを取得する
        /// </summary>
        /// <param name="select">SelectのSQL文</param>
        /// <param name="filter">フィルター</param>
        /// <param name="orderBy">ソート文</param>
        /// <param name="rowFrom">開始行番号</param>
        /// <param name="rowTo">終了行番号</param>
        /// <returns>データセット</returns>
        public DataSet GetDataSet(string select, string filter, string orderBy, int rowFrom, int rowTo)
        {
            return this.GetDataSet(select, filter, orderBy, rowFrom, rowTo, false);
        }

        /// <summary>
        /// データセットを取得する
        /// </summary>
        /// <param name="select">SelectのSQL文</param>
        /// <param name="filter">フィルター</param>
        /// <param name="orderBy">ソート文</param>
        /// <param name="rowFrom">開始行番号</param>
        /// <param name="rowTo">終了行番号</param>
        /// <param name="distinct">Distinctフラグ</param>
        /// <returns>データセット</returns>
        private DataSet GetDataSet(string select, string filter, string orderBy, int rowFrom, int rowTo, bool distinct)
        {
            DataSet ds = new DataSet(); // TableManager.GetDataSetFromName(this._TableDefine.sTableName);
            this.FillDataSet(ds, select, filter, orderBy, rowFrom, rowTo, distinct);

            return ds;
        }

        /// <summary>
        /// データセットの取得
        /// </summary>
        /// <param name="ds">データセット</param>
        private  void FillDataSet(DataSet ds)
        {
            this.FillDataSet(ds, "*", "", "", 0, 0, false);
        }

        /// <summary>
        /// データセットの取得
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="select">取得する項目の配列</param>
        private  void FillDataSet(DataSet ds, string select)
        {
            this.FillDataSet(ds, select, "", "", 0, 0, false);
        }

        /// <summary>
        /// データセットの取得
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="select">取得する項目の配列</param>
        /// <param name="filter">フィルター</param>
        private  void FillDataSet(DataSet ds, string select, string filter)
        {
            this.FillDataSet(ds, select, filter, "", 0, 0, false);
        }

        private void FillDataTableByKey(ref DataTable dt, object[] keys)
        {
            

            string sql="SELECT * FROM " + this._Entity.EntityName + " " 
                       + "WHERE " + GetSQLFromKey(keys);

            base.FillDataTableBySQL(ref dt, sql);
      
        }

        /// <summary>
        /// データセットの取得
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="select">取得する項目の配列</param>
        /// <param name="filter">フィルター</param>
        /// <param name="orderBy">ソート文</param>
        /// <param name="rowFrom">開始行番号</param>
        /// <param name="rowTo">終了行番号</param>
        /// <param name="distinct">Distinctフラグ</param>
        private  void FillDataSet(DataSet ds, string select, string filter, string orderBy, int rowFrom, int rowTo, bool distinct)
        {
            const int MAXKEYS = 500;

            string sql = this.GetSelectSQL(select, filter, orderBy, rowFrom, rowTo, distinct);

            //-----------------------------------------------------------------
            // SQL文にINの値の個数が多すぎる場合、何度分けてデータを取得する処理
            //-----------------------------------------------------------------
            int p = sql.ToUpper().IndexOf(" IN");

            if (p > 0)
            {
                if (sql.IndexOf("(", p) - p < 3)
                {
                    p = -1;
                }
                if (sql.Split(',').Length < MAXKEYS)
                {
                    p = -1;
                }
            }

            if (p < 0)
            {
                // 「IN」キーは無し、またはINの値の個数 < 500の場合、データを取得する
                FillDataSetBySQL(ds, sql, this._Entity.EntityName);
            }
            else
            {
                // INの値の個数 > 500の場合、何度分けてデータを取得する
                int p1 = sql.IndexOf("(", p) + 1;
                int p2 = sql.IndexOf(")", p1);
                string[] values = sql.Substring(p1, p2 - p1).Split(',');
                string sql1 = sql.Substring(0, p1);
                string sql2 = sql.Substring(p2);
                List<string> al = new List<string>();
                for (int i = 0; i <= values.Length - 1; i++)
                {
                    al.Add(values[i]);
                    if ((i > 0 && (i % MAXKEYS) == 0) || i == values.Length - 1)
                    {
                        sql = sql1 + string.Join(",", al.ToArray()) + sql2;
                        FillDataSetBySQL(ds, sql, this._Entity.EntityName);
                        al.Clear();
                    }
                }
            }
        }

        /// <summary>
        /// キーの値を条件に、データセットを取得する
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="selectItems">抽出する項目</param>
        /// <param name="objKeys">キー</param>
        private void FillDataSetByKey(DataSet ds,  params object[] objKeys)
        {
            FillDataSet(ds, "*", this.GetSQLFromKey(objKeys));
            

        }

       


        /// <summary>
        /// Commandの初期化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="p_fields"></param>
        private IDbDataAdapter InitAdpater(IProviderManager provider)
        {
            IDbDataAdapter Adapter = provider.DBAdapter();
            List<Y_EntityField> p_fields = _Entity.FieldList.Where(f => f.IsIdentity == false).ToList();

            IDbCommand InsertCommand;
            InsertCommand = provider.DBCommand();
            InsertCommand.CommandText = GetInsertCommandText(provider, _Entity, p_fields);

            foreach (Y_EntityField d in p_fields)
            {
                InsertCommand.Parameters.Add(provider.DBParameter(d));
            }
            Adapter.InsertCommand = InsertCommand;
            
            
            IDbCommand UpdateCommand;
            UpdateCommand = provider.DBCommand();
            UpdateCommand.CommandText = GetUpdateCommandText(provider, _Entity, p_fields);
            List<Y_EntityField> notkeyfields = GetNotKeyFields();
            List<Y_EntityField> keyfields = GetKeyFields();

            foreach (Y_EntityField d in p_fields)
            {
                UpdateCommand.Parameters.Add(provider.DBParameter(d));
            }
            
            foreach (Y_EntityField d in keyfields)
            {
                UpdateCommand.Parameters.Add(provider.DBOriginalParameter(d));
            }
            Adapter.UpdateCommand = UpdateCommand;
            
            
            IDbCommand deleteCommand;
            deleteCommand = provider.DBCommand();
            deleteCommand.CommandText = GetDeleteCommandText(provider);
            keyfields =GetKeyFields();
            foreach (Y_EntityField d in keyfields)
            {
                deleteCommand.Parameters.Add(provider.DBOriginalParameter(d));
            }
            Adapter.DeleteCommand = deleteCommand;
            return Adapter;
        }

        /// <summary>
        /// キー配列を取得する
        /// </summary>
        /// <param name="tblDef"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        private List<object[]> GetKeyArrayOfAdd(Y_EntityField tblDef, DataSet ds)
        {
            List<object[]> al = new List<object[]>();
            string[] keys = GetKeys();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    List<object> r = new List<object>();
                    foreach (string k in keys)
                    {
                        r.Add(row[k]);
                    }
                    al.Add(r.ToArray());
                }
            }

            return al;
        }

        /// <summary>
        /// Insert文を取得する
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private string GetInsertCommandText(IProviderManager provider, Y_Entity table, List<Y_EntityField> field)
        {
            string insertCommand;

            StringBuilder sqlbuff = new StringBuilder();

            //List<Y_EntityField> allfields = null;   // tblDef.Fields;

            sqlbuff.Append("INSERT INTO " + table.EntityName);
            sqlbuff.Append("(" + string.Join(",", field.Select(e=>e.FieldName).ToArray()) + ") ");
            sqlbuff.Append("\nVALUES(");
            sqlbuff.Append(GetFieldsParameterString(provider, field, ","));
            sqlbuff.Append(") ");
            insertCommand = sqlbuff.ToString();
            return insertCommand;
        }

        /// <summary>
        /// Update文を取得する
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fieldList"></param>
        /// <returns></returns>
        private string GetUpdateCommandText(IProviderManager provider, Y_Entity table, List<Y_EntityField> fieldList)
        {

            StringBuilder sqlbuff = new StringBuilder();
            sqlbuff.Append("UPDATE " + table.EntityName + " SET ");
            sqlbuff.Append(GetFieldsParameterSQL(provider, ref fieldList,  ","));
            if (GetKeyFields().Count > 0)
            {
                List<Y_EntityField> keyList = GetKeyFields();

                sqlbuff.Append(" WHERE ");
                sqlbuff.Append(GetFieldsParameterSQL_Original(provider, ref keyList, " and "));
            }
            return sqlbuff.ToString();
        }

        /// <summary>
        /// Delete文を取得する
        /// </summary>
        /// <param name="tblDef"></param>
        /// <returns></returns>
        private string GetDeleteCommandText(IProviderManager provider)
        {
            StringBuilder sqlbuff = new StringBuilder();
            sqlbuff.Append("DELETE FROM " + this._Entity.EntityName + " ");
            if (GetKeyFields().Count > 0)
            {
                List<Y_EntityField> f = GetKeyFields();

                sqlbuff.Append(" WHERE ");
                sqlbuff.Append(GetFieldsParameterSQL_Original(provider, ref f, " and "));
            }
            return sqlbuff.ToString();
        }

        private void DeleteByKey(params object[] keys)
        {
            string sql="DELETE FROM " + this._Entity.EntityName + " " 
                       + "WHERE " + GetSQLFromKey(keys);
            base.RunSQL(sql);

        }

        public void DeleteByRelaKey(string[] keyFields, object[] keys)
        {
            string sql = "DELETE FROM " + this._Entity.EntityName + " "
                       + "WHERE " + GetSQLFromItem(keyFields, keys);
            base.RunSQL(sql);

        }

        public bool HasData(string[] keyFields, object[] keys)
        {
            string sql = "select count(*) FROM(SELECT TOP 1 * FROM " + this._Entity.EntityName + " "
                       + "WHERE " + GetSQLFromItem(keyFields,keys) + ") AS T";
            DataSet ds = new DataSet();
            base.FillDataSetBySQL(ds, sql);

            return ((int) ds.Tables[0].Rows[0][0] >0);

        }

        public bool UniqueCheck(object[] keys, string[] uniqueFields, object[] values)
        {
            //SQL Server
            string sql = "select count(*) FROM(SELECT TOP 1 * FROM " + this._Entity.EntityName + " "
                          + "WHERE (" + GetSQLFromItemNoEQ(GetKeys(), keys) + ") AND " + GetSQLFromItem(uniqueFields, values) + ") AS T";
            DataSet ds = new DataSet();
            base.FillDataSetBySQL(ds, sql);

            return ((int)ds.Tables[0].Rows[0][0] > 0);
        }

        /// <summary>
        /// Datasetにある、更新者、更新日付の更新
        /// </summary>
        /// <param name="ds"></param>
        private void UpdateDataSetInfo(DataSet ds)
        {
            if (ds == null)
            {
                return;
            }
            this.UpdateDataTableInfo(ds.Tables[0]);
        }


        /// <summary>
        /// Datatableにある、更新者、更新日付の更新
        /// </summary>
        /// <param name="dt"></param>
        private void UpdateDataTableInfo(DataTable dt)
        {
            if (dt == null)
            {
                return;
            }
            if (UpdateUserFieldName == null && CreateTimeFieldName == null)
            {
                return;
            }

            System.DateTime nowtime = GetSysDateTime();

            nowtime = DataUtil.StringToDate(DataUtil.DateToString(nowtime, "yyyy\\/MM\\/dd HH:mm:ss"));
            if (!(UpdateUserFieldName == null))
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        row[UpdateUserFieldName] = _UpdateUser;
                    }
                }
            }
            if (!(CreateTimeFieldName == null))
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        row[CreateTimeFieldName] = nowtime;
                    }
                }
            }
            if (CreateTimeFieldName == null && !(UpdateTimeFieldName == null))
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        row[UpdateTimeFieldName] = nowtime;
                    }
                }
            }
            if (!(UpdateTimeFieldName == null))
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Modified)
                    {
                        row[UpdateTimeFieldName] = nowtime;
                    }
                }
            }
        }

        private string GetFieldsParameterSQL(IProviderManager provider, ref List<Y_EntityField> p_fields, string p_sJoin)
        {
            List<string> list = new List<string>();
            foreach (Y_EntityField d in p_fields)
            {
                list.Add(provider.GetFieldsParameterSQL(d.FieldName));
            }
            return string.Join(p_sJoin, list.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_fields"></param>
        /// <param name="p_adapter"></param>
        /// <param name="p_sJoin"></param>
        /// <returns></returns>
        private string GetFieldsParameterSQL_Original(IProviderManager provider, ref List<Y_EntityField> p_fields, string p_sJoin)
        {
            List<string> list = new List<string>();
            foreach (Y_EntityField d in p_fields)
            {
                list.Add(provider.GetFieldsParameterSQL_Original(d.FieldName));
            }
            return string.Join(p_sJoin, list.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="p_adapter"></param>
        /// <param name="p_sJoin"></param>
        /// <returns></returns>
        private string GetFieldsParameterString(IProviderManager provider, List<Y_EntityField> fields, string p_sJoin)
        {
            List<string> list = new List<string>();

            foreach (Y_EntityField d in fields)
            {
                list.Add(provider.GetFieldsParameterString(d.FieldName));
            }

            return string.Join(p_sJoin, list.ToArray());
        }

        /// <summary>
        /// 唐 获取关系表名称
        /// </summary>
        /// <returns></returns>
        public string[] GetRela1NName()
        {
            return _Entity.Rela1NList.Where(e => e.IsSubTable).Select(e => e.RelaEntityName).ToArray();
        }
        /// <summary>
        /// 唐 加密数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="keyFieldList"></param>
        /// <returns></returns>
        private DataTable Encrypt(DataTable dt)
        {
            if (dt == null || dt.Rows.Count==0)
            {
                return dt;
            }
            List<Y_EntityField> fieldList = GetListColumns(dt.TableName);
            if (fieldList==null || fieldList.Count == 0)
            {
                return null;
            }
            foreach (DataRow dr in dt.Rows)
            {
                foreach (Y_EntityField ef in fieldList)
                {
                    if (ef.IsEncryption == false)
                    {
                        continue;
                    }
                    if (dt.Columns.Contains(ef.FieldName))
                    {
                        dr[ef.FieldName] = DataUtil.Encrypt(DataUtil.CStr(dr[ef.FieldName]), ef.EncryptionType);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 唐 解密数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="keyFieldList"></param>
        /// <returns></returns>
        private DataTable Decrypt(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }
            List<Y_EntityField> fieldList = GetListColumns(dt.TableName);
            if (fieldList == null || fieldList.Count == 0)
            {
                return null;
            }
            foreach (DataRow dr in dt.Rows)
            {
                foreach (Y_EntityField ef in fieldList)
                {
                    if (ef.IsEncryption == false)
                    {
                        continue;
                    }
                    dr[ef.FieldName] = DataUtil.Decrypt(DataUtil.CStr(dr[ef.FieldName]), ef.EncryptionType );
                }
            }
            return dt;
        }

        
        /// /// <summary>
        /// 唐 Y_EntityField中是否加密解密封装成dic
        /// </summary>
        /// <param name="keyFieldList"></param>
        /// <returns></returns>
        private Dictionary<string, Y_EntityField> GetDicColumns(string entityName)
        {
            Dictionary<string, Y_EntityField> dic = new Dictionary<string, Y_EntityField>();
            List<Y_EntityField> listField = GetListColumns(entityName);
            foreach (Y_EntityField fidld in listField)
            {
                dic.Add(fidld.FieldName, fidld);
            }
            return dic;
        }

        /// <summary>
        /// 唐 Y_EntityField中是否加密解密封装成List
        /// </summary>
        /// <param name="keyFieldList"></param>
        /// <returns></returns>
        private List<Y_EntityField> GetListColumns(string entityName)
        {
            return _Entity.FieldList.Where<Y_EntityField>(o=>o.EntityName==entityName && o.IsEncryption==true).ToList();
        }

    }
}
