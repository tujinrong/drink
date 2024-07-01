using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DrinkService.Data.Logics;
using DrinkService.Data.Models;
using DrinkService.Data.ViewModels;
using DrinkService.Filters;
using SafeNeeds.DySmat;
using System.Transactions;

namespace DrinkService.Controllers
{
    public class CollectionController : BaseController
    {

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult CollectionList()
        {
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult CollectionSearch(string shopCD, string StartDate, string EndDate, string staffCD, string pageNumber)
        {
            PagedResult data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                CollectionLogic collectionLogic = new CollectionLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = collectionLogic.GetCollectionList(shopCD, StartDate, EndDate, staffCD, pageNumber);
                collectionLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}