using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DrinkService.Utils;
using DrinkService.Data.Logics;
using DrinkService.Data.Models;
using DrinkService.Data.ViewModels;
using DrinkService.Filters;
using SafeNeeds.DySmat;
using System.Transactions;
using DrinkService.Data.Common;

namespace DrinkService.Controllers
{
    [AuthenticationFilter]
    [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
    public class ClientController : BaseController
    {

        public ActionResult ClientEdit(string shopCD, string clientCD)
        {
            M_Client client = new M_Client();
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,opt))
            {
                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                ClientLogic clientLogic = new ClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                M_Shop shop = shopLogic.GetShopByShopCD(shopCD);
                ViewBag.shopCD = shopCD;
                ViewBag.shopName = shop.ShopName;

                if (!string.IsNullOrEmpty(shopCD) && !string.IsNullOrEmpty(clientCD))
                {
                    client = clientLogic.GetClientByKey(shopCD, clientCD);
                }

                if (shopCD != null && clientCD != null)
                {
                    //後日
                    T_HoOrderClient oederclient = clientLogic.GetLastOrderClient(client.ShopCD, client.ClientCD);

                    if (oederclient != null && oederclient.AfterStopFlag == "2")
                    {
                        client.AfterDate = oederclient.AfterDate;

                        HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName, ""));

                        DateTime nextHoDate = hoOrderClientLogic.GetNextHoDate(client.ShopCD, client.ClientCD, oederclient.Route);

                        ViewBag.NextHoDate = nextHoDate;
                    }
                    else
                    {
                        client.AfterDate = null;
                    }
                }
                else
                {
                    client.AfterDate = null;
                }

