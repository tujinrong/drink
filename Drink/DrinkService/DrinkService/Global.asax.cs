using DrinkService.Controllers.Common;
using DrinkService.Data.Models;
using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DrinkService
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["user_session_keys"] = new Dictionary<string,object>();

            string dbConnectionString = ConfigurationManager.ConnectionStrings["DrinkServiceContext"].ToString();
            if (dbConnectionString.Contains("Data Source=10.193.13.199;"))
            {
                CommonLogic.IsTestVersion = "false";
            }
            else if (dbConnectionString.Contains("Initial Catalog=DrinkTemp;"))
            {
                CommonLogic.IsTestVersion = "kensyu";
            }
            else
            {
                CommonLogic.IsTestVersion = "test";
            }

            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new DyJsonValueProviderFactory());
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            Application.Lock();
            Session.Timeout = 60;
            //Application["user_sessions "] = (int)Application["user_sessions "] + 1;
            Application.UnLock();
        }
        protected void Session_End(Object sender, EventArgs e)
        {
            Application.Lock();

            if (Session["user"] != null) {
                UserSession user = Session["user"] as UserSession;
                Dictionary<string, object> keys = (Application["user_session_keys"] as Dictionary<string, object>);
                if (keys.ContainsKey(user.ShopCD + "_" + user.StaffCD)) {
                    keys.Remove(user.ShopCD + "_" + user.StaffCD);
                }

            }
            Application.UnLock();
        } 
    }
}
