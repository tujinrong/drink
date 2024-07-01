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
using System.Data.SqlClient;

namespace DrinkServiceTest
{
    public partial class ConvertData
    {


        public void Convert補充集金00_H(string shopCD)
        {
            Console.WriteLine(string.Format("Convert補充集金00_H:{0}", shopCD));

            RunSQL(string.Format("delete from {0} where ShopCD='{1}'", "T_HoClient", shopCD));


            mdb.trn_補充集金00_HDataTable dt = 補充集金00_H;
            DataRow[] rows = dt.Select("", "顧客コード,訪問日");

            List<T_HoClient> hoClientlist = new List<T_HoClient>();

            foreach (mdb.trn_補充集金00_HRow mdbrow in rows)
            {
                int lastSeq = hoClientlist.Where(h => h.ShopCD == shopCD && h.ClientCD == mdbrow.顧客コード).ToList().Count;

                T_HoClient row = new T_HoClient();

                row.ShopCD = shopCD;
                row.ClientCD = CStr(mdbrow["顧客コード"]);
                row.Seq = lastSeq + 1;
                row.HoDate = mdbrow.訪問日;
                row.Route = GetRoute(shopCD,row.ClientCD,row.HoDate);
                row.TantoCD = CStr(mdbrow["担当者コード"]);
                row.SoldMoney = CInt(mdbrow["売上計"]);
                row.GetMoney = CInt(mdbrow["集金計"]);
                row.DiffMoney = CInt(mdbrow["過不足"]);
                row.Memo = CStr(mdbrow["メモ"]);
                row.UpdateTime = DateTime.Now;
                row.UpdateUser = UpdateUser;

                if (!HasStaff(shopCD, row.TantoCD))
                {
                    string log = "担当者は存在しません、【trn_補充集金00_H】データ：" + CStr(mdbrow["顧客コード"]) + "," + CStr(mdbrow["担当者コード"])
                        + "," + CStr(mdbrow["訪問日"]) + "," + CStr(mdbrow["訪問日YYYYMMDD"]) + "," + CStr(mdbrow["売上計"])
                        + "," + CStr(mdbrow["集金計"]) + "," + CStr(mdbrow["過不足"]) + "," + CStr(mdbrow["メモ"]);
                    Console.WriteLine(log);
                    logStr.AppendLine(log);
                    continue;
                }


                Console.WriteLine(string.Format("T_HoClient ==> {0},{1},{2},{3}",
                                   row.ShopCD, row.ClientCD, row.Seq, row.HoDate));
                hoClientlist.Add(row);

            }
#if TEST
            db.HoClients.AddRange(hoClientlist);
            db.SaveChanges();
#endif
        }

        private string GetRoute(string shopCD, string clientCD, DateTime hoDate)
         {
            string routeHead = CommonLogic.GetRouteFromDate(hoDate);
            return routeHead + "01"; 
            //List<string> routeList = GetRouteList(shopCD, clientCD);

            //if (routeList.Count > 0)
            //{

            //    routeList.Sort();

            //    for (int i = 0; i < routeList.Count; i++)
            //    {
            //        if (string.CompareOrdinal(routeList[i].Substring(0, 2), routeHead) > 0)
            //        {
            //            if (i == 0)
            //            {
            //                return routeList.Last();
            //            }
            //            else
            //            {
            //                return routeList[i - 1];
            //            }
            //        }
            //    }
            //    return routeList.Last();
            //}
            //else
            //{
            //    return routeHead + "01"; 
            //}
        }

        private List<string> GetRouteList(string shopCD, string clientCD)
        {
            List<M_ClientRoute> list =   db.ClientRoutes.Where(c => c.ShopCD == shopCD && c.ClientCD == clientCD && c.Route.Length > 0).ToList();
            List<string> routeList = new List<string>();
            foreach (var item in list)
            {
                routeList.Add(item.Route);
            }
           
            return routeList;
        }

    }
}
