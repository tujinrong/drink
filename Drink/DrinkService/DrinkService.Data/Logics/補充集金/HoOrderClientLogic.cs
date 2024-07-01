using DrinkService.Data.Models;
using DrinkService.Data.ViewModels;
using DrinkService.Models;
using DrinkService.Utils;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Logic;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Transactions;

namespace DrinkService.Data.Logics
{
    public class HoOrderClientLogic :LogicBase
    {
        public HoOrderClientLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }
        /// <summary>
        /// 補充集金リストを取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="staffCD">担当者コード</param>
        /// <param name="route">ルート</param>
        /// <param name="date">日付</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public PagedResult GetPagedDeliveryRouteList(string shopCD, string staffCD, string route, string hoDate, string doneFlag, string LastFlag, string pageNumber)
        {
            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            T_HoDayAdapter logic = new T_HoDayAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = true;

            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { shopCD });
            req.FilterDic.Add(Y_EntityFilterData.StaffFilter, new string[] { staffCD });
            req.FilterDic.Add(Y_EntityFilterData.OrderTantoFilter, new string[] { route });
            req.FilterDic.Add(Y_EntityFilterData.HoDateFilter, new string[] { hoDate });
            req.FilterDic.Add(Y_EntityFilterData.LastEmptyFilter, new string[] { LastFlag });
            req.FilterDic.Add(Y_EntityFilterData.DoneFilter, new string[] { doneFlag });
            
            
            //一覧データの取得
            PageViewResult result = logic.GetOrderList(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }

        /// <summary>
        /// 補充集金照会を取得
        /// </summary>
        /// <returns></returns>
        public PagedResult GetPagedDeliveryRouteRefer(string shopCD, string clientCD, string staffCD, string route, string hoDateFrom, string hoDateTo, string LastFlag, string pageNumber)
        {
            //var models = GetModels(shopCD, staffCD, route, hoDate);

            //int totalSize = models.Count();
            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            T_HoDayAdapter logic = new T_HoDayAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = true;

            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { shopCD });
            req.FilterDic.Add(Y_EntityFilterData.StaffFilter, new string[] { staffCD });
            req.FilterDic.Add(Y_EntityFilterData.OrderTantoFilter, new string[] { route });
            req.FilterDic.Add(Y_EntityFilterData.LastFilter, new string[] { LastFlag });
            req.FilterDic.Add(Y_EntityFilterData.HoDateFromFilter, new string[] { hoDateFrom });
            req.FilterDic.Add(Y_EntityFilterData.HoDateToFilter, new string[] { hoDateTo });
            req.FilterDic.Add(Y_EntityFilterData.ClientFilter, new string[] { clientCD });
            //req.FilterDic.Add(Y_EntityFilterData.DoneFilter, new string[] { "2,1,3" });
            req.FilterDic.Add(Y_EntityFilterData.DoneStopFilter, new string[] { "2,1,3" });
            
            //一覧データの取得
            PageViewResult result = logic.GetOrderRefer(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }

        public PagedResult GetPagedDeliveryRouteUndoRefer(string shopCD, string clientCD, string staffCD, string route, string hoDateFrom, string hoDateTo, string LastFlag, string pageNumber)
        {
            //var models = GetModels(shopCD, staffCD, route, hoDate);

            //int totalSize = models.Count();
            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            T_HoDayAdapter logic = new T_HoDayAdapter(_enreq);

            PageViewRequest req = new PageViewRequest();
            req.PageNo = pNumber;
            req.GetPageCount = true;

            req.FilterDic.Add(Y_EntityFilterData.ShopFilter, new string[] { shopCD });
            req.FilterDic.Add(Y_EntityFilterData.StaffFilter, new string[] { staffCD });
            req.FilterDic.Add(Y_EntityFilterData.OrderTantoFilter, new string[] { route });
            req.FilterDic.Add(Y_EntityFilterData.LastFilter, new string[] { LastFlag });
            req.FilterDic.Add(Y_EntityFilterData.HoDateFromFilter, new string[] { hoDateFrom });
            req.FilterDic.Add(Y_EntityFilterData.HoDateToFilter, new string[] { hoDateTo });
            req.FilterDic.Add(Y_EntityFilterData.ClientFilter, new string[] { clientCD });
            //req.FilterDic.Add(Y_EntityFilterData.DoneFilter, new string[] { "2,1,3" });
            req.FilterDic.Add(Y_EntityFilterData.UndoFilter, new string[] { "0,1" });

            //一覧データの取得
            PageViewResult result = logic.GetUndoOrderRefer(req);

            DyEntityLogic dyLogic = new DyEntityLogic();

            return new PagedResult(pageSize, result.PageCount, pNumber, dyLogic.DataTableToDic(result.DataTable));
        }

