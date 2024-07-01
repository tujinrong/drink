using System;

namespace SafeNeeds.DySmat.DB.Exception
{
    /// <summary>
    /// �X�V���̗�O�N���X
    /// </summary>
    public class UpdateException : System.Exception
    {
        private DBErrorType _ErrorType = DBErrorType.Other;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="msg">���b�Z�[�W���e</param>
        public UpdateException(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="msg">���b�Z�[�W���e</param>
        /// <param name="errorType">�G���[���</param>
        public UpdateException(string msg, DBErrorType errorType)
            : base(msg)
        {
            this.ErrorType = errorType;
        }
        
        /// <summary>
        /// �G���[��ʂ̗񋓌^
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

        #region " �v���p�e�B "

        /// <summary>
        /// �G���[���
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

        #region " ���\�b�h "
        #endregion
    }
}
