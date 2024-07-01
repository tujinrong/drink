using System;


namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// �e�[�u���\����`�N���X�̂b�����������N���X
    /// </summary>
    public class SqlConfig
    {
        private EnumAdapterType _Adapter;
        private EnumDatabaseType _DbType;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="dbType">�f�[�^�x�[�X���</param>
        /// <param name="adapter">�A�_�v�^�[</param>
        public SqlConfig(EnumDatabaseType dbType, EnumAdapterType adapter)
        {
            _DbType = dbType;
            _Adapter = adapter;
        }

        /// <summary>
        /// �A�_�v�^�[
        /// </summary>
        public EnumAdapterType Adapter
        {
            get
            {
                return this._Adapter;
            }
            set
            {
                this._Adapter = value;
            }
        }

        /// <summary>
        /// �f�[�^�x�[�X���
        /// </summary>
        public EnumDatabaseType Database
        {
            get
            {
                return _DbType;
            }
            set
            {
                _DbType = value;
            }
        }
    }
}
