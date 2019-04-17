using EP.CursoMvc.Infra.CrossCutting.Filters;
using System.Web;
using System.Web.Mvc;

namespace EP.CursoMvc.UI.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // A cada request meu filtro personalizado será chamado, os erros serão mantidos neste filtro
            filters.Add(new GlobalActionLogger());
        }
    }
}
