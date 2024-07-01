using SafeNeeds.DySmat.DB.Exception;
using SafeNeeds.DySmat.Model;
using System.Data;
using System.Data.OleDb;
using System;

namespace SafeNeeds.DySmat.DB
{


    /// <summary>
    /// 
    /// </summary>
    public partial class ProviderManager_OLEDB : ProviderManager, IProviderManager
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public ProviderManager_OLEDB(DBConfig config)
        {
            _Config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbDataAdapter DBAdapter()
        {

            return new OleDbDataAdapter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public IDbDataAdapter DBAdapter(string sql, IDbConnection connection)
        {
            return new OleDbDataAdapter(sql, ((OleDbConnection)(connection)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adpater"></param>
        public void Dispose(IDbDataAdapter adpater)
        {
            ((OleDbDataAdapter)adpater).Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbCommand DBCommand()
        {
            return new System.Data.OleDb.OleDbCommand();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbConnection DBConnection()
        {
            return new OleDbConnection(_Config.ConnectionString);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public IDbDataParameter DBParameter(DBParameter para)
        {
            //return para.ToParameter_OLEDB();

            OleDbParameter p = new OleDbParameter();
            if (para.SourceColumn == null)
            {
                p.ParameterName = para.ParameterName;
            }
            else
            {
                p.ParameterName = para.SourceColumn;
            }
            p.OleDbType = GetDBType_OLEDB(para.DataType);
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
            //return para.ToOriginalParameter_OLEDB();

            OleDbParameter p = new OleDbParameter();
            p.ParameterName = "Original_" + para.SourceColumn;
            p.OleDbType = GetDBType_OLEDB(para.DataType);
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
            ((OleDbDataAdapter)(adapter)).Fill(ds);
        }

        /// <summary>
        /// Fill
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        public void Fill(IDbDataAdapter adapter, DataTable ds)
        {
            ((OleDbDataAdapter)(adapter)).Fill(ds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        public void Fill(IDbDataAdapter adapter, DataSet ds, string tableName)
        {
            ((OleDbDataAdapter)(adapter)).Fill(ds, tableName);
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
        //       return Update_OLEDB(adapter, ds, tableName);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="adapter"></param>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public int Update(IDbDataAdapter adapter, DataTable dt)
        //{
        //            return Update_OLEDB(adapter, dt);
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
                return ((OleDbDataAdapter)(adapter)).Update(ds, tableName);
            }
            catch (OleDbException ex)
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
                else if (ex.ErrorCode == -2147467259)
                {
                    throw new UpdateException(ex.Message, UpdateException.DBErrorType.Other);
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
                return ((OleDbDataAdapter)(adapter)).Update(dt);
            }
            catch (OleDbException ex)
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
                else if (ex.ErrorCode == -2147467259)
                {
                    throw new UpdateException(ex.Message, UpdateException.DBErrorType.Other);
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

        private OleDbType GetDBType_OLEDB(EnumDbDataType DataType)
        {
            switch (DataType)
            {
                case EnumDbDataType.UNICODE:

                    return System.Data.OleDb.OleDbType.VarWChar;

                case EnumDbDataType.STRING:

                    return System.Data.OleDb.OleDbType.VarChar;

                case EnumDbDataType.INT32:

                    return System.Data.OleDb.OleDbType.Integer;

                case EnumDbDataType.BOOLEAN:

                    return System.Data.OleDb.OleDbType.Boolean;

                case EnumDbDataType.DATE:

                    return System.Data.OleDb.OleDbType.Date;

                case EnumDbDataType.FLOAT:

                    return System.Data.OleDb.OleDbType.Double;

                case EnumDbDataType.NUMBER:

                    return System.Data.OleDb.OleDbType.Decimal;

                case EnumDbDataType.LONGTEXT:

                    return System.Data.OleDb.OleDbType.LongVarChar;

                default:
                    throw new ApplicationException("type error");
            }
        }


        public string GetFieldsParameterSQL(string FieldName)
        {
            return FieldName + "=?";
        }


        public string GetFieldsParameterSQL_Original(string FieldName)
        {
            return FieldName + "=?";
        }


        public string GetFieldsParameterString(string FieldName)
        {
            return "?";
        }

    }
}