#define TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DrinkServiceTest.mdbTableAdapters;
using DrinkService.Models;
using SafeNeeds.DySmat;

namespace DrinkServiceTest
{
    public partial class ConvertData
    {


        public void Convert種別(string shopCD)
        {
            Console.WriteLine(string.Format("Convert種別:{0}", shopCD));

            foreach (mdb.mst_種別Row mdbrow in 種別.Rows)
            {
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
