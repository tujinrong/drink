using System;
using System.Data;
using System.Text;
//using SafeNeeds.SMAT.Core.Define;


namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// ÉOÉçÅ[ÉoÉã
    /// </summary>
    public class DBGlobal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public delegate void DelegateLog(string msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
//        public delegate SafeNeeds.SMAT.DB.TableEngine DelegateTableEngine(DBConfig dbConfig, string tableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        //public delegate TableDef DelegateTableDef(string tableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public delegate DataSet DelegateDataSet(string tableName);
        
        /// <summary>
        /// 
        /// </summary>
        public static DelegateLog LogDelegate;
        
        /// <summary>
        /// 
        /// </summary>
        public static DelegateLog DataDelegate;

        /// <summary>
        /// 
        /// </summary>
      //  public static DelegateTableEngine TableEngineDelegate;

        /// <summary>
        /// 
        /// </summary>
        //public static DelegateTableDef TableDefDelegate;

        /// <summary>
        /// 
        /// </summary>
        public static DelegateDataSet DataSetDelegate;


        public static void WriteLog(string msg)
        {
            if (LogDelegate != null)
            {
                DBGlobal.LogDelegate(msg);
            }

        }
    }
}
