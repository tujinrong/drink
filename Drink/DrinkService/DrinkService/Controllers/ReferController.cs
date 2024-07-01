using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DrinkService.Data.Logics;
using DrinkService.Data.Models;
using DrinkService.Filters;
using SafeNeeds.DySmat;
using System.Data;
using SafeNeeds.DySmat.Logic;
using SafeNeeds.DySmat.Util;
using System.Transactions;

namespace DrinkService.Controllers
{
    public class ReferController : BaseController
    {

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult ReferClient(string ShopCD)
        {
            ViewBag.ShopCD = ShopCD;

            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult ReferNoPlanClient(string shopCD, string staffCD, string routeCD, string hoDate)
        {
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ViewBag.shopCD = shopCD;
                ViewBag.staffCD = staffCD;
                ViewBag.routeCD = routeCD;
                ViewBag.hoDate = hoDate;

                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                M_Shop shop = shopLogic.GetShopByShopCD(shopCD);
                ViewBag.shopName = shop.ShopName;

                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");

                T_HoClientAdapter logic = new T_HoClientAdapter(enreq);
                List<object> routes = logic.GetShopRoute(shopCD, hoDate, true);

                ViewData["routeList"] = routes;
                logic.Dispose();
                shopLogic.Dispose();
            }
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public ActionResult ReferItem(List<string> IgnoreItems,string ShopCD)
        {
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                if (IgnoreItems != null)
                {
                    ViewBag.IgnoreItems = string.Join(",", IgnoreItems);
                }
                else
                {
                    ViewBag.IgnoreItems = "";
                }

                ViewBag.ShopCD = ShopCD;

                DyEntityLogic logic = new DyEntityLogic();
                //メーカー取得
                DynamicsDsRequest request2 = new DynamicsDsRequest();
                request2.ProjID = 1;
                request2.DsRequests = new List<DsRequestModel>();

                DsRequestModel modelQualified2 = new DsRequestModel();
                modelQualified2.TableName = "M_Maker";
                modelQualified2.OrderBy = "MakerNameKana,MakerName";
                request2.DsRequests.Add(modelQualified2);

                DataSet ds2 = logic.GetDyDs(request2);

                List<string> MakerMstList = new List<string>();

                foreach (DataRow dr in ds2.Tables["M_Maker"].Rows)
                {
                    MakerMstList.Add(DataUtil.CStr(dr["MakerCD"]) + "|" + DataUtil.CStr(dr["MakerName"]));
                }

                ViewBag.MakerMstList = string.Join(",", MakerMstList);
            }
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,6")]
        public ActionResult ReferShop(string shopTypeCD)
        {
            ViewBag.AllowShopType = shopTypeCD;
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,6")]
        public JsonResult ReferShopSearch(string shopTypeCD, string shopKey, string pageNumber)
        {
            PagedResult data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = shopLogic.GetShopPagedListByKeyAndPageNumber(shopTypeCD, shopKey, pageNumber);
                shopLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,6")]
        public JsonResult ShopFindAll(string shopTypeCD)
        {
            List<M_Shop> shopsList = new List<M_Shop>();
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                shopsList = shopLogic.GetAllShops(shopTypeCD);
                shopLogic.Dispose();
            }
            return Json(shopsList, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult ShopFindOne(string key)
        {
            M_Shop shop = null; 
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                shop = shopLogic.GetShopByShopCD(key);
            }
            return Json(shop, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShopAutoComplete(string shopTypeCD, string key)
        {
            object result = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                result = shopLogic.GetShopAutoComplete(shopTypeCD, key);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult ReferItemSearch(string itemKey, string IgnoreItems, string ShopCD, string MakerCD, string pageNumber)
        {
            PagedResult data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ItemLogic itemLogic = new ItemLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = itemLogic.GetItemPagedListByKeyAndPageNumber(itemKey, IgnoreItems, ShopCD, MakerCD, pageNumber);
                itemLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult ItemFindAll(string IgnoreItems)
        {
            List<M_Item> itemList = new List<M_Item>();
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ItemLogic itemLogic = new ItemLogic(new EntityRequest(1, loginUser.StaffName, ""));
                itemList = itemLogic.GetAllItems(IgnoreItems);
                itemLogic.Dispose();
            }
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult ItemFindOne(string key)
        {
            M_Item item = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ItemLogic itemLogic = new ItemLogic(new EntityRequest(1, loginUser.StaffName, ""));
                item = itemLogic.GetItemByItemCD(key);
            }

            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult ItemAutoComplete(string key, string IgnoreItems)
        {
            ItemLogic itemLogic = new ItemLogic(new EntityRequest(1, loginUser.StaffName , ""));
            PagedResult data = itemLogic.GetAutoComplete(key, IgnoreItems);
            itemLogic.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult ReferClientSearch(string clientKey, string ShopCD, string pageNumber)
        {
            PagedResult data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ClientLogic clientLogic = new ClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = clientLogic.GetClientPagedListByKeyAndPageNumber(clientKey, ShopCD, pageNumber);
                clientLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult ClientFindAll(string ShopCD)
        {
            List<M_Client> clientsList = new List<M_Client>();
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ClientLogic clientLogic = new ClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                clientsList = clientLogic.GetAllClients(ShopCD);
                clientLogic.Dispose();
            }
            return Json(clientsList, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult ClientFindOne(string key, string ShopCD)
        {
            M_Client client = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ClientLogic clientLogic = new ClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                client = clientLogic.GetClientByKey(ShopCD, key);
            }
            return Json(client, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult ClientAutoComplete(string key, string ShopCD)
        {
            PagedResult data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ClientLogic clientLogic = new ClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = clientLogic.GetAutoComplete(key, ShopCD);
                clientLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}