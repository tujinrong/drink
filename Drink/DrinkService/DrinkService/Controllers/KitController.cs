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
using DrinkService.Data.Common;
using System.Transactions;

namespace DrinkService.Controllers
{
    public class KitController : BaseController
    {
        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult KitList()
        {
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult KitEdit(string ShopCD, int? kitID)
        {
            M_ItemKit itemKit = new M_ItemKit();
            //locker
            using (DrinkLocker locker = new DrinkLocker(ShopCD))
            {
                ItemKitLogic itemKitLogic = new ItemKitLogic(new EntityRequest(1, loginUser.StaffName, ""));
                if (!string.IsNullOrEmpty(ShopCD) && kitID != null)
                {
                    itemKit = itemKitLogic.GetItemKitByShopCD(ShopCD, kitID);
                }

                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                M_Shop shop = shopLogic.GetShopByShopCD(ShopCD);
                ViewBag.ShopName = shop.ShopName;
                ViewBag.ShopCD = ShopCD;
                itemKitLogic.Dispose();
                shopLogic.Dispose();
            }
            return View(itemKit);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult ItemKitSearch(string shopCD,int? kitID,string kitName,string pageNumber)
        {
            PagedResult data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ItemKitLogic itemKitLogic = new ItemKitLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = itemKitLogic.GetPagedItemKitList(shopCD, kitID, kitName, pageNumber);
                itemKitLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult KitCsv(string shopCD, int? kitID, string kitName)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ItemKitLogic itemKitLogic = new ItemKitLogic(new EntityRequest(1, loginUser.StaffName, ""));
                List<ItemKitListViewModel> data = itemKitLogic.GetItemKitList(shopCD, kitID, kitName);

                if (data.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;


                String fileName = string.Format("\\CSV\\Kit_{0}.csv", date);

                CsvUtils.ModlesToCsv<ItemKitListViewModel>(basePath + fileName, data);
                itemKitLogic.Dispose();
            }
            return Json(new { Path = string.Format("/CSV/Kit_{0}.csv", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);
        }


        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult GetItemKitName(string shopCD)
        {
            List<M_ItemKit> list = new List<M_ItemKit>();
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            { 
                ItemKitLogic itemKitLogic = new ItemKitLogic(new EntityRequest(1, loginUser.StaffName , ""));
                list = itemKitLogic.GetItemKitName(shopCD);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult KitItemList(string ShopCD, int? kitID)
        {
            List<KitDetailViewModel> itemKits = new List<KitDetailViewModel>();
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ItemKitLogic itemKitLogic = new ItemKitLogic(new EntityRequest(1, loginUser.StaffName, ""));
                itemKits = itemKitLogic.KitItemList(ShopCD, kitID);
                itemKitLogic.Dispose();
            }
            return Json(itemKits, JsonRequestBehavior.AllowGet); ;
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult Save(M_ItemKit _itemKit, List<M_ItemKitDetail> detailList)
        {
            object result = null;
            using (DrinkLocker locker = new DrinkLocker(_itemKit.ShopCD))
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     ItemKitLogic itemKitLogic = new ItemKitLogic(new EntityRequest(1, loginUser.StaffName, ""));
                     result = itemKitLogic.Save(_itemKit, detailList, loginUser.StaffName);
                     itemKitLogic.Dispose();
                     scope.Complete();
                 }
             }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult Del([Bind(Include = "ShopCD,KitID,KitName")] M_ItemKit _itemKit)
        {
            using (DrinkLocker locker = new DrinkLocker(_itemKit.ShopCD))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ItemKitLogic itemKitLogic = new ItemKitLogic(new EntityRequest(1, loginUser.StaffName, ""));
                    itemKitLogic.Del(_itemKit);
                    itemKitLogic.Dispose();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet); ;
        }
        
    }
}