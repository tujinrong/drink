//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  DBContext
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************


using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System;

using System.Data;
using SafeNeeds.DySmat;

using SafeNeeds.DySmat.DB;
using SafeNeeds.DySmat.Util;
using SafeNeeds.DySmat.DB.Util;

namespace SafeNeeds.DySmat
{

    /// <summary>
    /// 
    /// </summary>
    public class AdapterBase
    {
        protected  DynamicContext _context;

        public EntityRequest _entityRequest;


        public AdapterBase()
        {

        }

        //private IDbConnection _TransConnectin;
        private DBConfig _Config;
        private string _LastSql;

        private const string Start = "SQL実行開始";
        private const string End = "SQL実行終了";

        /// <summary>
        /// 
        /// </summary>
        protected int cUpdatedTimes;

        /// <summary>
        /// 
        /// </summary>
        protected int cTotalUpdatedTimes;

        /// <summary>
        /// 
        /// </summary>
        protected IDBConnection m_DBMachine;

        /// <summary>
        /// 
        /// </summary>
        //protected DBTransaction m_trans;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="config">データベースの情報 DBConfigクラスを参照</param>

        #region " プロパティ "

        /// <summary>
        /// 
        /// </summary>
        public DBConfig Config
        {
            get
            {
                return _Config;
            }
            set
            {
                _Config = value;
            }
        }

        ///// <summary>
        ///// 接続チェック
        ///// </summary>
        ///// <returns></returns>
        //public bool CheckConnection()
        //{
        //    try
        //    {
        //        System.DateTime temp =  Common.GetSystemTime(this.Config, true);
        //    }
        //    catch (System.Exception)
        //    {
        //        return false;
        //    }
        //    return true;
        //}



        /// <summary>
        /// タイムアウトの設定
        /// </summary>
        public int CommandTimeOut
        {
            get
            {
                return this.m_DBMachine.CommandTimeout;
            }
            set
            {
                if (value > 600)
                {
                    value = 600;
                }
                this.m_DBMachine.CommandTimeout = value;
            }
        }

        #endregion

        #region " メソッド "


        /// <summary>
        /// SQLの実行結果
        /// </summary>
        /// <returns></returns>
        public string GetLastSQL()
        {
            return this._LastSql;
        }

        /// <summary>
        /// コネクションのクローズ
        /// </summary>
        public void Close()
        {
            this.m_DBMachine.Close();
        }

        /// <summary>
        /// SQLよりデータセットを生成する
        /// </summary>
        /// <param name="strSQL">SQL文</param>
        /// <returns>SQLより生成されたデータセットを返す</returns>
        public DataSet GetDataSetBySQL(string strSQL, params DBParameter[] param)
        {
            DataSet ds = new DataSet();
            FillDataSetBySQL(ds, strSQL, param);
            return ds;
        }

        /// <summary>
        /// SPよりデータセットを生成する
        /// </summary>
        /// <param name="strSP">SPの名称</param>
        /// <param name="param">引数</param>
        /// <returns>データセット</returns>
        public DataSet GetDataSetBySP(string strSP, params DBParameter[] param)
        {
            DataSet ds = new DataSet();
            FillDataSetBySP(ds, strSP, "", param);
            return ds;
        }

        /// <summary>
        /// SQLよりデータセットを生成する
        /// </summary>
        /// <param name="strSQL">SQL文</param>
        /// <param name="tableName">対象のテーブル名</param>
        /// <returns>SQLよりデータセットを生成する</returns>
        public DataSet GetDataSetBySQL(string strSQL, string tableName)
        {
            DataSet ds = new DataSet();
            FillDataSetBySQL(ds, strSQL, tableName);
            return ds;
        }

        /// <summary>
        /// SQLの実行
        /// </summary>
        /// <param name="sql">実行するSQL文</param>
        /// <returns>対象データの件数</returns>
        public int RunSQL(string sql)
        {
            this._LastSql = sql;
            
            StringBuilder log = new StringBuilder();
            log.AppendLine(DateTime.Now.ToString() + " " + Start);
            log.AppendLine(sql);
           // this.OnLogDelegate(log.ToString());

         //   int start =DataUtil.CInt(DateTime.Now.ToString("fff"));
            int i = this.m_DBMachine.RunSQL(sql);
           // int end = DataUtil.CInt(DateTime.Now.ToString("fff"));

            //this.OnLogDelegate(End + DateTime.Now.ToString());
          //  this.OnLogDelegate(End + (end - start).ToString() + "ミリ秒をかかります 。");
            return i;
        }

