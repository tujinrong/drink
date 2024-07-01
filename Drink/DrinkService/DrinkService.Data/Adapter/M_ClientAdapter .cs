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
using System.Linq;
using System.Data.Entity.Migrations;
using SafeNeeds.DySmat;
using DrinkService.Utils;
using System.Transactions;
using System.Threading;
using System.Data.SqlClient;
using SafeNeeds.DySmat.Util;
using DrinkService.Data.ViewModels;

namespace DrinkService.Models
{
    /// <summary>
    /// 顧客管理
    /// </summary>
    public class M_ClientAdapter : HoEntityAdapterBase
    {
        
        public M_ClientAdapter(EntityRequest request): base(request, typeof(M_Client).Name)
        {

        }

        /// <summary>
        /// 検索一覧画面
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PageViewResult GetList(PageViewRequest req)
        {
            return base.GetList(req, Y_EntityViewData.顧客一覧);

            //DynamicView view = new DynamicView(0, typeof(M_Client).Name, Y_EntityViewData.顧客一覧);
            //return view.GetPageView(req);
        }

        /// <summary>
        /// 初期データ取得
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public Result GetData(M_Client.Key key, out M_Client data)
        {
            data = dbContext.Clients.Find(key.ShopCD, key.ClientCD);
            

            if (data == null)
            {
                data = new M_Client();
                data.InitItemDetail = new  List<M_ClientInitItems>();
                data.clientInitItemDetail = new List<M_ClientInitItems>();
                data.RouteDetail = new List<M_ClientRoute>();
                //data.HoClient = new T_HoClient();
            }
            else
            {
                int seq = data.LastSeq;

                List<M_ClientInitItems> inits = dbContext.ClientItems.Where(e => e.ShopCD == key.ShopCD && e.ClientCD==key.ClientCD).ToList();

                if(inits == null){
                   data.InitItemDetail = new  List<M_ClientInitItems>();
                   data.clientInitItemDetail = new List<M_ClientInitItems>();
                }else{
                    var initItemQuary = from c in inits

                                        group c by c.ItemCD into cg
                                        select new M_ClientInitItems {
                                            ShopCD = key.ShopCD,
                                            ClientCD = key.ClientCD,
                                            ItemCD = cg.Key,
                                            ShelfCD = cg.Min(a => a.ShelfCD),
                                            Num = cg.Sum(a => a.Num),
                                            Price = cg.Max(a => a.Price)
                                        };

                    data.InitItemDetail = initItemQuary.ToList();
                    data.clientInitItemDetail = inits;
                }

                data.RouteDetail = dbContext.ClientRoutes.Where(e => e.ShopCD == key.ShopCD && e.ClientCD == key.ClientCD).ToList();
                data.HoDetail = dbContext.HoClientItems.Where(e => e.ShopCD == key.ShopCD && e.ClientCD == key.ClientCD && e.Seq==seq).ToList();
                data.HoClient = dbContext.HoClients.Find(key.ShopCD,key.ClientCD,seq);
            }

            return new Result();
        }


        ///// <summary>
        /////  顧客管理画面
        ///// 
        ///// 保存処理
        ///// </summary>
        ///// <param name="data"></param>
        //public Result TantoBatchSet(HoRequest req, List<M_Client> clients, string shopCD, string routeFrom, string routeTo, string tantoFrom, string tantoTo)
        //{
        //    Result result = new Result();

        //    string msg = "";

        //    int okCount = 0;

        //    SqlConnection connection = new SqlConnection(Config.ConnectionString);
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    DataTable dt = new DataTable();
        //    string sql = "";

        //    foreach (M_Client client in clients) { 
        //        //route change
        //        if (routeFrom != routeTo) {
        //            //変更元使用check
        //            sql = string.Format("select count(1) AS routeNum from T_HoOrderClient where ShopCD = '{0}' and ClientCD = '{1}' and (Seq = 0 or Seq = {2}) and Route = '{3}'", shopCD, client.ClientCD, client.LastSeq, routeFrom);

        //            adapter.SelectCommand = new SqlCommand(sql, connection);
        //            dt = new DataTable();
        //            adapter.Fill(dt);

        //            if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) > 0)
        //            {
        //                msg += "顧客【" + client.ClientCD + "】ルート【" + routeFrom + "】現在使用していますので、変更できません。<br />";
        //                continue;
        //            }

        //            //変更先存在check
        //            sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}'　and Route = '{2}'", shopCD, client.ClientCD, routeTo);

        //            adapter.SelectCommand = new SqlCommand(sql, connection);
        //            dt = new DataTable();
        //            adapter.Fill(dt);

