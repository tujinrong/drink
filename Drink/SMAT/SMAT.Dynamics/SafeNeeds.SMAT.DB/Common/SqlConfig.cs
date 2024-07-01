using System;


namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// テーブル構造定義クラスのＣｏｎｆｉｇクラス
    /// </summary>
    public class SqlConfig
    {
        private EnumAdapterType _Adapter;
        private EnumDatabaseType _DbType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbType">データベース種別</param>
        /// <param name="adapter">アダプター</param>
        public SqlConfig(EnumDatabaseType dbType, EnumAdapterType adapter)
        {
            _DbType = dbType;
            _Adapter = adapter;
        }

        /// <summary>
        /// アダプター
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
        /// データベース種類
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
