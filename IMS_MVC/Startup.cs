using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IMS_MVC.Startup))]
namespace IMS_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
