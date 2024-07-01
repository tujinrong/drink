using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEvaluation.Models;
using WebEvaluation.Utils;

namespace WebEvaluation.Controllers.Filters
{
    public class AuthenticationFilter : FilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
            //string ip = HttpContext.Current.Request.UserHostAddress;
            //List<string> iplist = CommonUtils.getIpRules();

            //if (!iplist.Contains(ip))
            //{
            //    filterContext.Result = new RedirectResult("/Home/Login");
            //}

            Console.WriteLine("--OnActionExecuting---");
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            Console.WriteLine("--OnActionExecuted---");
        }

    }
}