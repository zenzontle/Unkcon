using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Unkcon.Startup))]
namespace Unkcon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
