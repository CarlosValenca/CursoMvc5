using EP.CursoMvc.Application.Services;
using SimpleInjector;
using EP.CursoMvc.Application.Interfaces;
using EP.CursoMvc.Domain.Interfaces;
using EP.CursoMvc.Domain.Services;
using EP.CursoMvc.Infra.Data.Repository;
using EP.CursoMvc.Infra.Data.Context;
using EP.CursoMvc.Infra.Data.UoW;

namespace EP.CursoMvc.Infra.CrossCutting.IoC
{
    public class SimpleInjectorBootstrapper
    {
        public static void Register(Container container)
        {
            // Lifestyle.Transient => Uma instância para cada solicitação (padrão) : utilize quando você não quer alterar o estado dela em outras classes injetadas
            // Lifestyle.Singleton => Uma instância única para a classe (para o servidor), cuidado para classes que envolvam dados como a Repository por exemplo ! Quando vc não quer criar instâncias diversas, por exemplo uma classe que envia email, qualquer coisa genérica que não involva dados
            // Lifestyle.Scoped => Uma instância única para o request. Esta poderia ser a opção padrão, pois permite uma instância só por usuário.
            
            // APP
            container.Register<IClienteAppService, ClienteAppService>(Lifestyle.Scoped);

            // Domain
            container.Register<IClienteService, ClienteService>(Lifestyle.Scoped);

            // Infra
            container.Register<IClienteRepository, ClienteRepository>(Lifestyle.Scoped);

            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);

            container.Register<CursoMvcContext>(Lifestyle.Scoped);
        }
    }
}
