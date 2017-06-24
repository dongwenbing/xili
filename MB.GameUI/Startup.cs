using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MB.GameUI.Startup))]
namespace MB.GameUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