        //            if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) > 0)
        //            {
        //                msg += "顧客【" + client.ClientCD + "】ルート【" + routeTo + "】現在存在していますので、変更できません。<br />";
        //                continue;
        //            }

        //            db.ClientRoutes.RemoveRange(db.ClientRoutes.Where(e => e.ShopCD == shopCD && e.ClientCD == client.ClientCD && e.Route == routeFrom));
        //            M_ClientRoute newRoute = new M_ClientRoute();
        //            newRoute.ShopCD = shopCD;
        //            newRoute.ClientCD =client.ClientCD;
        //            newRoute.Route = routeTo;
        //            newRoute.WeekNo = short.Parse(routeTo.Substring(0, 1));

        //            //RouteNo
        //            sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}' and RouteNo = '{2}' and WeekNo = '{3}' and Route <> '{4}'", shopCD, client.ClientCD, 1, newRoute.WeekNo, routeFrom);

        //            adapter.SelectCommand = new SqlCommand(sql, connection); 
        //            dt = new DataTable();
        //            adapter.Fill(dt);

        //            if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) == 0)
        //            {
        //                newRoute.RouteNo = 1;
        //            }
        //            else 
        //            {
        //                sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}' and RouteNo = '{2}' and WeekNo = '{3}' and Route <> '{4}'", shopCD, client.ClientCD, 2, newRoute.WeekNo, routeFrom);

        //                adapter.SelectCommand = new SqlCommand(sql, connection);
        //                dt = new DataTable();
        //                adapter.Fill(dt);

        //                if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) == 0)
        //                {
        //                    newRoute.RouteNo = 2;
        //                }
        //                else
        //                {
        //                    sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}' and RouteNo = '{2}' and WeekNo = '{3}' and Route <> '{4}'", shopCD, client.ClientCD, 3, newRoute.WeekNo, routeFrom);

        //                    adapter.SelectCommand = new SqlCommand(sql, connection);
        //                    dt = new DataTable();
        //                    adapter.Fill(dt);

        //                    if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) == 0)
        //                    {
        //                        newRoute.RouteNo = 3;
        //                    }
        //                    else 
        //                    {
        //                        string weekStr = "";
        //                        if (newRoute.WeekNo == 1) {
        //                            weekStr = "A週";
        //                        }
        //                        else if (newRoute.WeekNo == 2)
        //                        {
        //                            weekStr = "B週";
        //                        }
        //                        else if (newRoute.WeekNo == 3)
        //                        {
        //                            weekStr = "C週";
        //                        }
        //                        else if (newRoute.WeekNo == 4)
        //                        {
        //                            weekStr = "D週";
        //                        }
        //                        msg += "顧客【" + client.ClientCD + "】【" + weekStr + "】三つ存在していますので、変更できません。<br />";
        //                        continue;
        //                    }
        //                }
        //            }

        //            db.ClientRoutes.Add(newRoute);

        //        }

        //        //tanto change
        //        if (tantoFrom != tantoTo)
        //        {
        //            M_Client _client = db.Clients.Find(shopCD, client.ClientCD);
        //            _client.TantoCD = tantoTo;
        //            _client.UpdateUser = this._entityRequest.User;
        //            _client.UpdateTime = CommonUtils.GetDateTimeNow();
        //            db.Clients.AddOrUpdate(_client);
        //        }

        //        okCount++;
        //    }
        //    connection.Close();
        //    msg = okCount + "件変更完了しました<br />" + msg;
        //    result.Message = msg;

        //    db.SaveChanges();
        //    return result;
        //}

