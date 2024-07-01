using System;

namespace SafeNeeds.DySmat.DB.Exception
{
    /// <summary>
    /// 更新時の例外クラス
    /// </summary>
    public class UpdateException : System.Exception
    {
        private DBErrorType _ErrorType = DBErrorType.Other;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg">メッセージ内容</param>
        public UpdateException(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg">メッセージ内容</param>
        /// <param name="errorType">エラー種別</param>
        public UpdateException(string msg, DBErrorType errorType)
            : base(msg)
        {
            this.ErrorType = errorType;
        }
        
        /// <summary>
        /// エラー種別の列挙型
        /// </summary>
        public enum DBErrorType
        {
            /// <summary>
            /// 
            /// </summary>
            Duplicate,

            /// <summary>
            /// 
            /// </summary>
            ValueRequired,

            /// <summary>
            /// 
            /// </summary>
            Concurrency,

            /// <summary>
            /// 
            /// </summary>
            Other
        }

        #region " プロパティ "

        /// <summary>
        /// エラー種別
        /// </summary>
        public DBErrorType ErrorType
        {
            get
            {
                return _ErrorType;
            }
            set
            {
                _ErrorType = value;
            }
        }

        #endregion

        #region " メソッド "
        #endregion
    }
}
