using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DrinkService.Models;
using SafeNeeds.DySmat;

namespace DrinkServiceTest
{
    class Program
    {
        static void Main(string[] args)
        
        {

            if (true)
            {
                ConvertData cd = new ConvertData();
                cd.ConvertAll();
                Console.ReadLine();
            }

            /*
                        EntityRequest enreq = new EntityRequest(0, "test");

                        EntityAdapter logic1 = new EntityAdapter(enreq, typeof(M_Client).Name);
                        TableDeleteRequest d = new TableDeleteRequest("11", "aac");
                        logic1.Delete(d);

                        //集計表
                        if (true)
                        {

                            EntityAdapter logic = new EntityAdapter(enreq, typeof(T_HoClient).Name);
                            PageViewRequest req = new PageViewRequest();
                            PageViewResult result = logic.GetList(req, Y_EntityViewData.補充集金集計表);
                        }


                        //顧客管理 DySMAT版
                        if (true)
                        {
                            EntityAdapter logic = new EntityAdapter(enreq, typeof(M_Client).Name);

                            //データの取得
                            TableReadRequest greq = new TableReadRequest(M_ShopData.南森町店, M_ClientData.ニッシンシステム);
                            greq.ReadSubTables = true;
                            DataSet ds = new DataSet();
                            logic.GetData(greq, ref ds);

                            //データの削除
                            TableDeleteRequest dreq = new TableDeleteRequest(M_ShopData.南森町店, M_ClientData.ニッシンシステム);
                            dreq.CheckRelationTable = true;
                            dreq.DeleteSubTables = true;
                            logic.Delete(dreq);

                            //データの更新
                            TableSaveRequest sreq = new TableSaveRequest();

                            sreq.SaveSubTables = true;
                            sreq.IsolationMode = TableSaveRequest.EnumIsolationMode.None;

                            //＊＊＊差分更新＊＊＊
                            sreq.SaveMode = TableSaveRequest.EnumSaveMode.SaveChange;
                            //追加行
                            DataRow row = ds.Tables[typeof(M_ClientRoute).Name].NewRow();
                            row[M_ClientRoute.I店舗コード] = M_ShopData.南森町店;
                            row[M_ClientRoute.I顧客コード] = M_ClientData.ニッシンシステム;
                            row[M_ClientRoute.I週] = ds.Tables[typeof(M_ClientRoute).Name].Rows.Count + 1;
                            row[M_ClientRoute.IルートNo] = 1;
                            row[M_ClientRoute.Iルート] = "1112";
                            ds.Tables[typeof(M_ClientRoute).Name].Rows.Add(row);
                            //修正行
                            row = ds.Tables[typeof(M_ClientRoute).Name].Rows[0];
                            row.BeginEdit();
                            row[M_ClientRoute.Iルート] = ds.Tables[typeof(M_ClientRoute).Name].Rows.Count.ToString();
                            row.EndEdit();
                            ds.AcceptChanges();

                            logic.SaveData(sreq, ds);


                            //＊＊＊直接更新＊＊＊
                            sreq.SaveMode = TableSaveRequest.EnumSaveMode.SaveBinding;
                            //データ追加
                            row = ds.Tables[0].NewRow();
                            row[M_Client.I店舗コード] = "11";
                            row[M_Client.I顧客コード] = "aaf";
                            row[M_Client.I顧客名] = "顧客名a";
                            row[M_Client.I回数] = 1;
                            ds.Tables[0].Rows.Add(row);
                            //データ更新
                            logic.SaveData(sreq, ds);
                            ds.AcceptChanges();
                            row = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];
                            row[M_Client.I回数] = 2;
                            logic.SaveData(sreq, ds);

                            //データ削除
                            ds.AcceptChanges();
                            ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1].Delete();
                            logic.SaveData(sreq, ds);
                        }

                        //顧客管理 カスタマイズ版
                        if (false)
                        {
                            M_ClientAdapter logic = new M_ClientAdapter(enreq);
                            PageViewRequest req = new PageViewRequest();

                            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { M_ShopData.南森町店 });
                            PageViewResult result = logic.GetList(req);

                            M_Client data = new M_Client();
                            data.ShopCD = M_ShopData.南森町店;
                            data.ClientCD = M_ClientData.ニッシンシステム;
                            data.ClientName = "ニッシンシステム";
                            data.InitItemDetail = new List<M_ClientInitItems>();
                            data.InitItemDetail.AddRange(new M_ClientInitItems[] {
                                new M_ClientInitItems
                                {
                                    ShopCD = M_ShopData.南森町店,
                                    ClientCD = M_ClientData.ニッシンシステム,
                                    ShelfCD="1",
                                    ItemCD = M_ItemData.コカコーラ,
                                    Num=1,
                                    Price=100
                                }
                            });

                            data.RouteDetail = new List<M_ClientRoute>();
                            data.RouteDetail.AddRange(new M_ClientRoute[]{
                                new M_ClientRoute{ 
                                    ShopCD = M_ShopData.南森町店,
                                    ClientCD = M_ClientData.ニッシンシステム,
                                    WeekNo=1,
                                   //  WeekDayNo=1,
                                     Route="1101"
                           
                                }
                            }
                            );

                            //HoRequest hreq = new HoRequest();
                            ////データの保存
                            //Result res = logic.SaveData(hreq, data);
                            //M_Client.Key key = data.GetKey();

                            ////データの取得
                            //res = logic.GetData(key, out data);
                            //res = logic.Delete(hreq, key);

                        }


                        //補充集金
                        if (false)
                        {
                            T_HoClientAdapter logic = new T_HoClientAdapter(enreq);
                            PageViewRequest req = new PageViewRequest();

                            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { M_ShopData.南森町店 });
                            PageViewResult result = logic.GetList(req);

                            T_HoClient data = new T_HoClient();
                            data.ShopCD = M_ShopData.南森町店;
                            data.ClientCD = M_ClientData.ニッシンシステム;
                            data.Seq = 1;
                            data.HoDate = DateTime.Today;
                            data.Route = "0100";
                            data.TantoCD = M_StaffData.田中担当者;
                            data.Detail = new List<T_HoClientItem>();
                            data.Detail.AddRange(new T_HoClientItem[] {
                            new T_HoClientItem
                            {
                                ShopCD = M_ShopData.南森町店,
                                Seq = 1,
                                ClientCD = M_ClientData.ニッシンシステム,
                                ItemCD="1", 
                                 AddNum=1, AfterNum=1,   BeforeNum=1,  Money=1,  Price =1, ShelfNo=1, ShelfSubNo=1
                            }
                            }
                            );

                            //HoRequest req1 = new HoRequest();
                            ////データの保存
                            //Result res = logic.SaveData(req1, data);
                            //T_HoClient.Key key = data.GetKey();

                            ////データの取得
                            //res = logic.GetData(key, out data);
                            //res = logic.Delete(req1, key);



                            return;
                        }


                        //補充集金指示リスト
                        if (false)
                        {
                            //一覧表示
                            T_HoDayAdapter logic = new T_HoDayAdapter(enreq);
                            PageViewRequest req = new PageViewRequest();
                            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { M_ShopData.南森町店 });
                            //一覧データの取得
                            PageViewResult result = logic.GetOrderList(req);

                            T_HoDay day = new T_HoDay();
                            day.ShopCD = M_ShopData.南森町店;
                            day.HoDate = DateTime.Today;
                            day.Route = "0100";
                            day.Detail = new List<T_HoOrderClient>();
                            day.Detail.AddRange(new T_HoOrderClient[] {
                            new T_HoOrderClient
                            {
                                ShopCD = M_ShopData.南森町店,
                                HoDate = DateTime.Today,
                                TantoCD = M_StaffData.田中担当者,
                                Seq = 1,
                                ClientCD = M_ClientData.ニッシンシステム,
                                DoneFlag = "0",
                                FirstFlag = "1"
                            }
                            }
                            );

                            //HoRequest req1 = new HoRequest();
                            ////データの保存
                            //Result res = logic.SaveData(req1, day);
                            //T_HoDay.Key key = day.GetKey();

                            ////データの取得
                            //res = logic.GetData(key, out day);
                            //res = logic.Delete(req1, key);

                        }

                        if (false)
                        {
                            M_ShopAdapter logic = new M_ShopAdapter(enreq);
                            PageViewRequest req = new PageViewRequest();

                            req.FilterDic.Add(Y_EntityFilterData.ShopTypeFilter, new string[] { "1" });

                            PageViewResult result = logic.GetList(req);
                            return;
                        }

                        if (false)
                        {
                            M_StaffAdapter logic = new M_StaffAdapter(enreq);
                            PageViewRequest req = new PageViewRequest();

                            req.FilterDic.Add(Y_EntityFilterData.StaffNameFilter, new string[] { "田中" });
                            PageViewResult result = logic.GetList(req);

                            return;
                        }


                        if (false)
                        {
                            M_ShopAdapter logic = new M_ShopAdapter(enreq);

                            M_Shop row = new M_Shop();
                            row.ShopCD = "1";
                            row.ShopName = "大阪南森町点";
                            row.ShopTypeCD = "2";
                            row.RegionCD = "01";

                            logic.Add(row);
                        }
                         * */
        }

    }
}
