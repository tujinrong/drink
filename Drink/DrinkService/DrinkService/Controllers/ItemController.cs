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
using SafeNeeds.DySmat.Logic;
using System.Data;
using SafeNeeds.DySmat.Util;
using DrinkService.Data.Common;
using System.Transactions;
namespace DrinkService.Controllers
{
    public class ItemController : BaseController
    {

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult ItemList()
        {
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
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

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult ItemEdit(string itemCD)
        {
            ItemLogic itemLogic = new ItemLogic(new EntityRequest(1, loginUser.StaffName , ""));
            M_Item item = new M_Item();
            if (!string.IsNullOrEmpty(itemCD))
            {
                item = itemLogic.GetItemByItemCD(itemCD);
              
            }

            DyEntityLogic logic = new DyEntityLogic();

            //研修資格取得
            DynamicsDsRequest request = new DynamicsDsRequest();
            request.ProjID = 1;
            request.DsRequests = new List<DsRequestModel>();

            DsRequestModel modelQualified = new DsRequestModel();
            modelQualified.TableName = "M_Qualified";
            request.DsRequests.Add(modelQualified);

            DataSet ds = logic.GetDyDs(request);

            List<string> QualifiedsMstList = new List<string>();

            foreach (DataRow dr in ds.Tables["M_Qualified"].Rows)
            {
                QualifiedsMstList.Add(DataUtil.CStr(dr["QualifiedCD"]) + "|" + DataUtil.CStr(dr["QualifiedName"]));
            }

            ViewBag.QualifiedsMstList = string.Join(",", QualifiedsMstList);


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
            itemLogic.Dispose();
            return View(item);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult ItemSearch(string itemCD, bool searchAll, string itemName, string MakerCD, string pageNumber)
        {
            PagedResult data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ItemLogic itemLogic = new ItemLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = itemLogic.GetPagedItemList(itemCD, itemName, searchAll, MakerCD, pageNumber);
                itemLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult ItemCsv(string itemCD, string itemName, string MakerCD, bool searchAll)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());

            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ItemLogic itemLogic = new ItemLogic(new EntityRequest(1, loginUser.StaffName, ""));
                List<ItemListViewModel> data = itemLogic.GetItemList(itemCD, itemName, MakerCD, searchAll);

                if (data.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

                String fileName = string.Format("\\CSV\\Item_{0}.csv", date);

                CsvUtils.ModlesToCsv<ItemListViewModel>(basePath + fileName, data);
                itemLogic.Dispose();
            }
            return Json(new { Path = string.Format("/CSV/Item_{0}.csv", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult ItemInSearch(string ShopCD, string pageNumber)
        {
            PagedResult<ItemInListViewModel> data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ItemInListLogic itemInListLogic = new ItemInListLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = itemInListLogic.GetItemInList(ShopCD, pageNumber);
                itemInListLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult ItemInList()
        {
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult Save(M_Item _item, string SaleStartDay, string SaleEndDay, string FreezingDay, bool newMode)
        {
            Result result = null;
            using (TransactionScope scope = new TransactionScope())
            {
                if (SaleStartDay != null)
                {
                    _item.SaleStartDay = DateTime.ParseExact(SaleStartDay, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                }

                if (SaleEndDay != null)
                {
                    _item.SaleEndDay = DateTime.ParseExact(SaleEndDay, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                }

                if (FreezingDay != null)
                {
                    _item.FreezingDay = DateTime.ParseExact(FreezingDay, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                }

                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                M_ItemAdapter adapter = new M_ItemAdapter(enreq);

                result = adapter.Save(_item, newMode);
                adapter.Dispose();
                scope.Complete();
            }
            return Json(result, JsonRequestBehavior.AllowGet); 
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult Del([Bind(Include = "ItemCD,ItemName,ShortName,StandardPrice,ShopPrice,InNum,SaleStartDay,SaleEndDay,FreezingDay")]  M_Item _item)
        {
             Result result = null;
             using (TransactionScope scope = new TransactionScope())
             {
                 EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                 M_ItemAdapter adapter = new M_ItemAdapter(enreq);
                 result = adapter.Delete(_item);
                 if (result.ReturnValue == EnumResult.OK)
                 {
                     result.Message = "商品削除完了しました。";
                 }
                 else if (result.ReturnValue == EnumResult.Error)
                 {
                     result.Message = "関連データが存在している為、削除できません。";
                 }
                 else if (result.ReturnValue == EnumResult.CheckDelete)
                 {
                     result.Message = "既に補充実績が存在している為に削除できません。";
                 }
                 adapter.Dispose();
                 scope.Complete();
             }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult StockList(string shopCD, string itemKey, bool getAll, int pageNumber = 1)
        {
            PageViewResult list = null;
            List<Dictionary<string, object>> dic = null;
            int PageRows = 0;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                M_ItemAdapter adapter = new M_ItemAdapter(enreq);

                DyEntityLogic logic = new DyEntityLogic();

                list = adapter.StockList(shopCD, itemKey, pageNumber, getAll, true);

                adapter.Dispose();

                PageRows = adapter._Proj.PageRows;
                dic = logic.DataTableToDic(list.DataTable);
            }
            return Json(
                    new
                    {
                        pageSize = PageRows
                    ,
                        totalSize = list.PageCount
                    ,
                        pageNumber = pageNumber
                    ,
                        pageData = dic
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

        public JsonResult StockEditList(string shopCD)
        {
            List<Dictionary<string, object>> dic = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                M_ItemAdapter adapter = new M_ItemAdapter(enreq);

                DyEntityLogic logic = new DyEntityLogic();

                PageViewResult list = adapter.StockList(shopCD, "", 1, false, false);

                adapter.Dispose();
                dic = logic.DataTableToDic(list.DataTable);
            }
            return Json(dic, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ExportStockList(string shopCD, string itemKey, bool getAll, int pageNumber = 1)
        {
            DataExportResult result = null;
            DyEntityLogic logic = new DyEntityLogic();

            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                M_ItemAdapter adapter = new M_ItemAdapter(enreq);


                PageViewResult list = adapter.StockList(shopCD, itemKey, pageNumber, false, false);


                DataExportSetting dataExportSetting = new DataExportSetting();
                dataExportSetting.ExportField = new Dictionary<string, string>();

                dataExportSetting.ExportField.Add("_rowNo", "№");
                dataExportSetting.ExportField.Add("ItemCD", "商品コード");
                dataExportSetting.ExportField.Add("ItemName", "商品名");
                dataExportSetting.ExportField.Add("AfterNum", "設置数");
                dataExportSetting.ExportField.Add("StockNum", "店内在庫数");
                dataExportSetting.ExportField.Add("SumNum", "合計数");

                String resourceDir = "/App_Resource";

                var physicalDir = Server.MapPath("~" + resourceDir);
                dataExportSetting.Dir = physicalDir;

                result = DataIOUtils.DataTableToCsv(dataExportSetting, list);
                adapter.Dispose();
            }
            if (result.ResultType == "Success")
            {
                DynamicsSaveRequest resourcesReq = new DynamicsSaveRequest();
                resourcesReq.ProjID = 1;
                resourcesReq.EntityName = "Y_Resources";
                resourcesReq.SaveData = new List<Dictionary<string, object>>();

                string fileName = "在庫一覧表_" + shopCD;
                

                //String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
                //fileName += "_" + date;

                Dictionary<string, object> resourcesDataItem = new Dictionary<string, object>();
                resourcesDataItem.Add("DyTableName", "Y_Resources");
                resourcesDataItem.Add("ResourcesID", result.ResourceId);
                resourcesDataItem.Add("ResourcesName", fileName);
                resourcesDataItem.Add("Extension", result.Extension);
                resourcesDataItem.Add("Path", result.Path);
                resourcesDataItem.Add("UploadName", fileName + "." + result.Extension);
                resourcesDataItem.Add("Size", "0");
                resourcesDataItem.Add("UploadTime", DateTime.Now);
                resourcesReq.SaveData.Add(resourcesDataItem);

                logic.Save(resourcesReq);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveItemStock(List<M_ItemStock> saveDatas, List<M_ItemStock> delDatas, string shopCD)
        {
            Result result = new Result();
            //locker
            using (DrinkLocker locker = new DrinkLocker(shopCD))
            {
                EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                M_ItemAdapter adapter = new M_ItemAdapter(enreq);

                DyEntityLogic logic = new DyEntityLogic();

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        result = adapter.SaveItemStock(saveDatas, delDatas);

                        scope.Complete();
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        adapter.Dispose();
                        scope.Dispose();
                    }

                }
            }

        }

    }
}