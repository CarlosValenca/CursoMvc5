using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EP.CursoMvc.UI.Site.Startup))]
namespace EP.CursoMvc.UI.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
