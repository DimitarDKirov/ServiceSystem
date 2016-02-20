using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(ServiceSystem.Web.Startup))]

namespace ServiceSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
