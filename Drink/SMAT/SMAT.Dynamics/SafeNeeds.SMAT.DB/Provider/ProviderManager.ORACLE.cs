using SafeNeeds.DySmat.DB.Exception;
using SafeNeeds.DySmat.Model;
using System.Data;
using System.Data.OracleClient;
using System;

namespace SafeNeeds.DySmat.DB
{

    /// <summary>
    /// 
    /// </summary>
    public partial class ProviderManager_ORACLE : ProviderManager, IProviderManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public ProviderManager_ORACLE(DBConfig config)
        {
            _Config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbDataAdapter DBAdapter()
        {

            return new OracleDataAdapter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public IDbDataAdapter DBAdapter(string sql, IDbConnection connection)
        {
            return new System.Data.OracleClient.OracleDataAdapter(sql, ((OracleConnection)(connection)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adpater"></param>
        public void Dispose(IDbDataAdapter adpater)
        {
            ((OracleDataAdapter)adpater).Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbCommand DBCommand()
        {
            return new System.Data.OracleClient.OracleCommand();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbConnection DBConnection()
        {
            return new OracleConnection(_Config.ConnectionString);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public IDbDataParameter DBParameter(DBParameter para)
        {
            //return para.ToParameter_ORACLE();

            OracleParameter p = new OracleParameter();
            if (para.SourceColumn == null)
            {
                p.ParameterName = para.ParameterName;
            }
            else
            {
                p.ParameterName = ":" + para.SourceColumn;
            }
            p.OracleType = GetOracleDataType(para.DataType);
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

            //return para.ToOriginalParameter_ORACLE();

            OracleParameter p = new OracleParameter();
            p.ParameterName = ":O_" + para.SourceColumn;
            p.OracleType = GetOracleDataType(para.DataType);
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
            ((OracleDataAdapter)(adapter)).Fill(ds);
        }

        /// <summary>
        /// Fill
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        public void Fill(IDbDataAdapter adapter, DataTable ds)
        {
            ((OracleDataAdapter)(adapter)).Fill(ds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        public void Fill(IDbDataAdapter adapter, DataSet ds, string tableName)
        {
            ((OracleDataAdapter)(adapter)).Fill(ds, tableName);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="adapter"></param>
        ///// <param name="ds"></param>
        ///// <param name="tableName"></param>
        ///// <returns></returns>
        //public int Update(IDbDataAdapter adapter, DataSet ds, string tableName)
        //{
        //            return Update_ORACLE(adapter, ds, tableName);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="adapter"></param>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public int Update(IDbDataAdapter adapter, DataTable dt)
        //{
        //            return Update_ORACLE(adapter, dt);
        //}

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
                return ((OracleDataAdapter)(adapter)).Update(ds, tableName);
            }
            catch (System.Data.OracleClient.OracleException ex)
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
                return ((OracleDataAdapter)(adapter)).Update(dt);
            }
            catch (System.Data.OracleClient.OracleException ex)
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


        private OracleType GetOracleDataType(EnumDbDataType DataType)
        {
            switch (DataType)
            {
                case EnumDbDataType.STRING:
                    return OracleType.VarChar;

                case EnumDbDataType.UNICODE:
                    return OracleType.NVarChar;

                case EnumDbDataType.INT32:
                    return OracleType.Int32;

                case EnumDbDataType.BOOLEAN:
                    return OracleType.Number;

                case EnumDbDataType.DATE:
                    return OracleType.DateTime;

                case EnumDbDataType.FLOAT:
                    return OracleType.Double;

                case EnumDbDataType.NUMBER:
                    return OracleType.Number;

                case EnumDbDataType.LONGTEXT:
                    return OracleType.LongVarChar;

                case EnumDbDataType.BLOB:
                    return OracleType.Blob;

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
