using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TipidPC.Presentation.Web.Startup))]
namespace TipidPC.Presentation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
