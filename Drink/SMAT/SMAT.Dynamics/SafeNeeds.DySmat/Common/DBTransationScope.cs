using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using System.Threading;

using SafeNeeds.DySmat.DB.Exception;
using SafeNeeds.DySmat.Model;
using SafeNeeds.DySmat.DB;

namespace SafeNeeds.DySmat
{
    
    /// <summary>
    /// 
    /// </summary>
    public class DBTransactionScope : IDisposable
    {
        private static Dictionary<int, IDbTransaction> TransList = new Dictionary<int, IDbTransaction>();
        private static Dictionary<int, IDbConnection> ConnList = new Dictionary<int, IDbConnection>();
        private static Dictionary<int, int> Nest = new Dictionary<int, int>();
        private int _dbID;


        public DBTransactionScope(int dbID)
        {
            _dbID = dbID;

            lock (this)
            {
                int id=GetID();
                if (!TransList.ContainsKey(id))
                {
                    Global.Init(dbID);
                    Y_Proj proj = Global.ProjDic[dbID];
                    DySmat.DB.DBConfig config = new DySmat.DB.DBConfig(proj.DatabaseType, proj.ProviderType, proj.GetConnectionString(), dbID);

                    IProviderManager pm = ProviderFactory.GetProvider(config);  // new ProviderManager(config);
                    IDbConnection conn = pm.DBConnection();
                    conn.Open();
                    IDbTransaction trans = conn.BeginTransaction();
                    
                    TransList.Add(id, trans);
                    ConnList.Add(id, conn);
                    Nest.Add(id, 0);
                }
                else
                {
                    Nest[id] += 1;
                }
            }
        }

        public IDbConnection GetConnection()
        {
            lock (this)
            {
                int id = GetID();
                if (ConnList.ContainsKey(id))
                {
                    return ConnList[id];
                }
                else
                {
                    throw new ApplicationException("GetConnection()");
                    //2016.02.16
                    //ProviderManager pm = new ProviderManager(null);
                    //return pm.DBConnection();

                }

            }
        }


        public void Complete()
        {
            lock (this)
            {
                int id = GetID();
                if (Nest[id] == 0)
                {


                    IDbTransaction trans = TransList[id];
                    if (trans != null)
                    {
                        trans.Commit();
                        trans.Dispose();
                    }
                    TransList.Remove(id);

                    IDbConnection conn = ConnList[id];
                    conn.Close();
                    conn.Dispose();
                    ConnList.Remove(id);

                    Nest.Remove(id);
                }

            }

        }

        public void Dispose()
        {
            lock (this)
            {
                int id = GetID();
                
                if (!TransList.ContainsKey(id)) return;
                
                if (Nest[id] == 0)
                {
                    IDbTransaction trans = TransList[id];
                    if (trans != null)
                    {

                        trans.Rollback();
                        trans.Dispose();
                        TransList.Remove(id);

                        IDbConnection conn = ConnList[id];
                        conn.Close();
                        conn.Dispose();

                        ConnList.Remove(id);
                        Nest.Remove(id);
                    }
                }
                else
                {
                    Nest[id] -= 1;
                }
            }
            
        }

        private int GetID()
        {
            return GetID(_dbID);
        }

        private static int GetID(int dbno)
        {
            return Thread.CurrentThread.ManagedThreadId * 1000 + dbno;
        }

        public static IDbConnection GetConnection(int dbNo, out IDbTransaction transction)
        {
            int id = GetID(dbNo);
            if (ConnList.ContainsKey(id))
            {
                transction = TransList[id];
                return ConnList[id];
            }

            Global.Init(dbNo);
            Y_Proj proj = Global.ProjDic[dbNo];
            DySmat.DB.DBConfig config = new DySmat.DB.DBConfig(proj.DatabaseType, proj.ProviderType, proj.GetConnectionString(), dbNo);

            IProviderManager pm = ProviderFactory.GetProvider(config);
            IDbConnection conn = pm.DBConnection();
            conn.Open();
            transction = null;
            return conn;

        }
    }
}
