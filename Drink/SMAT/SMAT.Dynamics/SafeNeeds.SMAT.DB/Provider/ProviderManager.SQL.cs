using SafeNeeds.DySmat.DB.Exception;
using SafeNeeds.DySmat.Model;
using System.Data;
using System.Data.SqlClient;
using System;

namespace SafeNeeds.DySmat.DB
{

    /// <summary>
    /// 
    /// </summary>
    public partial class ProviderManager_SQL : ProviderManager, IProviderManager
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public ProviderManager_SQL(DBConfig config)
        {
            _Config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbDataAdapter DBAdapter()
        {
            return new SqlDataAdapter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public IDbDataAdapter DBAdapter(string sql, IDbConnection connection)
        {
            return new SqlDataAdapter(sql, ((SqlConnection)(connection)));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adpater"></param>
        public void Dispose(IDbDataAdapter adpater)
        {
            ((SqlDataAdapter)adpater).Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbCommand DBCommand()
        {
            return new System.Data.SqlClient.SqlCommand();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbConnection DBConnection()
        {
            return new SqlConnection(_Config.ConnectionString);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public IDbDataParameter DBParameter(DBParameter para)
        {
            //return para.ToParameter_SQL();

            SqlParameter p = new SqlParameter();
            if (para.SourceColumn == null)
            {
                p.ParameterName = para.ParameterName;
            }
            else
            {
                p.ParameterName = "@" + para.SourceColumn;
            }
            p.SqlDbType = GetDataType_SQL(para.DataType);
            p.Size = para.Size;
            p.SourceColumn = para.SourceColumn;
            p.Direction = para.Direction;
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
            //return para.ToOriginalParameter_SQL();

            SqlParameter p = new SqlParameter();
            p.ParameterName = "@Original_" + para.SourceColumn;
            p.SqlDbType = GetDataType_SQL(para.DataType);
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
            ((SqlDataAdapter)(adapter)).Fill(ds);
        }

        /// <summary>
        /// Fill
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        public void Fill(IDbDataAdapter adapter, DataTable ds)
        {
            ((SqlDataAdapter)(adapter)).Fill(ds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        public void Fill(IDbDataAdapter adapter, DataSet ds, string tableName)
        {
            ((SqlDataAdapter)(adapter)).Fill(ds, tableName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        //public int Update(IDbDataAdapter adapter, DataSet ds, string tableName)
        //{
        //            return Update_SQL(adapter, ds, tableName);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="adapter"></param>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public int Update(IDbDataAdapter adapter, DataTable dt)
        //{
        //            return Update_SQL(adapter, dt);
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
                return ((SqlDataAdapter)(adapter)).Update(ds, tableName);
            }
            catch (System.Data.SqlClient.SqlException ex)
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
                return ((SqlDataAdapter)(adapter)).Update(dt);
            }
            catch (System.Data.SqlClient.SqlException ex)
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


        private SqlDbType GetDataType_SQL(EnumDbDataType DataType)
        {
            switch (DataType)
            {
                case EnumDbDataType.UNICODE:
                    return SqlDbType.NVarChar;

                case EnumDbDataType.STRING:
                    return SqlDbType.VarChar;

                case EnumDbDataType.NUMBER:
                    return SqlDbType.Decimal;

                case EnumDbDataType.INT64:
                    return SqlDbType.BigInt;

                case EnumDbDataType.INT32:
                    return SqlDbType.Int;

                case EnumDbDataType.INT16:
                    return SqlDbType.SmallInt;

                case EnumDbDataType.INT8:
                    return SqlDbType.TinyInt;

                case EnumDbDataType.BOOLEAN:
                    return SqlDbType.Bit;

                case EnumDbDataType.DATE:
                    return SqlDbType.DateTime;

                case EnumDbDataType.FLOAT:
                    return SqlDbType.Float;

                case EnumDbDataType.LONGTEXT:
                    return SqlDbType.Text;

                default:
                    throw new ApplicationException("type error");
            }
        }


        public string GetFieldsParameterSQL(string FieldName)
        {
            return FieldName + "=@" + FieldName;
        }

        public string GetFieldsParameterSQL_Original(string FieldName)
        {
            return FieldName + "=@Original_" + FieldName;
        }


        public string GetFieldsParameterString(string FieldName)
        {
            return "@" + FieldName;
        }

    }
}
