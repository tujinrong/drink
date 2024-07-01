using System;

namespace SafeNeeds.DySmat.DB.Exception
{
    /// <summary>
    /// �X�V���̔r����O�N���X
    /// </summary>
    public class HaitaException : System.Exception
    {
        private string _EntityName = string.Empty;

        #region " �R���X�g���N�^ "

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="msg">���b�Z�[�W���e</param>
        public HaitaException(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="msg">���b�Z�[�W���e</param>
        /// <param name="EntityName">�G���f�B�f�B����</param>
        public HaitaException(string msg, string EntityName)
            : base(msg)
        {
            this.EntityName = EntityName;
        }

        #endregion

        #region " �v���p�e�B "

        /// <summary>
        /// �G���f�B�f�B����
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

        #region " ���\�b�h "

        #endregion

    }
}
