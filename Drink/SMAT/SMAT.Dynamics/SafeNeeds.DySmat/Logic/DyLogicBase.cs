using SafeNeeds.DySmat.Model;
using SafeNeeds.DySmat.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeNeeds.DySmat.Logic
{
    public class DyLogicBase
    {
        protected DynamicContext db = new DynamicContext(Global.GetConnectionString());

        public virtual bool RunSQL(int ProjID, string sql)
        {
            Global.Init(ProjID, true);
            Y_Proj _Proj = Global.ProjDic[ProjID];
            DMNewConnection DBMachine = new DMNewConnection(new DySmat.DB.DBConfig(_Proj.DatabaseType, _Proj.ProviderType, _Proj.GetConnectionString(), ProjID));

            try
            {
                DBMachine.RunSQL(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }

            return true;
        }

        public virtual DataTable FillDataTableBySQL(int ProjID, string sql)
        {
            DataTable dt = new DataTable();
            Global.Init(ProjID, true);
            Y_Proj _Proj = Global.ProjDic[ProjID];
            DMNewConnection DBMachine = new DMNewConnection(new DySmat.DB.DBConfig(_Proj.DatabaseType, _Proj.ProviderType, _Proj.GetConnectionString(), ProjID));

            try
            {
                DBMachine.FillDataTableBySQL(ref dt, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }

            return dt;
        }
    }
}
