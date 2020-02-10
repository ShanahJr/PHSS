using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PHSS.Startup))]
namespace PHSS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
