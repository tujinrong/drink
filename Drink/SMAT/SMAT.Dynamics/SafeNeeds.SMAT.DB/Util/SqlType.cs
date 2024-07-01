using System;

namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// SQL���̌^�ϊ��N���X
    /// </summary>
    public class SqlType
    {
        private EnumDatabaseType _DbType;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="dbType">�f�[�^�x�[�X�^�C�v</param>
        public SqlType(EnumDatabaseType dbType)
        {
            _DbType = dbType;
        }

        /// <summary>
        /// Varchar�^�ɕϊ�����
        /// </summary>
        /// <param name="itemName">���ږ�</param>
        /// <returns>�^�ϊ����ꂽ�l</returns>
        public string ToVarchar(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "CSTR(" + itemName + ")";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "CAST(" + itemName + " AS Varchar)";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_Char(" + itemName + ")";
            }

            return sRet;
        }

        /// <summary>
        /// Int�^�ɕϊ�����
        /// </summary>
        /// <param name="itemName">���ږ�</param>
        /// <returns>�^�ϊ����ꂽ�l</returns>
        public string ToInt(string itemName)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "cint(" + itemName + ")";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "CAST(" + itemName + " AS INT)";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_number(" + itemName + ")";
            }

            return sRet;
        }

        /// <summary>
        /// SQL�Ŏg�p����^�ɕϊ�����
        /// </summary>
        /// <param name="date">���t</param>
        /// <returns>�^�ϊ����ꂽ�l</returns>
        public string ToSQLDate(string date)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "#" + date + "#";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = "'" + date + "'";
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "to_date('" + date + "','yyyy/mm/dd')";
            }

            return sRet;
        }

        /// <summary>
        /// �����̐擪�Ɂu0�v�𖄂߂�
        /// </summary>
        /// <param name="itemName">���ږ�</param>
        /// <param name="digit">�u0�v�̌�</param>
        /// <returns>�ϊ����ꂽ�l</returns>
        public string FillZero(string itemName, int digit)
        {
            string sRet = string.Empty;

            if (_DbType == EnumDatabaseType.ACCESS)
            {
                sRet = "Format(" + itemName + ",'" + new string('0', digit) + "')";
            }
            else if (_DbType == EnumDatabaseType.SQLSERVER)
            {
                sRet = itemName;
            }
            else if (_DbType == EnumDatabaseType.ORACLE)
            {
                sRet = "LPAD(" + itemName + "," + digit + ",'0')";
            }

            return sRet;
        }
    }
}
