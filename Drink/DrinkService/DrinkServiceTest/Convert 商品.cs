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
using System.Text.RegularExpressions;

namespace DrinkServiceTest
{
    public partial class ConvertData
    {


        public void Convert商品マスタ(string shopCD)
        {
            Console.WriteLine(string.Format("Convert商品マスタ:{0}", shopCD));

            var index = 0;
            var count = 商品マスタ.Rows.Count;

            foreach (mdb.mst_商品マスタRow mdbrow in 商品マスタ.Rows)
            {

                M_Item row = new M_Item();
                row.ItemCD = CStr(mdbrow["商品コード"]);
                row.ItemName = GetItemName(mdbrow["商品名"]);
                row.ShortName = GetShortName(shopCD, row.ItemName);
                row.StandardPrice = CInt(mdbrow["単価"]);
                row.ShopPrice = CInt(mdbrow["売価"]);
                row.InNum = 1;
                row.UpdateTime = DateTime.Now;
                row.UpdateUser = UpdateUser;

                //DB修正
                //if (row.ShopPrice > 999) row.ShopPrice = 999;
                ++index;
                if (row.ItemName.Length > 0 && row.StandardPrice < 10000 && row.ShopPrice < 10000)
                {
                    Console.WriteLine(string.Format("M_Item =(" + index + "/" + count + ")=> {0},{1},{2}",
                                       row.ItemCD, row.ShortName, row.ShopPrice));
                    db.Items.AddOrUpdate(row);
                }
            }
#if TEST

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
                
             
#endif
           

        }

        private string GetItemName(object itemName)
        {
            string name = CStr(itemName);

            if (Regex.Match(name, @"^\d+\u3000{1}.+").Success)
            {
                name = name.Substring(name.IndexOf("　") + 1);
            } else if (Regex.Match(name, @"^\d+\s{1}.+").Success)
            {
                name = name.Substring(name.IndexOf(" ") + 1);
            }

            return name;
        }

        string GetShortName(string shopCD, string name)
        {
            if (name.Length <= 20) return name;

            return name.Substring(0, 20);
        }

    }
}
