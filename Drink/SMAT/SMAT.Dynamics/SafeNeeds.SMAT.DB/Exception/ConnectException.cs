using System;


namespace SafeNeeds.DySmat.DB.Exception
{
    /// <summary>
    /// 接続例外クラス
    /// </summary>
    public class ConnectException : System.Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public ConnectException(string msg) : base(msg)
        {

        }
    }
}
