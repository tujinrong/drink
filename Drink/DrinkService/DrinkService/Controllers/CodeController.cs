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

namespace DrinkService.Controllers
{
    public class CodeController : BaseController
    {

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult CodeList()
        {

            return View(); 
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult CodeKindList(string kind)
        {
            List<M_Code> codes = new List<M_Code>();
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                codes = CommonLogic.CodeKindList(kind);
            }
            return Json(codes, JsonRequestBehavior.AllowGet); 
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult CodeEdit(string kind,string cD)
        {
            CodeLogic codeLogic = new CodeLogic(new EntityRequest(1, loginUser.StaffName , ""));
            M_Code code = new M_Code();
            if (!string.IsNullOrEmpty(kind) && cD != null)
            {
                code = codeLogic.GetCodeByKind(kind,cD);
            }
            codeLogic.Dispose();
            return View(code);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult GetCodeList(string cD)
        {
            List<M_Code> codes = new List<M_Code>();
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                CodeLogic codeLogic = new CodeLogic(new EntityRequest(1, loginUser.StaffName, ""));
                codes = codeLogic.GetCodeByCD(cD);
                codeLogic.Dispose();
            }
            return Json(codes, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult GetRefCode(string Kind, string RefCD)
        {
            List<M_Code> codes = new List<M_Code>();
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                CodeLogic codeLogic = new CodeLogic(new EntityRequest(1, loginUser.StaffName, ""));
                codes = codeLogic.GetRefCode(Kind, RefCD);
                codeLogic.Dispose();
            }
            return Json(codes, JsonRequestBehavior.AllowGet);
        }



        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult CodeSearch(string kind, string pageNumber)
        {
            PagedResult<CodeListViewModel> data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                CodeLogic codeLogic = new CodeLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = codeLogic.GetPagedCodeList(kind, pageNumber);
                codeLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult CodeCsv(string kind)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                CodeLogic codeLogic = new CodeLogic(new EntityRequest(1, loginUser.StaffName, ""));
                List<CodeListViewModel> data = codeLogic.GetCodeList(kind);

                if (data.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;


                String fileName = string.Format("\\CSV\\Code_{0}.csv", date);

                CsvUtils.ModlesToCsv<CodeListViewModel>(basePath + fileName, data);
                codeLogic.Dispose();
            }
            return Json(new { Path = string.Format("/CSV/Code_{0}.csv", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);
        }



        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult Save([Bind(Include = "Kind,CD,Name,RefNo,RefCD")] M_Code _code,bool newMode)
        {
            Result result = null;
            using (TransactionScope scope = new TransactionScope())
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                M_CodeAdapter adapter = new M_CodeAdapter(enreq);

                result = adapter.Save(_code, newMode);
                adapter.Dispose(); 
                scope.Complete();
                CommonLogic.M_CodeKindList = null;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult Del([Bind(Include = "Kind,CD,Name,RefNo,RefCD")] M_Code _code)
        {
            Result result = null;
            using (TransactionScope scope = new TransactionScope())
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                M_CodeAdapter adapter = new M_CodeAdapter(enreq);
                result = adapter.Delete(_code);
                if (result.ReturnValue == EnumResult.OK)
                {
                    result.Message = "コード削除完了しました。";
                }
                else if (result.ReturnValue == EnumResult.Error)
                {
                    result.Message = "関連データが存在している為、削除できません。";
                }
                adapter.Dispose();
                scope.Complete();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}