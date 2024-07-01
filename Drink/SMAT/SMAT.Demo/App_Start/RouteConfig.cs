using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASMAT.Demo
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

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Dynamics", action = "FormList", id = UrlParameter.Optional }
            );

        }
    }
}