                shopLogic.Dispose();
                clientLogic.Dispose();

            }
            return View(client);
        }

        public ActionResult ClientList()
        {
            return View();
        }

        public JsonResult ClientItemList(string ClientCD)
        {
            List<M_Client> clients = new List<M_Client>(M_ClientData.GetData());

            return Json(clients, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClientKindList()
        {
            List<M_Client> clients = new List<M_Client>(M_ClientData.GetData());

            return Json(clients, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClientSearch(string shopCD, string clientName, string staffCD, string pageNumber)
        {
            PagedResult data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ClientLogic clientLogic = new ClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = clientLogic.GetPagedClientList(shopCD, clientName, staffCD, pageNumber);
                clientLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult ClientCsv(string shopCD, string clientName)
        {
            List<ClientListViewModel> data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ClientLogic clientLogic = new ClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = clientLogic.GetClientList(shopCD, clientName);
                clientLogic.Dispose();
            }
            if (data.Count == 0)
            {
                return Json(new { ResultType = "NoData" });
            }

            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());

            String fileName = string.Format("\\CSV\\Client_{0}.csv", date);

            CsvUtils.ModlesToCsv<ClientListViewModel>(basePath + fileName, data);

            return Json(new { Path = string.Format("/CSV/Client_{0}.csv", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClientInitItems(string shopCD, string clientCD)
        {
            object data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ClientLogic clientLogic = new ClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                clientCD = System.Web.HttpUtility.HtmlDecode(clientCD);
                data = clientLogic.GetClientEditData(shopCD, clientCD);
                clientLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClientEditList(string ClientCD)
        {
            List<M_Client> kits = new List<M_Client>(M_ClientData.GetData());

            return Json(clients, JsonRequestBehavior.AllowGet);
        }

        public object clients { get; set; }

        public JsonResult Save(M_Client client, T_HoClient hoClient, string hoClientUpdateTime, string clientUpdateTime, string FirstDate,string AfterDate, List<ClientEditRouteViewModel> routeList, List<M_ClientInitItems> clientInitItemAddList, List<M_ClientInitItems> clientInitItemDelList, List<M_ClientInitItems> clientInitItemUpdateList, List<T_HoClientItem> hoClientUpdateList, List<T_HoClientItem> hoClientAddList, List<T_HoClientItem> hoClientDelList, bool newMode)
        {
            //locker
            using (DrinkLocker locker = new DrinkLocker(client.ShopCD))
            {
                ClientLogic clientLogic = new ClientLogic(new EntityRequest(1, loginUser.StaffName, ""));

                Result result = null;

                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                //log
                UserSession userInfo = Session["user"] as UserSession;
                LogContent logcontent = new LogContent();
                logcontent.FunctionPath = ViewBag.ControllerName;
                logcontent.FunctionName = ViewBag.ActionName;
                logcontent.FunctionType = "save";
                logcontent.LogType = "info";
                logcontent.Key1 = userInfo.ShopCD;
                logcontent.Key2 = userInfo.StaffCD;

                logcontent.AddContent("user", userInfo);
                logcontent.AddContent("client", client);
                logcontent.AddContent("hoClient", hoClient);
                logcontent.AddContent("hoClientUpdateTime", hoClientUpdateTime);
                logcontent.AddContent("clientUpdateTime", clientUpdateTime);
                logcontent.AddContent("FirstDate", FirstDate);
                logcontent.AddContent("AfterDate", AfterDate);
                logcontent.AddContent("clientInitItemAddList", clientInitItemAddList);
                logcontent.AddContent("clientInitItemDelList", clientInitItemDelList);
                logcontent.AddContent("clientInitItemUpdateList", clientInitItemUpdateList);
                logcontent.AddContent("hoClientUpdateList", hoClientUpdateList);
                logcontent.AddContent("hoClientAddList", hoClientAddList);
                logcontent.AddContent("hoClientDelList", hoClientDelList);
                logcontent.AddContent("newMode", newMode);
                LogLogic logLogic = new LogLogic(enreq);

                try
                {
                    result = clientLogic.Save(client, hoClient, hoClientUpdateTime, clientUpdateTime, FirstDate, AfterDate, routeList, clientInitItemAddList, clientInitItemDelList, clientInitItemUpdateList, hoClientUpdateList, hoClientAddList, hoClientDelList, newMode);
                }
                catch (Exception e)
                {
                    logcontent.LogType = "error";
                    logcontent.AddContent("errorMsg", e.Message);
                    logcontent.AddContent("StackTrace", e.StackTrace);
                    logLogic.WriteLog(logcontent);
                    throw;
                }

                //logLogic.WriteLog(logcontent);
                clientLogic.Dispose();
                logLogic.Dispose();
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult Del(M_Client client)
        {
            //locker
            using (DrinkLocker locker = new DrinkLocker(client.ShopCD))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                M_ClientAdapter adapter = new M_ClientAdapter(enreq);
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        Result result = adapter.Delete(new HoRequest(), new M_Client.Key() { ShopCD = client.ShopCD, ClientCD = client.ClientCD });
                        scope.Complete();

                        if (result.ReturnValue == EnumResult.OK)
                        {
                            result.Message = "顧客削除完了しました。";
                        }
                        else if (result.ReturnValue == EnumResult.Error)
                        {
                            result.Message = "関連データが存在している為、削除できません。";
                        }
                        adapter.Dispose();
                        return Json(result, JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// ルート担当者一括変更
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="shopCD"></param>
        /// <param name="routeFrom"></param>
        /// <param name="routeTo"></param>
        /// <param name="tantoFrom"></param>
        /// <param name="tantoTo"></param>
        /// <returns></returns>
        public JsonResult RouteBatchSetCheck(List<ClientRouteTantoViewModel> datas, string shopCD)
        {
            //locker
            using (DrinkLocker locker = new DrinkLocker(shopCD))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                M_ClientAdapter adapter = new M_ClientAdapter(enreq);
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        List<ClientRouteTantoViewModel> errorList = adapter.RouteBatchSetCheck(new HoRequest(), datas, shopCD);

                        Result result = new Result();

                        if (errorList.Count() > 0)
                        {
                            result.data = errorList;
                            result.ErrorKey = "Error";
                        }
                        adapter.Dispose();
                        scope.Complete();
                        return Json(result, JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// ルート担当者一括変更
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="shopCD"></param>
        /// <param name="routeFrom"></param>
        /// <param name="routeTo"></param>
        /// <param name="tantoFrom"></param>
        /// <param name="tantoTo"></param>
        /// <returns></returns>
        public JsonResult RouteBatchSet(List<ClientRouteTantoViewModel> datas, string shopCD)
        {
            //locker
            using (DrinkLocker locker = new DrinkLocker(shopCD))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                M_ClientAdapter adapter = new M_ClientAdapter(enreq);
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        Result result = adapter.RouteBatchSet(new HoRequest(), datas, shopCD);
                        adapter.Dispose();
                        scope.Complete();
                        return Json(result, JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// ルート担当者一括変更
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="shopCD"></param>
        /// <param name="routeFrom"></param>
        /// <param name="routeTo"></param>
        /// <param name="tantoFrom"></param>
        /// <param name="tantoTo"></param>
        /// <returns></returns>
        public JsonResult TantoBatchSet(List<ClientRouteTantoViewModel> datas, string shopCD)
        {
            //locker
            using (DrinkLocker locker = new DrinkLocker(shopCD))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                M_ClientAdapter adapter = new M_ClientAdapter(enreq);
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        Result result = adapter.TantoBatchSet(new HoRequest(), datas, shopCD);
                        adapter.Dispose();
                        scope.Complete();
                        return Json(result, JsonRequestBehavior.AllowGet);
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
}