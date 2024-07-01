using System;

namespace SafeNeeds.DySmat.DB.Exception
{
    /// <summary>
    /// 更新時の排他例外クラス
    /// </summary>
    public class HaitaException : System.Exception
    {
        private string _EntityName = string.Empty;

        #region " コンストラクタ "

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg">メッセージ内容</param>
        public HaitaException(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg">メッセージ内容</param>
        /// <param name="EntityName">エンディディ名称</param>
        public HaitaException(string msg, string EntityName)
            : base(msg)
        {
            this.EntityName = EntityName;
        }

        #endregion

        #region " プロパティ "

        /// <summary>
        /// エンディディ名称
        /// </summary>
        public string EntityName
        {
            get
            {
                return _EntityName;
            }
            set
            {
                _EntityName = value;
            }
        }

        #endregion

        #region " メソッド "

        #endregion

    }
}
