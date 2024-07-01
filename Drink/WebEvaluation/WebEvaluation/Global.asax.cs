using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebEvaluation.DAL;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity;
using WebEvaluation.Controllers.Common;

namespace WebEvaluation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbInterception.Add(new EvaluationInterceptorTransientErrors());
            DbInterception.Add(new EvaluationInterceptorLogging());
            Database.SetInitializer<EvaluationContext>(null);

            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new DyJsonValueProviderFactory());
        }
    }
}
