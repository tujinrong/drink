using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using DrinkService.Data.Models;
using DrinkService.Data.ViewModels;
using DrinkService.Utils;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Logic;
using SafeNeeds.DySmat.Util;

namespace DrinkService.Data.Logics
{
    public class ClientLogic : LogicBase
    {
        public ClientLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }

        /// <summary>
        /// 顧客リストを取得
        /// </summary>
        /// <returns></returns>
        public List<M_Client> GetAllClients(string ShopCD)
        {
            List<M_Client> ClientsList = new List<M_Client>();

            if (string.IsNullOrEmpty(ShopCD))
            {
                ClientsList = db.Clients.ToList();
            }
            else 
            {
                ClientsList = db.Clients.Where(e => e.ShopCD == ShopCD).ToList();
            }
            
            return ClientsList;
        }

        /// <summary>
        /// 顧客ページリストを取得
        /// </summary>
        /// <param name="clientKey">キー</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public PagedResult GetClientPagedListByKeyAndPageNumber(string clientKey,string ShopCD, string pageNumber)
        {

            //List<M_Client> ClientsList = db.Clients.ToList();

            //var Clients = from Client in ClientsList

            //              select Client;

            //if (String.IsNullOrEmpty(clientKey) == false)
            //{
            //    Clients = Clients.Where(p => (p.ClientCD == clientKey) || (p.ClientName.Contains(clientKey)));
            //}

            //if (String.IsNullOrEmpty(ShopCD) == false)
            //{
            //    Clients = Clients.Where(p => p.ShopCD == ShopCD);
            //}

            //int totalSize = Clients.Count();
            //int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            //return new PagedResult<M_Client>(pageSize, Clients.Count(), pNumber, Clients.ToPagedList(pNumber, pageSize));


            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            M_ClientAdapter logic = new M_ClientAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = true;

            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { ShopCD});
            req.FilterDic.Add(Y_EntityFilterData.ReferClientFilter, new string[] {clientKey });
            
            //一覧データの取得
            PageViewResult result = logic.GetClientRefer(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }

