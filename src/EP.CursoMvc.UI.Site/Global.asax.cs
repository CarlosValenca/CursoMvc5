using EP.CursoMvc.Application.AutoMapper;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EP.CursoMvc.UI.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Ao subir a aplicação no IIS o AutoMapper é registrado
            // Desta forma esta camada sabe se comunicar com o domínio quando for necessário
            AutoMapperConfig.RegisterMappings();
        }
    }
}
