using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERP_Web_Portal.Startup))]
namespace ERP_Web_Portal
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
