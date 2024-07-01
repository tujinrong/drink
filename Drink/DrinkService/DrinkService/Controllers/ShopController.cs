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
using DrinkService.Data;
using System.Transactions;
using DrinkService.Data.Common;

namespace DrinkService.Controllers
{
    public class ShopController : BaseController
    {

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult ShopList()
        {
            return View();
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public ActionResult ShopEdit(string shopCD)
        {
            M_Shop shop = new M_Shop();
            //locker
            using (DrinkLocker locker = new DrinkLocker(shopCD))
            {
                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));


                DynamicsDsRequest request = new DynamicsDsRequest();
                request.ProjID = 1;
                request.DsRequests = new List<DsRequestModel>();

                List<string> QualifiedsMstList = new List<string>();

                DsRequestModel modelQualified = new DsRequestModel();
                modelQualified.TableName = "M_Qualified";
                request.DsRequests.Add(modelQualified);


                DyEntityLogic logic = new DyEntityLogic();
                DataSet ds = logic.GetDyDs(request);

                foreach (DataRow dr in ds.Tables["M_Qualified"].Rows)
                {
                    QualifiedsMstList.Add(DataUtil.CStr(dr["QualifiedCD"]) + "|" + DataUtil.CStr(dr["QualifiedName"]));
                }


                ViewBag.QualifiedsMstList = string.Join(",", QualifiedsMstList);

                if (!string.IsNullOrEmpty(shopCD))
                {
                    shop = shopLogic.GetShopByShopCD(shopCD);

                    DynamicsDsRequest request2 = new DynamicsDsRequest();

                    request2.ProjID = 1;
                    request2.DsRequests = new List<DsRequestModel>();
                    DsRequestModel model = new DsRequestModel();
                    model.TableName = "M_QualifiedShop";
                    model.Filter = "ShopCD = '" + shopCD + "'";
                    request2.DsRequests.Add(model);

                    DataSet ds2 = logic.GetDyDs(request2);


                    List<string> QualifiedsList = new List<string>();

                    foreach (DataRow dr in ds2.Tables["M_QualifiedShop"].Rows)
                    {
                        QualifiedsList.Add(DataUtil.CStr(dr["QualifiedCD"]));
                    }



                    ViewBag.QualifiedsList = string.Join(",", QualifiedsList);

                    List<string> QualifiedsHadDataList = new List<string>();
                    foreach (string key in QualifiedsList)
                    {
                        string sql = @"select count(*) from T_HoClientItem left join M_Item on T_HoClientItem.ItemCD = M_Item.ItemCD left join M_Client T4 on T_HoClientItem.ShopCD = T4.ShopCD and T_HoClientItem.ClientCD = T4.ClientCD where T_HoClientItem.ShopCD ='{0}' and M_Item.QualifiedCD = '{1}' and T_HoClientItem.Seq = T4.LastSeq ";
                        DataTable dt = SQLHelper.GetDataTable(string.Format(sql, shopCD, key));
                        if (DataUtil.CDec(dt.Rows[0][0]) > 0)
                        {
                            QualifiedsHadDataList.Add(key);
                        }
                        else
                        {
                            sql = @"select count(*) from M_ItemKitDetail left join M_Item on M_ItemKitDetail.ItemCD = M_Item.ItemCD where M_ItemKitDetail.ShopCD ='{0}' and M_Item.QualifiedCD = '{1}'";
                            dt = SQLHelper.GetDataTable(string.Format(sql, shopCD, key));
                            if (DataUtil.CDec(dt.Rows[0][0]) > 0)
                            {
                                QualifiedsHadDataList.Add(key);
                            }
                        }
                    }

                    ViewBag.QualifiedsHadDataList = string.Join(",", QualifiedsHadDataList);

                }
                shopLogic.Dispose();
            }
            return View(shop);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult ShopSearch(string shopCD , string shopType, string regionCD, string shopName, string pageNumber)
        {
            PagedResult data = null;
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                data = shopLogic.GetPagedShopList(shopCD, shopType, regionCD, shopName, pageNumber);
                shopLogic.Dispose();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,6")]
        public JsonResult ShopCsv(string shopType, string regionCD, string shopName)
        {
            String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                ShopLogic shopLogic = new ShopLogic(new EntityRequest(1, loginUser.StaffName, ""));
                List<ShopListViewModel> data = shopLogic.GetShopList(shopType, regionCD, shopName);

                if (data.Count == 0)
                {
                    return Json(new { ResultType = "NoData" });
                }

                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

                String fileName = string.Format("\\CSV\\Shop_{0}.csv", date);

                CsvUtils.ModlesToCsv<ShopListViewModel>(basePath + fileName, data);
                shopLogic.Dispose();
            }
            return Json(new { Path = string.Format("/CSV/Shop_{0}.csv", date), ResultType = "Success" }, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorizeAttribute(Roles = "1,2,3,4,6")]
        public JsonResult Save(M_Shop _shop, string SystemStartDate, bool newMode, List<string> QualifiedsList)
        {
            Result result = new Result();
            using (DrinkLocker locker = new DrinkLocker(_shop.ShopCD))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (SystemStartDate != null)
                    {
                        _shop.SystemStartDate = DateTime.ParseExact(SystemStartDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    }


                    //check
                    string sql = @"select M_Qualified.* from M_QualifiedShop left join M_Qualified on M_QualifiedShop.QualifiedCD = M_Qualified.QualifiedCD where M_QualifiedShop.ShopCD ='{0}'";
                    DataTable dt = SQLHelper.GetDataTable(string.Format(sql, _shop.ShopCD));

                    List<string[]> QualifiedsListOld = new List<string[]>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        QualifiedsListOld.Add(new string[] { DataUtil.CStr(dr["QualifiedCD"]), DataUtil.CStr(dr["QualifiedName"]) });
                    }

                    List<string> errorList = new List<string>();
                    foreach (string[] key in QualifiedsListOld)
                    {
                        if (QualifiedsList == null || QualifiedsList.Contains(key[0]) == false)
                        {

                            sql = @"select count(*) from T_HoClientItem left join M_Item on T_HoClientItem.ItemCD = M_Item.ItemCD left join M_Client T4 on T_HoClientItem.ShopCD = T4.ShopCD and T_HoClientItem.ClientCD = T4.ClientCD where T_HoClientItem.ShopCD ='{0}' and M_Item.QualifiedCD = '{1}' and T_HoClientItem.Seq = T4.LastSeq ";
                            dt = SQLHelper.GetDataTable(string.Format(sql, _shop.ShopCD, key[0]));
                            if (DataUtil.CDec(dt.Rows[0][0]) > 0)
                            {
                                errorList.Add("【" + key[1] + "】");
                            }
                            else
                            {
                                sql = @"select count(*) from M_ItemKitDetail left join M_Item on M_ItemKitDetail.ItemCD = M_Item.ItemCD where M_ItemKitDetail.ShopCD ='{0}' and M_Item.QualifiedCD = '{1}'";
                                dt = SQLHelper.GetDataTable(string.Format(sql, _shop.ShopCD, key[0]));
                                if (DataUtil.CDec(dt.Rows[0][0]) > 0)
                                {
                                    errorList.Add("【" + key[1] + "】");
                                }
                            }
                        }
                    }
                    if (errorList.Count > 0)
                    {
                        Result resultCheck = new Result();
                        resultCheck.ReturnValue = EnumResult.Error;
                        resultCheck.Message = "研修資格" + string.Join(",", errorList) + "関連データが存在している為、削除できません。";
                        return Json(resultCheck, JsonRequestBehavior.AllowGet);
                    }


                    EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                    M_ShopAdapter adapter = new M_ShopAdapter(enreq);

                    result = adapter.Save(_shop, newMode);


                    DyEntityLogic logic = new DyEntityLogic();
                    DynamicsSaveRequest request = new DynamicsSaveRequest();
                    request.EntityName = "M_QualifiedShop";
                    request.ProjID = 1;

                    request.SaveData = new List<Dictionary<string, object>>();

                    Dictionary<string, object> del = new Dictionary<string, object>();
                    del.Add("DyDelTableName", "M_QualifiedShop");
                    del.Add("ShopCD", _shop.ShopCD);
                    request.SaveData.Add(del);

                    if (QualifiedsList != null)
                    {
                        foreach (string Qualified in QualifiedsList)
                        {
                            Dictionary<string, object> model = new Dictionary<string, object>();
                            model.Add("DyTableName", "M_QualifiedShop");
                            model.Add("ShopCD", _shop.ShopCD);
                            model.Add("QualifiedCD", Qualified);
                            request.SaveData.Add(model);
                        }
                    }
                    logic.Save(request);
                    adapter.Dispose();
                    scope.Complete();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet); 
        }

        [WebAuthorizeAttribute(Roles = "1,2,6")]
        public JsonResult Del([Bind(Include = "ShopCD")] M_Shop _shop)
        {
             Result result = new Result();
             using (DrinkLocker locker = new DrinkLocker(_shop.ShopCD))
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     EntityRequest enreq = new EntityRequest(1, loginUser.StaffName, "");
                     M_ShopAdapter adapter = new M_ShopAdapter(enreq);
                     result = adapter.Delete(_shop);
                     adapter.Dispose();
                     if (result.ReturnValue == EnumResult.OK)
                     {
                         result.Message = "店舗削除完了しました。";
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