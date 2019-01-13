using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FortyLife.Startup))]
namespace FortyLife
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