        /// <summary>
        /// 補充集金リストを取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="staffCD">担当者コード</param>
        /// <param name="route">ルート</param>
        /// <param name="date">日付</param>
        /// <returns></returns>
        private IEnumerable<DeliveryRouteListViewModel> GetModels(string shopCD, string staffCD, string route, string hoDate)
        {
            var HoOrderClients = db.HoOrderClients.ToList();
            var Staffs = db.Staffs.ToList();
            var Clients = db.Clients.ToList();
            
            var models = from hoOrderClient in HoOrderClients
                         join staff in Staffs on hoOrderClient.TantoCD equals staff.StaffCD into g_order_staff
                         from h_staff in g_order_staff.DefaultIfEmpty(new M_Staff())
                         join client in Clients on hoOrderClient.ClientCD equals client.ClientCD into g_order_client
                         from h_client in g_order_client.DefaultIfEmpty(new M_Client())
                         select new DeliveryRouteListViewModel
                         {
                            FirstFlag = hoOrderClient.FirstFlag,
                            DoneFlag = hoOrderClient.DoneFlag,
                            TantoCD = hoOrderClient.TantoCD,
                            StaffName = h_staff.StaffName,
                            ShopCD = hoOrderClient.ShopCD,
                            ClientCD = hoOrderClient.ClientCD,
                            ClientName = h_client.ClientName,
                            HoDate = hoOrderClient.HoDate,
                            Address = h_client.Address
                         };
   
            if (string.IsNullOrEmpty(shopCD) == false)
            {
                models = models.Where(m => m.ShopCD == shopCD);
            }

            if (string.IsNullOrEmpty(staffCD) == false)
            {
                models = models.Where(m => m.TantoCD == staffCD);
            }

            return models; 
        }

