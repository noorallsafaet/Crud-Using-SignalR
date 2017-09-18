using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CallerInfo.Startup))]
namespace CallerInfo
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
