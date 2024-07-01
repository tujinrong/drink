using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEvaluation.DataModels;
using WebEvaluation.Models;

namespace WebEvaluation.Controllers.Filters
{
    public class WebAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (null != httpContext.Session["user"])
            {
                string roleCD = (httpContext.Session["user"] as UserSession).RoleCD;
                if (Roles.Contains(roleCD))
                {
                    return true;
                }
                else
                {
                    httpContext.Response.StatusCode = 401;
                }
            }
            else
            {
                httpContext.Response.StatusCode = 401;
            }
            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 401)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }  
        }
    }
}