using WebEvaluation.Models;
using WebEvaluation.Utils;
using WebEvaluation.DataModels;


using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Logic;
using SafeNeeds.DySmat.Model;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEvaluation.Controllers
{
    public class DynamicsController : BaseController
    {
        public ActionResult FormEdit(int ProjID, string EntityName, string FormName, string type)
        {
            ViewBag.ProjID = ProjID;
            ViewBag.FormName = FormName;
            ViewBag.EntityName = EntityName;
            ViewBag.type = type;

            return View();
        }

        public ActionResult FormPage(int ProjID, string EntityName, string FormName, string type)
        {
            ViewBag.ProjID = ProjID;
            ViewBag.FormName = FormName;
            ViewBag.EntityName = EntityName;
            ViewBag.type = type;

            ViewBag.id = "_" + ProjID + "_" + EntityName + "_" + FormName;

            return View();
        }

        public ActionResult FormList(string type)
        {
            ViewBag.type = type;

            return View();
        }

        public ActionResult FormSetting(string type)
        {
            ViewBag.type = type;

            return View();
        }

        public ActionResult ProjectManager(int ProjID)
        {
            ViewBag.ProjID = ProjID;

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Loading()
        {
            return View();
        }

        public ActionResult PreLoading()
        {
            return View();
        }


        public ActionResult SubFormTest()
        {
            return View();
        }

        //public ActionResult Home(int ProjID, string roleName)
        //{
        //    DyEntityLogic logic = new DyEntityLogic();
        //    List<Y_MenuGroup> groups = logic.GetMenuGroupList(ProjID);
        //    //List<Y_Menu> menus = logic.GetRoleMenuList(ProjID, roleName);
        //    List<Y_Menu> menus = logic.GetMenuList(ProjID);


        //    List<Y_MenuGroup> menuGroupList = new List<Y_MenuGroup>();
        //    foreach (Y_MenuGroup group in groups)
        //    {
        //        Y_MenuGroup menuGroup = new Y_MenuGroup()
        //        {
        //            ProjID = group.ProjID,
        //            GroupName = group.GroupName,
        //            GroupDesc = group.GroupDesc,
        //            GroupIcon = group.GroupIcon,
        //            Seq = group.Seq,
        //            Menus = new List<Y_Menu>()
        //        };
        //        foreach (Y_Menu menu in group.Menus)
        //        {
        //            if (menus.Contains(menu))
        //            {
        //                menuGroup.Menus.Add(menu);
        //            }
        //        }

        //        if (menuGroup.Menus.Count > 0)
        //        {
        //            menuGroupList.Add(menuGroup);
        //        }
        //    }

        //    ViewBag.ProjID = ProjID;
        //    ViewData.Add("menuGroupList", menuGroupList);
        //    return View();
        //}

        public ActionResult Home()
        {
            //DyEntityLogic logic = new DyEntityLogic();
            //List<Y_MenuGroup> groups = logic.GetMenuGroupList(1);
            ////List<Y_Menu> menus = logic.GetRoleMenuList(ProjID, roleName);
            //List<Y_Menu> menus = logic.GetMenuList(1);


            //List<Y_MenuGroup> menuGroupList = new List<Y_MenuGroup>();
            //foreach (Y_MenuGroup group in groups)
            //{
            //    Y_MenuGroup menuGroup = new Y_MenuGroup()
            //    {
            //        ProjID = group.ProjID,
            //        GroupName = group.GroupName,
            //        GroupDesc = group.GroupDesc,
            //        GroupIcon = group.GroupIcon,
            //        Seq = group.Seq,
            //        Menus = new List<Y_Menu>()
            //    };
            //    foreach (Y_Menu menu in group.Menus)
            //    {
            //        if (menus.Contains(menu))
            //        {
            //            menuGroup.Menus.Add(menu);
            //        }
            //    }

            //    if (menuGroup.Menus.Count > 0)
            //    {
            //        menuGroupList.Add(menuGroup);
            //    }
            //}

            ViewBag.ProjID = 1;
            //ViewData.Add("menuGroupList", menuGroupList);

            DyEntityLogic logic = new DyEntityLogic();

            Y_Proj proj = logic.GetProjData(1);

            ViewBag.titleStr = proj.ProjDesc;

            return View();
        }

        public JsonResult GetMenus(int ProjID, string roleName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            List<Y_MenuGroup> groups = logic.GetMenuGroupList(ProjID);
            List<Y_Menu> menus = logic.GetMenuList(ProjID);


            List<Y_MenuGroup> menuGroupList = new List<Y_MenuGroup>();
            foreach (Y_MenuGroup group in groups)
            {
                Y_MenuGroup menuGroup = new Y_MenuGroup()
                {
                    ProjID = group.ProjID,
                    GroupName = group.GroupName,
                    GroupDesc = group.GroupDesc,
                    GroupIcon = group.GroupIcon,
                    Seq = group.Seq,
                    Menus = new List<Y_Menu>()
                };
                foreach (Y_Menu menu in group.Menus)
                {
                    if (menus.Contains(menu))
                    {
                        menuGroup.Menus.Add(menu);
                    }
                }

                if (menuGroup.Menus.Count > 0)
                {
                    menuGroupList.Add(menuGroup);
                }
            }

            return Json(menuGroupList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFormListByEntityName(int ProjID, string entityName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.DataTableToDic(logic.GetFormListByEntityName(ProjID, entityName)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFormList(int ProjID, string type)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.DataTableToDic(logic.GetFormList(ProjID, type)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEntityDataItem(int ProjID, string EntityName, Dictionary<string, object> dataItem)
        {
            DyEntityLogic logic = new DyEntityLogic();
            //Dictionary<string, object> di = logic.GetEntityDataItem(ProjID, EntityName, dataItem);
            return Json(logic.GetEntityDataItem(ProjID, EntityName, dataItem), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReInitGlobal(int ProjID)
        {
            DyEntityLogic logic = new DyEntityLogic();
            logic.ReInitGlobal(ProjID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDyDs(DynamicsDsRequest request)
        {
            DyEntityLogic logic = new DyEntityLogic();
            UserSession user = Session["user"] as UserSession;
            //request.UpdateUser = user.StaffCD;

            DataSet ds = logic.GetDyDs(request);

            return Json(new { ds = logic.DataSetToDic(ds) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(DynamicsSaveRequest request)
        {
            DyEntityLogic logic = new DyEntityLogic();
            UserSession user = Session["user"] as UserSession;

            if (user != null)
            {
                request.UpdateUser = user.StaffName;
            }

            //Save log
            LogContent logcontent = new LogContent();

            logcontent.FunctionPath = request.FromEntityName;
            logcontent.FunctionName = request.FromFormName;
            logcontent.FunctionType = "save";
            logcontent.LogType = "info";
            logcontent.ProjID = request.ProjID;
            //logcontent.LoginCD = user == null ? "" : user.UserName;
            logcontent.UpdateUser = user == null ? "" : user.StaffName;
            logcontent.Code1 = request.EntityName;

            logcontent.AddContent("request", request);

            Result result = logic.Save(request);
            if (result.ErrorKey != null)
            {
                logcontent.LogType = "error";
                logcontent.AddContent("Message", result.Message);
            }
            //DyLogLogic.WriteLog(logcontent);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Del(DynamicsDeleteRequest request)
        {
            DyEntityLogic logic = new DyEntityLogic();

            UserSession user = Session["user"] as UserSession;

            //Del log
            LogContent logcontent = new LogContent();

            logcontent.FunctionPath = request.FromEntityName;
            logcontent.FunctionName = request.FromFormName;
            logcontent.FunctionType = "delete";
            logcontent.LogType = "info";
            logcontent.ProjID = request.ProjID;
            //logcontent.LoginCD = user == null ? "" : user.UserName;
            logcontent.UpdateUser = user == null ? "" : user.StaffCD;
            logcontent.Code1 = request.EntityName;

            logcontent.AddContent("request", request);

            Result result = logic.Del(request);
            if (result.ErrorKey != null)
            {
                logcontent.LogType = "error";
                logcontent.AddContent("Message", result.Message);
            }
            //DyLogLogic.WriteLog(logcontent);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEntity(int ProjID, string EntityName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.GetEntity(ProjID, EntityName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DelForm(int ProjID, string EntityName, string FormName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.DelForm(ProjID, EntityName, FormName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEntityList(int ProjID)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.GetEntityList(ProjID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEntityListWithDetail(int ProjID)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.GetEntityListWithDetail(ProjID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetView(int ProjID, string EntityName, string ViewName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.GetView(ProjID, EntityName, ViewName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetViewList(int ProjID, string EntityName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.GetViewList(ProjID, EntityName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveForm(Y_EntityForm Form, List<Y_EntityFormControl> Controls, List<Y_EntityView> Views, List<Y_EntityFilter> Filters, List<Y_EntityFilterControl> FilterControls)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.SaveForm(Form, Controls, Views, Filters, FilterControls), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveProj(Y_Proj Proj)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.SaveProj(Proj), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveControls(List<Y_EntityFormControl> Controls)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.SaveControls(Controls), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveOptions(List<Y_OptionSet> Options)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.SaveOptions(Options), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveView(Y_EntityView View)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.SaveView(View), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetForm(int ProjID, string EntityName, string FormName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            UserSession user = Session["user"] as UserSession;

            //open form log
            LogContent logcontent = new LogContent();

            logcontent.FunctionPath = "DynamicsController";
            logcontent.FunctionName = "GetForm";
            logcontent.FunctionType = "open";
            logcontent.LogType = "info";
            logcontent.ProjID = ProjID;
            //logcontent.LoginCD = user == null ? "" : user.UserName;
            logcontent.UpdateUser = user == null ? "" : user.StaffCD;
            logcontent.Code1 = EntityName;
            logcontent.Code2 = FormName;

            logcontent.AddContent("EntityName", EntityName);
            logcontent.AddContent("FormName", FormName);

            //DyLogLogic.WriteLog(logcontent);

            return Json(logic.GetForm(ProjID, EntityName, FormName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFormControls(int ProjID, string EntityName, string FormName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.GetFormControls(ProjID, EntityName, FormName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveUserControl(Y_EntityUserControl UserControl)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.SaveUserControl(UserControl), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserControl(int ProjID, string EntityName, string UserControlName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.GetUserControl(ProjID, EntityName, UserControlName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserControlList(int ProjID, string EntityName, string Category)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.GetUserControlList(ProjID, EntityName, Category), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DelUserControl(int ProjID, string EntityName, string UserControlName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.DelUserControl(ProjID, EntityName, UserControlName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckUserControlExist(int ProjID, string EntityName, string UserControlName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.CheckUserControlExist(ProjID, EntityName, UserControlName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckFormExist(int ProjID, string EntityName, string FormName, string FormDesc, string Belong)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.CheckFormExist(ProjID, EntityName, FormName, FormDesc, Belong), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPageView(DynamicsViewRequest request)
        {
            DyEntityLogic logic = new DyEntityLogic();

            if (request.pageNumber == 0) request.pageNumber = 1;

            PageViewResult list = logic.GetList(request);

            if (list.ReturnValue != EnumResult.OK)
            {
                return Json(
                    new
                    {
                        pageSize = request.GetPageSize
                    ,
                        totalSize = list.PageCount
                    ,
                        pageNumber = request.pageNumber
                    ,
                        pageData = logic.DataTableToDic(list.DataTable)
                    ,
                        pageSeries = list.series
                    ,
                        pageCategories = list.categories
                    ,
                        pageCrossYItems = list.crossYItems
                    ,
                        ReturnValue = list.ReturnValue
                    ,
                        SQL = list.SQL
                    ,
                        Message = list.Message
                    }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(
                    new
                    {
                        pageSize = request.GetPageSize
                    ,
                        totalSize = list.PageCount
                    ,
                        pageNumber = request.pageNumber
                    ,
                        pageData = logic.DataTableToDic(list.DataTable)
                    ,
                        pageSeries = list.series
                    ,
                        pageCategories = list.categories
                    ,
                        pageCrossYItems = list.crossYItems
                    ,
                        SQL = list.SQL

                    }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetGroupPageView(List<DynamicsViewRequest> requests)
        {
            DyEntityLogic logic = new DyEntityLogic();

            PageViewResult list = logic.GetGroupList(requests);

            if (list.ReturnValue != EnumResult.OK)
            {
                return Json(
                    new
                    {
                        pageSize = requests[0].GetPageSize
                    ,
                        totalSize = list.PageCount
                    ,
                        pageNumber = requests[0].pageNumber
                    ,
                        pageData = logic.DataTableToDic(list.DataTable)
                    ,
                        pageSeries = list.series
                    ,
                        pageCategories = list.categories
                    ,
                        pageCrossYItems = list.crossYItems
                    ,
                        ReturnValue = list.ReturnValue
                    ,
                        Message = list.Message
                    }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(
                    new
                    {
                        pageSize = requests[0].GetPageSize
                    ,
                        totalSize = list.PageCount
                    ,
                        pageNumber = requests[0].pageNumber
                    ,
                        pageData = logic.DataTableToDic(list.DataTable)
                    ,
                        pageSeries = list.series
                    ,
                        pageCategories = list.categories
                    ,
                        pageCrossYItems = list.crossYItems
                    }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult DataExportToExcel(DynamicsViewRequest request, DataExportSetting dataExportSetting)
        {
            DyEntityLogic logic = new DyEntityLogic();

            if (request.pageNumber == 0) request.pageNumber = 1;
            request.GetPageSize = 65535;

            UserSession user = Session["user"] as UserSession;

            if (request.FilterDic != null) request.FilterDic.Add("LoginStaffFilter", user.StaffCD);

            if (request.GetPageSize == 0)
            {
                request.GetPageCount = false;
            }

            PageViewResult list = logic.GetList(request);

            DataExportResult result = doDataExportToExcel(list, dataExportSetting);


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DataMergeExportToExcel(List<DynamicsViewRequest> requests, DataExportSetting dataExportSetting)
        {
            DyEntityLogic logic = new DyEntityLogic();

            PageViewResult list = logic.GetGroupList(requests);

            DataExportResult result = doDataExportToExcel(list, dataExportSetting);


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public DataExportResult doDataExportToExcel(PageViewResult list, DataExportSetting dataExportSetting)
        {
           
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

                    if (caption == "LinkGroupTitle")
                    {
                        caption = "";
                    }

                    if (list.DataTable.Columns.IndexOf(col) >= list.crossYItems.Count)
                    {
                        col.Caption = "beforeType:System.Decimal";
                        caption = list.categories[list.DataTable.Columns.IndexOf(col) - list.crossYItems.Count].field;
                    }

                    if (caption == "AutoSum")
                    {
                        caption = "合計";
                    }

                    if (caption.Length == 8)
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
                    else if (caption.Length == 6)
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

            DataExportResult result = DataIOUtils.ExportToExcel(dataExportSetting, list);

            DyEntityLogic logic = new DyEntityLogic();
            if (result.ResultType == "Success")
            {
                DynamicsSaveRequest resourcesReq = new DynamicsSaveRequest();
                resourcesReq.ProjID = 1;
                resourcesReq.EntityName = "Y_Resources";
                resourcesReq.SaveData = new List<Dictionary<string, object>>();

                string fileName = "";
                if (string.IsNullOrEmpty(dataExportSetting.FileName) == false)
                {
                    fileName = dataExportSetting.FileName;
                }

                String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
                fileName += "_" + date;

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


            return result;
        }

        public JsonResult DataExportToCsv(DynamicsViewRequest request, DataExportSetting dataExportSetting)
        {
            DyEntityLogic logic = new DyEntityLogic();

            if (request.pageNumber == 0) request.pageNumber = 1;
            request.GetPageSize = 65535;

            UserSession user = Session["user"] as UserSession;

            if (request.FilterDic != null) request.FilterDic.Add("LoginStaffFilter", user.StaffCD);

            if (request.GetPageSize == 0)
            {
                request.GetPageCount = false;
            }

            PageViewResult list = logic.GetList(request);

            DataExportResult result = doDataExportToCsv(list, dataExportSetting);


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DataMergeExportToCsv(List<DynamicsViewRequest> requests, DataExportSetting dataExportSetting)
        {
            DyEntityLogic logic = new DyEntityLogic();

            PageViewResult list = logic.GetGroupList(requests);

            DataExportResult result = doDataExportToCsv(list, dataExportSetting);


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public DataExportResult doDataExportToCsv(PageViewResult list, DataExportSetting dataExportSetting)
        {
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

                    if (caption == "LinkGroupTitle")
                    {
                        caption = "";
                    }

                    if (list.DataTable.Columns.IndexOf(col) >= list.crossYItems.Count)
                    {
                        col.Caption = "beforeType:System.Decimal";
                        caption = list.categories[list.DataTable.Columns.IndexOf(col) - list.crossYItems.Count].field;
                    }



                    if (caption == "AutoSum")
                    {
                        caption = "合計";
                    }

                    if (caption.Length == 8)
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
                    else if (caption.Length == 6)
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

            DyEntityLogic logic = new DyEntityLogic();
            if (result.ResultType == "Success")
            {
                DynamicsSaveRequest resourcesReq = new DynamicsSaveRequest();
                resourcesReq.ProjID = 1;
                resourcesReq.EntityName = "Y_Resources";
                resourcesReq.SaveData = new List<Dictionary<string, object>>();

                string fileName = "";
                if (string.IsNullOrEmpty(dataExportSetting.FileName) == false)
                {
                    fileName = dataExportSetting.FileName;
                }

                String date = string.Format("{0:yyyyMMddHHmmssfff}", CommonUtils.GetDateTimeNow());
                fileName += "_" + date;

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


            return result;
        }

        public JsonResult DataImport(string ResourcesID, DataImportSetting dataImportSetting, string FromEntityName, string FromFormName)
        {
            DataImportResult result = new DataImportResult();

            DyEntityLogic logic = new DyEntityLogic();
            List<Dictionary<string, object>> resourceData = new List<Dictionary<string, object>>();

            DataTable resourceDT = logic.FillDataTableBySQL(1, "select * from Y_Resources where ResourcesID =" + DataUtil.AllAgreeSql(ResourcesID));

            if (resourceDT.Rows.Count > 0)
            {
                var physicalPath = Server.MapPath("~/") + DataUtil.CStr(resourceDT.Rows[0]["Path"]);

                if (System.IO.File.Exists(physicalPath))
                {
                    dataImportSetting.FilePath = physicalPath;

                    result = DataIOUtils.DataImport(dataImportSetting);
                }
            }
            else
            {
                result.ResultType = "error";
            }

            UserSession user = Session["user"] as UserSession;
            //DataImport log
            LogContent logcontent = new LogContent();

            logcontent.FunctionPath = FromEntityName;
            logcontent.FunctionName = FromFormName;
            logcontent.FunctionType = "dataImport";
            logcontent.ProjID = dataImportSetting.ProjID;
            //logcontent.LoginCD = user == null ? "" : user.UserName;
            logcontent.UpdateUser = user == null ? "" : user.StaffCD;
            logcontent.Code1 = dataImportSetting.EntityName;

            logcontent.AddContent("dataImportSetting", dataImportSetting);
            logcontent.AddContent("result", result);



            if (result.ResultType != "Success")
            {
                logcontent.LogType = "error";
            }
            else
            {
                logcontent.LogType = "info";
            }

            //DyLogLogic.WriteLog(logcontent);


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOptionSet(int ProjID, string CodeKind, string TargetLang, string DefaultLang)
        {
            DyEntityLogic logic = new DyEntityLogic();
            return Json(logic.DataTableToDic(logic.GetOptionSet(ProjID, CodeKind, TargetLang, DefaultLang)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOptionSetAllLang(int ProjID, string CodeKind, string CD)
        {

            DyEntityLogic logic = new DyEntityLogic();
            return Json(logic.DataTableToDic(logic.GetOptionSet(ProjID, CodeKind, CD)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExecEntitySql(int ProjID, List<string> EntityList)
        {
            DyEntityLogic logic = new DyEntityLogic();
            return Json(logic.ExecEntitySql(ProjID, EntityList), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProjInfo(int ProjID)
        {
            DyEntityLogic logic = new DyEntityLogic();
            return Json(logic.GetProjInfo(ProjID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMenuList(int ProjID)
        {
            DyEntityLogic logic = new DyEntityLogic();
            return Json(logic.GetMenuList(ProjID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMenuGroupList(int ProjID)
        {
            DyEntityLogic logic = new DyEntityLogic();
            return Json(logic.GetMenuGroupList(ProjID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveMenu(Y_Menu menu, bool isUpdate)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.SaveMenu(menu, isUpdate), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DelMenu(int ProjID, string MenuName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.DelMenu(ProjID, MenuName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveMenuGroup(int ProjID, List<Y_MenuGroup> menuGroupList)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.SaveMenuGroup(ProjID, menuGroupList), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoleList(int ProjID)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.GetRoleList(ProjID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveRole(int ProjID, List<Y_Role> roleList)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.SaveRole(ProjID, roleList), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReferInfo(int ProjID)
        {
            DyEntityLogic logic = new DyEntityLogic();
            return Json(logic.DataTableToDic(logic.GetReferInfo(ProjID)), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveResource(IEnumerable<HttpPostedFileBase> files, string resourceIds, string newResourceId)
        {
            // The Name of the Upload component is "files"
            List<string> keys = new List<string>();
            if (files != null)
            {

                string[] rids = null;
                if (string.IsNullOrEmpty(resourceIds) == false)
                {
                    rids = resourceIds.Split(',');

                    this.DeleteResource(rids);
                }

                DyEntityLogic logic = new DyEntityLogic();

                DynamicsSaveRequest resourcesReq = new DynamicsSaveRequest();
                resourcesReq.ProjID = 1;
                resourcesReq.EntityName = "Y_Resources";
                resourcesReq.SaveData = new List<Dictionary<string, object>>();

                int index = 0;
                foreach (HttpPostedFileBase file in files)
                {
                    // Some browsers send file names with full path. This needs to be stripped.
                    var fileName = Path.GetFileName(file.FileName);

                    String extension = fileName.Substring(fileName.LastIndexOf('.') + 1);

                    //get key
                    //new resource
                    String key = Guid.NewGuid().ToString().Replace("-", "");

                    if (newResourceId != null)
                    {
                        key = newResourceId;
                    }

                    string yearMonth = DateTime.Now.ToString("yyyyMM");

                    string resourceDir = "/App_Resource/" + yearMonth;

                    DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Server.MapPath("~" + resourceDir)));
                    if (!directoryInfo.Exists)
                    {
                        directoryInfo.Create();
                    }

                    var physicalPath = Path.Combine(Server.MapPath("~" + resourceDir), key + "." + extension);

                    // The files are not actually saved in this demo
                    file.SaveAs(physicalPath);

                    Dictionary<string, object> resourcesDataItem = new Dictionary<string, object>();
                    resourcesDataItem.Add("DyTableName", "Y_Resources");
                    resourcesDataItem.Add("ResourcesID", key);
                    resourcesDataItem.Add("ResourcesName", fileName.Substring(0, fileName.LastIndexOf('.')));
                    resourcesDataItem.Add("Extension", extension);
                    resourcesDataItem.Add("Path", resourceDir + "/" + key + "." + extension);
                    resourcesDataItem.Add("UploadName", fileName);
                    resourcesDataItem.Add("Size", file.ContentLength);
                    resourcesDataItem.Add("UploadTime", DateTime.Now);
                    resourcesReq.SaveData.Add(resourcesDataItem);

                    keys.Add(key);

                    index++;
                }

                logic.Save(resourcesReq);
            }


            string ks = string.Join(",", keys.ToArray());
            //HttpResponseBase response = ControllerContext.HttpContext.Response;
            ControllerContext.HttpContext.Response.StatusDescription = ks;

            //response.Write(ks);

            // Return an empty string to signify success
            return Content("");
        }


        public ActionResult RemoveResource(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Resource"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        // System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        /// <summary>
        /// リソース削除
        /// </summary>
        /// <param name="ResourcesIDList">リソースIDリスト</param>
        /// <returns></returns>
        public JsonResult DeleteResource(string[] ResourcesIDList)
        {
            if (ResourcesIDList != null)
            {
                DyEntityLogic logic = new DyEntityLogic();
                List<Dictionary<string, object>> resourceData = new List<Dictionary<string, object>>();

                foreach (string resourceID in ResourcesIDList)
                {
                    DataTable resourceDT = logic.FillDataTableBySQL(1, "select * from Y_Resources where ResourcesID =" + DataUtil.AllAgreeSql(resourceID));

                    if (resourceDT.Rows.Count > 0)
                    {
                        var physicalPath = Server.MapPath("~/") + DataUtil.CStr(resourceDT.Rows[0]["Path"]);

                        if (System.IO.File.Exists(physicalPath))
                        {
                            System.IO.File.Delete(physicalPath);
                        }
                    }

                    Dictionary<string, object> resource = new Dictionary<string, object>();
                    resource.Add("DyTableName", "Y_Resources");
                    resource.Add("ResourcesID", resourceID);
                    resourceData.Add(resource);
                }

                DynamicsDeleteRequest request = new DynamicsDeleteRequest();
                request.EntityName = "Y_Resources";
                request.ProjID = 1;
                request.SaveData = resourceData;

                logic.Del(request);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// リソースパスを取得
        /// </summary>
        /// <param name="ResourcesID">リソースID</param>
        /// <returns></returns>
        public JsonResult FindResourcePath(string ResourcesID)
        {
            DyEntityLogic logic = new DyEntityLogic();
            List<Dictionary<string, object>> resourceData = new List<Dictionary<string, object>>();

            string path = "/SMAT.UI/images/image.png";

            DataTable resourceDT = logic.FillDataTableBySQL(1, "select * from Y_Resources where ResourcesID =" + DataUtil.AllAgreeSql(ResourcesID));

            if (resourceDT.Rows.Count > 0)
            {
                var physicalPath = Server.MapPath("~/") + DataUtil.CStr(resourceDT.Rows[0]["Path"]);

                if (System.IO.File.Exists(physicalPath))
                {
                    path = DataUtil.CStr(resourceDT.Rows[0]["Path"]);
                }
            }

            return Json(path, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownLoadResource(string ResourcesID)
        {
            DyEntityLogic logic = new DyEntityLogic();

            DataTable resourceDT = logic.FillDataTableBySQL(1, "select * from Y_Resources where ResourcesID =" + DataUtil.AllAgreeSql(ResourcesID));

            if (resourceDT.Rows.Count > 0)
            {
                var physicalPath = Server.MapPath("~/") + DataUtil.CStr(resourceDT.Rows[0]["Path"]);

                if (System.IO.File.Exists(physicalPath))
                {
                    FileStream fs = new FileStream(physicalPath, FileMode.Open);
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                    Response.ContentType = "application/octet-stream";

                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(DataUtil.CStr(resourceDT.Rows[0]["UploadName"])));
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }

            return new EmptyResult();
        }

        public ActionResult GetExportedResource(string ResourcesID)
        {
            DownLoadResource(ResourcesID);

            DeleteResource(new string[1] { ResourcesID });

            return new EmptyResult();
        }

        /// <summary>
        /// ノート送信
        /// </summary>
        /// <param name="note">ノート</param>
        /// <returns></returns>
        public JsonResult SendNode(Y_Note note)
        {
            DyNoteLogic logic = new DyNoteLogic();
            return Json(logic.SendNode(note), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ノートを取得
        /// </summary>
        /// <param name="note">ノートリスト</param>
        /// <returns></returns>
        public JsonResult GetNodes(int ProjID, string NoteUserCD, DateTime? SendTimeFrom)
        {
            DyNoteLogic logic = new DyNoteLogic();
            return Json(logic.GetNodes(ProjID, NoteUserCD, SendTimeFrom), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ノートを取得
        /// </summary>
        /// <param name="note">ノートリスト</param>
        /// <returns></returns>
        public JsonResult SaveNodeStatus(string NoteCD, string Status)
        {
            DyNoteLogic logic = new DyNoteLogic();
            return Json(logic.SaveNodeStatus(NoteCD, Status), JsonRequestBehavior.AllowGet);
        }
    }
}