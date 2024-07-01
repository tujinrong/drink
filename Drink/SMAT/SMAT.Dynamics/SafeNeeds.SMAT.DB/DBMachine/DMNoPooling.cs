using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Threading;

using SafeNeeds.SMAT.DB.Exception;
using System.Data.SqlClient;

namespace SafeNeeds.SMAT.DB
{
    /// <summary>
    /// 
    /// </summary>
    public class DMNewConnection : DMBase, IDBMachine
    {
        private int _CommandTimeOut = 30;
        //private DBTransaction m_cmTrans;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public DMNewConnection(DBConfig config):base(config)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                return _CommandTimeOut;
            }
            set
            {
                _CommandTimeOut = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Close()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strSQL"></param>
        /// <param name="tableName"></param>
        public virtual void FillDataSetBySQL(DataSet ds, string strSQL, string tableName)
        {
            IDbConnection cn;
            IDbDataAdapter adapter;

            //if (m_cmTrans == null)
            //{
                cn = m_DBProvider.DBConnection();
                try
                {
                    cn.Open();
                }
                catch (System.Exception ex)
                {
                    throw new ConnectException(ex.Message);
                }
                adapter = m_DBProvider.DBAdapter(strSQL, cn);
            //}
            //else
            //{
            //    cn = m_cmTrans.Connection;
            //    adapter = m_DBProvider.DBAdapter(strSQL, cn);
            //    adapter.SelectCommand.Transaction = m_cmTrans.Transaction;
            //}
            if (_CommandTimeOut != 30)
            {
                adapter.SelectCommand.CommandTimeout = _CommandTimeOut;
            }
            if (tableName == "")
            {
                m_DBProvider.Fill(adapter, ds);
            }
            else
            {
                m_DBProvider.Fill(adapter, ds, tableName);
            }

            m_DBProvider.Dispose(adapter);

            //if (m_cmTrans == null)
            //{
            cn.Close();
            cn.Dispose();
            //}
        }

        public virtual void FillDataTableBySQL(ref DataTable dt, string strSQL)
        {
            DataSet ds = new DataSet();
            FillDataSetBySQL(ds, strSQL, "");
            dt = ds.Tables[0].Copy();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strSP"></param>
        /// <param name="strTableName"></param>
        /// <param name="param"></param>
        public virtual void FillDataSetBySP(DataSet ds, string strSP, string strTableName, params DBParameter[] param)
        {
            IDbConnection cn;
            IDbDataAdapter adapter;

            //if (m_cmTrans == null)
            //{
                cn = m_DBProvider.DBConnection();
                try
                {
                    cn.Open();
                }
                catch (System.Exception ex)
                {
                    throw new ConnectException(ex.Message);
                }
                adapter = m_DBProvider.DBAdapter(strSP, cn);
            //}
            //else
            //{
            //    cn = m_cmTrans.Connection;
            //    adapter = m_DBProvider.DBAdapter(strSP, cn);
            //    adapter.SelectCommand.Transaction = m_cmTrans.Transaction;
            //}
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            foreach (DBParameter p in param)
            {
                adapter.SelectCommand.Parameters.Add(m_DBProvider.DBParameter(p));
            }
            if (_CommandTimeOut != 30)
            {
                adapter.SelectCommand.CommandTimeout = _CommandTimeOut;
            }
            if (strTableName == "")
            {
                m_DBProvider.Fill(adapter, ds);
            }
            else
            {
                m_DBProvider.Fill(adapter, ds, strTableName);
            }
            //if (m_cmTrans == null)
            //{
            cn.Close(); 
            cn.Dispose();
            //}
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
        public virtual int RunSQL(string sql)
        {
            IDbConnection cn = null;
            IDbCommand dc = m_DBProvider.DBCommand();
            dc.CommandText = sql;
            
            //if (m_cmTrans == null)
            //{
                cn = m_DBProvider.DBConnection();
                try
                {
                    cn.Open();
                }
                catch (System.Exception ex)
                {
                    throw new ConnectException(ex.Message);
                }
                dc.Connection = cn;
            //}
            //else
            //{
            //    dc.Transaction = m_cmTrans.Transaction;
            //    dc.Connection = m_cmTrans.Connection;
            //}
            if (_CommandTimeOut != 30)
            {
                dc.CommandTimeout = _CommandTimeOut;
            }
            int ret = base.ExecuteNonQuery(dc);
            //if (m_cmTrans == null)
            //{
            cn.Close();
            cn.Dispose();
            //}
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual int RunSP(string sql, params DBParameter[] param)
        {
            IDbCommand dc = m_DBProvider.DBCommand();
            dc.CommandText = sql;
            dc.CommandType = CommandType.StoredProcedure;
            foreach (DBParameter p in param)
            {
                dc.Parameters.Add(m_DBProvider.DBParameter(p));
            }
            IDbConnection cn = null;
            //if (m_cmTrans == null)
            //{
                cn = m_DBProvider.DBConnection();
                try
                {
                    cn.Open();
                }
                catch (System.Exception ex)
                {
                    throw new ConnectException(ex.Message);
                }
                dc.Connection = cn;
            //}
            //else
            //{
            //    dc.Transaction = m_cmTrans.Transaction;
            //    dc.Connection = m_cmTrans.Connection;
            //}
            if (_CommandTimeOut != 30)
            {
                dc.CommandTimeout = _CommandTimeOut;
            }
            int ret = base.ExecuteNonQuery(dc);
            foreach (DBParameter p in param)
            {
                p.Value = (dc.Parameters[p.ParameterName] as IDataParameter).Value;
            }

            //if (m_cmTrans == null)
            //{
            cn.Close();
            cn.Dispose();
            //}
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual int UpdateDataSet(IDbDataAdapter adapter, DataSet ds, string tableName)
        {
            IDbConnection cn = null;
            //IDbTransaction tran = null;
            //if (m_cmTrans == null)
            //{
                cn = m_DBProvider.DBConnection();
                try
                {
                    cn.Open();
                }
                catch (System.Exception ex)
                {
                    throw new ConnectException(ex.Message);
                }
            //}
            //else
            //{
            //    cn = m_cmTrans.Connection;
            //    tran = m_cmTrans.Transaction;
            //}
            
            adapter.UpdateCommand.Connection = cn;
            //adapter.UpdateCommand.Transaction = tran;
            adapter.InsertCommand.Connection = cn;
            //adapter.InsertCommand.Transaction = tran;
            adapter.DeleteCommand.Connection = cn;
            //adapter.DeleteCommand.Transaction = tran;
            int ret = m_DBProvider.update(adapter, ds, tableName);
            //if (m_cmTrans == null)
            //{
            cn.Close();
            cn.Dispose();
            //}
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public virtual int UpdateDataTable(IDbDataAdapter adapter, System.Data.DataTable dt)
        {
            IDbConnection cn = null;
            //IDbTransaction tran = null;
         
            //if (m_cmTrans == null)
            //{
                cn = m_DBProvider.DBConnection();
                try
                {
                    cn.Open();
                }
                catch (System.Exception ex)
                {
                    throw new ConnectException(ex.Message);
                }
            //}
            //else
            //{
            //    cn = m_cmTrans.Connection;
            //    tran = m_cmTrans.Transaction;
            //}
            adapter.UpdateCommand.Connection = cn;
            //adapter.UpdateCommand.Transaction = tran;
            adapter.InsertCommand.Connection = cn;
            //adapter.InsertCommand.Transaction = tran;
            adapter.DeleteCommand.Connection = cn;
            //adapter.DeleteCommand.Transaction = tran;
            int ret = m_DBProvider.Update(adapter, dt);
            //if (m_cmTrans == null)
            //{
            cn.Close();
            cn.Dispose();
            //}
            return ret;
        }
    }
}
