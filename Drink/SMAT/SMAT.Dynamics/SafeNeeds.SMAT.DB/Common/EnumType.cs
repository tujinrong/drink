using System;
using System.Text;

namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// データベース種別
    /// </summary>
    public enum EnumDatabaseType : byte
    {
        /// <summary>
        /// データベースがSql Serverの場合
        /// </summary>
        SQLSERVER = 0,


        /// <summary>
        /// データベースがOracleの場合
        /// </summary>
        ORACLE = 1,


        /// <summary>
        /// データベースがACCESSの場合
        /// </summary>
        ACCESS = 2,

        /// <summary>
        /// 
        /// </summary>
        POSTGRESQL = 3,

        /// <summary>
        /// 
        /// </summary>
        MYSQL = 4
    }

    /// <summary>
    /// アダプターの種別
    /// </summary>
    public enum EnumAdapterType
    {
        /// <summary>
        /// アダプターがOleDBの場合
        /// </summary>
        OLEDB = 0,

        /// <summary>
        /// アダプターがSQLDATAの場合
        /// </summary>
        SQL = 1,

        /// <summary>
        /// アダプターがODBCの場合（現在未対応）
        /// </summary>
        ODBC = 2,

        /// <summary>
        /// アダプターがOracleの場合
        /// </summary>
        ORACLE = 3,

        /// <summary>
        /// ODP
        /// </summary>
        ODP=4
    }

    /// <summary>
    /// プロバイダーの種別
    /// </summary>
    public enum EnumProviderType : byte
    {
        /// <summary>
        /// Provide=MS SQL SqlServer
        /// </summary>
        MS_SQL_SQLSERVER = 0,


        /// <summary>
        /// Provider=SQLOLEDB.1 (MS OleDB for SQLServer)
        /// </summary>
        MS_OLEDB_SQLSERVER = 1,

        /// <summary>
        /// Provide=MSDAORA.1 (MS OleDB for Oracle)
        /// </summary>
        MS_OLEDB_ORACLE = 2,



        /// <summary>
        /// Provide=Oracle provider by Oracle
        /// </summary>
        ORA_ORACLE = 4,

        /// <summary>
        /// Provide=Oracle provider for OLE DB
        /// </summary>
        ORA_OLEDB_ORACLE = 5,

        /// <summary>
        /// ODP
        /// </summary>
        ORA_ODP = 6,


        /// <summary>
        /// Provider=Microsoft.Jet.OLEDB.4.0
        /// </summary>
        MS_OLEDB_Jet,

        /// <summary>
        /// 
        /// </summary>
        MS_ODBC_SQLSERVER,

        /// <summary>
        /// 
        /// </summary>
        MS_ODBC_ORACLE,

        /// <summary>
        /// 
        /// </summary>
        MS_ODBC_ACCESS,

        /// <summary>
        /// 
        /// </summary>
        ORA_ODBC_ORACLE
    }

    /// <summary>
    /// DB項目型
    /// </summary>
    public enum EnmItemType
    {
        /// <summary>
        /// 未定義
        /// </summary>
        UnDefined,

        /// <summary>
        /// 文字列
        /// </summary>
        String,

        /// <summary>
        /// 数値
        /// </summary>
        Integer,

        /// <summary>
        /// 数値(長さ)
        /// </summary>
        Long,

        /// <summary>
        /// 十進数
        /// </summary>
        Decimal,

        /// <summary>
        /// 論理型
        /// </summary>
        Boolean,

        /// <summary>
        /// 日付型
        /// </summary>
        Date,

        /// <summary>
        /// 浮動小数点
        /// </summary>
        Float,

        /// <summary>
        /// 浮動小数点(精度高さ)
        /// </summary>
        Double
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnmDBObjectType
    {
        /// <summary>
        /// 
        /// </summary>
        MASTERVIVW,

        /// <summary>
        /// 
        /// </summary>
        TABLEVIVW,

        /// <summary>
        /// 
        /// </summary>
        TABLEALIASVIVW,

        /// <summary>
        /// 
        /// </summary>
        VIEW,

        /// <summary>
        /// 
        /// </summary>
        SQL,

        /// <summary>
        /// 
        /// </summary>
        RESTRICTIONSQL,

        /// <summary>
        /// 
        /// </summary>
        DYNAMICVIEW,

        /// <summary>
        /// 
        /// </summary>
        DYNAMICSQL
    }
    
    /// <summary>
    /// ソートタイプ
    /// </summary>
    public enum EnmSortType
    {
        /// <summary>
        /// 
        /// </summary>
        NONE,

        /// <summary>
        /// 
        /// </summary>
        ASC,

        /// <summary>
        /// 
        /// </summary>
        DESC
    }

    /// <summary>
    /// ソートタイプ
    /// </summary>
    public enum EnmOrderType
    {
        /// <summary>
        /// 
        /// </summary>
        ASC,

        /// <summary>
        /// 
        /// </summary>
        DESC
    }

    /// <summary>
    /// 整合性チェックのエラー種別
    /// </summary>
    public enum EnmConsistenceType
    {
        /// <summary>
        /// 致命エラー
        /// </summary>
        FatalError,

        /// <summary>
        /// 警告
        /// </summary>
        Warning,

        /// <summary>
        /// 無視
        /// </summary>
        Ignore
    }




    //public enum EnumDatabaseType : short
    //{
    //    SQLServer,
    //    Oracle,
    //    Access
    //}



    /// <summary>
    /// 接続方法
    /// </summary>
    public enum EnmConnectionType
    {
        /// <summary>
        /// 接続管理せず、毎回接続しなおす
        /// </summary>
        NoPooling = 0,
        
        /// <summary>
        /// 一つ接続でシステム共有する
        /// </summary>
        SimpleConnection = 1
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnmViewType
    {
        /// <summary>
        /// 
        /// </summary>
        TableView,

        /// <summary>
        /// 
        /// </summary>
        SqlViewType1,

        /// <summary>
        /// 
        /// </summary>
        SqlViewType2
    }
}
