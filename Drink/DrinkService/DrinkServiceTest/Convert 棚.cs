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


        public void Convert棚マスタ(string shopCD)
        {
            Console.WriteLine(string.Format("Convert棚:{0}", shopCD));

            int i = 0;
            foreach (mdb.mst_棚マスタRow mdbrow in 棚マスタ.Rows)
            {

                M_Code row = new M_Code();

                row.Kind = "Shelf";
                row.CD = i++.ToString();
                row.Name = mdbrow.棚;
                row.UpdateTime = DateTime.Now;
                row.UpdateUser = UpdateUser;

                Console.WriteLine(string.Format("{0},{1},{2}",
                                   row.Kind, row.CD, row.Name));

                db.Codes.AddOrUpdate(row);
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
