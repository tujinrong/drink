using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SafeNeeds.DySmat.DB.Exception; 

namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// 
    /// </summary>
    public class DMConnectionBase //: IDBMachineBase
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //protected IDbDataAdapter adapter;

        /// <summary>
        /// 
        /// </summary>
        protected IProviderManager m_DBProvider;

        /// <summary>
        /// 
        /// </summary>
        protected DBConfig m_config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public DMConnectionBase(DBConfig config)
        {
            this.m_config = config;
            //m_DBProvider = new ProviderManager(config);
            m_DBProvider = ProviderFactory.GetProvider(config);
        }


        //public IDbDataAdapter Adapter
        //{
        //    get
        //    {
        //        return this.adapter;
        //    }
        //    set
        //    {
        //        this.adapter = value;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        public IProviderManager Provider
        {
            get
            {
                return m_DBProvider;
            }
            set
            {
                m_DBProvider = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(IDbCommand dc)
        {
            int ret = -1;

            if (this.m_config.Adapter == EnumAdapterType.SQL)
            {
                try
                {
                    ret = dc.ExecuteNonQuery();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    if (ex.Message.IndexOf("d•¡") > 0)
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
            else if (this.m_config.Adapter == EnumAdapterType.OLEDB)
            {
                try
                {
                    ret = dc.ExecuteNonQuery();
                }
                catch (System.Data.OleDb.OleDbException ex)
                {
                    if (ex.Message.IndexOf("d•¡") > 0)
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
            else if (this.m_config.Adapter == EnumAdapterType.ORACLE || this.m_config.Adapter == EnumAdapterType.ODP)
            {
                try
                {
                    ret = dc.ExecuteNonQuery();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    if (ex.Message.IndexOf("d•¡") > 0)
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
            return ret;
        }
    }
}
