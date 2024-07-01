using System;
using System.Text;

namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// NNDB�I�v�V�����N���X
    /// </summary>
    public class DBOption
    {
        /// <summary>
        /// SQL����s����
        /// </summary>
        public static string SqlCrLf = " ";

        /// <summary>
        /// SqlServer2005���ǂ���
        /// </summary>
        public static bool SqlServer2005 = true;
        
        /// <summary>
        /// ������DB���ǂ���
        /// </summary>
        public static bool MultiDatabase = false;

        /// <summary>
        /// �ڑ����@
        /// </summary>
        public static EnmConnectionType Connection = EnmConnectionType.NoPooling;

        /// <summary>
        /// 
        /// </summary>
        public static CUpdateControlTable UpdateControlTable = new CUpdateControlTable();

        /// <summary>
        /// �R���g���[���̍X�V�p�N���X
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
