using SafeNeeds.DySmat.DB.Exception;
using SafeNeeds.DySmat.Model;
using System.Data;
using System.Data.Odbc;
using System;

namespace SafeNeeds.DySmat.DB
{


    /// <summary>
    /// 
    /// </summary>
    public partial class ProviderManager_ODBC : ProviderManager, IProviderManager
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public ProviderManager_ODBC(DBConfig config)
        {
            _Config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbDataAdapter DBAdapter()
        {
            return new OdbcDataAdapter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public IDbDataAdapter DBAdapter(string sql, IDbConnection connection)
        {
            return new OdbcDataAdapter(sql, ((OdbcConnection)(connection)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adpater"></param>
        public void Dispose(IDbDataAdapter adpater)
        {
            ((OdbcDataAdapter)adpater).Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbCommand DBCommand()
        {
            return new System.Data.Odbc.OdbcCommand();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbConnection DBConnection()
        {
            return new OdbcConnection(_Config.ConnectionString);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public IDbDataParameter DBParameter(DBParameter para)
        {
            //return para.ToParameter_ODBC();

            OdbcParameter p = new OdbcParameter();
            if (para.SourceColumn == null)
            {
                p.ParameterName = para.ParameterName;
            }
            else
            {
                p.ParameterName = "@" + para.SourceColumn;
            }
            p.OdbcType = GetDataType_ODBC(para.DataType);
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

            OdbcParameter p = new OdbcParameter();
            if (para.SourceColumn == null)
            {
                p.ParameterName = para.ParameterName;
            }
            else
            {
                p.ParameterName = "@" + para.SourceColumn;
            }
            p.OdbcType = GetDataType_ODBC(para.DataType);
            p.Size = para.Size;
            p.SourceColumn = para.SourceColumn;
            p.Value = para.Value;
            return p;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fldDef"></param>
        /// <returns></returns>
        public IDbDataParameter DBOriginalParameter(Y_EntityField fldDef)
        {
            DBParameter para = new DBParameter(fldDef);

            OdbcParameter p = new OdbcParameter();
            p.ParameterName = "@Original_" + para.SourceColumn;
            p.OdbcType = this.GetDataType_ODBC(para.DataType);
            p.Size = para.Size;
            p.SourceColumn = para.SourceColumn;
            p.Direction = ParameterDirection.Input; // this._Direction;
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
            ((OdbcDataAdapter)(adapter)).Fill(ds);
        }

        /// <summary>
        /// Fill
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        public void Fill(IDbDataAdapter adapter, DataTable ds)
        {
            ((OdbcDataAdapter)(adapter)).Fill(ds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        public void Fill(IDbDataAdapter adapter, DataSet ds, string tableName)
        {
            ((OdbcDataAdapter)(adapter)).Fill(ds, tableName);
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
        //      return Update_ODBC(adapter, ds, tableName);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="adapter"></param>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public int Update(IDbDataAdapter adapter, DataTable dt)
        //{
        //     return Update_ODBC(adapter, dt);
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
                return ((OdbcDataAdapter)(adapter)).Update(ds, tableName);
            }
            catch (OdbcException ex)
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
                return ((OdbcDataAdapter)(adapter)).Update(dt);
            }
            catch (OdbcException ex)
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
        /// <returns></returns>
        private OdbcType GetDataType_ODBC(EnumDbDataType DataType)
        {
            switch (DataType)
            {
                case EnumDbDataType.STRING:
                    return OdbcType.VarChar;

                case EnumDbDataType.UNICODE:
                    return OdbcType.NVarChar;

                case EnumDbDataType.BOOLEAN:
                    return OdbcType.Bit;

                case EnumDbDataType.DATE:
                    return OdbcType.DateTime;

                case EnumDbDataType.FLOAT:
                    return OdbcType.Double;

                case EnumDbDataType.NUMBER:
                    return OdbcType.Decimal;

                case EnumDbDataType.LONGTEXT:
                    return OdbcType.Text;

                default:
                    throw new ApplicationException("");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_fields"></param>
        /// <param name="p_adapter"></param>
        /// <param name="p_sJoin"></param>
        /// <returns></returns>
        public string GetFieldsParameterSQL(string FieldName)
        {
            return FieldName + "=?";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_fields"></param>
        /// <param name="p_adapter"></param>
        /// <param name="p_sJoin"></param>
        /// <returns></returns>
        public string GetFieldsParameterSQL_Original(string FieldName)
        {
            return FieldName + "=?";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="p_adapter"></param>
        /// <param name="p_sJoin"></param>
        /// <returns></returns>
        public string GetFieldsParameterString(string FieldName)
        {
            return "?";
        }

    }
}
