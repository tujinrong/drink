using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DrinkService.Data.Models;
using DrinkService.Data.ViewModels;
using DrinkService.Data.Logics;
using SafeNeeds.DySmat;
using DrinkService.Filters;
using DrinkService.Utils;
using System.Transactions;
using SafeNeeds.DySmat.Util;
using DrinkService.Data.Common;
using System.Data;

namespace DrinkService.Controllers
{
    public class DeliveryRouteController : BaseController
    {
        private CommonLogic commonLogic = new CommonLogic();

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult DeliveryRouteEdit(string ShopCD, string ShopName, string ClientCD, string ClientName, DateTime HoDate, string Route, string StaffCD, string StaffName, string FirstFlag, string DoneFlag)
        {
            //locker
            using (DrinkLocker locker = new DrinkLocker(ShopCD))
            {
                ViewBag.ShopCD = ShopCD;

                ViewBag.ClientCD = ClientCD;

                ViewBag.HoDate = HoDate;
                ViewBag.Route = Route;
                ViewBag.StaffCD = StaffCD;

                ViewBag.FirstFlag = FirstFlag;
                ViewBag.DoneFlag = DoneFlag;

                ClientLogic clientLogic = new ClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                M_Client client = clientLogic.GetClient(ShopCD, ClientCD);
                ViewBag.ClientName = client.ClientName;

                StaffLogic staffLogic = new StaffLogic(new EntityRequest(1, loginUser.StaffName, ""));
                M_Staff staff = staffLogic.GetStaffByKey(ShopCD, StaffCD);
                ViewBag.StaffName = staff.StaffName;

                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                M_Shop shop = shopLogic.GetShopByShopCD(ShopCD);
                ViewBag.ShopName = shop.ShopName;

                HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                T_HoOrderClient orderClient = hoOrderClientLogic.GetHoClientModel(ShopCD, HoDate, StaffCD, ClientCD, Route);
                if (orderClient != null)
                {
                    ViewBag.Seq = orderClient.Seq;
                }

                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");

                T_HoClientAdapter logic = new T_HoClientAdapter(enreq);

                if (orderClient.Seq > 0)
                {
                    //update

                    T_HoClient hoClient = null;
                    logic.GetHoClient(new T_HoClient.Key { ShopCD = ShopCD, ClientCD = ClientCD, Seq = orderClient.Seq }, out hoClient);
                    ViewBag.Memo = hoClient.Memo;
                    ViewBag.GetMoney = hoClient.GetMoney;
                    ViewBag.hoClientUpdateTime = hoClient.UpdateTime == null ? "" : hoClient.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss fff");
                    T_Sign hoSign = null;
                    logic.GetSign(new T_HoClient.Key { ShopCD = ShopCD, ClientCD = ClientCD, Seq = orderClient.Seq }, out hoSign);
                    ViewBag.signData = hoSign.SignData;
                }
                else
                {
                    //new

                    T_HoClient hoClient = null;
                    logic.GetHoClient(new T_HoClient.Key { ShopCD = ShopCD, ClientCD = ClientCD, Seq = client.LastSeq }, out hoClient);
                    ViewBag.Memo = hoClient.Memo;
                    ViewBag.GetMoney = "";
                    ViewBag.hoClientUpdateTime = hoClient.UpdateTime == null ? "" : hoClient.UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss fff");
                    T_Sign hoSign = null;
                    logic.GetSign(new T_HoClient.Key { ShopCD = ShopCD, ClientCD = ClientCD, Seq = orderClient.Seq }, out hoSign);
                    ViewBag.signData = hoSign.SignData;
                }


                DateTime nextHoDate = hoOrderClientLogic.GetNextHoDate(ShopCD, ClientCD, Route);

                ViewBag.NextHoDate = nextHoDate;
                ViewData["routeList"] = commonLogic.GetRouteList(ShopCD, null, false);
                logic.Dispose();
                clientLogic.Dispose();
                staffLogic.Dispose();
                shopLogic.Dispose();
                hoOrderClientLogic.Dispose();
            }
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult DeliveryRouteAfterStopEdit(string ShopCD, string ShopName, string ClientCD, string ClientName, DateTime HoDate, string Route, string StaffCD, string StaffName, string FirstFlag, string DoneFlag)
        {
            //locker
            using (DrinkLocker locker = new DrinkLocker(ShopCD))
            {

                ViewBag.ShopCD = ShopCD;

                ViewBag.ClientCD = ClientCD;

                ViewBag.HoDate = HoDate;
                ViewBag.Route = Route;
                ViewBag.StaffCD = StaffCD;

                ViewBag.FirstFlag = FirstFlag;

                ClientLogic clientLogic = new ClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                M_Client client = clientLogic.GetClient(ShopCD, ClientCD);
                ViewBag.ClientName = client.ClientName;

                StaffLogic staffLogic = new StaffLogic(new EntityRequest(1, loginUser.StaffName, ""));
                M_Staff staff = staffLogic.GetStaffByKey(ShopCD, StaffCD);
                ViewBag.StaffName = staff.StaffName;

                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                M_Shop shop = shopLogic.GetShopByShopCD(ShopCD);
                ViewBag.ShopName = shop.ShopName;

                HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                T_HoOrderClient orderClient = hoOrderClientLogic.GetHoClientModel(ShopCD, HoDate, StaffCD, ClientCD, Route);
                if (orderClient != null)
                {
                    ViewBag.Seq = orderClient.Seq;
                }

                DateTime nextHoDate = hoOrderClientLogic.GetNextHoDate(ShopCD, ClientCD, Route);

                ViewBag.NextHoDate = nextHoDate;


                ViewBag.AfterStopFlag = orderClient.AfterStopFlag;
                if (orderClient.AfterStopFlag == null)
                {
                    ViewBag.AfterStopFlag = "0";
                }

                ViewBag.AfterDate = orderClient.AfterDate;
                clientLogic.Dispose();
                staffLogic.Dispose();
                shopLogic.Dispose();
                hoOrderClientLogic.Dispose();
            }
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult SaveHoClient(T_HoClient hoClient,string hoClientUpdateTime, List<T_HoClientItem> addList, List<T_HoClientItem> updateList, string doneFlag, List<T_HoClientItem> delList,string signData)
        {
            //locker
            using (DrinkLocker locker = new DrinkLocker(hoClient.ShopCD))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");


                hoClient.Detail = updateList;

                Result result = null;

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
                logcontent.AddContent("hoClient", hoClient);
                logcontent.AddContent("hoClientUpdateTime", hoClientUpdateTime);
                logcontent.AddContent("addList", addList);
                logcontent.AddContent("doneFlag", doneFlag);
                logcontent.AddContent("delList", delList);
                LogLogic logLogic = new LogLogic(enreq);

                try
                {
                    using (T_HoClientAdapter logic = new T_HoClientAdapter(enreq))
                    {
                        result = logic.SaveData(hoClient, hoClientUpdateTime, addList, doneFlag, delList, signData);
                    }
                }
                catch (Exception e)
                {
                    logcontent.LogType = "error";
                    logcontent.AddContent("errorMsg", e.Message);
                    logcontent.AddContent("StackTrace", e.StackTrace);
                    logLogic.WriteLog(logcontent);
                    throw;
                }

                logLogic.Dispose();
                //logLogic.WriteLog(logcontent);

                if (result.ReturnValue == EnumResult.Haita)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { seq = hoClient.Seq, updateTime = result.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5")]
        public JsonResult SaveAfterStop(string ShopCD, string ClientCD, DateTime? HoDate, string Route, string StaffCD, string AfterStopFlag, DateTime? AfterDate, int Seq)
        {
             //locker
            using (DrinkLocker locker = new DrinkLocker(ShopCD))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");

                T_HoClientAdapter logic = new T_HoClientAdapter(enreq);

                Result result = null;

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
                logcontent.AddContent("ShopCD", ShopCD);
                logcontent.AddContent("ClientCD", ClientCD);
                logcontent.AddContent("HoDate", HoDate);
                logcontent.AddContent("Route", Route);
                logcontent.AddContent("StaffCD", StaffCD);
                LogLogic logLogic = new LogLogic(enreq);

                try
                {
                    result = logic.SaveAfterStop(ShopCD, ClientCD, HoDate, Route, StaffCD, AfterStopFlag, AfterDate, Seq);
                    logLogic.Dispose();
                    logic.Dispose();
                }
                catch (Exception e)
                {
                    logcontent.LogType = "error";
                    logcontent.AddContent("errorMsg", e.Message);
                    logcontent.AddContent("StackTrace", e.StackTrace);
                    logLogic.WriteLog(logcontent);
                    logic.Dispose();
                    logLogic.Dispose();
                    throw;
                }


                //logLogic.WriteLog(logcontent);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult GetHoClientItems(string ShopCD, string ClientCD, DateTime HoDate, string FirstFlag, string TantoCD,string showAll,string Route)
        {
             //locker
            using (DrinkLocker locker = new DrinkLocker(ShopCD))
            {
                HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                List<HoClientItemViewModel> clientItems = hoOrderClientLogic.GetHoClientItems(ShopCD, ClientCD, HoDate, FirstFlag, TantoCD, Route);

                if (string.IsNullOrEmpty(showAll))
                {
                    List<HoClientItemViewModel> clientItemsAdd = new List<HoClientItemViewModel>();

                    for (int i = clientItems.Count - 1; i >= 0; i--)
                    {
                        HoClientItemViewModel item = clientItems[i];

                        if (item.ItemAddFlag == "1")
                        {

                            clientItemsAdd.Insert(0, item);
                            clientItems.Remove(item);
                        }
                    }
                    hoOrderClientLogic.Dispose();
                    return Json(new { clientItems = clientItems, clientItemsAdd = clientItemsAdd }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    hoOrderClientLogic.Dispose();
                    return Json(clientItems, JsonRequestBehavior.AllowGet);
                }
            }
            
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult DeliveryRouteList()
        {
            
            List<object> routeList = new List<object>();

            routeList.Insert(0, new { RouteCD = "" });

            ViewData["routeList"] = routeList;

            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult DeliveryRouteListCreateRefer(string shopCD, string clientCD, string staffCD, string route, string hoDateFrom, string hoDateTo, string LastFlag, string pageNumber)
        {
            HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName , ""));
            PagedResult data = hoOrderClientLogic.GetPagedDeliveryRouteRefer(shopCD, clientCD, staffCD, route, hoDateFrom, hoDateTo, LastFlag, pageNumber);
            hoOrderClientLogic.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult DeliveryRouteUndoRefer(string shopCD, string clientCD, string staffCD, string route, string hoDateFrom, string hoDateTo, string LastFlag, string pageNumber)
        {
            HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
            PagedResult data = hoOrderClientLogic.GetPagedDeliveryRouteUndoRefer(shopCD, clientCD, staffCD, route, hoDateFrom, hoDateTo, LastFlag, pageNumber);
            hoOrderClientLogic.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult DeliveryRouteListCreateSearch(string shopCD, string staffCD, string route, string hoDate, string doneFlag, string LastFlag, string pageNumber)
        {
            HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName , ""));
            PagedResult data = hoOrderClientLogic.GetPagedDeliveryRouteList(shopCD, staffCD, route, hoDate, doneFlag,LastFlag, pageNumber);
            hoOrderClientLogic.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult DeliveryRouteListCreate()
        {
            ViewData["routeList"] = commonLogic.GetRouteList(null, CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd"), true);
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult DeliveryRouteListDoCreate(string shopCD,string shopName,string staffCD,string route,string hoDate)
        {
            ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName , ""));
            ViewBag.shopCD = shopCD;
            ViewBag.shopName = shopName;
            ViewBag.staffCD = staffCD;
            ViewBag.route = route;
            ViewBag.hoDate = hoDate;
            M_Shop shop = shopLogic.GetShopByShopCD(shopCD);
            ViewBag.shopName = shop.ShopName;

            EntityRequest enreq = new EntityRequest(1, loginUser.StaffName , "");

            T_HoClientAdapter logic = new T_HoClientAdapter(enreq);

            List<object> routes = logic.GetShopRoute(shopCD, hoDate,false);

            ViewData["routeList"] = routes;
            logic.Dispose();
            shopLogic.Dispose();
            return View();
        }

        public JsonResult GetClientItemLimitList(string shopCD, string dayLimit, int pageNumber = 1)
        {
            EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
            T_HoClientAdapter adapter = new T_HoClientAdapter(enreq);

            SafeNeeds.DySmat.Logic.DyEntityLogic logic = new SafeNeeds.DySmat.Logic.DyEntityLogic();

            PageViewResult list = adapter.GetClientItemLimitList(shopCD, dayLimit, false, false, pageNumber);
            adapter.Dispose();
            return Json(
                    new
                    {
                        pageSize = adapter._Proj.PageRows
                    ,
                        totalSize = list.PageCount
                    ,
                        pageNumber = pageNumber
                    ,
                        pageData = logic.DataTableToDic(list.DataTable)
                    ,
                        pageSeries = list.series
                    ,
                        pageCategories = list.categories
                    ,
                        pageCrossYItems = list.crossYItems
                    ,
                        Sql = list.SQL
                    ,
                        sqlTimes = list.sqlTimes
                    }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClientItemLimitListCSV(string shopCD, string dayLimit, DataExportSetting dataExportSetting)
        {
            EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
            T_HoClientAdapter adapter = new T_HoClientAdapter(enreq);

            SafeNeeds.DySmat.Logic.DyEntityLogic logic = new SafeNeeds.DySmat.Logic.DyEntityLogic();

            PageViewResult list = adapter.GetClientItemLimitList(shopCD, dayLimit, false,true, 1);

            if (list.DataTable.Columns.Contains("seriesFieldAutoSum"))
            {
                list.DataTable.Columns.Remove("seriesFieldAutoSum");
            }

            if (list.categories != null && list.categories.Count > 0)
            {
                dataExportSetting.ExportField = new Dictionary<string, string>();
                foreach (DataColumn col in list.DataTable.Columns)
                {
                    string caption = col.ColumnName.Replace("seriesField", "");
                    if (caption == "vField")
                    {
                        caption = "";
                    }

                    if (list.DataTable.Columns.IndexOf(col) >= list.crossYItems.Count)
                    {
                        col.Caption = "beforeType:System.Decimal";
                        caption = list.categories[list.DataTable.Columns.IndexOf(col) - list.crossYItems.Count].field;
                    }

                    if (caption.Length == 8 && caption.Contains("Date"))
                    {
                        string s = caption.Insert(4, "/");
                        s = s.Insert(7, "/");

                        try
                        {
                            DateTime d = Convert.ToDateTime(s);
                            caption = s;
                        }
                        catch
                        {
                        }
                    }
                    else if (caption.Length == 6 && caption.Contains("Month"))
                    {
                        string s = caption + "01";
                        s = s.Insert(4, "/");
                        s = s.Insert(7, "/");

                        try
                        {
                            DateTime d = Convert.ToDateTime(s);
                            caption = caption.Insert(4, "/");

                        }
                        catch
                        {
                        }
                    }
                    else if (caption.Length == 10)
                    {
                        string s = caption.Replace("_", "/");

                        try
                        {
                            DateTime d = Convert.ToDateTime(s);
                            caption = s;
                        }
                        catch
                        {
                        }
                    }

                    dataExportSetting.ExportField.Add(col.ColumnName, caption);
                }
            }

            String resourceDir = "/App_Resource";

            var physicalDir = Server.MapPath("~" + resourceDir);
            dataExportSetting.Dir = physicalDir;

            DataExportResult result = DataIOUtils.DataTableToCsv(dataExportSetting, list);

            if (result.ResultType == "Success")
            {
                DynamicsSaveRequest resourcesReq = new DynamicsSaveRequest();
                resourcesReq.ProjID = 1;
                resourcesReq.EntityName = "Y_Resources";
                resourcesReq.SaveData = new List<Dictionary<string, object>>();

                string fileName = "賞味期限切れ商品一覧";
                if (string.IsNullOrEmpty(dataExportSetting.FileName) == false)
                {
                    fileName = dataExportSetting.FileName;
                }

                String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
                fileName += "_" + date;

                Dictionary<string, object> resourcesDataItem = new Dictionary<string, object>();
                resourcesDataItem.Add("DyTableName", "Y_Resources");
                resourcesDataItem.Add("ResourcesID", result.ResourceId);
                resourcesDataItem.Add("ResourcesName", "賞味期限切れ商品一覧");
                resourcesDataItem.Add("Extension", result.Extension);
                resourcesDataItem.Add("Path", result.Path);
                resourcesDataItem.Add("UploadName", fileName + "." + result.Extension);
                resourcesDataItem.Add("Size", "0");
                resourcesDataItem.Add("UploadTime", DateTime.Now);
                resourcesReq.SaveData.Add(resourcesDataItem);

                logic.Save(resourcesReq);
            }

            adapter.Dispose();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult CheckPasedPlanDone(string shopCD, string shopName, string staffCD, string route, string hoDate)
        {
            HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName , ""));

            string str = hoOrderClientLogic.CheckPasedPlanDone(shopCD, staffCD, route, hoDate);
            if (str.Length == 0)
            {
                str = hoOrderClientLogic.CheckPasedPlanBefore(shopCD, staffCD, route, hoDate);
                hoOrderClientLogic.Dispose();
                if (str.Length == 0)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = str, type = "2" }, JsonRequestBehavior.AllowGet);
                }
            }
            else {
                hoOrderClientLogic.Dispose();
                return Json(new { msg=str,type="1" }, JsonRequestBehavior.AllowGet);
            }

            
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult CheckPasedPlanDoneRange(string shopCD, string hoDateFrom, string hoDateTo)
        {
            HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName , ""));
            string str = hoOrderClientLogic.CheckPasedPlanDoneRange(shopCD, hoDateFrom, hoDateTo);
            hoOrderClientLogic.Dispose();
            if (str.Length == 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { msg=str,type="1" }, JsonRequestBehavior.AllowGet);
            }

            
        }
        

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult GetClientList(string shopCD, string staffCD, string routeCD, string hoDate)
        {
            HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName , ""));
            List<DeliveryRouteClientViewModel> dataSaved = new List<DeliveryRouteClientViewModel>();
            List<DeliveryRouteClientViewModel> data = hoOrderClientLogic.GetClientList(shopCD, staffCD, routeCD, hoDate, out dataSaved);
            hoOrderClientLogic.Dispose();
            return Json(new { data = data, dataSaved = dataSaved }, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult DeliveryRouteRefer()
        {
            //List<object> routes = commonLogic.GetRouteList(null, CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd"),false);

            //ViewData["routeList"] = routes;
            
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult DeliveryRouteUndo()
        {
            return View();
        }
        

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult Save(T_HoDay hoDay, List<DeliveryRouteClientViewModel> addList, List<DeliveryRouteClientViewModel> delList, string HoDate, string Rote)
        {
            //locker
            //using (DrinkLocker locker = new DrinkLocker(hoDay.ShopCD))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");

                string DoneFlag = "0";

                if (HoDate != null)
                {
                    hoDay.HoDate = DateTime.ParseExact(HoDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }


                T_HoDayAdapter logic = new T_HoDayAdapter(enreq);
                HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                List<T_HoOrderClient> oldClents = hoOrderClientLogic.GetOldDayClient(hoDay.ShopCD, hoDay.HoDate);

                //add
                List<T_HoOrderClient> addClients = new List<T_HoOrderClient>();
                List<T_HoOrderTanto> addTantos = new List<T_HoOrderTanto>();
                if (addList != null)
                {
                    foreach (DeliveryRouteClientViewModel model in addList)
                    {
                        T_HoOrderClient client = new T_HoOrderClient();
                        client.ShopCD = model.ShopCD;
                        client.HoDate = hoDay.HoDate;
                        client.TantoCD = model.StaffCD;
                        client.FirstFlag = model.FirstFlag;
                        client.ClientCD = model.ClientCD;
                        client.DoneFlag = DoneFlag;
                        client.Route = Rote;
                        client.LastAfterStopFlag = model.AfterStopFlag;
                        addClients.Add(client);


                        T_HoOrderTanto tanto = new T_HoOrderTanto();
                        tanto.ShopCD = model.ShopCD;
                        tanto.HoDate = hoDay.HoDate;
                        tanto.TantoCD = model.StaffCD;
                        tanto.Route = Rote;

                        if (addTantos.Any(e => e.ShopCD == tanto.ShopCD && e.HoDate == tanto.HoDate && e.TantoCD == tanto.TantoCD && e.Route == tanto.Route) == false
                            && oldClents.Any(e => e.ShopCD == tanto.ShopCD && e.HoDate == tanto.HoDate && e.TantoCD == tanto.TantoCD && e.Route == tanto.Route) == false)
                        {
                            addTantos.Add(tanto);
                        }
                    }
                }

                oldClents.AddRange(addClients);

                //delete
                List<T_HoOrderClient> delClients = new List<T_HoOrderClient>();
                List<T_HoOrderTanto> delTantos = new List<T_HoOrderTanto>();
                if (delList != null)
                {
                    foreach (DeliveryRouteClientViewModel model in delList)
                    {
                        T_HoOrderClient client = new T_HoOrderClient();
                        client.ShopCD = model.ShopCD;
                        client.HoDate = hoDay.HoDate;
                        client.TantoCD = model.StaffCD;
                        client.FirstFlag = model.FirstFlag;
                        client.ClientCD = model.ClientCD;
                        client.Route = Rote;
                        delClients.Add(client);

                        T_HoOrderTanto tanto = new T_HoOrderTanto();
                        tanto.ShopCD = model.ShopCD;
                        tanto.HoDate = hoDay.HoDate;
                        tanto.TantoCD = model.StaffCD;
                        tanto.Route = Rote;
                        if (delTantos.Any(e => e.ShopCD == tanto.ShopCD && e.HoDate == tanto.HoDate && e.TantoCD == tanto.TantoCD && e.Route == tanto.Route) == false)
                        {
                            if (oldClents.Where(e => e.ShopCD == tanto.ShopCD && e.HoDate == tanto.HoDate && e.TantoCD == tanto.TantoCD && e.Route == tanto.Route).Count()
                                == delList.Where(e => e.ShopCD == tanto.ShopCD && e.StaffCD == tanto.TantoCD).Count())
                            {
                                delTantos.Add(tanto);
                            }

                        }

                    }
                }

                hoDay.Detail = addClients;
                hoDay.DetailTanto = addTantos;

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
                logcontent.AddContent("hoDay", hoDay);
                logcontent.AddContent("delClients", delClients);
                logcontent.AddContent("delTantos", delTantos);
                LogLogic logLogic = new LogLogic(enreq);
                hoOrderClientLogic.Dispose();
                try
                {
                    logic.SaveData(hoDay, delClients, delTantos);
                    logic.Dispose();
                    logLogic.Dispose();
                }
                catch (Exception e)
                {
                    logcontent.LogType = "error";
                    logcontent.AddContent("errorMsg", e.Message);
                    logcontent.AddContent("StackTrace", e.StackTrace);
                    logLogic.WriteLog(logcontent);
                    logic.Dispose();
                    logLogic.Dispose();
                    throw;
                }

                //logLogic.WriteLog(logcontent);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult Del(T_HoDay hoDay, string HoDate)
        {
            //locker
            using (DrinkLocker locker = new DrinkLocker(hoDay.ShopCD))
            {
                if (HoDate != null)
                {
                    hoDay.HoDate = DateTime.ParseExact(HoDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }

                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");

                T_HoDayAdapter logic = new T_HoDayAdapter(enreq);

                Result result = null;

                //log
                UserSession userInfo = Session["user"] as UserSession;
                LogContent logcontent = new LogContent();
                logcontent.FunctionPath = ViewBag.ControllerName;
                logcontent.FunctionName = ViewBag.ActionName;
                logcontent.FunctionType = "del";
                logcontent.LogType = "info";
                logcontent.Key1 = userInfo.ShopCD;
                logcontent.Key2 = userInfo.StaffCD;

                logcontent.AddContent("hoDay", hoDay);
                LogLogic logLogic = new LogLogic(enreq);

                try
                {
                    result = logic.Delete(hoDay.GetKey());
                    logic.Dispose();
                    logLogic.Dispose();
                }
                catch (Exception e)
                {
                    logcontent.LogType = "error";
                    logcontent.AddContent("errorMsg", e.Message);
                    logcontent.AddContent("StackTrace", e.StackTrace);
                    logLogic.WriteLog(logcontent);
                    logic.Dispose();
                    logLogic.Dispose();
                    throw;
                }

                //logLogic.WriteLog(logcontent);

                if (result.ReturnValue == EnumResult.OK)
                {
                    result.Message = "削除完了しました。";
                }
                else if (result.ReturnValue == EnumResult.Error)
                {
                    result.Message = "実績データが存在している為、削除できません。";
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult DelHoClient(T_HoClient hoClient, string hoClientUpdateTime)
        {
            //locker
            using (DrinkLocker locker = new DrinkLocker(hoClient.ShopCD))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");

                T_HoClientAdapter logic = new T_HoClientAdapter(enreq);

                Result result = null;

                //log
                UserSession userInfo = Session["user"] as UserSession;
                LogContent logcontent = new LogContent();
                logcontent.FunctionPath = ViewBag.ControllerName;
                logcontent.FunctionName = ViewBag.ActionName;
                logcontent.FunctionType = "save";
                logcontent.LogType = "del";
                logcontent.Key1 = userInfo.ShopCD;
                logcontent.Key2 = userInfo.StaffCD;

                logcontent.AddContent("hoClient", hoClient);
                LogLogic logLogic = new LogLogic(enreq);
                try
                {
                    result = logic.Delete(hoClient, hoClientUpdateTime);
                    logic.Dispose();
                    logLogic.Dispose();
                }
                catch (Exception e)
                {
                    logcontent.LogType = "error";
                    logcontent.AddContent("errorMsg", e.Message);
                    logcontent.AddContent("StackTrace", e.StackTrace);
                    logLogic.WriteLog(logcontent);
                    logic.Dispose();
                    logLogic.Dispose();
                    throw;
                }

                //logLogic.WriteLog(logcontent);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult GetRouteList(string ShopCD,string date)
        {
            List<object> data = commonLogic.GetRouteList(ShopCD, date,true);

            if (!string.IsNullOrEmpty(ShopCD) && !string.IsNullOrEmpty(date))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName , "");
                T_HoClientAdapter logic = new T_HoClientAdapter(enreq);
                string tempDate = date.Replace("/", "");
                date = tempDate.Substring(0, 4) + tempDate.Substring(4, 2) + tempDate.Substring(6, 2);
                string rote = "";
                List<object> planedRoteList = logic.GetShopPlanedRoute(ShopCD, date, ref rote);

                foreach (var item in planedRoteList)
                {
                    if (data.Any(e => DataUtil.GetObjectValue(e, "RouteCD").Equals(DataUtil.GetObjectValue(item, "RouteCD")))) continue;
                    data.Add(item);
                }
                logic.Dispose();
            }
            
           return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult GetShopUndoRoute(string ShopCD)
        {
            EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
            T_HoClientAdapter logic = new T_HoClientAdapter(enreq);
            List<object> data = logic.GetShopUndoRoute(ShopCD);

            logic.Dispose();
           return Json(data, JsonRequestBehavior.AllowGet);
        }
        

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult GetRouteClientList(string ShopCD, string Route)
        {
            EntityRequest enreq = new EntityRequest(1, loginUser.StaffName , "");

            T_HoClientAdapter logic = new T_HoClientAdapter(enreq);
            Dictionary<string, string> data = logic.GetRouteClientList(ShopCD, Route);
            logic.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRouteByDay(string ShopCD,string DayStr)
        {
            HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName , ""));
            string rote = hoOrderClientLogic.GetRoteByDate(ShopCD, DayStr);
            return Json(rote, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetShopPlanedRoute(string ShopCD, string DayStr)
        {
            EntityRequest enreq = new EntityRequest(1, loginUser.StaffName , "");

            T_HoClientAdapter logic = new T_HoClientAdapter(enreq);
            string rote = "";
            List<object> roteList = logic.GetShopPlanedRoute(ShopCD, DayStr, ref rote);
            logic.Dispose();

            object result = new
            {
                rote = rote,
                roteList = roteList
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeliveryCancel(List<T_HoOrderClient> hoClientList)
        {

            HoOrderClientLogic hoOrderClientLogic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName, ""));

            //delete
            List<T_HoOrderTanto> delTantos = new List<T_HoOrderTanto>();
            if (hoClientList != null)
            {
                foreach (T_HoOrderClient model in hoClientList)
                {
                    //T_HoOrderClient client = new T_HoOrderClient();
                    //client.ShopCD = model.ShopCD;
                    //client.HoDate = model.HoDate;
                    //client.TantoCD = model.TantoCD;
                    //client.FirstFlag = model.FirstFlag;
                    //client.ClientCD = model.ClientCD;
                    //client.Route = model.Route;
                    //delClients.Add(client);

                    List<T_HoOrderClient> oldClents = hoOrderClientLogic.GetOldDayClient(model.ShopCD, model.HoDate);

                    T_HoOrderTanto tanto = new T_HoOrderTanto();
                    tanto.ShopCD = model.ShopCD;
                    tanto.HoDate = model.HoDate;
                    tanto.TantoCD = model.TantoCD;
                    tanto.Route = model.Route;
                    if (delTantos.Any(e => e.ShopCD == tanto.ShopCD && e.HoDate == tanto.HoDate && e.TantoCD == tanto.TantoCD && e.Route == tanto.Route) == false)
                    {
                        if (oldClents.Where(e => e.ShopCD == tanto.ShopCD && e.HoDate == tanto.HoDate && e.TantoCD == tanto.TantoCD && e.Route == tanto.Route).Count()
                            == hoClientList.Where(e => e.ShopCD == tanto.ShopCD && e.HoDate == tanto.HoDate && e.TantoCD == tanto.TantoCD).Count())
                        {
                            delTantos.Add(tanto);
                        }

                    }

                }
            }
            EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");

            T_HoDayAdapter logic = new T_HoDayAdapter(enreq);
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
            logcontent.AddContent("delClients", hoClientList);
            logcontent.AddContent("delTantos", delTantos);
            LogLogic logLogic = new LogLogic(enreq);

            try
            {
                logic.CancelData(hoClientList, delTantos);
                logic.Dispose();
                logLogic.Dispose();
            }
            catch (Exception e)
            {
                logcontent.LogType = "error";
                logcontent.AddContent("errorMsg", e.Message);
                logcontent.AddContent("StackTrace", e.StackTrace);
                logLogic.WriteLog(logcontent);
                logic.Dispose();
                logLogic.Dispose();
                throw;
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}