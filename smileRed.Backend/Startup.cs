using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(smileRed.Backend.Startup))]
namespace smileRed.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
