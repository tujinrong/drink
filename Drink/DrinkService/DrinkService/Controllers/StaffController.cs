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
    public class StaffController : BaseController
    {

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult StaffList()
        {
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult StaffEdit(string shopCD, string staffCD, string shopTypeCD)
        {
            M_Staff staff = new M_Staff();
            using (DrinkLocker locker = new DrinkLocker(shopCD))
            {
                StaffLogic staffLogic = new StaffLogic(new EntityRequest(1, loginUser.StaffName, ""));
                ViewBag.shopTypeCD = shopTypeCD;

                
                if (!string.IsNullOrEmpty(staffCD))
                {
                    staff = staffLogic.GetStaffByKey(shopCD, staffCD);
                }
                staffLogic.Dispose();
            }
            return View(staff);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult GetStaffList(string shopCD)
        {
            List<M_Staff> staffs = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                StaffLogic staffLogic = new StaffLogic(new EntityRequest(1, loginUser.StaffName, ""));
                staffs = staffLogic.GetStaffByShopCD(shopCD);
                staffLogic.Dispose();
            }
            return Json(staffs, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult StaffSearch(string shopType,string shopCD, string staffName, string pageNumber)
        {
            PagedResult data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                StaffLogic staffLogic = new StaffLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = staffLogic.GetPagedStaffList(shopType, shopCD, staffName, pageNumber);
                staffLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult StaffCsv(string shopType, string shopCD, string staffName)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                StaffLogic staffLogic = new StaffLogic(new EntityRequest(1, loginUser.StaffName, ""));
                List<StaffListViewModel> data = staffLogic.GetStaffList(shopType, shopCD, staffName);

                if (data.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;


                String fileName = string.Format("\\CSV\\Staff_{0}.csv", date);

                CsvUtils.ModlesToCsv<StaffListViewModel>(basePath + fileName, data);
                staffLogic.Dispose();
            }
            return Json(new { Path = string.Format("/CSV/Staff_{0}.csv", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);
        }


        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult Save([Bind(Include = "ShopTypeCD,StaffCD,StaffName,ShopCD,Password,RoleCD,SosikinCD")] M_Staff _staff, bool newMode)
        {
             Result result = new Result();
             using (DrinkLocker locker = new DrinkLocker(_staff.ShopCD))
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                     M_StaffAdapter adapter = new M_StaffAdapter(enreq);

                     result = adapter.Save(_staff, newMode);
                     adapter.Dispose();
                     scope.Complete();
                 }
             }
            return Json(result, JsonRequestBehavior.AllowGet); 
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult Del([Bind(Include = "ShopTypeCD,StaffCD,StaffName,ShopCD,Password,RoleCD,SosikinCD")] M_Staff _staff)
        {
            Result result = new Result();
            using (DrinkLocker locker = new DrinkLocker(_staff.ShopCD))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //staffLogic.Del(_staff);
                    EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                    M_StaffAdapter adapter = new M_StaffAdapter(enreq);
                    result = adapter.Delete(_staff);
                    adapter.Dispose();
                    if (result.ReturnValue == EnumResult.OK)
                    {
                        result.Message = "担当者削除完了しました。";
                    }
                    else if (result.ReturnValue == EnumResult.Error)
                    {
                        result.Message = "関連データが存在している為、削除できません。";
                    }
                    scope.Complete();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}