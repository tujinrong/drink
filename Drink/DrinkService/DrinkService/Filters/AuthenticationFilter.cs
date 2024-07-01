using DrinkService.Data.Models;
using DrinkService.Models;
using DrinkService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DrinkService.Filters
{
    public class AuthenticationFilter : FilterAttribute, IActionFilter,IExceptionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.isTestVersion = CommonLogic.IsTestVersion;
            filterContext.Controller.ViewBag.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.Name;
            filterContext.Controller.ViewBag.ActionName = filterContext.ActionDescriptor.ActionName;
            if (filterContext.HttpContext.Session["user"] == null)
            {
                if (!(filterContext.ActionDescriptor.ActionName == "Login"))
                {
                    filterContext.Result = new RedirectResult("/Main/Login");
                }
            }
            else 
            {
                filterContext.Controller.ViewBag.LoginUser = filterContext.HttpContext.Session["user"] as UserSession;
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnException(ExceptionContext filterContext)
        {
            string controllerName = filterContext.Controller.ViewBag.ControllerName;
            string actionName = filterContext.Controller.ViewBag.ActionName;
            new Thread(() => { Email.SendMail(controllerName, actionName, filterContext.Exception.Message, filterContext.Exception.StackTrace); }).Start();
            new Thread(() => { Log.WriteLog("controllerName:" + controllerName + "\r\n" + "actionName:" + actionName + "\r\n" + filterContext.Exception.Message + "\r\n" + filterContext.Exception.StackTrace); }).Start();
        }

    }
}