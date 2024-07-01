using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASMAT.Demo.Startup))]
namespace ASMAT.Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
