using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace EP.CursoMvc.REST.ClienteAPI.Controllers
{
    // Attribute: Para decorar a controller com os atributos e implementa o ICorsPolicyProvider que vai te obrigar a implementar o método da interface
    public class MyCorsPolicy : Attribute, ICorsPolicyProvider
    {
        private CorsPolicy _policy;
        public MyCorsPolicy()
        {
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };
            _policy.Origins.Add("http://myclient.azurewebsites.net");
            _policy.Origins.Add("http://www.contoso.com");
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }
    }
}