using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DrinkService
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Dynamic",
               "dy/{ProjID}/{EntityName}/{FormName}",
               new { controller = "Dynamics", action = "FormPage", ProjID = UrlParameter.Optional, EntityName = UrlParameter.Optional, FormName = UrlParameter.Optional }// 参数默认值    
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
                defaults: new { controller = "Main", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
