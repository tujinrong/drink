//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  。
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

using SafeNeeds.DySmat;
using System.Data.SqlClient;
using DrinkService.Data.ViewModels;
using System.Transactions;
using DrinkService.Utils;
using DrinkService.Data;
using DrinkService.Data.Logics;
using SafeNeeds.DySmat.Util;

namespace DrinkService.Models
{
    /// <summary>
    /// 補充集金計画
    /// </summary>
    public class T_HoClientAdapter : HoEntityAdapterBase
    {
        public T_HoClientAdapter(EntityRequest request):base(request, typeof(T_HoClient).Name)
        {
        }


        public PageViewResult GetList(PageViewRequest req)
        {
            return base.GetList(req, Y_EntityViewData.補充集金リスト);
        }

        /// <summary>
        /// 補充集金指示書作成画面
        /// 
        /// 初期データ取得
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public Result GetData(T_HoClient.Key key, out T_HoClient data)
        {
            data = dbContext.HoClients.Find(key.ShopCD, key.ClientCD, key.Seq);

            if (data == null)
            {
                data = new T_HoClient();
                data.Detail = new List<T_HoClientItem>();

            }
            else
            {
                data.Detail = dbContext.HoClientItems.Where(e => e.ShopCD == key.ShopCD && e.ClientCD==key.ClientCD && e.Seq==key.Seq).ToList();
            }

            return new Result();

        }

        /// <summary>
        /// 補充集金指示書作成画面
        /// 
        /// 初期データ取得
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public Result GetHoClient(T_HoClient.Key key, out T_HoClient data)
        {
            data = dbContext.HoClients.Find(key.ShopCD, key.ClientCD, key.Seq);

            if (data == null)
            {
                data = new T_HoClient();

            }

            return new Result();

        }

