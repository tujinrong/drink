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


        public void Convert契約内容(string shopCD)
        {
            Console.WriteLine(string.Format("Convert契約内容:{0}", shopCD));

            RunSQL(string.Format("delete from {0} where ShopCD='{1}'", "M_ClientRoute", shopCD));

            foreach (mdb.trn_顧客00_契約内容Row mdbrow in 顧客00_契約内容.Rows)
            {
                M_Client client = null;
                List<M_Client> list = db.Clients.Where(c => c.ShopCD == shopCD && c.ClientCD == mdbrow.顧客コード).ToList();

                if (list.Count > 0)
                {
                    client = list.First<M_Client>();
                }
                
                if (client == null)
                {
                    client = new M_Client();
                    client.ShopCD = shopCD;
                    client.ClientCD = CStr(mdbrow["顧客コード"]);
                    client.ClientName = CStr(mdbrow["顧客名"]);
                    client.CustomerTanto = CStr(mdbrow["顧客担当者"]);
                    client.Tel = CStr(mdbrow["顧客ＴＥＬ"]);
                   
                }

                client.TantoCD = CStr(mdbrow["担当者コード"]);

                if (!HasStaff(shopCD, client.TantoCD))
                {
                    string log = "担当者[" + client.TantoCD + "]は存在しません、【trn_顧客00_契約内容】データ：" + CStr(mdbrow["顧客コード"]) + "," + CStr(mdbrow["顧客名"])
                        + "," + CStr(mdbrow["顧客ＴＥＬ"]) + "," + CStr(mdbrow["顧客担当者"]) + "," + CStr(mdbrow["担当者コード"])
                        + "," + CStr(mdbrow["ルートA1"]) + "," + CStr(mdbrow["ルートA2"]) + "," + CStr(mdbrow["ルートA3"])
                        + "," + CStr(mdbrow["ルートB1"]) + "," + CStr(mdbrow["ルートB2"]) + "," + CStr(mdbrow["ルートB3"])
                        + "," + CStr(mdbrow["ルートC1"]) + "," + CStr(mdbrow["ルートC2"]) + "," + CStr(mdbrow["ルートC3"])
                        + "," + CStr(mdbrow["ルートD1"]) + "," + CStr(mdbrow["ルートD2"]) + "," + CStr(mdbrow["ルートD3"])
                        + "," + CStr(mdbrow["備考"]) + "," + CStr(mdbrow["印刷"]);
                    Console.WriteLine(log);
                    logStr.AppendLine(log);
                    continue;
                }

                client.Memo = CStr(mdbrow["備考"]);
                client.UpdateTime = DateTime.Now;
                client.UpdateUser = UpdateUser;
                db.Clients.AddOrUpdate(client);


                for (int i = 1; i < 5; i++)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        M_ClientRoute route = new M_ClientRoute();
                        route.ShopCD = shopCD;
                        route.ClientCD = CStr(mdbrow["顧客コード"]);
                        route.WeekNo = (short)i;
                        route.RouteNo = (short)j;
                        switch (i)
                        {
                            case 1:
                                switch (j)
	                            {
		                            case 1:
                                        route.Route = ToDBC(CStr(mdbrow["ルートA1"]));
                                        break;
                                    case 2:
                                        route.Route = ToDBC(CStr(mdbrow["ルートA2"]));
                                        break;
                                    case 3:
                                        route.Route = ToDBC(CStr(mdbrow["ルートA3"]));
                                        break;
	                            }
                                break;

                            case 2:
                                switch (j)
                                {
                                    case 1:
                                        route.Route = ToDBC(CStr(mdbrow["ルートB1"]));
                                        break;
                                    case 2:
                                        route.Route = ToDBC(CStr(mdbrow["ルートB2"]));
                                        break;
                                    case 3:
                                        route.Route = ToDBC(CStr(mdbrow["ルートB3"]));
                                        break;
                                }
                                break;

                            case 3:
                                switch (j)
                                {
                                    case 1:
                                        route.Route = ToDBC(CStr(mdbrow["ルートC1"]));
                                        break;
                                    case 2:
                                        route.Route = ToDBC(CStr(mdbrow["ルートC2"]));
                                        break;
                                    case 3:
                                        route.Route = ToDBC(CStr(mdbrow["ルートC3"]));
                                        break;
                                }
                                break;
                                
                            case 4:
                                switch (j)
                                {
                                    case 1:
                                        route.Route = ToDBC(CStr(mdbrow["ルートD1"]));
                                        break;
                                    case 2:
                                        route.Route = ToDBC(CStr(mdbrow["ルートD2"]));
                                        break;
                                    case 3:
                                        route.Route = ToDBC(CStr(mdbrow["ルートD3"]));
                                        break;
                                }
                                break;
                        }

                        if (route.Route.Length > 0)
                        {
                            Console.WriteLine(string.Format("M_ClientRoute ==> {0},{1},{2},{3}",
                                   route.ShopCD, route.ClientCD, route.RouteNo, route.Route));
                            db.ClientRoutes.Add(route);
                        }
                        
                    }
                     
                }
            }
#if TEST
        db.SaveChanges();
            
#else
        db.SaveChanges();
#endif

        }

    }
}