        /// <summary>
        /// 顧客リストを取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="clientName">顧客名</param>
        /// <param name="staffCD">担当者</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public PagedResult GetPagedClientList(string shopCD, string clientName, string staffCD, string pageNumber)
        {
            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);
            return GetModels(shopCD, clientName, staffCD, pNumber);
        }

        /// <summary>
        /// 顧客リストを取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="clientName">顧客名</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public List<ClientListViewModel> GetClientList(string shopCD, string clientName)
        {
            List<ClientListViewModel> modelList = new List<ClientListViewModel>();
            PagedResult data = GetModels(shopCD, clientName,null, null);
            data.pageData.ForEach(
                s =>
                {
                    ClientListViewModel model = new ClientListViewModel();
                    model.ShopCD = DataUtil.CStr(s["ShopCD"]);
                    model.ClientCD = DataUtil.CStr(s["ClientCD"]);
                    model.ClientName = DataUtil.CStr(s["ClientName"]);
                    model.StaffName = DataUtil.CStr(s["StaffName"]);
                    model.Tel = DataUtil.CStr(s["Tel"]);
                    modelList.Add(model);
                });

            return modelList;
        }

        /// <summary>
        /// 顧客リストを取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="clientName">顧客名</param>
        /// <param name="staffCD">担当者</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        private PagedResult GetModels(string shopCD, string clientName, string staffCD, int? pageNumber)
        {
            //var Clients = db.Clients.ToList();
            //var Staffs = db.Staffs.ToList();
            //var models = from client in Clients
            //             join staff in Staffs on new { shopCd = client.ShopCD, staffCd = client.TantoCD } equals new { shopCd = staff.ShopCD, staffCd = staff.StaffCD } into g_staff
            //             from c_staff in g_staff.DefaultIfEmpty(new M_Staff())
            //             select new ClientListViewModel
            //             {
            //                 ShopCD = client.ShopCD,
            //                 ClientCD = client.ClientCD,
            //                 ClientName = client.ClientName,
            //                 Tel = client.Tel,
            //                 TantoCD = client.TantoCD,
            //                 StaffName = c_staff.StaffName
            //             };

            //if (string.IsNullOrEmpty(shopCD) == false)
            //{
            //    models = models.Where(m => m.ShopCD == shopCD);
            //}

            //if (string.IsNullOrEmpty(clientName) == false)
            //{
            //    models = models.Where(m => CommonUtils.isContains(m.ClientName, clientName));
            //}

            //return models;

            int pNumber = pageNumber == null ? 1 : pageNumber.Value;

            bool getPageCount = pageNumber == null ? false : true;

            M_ClientAdapter logic = new M_ClientAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = getPageCount;
            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { shopCD });
            req.FilterDic.Add(Y_EntityFilterData.ClientNameFilter, new string[] { clientName });

            if (staffCD != null) {
                req.FilterDic.Add(Y_EntityFilterData.StaffFilter, new string[] { staffCD });
            }

            //一覧データの取得
            PageViewResult result = logic.GetClientList(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }

        /// <summary>
        /// 顧客を取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="clientCD">顧客コード</param>
        /// <returns></returns>
        public M_Client GetClientByKey(string shopCD, string clientCD)
        {
            M_Client client;
            if (string.IsNullOrEmpty(shopCD))
            {
                client = db.Clients.Where(e => e.ClientCD == clientCD).FirstOrDefault();
            }
            else
            {
                client = db.Clients.Find(shopCD, clientCD);
            }

            if (client == null)
                return new M_Client();
            else
                return client;
        }

        /// <summary>
        /// 顧客データを取得
        /// </summary>
        /// <param name="shopCD"></param>
        /// <param name="clientCD"></param>
        /// <returns></returns>
        public object GetClientEditData(string shopCD, string clientCD)
        {
            M_Client client = null;

            M_ClientAdapter adapter = new M_ClientAdapter(_enreq);
            Result result = adapter.GetData(new M_Client.Key() { ShopCD = shopCD, ClientCD = clientCD }, out client);

            List<M_Item> items = db.Items.ToList();

            List<M_ClientRoute> clientRoute = client.RouteDetail;

            List<ClientEditRouteViewModel> clientRouteList = GetInitRoute();

            foreach (var item in clientRoute)
            {
                string weekNo = item.WeekNo.ToString();
                string routeNo = item.RouteNo.ToString();

               var route = (from clientRouteItem in clientRouteList where(clientRouteItem.WeekNo == weekNo) select clientRouteItem).FirstOrDefault();

               switch (routeNo)
               {
                   case "1":
                       route.Rule1 = item.Route;
                       break;
                   case "2":
                       route.Rule2 = item.Route;
                       break;
                   case "3":
                       route.Rule3 = item.Route;
                       break;
               }
            }

            var clientInitItems = from t in client.clientInitItemDetail
                                    join item in items on t.ItemCD equals item.ItemCD into g_item
                                    from t_item in g_item.DefaultIfEmpty(new M_Item())
                                    select new ClientInitItemViewModel
                                    {
                                        ShelfCD = t.ShelfCD,
                                        ItemCD = t.ItemCD,
                                        ShortName = t_item.ShortName,
                                        Num = t.Num,
                                        Price = t.Price
                                    };

            var hoClientItems = from t in client.HoDetail
                                    join item in items on t.ItemCD equals item.ItemCD into g_item
                                    from t_item in g_item.DefaultIfEmpty(new M_Item())
                                orderby t.ItemAddFlag, t.ShelfNo, t.ShelfSubNo
                                    select new HoClientItemViewModel
                                    {
                                        ShopCD = t.ShopCD,                      //店舗コード
                                        ClientCD = t.ClientCD,                  //顧客コード
                                        Seq = DataUtil.CStr(t.Seq),                               //補充回数
                                        ItemCD = t.ItemCD,                      //補充回数
                                        ShelfNo = DataUtil.CStr(t.ShelfNo),                    //棚:店舗初期設定情報より
                                        ShelfSubNo = DataUtil.CStr(t.ShelfSubNo),                       //順:は０と表示。
                                        PrevNum = DataUtil.CStr(t.PrevNum),                           //前回在庫数
                                        ThisNum = DataUtil.CStr(t.ThisNum),         //今回在庫数:は初期在庫集計値を表示のみとする。
                                        AddNum = DataUtil.CStr(t.AddNum),                            //補充数
                                        BeforeNum = DataUtil.CStr(t.BeforeNum),                         //補充前数
                                        UsedNum = DataUtil.CStr(t.UsedNum),                           //使用数
                                        AfterNum = DataUtil.CStr(t.AfterNum),                          //補充後数
                                        Price = DataUtil.CStr(t.Price),         //単価
                                        Money = DataUtil.CStr(t.Money),                             //金額
                                        FreshDate = DataUtil.CStr(t.FreshDate),                         //賞味期限
                                        NextPrice = DataUtil.CStr(t.NextPrice),     //次回単価
                                        NextStopFlag = DataUtil.CStr(t.NextStopFlag),                      //次回中止
                                        ItemsName = t_item.ShortName,             //商品名
                                        ItemAddFlag = DataUtil.CStr(t.ItemAddFlag)
                                        
                                    };

            T_HoClient hoClient = db.HoClients.Find(shopCD, clientCD, client.LastSeq);

            string hoDate = string.Empty;
            if (hoClient != null)
            {
               hoDate = hoClient.HoDate.ToString("yyyy/MM/dd");
            }

            string firstDay = "";
            var query = from c in db.HoOrderClients
                        where c.ShopCD == shopCD && c.ClientCD == clientCD
                                          group c by c.ClientCD into cg 
                                          let minId = cg.Min(a => a.HoDate)
                                          from row in cg
                        where row.HoDate == minId
                                          select row;

            if (query.Count() > 0)
            {
                firstDay = query.First().HoDate.ToString("yyyy/MM/dd");
            }


            List<HoClientItemViewModel> hoClientItemsList = hoClientItems.ToList();
            List<HoClientItemViewModel> hoClientItemsAdd = new List<HoClientItemViewModel>();

            for (int i = hoClientItemsList.Count - 1; i >= 0; i--)
            {
                HoClientItemViewModel item = hoClientItemsList[i];

                if (item.ItemAddFlag == "2")
                {

                    hoClientItemsAdd.Insert(0, item);
                    hoClientItemsList.Remove(item);
                }
            }
            string hoClientUpdateTime = "";
            if (hoClient != null)
            {
                hoClientUpdateTime = hoClient.UpdateTime == null ? "" : hoClient.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss fff");
            }
            string clientUpdateTime = client.UpdateTime == null ? "" : client.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss fff");
            return new { hoDate = hoDate, clientRoute = clientRouteList, clientInitItems = clientInitItems.ToList(), hoClientItems = hoClientItemsList, hoClientItemsAdd = hoClientItemsAdd, hoClient = client.HoClient, hoClientUpdateTime = hoClientUpdateTime,clientUpdateTime =clientUpdateTime, firstDay = firstDay };
        }

        /// <summary>
        /// ルート初期化データを取得
        /// </summary>
        /// <returns></returns>
        private List<ClientEditRouteViewModel> GetInitRoute()
        {
            return new List<ClientEditRouteViewModel>()
            {
                new ClientEditRouteViewModel(){ WeekNo ="1", WeekName ="A週", Rule1 ="", Rule2 ="", Rule3 =""},
                new ClientEditRouteViewModel(){ WeekNo ="2", WeekName ="B週", Rule1 ="", Rule2 ="", Rule3 =""},
                new ClientEditRouteViewModel(){ WeekNo ="3", WeekName ="C週", Rule1 ="", Rule2 ="", Rule3 =""},
                new ClientEditRouteViewModel(){ WeekNo ="4", WeekName ="D週", Rule1 ="", Rule2 ="", Rule3 =""}
            };
        }
        /// <summary>
        /// 顧客を取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="clientName">顧客名</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public M_Client GetClient(string shopCD, string clientCD)
        {
            return db.Clients.Find(shopCD, clientCD);
        }

        /// <summary>
        /// 顧客を保存する
        /// </summary>
        /// <param name="_client">顧客</param>
        public Result Save(M_Client _client, T_HoClient hoClient,string hoClientUpdateTime,string clientUpdateTime, string FirstDate,string AfterDate, List<ClientEditRouteViewModel> routeList, List<M_ClientInitItems> clientInitItemAddList, List<M_ClientInitItems> clientInitItemDelList, List<M_ClientInitItems> clientInitItemUpdateList, List<T_HoClientItem> hoClientUpdateList, List<T_HoClientItem> hoClientAddList, List<T_HoClientItem> hoClientDelList, bool newMode)
        {
            if (FirstDate != null)
            {
                _client.FirstDate = DateTime.ParseExact(FirstDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (AfterDate != null)
            {
                _client.AfterDate = DateTime.ParseExact(AfterDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }

            List<M_ClientRoute> clientRouteList = new List<M_ClientRoute>();
            List<M_ClientInitItems> clientInitList = new List<M_ClientInitItems>();
            List<M_ClientInitItems> clientInitDelList = new List<M_ClientInitItems>();
            List<T_HoClientItem> hoclientList = new List<T_HoClientItem>();

            if (routeList != null)
            {
                foreach (var item in routeList)
                {
                    M_ClientRoute clientRoute = new M_ClientRoute();

                    if (item.Rule1 != null)
                    {
                        clientRoute.ShopCD = _client.ShopCD;
                        clientRoute.ClientCD = _client.ClientCD;
                        clientRoute.WeekNo = short.Parse(item.WeekNo);
                        clientRoute.RouteNo = short.Parse("1");
                        clientRoute.Route = item.Rule1;
                        clientRouteList.Add(clientRoute);
                    }

                    if (item.Rule2 != null)
                    {
                        clientRoute = new M_ClientRoute();
                        clientRoute.ShopCD = _client.ShopCD;
                        clientRoute.ClientCD = _client.ClientCD;
                        clientRoute.WeekNo = short.Parse(item.WeekNo);
                        clientRoute.RouteNo = short.Parse("2");
                        clientRoute.Route = item.Rule2;
                        clientRouteList.Add(clientRoute);
                    }
                    if (item.Rule3 != null)
                    {
                        clientRoute = new M_ClientRoute();
                        clientRoute.ShopCD = _client.ShopCD;
                        clientRoute.ClientCD = _client.ClientCD;
                        clientRoute.WeekNo = short.Parse(item.WeekNo);
                        clientRoute.RouteNo = short.Parse("3");
                        clientRoute.Route = item.Rule3;
                        clientRouteList.Add(clientRoute);
                    }
                }
            }

            if (clientInitItemAddList != null)
            {
                foreach (M_ClientInitItems item in clientInitItemAddList)
                {
                    item.ShopCD = _client.ShopCD;
                    item.ClientCD = _client.ClientCD;
                }
                clientInitList.AddRange(clientInitItemAddList);
            }

            if (clientInitItemDelList != null)
            {
                foreach (M_ClientInitItems item in clientInitItemDelList)
                {
                    item.ShopCD = _client.ShopCD;
                    item.ClientCD = _client.ClientCD;
                }
                clientInitDelList.AddRange(clientInitItemDelList);
            }

            if (clientInitItemUpdateList != null)
            {
                foreach (M_ClientInitItems item in clientInitItemUpdateList)
                {
                    item.ShopCD = _client.ShopCD;
                    item.ClientCD = _client.ClientCD;
                }
                clientInitList.AddRange(clientInitItemUpdateList);
            }


            if (hoClientUpdateList != null)
            {
                foreach (T_HoClientItem item in hoClientUpdateList)
                {
                    item.ShopCD = _client.ShopCD;
                    item.ClientCD = _client.ClientCD;
                    item.Seq = _client.LastSeq;
                }
                hoclientList.AddRange(hoClientUpdateList);
            }

            if (hoClientAddList != null)
            {
                foreach (T_HoClientItem item in hoClientAddList)
                {
                    item.ShopCD = _client.ShopCD;
                    item.ClientCD = _client.ClientCD;
                    item.Seq = _client.LastSeq;
                }
                hoclientList.AddRange(hoClientAddList);
            }

            _client.RouteDetail = clientRouteList;
            _client.InitItemDetail = clientInitList;
            _client.HoDetail = hoclientList;

            M_ClientAdapter adapter = new M_ClientAdapter(_enreq);

            try
            {
                return adapter.SaveData(new HoRequest() { User = _enreq.User }, _client, hoClient, hoClientUpdateTime,clientUpdateTime, clientInitDelList, hoClientDelList, newMode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 最新T_HoOrderClientを取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="clientCD">clientコード</param>
        /// <param name="routeCD"></param>
        /// <returns></returns>
        public T_HoOrderClient GetLastOrderClient(string shopCD, string clientCD)
        {

            M_ClientAdapter adapter = new M_ClientAdapter(_enreq);

            return adapter.GetLastOrderClient(shopCD, clientCD);
        }

        public PagedResult GetAutoComplete(string clientKey, string ShopCD)
        {
            int pNumber = 1;

            M_ClientAdapter logic = new M_ClientAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.PageRows = 10;
            req.GetPageCount = true;

            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { ShopCD });
            req.FilterDic.Add(Y_EntityFilterData.ReferClientFilter, new string[] { clientKey });

            //一覧データの取得
            PageViewResult result = logic.GetClientRefer(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }
    }
}
