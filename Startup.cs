using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webstats.Startup))]
namespace webstats
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
