using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Threading;

using SafeNeeds.DySmat.DB.Exception;
using System.Data.SqlClient;

namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// 
    /// </summary>
    public class DMNewConnection : DMConnectionBase, IDBConnection
    {
        private int _CommandTimeOut = 30;
        private int _dbno;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public DMNewConnection(DBConfig config):base(config)
        {
            _dbno = config.ProjID;
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
        public  void Close()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strSQL"></param>
        /// <param name="tableName"></param>
        public  void FillDataSetBySQL(DataSet ds, string strSQL, string tableName, params DBParameter[] param)
        {
            DBGlobal.WriteLog(strSQL);

            IDbTransaction tran;
            IDbConnection cn = DySmat.DBTransactionScope.GetConnection(_dbno, out tran);
            IDbDataAdapter adapter;

            adapter = m_DBProvider.DBAdapter(strSQL, cn);
            adapter.SelectCommand.Transaction = tran;

            foreach (DBParameter p in param)
            {
                adapter.SelectCommand.Parameters.Add(m_DBProvider.DBParameter(p));
            }

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

            if (tran==null) cn.Close();
        }

        public  void FillDataTableBySQL(ref DataTable dt, string strSQL, params DBParameter[] param)
        {
            DataSet ds = new DataSet();
            FillDataSetBySQL(ds, strSQL, "", param);
            dt = ds.Tables[0].Copy();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strSP"></param>
        /// <param name="strTableName"></param>
        /// <param name="param"></param>
        public  void FillDataSetBySP(DataSet ds, string strSP, string strTableName, params DBParameter[] param)
        {
            DBGlobal.WriteLog(strSP);


            IDbTransaction tran;
            IDbConnection cn = DySmat.DBTransactionScope.GetConnection(_dbno, out tran);

            IDbDataAdapter adapter;

            adapter = m_DBProvider.DBAdapter(strSP, cn);
            adapter.SelectCommand.Transaction = tran;

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

            if (tran == null) cn.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public  int RunSQL(string sql)
        {
            DBGlobal.WriteLog(sql);

            IDbTransaction tran;
            IDbConnection cn = DySmat.DBTransactionScope.GetConnection(_dbno, out tran);

            IDbCommand dc = m_DBProvider.DBCommand();
            dc.CommandText = sql;
            
            dc.Connection = cn;
            dc.Transaction = tran;

            if (_CommandTimeOut != 30)
            {
                dc.CommandTimeout = _CommandTimeOut;
            }
            int ret = base.ExecuteNonQuery(dc);

            if (tran==null) cn.Close();
            
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public  int RunSP(string sql, params DBParameter[] param)
        {
            IDbTransaction tran;
            IDbConnection cn = DySmat.DBTransactionScope.GetConnection(_dbno, out tran);

            IDbCommand dc = m_DBProvider.DBCommand();
            dc.CommandText = sql;
            dc.CommandType = CommandType.StoredProcedure;
            foreach (DBParameter p in param)
            {
                dc.Parameters.Add(m_DBProvider.DBParameter(p));
            }
            dc.Connection = cn;
            dc.Transaction = tran;

            if (_CommandTimeOut != 30)
            {
                dc.CommandTimeout = _CommandTimeOut;
            }
            int ret = base.ExecuteNonQuery(dc);
            foreach (DBParameter p in param)
            {
                p.Value = (dc.Parameters[p.ParameterName] as IDataParameter).Value;
            }

            if (tran==null) cn.Close();

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public  int UpdateDataSet(IDbDataAdapter adapter, DataSet ds, string tableName)
        {
            IDbTransaction tran;
            IDbConnection cn = DySmat.DBTransactionScope.GetConnection(_dbno, out tran);
            
            adapter.UpdateCommand.Connection = cn;
            adapter.UpdateCommand.Transaction = tran;
            adapter.InsertCommand.Connection = cn;
            adapter.InsertCommand.Transaction = tran;
            adapter.DeleteCommand.Connection = cn;
            adapter.DeleteCommand.Transaction = tran;
            
            int ret = m_DBProvider.Update(adapter, ds, tableName);
            if (tran==null) cn.Close();

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public  int UpdateDataTable(IDbDataAdapter adapter, System.Data.DataTable dt)
        {
            IDbTransaction tran;
            IDbConnection cn = DySmat.DBTransactionScope.GetConnection(_dbno, out tran);

            adapter.UpdateCommand.Connection = cn;
            adapter.UpdateCommand.Transaction = tran;
            adapter.InsertCommand.Connection = cn;
            adapter.InsertCommand.Transaction = tran;
            adapter.DeleteCommand.Connection = cn;
            adapter.DeleteCommand.Transaction = tran;

            int ret = m_DBProvider.Update(adapter, dt);
            if (tran == null) cn.Close();
            return ret;
        }
    }
}
