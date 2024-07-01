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
using System.Transactions;
using DrinkService.Utils;
using System.Data.SqlClient;
using SafeNeeds.DySmat.Util;

namespace DrinkService.Models
{
    /// <summary>
    /// 補充集金計画
    /// </summary>
    public class T_HoDayAdapter : HoEntityAdapterBase
    {
        public T_HoDayAdapter(EntityRequest request)
            : base(request, typeof(T_HoDay).Name)
        {
        }

        /// <summary>
        /// 補充集金指示リスト
        /// 一覧表示
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PageViewResult GetOrderList(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(T_HoOrderClient).Name, Y_EntityViewData.補充集金指示リスト);

            return view.GetPageView(req);

        }

        /// <summary>
        /// 補充集金照会
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PageViewResult GetOrderRefer(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(T_HoOrderClient).Name, Y_EntityViewData.補充集金照会);

            return view.GetPageView(req);

        }

        /// <summary>
        /// 未処理補充集金照会
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PageViewResult GetUndoOrderRefer(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(T_HoOrderClient).Name, Y_EntityViewData.未処理補充集金照会);

            return view.GetPageView(req);

        }
        

        /// <summary>
        /// 補充集金指示書作成画面
        /// 
        /// 初期データ取得
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public Result GetData(T_HoDay.Key key, out T_HoDay data)
        {
            data = dbContext.HoDays.Find(key.ShopCD, key.HoDate);

            if (data==null)
            {
                data = new T_HoDay();
                //data.Detail = new List<T_HoOrderClient>();
            }
            else
            {
                //data.Detail = db.HoOrderClients.Where(e => e.ShopCD == key.ShopCD && e.HoDate == key.HoDate).ToList();
            }

            return new Result();

        }
        /// <summary>
        ///  補充集金指示書作成画面
        /// 
        /// 保存処理
        /// </summary>
        /// <param name="data"></param>
        public Result SaveData(T_HoDay data, List<T_HoOrderClient> delClients, List<T_HoOrderTanto> delTantos)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    List<M_Client> clients = dbContext.Clients.Where(e => e.ShopCD == data.ShopCD).ToList();

                    foreach (T_HoOrderClient c in delClients)
                    {
                        dbContext.HoOrderClients.RemoveRange(dbContext.HoOrderClients.Where(e => e.ShopCD == c.ShopCD && e.HoDate == c.HoDate && e.TantoCD == c.TantoCD && e.ClientCD == c.ClientCD));

                        //M_Client cli = clients.Find(e => e.ClientCD == c.ClientCD);
                        //if (cli != null && cli.LastSeq > 0)
                        //{
                        //    var preQuery = db.HoOrderClients.Where(e => e.ShopCD == c.ShopCD && e.ClientCD == c.ClientCD && e.Seq == cli.LastSeq);

                        //    if (preQuery.Count() > 0) {
                        //        T_HoOrderClient preClient = preQuery.First();
                        //        preClient.DoneFlag = "1";
                        //        db.HoOrderClients.AddOrUpdate(preClient);
                        //    }


                        //}

                    }

                    foreach (T_HoOrderTanto c in delTantos)
                    {
                        dbContext.HoOrders.RemoveRange(dbContext.HoOrders.Where(e => e.ShopCD == c.ShopCD && e.HoDate == c.HoDate && e.TantoCD == c.TantoCD));
                    }

                    //db.HoDays.Remove(db.HoDays.Find(data.ShopCD, data.HoDate));

                    data.UpdateUser = this._entityRequest.User;
                    data.UpdateTime = CommonUtils.GetDateTimeNow();
                    dbContext.HoDays.AddOrUpdate(data);

                    foreach (T_HoOrderClient c in data.Detail)
                    {
                        M_Client cli = clients.Find(e => e.ClientCD == c.ClientCD);
                        if (cli != null)
                        {


                            //last seq check
                            SqlConnection connection = new SqlConnection(Config.ConnectionString);
                            SqlDataAdapter adapter = new SqlDataAdapter();
                            bool seqOk = true;
                            string sql = string.Format("select max(seq) seq from T_hoClient  where ShopCD = '{0}' and ClientCD = '{1}'", cli.ShopCD, cli.ClientCD);


                            DataTable dt = new DataTable();
                            adapter.SelectCommand = new SqlCommand(sql, connection);
                            adapter.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                int lastSeq = DataUtil.CInt(dt.Rows[0]["seq"]);

                                if (lastSeq > cli.LastSeq) {
                                    cli.LastSeq = lastSeq;
                                    seqOk = false;
                                }
                            }

                            sql = string.Format("select max(seq) seq from T_HoOrderClient  where ShopCD = '{0}' and ClientCD = '{1}'", cli.ShopCD, cli.ClientCD);


                            dt = new DataTable();
                            adapter.SelectCommand = new SqlCommand(sql, connection);
                            adapter.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                int lastSeq = DataUtil.CInt(dt.Rows[0]["seq"]);

                                if (lastSeq > cli.LastSeq)
                                {
                                    cli.LastSeq = lastSeq;
                                    seqOk = false;
                                }
                            }

                            if (!seqOk)
                            {
                                dbContext.Clients.AddOrUpdate(cli);
                            }



                            var preQuery = dbContext.HoOrderClients.Where(e => e.ShopCD == c.ShopCD && e.ClientCD == c.ClientCD && e.Seq == cli.LastSeq);

                            if (preQuery.Count() > 0)
                            {
                                T_HoOrderClient preClient = preQuery.First();
                                preClient.DoneFlag = "3";
                                dbContext.HoOrderClients.AddOrUpdate(preClient);
                            }
                            connection.Close();
                        }

                    }

                    dbContext.SaveChanges();
                    dbContext.HoOrderClients.AddOrUpdate(data.Detail.ToArray());
                    data.DetailTanto.ForEach(d =>
                    {
                        d.UpdateUser = this._entityRequest.User;
                        d.UpdateTime = CommonUtils.GetDateTimeNow();
                    });
                    dbContext.HoOrders.AddOrUpdate(data.DetailTanto.ToArray());
                    dbContext.SaveChanges();

                    if (dbContext.HoOrderClients.Any(e => e.HoDate == data.HoDate && e.ShopCD == data.ShopCD) == false)
                    {
                        dbContext.HoDays.RemoveRange(dbContext.HoDays.Where(e => e.ShopCD == data.ShopCD && e.HoDate == data.HoDate));
                        dbContext.SaveChanges();
                    }
                    scope.Complete();
                    return new Result();
                    
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

        /// <summary>
        ///  
        /// 
        /// 保存処理
        /// </summary>
        /// <param name="data"></param>
        public Result CancelData(List<T_HoOrderClient> delClients, List<T_HoOrderTanto> delTantos)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    foreach (T_HoOrderClient c in delClients)
                    {
                        dbContext.HoOrderClients.RemoveRange(dbContext.HoOrderClients.Where(e => e.ShopCD == c.ShopCD && e.HoDate == c.HoDate && e.TantoCD == c.TantoCD && e.ClientCD == c.ClientCD));
                    }

                    foreach (T_HoOrderTanto c in delTantos)
                    {
                        dbContext.HoOrders.RemoveRange(dbContext.HoOrders.Where(e => e.ShopCD == c.ShopCD && e.HoDate == c.HoDate && e.TantoCD == c.TantoCD));
                    }


                    dbContext.SaveChanges();
                   
                    scope.Complete();
                    return new Result();

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

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="id"></param>
        public Result Delete(T_HoDay.Key key)
        {

            //string sql=string.Format("DELETE FROM " +typeof(T_HoDay).Name + " WHERE ShopCD='{0}' AND HoDate='{1}'",
            //     key.ShopCD,key.HoDate);

            //db.Database.ExecuteSqlCommand(sql);

            //sql = string.Format("DELETE FROM " + typeof(T_HoOrderClient).Name + " WHERE ShopCD='{0}' AND HoDate='{1}'",
            //     key.ShopCD, key.HoDate);

            //db.Database.ExecuteSqlCommand(sql);

            //sql = string.Format("DELETE FROM " + typeof(T_HoOrderTanto).Name + " WHERE ShopCD='{0}' AND HoDate='{1}'",
            //    key.ShopCD, key.HoDate);

            //db.Database.ExecuteSqlCommand(sql);

            //return new Result();

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    List<M_Client> clients = dbContext.Clients.Where(e => e.ShopCD == key.ShopCD).ToList();
                    List<T_HoOrderClient> delClients = dbContext.HoOrderClients.Where(e => e.ShopCD == key.ShopCD && e.HoDate == key.HoDate).ToList();

                    TableDeleteRequest dreq = new TableDeleteRequest(key.ShopCD, key.HoDate);

                    Result result = base.Delete(dreq);
                    dbContext.SaveChanges();
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
            //if (result.ReturnValue == EnumResult.OK) {
            //    foreach (T_HoOrderClient c in delClients)
            //    {
                    
            //        M_Client cli = clients.Find(e => e.ClientCD == c.ClientCD);
            //        if (cli != null && cli.LastSeq > 0)
            //        {
            //            T_HoOrderClient preClient = db.HoOrderClients.Where(e => e.ShopCD == c.ShopCD && e.ClientCD == c.ClientCD && e.Seq == cli.LastSeq).First();
            //            preClient.DoneFlag = "1";
            //            db.HoOrderClients.AddOrUpdate(preClient);
            //        }

            //    }

            //}
            
          
        }


        internal PageViewResult GetSaleDataList(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(T_HoDay).Name, Y_EntityViewData.売上データ出力);

            return view.GetPageView(req);
        }
    }
}