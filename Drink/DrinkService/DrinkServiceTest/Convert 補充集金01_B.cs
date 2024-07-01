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


        public void Convert補充集金01_B(string shopCD)
        {
            Console.WriteLine(string.Format("Convert補充集金01_B:{0}", shopCD));

            RunSQL(string.Format("delete from {0} where ShopCD='{1}'", "T_HoClientItem", shopCD));

            List<T_HoClientItem> list = new List<T_HoClientItem>();

            var HoBeforeDayClientsQuary = from c in 補充集金01_B.AsEnumerable()
                                          group c by new { c.顧客コード, c.訪問日, c.商品コード } into cg
                                          select new
                                          {
                                              key = cg.Key,
                                              棚 = cg.First().棚,
                                              売価 = cg.First().売価,
                                              前回在庫 = cg.Sum(c => CInt(c["前回在庫"])),
                                              現在庫 = cg.Sum(c => CInt(c["現在庫"])),
                                              使用数 = cg.Sum(c => CInt(c["使用数"])),
                                              補充数 = cg.Sum(c => CInt(c["補充数"])),
                                              補充後 = cg.Sum(c => CInt(c["補充後"])),
                                              金額 = cg.Sum(c => CInt(c["金額"]))
                                          };

            var HoClients = db.HoClients.ToList();
            var count = 補充集金01_B.Rows.Count;
            var index = 0;
            foreach (var mdbrow in HoBeforeDayClientsQuary)
            {
                T_HoClient hoClient = null;
                T_HoClientItem beforeHoClientItem = null;
                T_HoClientItem before2HoClientItem = null;
                List<T_HoClient> listx = HoClients.Where(h => h.ShopCD == shopCD && h.ClientCD == mdbrow.key.顧客コード && h.HoDate == mdbrow.key.訪問日).ToList();

                if (listx.Count > 0)
                {
                    hoClient = listx.First();

                    List<T_HoClientItem> beforeList = list.Where(h => h.ShopCD == shopCD && h.ClientCD == mdbrow.key.顧客コード && h.Seq == hoClient.Seq - 1 && h.ItemCD == mdbrow.key.商品コード).ToList();

                     if (beforeList.Count > 0)
                     {
                         beforeHoClientItem = beforeList.First();
                     }

                     List<T_HoClientItem> before2List = list.Where(h => h.ShopCD == shopCD && h.ClientCD == mdbrow.key.顧客コード && h.Seq == hoClient.Seq - 2 && h.ItemCD == mdbrow.key.商品コード).ToList();

                     if (before2List.Count > 0)
                     {
                         before2HoClientItem = before2List.First();
                     }
                }

                if (hoClient == null)
                {
                    string log = "【trn_補充集金01_B】テーブル対応するデータは存在しません、【trn_補充集金01_B】データ：" + mdbrow.key.顧客コード
                      + "," + CStr(mdbrow.key.訪問日) + "," + mdbrow.棚 + "," + mdbrow.key.商品コード
                      + "," + mdbrow.売価 + "," + mdbrow.前回在庫 + "," + mdbrow.現在庫
                      + "," + mdbrow.使用数 + "," + mdbrow.補充数 + "," + mdbrow.補充後
                      + "," + mdbrow.金額;
                    Console.WriteLine(log);
                    logStr.AppendLine(log);
                    continue;
                }


                if (!HasItem(mdbrow.key.商品コード))
                {
                    string log = "商品は存在しません、【trn_補充集金01_B】データ：" + mdbrow.key.顧客コード
                      + "," + CStr(mdbrow.key.訪問日) + "," + mdbrow.棚 + "," + mdbrow.key.商品コード
                      + "," + mdbrow.売価 + "," + mdbrow.前回在庫 + "," + mdbrow.現在庫
                      + "," + mdbrow.使用数 + "," + mdbrow.補充数 + "," + mdbrow.補充後
                      + "," + mdbrow.金額;
                    Console.WriteLine(log);
                    logStr.AppendLine(log);
                    continue;
                }

                T_HoClientItem row = new T_HoClientItem();
                row.ShopCD = shopCD;
                row.ClientCD = CStr(mdbrow.key.顧客コード);
                row.Seq = hoClient.Seq;
                row.ItemCD = CStr(mdbrow.key.商品コード);
                row.ShelfNo = CInt(GetShelfCD(CStr(mdbrow.棚)));
                row.ShelfSubNo = 0;
                
                //今回在庫数
                row.ThisNum = mdbrow.現在庫;
                //補充数
                row.AddNum = mdbrow.補充数;

                if (beforeHoClientItem != null)
                {
                    //前回在庫数
                    row.PrevNum = mdbrow.前回在庫;
                    //補充前数
                    row.BeforeNum = beforeHoClientItem.AddNum;
                }
                //使用数
                row.UsedNum = mdbrow.使用数;
                //補充後数
                row.AfterNum = mdbrow.補充後;
                //単価
                row.Price = mdbrow.売価;
                //金額
                row.Money = mdbrow.金額;
                //賞味期限
                //row.FreshDate = mdbrow.xxx
                //次回単価
                row.NextPrice = mdbrow.売価;

                if (beforeHoClientItem == null)
                {
                    //商品追加
                    row.ItemAddFlag = 1;
                }
                else
                {
                    row.ItemAddFlag = 0;
                }

                if (row.Seq == 1)
                {
                    row.ItemAddFlag = 0;
                }

                if (beforeHoClientItem != null && before2HoClientItem != null && beforeHoClientItem.UsedNum == 0 && before2HoClientItem.UsedNum == 0)
                {
                    row.SaleFlag = 1;
                }


                Console.WriteLine(string.Format("T_HoClientItem =(" + (++index) + "/"+ count +")=> {0},{1},{2},{3},{4}",
                                   row.ShopCD, row.ClientCD, row.Seq, row.ItemCD, row.ShelfNo));
                list.Add(row);
            }
#if TEST
            db.HoClientItems.AddRange(list);
            db.SaveChanges();
             
#endif
           

        }

    }
}