        /// <summary>
        /// 署名取得
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public Result GetSign(T_HoClient.Key key, out T_Sign data)
        {
            data = dbContext.HoSign.Find(key.ShopCD + "," + key.ClientCD + "," + key.Seq);

            if (data == null)
            {
                data = new T_Sign();

            }

            return new Result();

        }
        /// <summary>
        ///  実績入力画面
        /// 
        /// 保存処理
        /// </summary>
        /// <param name="data"></param>
        public Result SaveData(T_HoClient data, string hoClientUpdateTime, List<T_HoClientItem> addList, string doneFlag, List<T_HoClientItem> delList, string signData)
        {
            Result result = new Result();
            string updateTime = "";

            using (TransactionScope scope = new TransactionScope())
            {
                //using (SqlConnection conn = new SqlConnection(Config.ConnectionString))
                //{
                    using (dbContext = new DrinkServiceContext())
                    {
                        T_HoOrderClient orderClient = dbContext.HoOrderClients.Find(data.ShopCD, data.HoDate, data.TantoCD, data.ClientCD, data.Route);
                        if (data.Seq == 0)
                        {
                            if (orderClient.Seq != 0)
                            {
                                data.Seq = orderClient.Seq;
                            }
                            else
                            {
                                M_Client client = dbContext.Clients.Find(data.ShopCD, data.ClientCD);

                                client.LastSeq = client.LastSeq + 1;
                                data.Seq = client.LastSeq;

                                client.AfterStopVisitDate = null;
                                client.LastAfterStopFlag = null;

                                client.UpdateUser = this._entityRequest.User;
                                client.UpdateTime = CommonUtils.GetDateTimeNow();
                                dbContext.Clients.AddOrUpdate(client);
                            }
                        }
                        orderClient.Seq = data.Seq;

                        //状態
                        if (data.Detail != null)
                        {
                            foreach (T_HoClientItem item in data.Detail)
                            {
                                item.Seq = data.Seq;
                            }
                        }


                        if (addList != null)
                        {
                            foreach (T_HoClientItem item in addList)
                            {
                                item.Seq = data.Seq;
                            }
                        }

                        if (delList != null)
                        {
                            foreach (T_HoClientItem item in delList)
                            {
                                T_HoClientItem t_item = dbContext.HoClientItems.Find(data.ShopCD, data.ClientCD, data.Seq, item.ItemCD);
                                if (t_item != null) dbContext.HoClientItems.Remove(t_item);
                            }
                        }

                        orderClient.DoneFlag = doneFlag;
                        T_HoClient t = dbContext.HoClients.Find(data.ShopCD, data.ClientCD, data.Seq);

                        if (t != null)
                        {
                            string updateTimeIndb = t.UpdateTime == null ? "" : t.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss fff");
                            if (updateTimeIndb != hoClientUpdateTime)
                            {
                                result.Message = "データは、他の端末（" + t.UpdateUser + "）より更新されました。　閉じるボタンを押して再度やり直して下さい。";
                                result.ReturnValue = EnumResult.Haita;
                                result.ErrorKey = "haita";
                                return result;
                            }
                        }

                        if (t != null) dbContext.HoClients.Remove(t);

                        data.UpdateUser = this._entityRequest.User;
                        data.UpdateTime = CommonUtils.GetDateTimeNow();
                        dbContext.HoClients.Add(data);

                        if (data.Detail != null)
                        {
                            dbContext.HoClientItems.AddOrUpdate(data.Detail.ToArray());
                        }

                        if (addList != null)
                        {
                            dbContext.HoClientItems.AddOrUpdate(addList.ToArray());
                        }

                        dbContext.HoOrderClients.AddOrUpdate(orderClient);

                        var hoSign = dbContext.HoSign.Find(data.ShopCD + "," + data.ClientCD + "," + data.Seq);
                        if (hoSign == null)
                        {
                            hoSign = new T_Sign();
                        }
                        hoSign.SignKey = data.ShopCD + "," + data.ClientCD + "," + data.Seq;
                        hoSign.SignData = signData;
                        dbContext.HoSign.AddOrUpdate(hoSign);
                        dbContext.SaveChanges();

                        string sql = string.Format("select * from T_hoClient  where ShopCD = '{0}' and ClientCD = '{1}' and Seq = {2}", data.ShopCD, data.ClientCD, data.Seq);
                        var dt = SQLHelper.GetDataTable(sql);
                        //SqlDataAdapter adapter = new SqlDataAdapter();
                        //DataTable dt = new DataTable();
                        //adapter.SelectCommand = new SqlCommand(sql);
                        //adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DateTime? time = DataUtil.CDate(dt.Rows[0]["UpdateTime"]);
                            updateTime = time.Value.ToString("yyyy/MM/dd HH:mm:ss fff");
                        }

                        result = new Result()
                        {
                            ReturnValue = EnumResult.OK,
                            Message = updateTime
                        };

                        if (doneFlag == "1")
                        {
                            //null check
                            string sql2 = @"
select 

T_HoClientItem.ShopCD
,T_HoClientItem.ClientCD
,T_HoClientItem.ItemCD
 from T_HoClientItem 


where (T_HoClientItem.AfterNum is null or (T_HoClientItem.ThisNum is null and T_HoClientItem.ItemAddFlag <> '1'))
and T_HoClientItem.ItemAddFlag <> '2' 
and T_HoClientItem.ShopCD = '{0}'
and T_HoClientItem.ClientCD = '{1}'
and T_HoClientItem.Seq = '{2}'
";

                            DataTable dt2 = SQLHelper.GetDataTable(string.Format(sql2, orderClient.ShopCD, orderClient.ClientCD, orderClient.Seq));
                            if (dt2.Rows.Count > 0)
                            {
                                orderClient.DoneFlag = "2";
                                dbContext.HoOrderClients.AddOrUpdate(orderClient);
                                dbContext.SaveChanges();
                            }

                            //sum money check
                            sql2 = @"
select T.* from 
(select 

T_HoClient.shopcd
,T_HoClient.ClientCD
,T_HoOrderClient.HoDate
,T_HoOrderClient.FirstFlag
,T_HoClient.UpdateUser
,T_HoClient.UpdateTime
,T_HoClient.SoldMoney
,TY.sumMoney

 from T_HoClient 

left join T_HoOrderClient 
on T_HoClient.ShopCD = T_HoOrderClient.ShopCD
and  T_HoClient.ClientCD = T_HoOrderClient.ClientCD
and  T_HoClient.HoDate = T_HoOrderClient.HoDate
and  T_HoClient.TantoCD = T_HoOrderClient.TantoCD

left join (
SELECT ShopCD
      ,ClientCD
      ,Seq
      ,sum(Money) as sumMoney
  FROM T_HoClientItem

  group by ShopCD
      ,ClientCD
      ,Seq
) TY on T_HoClient.ShopCD = TY.ShopCD
and  T_HoClient.ClientCD = TY.ClientCD
and  T_HoClient.Seq = TY.Seq

where T_HoOrderClient.DoneFlag ='1'  
and T_HoOrderClient.ShopCD = '{0}'
and T_HoOrderClient.ClientCD = '{1}'
and T_HoOrderClient.Seq = '{2}'

) T

where T.SoldMoney <> T.sumMoney
";

                            DataTable dt3 = SQLHelper.GetDataTable(string.Format(sql2, orderClient.ShopCD, orderClient.ClientCD, orderClient.Seq));
                            if (dt3.Rows.Count > 0)
                            {
                                orderClient.DoneFlag = "2";
                                dbContext.HoOrderClients.AddOrUpdate(orderClient);
                                dbContext.SaveChanges();
                            }
                        }

                        //SoldMoney,GetMoney,DiffMoney  check
                        String sqlSoldMoney1 = @"select sum(UsedNum*Price) SoldMoney from T_HoClientItem where ShopCD = '{0}' AND ClientCD = '{1}' And Seq={2}";
                        String sqlSoldMoney2 = @"select SoldMoney,GetMoney,DiffMoney from T_HoClient where ShopCD = '{0}' AND ClientCD = '{1}' And Seq={2}";
                        DataTable dtSoldMoney1 = SQLHelper.GetDataTable(string.Format(sqlSoldMoney1, data.ShopCD, data.ClientCD, data.Seq));
                        DataTable dtSoldMoney2 = SQLHelper.GetDataTable(string.Format(sqlSoldMoney2, data.ShopCD, data.ClientCD, data.Seq));
                        if (dtSoldMoney1.Rows.Count > 0 && dtSoldMoney2.Rows.Count > 0)
                        {
                            if (DataUtil.CInt(dtSoldMoney1.Rows[0]["SoldMoney"]) != DataUtil.CInt(dtSoldMoney2.Rows[0]["SoldMoney"]))
                            {
                                int SoldMoney = DataUtil.CInt(dtSoldMoney1.Rows[0]["SoldMoney"]);
                                int GetMoney = DataUtil.CInt(dtSoldMoney2.Rows[0]["GetMoney"]);
                                int DiffMoney = GetMoney - SoldMoney;
                                String sqlSoldMoney3 = @"update T_HoClient set SoldMoney = {3} ,DiffMoney = {4} where ShopCD = '{0}' AND ClientCD = '{1}' And Seq={2}";
                                SQLHelper.ExecuteNonQuery(string.Format(sqlSoldMoney3, data.ShopCD, data.ClientCD, data.Seq, SoldMoney, DiffMoney));
                            }
                        }
                    }
                    scope.Complete();
                    return result;
                //}
            }
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="id"></param>
        public Result Delete(T_HoClient hoClient, string hoClientUpdateTime)
        {
            Result result = new Result();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    T_HoOrderClient orderClient = dbContext.HoOrderClients.Find(hoClient.ShopCD, hoClient.HoDate, hoClient.TantoCD, hoClient.ClientCD, hoClient.Route);
                    orderClient.Seq = 0;
                    orderClient.DoneFlag = "0";

                    T_HoClient t = dbContext.HoClients.Find(hoClient.ShopCD, hoClient.ClientCD, hoClient.Seq);

                    if (t != null)
                    {
                        string updateTimeIndb = t.UpdateTime == null ? "" : t.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss fff");
                        if (updateTimeIndb != hoClientUpdateTime)
                        {
                            result.Message = "データは、他の端末（" + t.UpdateUser + "）より更新されました。　閉じるボタンを押して再度やり直して下さい。";
                            result.ReturnValue = EnumResult.Haita;
                            result.ErrorKey = "haita";
                            return result;
                        }
                    }

                    dbContext.HoOrderClients.AddOrUpdate(orderClient);

                    M_Client client = dbContext.Clients.Find(hoClient.ShopCD, hoClient.ClientCD);

                    if (client.LastSeq == hoClient.Seq)
                    {
                        client.LastSeq = client.LastSeq - 1;
                        client.UpdateUser = this._entityRequest.User;
                        client.UpdateTime = CommonUtils.GetDateTimeNow();

                        ////分组排序，取时间最大的一条记录
                        var HoBeforeDayClientsQuary = from c in dbContext.HoOrderClients
                                                      where c.ShopCD == orderClient.ShopCD && c.ClientCD == orderClient.ClientCD && c.HoDate < orderClient.HoDate
                                                      group c by c.ClientCD into cg
                                                      let maxId = cg.Max(a => a.HoDate)
                                                      from row in cg
                                                      where row.HoDate == maxId
                                                      select row;

                        List<T_HoOrderClient> HoBeforeDayClients = HoBeforeDayClientsQuary.ToList();

                        if (HoBeforeDayClients.Count > 0)
                        {
                            T_HoOrderClient preOrderClient = HoBeforeDayClients[0];

                            if (preOrderClient.AfterStopFlag == "1" || preOrderClient.AfterStopFlag == "2") {
                                client.LastAfterStopFlag = preOrderClient.AfterStopFlag;
                                client.AfterStopVisitDate = preOrderClient.HoDate;
                            }
                        }


                        dbContext.Clients.AddOrUpdate(client);
                    }

                    string sql;

                    sql = string.Format("DELETE FROM " + typeof(T_HoClient).Name + " WHERE ShopCD='{0}' AND ClientCD='{1}' and Seq={2}",
                         hoClient.ShopCD, hoClient.ClientCD, hoClient.Seq);

                    dbContext.Database.ExecuteSqlCommand(sql);

                    sql = string.Format("DELETE FROM " + typeof(T_HoClientItem).Name + " WHERE ShopCD='{0}' AND ClientCD='{1}' and Seq={2}",
                         hoClient.ShopCD, hoClient.ClientCD, hoClient.Seq);

                    dbContext.Database.ExecuteSqlCommand(sql);

                    sql = string.Format("DELETE FROM " + typeof(T_Sign).Name + " WHERE SignKey='{0}'",
                         hoClient.ShopCD + "," + hoClient.ClientCD + "," + hoClient.Seq);

                    dbContext.Database.ExecuteSqlCommand(sql);

                    dbContext.SaveChanges();
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
            return new Result();
        }

