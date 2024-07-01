using SafeNeeds.DySmat.DB.Exception;
using SafeNeeds.DySmat.Model;
using System.Data;
using System;

namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProviderManager_ODP : ProviderManager, IProviderManager
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public ProviderManager_ODP(DBConfig config)
        {
            _Config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbDataAdapter DBAdapter()
        {

            return new Oracle.ManagedDataAccess.Client.OracleDataAdapter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public IDbDataAdapter DBAdapter(string sql, IDbConnection connection)
        {
            return new Oracle.ManagedDataAccess.Client.OracleDataAdapter(sql, ((Oracle.ManagedDataAccess.Client.OracleConnection)(connection)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adpater"></param>
        public void Dispose(IDbDataAdapter adpater)
        {
            ((Oracle.ManagedDataAccess.Client.OracleDataAdapter)adpater).Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbCommand DBCommand()
        {
            return new Oracle.ManagedDataAccess.Client.OracleCommand();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbConnection DBConnection()
        {
            return new Oracle.ManagedDataAccess.Client.OracleConnection(_Config.ConnectionString);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public IDbDataParameter DBParameter(DBParameter para)
        {

            Oracle.ManagedDataAccess.Client.OracleParameter p = new Oracle.ManagedDataAccess.Client.OracleParameter();
            if (para.SourceColumn == null)
            {
                p.ParameterName = para.ParameterName;
            }
            else
            {
                p.ParameterName = ":" + para.SourceColumn;
            }
            p.OracleDbType = GetDataType_ODP(para.DataType);
            p.Direction = para.Direction;
            p.Size = para.Size;
            p.SourceColumn = para.SourceColumn;
            p.Value = para.Value;
            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public IDbDataParameter DBParameter(Y_EntityField field)
        {
            DBParameter para = new DBParameter(field);

            return DBParameter(para);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fldDef"></param>
        /// <returns></returns>
        public IDbDataParameter DBOriginalParameter(Y_EntityField fldDef)
        {
            DBParameter para = new DBParameter(fldDef);

            Oracle.ManagedDataAccess.Client.OracleParameter p = new Oracle.ManagedDataAccess.Client.OracleParameter();
            p.ParameterName = ":O_" + para.SourceColumn;
            p.OracleDbType = GetDataType_ODP(para.DataType);
            p.Size = para.Size;
            p.SourceColumn = para.SourceColumn;
            p.Direction = para.Direction;
            p.SourceVersion = System.Data.DataRowVersion.Original;
            p.Value = para.Value;
            return p;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        public void Fill(IDbDataAdapter adapter, DataSet ds)
        {
            ((Oracle.ManagedDataAccess.Client.OracleDataAdapter)(adapter)).Fill(ds);
        }

        /// <summary>
        /// Fill
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        public void Fill(IDbDataAdapter adapter, DataTable ds)
        {
            ((Oracle.ManagedDataAccess.Client.OracleDataAdapter)(adapter)).Fill(ds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        public void Fill(IDbDataAdapter adapter, DataSet ds, string tableName)
        {
            ((Oracle.ManagedDataAccess.Client.OracleDataAdapter)(adapter)).Fill(ds, tableName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int Update(IDbDataAdapter adapter, DataSet ds, string tableName)
        {
            try
            {
                return ((Oracle.ManagedDataAccess.Client.OracleDataAdapter)adapter).Update(ds, tableName);
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException ex)
            {
                if (ex.Message.IndexOf("d•¡") > 0 ||
                    ex.Message.IndexOf("ˆêˆÓ§–ñ") > 0 ||
                    ex.Message.IndexOf("constraint") > 0)
                {
                    throw new UpdateException(ex.Message, UpdateException.DBErrorType.Duplicate);
                }
                else if (ex.Message.ToUpper().IndexOf("NULL") > 0)
                {
                    throw new UpdateException(ex.Message, UpdateException.DBErrorType.ValueRequired);
                }
                else
                {
                    throw ex;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int Update(IDbDataAdapter adapter, DataTable dt)
        {

            try
            {
                return ((Oracle.ManagedDataAccess.Client.OracleDataAdapter)(adapter)).Update(dt);
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException ex)
            {
                if (ex.Message.IndexOf("d•¡") > 0 ||
                    ex.Message.IndexOf("ˆêˆÓ§–ñ") > 0 ||
                    ex.Message.IndexOf("constraint") > 0)
                {
                    throw new UpdateException(ex.Message, UpdateException.DBErrorType.Duplicate);
                }
                else if (ex.Message.ToUpper().IndexOf("NULL") > 0)
                {
                    throw new UpdateException(ex.Message, UpdateException.DBErrorType.ValueRequired);
                }
                else
                {
                    throw ex;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }


        }


        private Oracle.ManagedDataAccess.Client.OracleDbType GetDataType_ODP(EnumDbDataType DataType)
        {
            switch (DataType)
            {
                case EnumDbDataType.STRING:
                    return Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2;

                case EnumDbDataType.UNICODE:
                    return Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2;

                case EnumDbDataType.INT32:
                    return Oracle.ManagedDataAccess.Client.OracleDbType.Int32;

                case EnumDbDataType.BOOLEAN:
                    return Oracle.ManagedDataAccess.Client.OracleDbType.Decimal;

                case EnumDbDataType.DATE:
                    return Oracle.ManagedDataAccess.Client.OracleDbType.Date;

                case EnumDbDataType.FLOAT:
                    return Oracle.ManagedDataAccess.Client.OracleDbType.BinaryDouble;

                case EnumDbDataType.NUMBER:
                    return Oracle.ManagedDataAccess.Client.OracleDbType.Decimal;

                case EnumDbDataType.LONGTEXT:
                    return Oracle.ManagedDataAccess.Client.OracleDbType.Long;

                case EnumDbDataType.BLOB:
                    return Oracle.ManagedDataAccess.Client.OracleDbType.Blob;

                default:
                    throw new ApplicationException("TYPE ERROR");

            }
        }

        public string GetFieldsParameterSQL(string FieldName)
        {
            return FieldName + "=:" + FieldName;
        }

        public string GetFieldsParameterSQL_Original(string FieldName)
        {
            return FieldName + "=:O_" + FieldName;
        }

        public string GetFieldsParameterString(string FieldName)
        {
            return ":" + FieldName;
        }

    }
}
