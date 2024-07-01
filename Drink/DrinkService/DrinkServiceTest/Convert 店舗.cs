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


        public void Convert店舗マスタ(string shopCD)
        {
            Console.WriteLine(string.Format("Convert店舗マスタ:{0}", shopCD));

            M_Shop row = new M_Shop();
            row.ShopCD = shopCD;
            row.ShopTypeCD = "1";
            row.SysTypeCD = "1";
            row.ShopName = mdbDic[shopCD].Replace(".mdb", "");
            row.SystemStartDate = DateTime.Today;
            row.UpdateTime = DateTime.Now;
            row.UpdateUser = UpdateUser;

            Console.WriteLine(string.Format("M_Shop ==> {0},{1}",
                                  row.ShopCD, row.ShopName));

            db.Shops.AddOrUpdate(row);

#if TEST
#else
            db.SaveChanges();
#endif

        }

    }
}
