using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(DrinkService.Startup))]
namespace DrinkService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DrinkService.Report.Common.Config.TemplatePath = @AppDomain.CurrentDomain.BaseDirectory + @"PDFTemplate\";

            ConfigureAuth(app);
        }
    }
}
