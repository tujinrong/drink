using System;

using SafeNeeds.DySmat.DB.Util;


namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// データベース定義用クラス
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
        ///// コンストラクタ
        ///// </summary>
        ///// <param name="dbType">DBの名前</param>
        ///// <param name="provider">プロバイダー</param>
        ///// <param name="dataSource">ＤＢの種別</param>
        ///// <param name="dbname">データのソース</param>
        ///// <param name="userid">ユーザＩＤ</param>
        ///// <param name="password">パスワード</param>
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


        #region " プロパティ "
        public int ProjID
        {
            get
            {
                return _projID;
            }
        }
        /// <summary>
        /// プロバイダー
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
        /// データベースの名前
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
        /// データベースの種別
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
        /// アダプター
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
        /// 接続文字列
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

        #region " メソッド "

        /// <summary>
        /// SQLConfigタイプの変換
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
        /// シリアライズ
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _DbName + _Database + _ConnectionString;
        }

        #endregion

        /// <summary>
        /// アダプターの取得
        /// </summary>
        /// <param name="provider">プロバイダー</param>
        /// <returns>アダプター</returns>
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
