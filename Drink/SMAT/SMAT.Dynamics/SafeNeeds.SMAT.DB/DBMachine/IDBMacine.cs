using System;
using System.Data;


namespace SafeNeeds.SMAT.DB
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDBMachine : IDBMachineBase
    {
        /// <summary>
        /// 
        /// </summary>
        int CommandTimeout
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int RunSQL(string sql);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        int RunSP(string sql, params DBParameter[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int UpdateDataSet(IDbDataAdapter adapter, DataSet ds, string tableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        int UpdateDataTable(IDbDataAdapter adapter, DataTable dt);

        /// <summary>
        /// 
        /// </summary>
        void Close();

        /// <summary>
        /// 
        /// </summary>
        //void EndTrans();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strSQL"></param>
        /// <param name="tableName"></param>
        void FillDataSetBySQL(DataSet ds, string strSQL, string tableName);

        void FillDataTableBySQL(ref DataTable dt, string strSQL);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strSP"></param>
        /// <param name="strTableName"></param>
        /// <param name="param"></param>
        void FillDataSetBySP(DataSet ds, string strSP, string strTableName, params DBParameter[] param);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="trans"></param>
        //void StartTrans(DBTransaction trans);
    }
}