        /// <summary>
        /// SPを実行する
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="param">引数</param>
        /// <returns>実行結果</returns>
        public int RunSP(string sql, params DBParameter[] param)
        {
            this._LastSql = sql;

           // this.OnLogDelegate(sql);

            return this.m_DBMachine.RunSP(sql, param);
        }

        /// <summary>
        /// 複数ＳＱＬの実行
        /// </summary>
        /// <param name="sqls">ＳＱＬの配列</param>
        /// <returns>実行したデータの件数合計</returns>
        public int RunSQLs(string[] sqls)
        {
            int n = 0;
            foreach (string s in sqls)
            {
                n += this.RunSQL(s);
            }
            return n;
        }

        /// <summary>
        /// SQLでデータセットへデータをロードする
        /// </summary>
        /// <param name="ds">SQL文</param>
        /// <param name="SQL">ロード対象のデータセット</param>
        public void FillDataSetBySQL(DataSet ds, string SQL, params DBParameter[] param)
        {
            this._LastSql = SQL;

            StringBuilder log = new StringBuilder();

            log.AppendLine(DateTime.Now.ToString() + " " + Start);
            log.AppendLine(SQL);
        //    this.OnLogDelegate(log.ToString());

            int start = DataUtil.CInt(DateTime.Now.ToString("fff"));
            if (ds.Tables.Count == 1)
            {
                this.m_DBMachine.FillDataSetBySQL(ds, SQL, ds.Tables[0].TableName, param);
            }
            else
            {
                this.m_DBMachine.FillDataSetBySQL(ds, SQL, "", param);
            }
            int end = DataUtil.CInt(DateTime.Now.ToString("fff"));

         //   this.OnLogDelegate(End + (end - start).ToString() + "ミリ秒をかかります 。");

            //this.OnLogDelegate(End + DateTime.Now.ToString());
        }

        public void FillDataTableBySQL(ref DataTable dt, string SQL)
        {
            //this._LastSql = SQL;
            //StringBuilder log = new StringBuilder();
            //log.AppendLine(DateTime.Now.ToString() + " " + Start);
            //log.AppendLine(SQL);
            //this.OnLogDelegate(log.ToString());

            this.m_DBMachine.FillDataTableBySQL(ref dt, SQL);
           
            //this.OnLogDelegate(End + (end - start).ToString() + "ミリ秒をかかります 。");
            //this.OnLogDelegate(End + DateTime.Now.ToString());
        }

        /// <summary>
        /// SQLでデータセットへデータをロードする
        /// </summary>
        /// <param name="ds">ロード対象のデータセット</param>
        /// <param name="SQL">SQL文</param>
        /// <param name="tableName">SQL実行対象のテーブル名</param>
        public void FillDataSetBySQL(DataSet ds, string SQL, string tableName, params DBParameter[] param)
        {
            this._LastSql = SQL;

            //StringBuilder log = new StringBuilder();
            //log.AppendLine(DateTime.Now.ToString() + " " + Start);
            //log.AppendLine(SQL);
           // this.OnLogDelegate(log.ToString());
           // int start = DataUtil.CInt(DateTime.Now.ToString("fff"));

            this.m_DBMachine.FillDataSetBySQL(ds, SQL, tableName, param);

            //int end = DataUtil.CInt(DateTime.Now.ToString("fff"));
            //this.OnLogDelegate(End + DateTime.Now.ToString());
            //this.OnLogDelegate(End + (end - start).ToString() + "ミリ秒をかかります 。");
           // this.OnDataDelegate(DataSetUtil.ToCsv(ds.Tables[0]));
        }

        /// <summary>
        /// SPよりデータセットをセットする
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="strSP">SP名称</param>
        /// <param name="param">引数</param>
        public void FillDataSetBySP(DataSet ds, string strSP, params DBParameter[] param)
        {
            FillDataSetBySP(ds, strSP, "", param);
        }

        /// <summary>
        /// SPよりデータセットをセットする
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="strSP">SP名称</param>
        /// <param name="strTableName">テーブル名</param>
        /// <param name="param">引数</param>
        public void FillDataSetBySP(DataSet ds, string strSP, string strTableName, params DBParameter[] param)
        {
            this._LastSql = strSP;

           // this.OnLogDelegate(strSP);

            this.m_DBMachine.FillDataSetBySP(ds, strSP, strTableName, param);

            //this.OnDataDelegate(DataSetUtil.ToCsv(ds.Tables[0]));
        }

        #endregion



    }
}