        /// <summary>
        ///  顧客管理画面
        /// 
        /// 保存処理
        /// </summary>
        /// <param name="data"></param>
        public List<ClientRouteTantoViewModel> RouteBatchSetCheck(HoRequest req, List<ClientRouteTantoViewModel> datas, string shopCD)
        {
            Result result = new Result();

            int okCount = 0;

            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string sql = "";

            Dictionary<string, Int32> routeDic = new Dictionary<string, Int32>();

             foreach (ClientRouteTantoViewModel item in datas)
             {
                 if (routeDic.ContainsKey(shopCD + "_" + item.ClientCD + "_" + item.RouteTo))
                 {
                     routeDic[shopCD + "_" + item.ClientCD + "_" + item.RouteTo] = routeDic[shopCD + "_" + item.ClientCD + "_" + item.RouteTo] + 1;
                 }
                 else
                 {
                     routeDic.Add(shopCD + "_" + item.ClientCD + "_" + item.RouteTo, 1);
                 }
             }

            List<ClientRouteTantoViewModel> errorList = new List<ClientRouteTantoViewModel>();

            foreach (ClientRouteTantoViewModel item in datas)
            {

                if (routeDic.ContainsKey(shopCD + "_" + item.ClientCD + "_" + item.RouteTo) && routeDic[shopCD + "_" + item.ClientCD + "_" + item.RouteTo] > 1)
                {
                    item.Msg = "ルート変更時に、顧客に複数ルートの選択は不可。";
                    item.No = errorList.Count + 1;
                    errorList.Add(item);
                    continue;
                }

                //if (routeDic.ContainsKey(shopCD + "_" + item.ClientCD + "_" + item.RouteTo) && routeDic[shopCD + "_" + item.ClientCD + "_" + item.RouteTo] > 1)
                //{
                //    item.Msg = "【ルートコード】同じ日のルートが同時存在は不可。";
                //    item.No = errorList.Count + 1;
                //    errorList.Add(item);
                //    continue;
                //}

                //route change

                M_Client _client = dbContext.Clients.Find(shopCD, item.ClientCD);
                
                //変更元使用check
                sql = string.Format("select count(1) AS routeNum from T_HoOrderClient where ShopCD = '{0}' and ClientCD = '{1}' and (Seq = 0 or Seq = {2}) and Route = '{3}' and DoneFlag != '3'", shopCD, _client.ClientCD, _client.LastSeq, item.RouteFrom);

                adapter.SelectCommand = new SqlCommand(sql, connection);
                dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) > 0)
                {
                    item.Msg = "【" + item.RouteFrom + "】は現在未完了のデータがある。";
                    item.No = errorList.Count + 1;
                    errorList.Add(item);
                    continue;
                }


                //変更先存在check
                sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}'　and Route = '{2}'", shopCD, _client.ClientCD, item.RouteTo);

