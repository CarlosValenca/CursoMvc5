using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace EP.CursoMvc.Infra.CrossCutting.Filters
{
    // Esta é uma classe de extensão, vou adicionar funcionalidades a métodos já existentes
    public static class ClaimsHelper
    {
        // o this MvcHtmlString indica que estou extendendo a classe MvcHtmlString para que
        // possamos incluir a possibilidade de excluir um link disponibilizado via ActionLink
        // na página Index por exemplo sem ter que ficar colocando IFs e tudo o mais
        // O value representa uma instancia da classe MvcHtmlString
        public static MvcHtmlString IfClaimHelper(this MvcHtmlString value, string claimName, string claimValue)
        {
            // Checa nas claims deste usuário, se o mesmo possui uma claim específica
            return ValidarClaimsUsuario(claimName, claimValue) ? value : MvcHtmlString.Empty;
        }
        public static bool IfClaim(this WebViewPage value, string claimName, string claimValue)
        {
            // Checa nas claims deste usuário, se o mesmo possui uma claim específica
            return ValidarClaimsUsuario(claimName, claimValue);
        }

        public static string IfClaimShow(this WebViewPage value, string claimName, string claimValue)
        {
            // Checa nas claims deste usuário, se o mesmo possui uma claim específica
            return ValidarClaimsUsuario(claimName, claimValue) ? "" : "disabled";
        }

        public static bool ValidarClaimsUsuario(string claimName, string claimValue)
        {
            // Retorna as claims do usuário conectado
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var claim = identity.Claims.FirstOrDefault(c => c.Type == claimName);
            return claim != null && claim.Value.Contains(claimValue);
        }

    }
}