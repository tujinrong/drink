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


        public void Convert担当者マスタ(string shopCD)
        {
            Console.WriteLine(string.Format("Convert担当者マスタ:{0}", shopCD));

            var Staffs = db.Staffs.ToList();

            foreach (mdb.mst_担当者マスタRow mdbrow in 担当者マスタ.Rows)
            {

                M_Staff row = new M_Staff();
                row.ShopCD = shopCD;
                row.StaffCD = CStr(mdbrow["担当者コード"]);
                row.StaffName = CStr(mdbrow["担当者名"]);
                row.RoleCD = ModelBase.CN役割_店舗担当者;
                row.UpdateTime = DateTime.Now;
                row.UpdateUser = UpdateUser;

                if (Staffs.Where(s => s.ShopCD == row.ShopCD && s.StaffCD == row.StaffCD).ToList().Count == 0)
                {
                    Console.WriteLine(string.Format("M_Staff ==> {0},{1},{2}",
                                       row.ShopCD, row.StaffCD, row.StaffName));
                    db.Staffs.AddOrUpdate(row);
                }
               
#if TEST
                db.SaveChanges();
             
#endif
            }
#if TEST
#else
            db.SaveChanges();
#endif

        }

    }
}
