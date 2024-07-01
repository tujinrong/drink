using System;

namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// SQL���̕����񑀍�N���X
    /// </summary>
    public class SqlString
    {
        private EnumDatabaseType _DbType;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="dbType">�f�[�^�x�[�X�^�C�v</param>
        public SqlString(EnumDatabaseType dbType)
        {
            _DbType = dbType;
        }

        /// <summary>
        /// SQL���̃T�u��������擾����
        /// </summary>
        /// <param name="itemName">���ږ�</param>
        /// <param name="start">�J�n�ʒu</param>
        /// <param name="len">������</param>
        /// <returns>������</returns>
        public string SubString(string itemName, int start, int len)
        {
            string sRet = string.Empty;

            if (_DbType == DB.EnumDatabaseType.ACCESS)
            {
                sRet = "mid(" + itemName + "," + start + 1 + "," + len + ")";
            }
            else if (_DbType == DB.EnumDatabaseType.SQLSERVER)
            {
                sRet = "SubString(" + itemName + "," + start + 1 + "," + len + ")";
            }
            else if (_DbType == DB.EnumDatabaseType.ORACLE)
            {
                sRet = "subStr(" + itemName + "," + start + 1 + "," + len + ")";
            }

            return sRet;
        }
    }
}
