using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DrinkService.Report.Model;
using DrinkService.Report.Logic;
using DrinkService.Report;
using DrinkService.Report.Report;
using DrinkService.Filters;
using DrinkService.Data.Models;
using DrinkService.Data.Logics;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using DrinkService.Data.ViewModels;
using DrinkService.Report.FD.Logic;
using DrinkService.Report.FD.Report;
using DrinkService.Utils;
using System.IO;
using SafeNeeds.DySmat;
using System.Transactions;
using DrinkService.Data.Common;

namespace DrinkService.Controllers
{
    public class ReportController : BaseController
    {

        [WebAuthorizeAttribute(Roles = "1,2,3,4,5,6")]
        public JsonResult DeliveryRoute(List<T_HoClient> hoClientList)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                DeliveryRouteLogic deliveryRouteLogic = new DeliveryRouteLogic(new EntityRequest(1, loginUser.StaffName, ""));

                //データの取得
                List<DeliveryRouteModel> models = deliveryRouteLogic.GetModes(hoClientList);

                if (models.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                //PDF出力
                DeliveryRouteReport report = new DeliveryRouteReport();

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;


                String fileName = string.Format("\\PDF_TEMP\\DeliveryRoute_{0}.pdf", date);

                report.CreatePDF(models, basePath + fileName);
                deliveryRouteLogic.Dispose();
            }
            return Json(new { Path = string.Format("/PDF_TEMP/DeliveryRoute_{0}.pdf", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult DeliveryRouteEmpty(string shopCD, string staffCD, string route, string hoDate)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                DeliveryRouteLogic deliveryRouteLogic = new DeliveryRouteLogic(new EntityRequest(1, loginUser.StaffName, ""));
                //データの取得
                List<DeliveryRouteModel> models = deliveryRouteLogic.GetEmptyModes(shopCD, staffCD, route, hoDate);

                if (models.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                //PDF出力
                DeliveryRouteReport report = new DeliveryRouteReport();

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;


                String fileName = string.Format("\\PDF_TEMP\\DeliveryRouteEmpty_{0}.pdf", date);

                report.CreatePDF(models, basePath + fileName);
                deliveryRouteLogic.Dispose();
            }
            return Json(new { Path = string.Format("/PDF_TEMP/DeliveryRouteEmpty_{0}.pdf", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);

        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult DeliveryRoutePayment(List<CollectionViewModel> hoClientList)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                DeliveryRoutePaymentLogic deliveryRoutePaymentLogic = new DeliveryRoutePaymentLogic(new EntityRequest(1, loginUser.StaffName, ""));
                //データの取得
                List<DeliveryRoutePaymentModel> models = deliveryRoutePaymentLogic.GetModes(hoClientList);

                if (models.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                //PDF出力
                DeliveryRoutePaymentReport report = new DeliveryRoutePaymentReport();

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

                String fileName = string.Format("\\PDF_TEMP\\DeliveryRoutePayment_{0}.pdf", date);

                report.CreatePDF(models, basePath + fileName);
                deliveryRoutePaymentLogic.Dispose();
            }
            return Json(new { Path = string.Format("/PDF_TEMP/DeliveryRoutePayment_{0}.pdf", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);

        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult DeliveryRouteStorage(string shopCD, string staffCD, string route, string hoDate)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                DeliveryRouteStorageLogic deliveryRouteStorageLogic = new DeliveryRouteStorageLogic(new EntityRequest(1, loginUser.StaffName, ""));
                //データの取得
                List<DeliveryRouteStorageModel> models = deliveryRouteStorageLogic.GetModes(shopCD, staffCD, route, hoDate);

                if (models.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                //PDF出力
                DeliveryRouteStorageReport report = new DeliveryRouteStorageReport();

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;


                String fileName = string.Format("\\PDF_TEMP\\DeliveryRouteStorage_{0}.pdf", date);

                report.CreatePDF(models, basePath + fileName);
                deliveryRouteStorageLogic.Dispose();
            }
            return Json(new { Path = string.Format("/PDF_TEMP/DeliveryRouteStorage_{0}.pdf", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);

        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult DeliveryRouteFillup(List<CollectionViewModel> hoClientList)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                DeliveryRouteFillupLogic deliveryRouteFillupLogic = new DeliveryRouteFillupLogic(new EntityRequest(1, loginUser.StaffName, ""));
                //データの取得
                List<DeliveryRouteFillupModel> models = deliveryRouteFillupLogic.GetModes(hoClientList);
                List<DeliveryRouteFillupNotYetModel> notYetModels = deliveryRouteFillupLogic.GetNoYetModels(hoClientList);

                if (models.Count == 0 && notYetModels.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                //PDF出力
                DeliveryRouteFillupReport report = new DeliveryRouteFillupReport();

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

                String fileName = string.Format("\\PDF_TEMP\\DeliveryRouteFillup_{0}.pdf", date);

                report.CreatePDF(models, notYetModels, basePath + fileName);
                deliveryRouteFillupLogic.Dispose();
            }
            return Json(new { Path = string.Format("/PDF_TEMP/DeliveryRouteFillup_{0}.pdf", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);

        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult DeliveryRouteFillupList(List<CollectionViewModel> hoClientList)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                //データの取得
                List<DeliveryRouteFillupListModel> models = DeliveryRouteFillupListLogic.GetModes(hoClientList);

                if (models.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                //PDF出力
                DeliveryRouteFillupListReport report = new DeliveryRouteFillupListReport();

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

                String fileName = string.Format("\\PDF_TEMP\\DeliveryRouteFillupList_{0}.pdf", date);

                report.CreatePDF(models, basePath + fileName);
            }
            return Json(new { Path = string.Format("/PDF_TEMP/DeliveryRouteFillupList_{0}.pdf", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);

        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult DeliveryRoutePlacement(string ShopCD)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                DeliveryRoutePlacementLogic deliveryRoutePlacementLogic = new DeliveryRoutePlacementLogic(new EntityRequest(1, loginUser.StaffName, ""));
                //データの取得
                List<DeliveryRoutePlacementModel> models = deliveryRoutePlacementLogic.GetModes(ShopCD);

                if (models.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                //PDF出力
                DeliveryRoutePlacementReport report = new DeliveryRoutePlacementReport();

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

                String fileName = string.Format("\\PDF_TEMP\\DeliveryRoutePlacement_{0}.pdf", date);

                report.CreatePDF(models, basePath + fileName);
                deliveryRoutePlacementLogic.Dispose();
            }
            return Json(new { Path = string.Format("/PDF_TEMP/DeliveryRoutePlacement_{0}.pdf", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FDReport(List<SaleDataViewModel> saleViewDatas, string ShopCD)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            using (DrinkLocker locker = new DrinkLocker(ShopCD))
            {
                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));

                M_Shop shop = shopLogic.GetShopByShopCD(ShopCD);

                Dictionary<string, int> clientSlipNos = new Dictionary<string, int>();

                int shopLastSlipNo = 0;

                //データの取得
                List<object> saleDatas = ShopWithSysLogic.GetDatas(saleViewDatas, shop.SysTypeCD, ref clientSlipNos, out shopLastSlipNo);

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

                String fileName = string.Format("\\FD\\FD_{0}_{1}.JDT", shop.ShopCD, date);

                if (shop.SysTypeCD == "1")
                {
                    FDReportShopWithSys report = new FDReportShopWithSys();

                    report.CreateFD(saleDatas, basePath + fileName);
                }
                else
                {
                    FDReportShopNoSys report = new FDReportShopNoSys();

                    report.CreateFD(saleDatas, basePath + fileName);
                }

                //save
                HoOrderClientLogic logic = new HoOrderClientLogic(new EntityRequest(1, loginUser.StaffName, ""));
                logic.SaveDownloadInfo(saleViewDatas, (ViewBag.LoginUser as UserSession).StaffName, clientSlipNos, shopLastSlipNo);

                if (saleDatas.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }
                shopLogic.Dispose();
                logic.Dispose();
            }
            //FD出力
            return Json(new { Path = string.Format("/FD/FD_{0}_{1}.JDT", ShopCD, date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult DownLoad(string fileName)
        {
            fileName = System.Web.HttpUtility.UrlDecode(fileName);
             string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

             string name = fileName.Split('/').Last();

            if (System.IO.File.Exists(basePath + fileName))
            {
                FileInfo fi = new FileInfo(basePath + fileName);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = false;
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + name);
                Response.AppendHeader("Content-Length", fi.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(basePath + fileName);
                Response.Flush();
                Response.End();
            }  

            return new EmptyResult();
        }
    }
}