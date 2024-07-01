using System;

using SafeNeeds.DySmat.DB.Util;


namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// �f�[�^�x�[�X��`�p�N���X
    /// </summary>
    public class DBConfig
    {
        private string _DbName;
        private string _ConnectionString;
        private EnumProviderType _Provider;
        private EnumDatabaseType _Database;
        private EnumAdapterType _Adapter;
        private SqlConfig _SqlConfig;
        private int _projID;

        public DBConfig() { }

        ///// <summary>
        ///// �R���X�g���N�^
        ///// </summary>
        ///// <param name="dbType">DB�̖��O</param>
        ///// <param name="provider">�v���o�C�_�[</param>
        ///// <param name="dataSource">�c�a�̎��</param>
        ///// <param name="dbname">�f�[�^�̃\�[�X</param>
        ///// <param name="userid">���[�U�h�c</param>
        ///// <param name="password">�p�X���[�h</param>
        //public DBConfig(EnumDatabaseType dbType, EnumProviderType provider, string dataSource, string dbname, string userid, string password)
        //{
        //    _Provider = provider;
        //    _Adapter = this.GetAdpater(Provider);
        //    _DbName = dbname;
        //    _Database = dbType;
        //    _ConnectionString = Common.GetConnnectionStr(Provider, dataSource, dbname, userid, password);
        //}

        public DBConfig(EnumDatabaseType dbType, EnumProviderType provider, string connString, int projID)
        {
            _Provider = provider;
            _Adapter = this.GetAdpater(provider);
            _Database = dbType;
            _ConnectionString = connString;
            _projID = projID;
          //  _ConnectionString = Common.GetConnnectionStr(Provider, dataSource, dbname, userid, password);
        }


        #region " �v���p�e�B "
        public int ProjID
        {
            get
            {
                return _projID;
            }
        }
        /// <summary>
        /// �v���o�C�_�[
        /// </summary>
        public EnumProviderType Provider
        {
            get
            {
                return _Provider;
            }
            set
            {
                _Provider = value;
            }
        }

        /// <summary>
        /// �f�[�^�x�[�X�̖��O
        /// </summary>
        public string DBName
        {
            get
            {
                return _DbName;
            }
            set
            {
                _DbName = value;
            }
        }

        /// <summary>
        /// �f�[�^�x�[�X�̎��
        /// </summary>
        public EnumDatabaseType Database
        {
            get
            {
                return _Database;
            }
            set
            {
                _Database = value;
            }
        }

        /// <summary>
        /// �A�_�v�^�[
        /// </summary>
        public EnumAdapterType Adapter
        {
            get
            {
                return _Adapter;
            }
            set
            {
                _Adapter = value;
            }
        }

        /// <summary>
        /// �ڑ�������
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        #endregion

        #region " ���\�b�h "

        /// <summary>
        /// SQLConfig�^�C�v�̕ϊ�
        /// </summary>
        /// <returns></returns>
        public SqlConfig GetSQLConfig()
        {
            if (_SqlConfig == null)
            {
                _SqlConfig = new SqlConfig(_Database, GetAdpater(_Provider));
            }

            return _SqlConfig;
        }

        /// <summary>
        /// �V���A���C�Y
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _DbName + _Database + _ConnectionString;
        }

        #endregion

        /// <summary>
        /// �A�_�v�^�[�̎擾
        /// </summary>
        /// <param name="provider">�v���o�C�_�[</param>
        /// <returns>�A�_�v�^�[</returns>
        private EnumAdapterType GetAdpater(EnumProviderType provider)
        {
            if (provider == EnumProviderType.MS_OLEDB_Jet ||
                provider == EnumProviderType.MS_OLEDB_ORACLE ||
                provider == EnumProviderType.MS_OLEDB_SQLSERVER ||
                provider == EnumProviderType.ORA_OLEDB_ORACLE)
            {
                return EnumAdapterType.OLEDB;
            }
            else if (provider == EnumProviderType.MS_SQL_SQLSERVER)
            {
                return EnumAdapterType.SQL;
            }
            else if (provider == EnumProviderType.ORA_ORACLE)
            {
                return EnumAdapterType.ORACLE;
            }
            else if (provider == EnumProviderType.ORA_ODP)
            {
                return EnumAdapterType.ODP;
            }
            else
            {
                throw new ApplicationException("not provided");
            }
        }
    }
}