        public List<object> GetShopRoute(string shopCD, string date, bool isOnlyDate)
        {
            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable dt = new DataTable();
            string sql = string.Empty;
            if (shopCD == null)
            {
                sql = "SELECT DISTINCT ROUTE FROM M_CLIENTROUTE";
            }
            else
            {
                sql = string.Format("SELECT DISTINCT ROUTE FROM M_CLIENTROUTE WHERE SHOPCD = '{0}'", shopCD);
            }
            
            adapter.SelectCommand = new SqlCommand(sql, connection);
            adapter.Fill(dt);
            List<object> routeList = new List<object>();

            string roteToday = "";
            bool hasToday = false;
            string lastRote = "0000";

            if (string.IsNullOrEmpty(date) == false)
            {
                string tempDate = date.Replace("/", "");
                date = tempDate.Substring(0, 4) + "/" + tempDate.Substring(4, 2) + "/" + tempDate.Substring(6, 2);
                DateTime today = DateTime.ParseExact(date, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);

                roteToday = CommonLogic.GetRouteFromDate(today);
            }
            else {
                hasToday = true;
                isOnlyDate = false;
            }
            
            

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                if (DataUtil.CStr(dr[0]).Trim().Length != 4) {
                    continue;
                }

                if (DataUtil.CInt(DataUtil.CStr(dr[0]).Substring(0,2)) == DataUtil.CInt(roteToday))
                {
                    hasToday = true;
                }

                if (DataUtil.CInt(DataUtil.CStr(dr[0]).Substring(0,2)) > DataUtil.CInt(roteToday) && hasToday == false)
                {
                    routeList.Add(new { RouteCD = roteToday+"80" });
                    hasToday = true;
                }

                if (isOnlyDate)
                {
                    if (DataUtil.CInt(DataUtil.CStr(dr[0]).Substring(0, 2)) == DataUtil.CInt(roteToday))
                    {
                        routeList.Add(new { RouteCD = dr[0] });
                    }
                }
                else {
                    routeList.Add(new { RouteCD = dr[0] });
                }
                

                lastRote = DataUtil.CStr(dr[0]);
            }

