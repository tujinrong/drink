#define TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity.Migrations;
using DrinkServiceTest.mdbTableAdapters;
using DrinkService.Models;
using SafeNeeds.DySmat;

namespace DrinkServiceTest
{
    public partial class ConvertData
    {


        public void Convert顧客マスタ(string shopCD)
        {
            Console.WriteLine(string.Format("Convert顧客マスタ:{0}",shopCD));

            RunSQL(string.Format("delete from {0} where ShopCD='{1}'", "M_Client", shopCD));

           List<M_Client> list = new List<M_Client>();
            foreach (mdb.mst_顧客マスタRow mdbrow in 顧客マスタ.Rows)
            {
                M_Client row = new M_Client();
                row.ShopCD = shopCD;
                row.ClientCD = CStr(mdbrow["顧客コード"]);
                row.ClientName = CStr(mdbrow["顧客名"]);
                row.CustomerTanto = CStr(mdbrow["顧客担当者"]);
                row.Tel = ToDBC(CStr(mdbrow["顧客ＴＥＬ"]));
                row.UpdateTime = DateTime.Now;
                row.UpdateUser = UpdateUser;
                list.Add(row);
                Console.WriteLine(string.Format("M_Client ==> {0},{1},{2}",
                                   row.ShopCD, row.ClientCD, row.ClientName));
            }
#if TEST
                try
                {
                    db.Clients.AddOrUpdate(list.ToArray());
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
                

#endif

        }

    }
}
