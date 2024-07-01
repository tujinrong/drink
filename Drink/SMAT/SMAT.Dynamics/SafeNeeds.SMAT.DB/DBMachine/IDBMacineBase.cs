using System;
using System.Data;

namespace SafeNeeds.SMAT.DB
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDBMachineBase
    {

        //IDbDataAdapter Adapter
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 
        /// </summary>
        ProviderManager Provider
        {
            get;
            set;
        }
    }
}
