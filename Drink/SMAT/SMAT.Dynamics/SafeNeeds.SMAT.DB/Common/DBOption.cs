using System;
using System.Text;

namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// NNDBオプションクラス
    /// </summary>
    public class DBOption
    {
        /// <summary>
        /// SQL上改行文字
        /// </summary>
        public static string SqlCrLf = " ";

        /// <summary>
        /// SqlServer2005かどうか
        /// </summary>
        public static bool SqlServer2005 = true;
        
        /// <summary>
        /// 複数のDBかどうか
        /// </summary>
        public static bool MultiDatabase = false;

        /// <summary>
        /// 接続方法
        /// </summary>
        public static EnmConnectionType Connection = EnmConnectionType.NoPooling;

        /// <summary>
        /// 
        /// </summary>
        public static CUpdateControlTable UpdateControlTable = new CUpdateControlTable();

        /// <summary>
        /// コントロールの更新用クラス
        /// </summary>
        public class CUpdateControlTable
        {
            /// <summary>
            /// 
            /// </summary>
           // public static TableEngine TableEngine;

            /// <summary>
            /// 
            /// </summary>
            public static string TimeFieldName;

            /// <summary>
            /// 
            /// </summary>
            public static string TableFieldName;
        }
    }
}
