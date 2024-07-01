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


        public void Convert初期在庫(string shopCD)
        {
            Console.WriteLine(string.Format("Convert初期在庫:{0}", shopCD));

            RunSQL(string.Format("delete from {0} where ShopCD ='{1}'", "M_ClientInitItems", shopCD));


            List<M_ClientInitItems> list = new List<M_ClientInitItems>();



            foreach (mdb.trn_顧客01_初期在庫Row item in 顧客01_初期在庫.Rows)
            {
                item["商品コード"] = CStr(item["商品コード"]);
            }

            var HoBeforeDayClientsQuary = from c in 顧客01_初期在庫.AsEnumerable()

                                          group c by new { c.顧客コード, c.商品コード} into cg

                                          select new
                                          {
                                              key = cg.Key,
                                              初期在庫 = cg.Sum(c => CInt(c["初期在庫"])),
                                              棚 = CStr(cg.First()["棚"])
                                          };

            var count = HoBeforeDayClientsQuary.Count();
            var index = 0;
            foreach (var mdbrow in HoBeforeDayClientsQuary)
            {

                M_ClientInitItems row = new M_ClientInitItems();
                row.ShopCD = shopCD;
                row.ClientCD = mdbrow.key.顧客コード;

                try
                {
                    row.ShelfCD = GetShelfCD(mdbrow.棚);
                }
                catch (Exception ex)
                {
                    
                    throw;
                }

               
                row.ItemCD = mdbrow.key.商品コード;
                row.Num = mdbrow.初期在庫;

                if (Items.Where(i => i.ItemCD == row.ItemCD).Count() == 0) continue;

                if (!HasItem(row.ItemCD))
                {
                    string log = "商品は存在しません、【trn_顧客01_初期在庫】データ：" + mdbrow.key.顧客コード 
                        + "," + mdbrow.棚 + "," + mdbrow.key.商品コード + "," + mdbrow.初期在庫; ;
                    Console.WriteLine(log);
                    logStr.AppendLine(log);
                    continue;
                }

                ++index;
                if (row.ItemCD.Length > 0 )
                {
                    Console.WriteLine(string.Format("M_ClientInitItems =(" + (index) + "/" + count + ")=> {0},{1},{2},{3},{4}",
                                  row.ShopCD, row.ClientCD, row.ShelfCD, row.ItemCD, row.Num));
                    row.Price = GetItemPrice(row.ItemCD);
                    list.Add(row);
                }

            }
#if TEST
            try
            {
                db.ClientItems.AddRange(list);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw;
            }
          
             
#endif
        }

        private string GetShelfCD(string ShelfName)
        {

            ShelfName = ToDBC(ShelfName);
            switch (ShelfName)
            {
                case "上１":
                case "上２":
                case "上３":
                case "上1":
                case "上2":
                case "上3":
                    ShelfName = "０";
                    break;
                case "1":
                    ShelfName = "１";
                    break;
                case "2":
                    ShelfName = "２";
                    break;
                case "3":
                    ShelfName = "３";
                    break;
                case "4":
                    ShelfName = "４";
                    break;

                default:
                    break;
            }

            try
            {

                if (ShelfName == "")
                {
                    return db.Codes.Where(c => c.Kind == "Shelf").First().CD;
                }
                else
                {
                    return db.Codes.Where(c => c.Kind == "Shelf" && c.Name == ShelfName).First().CD;
                }
                
            }
            catch (Exception ex)
            {
                
                throw;
            }
          
        }

        private decimal GetItemPrice(string itemCD)
        {
            return db.Items.Where(i => i.ItemCD == itemCD).First().StandardPrice; 
        }

    }
}
