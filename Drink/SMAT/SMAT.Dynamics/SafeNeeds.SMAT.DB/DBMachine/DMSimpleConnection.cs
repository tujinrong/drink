using System;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Threading;

using SafeNeeds.DySmat.DB.Exception;
using SafeNeeds.DySmat.DB;


namespace SafeNeeds.SMAT.DB
{
    internal class DMOneConnection : DMBase, IDBMachine
    {
        internal static IDbConnection m_cn;
        //protected static IDbTransaction m_tran;
        private int m_commandTimeOut = 30;
        //private DBTransaction m_cmTrans;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public DMOneConnection(DBConfig config) : base(config)
        {
            if (m_cn == null)
            {
                m_cn = m_DBProvider.DBConnection();
                try
                {
                    m_cn.Open();
                }
                catch (System.Exception ex)
                {
                    throw new ConnectException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                return this.m_commandTimeOut;
            }
            set
            {
                this.m_commandTimeOut = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            m_cn.Close();
            m_cn.Dispose();
            m_cn = null;
            //m_tran = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strSQL"></param>
        /// <param name="tableName"></param>
        public void FillDataSetBySQL(DataSet ds, string strSQL, string tableName)
        {
            IDbDataAdapter adapter = m_DBProvider.DBAdapter();

            //if (m_cmTrans == null)
            //{
                adapter = m_DBProvider.DBAdapter(strSQL, m_cn);
                //adapter.SelectCommand.Transaction = m_tran;
            //}
            //else
            //{
            //    adapter = m_DBProvider.DBAdapter(strSQL, m_cmTrans.Connection);
            //    adapter.SelectCommand.Transaction = m_cmTrans.Transaction;
            //}
            if (this.m_commandTimeOut != 30)
            {
                adapter.SelectCommand.CommandTimeout = this.m_commandTimeOut;
            }
            if (tableName == "")
            {
                m_DBProvider.Fill(adapter, ds);
            }
            else
            {
                m_DBProvider.Fill(adapter, ds, tableName);
            }
        }
        public virtual void FillDataTableBySQL(ref DataTable dt, string strSQL)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strSP"></param>
        /// <param name="strTableName"></param>
        /// <param name="param"></param>
        public void FillDataSetBySP(DataSet ds, string strSP, string strTableName, params DBParameter[] param)
        {
            IDbDataAdapter adapter = m_DBProvider.DBAdapter();
            //if (m_cmTrans == null)
            //{
                adapter = m_DBProvider.DBAdapter(strSP, m_cn);
                //adapter.SelectCommand.Transaction = m_tran;
            //}
            //else
            //{
            //    adapter = m_DBProvider.DBAdapter(strSP, m_cmTrans.Connection);
            //    adapter.SelectCommand.Transaction = m_cmTrans.Transaction;
            //}
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            foreach (DBParameter p in param)
            {
                adapter.SelectCommand.Parameters.Add(m_DBProvider.DBParameter(p));
            }
            if (this.m_commandTimeOut != 30)
            {
                adapter.SelectCommand.CommandTimeout = this.m_commandTimeOut;
            }
            if (strTableName == "")
            {
                m_DBProvider.Fill(adapter, ds);
            }
            else
            {
                m_DBProvider.Fill(adapter, ds, strTableName);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public void EndTrans()
        //{
        //    m_cmTrans = null;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="trans"></param>
        //public void StartTrans(DBTransaction trans)
        //{
        //    m_cmTrans = trans;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int RunSQL(string sql)
        {
            IDbCommand dc = m_DBProvider.DBCommand();
            dc.CommandText = sql;
            if (this.m_commandTimeOut != 30)
            {
                dc.CommandTimeout = this.m_commandTimeOut;
            }
            //if (m_cmTrans == null)
            //{
                dc.Connection = m_cn;
                //dc.Transaction = m_tran;
            //}
            //else
            //{
            //    dc.Transaction = m_cmTrans.Transaction;
            //    dc.Connection = m_cmTrans.Connection;
            //}
            return base.ExecuteNonQuery(dc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int RunSP(string sql, params DBParameter[] param)
        {
            IDbCommand dc = m_DBProvider.DBCommand();
            dc.CommandText = sql;
            dc.CommandType = CommandType.StoredProcedure;
            foreach (DBParameter p in param)
            {
                dc.Parameters.Add(m_DBProvider.DBParameter(p));
            }
            if (this.m_commandTimeOut != 30)
            {
                dc.CommandTimeout = this.m_commandTimeOut;
            }
            //if (m_cmTrans == null)
            //{
                dc.Connection = m_cn;
                //dc.Transaction = m_tran;
            //}
            //else
            //{
            //    dc.Transaction = m_cmTrans.Transaction;
            //    dc.Connection = m_cmTrans.Connection;
            //}
            return base.ExecuteNonQuery(dc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int UpdateDataSet(IDbDataAdapter adapter, DataSet ds, string tableName)
        {
            
            //if (m_cmTrans == null)
            //{
                adapter.UpdateCommand.Connection = m_cn;
                //adapter.UpdateCommand.Transaction = m_tran;
                adapter.InsertCommand.Connection = m_cn;
                //adapter.InsertCommand.Transaction = m_tran;
                adapter.DeleteCommand.Connection = m_cn;
                //adapter.DeleteCommand.Transaction = m_tran;
            //}
            //else
            //{
            //    adapter.UpdateCommand.Connection = m_cmTrans.Connection;
            //    adapter.UpdateCommand.Transaction = m_cmTrans.Transaction;
            //    adapter.InsertCommand.Connection = m_cmTrans.Connection;
            //    adapter.InsertCommand.Transaction = m_cmTrans.Transaction;
            //    adapter.DeleteCommand.Connection = m_cmTrans.Connection;
            //    adapter.DeleteCommand.Transaction = m_cmTrans.Transaction;
            //}
            return m_DBProvider.update(adapter, ds, tableName);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int UpdateDataTable(IDbDataAdapter adapter, System.Data.DataTable dt)
        {
            
            //if (m_cmTrans == null)
            //{
                adapter.UpdateCommand.Connection = m_cn;
                //adapter.UpdateCommand.Transaction = m_tran;
                adapter.InsertCommand.Connection = m_cn;
                //adapter.InsertCommand.Transaction = m_tran;
                adapter.DeleteCommand.Connection = m_cn;
                //adapter.DeleteCommand.Transaction = m_tran;
            //}
            //else
            //{
            //    adapter.UpdateCommand.Connection = m_cmTrans.Connection;
            //    adapter.UpdateCommand.Transaction = m_cmTrans.Transaction;
            //    adapter.InsertCommand.Connection = m_cmTrans.Connection;
            //    adapter.InsertCommand.Transaction = m_cmTrans.Transaction;
            //    adapter.DeleteCommand.Connection = m_cmTrans.Connection;
            //    adapter.DeleteCommand.Transaction = m_cmTrans.Transaction;
            //}
            return m_DBProvider.Update(adapter, dt);
        }
    }
}