                adapter.SelectCommand = new SqlCommand(sql, connection);
                dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) > 0)
                {
                    item.Msg = "【" + item.RouteTo + "】はすでに存在する。";
                    item.No = errorList.Count + 1;
                    errorList.Add(item);
                    continue;
                }


                //変更先同じ日のルートが同時存在check
                sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}' and substring(Route,1,2) = '{2}' and Route <> '{3}'", shopCD, _client.ClientCD, item.RouteTo.Substring(0,2), item.RouteFrom);

                adapter.SelectCommand = new SqlCommand(sql, connection);
                dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) > 0)
                {
                    item.Msg = "【" + item.RouteTo.Substring(0, 2) + "xx】同じ日のルートが同時存在は不可。";
                    item.No = errorList.Count + 1;
                    errorList.Add(item);
                    continue;
                }

                
                //db.ClientRoutes.RemoveRange(db.ClientRoutes.Where(e => e.ShopCD == shopCD && e.ClientCD == _client.ClientCD && e.Route == item.RouteFrom));
                
                M_ClientRoute newRoute = new M_ClientRoute();
                newRoute.ShopCD = shopCD;
                newRoute.ClientCD = _client.ClientCD;
                newRoute.Route = item.RouteTo;
                newRoute.WeekNo = short.Parse(item.RouteTo.Substring(0, 1));
                

                //RouteNo
                sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}' and RouteNo = '{2}' and WeekNo = '{3}' and Route <> '{4}'", shopCD, _client.ClientCD, 1, newRoute.WeekNo, item.RouteFrom);

                adapter.SelectCommand = new SqlCommand(sql, connection);
                dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) == 0)
                {
                    newRoute.RouteNo = 1;
                }
                else
                {
                    sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}' and RouteNo = '{2}' and WeekNo = '{3}' and Route <> '{4}'", shopCD, _client.ClientCD, 2, newRoute.WeekNo, item.RouteFrom);

                    adapter.SelectCommand = new SqlCommand(sql, connection);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) == 0)
                    {
                        newRoute.RouteNo = 2;
                    }
                    else
                    {
                        sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}' and RouteNo = '{2}' and WeekNo = '{3}' and Route <> '{4}'", shopCD, _client.ClientCD, 3, newRoute.WeekNo, item.RouteFrom);

                        adapter.SelectCommand = new SqlCommand(sql, connection);
                        dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) == 0)
                        {
                            newRoute.RouteNo = 3;
                        }
                        else
                        {
                            string weekStr = "";
                            if (newRoute.WeekNo == 1)
                            {
                                weekStr = "A週";
                            }
                            else if (newRoute.WeekNo == 2)
                            {
                                weekStr = "B週";
                            }
                            else if (newRoute.WeekNo == 3)
                            {
                                weekStr = "C週";
                            }
                            else if (newRoute.WeekNo == 4)
                            {
                                weekStr = "D週";
                            }
                            item.Msg = "【" + weekStr + "】三つ存在していますので、変更できません。";
                            item.No = errorList.Count + 1;
                            errorList.Add(item);
                            continue;
                        }
                    }
                }

                //db.ClientRoutes.Add(newRoute);

              
                okCount++;
            }
            connection.Close();
            //result.Message = okCount + "件変更完了しました<br />";

            //db.SaveChanges();
            return errorList;
        }

        /// <summary>
        ///  顧客管理画面
        /// 
        /// 保存処理
        /// </summary>
        /// <param name="data"></param>
        public Result RouteBatchSet(HoRequest req, List<ClientRouteTantoViewModel> datas, string shopCD)
        {
            Result result = new Result();

            int okCount = 0;


            List<ClientRouteTantoViewModel> errorList = RouteBatchSetCheck(req, datas, shopCD);

            if (errorList.Count() > 0) {
                result.data = errorList;
                result.ErrorKey = "Error";
                return result;
            }


            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string sql = "";

            Dictionary<string, string> routeDic = new Dictionary<string, string>();

            foreach (ClientRouteTantoViewModel item in datas)
            {
                //route change

                M_Client _client = dbContext.Clients.Find(shopCD, item.ClientCD);

              
                dbContext.ClientRoutes.RemoveRange(dbContext.ClientRoutes.Where(e => e.ShopCD == shopCD && e.ClientCD == _client.ClientCD && e.Route == item.RouteFrom));


                if (routeDic.ContainsKey(shopCD + "_" + _client.ClientCD + "_" + item.RouteTo))
                {
                    continue;
                }
                else { 
                routeDic.Add(shopCD + "_" + _client.ClientCD + "_" + item.RouteTo,"1");
                }

                M_ClientRoute newRoute = new M_ClientRoute();
                newRoute.ShopCD = shopCD;
                newRoute.ClientCD = _client.ClientCD;
                newRoute.Route = item.RouteTo;
                newRoute.WeekNo = short.Parse(item.RouteTo.Substring(0, 1));

                //RouteNo
                sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}' and RouteNo = '{2}' and WeekNo = '{3}' and Route <> '{4}'", shopCD, _client.ClientCD, 1, newRoute.WeekNo, item.RouteFrom);

                adapter.SelectCommand = new SqlCommand(sql, connection);
                dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) == 0)
                {
                    newRoute.RouteNo = 1;
                }
                else
                {
                    sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}' and RouteNo = '{2}' and WeekNo = '{3}' and Route <> '{4}'", shopCD, _client.ClientCD, 2, newRoute.WeekNo, item.RouteFrom);

                    adapter.SelectCommand = new SqlCommand(sql, connection);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) == 0)
                    {
                        newRoute.RouteNo = 2;
                    }
                    else
                    {
                        sql = string.Format("select count(1) AS routeNum from M_ClientRoute where ShopCD = '{0}' and ClientCD = '{1}' and RouteNo = '{2}' and WeekNo = '{3}' and Route <> '{4}'", shopCD, _client.ClientCD, 3, newRoute.WeekNo, item.RouteFrom);

                        adapter.SelectCommand = new SqlCommand(sql, connection);
                        dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0 && DataUtil.CInt(dt.Rows[0]["routeNum"]) == 0)
                        {
                            newRoute.RouteNo = 3;
                        }
                        else
                        {
                            string weekStr = "";
                            if (newRoute.WeekNo == 1)
                            {
                                weekStr = "A週";
                            }
                            else if (newRoute.WeekNo == 2)
                            {
                                weekStr = "B週";
                            }
                            else if (newRoute.WeekNo == 3)
                            {
                                weekStr = "C週";
                            }
                            else if (newRoute.WeekNo == 4)
                            {
                                weekStr = "D週";
                            }
                            item.Msg = "【" + weekStr + "】三つ存在していますので、変更できません。";
                            item.No = errorList.Count + 1;
                            errorList.Add(item);
                            continue;
                        }
                    }
                }

                dbContext.ClientRoutes.Add(newRoute);
                okCount++;
            }
            connection.Close();
            result.Message = okCount + "件変更完了しました<br />";

            dbContext.SaveChanges();
            return result;
        }

        /// <summary>
        ///  顧客管理画面
        /// 
        /// 保存処理
        /// </summary>
        /// <param name="data"></param>
        public Result TantoBatchSet(HoRequest req, List<ClientRouteTantoViewModel> datas, string shopCD)
        {
            Result result = new Result();

            int okCount = 0;

            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string sql = "";


            foreach (ClientRouteTantoViewModel item in datas)
            {
                //route change

                M_Client _client = dbContext.Clients.Find(shopCD, item.ClientCD);
                if (item.TantoCDFrom != item.TantoCDTo)
                {
                    _client.TantoCD = item.TantoCDTo;
                    _client.UpdateUser = this._entityRequest.User;
                    _client.UpdateTime = CommonUtils.GetDateTimeNow();
                    dbContext.Clients.AddOrUpdate(_client);
                }
            }
            connection.Close();
            result.Message = okCount + "件変更完了しました<br />";

            dbContext.SaveChanges();
            return result;
        }

        /// <summary>
        ///  顧客管理画面
        /// 
        /// 保存処理
        /// </summary>
        /// <param name="data"></param>
        public Result SaveData(HoRequest req, M_Client _client, T_HoClient hoClient,string hoClientUpdateTime,string clientUpdateTime, List<M_ClientInitItems> clientInitDelList, List<T_HoClientItem> hoClientDelList, bool newMode)
        {
            Result result = new Result();
            string _hoClientUpdateTime = "";
            string _clientUpdateTime = "";
            string[] keys = { _client.ShopCD, _client.ClientCD };

            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string sql = "";

            if (newMode)
            {
                string[] keyFields = { "ShopCD", "ClientCD" };

                if (this.HasData(keyFields, keys))
                {
                    result.Message = "このデータはすでに存在しています。";
                    result.ReturnValue = EnumResult.Error;
                    result.ErrorKey = "key";
                    connection.Close();
                    return result;
                }
            }

            string[] clientNameField = { "ShopCD", "ClientName" };
            string[] clientNameValue = { _client.ShopCD, _client.ClientName };
            if (this.UniqueCheck(keys, clientNameField, clientNameValue))
            {
                result.Message = "このデータはすでに存在しています。";
                result.ReturnValue = EnumResult.Error;
                result.ErrorKey = "ClientName";
                connection.Close();
                return result;
            }

            string[] kanriClientCDField = { "ShopCD", "KanriClientCD" };
            string[] kanriClientCDValue = { _client.ShopCD, _client.KanriClientCD };
            if (this.UniqueCheck(keys, kanriClientCDField, kanriClientCDValue))
            {
                result.Message = "この販売管理顧客コードデータはすでに存在しています。";
                result.ReturnValue = EnumResult.Error;
                result.ErrorKey = "KanriClientCD";
                connection.Close();
                return result;
            }

            M_Client _oldClient = null;

            this.GetData(_client.GetKey(), out _oldClient);
            if (_oldClient.ClientCD != null)
            {

                sql = string.Format("select * from M_Client where ShopCD = '{0}' and ClientCD = '{1}'", _oldClient.ShopCD, _oldClient.ClientCD);

                adapter.SelectCommand = new SqlCommand(sql, connection);
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DateTime? time = DataUtil.CDate(dt.Rows[0]["UpdateTime"]);
                    string clientUpdateTimeIndb = time == null ? "" : time.Value.ToString("yyyy/MM/dd HH:mm:ss fff");
                    if (clientUpdateTimeIndb != clientUpdateTime)
                    {
                        result.Message = "データは、他の端末（" + _oldClient.UpdateUser + "）より更新されました。　閉じるボタンを押して再度やり直して下さい。";
                        result.ReturnValue = EnumResult.Haita;
                        result.ErrorKey = "haita";
                        connection.Close();
                        return result;
                    }
                }

                var roteQuery = from r in _oldClient.RouteDetail
                                join new_r in _client.RouteDetail on r.Route equals new_r.Route into g_route
                                from rItem in g_route.DefaultIfEmpty(null)

                                where rItem == null
                                select r;

                if (roteQuery.Count() > 0)
                {
                    var rotePlanedQuery = (from client in dbContext.HoOrderClients

                                           //join tanto in db.HoOrders on new { ShopCD = client.ShopCD, HoDate = client.HoDate, TantoCD = client.TantoCD } equals new { ShopCD = tanto.ShopCD, HoDate = tanto.HoDate, TantoCD = tanto.TantoCD } into g_route
                                           //from rItem in g_route.DefaultIfEmpty(null)


                                           where client.ShopCD == _client.ShopCD
                                           && client.ClientCD == _client.ClientCD
                                           && (client.Seq == 0 || client.Seq == _client.LastSeq)
                                           && client.DoneFlag != "3"

                                           select client).Distinct();

                    List<T_HoOrderClient> tantos = rotePlanedQuery.ToList();
                    List<M_ClientRoute> rotes = new List<M_ClientRoute>();

                    foreach (M_ClientRoute rote in roteQuery)
                    {
                        var t = tantos.Where(e => e.Route == rote.Route);
                        if (t.Count() > 0)
                        {
                            rotes.Add(rote);
                        }
                    }

                    if (rotes.Count > 0)
                    {

                        string msg = "";
                        foreach (M_ClientRoute rote in rotes)
                        {
                            if (msg.Length == 0)
                            {
                                msg = "【" + rote.Route + "】";
                            }
                            else
                            {
                                msg += "、【" + rote.Route + "】";
                            }
                        }

                        result.Message = "ルート" + msg + "現在使用していますので、変更削除できません。";
                        result.ReturnValue = EnumResult.Error;
                        result.ErrorKey = "";
                        connection.Close();
                        return result;
                    }
                }
            }


            //解約
            if (_client.CancelFlg == true)
            {
                adapter = new SqlDataAdapter();
                dt = new DataTable();

                //未、中チェック
                sql = string.Format("select Top 1 DoneFlag ,HoDate from T_HoOrderClient  "
                   + " where ShopCD = '{0}' and ClientCD = '{1}' order by HoDate desc", _client.ShopCD, _client.ClientCD);

                adapter.SelectCommand = new SqlCommand(sql, connection);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0) {
                    int doneFlag1 = DataUtil.CInt(dt.Rows[0]["DoneFlag"]);
                    string hoDate2 = DataUtil.CStr(dt.Rows[0]["HoDate"]);
                    if (doneFlag1 != 3)
                    {
                        result.Message = "未処理が残っている為、解約できません。";
                        result.ReturnValue = EnumResult.Error;
                        result.ErrorKey = "";
                        connection.Close();
                        return result;
                    }
                }
               

                dt = new DataTable();

                sql = string.Format("select SUM(T_HoClientItem.ThisNum) AS SUMNUM"
                    + ", SUM(T_HoClientItem.AfterNum) AS SUMAfterNUM"
                    + ",Max(T_HoOrderClient.DoneFlag) AS DoneFlag , Max(T_HoOrderClient.HoDate) AS HoDate from T_HoClientItem "
                    + "left join M_Client ON T_HoClientItem.ShopCD = M_Client.ShopCD and T_HoClientItem.ClientCD = M_Client.ClientCD "
                    + "left join T_HoClient ON T_HoClientItem.ShopCD = T_HoClient.ShopCD and T_HoClientItem.ClientCD = T_HoClient.ClientCD and T_HoClientItem.Seq = T_HoClient.Seq  "
                    + "left join T_HoOrderClient ON T_HoClientItem.ShopCD = T_HoOrderClient.ShopCD and T_HoClientItem.ClientCD = T_HoOrderClient.ClientCD and T_HoClient.HoDate = T_HoOrderClient.HoDate "
                    + " where T_HoClientItem.Seq = M_Client.LastSeq and T_HoClientItem.ShopCD = '{0}' and T_HoClientItem.ClientCD = '{1}'", _client.ShopCD, _client.ClientCD);

                adapter.SelectCommand = new SqlCommand(sql, connection);
                adapter.Fill(dt);
                
                int sumNum = DataUtil.CInt(dt.Rows[0]["SUMNUM"]);
                int sumAfterNum = DataUtil.CInt(dt.Rows[0]["SUMAfterNUM"]);
                int doneFlag = DataUtil.CInt(dt.Rows[0]["DoneFlag"]);
                string hoDate = DataUtil.CStr(dt.Rows[0]["HoDate"]);

                //if ((sumNum == 0 || (sumAfterNum == 0 && doneFlag == 3)) == false)
                if (sumAfterNum != 0)
                {
                    sql = string.Format("select Max(T_HoOrderClient.HoDate) AS HoDate from T_HoOrderClient "
                   + " where T_HoOrderClient.ShopCD = '{0}' and T_HoOrderClient.ClientCD = '{1}'", _client.ShopCD, _client.ClientCD);

                    adapter.SelectCommand = new SqlCommand(sql, connection);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    string hoDateNew = "";
                    if (dt.Rows.Count > 0) {
                        hoDateNew = DataUtil.CStr(dt.Rows[0]["HoDate"]);
                    }



                    if (hoDate != hoDateNew)
                    {
                        result.Message = "未処理が残っている為、解約できません。";
                        result.ReturnValue = EnumResult.Error;
                        result.ErrorKey = "";
                        connection.Close();
                        return result;
                    }
                    else {
                        result.Message = "現在庫が残っていますので、解約できません。";
                        result.ReturnValue = EnumResult.Error;
                        result.ErrorKey = "";
                        connection.Close();
                        return result;
                    }
                   
                }
                else if ((sumAfterNum == 0 && doneFlag == 2))
                {
                    result.Message = "未処理が残っている為、解約できません。";
                    result.ReturnValue = EnumResult.Error;
                    result.ErrorKey = "";
                    connection.Close();
                    return result;
                }



                //初回計画有る判断
                if (sumAfterNum == 0 && _client.LastSeq == 0)
                {
                    adapter = new SqlDataAdapter();
                    dt = new DataTable();

                    sql = string.Format("select FirstFlag from T_HoOrderClient  where ShopCD = '{0}' and ClientCD = '{1}'", _client.ShopCD, _client.ClientCD);

                    adapter.SelectCommand = new SqlCommand(sql, connection);
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 1 && DataUtil.CStr(dt.Rows[0]["FirstFlag"]) == "1")
                    {
                        result.Message = "現在庫が残っていますので、解約できません。";
                        result.ReturnValue = EnumResult.Error;
                        result.ErrorKey = "";
                        connection.Close();
                        return result;
                    }
                }
            }
            

            //string[] clientKanaField = { "ClientKana" };
            //string[] clientKanaValue = { _client.ClientKana };
            //if (this.UniqueCheck(keys, clientKanaField, clientKanaValue))
            //{
            //    result.Message = "このデータはすでに存在しています。";
            //    result.ReturnValue = EnumResult.Error;
            //    result.ErrorKey = "ClientKana";
            //    return result;
            //}

            //TransactionOptions transactionOption = new TransactionOptions();
            //transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            
           
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    if (hoClient != null)
                    {
                        T_HoClient hoClientIndb = dbContext.HoClients.Find(hoClient.ShopCD, hoClient.ClientCD, hoClient.Seq);
                        if (hoClientIndb != null)
                        {
                            string hoClientUpdateTimeIndb = hoClientIndb.UpdateTime == null ? "" : hoClientIndb.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss fff");
                            if (hoClientUpdateTimeIndb != hoClientUpdateTime)
                            {
                                result.Message = "データは、他の端末（" + hoClientIndb.UpdateUser + "）より更新されました。　閉じるボタンを押して再度やり直して下さい。";
                                result.ReturnValue = EnumResult.Haita;
                                result.ErrorKey = "haita";
                                connection.Close();
                                return result;
                            }
                        }

                        //if (clientIndb != null && clientIndb.SoldMoney != hoClient.SoldMoney)
                        //{
                        //    db.HoClients.AddOrUpdate(clientIndb);
                        //}
                        hoClientIndb.UpdateUser = this._entityRequest.User;
                        hoClientIndb.UpdateTime = CommonUtils.GetDateTimeNow();
                        dbContext.HoClients.AddOrUpdate(hoClientIndb);
                    }

                    _client.UpdateUser = this._entityRequest.User;
                    _client.UpdateTime = CommonUtils.GetDateTimeNow();
                    dbContext.Clients.AddOrUpdate(_client);
                    dbContext.ClientRoutes.RemoveRange(dbContext.ClientRoutes.Where(e => e.ShopCD == _client.ShopCD && e.ClientCD == _client.ClientCD));
                    //解約の場合ルートを削除
                    if (_client.CancelFlg != true)
                    {
                        dbContext.ClientRoutes.AddRange(_client.RouteDetail.ToArray());
                    }

                    

                    for (int i = clientInitDelList.Count - 1; i >= 0; i--)
                    {
                        M_ClientInitItems dItem = clientInitDelList[i];
                        string dKey = dItem.ShopCD + "," + dItem.ClientCD + "," + dItem.ShelfCD + "," + dItem.ItemCD;
                        foreach (var item in _client.InitItemDetail)
                        {
                            if (dKey == item.ShopCD + "," + item.ClientCD + "," + item.ShelfCD + "," + item.ItemCD)
                            {
                                clientInitDelList.RemoveAt(i);
                            }
                        }
                    }

                    foreach (var item in clientInitDelList)
                    {
                        dbContext.ClientItems.RemoveRange(dbContext.ClientItems.Where(e => e.ShopCD == item.ShopCD && e.ClientCD == item.ClientCD && e.ShelfCD == item.ShelfCD && e.ItemCD == item.ItemCD));
                    }
                    dbContext.ClientItems.AddOrUpdate(_client.InitItemDetail.ToArray());
                    dbContext.HoClientItems.AddOrUpdate(_client.HoDetail.ToArray());

                    if (hoClientDelList != null)
                    {
                        foreach (T_HoClientItem item in hoClientDelList)
                        {
                            dbContext.HoClientItems.RemoveRange(dbContext.HoClientItems.Where(e => e.ShopCD == item.ShopCD && e.ClientCD == item.ClientCD && e.ItemCD == item.ItemCD && e.Seq == item.Seq));
                        }
                    }

                    if (_client.AfterDate != null) {
                        T_HoOrderClient oederclient = this.GetLastOrderClient(_client.ShopCD, _client.ClientCD);

                        if (oederclient != null && oederclient.AfterStopFlag == "2" && oederclient.AfterDate != _client.AfterDate)
                        {
                            oederclient.AfterDate = _client.AfterDate;
                            dbContext.HoOrderClients.AddOrUpdate(oederclient);
                        }
                    }

                    

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

          
            if (hoClient != null)
            {
                sql = string.Format("select * from T_hoClient  where ShopCD = '{0}' and ClientCD = '{1}' and Seq = {2}", hoClient.ShopCD, hoClient.ClientCD, hoClient.Seq);
                dt = new DataTable();
                adapter.SelectCommand = new SqlCommand(sql, connection);
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DateTime? time = DataUtil.CDate(dt.Rows[0]["UpdateTime"]);
                    _hoClientUpdateTime = time.Value.ToString("yyyy/MM/dd HH:mm:ss fff");
                }
            }

            sql = string.Format("select * from M_Client where ShopCD = '{0}' and ClientCD = '{1}'", _client.ShopCD, _client.ClientCD);
            dt = new DataTable();
            adapter.SelectCommand = new SqlCommand(sql, connection);
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DateTime? time = DataUtil.CDate(dt.Rows[0]["UpdateTime"]);
                _clientUpdateTime = time.Value.ToString("yyyy/MM/dd HH:mm:ss fff");
            }

           //clientIndb = db.HoClients.Find(hoClient.ShopCD, hoClient.ClientCD, hoClient.Seq);
           //if (clientIndb != null)
           //{
           //    updateTime = clientIndb.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
           //}
            connection.Close();
            return new Result() { 
                ReturnValue = EnumResult.OK,
                Message = _hoClientUpdateTime,
                ErrorKey = _clientUpdateTime
            };
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

            //分组排序，取时间最大的一条记录
            var HoBeforeDayClientsQuary = from c in dbContext.HoOrderClients
                                          where c.ShopCD == shopCD && c.ClientCD == clientCD
                                          group c by c.ClientCD into cg
                                          let maxId = cg.Max(a => a.HoDate)
                                          from row in cg
                                          where row.HoDate == maxId
                                          select row;

            List<T_HoOrderClient> HoBeforeDayClients = HoBeforeDayClientsQuary.ToList();

            if (HoBeforeDayClients.Count > 0)
            {
                return HoBeforeDayClients[0];
            }

            return null;
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="id"></param>
        public Result Delete(HoRequest req, M_Client.Key key)
        {
            TableDeleteRequest dreq = new TableDeleteRequest(key.ShopCD, key.ClientCD);
            return base.Delete(dreq);

            //string sql;

            //sql = string.Format("DELETE FROM " + typeof(M_Client).Name + " WHERE ShopCD='{0}' AND ClientCD='{1}'",
            //     key.ShopCD, key.ClientCD);

            //db.Database.ExecuteSqlCommand(sql);

            //sql = string.Format("DELETE FROM " + typeof(M_ClientInitItems).Name + " WHERE ShopCD='{0}' AND ClientCD='{1}'",
            //     key.ShopCD, key.ClientCD);

            //sql = string.Format("DELETE FROM " + typeof(M_ClientRoute).Name + " WHERE ShopCD='{0}' AND ClientCD='{1}'",
            //    key.ShopCD, key.ClientCD);

            //db.Database.ExecuteSqlCommand(sql);

           // return new Result();
        }

        internal PageViewResult GetClientRefer(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(M_Client).Name, Y_EntityViewData.顧客参照);

            return view.GetPageView(req);
        }

        internal PageViewResult GetClientList(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(M_Client).Name, Y_EntityViewData.顧客一覧);

            return view.GetPageView(req);
        }
    }
}