        /// <summary>
        /// 顧客リストを取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="staffCD">担当者</param>
        /// <param name="routeCD"></param>
        /// <returns></returns>
        public List<DeliveryRouteClientViewModel> GetClientList(string shopCD, string staffCD, string routeCD, string hoDate, out  List<DeliveryRouteClientViewModel> dataSaved)
        {
            var Staffs = db.Staffs.Where(e => e.ShopCD == shopCD).ToList();

            List<M_Client> Clients = db.Clients.Where(e => e.ShopCD == shopCD).ToList();
           
            DateTime dt = DateTime.ParseExact(hoDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            List<T_HoOrderClient> HoOrderClients = db.HoOrderClients.Where(e => e.ShopCD == shopCD && e.HoDate == dt).ToList();

            //分组排序，取时间最大的一条记录
            var HoBeforeDayClientsQuary = from c in db.HoOrderClients
                                         where c.ShopCD == shopCD && c.HoDate < dt
                                         group c by c.ClientCD into cg
                                         let maxId = cg.Max(a => a.HoDate)
                                         from row in cg
                                         where row.HoDate == maxId
                                         select row;

            //分组排序，取时间最大的一条记录
            var HoAfterDayClientsQuary = from c in db.HoOrderClients
                                          where c.ShopCD == shopCD && c.HoDate >= dt
                                          group c by c.ClientCD into cg
                                          let maxId = cg.Max(a => a.HoDate)
                                          from row in cg
                                          where row.HoDate == maxId
                                          select row;

            List<T_HoOrderClient> HoBeforeDayClients = HoBeforeDayClientsQuary.ToList();
            List<T_HoOrderClient> HoAfterDayClients = HoAfterDayClientsQuary.ToList();

            var models = from client in Clients
                         join staff in Staffs on client.TantoCD equals staff.StaffCD into g_client_staff
                         from c_staff in g_client_staff.DefaultIfEmpty(new M_Staff())

                         join hoOrderClient in HoOrderClients on new { shopCD = client.ShopCD, clientCD = client.ClientCD } equals new { shopCD = hoOrderClient.ShopCD, clientCD = hoOrderClient.ClientCD } into g_client_order
                         from c_order in g_client_order.DefaultIfEmpty(new T_HoOrderClient())

                         //join tanto in oldTantos on new { HoDate = dt, TantoCD = client.TantoCD } equals new { HoDate =tanto.HoDate, TantoCD = tanto.TantoCD } into g_client_tanto
                         //from c_tanto in g_client_tanto.DefaultIfEmpty(null)


                         join beforeClient in HoBeforeDayClients on new { clientCD = client.ClientCD } equals new { clientCD = beforeClient.ClientCD } into g_client_before
                         from c_before_order in g_client_before.DefaultIfEmpty(null)

                         join afterClient in HoAfterDayClients on new { clientCD = client.ClientCD } equals new { clientCD = afterClient.ClientCD } into g_client_after
                         from c_after_order in g_client_after.DefaultIfEmpty(null)

                         where string.IsNullOrEmpty(c_order.ClientCD)

                         orderby client.TantoCD

                         select new DeliveryRouteClientViewModel
                         {
                             ShopCD = client.ShopCD,
                             ClientCD = client.ClientCD,
                             ClientName = client.ClientName,
                             StaffCD = client.TantoCD,
                             Route = c_order.Route == null ? "" : c_order.Route,
                             StaffName = c_staff.StaffName,
                             FirstDate = client.FirstDate == null ? "" : string.Format("{0:yyyyMMdd}", client.FirstDate),
                             LastDate = c_after_order == null ? "" : string.Format("{0:yyyyMMdd}", c_after_order.HoDate),
                             FirstFlag = c_before_order == null ? "1" : "0",
                             DoneFlag = c_before_order == null ? "" : c_before_order.DoneFlag,
                             DoneFlagSaved = "",
                             StaffCDPlan = client.TantoCD,
                             StaffNamePlan = c_staff.StaffName,
                             AfterDate = (c_before_order == null || c_before_order.AfterDate == null) ? "" : string.Format("{0:yyyyMMdd}", c_before_order.AfterDate),
                             AfterStopFlag = c_before_order == null ? "" : c_before_order.AfterStopFlag

                         };

            var savedModels = from c in HoOrderClients

                              join staff in Staffs on new { TantoCD = c.TantoCD, ShopCD = c.ShopCD } equals new { TantoCD = staff.StaffCD, ShopCD = staff.ShopCD } into g_client_staff
                              from c_staff in g_client_staff.DefaultIfEmpty(new M_Staff())

                              //join tanto in oldTantos on new { HoDate = c.HoDate, ShopCD = c.ShopCD, TantoCD = c.TantoCD } equals new { HoDate = tanto.HoDate, ShopCD = tanto.ShopCD, TantoCD = tanto.TantoCD } into g_client_tanto
                              //from c_tanto in g_client_tanto.DefaultIfEmpty(new T_HoOrderTanto())

                              join client in Clients on new { clientCD = c.ClientCD } equals new { clientCD = client.ClientCD } into g_client
                              from c_client in g_client.DefaultIfEmpty(new M_Client())

                              join beforeClient in HoBeforeDayClients on new { clientCD = c.ClientCD } equals new { clientCD = beforeClient.ClientCD } into g_client_before
                              from c_before_order in g_client_before.DefaultIfEmpty(null)

                              join staff in Staffs on c_client.TantoCD equals staff.StaffCD into g_client_staff_p
                              from p_staff in g_client_staff_p.DefaultIfEmpty(new M_Staff())


                              orderby c.TantoCD

                              select new DeliveryRouteClientViewModel
                              {
                                  ShopCD = c.ShopCD,
                                  ClientCD = c.ClientCD,
                                  ClientName = c_client.ClientName,
                                  StaffCD = c.TantoCD,
                                  Route = c.Route,
                                  FirstDate = c_client.FirstDate == null ? "" : string.Format("{0:yyyyMMdd}", c_client.FirstDate),
                                  StaffName = c_staff.StaffName,
                                  FirstFlag = c.FirstFlag,
                                  LastDate = "",
                                  DoneFlag = "",
                                  DoneFlagSaved = c.DoneFlag,
                                  StaffCDPlan = c_client.TantoCD,
                                  StaffNamePlan = p_staff.StaffName,
                                  AfterDate = (c_before_order == null ||c_before_order.AfterDate == null) ? "" : string.Format("{0:yyyyMMdd}", c_before_order.AfterDate),
                                  AfterStopFlag = c_before_order == null ? "" : c_before_order.AfterStopFlag
                              };

            dataSaved = savedModels.ToList();

            return models.ToList();
        }

        /// <summary>
        /// 顧客リストを取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="staffCD">担当者</param>
        /// <param name="routeCD"></param>
        /// <returns></returns>
        public string CheckPasedPlanDone(string shopCD, string staffCD, string routeCD, string hoDate)
        {
            
            List<M_Client> Clients = db.Clients.Where(e => e.ShopCD == shopCD).ToList();

            DateTime dt = DateTime.ParseExact(hoDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            //分组排序，取时间最大的一条记录
            var HoBeforeDayClientsQuary = from c in db.HoOrderClients
                                          where c.ShopCD == shopCD && c.HoDate < dt && c.DoneFlag != "1" && c.DoneFlag != "3"
                                          group c by c.ClientCD into cg
                                          let maxId = cg.Max(a => a.HoDate)
                                          from row in cg
                                          where row.HoDate == maxId
                                          select row;

            List<T_HoOrderClient> HoBeforeDayClients = HoBeforeDayClientsQuary.ToList();


            string days = "";

            foreach (T_HoOrderClient c in HoBeforeDayClients) {
                if (days == "") {
                    days = "【" +c.HoDate.ToString("yyyy/MM/dd")+ "】";
                }
                else 
                {
                    if (days.Contains(c.HoDate.ToString("yyyy/MM/dd")) == false) {
                        days += System.Environment.NewLine + "【" + c.HoDate.ToString("yyyy/MM/dd") + "】";
                    }
                }
            }

            return days;
        }

        /// <summary>
        /// 顧客リストを取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="staffCD">担当者</param>
        /// <param name="routeCD"></param>
        /// <returns></returns>
        public string CheckPasedPlanDoneRange(string shopCD, string hoDateFrom, string hoDateTo)
        {
            if (string.IsNullOrEmpty(hoDateFrom)) {
                hoDateFrom = "18990101";
            }
            if (string.IsNullOrEmpty(hoDateTo))
            {
                hoDateTo = "28990101";
            }
            List<M_Client> Clients = db.Clients.Where(e => e.ShopCD == shopCD).ToList();

            DateTime dtFrom = DateTime.ParseExact(hoDateFrom, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtTo = DateTime.ParseExact(hoDateTo, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            //分组排序，取时间最大的一条记录
            var HoBeforeDayClientsQuary = from c in db.HoOrderClients
                                          where 
                                            c.ShopCD == shopCD 
                                            && c.HoDate <= dtTo 
                                            && c.HoDate >= dtFrom 
                                            && c.DoneFlag != "1" 
                                            && c.DoneFlag != "3" 
                                            && c.AfterStopFlag!="1"
                                            && c.AfterStopFlag != "2"
                                          group c by c.ClientCD into cg
                                          let maxId = cg.Max(a => a.HoDate)
                                          from row in cg
                                          where row.HoDate == maxId
                                          select row;

            List<T_HoOrderClient> HoBeforeDayClients = HoBeforeDayClientsQuary.ToList();


            string days = "";

            foreach (T_HoOrderClient c in HoBeforeDayClients) {
                if (days == "") {
                    days = "【" +c.HoDate.ToString("yyyy/MM/dd")+ "】";
                }
                else 
                {
                    if (days.Contains(c.HoDate.ToString("yyyy/MM/dd")) == false) {
                        days += System.Environment.NewLine + "【" + c.HoDate.ToString("yyyy/MM/dd") + "】";
                    }
                }
            }

            if (days == "") {

                string sql = @"
select 

--top 10 

distinct
T_HoClientItem.ShopCD
,T_HoClientItem.ClientCD
,T_HoOrderClient.HoDate
,T_HoOrderClient.FirstFlag
,T_HoClient.UpdateUser
,T_HoClient.UpdateTime
 from T_HoClientItem 

left join T_HoClient 
on T_HoClient.ShopCD = T_HoClientItem.ShopCD
and  T_HoClient.ClientCD = T_HoClientItem.ClientCD
and  T_HoClient.Seq = T_HoClientItem.Seq

left join T_HoOrderClient 
on T_HoClient.ShopCD = T_HoOrderClient.ShopCD
and  T_HoClient.ClientCD = T_HoOrderClient.ClientCD
and  T_HoClient.HoDate = T_HoOrderClient.HoDate
and  T_HoClient.TantoCD = T_HoOrderClient.TantoCD


where (T_HoClientItem.AfterNum is null or (T_HoClientItem.ThisNum is null and T_HoClientItem.ItemAddFlag <> '1'))
and T_HoClientItem.ItemAddFlag <> '2' 
and T_HoOrderClient.HoDate = '{0}'
and T_HoClientItem.ShopCD = '{1}'
";

                DataTable dt = SQLHelper.GetDataTable(string.Format(sql, dtFrom.ToString("yyyy/MM/dd"), shopCD));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows) {
                        if (days == "")
                        {
                            days = "顧客:【" + row["ClientCD"] + "】";
                        }
                        else
                        {
                            days += ",【" + row["ClientCD"] + "】";
                        }
                    }
                }
            }

            return days;
        }
        

        /// <summary>
        /// 顧客リストを取得
        /// </summary>
        /// <param name="shopCD">店舗コード</param>
        /// <param name="staffCD">担当者</param>
        /// <param name="routeCD"></param>
        /// <returns></returns>
        public string CheckPasedPlanBefore(string shopCD, string staffCD, string routeCD, string hoDate)
        {

            List<M_Client> Clients = db.Clients.Where(e => e.ShopCD == shopCD).ToList();

            DateTime dt = DateTime.ParseExact(hoDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            var query = from c in db.HoOrderClients
                                          where c.ShopCD == shopCD
                        orderby c.HoDate descending
                                          select c;

            T_HoOrderClient first = null;
            if (query.Count() > 0) {
                first = query.First();
            }
            
            string days = "";

            if (first != null && first.HoDate > dt)
            {
                days = "【" + first.HoDate.ToString("yyyy/MM/dd") + "】";
            }

            return days;
        }

        public List<T_HoOrderClient> GetOldDayClient(string shopCD, DateTime hoDate)
        {
            List<T_HoOrderClient> oldTantos = db.HoOrderClients.Where(e => e.ShopCD == shopCD && e.HoDate == hoDate).ToList();

            return oldTantos;
        }

        public T_HoOrderClient GetHoClientModel(string ShopCD, DateTime HoDate, string TantoCD, string ClientCD,string Route)
        {
            return db.HoOrderClients.Find(ShopCD, HoDate, TantoCD, ClientCD, Route);
        }

        public List<HoClientItemViewModel> GetHoClientItems(string ShopCD, string ClientCD, DateTime HoDate, string FirstFlag, string TantoCD,string Route)
        {
            List<HoClientItemViewModel> hoClientItems = new List<HoClientItemViewModel>();

            M_Client client = null;

            List<M_Item> items = db.Items.ToList();

            M_ClientAdapter clientLogic = new M_ClientAdapter(_enreq);

            clientLogic.GetData(new M_Client.Key { ShopCD = ShopCD, ClientCD = ClientCD }, out client);


            T_HoOrderClient orderClient = GetHoClientModel(ShopCD, HoDate, TantoCD, ClientCD,Route);

            if (orderClient == null) {
                return hoClientItems;
            }

            if (orderClient.Seq == 0)
            {
                //new
                //初回
                if (FirstFlag == "1")
                {
                    //・店舗初期設定情報より、商品を集計して表示。
                    var initItems = from t in client.InitItemDetail

                                    join item in items on t.ItemCD equals item.ItemCD into g_item
                                    from t_item in g_item.DefaultIfEmpty(new M_Item())

                                    orderby t.ShelfCD ,t.ItemCD

                                    select new HoClientItemViewModel
                                    {
                                        ShopCD = t.ShopCD,                      //店舗コード
                                        ClientCD = t.ClientCD,                  //顧客コード
                                        Seq = "",                               //補充回数
                                        ItemCD = t.ItemCD,                      //補充回数
                                        ShelfNo = t.ShelfCD,                    //棚:店舗初期設定情報より
                                        ShelfSubNo = "0",                       //順:は０と表示。
                                        PrevNum = "",                           //前回在庫数
                                        ThisNum = DataUtil.CStr(t.Num),         //今回在庫数:は初期在庫集計値を表示のみとする。
                                        AddNum = DataUtil.CStr(t.Num),          //補充数
                                        BeforeNum = "",                         //補充前数
                                        UsedNum = "",                           //使用数
                                        AfterNum = DataUtil.CStr(t.Num),        //補充後数
                                        Price = DataUtil.CStr(t.Price),         //単価
                                        Money = "",                             //金額
                                        FreshDate = "",                         //賞味期限
                                        NextPrice = DataUtil.CStr(t.Price),     //次回単価
                                        NextStopFlag = "",                      //次回中止
                                        SaleFlag = "",                          //売れるマーク
                                        ItemsName = t_item.ShortName,           //商品名
                                        ItemAddFlag = "0"                       //商品追加

                                    };

                    hoClientItems = initItems.ToList();
                }
                else
                {
                    //update
                    var prePreItems = from ppi in db.HoClientItems
                                      where ppi.ShopCD == orderClient.ShopCD
                                         && ppi.ClientCD == orderClient.ClientCD
                                         && ppi.Seq == (client.LastSeq - 1)
                                      select ppi;

                    var preItems = from pi in db.HoClientItems
                                   where pi.ShopCD == orderClient.ShopCD
                                     && pi.ClientCD == orderClient.ClientCD
                                     && pi.Seq == (client.LastSeq)
                                     //&& pi.AfterNum != 0
                                   select pi;

                    List<T_HoClientItem> prePreList = prePreItems.ToList();
                    List<T_HoClientItem> preList = preItems.ToList();

                    var clientItems = from pre_item in preList

                                      join item in items on pre_item.ItemCD equals item.ItemCD into g_item
                                      from t_item in g_item.DefaultIfEmpty(new M_Item())

                                      join prePreItem in prePreList on pre_item.ItemCD equals prePreItem.ItemCD into g_prePreItem
                                      from pre_pre_item in g_prePreItem.DefaultIfEmpty(new T_HoClientItem())

                                      where (pre_item != null && pre_pre_item != null && pre_item.AfterNum == 0 && pre_pre_item.AfterNum == 0)==false

                                      orderby pre_item.ShelfNo, pre_item.ShelfSubNo,pre_item.ItemCD

                                      select new HoClientItemViewModel
                                      {
                                          ShopCD = pre_item.ShopCD,                                                 //店舗コード
                                          ClientCD = pre_item.ClientCD,                                             //顧客コード
                                          Seq = "",                                                                 //補充回数
                                          ItemCD = DataUtil.CStr(pre_item.ItemCD),                                  //補充回数
                                          ShelfNo = DataUtil.CStr(pre_item.ShelfNo),                                //棚:
                                          ShelfSubNo = DataUtil.CStr(pre_item.ShelfSubNo),                          //順:
                                          PrevNum = DataUtil.CStr(pre_item.AfterNum),                               //前回在庫数
                                          ThisNum = "",                                                             //今回在庫数。
                                          AddNum = "",                                                              //補充数
                                          BeforeNum = DataUtil.CStr(pre_item.AddNum),                               //補充前数
                                          UsedNum = "",                                                             //使用数
                                          AfterNum = pre_item.NextStopFlag ==1? "0":"",                             //補充後数
                                          Price = DataUtil.CStr(pre_item.NextPrice),                                //単価
                                          Money = "",                                                               //金額
                                          FreshDate = DataUtil.CStr(pre_item.FreshDate),                            //賞味期限
                                          NextPrice = DataUtil.CStr(pre_item.NextPrice),                            //次回単価
                                          NextStopFlag = "",                                                        //次回中止
                                          SaleFlag = pre_pre_item.UsedNum == 0 && pre_item.UsedNum == 0 ? "1" : "", //売れるマーク
                                          ItemsName = t_item.ShortName,                                             //商品名
                                          ItemAddFlag = "0"                                                         //商品追加

                                      };
                    hoClientItems = clientItems.ToList();
                }
            }
            else 
            { 
                var nowItems = from ni in db.HoClientItems
                               where ni.ShopCD == orderClient.ShopCD
                                 && ni.ClientCD == orderClient.ClientCD
                                 && ni.Seq == (orderClient.Seq)
                               select ni;

                List<T_HoClientItem> nowList = nowItems.OrderBy(r => r.ItemAddFlag).ThenBy(r => r.ShelfNo).ThenBy(r => r.ShelfSubNo).ThenBy(r=>r.ItemCD).ToList();

                var clientItems = from i in nowList

                                  join item in items on i.ItemCD equals item.ItemCD into g_item
                                  from t_item in g_item.DefaultIfEmpty(new M_Item())

                                  where i.ItemAddFlag != 2

                                  select new HoClientItemViewModel
                                  {
                                      ShopCD = i.ShopCD,                                                        //店舗コード
                                      ClientCD = i.ClientCD,                                                    //顧客コード
                                      Seq = DataUtil.CStr(i.Seq),                                               //補充回数
                                      ItemCD = DataUtil.CStr(i.ItemCD),                                         //補充回数
                                      ShelfNo = DataUtil.CStr(i.ShelfNo),                                       //棚:
                                      ShelfSubNo = DataUtil.CStr(i.ShelfSubNo),                                 //順:
                                      PrevNum = DataUtil.CStr(i.PrevNum),                                       //前回在庫数
                                      ThisNum = DataUtil.CStr(i.ThisNum),                                       //今回在庫数。
                                      AddNum = DataUtil.CStr(i.AddNum),                                         //補充数
                                      BeforeNum = DataUtil.CStr(i.BeforeNum),                                   //補充前数
                                      UsedNum = DataUtil.CStr(i.UsedNum),                                       //使用数
                                      AfterNum = DataUtil.CStr(i.AfterNum),                                     //補充後数
                                      Price = DataUtil.CStr(i.Price),                                           //単価
                                      Money = DataUtil.CStr(i.Money),                                           //金額
                                      FreshDate = DataUtil.CStr(i.FreshDate),                                   //賞味期限
                                      NextPrice = DataUtil.CStr(i.NextPrice),                                   //次回単価
                                      NextStopFlag = DataUtil.CStr(i.NextStopFlag),                             //次回中止
                                      SaleFlag = DataUtil.CStr(i.SaleFlag),                                     //売れるマーク
                                      ItemsName = t_item.ShortName,                                             //商品名
                                      ItemAddFlag = i.ItemAddFlag == null ? "0" : DataUtil.CStr(i.ItemAddFlag)  //商品追加
                                  };
                hoClientItems = clientItems.ToList();
            }

            return hoClientItems;
        }

        public DateTime GetNextHoDate(string shopCD, string clientCD, string Route)
        {

            return GetNextHoDate(shopCD, clientCD, Route, CommonUtils.GetDateTimeNow());
        }

        public DateTime GetNextHoDate(string shopCD,string clientCD, string Route , DateTime date)
        {

            T_HoClientAdapter logic = new T_HoClientAdapter(_enreq);

            List<string> routeList = logic.GetShopRouteList(shopCD, clientCD);
            string nextRoute = string.Empty;
            if (routeList.Count > 0)
            {
                routeList.Sort();

                nextRoute = routeList[0];

                foreach (string item in routeList)
                {
                    if (string.CompareOrdinal(item, Route) > 0)
                    {
                        nextRoute = item;
                        break;
                    }
                }
            }
            else
            {
                nextRoute = Route;
            }

            if (string.IsNullOrEmpty(nextRoute)) {
                return DateTime.Now;
            }



            return CommonLogic.GetNextRouteDate(date, nextRoute.Substring(0, 2));
        }

        public string GetRoteByDate(string shopCD, string dateStr)
        {

            DateTime hoDate = DateTime.ParseExact(dateStr, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            string rote = CommonLogic.GetRouteFromDate(hoDate);

            T_HoClientAdapter logic = new T_HoClientAdapter(_enreq);

            List<string> routeList = logic.GetShopRoutes(shopCD);
            string outRoute = string.Empty;

            if (routeList.Count > 0)
            {
                routeList.Reverse();

                outRoute = routeList[0];

                foreach (string item in routeList)
                {
                    if (string.CompareOrdinal(item.Substring(0, 2), rote) <= 0)
                    {
                        outRoute = item;
                        break;
                    }
                }

                if (outRoute.Substring(0, 2) != rote)
                {
                    outRoute = rote + "80";
                }
            }
            else
            {
                outRoute = rote + "80";
            }

            return outRoute;
        }

        public void SaveDownloadInfo(List<SaleDataViewModel> datas, string staffName, Dictionary<string, int> clientSlipNos, int shopLastSlipNo)
        {

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    string shopCD = datas[0].ShopCD;

                    List<DateTime> dateTimes = new List<DateTime>();

                    foreach (SaleDataViewModel m in datas)
                    {
                        dateTimes.Add(DateTime.ParseExact(m.HoDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture));
                    }

                    var query = from day in db.HoDays

                                where day.ShopCD == shopCD && dateTimes.Contains(day.HoDate)

                                select day;

                    var orderQuery = from order in db.HoOrderClients
                                     where order.ShopCD == shopCD && dateTimes.Contains(order.HoDate)
                                     select order;

                    DateTime now = DateTime.ParseExact(CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd HH:mm:ss"), "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    List<T_HoDay> days = query.ToList();
                    foreach (T_HoDay day in days)
                    {
                        day.DownloadStaffName = staffName;
                        day.DownloadDate = now;
                        day.UpdateUser = staffName;
                        day.UpdateTime = now;
                        db.HoDays.AddOrUpdate(day);
                    }

                    List<T_HoOrderClient> orders = orderQuery.ToList();

                    orders.ForEach(o =>
                    {
                        o.DoneFlag = "3";
                        if (clientSlipNos.ContainsKey(o.ClientCD))
                        {
                            o.SlipNO = clientSlipNos[o.ClientCD];
                        }
                    });

                    db.HoOrderClients.AddOrUpdate(orders.ToArray());

                    //slipno update
                    M_Shop shop = db.Shops.Find(shopCD);
                    shop.LastSlipNO = shopLastSlipNo;
                    shop.UpdateUser = staffName;
                    shop.UpdateTime = now;
                    db.Shops.AddOrUpdate(shop);

                    db.SaveChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scope.Dispose();
                }

            }
        }
    }
}
