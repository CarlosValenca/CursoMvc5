using EP.CursoMvc.Application.AutoMapper;
using System.Web.Http;

namespace EP.CursoMvc.REST.ClienteAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // Ao subir a aplicação no IIS o AutoMapper é registrado
            // Desta forma esta camada sabe se comunicar com o domínio quando for necessário
            AutoMapperConfig.RegisterMappings();
        }
    }
}
