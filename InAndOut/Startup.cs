using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InAndOut.Startup))]
namespace InAndOut
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