            if (DataUtil.CInt(lastRote.Substring(0, 2)) < DataUtil.CInt(roteToday) && hasToday == false)
            {
                routeList.Add(new { RouteCD = roteToday + "80" });
            }

            routeList.Insert(0, new { RouteCD = "" });
            connection.Close();
            return routeList;
        }

        public List<string> GetShopRouteList(string shopCD,string clientCD)
        {
            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable dt = new DataTable();
            string sql = string.Format("SELECT ROUTE FROM M_CLIENTROUTE WHERE SHOPCD = '{0}' AND CLIENTCD ='{1}' ORDER BY ROUTE", shopCD, clientCD);
            adapter.SelectCommand = new SqlCommand(sql, connection);
            adapter.Fill(dt);
            List<string> routeList = new List<string>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                routeList.Add(DataUtil.CStr(dr[0]));
            }

            connection.Close();
            return routeList;
        }

        public List<string> GetShopRoutes(string shopCD)
        {
            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable dt = new DataTable();
            string sql = "";
            if (string.IsNullOrEmpty(shopCD))
            {
                sql = string.Format("SELECT DISTINCT ROUTE FROM M_CLIENTROUTE  ORDER BY ROUTE");
            }
            else 
            {
                sql = string.Format("SELECT DISTINCT ROUTE FROM M_CLIENTROUTE WHERE SHOPCD = '{0}'  ORDER BY ROUTE", shopCD);
            }
            adapter.SelectCommand = new SqlCommand(sql, connection);
            adapter.Fill(dt);
            List<string> routeList = new List<string>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                routeList.Add(DataUtil.CStr(dr[0]));
            }
            connection.Close();
            return routeList;
        }

        public Dictionary<string, string> GetRouteClientList(string ShopCD, string Route)
        {
            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = string.Format("SELECT DISTINCT CLIENTCD FROM M_CLIENTROUTE WHERE SHOPCD = '{0}' AND ROUTE = '{1}'", ShopCD, Route);
            adapter.SelectCommand = new SqlCommand(sql, connection);
            adapter.Fill(dt);
            Dictionary<string, string> clientCDList = new Dictionary<string, string>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                clientCDList.Add(DataUtil.CStr(dr[0]),"1");
            }
            connection.Close();
            return clientCDList;
        }


        public List<ItemInListViewModel> GetItemInList(string shopCD)
        {

            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable dt = new DataTable();
//            string sql = @"
//                            select T_HoClientItem.ItemCD,M_Item.ShortName as ItemName 
//
//                            ,sum(isnull(T_HoClientItem.AfterNum,0)) as ThisNum
//            
//                            from T_HoClientItem 
//            
//                            left join M_Shop 
//                            on T_HoClientItem.ShopCD = M_Shop.ShopCD
//            
//                            left join M_Item
//                            on T_HoClientItem.ItemCD = M_Item.ItemCD
//            
//                            left join ( 
//                                select
//                                    ShopCD
//                                    , ClientCD
//                                    , max(Seq) MaxSeq 
//                                from
//                                    T_HoOrderClient 
//                                where
//                                    DoneFlag = 1 
//                                    or DoneFlag = 3 
//                                group by
//                                    ShopCD
//                                    , ClientCD
//                            ) T4 
//                            on T_HoClientItem.ShopCD = T4.ShopCD 
//                            and T_HoClientItem.ClientCD = T4.ClientCD
//            
//                            where 1=1 {0} and T_HoClientItem.Seq = T4.MaxSeq and isnull(T_HoClientItem.AfterNum,0) > 0
//            
//                            group by T_HoClientItem.ItemCD,M_Item.ShortName
//
//                            order by T_HoClientItem.ItemCD
//            ";

            string sql = @"
                            select T_HoClientItem.ItemCD,M_Item.ShortName as ItemName 

                            ,sum(isnull(T_HoClientItem.AfterNum,0)) as ThisNum
            
                            from T_HoClientItem 
            
                            left join M_Shop 
                            on T_HoClientItem.ShopCD = M_Shop.ShopCD
            
                            left join M_Item
                            on T_HoClientItem.ItemCD = M_Item.ItemCD
            
                            left join M_Client T4
                            on T_HoClientItem.ShopCD = T4.ShopCD 
                            and T_HoClientItem.ClientCD = T4.ClientCD
            
                            where 1=1 {0} and T_HoClientItem.Seq = T4.LastSeq and isnull(T_HoClientItem.AfterNum,0) > 0
            
                            group by T_HoClientItem.ItemCD,M_Item.ShortName

                            order by T_HoClientItem.ItemCD
            ";

            string shopSql = "";

            if (shopCD != null && shopCD.Length > 0)
            {
                shopSql = string.Format("and M_Shop.ShopCD = '{0}'", shopCD);
            }

            adapter.SelectCommand = new SqlCommand(string.Format(sql, shopSql), connection);
            adapter.Fill(dt);
            List<ItemInListViewModel> lst = new List<ItemInListViewModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                lst.Add(new ItemInListViewModel { ItemCD = DataUtil.CStr(dr["ItemCD"]), ItemName = DataUtil.CStr(dr["ItemName"]), InNum = DataUtil.CDec(dr["ThisNum"]) });
            }
            connection.Close();

            return lst;
        }

        public List<object> GetShopPlanedRoute(string shopCD, string DayStr,ref string outRoute)
        {
            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable dt = new DataTable();
            string sql = string.Empty;
            if (shopCD == null)
            {
                sql = "SELECT DISTINCT ROUTE FROM M_CLIENTROUTE";
            }
            else
            {
                sql = string.Format("SELECT DISTINCT ROUTE FROM T_HoOrderClient WHERE SHOPCD = '{0}' AND HoDate = '{1}'", shopCD, DayStr);
            }

            adapter.SelectCommand = new SqlCommand(sql, connection);
            adapter.Fill(dt);
            List<object> routeList = new List<object>();

            List<string> routeListTemp = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                routeList.Add(new { RouteCD = dr[0] });
                routeListTemp.Add(DataUtil.CStr(dr[0]));
            }


            DateTime hoDate = DateTime.ParseExact(DayStr, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            outRoute = string.Empty;
            string rote = CommonLogic.GetRouteFromDate(hoDate);
            if (routeListTemp.Count == 1)
            {
                routeListTemp.Reverse();

                outRoute = routeListTemp[0];

                foreach (string item in routeListTemp)
                {
                    if (string.CompareOrdinal(item.Substring(0, 2), rote) <= 0)
                    {
                        outRoute = item;
                        break;
                    }
                }
            }

            routeList.Insert(0, new { RouteCD = "" });
            connection.Close();
            return routeList;
        }

        public List<object> GetShopUndoRoute(string shopCD)
        {
            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable dt = new DataTable();
            string sql = string.Empty;
            if (shopCD == null)
            {
                return null;
            }
            else
            {
                sql = string.Format("SELECT DISTINCT ROUTE FROM T_HoOrderClient WHERE SHOPCD = '{0}' AND DoneFlag in (0,2) ORDER BY ROUTE", shopCD);
            }

            adapter.SelectCommand = new SqlCommand(sql, connection);
            adapter.Fill(dt);
            List<object> routeList = new List<object>();

            List<string> routeListTemp = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                routeList.Add(new { RouteCD = dr[0] });
            }
            routeList.Insert(0, new { RouteCD = "" });
            connection.Close();
            return routeList;
        }

        internal PageViewResult GetCollectionList(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(T_HoOrderClient).Name, Y_EntityViewData.集金リスト);

            return view.GetPageView(req);
        }

        public Result SaveAfterStop(string ShopCD, string ClientCD, DateTime? HoDate, string Route, string StaffCD, string AfterStopFlag, DateTime? AfterDate, int Seq)
        {
            Result result = new Result();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    T_HoOrderClient orderClient = dbContext.HoOrderClients.Find(ShopCD, HoDate, StaffCD, ClientCD, Route);

                    orderClient.Seq = 0;



                    if (string.IsNullOrEmpty(AfterStopFlag) || AfterStopFlag == "0")
                    {
                        orderClient.DoneFlag = "0";
                    }
                    else 
                    {
                        orderClient.DoneFlag = "1";
                    }

                    orderClient.AfterStopFlag = AfterStopFlag;
                    orderClient.AfterDate = AfterDate;

                    M_Client client = dbContext.Clients.Find(ShopCD, ClientCD);

                    //delete T_HoClient
                    T_HoClient t = dbContext.HoClients.Find(ShopCD, ClientCD, Seq);

                    if (t != null) {

                        if (client.LastSeq == t.Seq)
                        {
                            client.LastSeq = client.LastSeq - 1;
                        }

                        string sql;

                        sql = string.Format("DELETE FROM " + typeof(T_HoClient).Name + " WHERE ShopCD='{0}' AND ClientCD='{1}' and Seq={2}",
                             t.ShopCD, t.ClientCD, t.Seq);

                        dbContext.Database.ExecuteSqlCommand(sql);

                        sql = string.Format("DELETE FROM " + typeof(T_HoClientItem).Name + " WHERE ShopCD='{0}' AND ClientCD='{1}' and Seq={2}",
                             t.ShopCD, t.ClientCD, t.Seq);

                        dbContext.Database.ExecuteSqlCommand(sql);

                        sql = string.Format("DELETE FROM " + typeof(T_Sign).Name + " WHERE SignKey='{0}'",
                             t.ShopCD + "," + t.ClientCD + "," + t.Seq);

                        dbContext.Database.ExecuteSqlCommand(sql);

                    };

                    client.LastAfterStopFlag = AfterStopFlag;

                    if (client.LastAfterStopFlag == "0") {
                        client.LastAfterStopFlag = null;
                    }

                    client.AfterStopVisitDate = HoDate;
                    client.AfterDate = AfterDate;
                    client.UpdateUser = this._entityRequest.User;
                    client.UpdateTime = CommonUtils.GetDateTimeNow();
                    dbContext.Clients.AddOrUpdate(client);

                    dbContext.HoOrderClients.AddOrUpdate(orderClient);

                    dbContext.SaveChanges();

                    result = new Result()
                    {
                        ReturnValue = EnumResult.OK
                    };

                    scope.Complete();
                    return result;
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


        public PageViewResult GetClientItemLimitList(string shopCD, string dayLimit, bool getCountOnly, bool getDataOnly, int page)
        {
            PageViewResult result = new PageViewResult();

            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string sql = @"
               SELECT T_HoClientItem.ShopCD AS ShopCD
  , M_Shop.ShopName AS ShopName
  , T_HoClientItem.ClientCD AS ClientCD
  , M_Client.ClientName AS ClientName
  , M_Staff.StaffName AS StaffName 
  , T_HoClientItem.ItemCD AS ItemCD
  , M_Item.ItemName AS ItemName
  , T_HoClientItem.FreshDate AS FreshDate
  , M_Item.SaleEndDay AS SaleEndDay
  , T_HoClient.TantoCD AS TantoCD
  , T_HoClientItem.AfterNum AS AfterNum
FROM
  T_HoClientItem WITH(NOLOCK)
  INNER JOIN M_Item WITH(NOLOCK)
    ON T_HoClientItem.ItemCD = M_Item.ItemCD 
    AND T_HoClientItem.AfterNum > 0 
  INNER JOIN T_HoClient WITH(NOLOCK)
    ON T_HoClientItem.ShopCD = T_HoClient.ShopCD 
    AND T_HoClientItem.ClientCD = T_HoClient.ClientCD 
    AND T_HoClientItem.Seq = T_HoClient.Seq 
  INNER JOIN M_Client WITH(NOLOCK)
    ON T_HoClient.ShopCD = M_Client.ShopCD 
    AND T_HoClient.ClientCD = M_Client.ClientCD 
    AND T_HoClient.Seq = M_Client.LastSeq 
    AND M_Client.LastSeq > 0 
  INNER JOIN M_Shop WITH(NOLOCK)
    ON T_HoClient.ShopCD = M_Shop.ShopCD 
  INNER JOIN M_Staff WITH(NOLOCK)
    ON T_HoClient.ShopCD = M_Staff.ShopCD 
    AND T_HoClient.TantoCD = M_Staff.StaffCD 

 where T_HoClientItem.FreshDate <= '{0}'
　and ((M_Client.LastAfterStopFlag is null)OR(M_Client.LastAfterStopFlag is not null AND T_HoClient.Hodate=M_Client.AfterStopVisitDate))

        ";
           
            //sql += " order by  M_Maker.MakerNameKana ,M_Maker.MakerName ,M_Item.SaleStartDay DESC";


            if (!DataUtil.IsNullOrEmpty(shopCD))
            {
                sql += " and T_HoClientItem.ShopCD = '" + shopCD + "'";
            }

            sql = String.Format(sql, dayLimit);


            if (getDataOnly)
            {
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(sql, connection);
                adapter.SelectCommand.CommandTimeout = 300;
                dt = new DataTable();
                adapter.Fill(dt);

                result.DataTable = dt;
                //adapter.SelectCommand.Dispose();
                connection.Close();
                return result;
            }


            if (getCountOnly)
            {
                //sql = "SELECT * FROM (" + sql.Replace("SELECT T_HoClientItem.ShopCD AS ShopCD", "select ROW_NUMBER() OVER (ORDER BY T_HoClientItem.ShopCD, T_HoClientItem.ClientCD, T_HoClientItem.ItemCD ) as _rowNo,T_HoClientItem.ShopCD AS ShopCD ");
                //sql += " ) _t WHERE _t._rowNo> 0 and _t._rowNo<=1";

                sql = "SELECT COUNT(*) from (" + sql + ") as _counttable";
                

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(sql, connection);
                adapter.SelectCommand.CommandTimeout = 300;
                dt = new DataTable();
                adapter.Fill(dt);

                result.DataTable = dt;
                //adapter.SelectCommand.Dispose();
                connection.Close();
                return result;
            }
            //count 
            string sqlCount = "SELECT COUNT(*) from (" + sql + ") as _counttable";
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sqlCount, connection);
            adapter.SelectCommand.CommandTimeout = 300;
            dt = new DataTable();
            adapter.Fill(dt);
            int count = (int)dt.Rows[0][0];

            if (count > 0)
            {
                int pageRows = _Proj.PageRows;
                int startRow = (page - 1) * pageRows;
                sql = "SELECT * FROM (" + sql.Replace("SELECT T_HoClientItem.ShopCD AS ShopCD", "select ROW_NUMBER() OVER (ORDER BY T_HoClientItem.ShopCD, T_HoClientItem.ClientCD, T_HoClientItem.ItemCD ) as _rowNo,T_HoClientItem.ShopCD AS ShopCD ");
                sql += " ) _t WHERE _t._rowNo>" + startRow + " and _t._rowNo<=" + (startRow + pageRows).ToString();

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(sql, connection);
                adapter.SelectCommand.CommandTimeout = 300;
                dt = new DataTable();
                adapter.Fill(dt);

                result.DataTable = dt;
            }
            else
            {
                result.DataTable = new DataTable();
            }
            result.PageCount = count;

            //adapter.SelectCommand.Dispose();
            connection.Close();
            return result;
        }
    }
}