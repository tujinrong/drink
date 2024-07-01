using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.OracleClient;

using SafeNeeds.DySmat.DB.Util;


using SafeNeeds.DySmat.Model;

namespace SafeNeeds.DySmat.DB
{


    public interface IProviderManager
    {
        IDbDataAdapter DBAdapter();

        IDbDataAdapter DBAdapter(string sql, IDbConnection connection);

        IDbCommand DBCommand();

        IDbConnection DBConnection();

        IDbDataParameter DBParameter(DBParameter para);
        IDbDataParameter DBParameter(Y_EntityField field);
        IDbDataParameter DBOriginalParameter(Y_EntityField fldDef);

        void Dispose(IDbDataAdapter adpater);

        void Fill(IDbDataAdapter adapter, DataSet ds);
        void Fill(IDbDataAdapter adapter, DataTable ds);
        void Fill(IDbDataAdapter adapter, DataSet ds, string tableName);

        int Update(IDbDataAdapter adapter, DataSet ds, string tableName);
        int Update(IDbDataAdapter adapter, DataTable dt);

        //IDbDataParameter DBParameter(DBParameter para);

        string GetFieldsParameterSQL(string FieldName);
        string GetFieldsParameterSQL_Original(string FieldName);
        string GetFieldsParameterString(string FieldName);

    }



    public class ProviderFactory
    {
        public static IProviderManager GetProvider(DBConfig config)
        {
            switch (config.Adapter)
            {
                case EnumAdapterType.SQL:
                    return new ProviderManager_SQL(config);

                case EnumAdapterType.OLEDB:
                    return new ProviderManager_OLEDB(config);

                case EnumAdapterType.ORACLE:
                    return new ProviderManager_ORACLE(config);

                case EnumAdapterType.ODBC:
                    return new ProviderManager_ODBC(config);

                case EnumAdapterType.ODP:
                    return new ProviderManager_ODP(config);

                default:
                    throw new ApplicationException("");
            }

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class ProviderManager 
    {
        protected DBConfig _Config;

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="db"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        //public string GetConnnectionStr(string source, string db, string uid, string pwd)
        //{
        //    return Common.GetConnnectionStr(_Config.Provider, source, db, uid, pwd);
        //}
    }
}
