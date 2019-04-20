using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace EP.CursoMvc.Infra.CrossCutting.Filters
{
    public class ClaimsAuthorize : AuthorizeAttribute
    {
        private static string _claimName;
        private static string _claimValue;

        public ClaimsAuthorize(string claimName, string claimValue)
        {
            _claimName = claimName;
            _claimValue = claimValue;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName == "Index")
                _claimValue = "LI";
            if (filterContext.ActionDescriptor.ActionName == "Details")
                _claimValue = "DE";
            if (filterContext.ActionDescriptor.ActionName == "Create")
                _claimValue = "IN";
            if (filterContext.ActionDescriptor.ActionName == "Edit")
                _claimValue = "ED";
            if (filterContext.ActionDescriptor.ActionName == "Delete")
                _claimValue = "EX";
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var identity = (ClaimsIdentity)httpContext.User.Identity;
            var claim = identity.Claims.FirstOrDefault(c => c.Type == _claimName);
            return claim != null && claim.Value.Contains(_claimValue);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // estando conectado informa que o usuário não tem acesso
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                // HttpStatusCode pode ser usado para informar o tipo de problema ocorrido
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            else
            {
                // comportamento padrão, envia para a tela de login
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
