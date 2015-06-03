using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAuth.Startup))]
namespace WebAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
