using DrinkService.Models;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Logic;
using SafeNeeds.DySmat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASMAT.Demo.Controllers
{
    public class DynamicsController : Controller
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

        public ActionResult ProjectManager(int ProjID)
        {
            ViewBag.ProjID = ProjID;

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult SubFormTest()
        {
            return View();
        }

        public ActionResult Home(int ProjID, string roleName)
        {
            DyEntityLogic logic = new DyEntityLogic();
            List<Y_MenuGroup> groups = logic.GetMenuGroupList(ProjID);
            List<Y_Menu> menus = logic.GetRoleMenuList(ProjID, roleName);

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

            ViewBag.ProjID = ProjID;
            ViewData.Add("menuGroupList", menuGroupList);
            return View();
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

            return Json(logic.GetEntityDataItem(ProjID, EntityName, dataItem), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(DynamicsSaveRequest request)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.Save(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Del(DynamicsDeleteRequest request)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.Del(request), JsonRequestBehavior.AllowGet);
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

        public JsonResult SaveView(Y_EntityView View)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.SaveView(View), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetForm(int ProjID, string EntityName, string FormName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.GetForm(ProjID, EntityName, FormName), JsonRequestBehavior.AllowGet);
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

        public JsonResult GetUserControlList(int ProjID, string EntityName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.GetUserControlList(ProjID, EntityName), JsonRequestBehavior.AllowGet);
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

        public JsonResult CheckFormExist(int ProjID, string EntityName, string FormName)
        {
            DyEntityLogic logic = new DyEntityLogic();

            return Json(logic.CheckFormExist(ProjID, EntityName, FormName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPageView(DynamicsViewRequest request)
        {
            DyEntityLogic logic = new DyEntityLogic();

            request.GetPageCount = true;

            if (request.pageNumber == 0) request.pageNumber = 1;

            PageViewResult list = logic.GetList(request);


            return Json(new { pageSize = request.GetPageSize, totalSize = list.PageCount, pageNumber = request.pageNumber, pageData = logic.DataTableToDic(list.DataTable) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOptionSet(int ProjID, string CodeKind, string TargetLang, string DefaultLang)
        {
            DyEntityLogic logic = new DyEntityLogic();
            return Json(logic.DataTableToDic(logic.GetOptionSet(ProjID, CodeKind, TargetLang, DefaultLang)), JsonRequestBehavior.AllowGet);
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
        
    }
}