using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppliedSystems.Web.Startup))]
namespace AppliedSystems.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
