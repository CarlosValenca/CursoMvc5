using AutoMapper;
namespace EP.CursoMvc.Application.AutoMapper
{
    // Esta classe transforma de View Model para Domínio
    public class ViewModelToDomain : Profile
    {
        public ViewModelToDomain()
        {
            /*
             * Com o comando ReverseMap do DomainViewModel não é necessário
             * colocar os comandos abaixo, principalmente por que as propriedades da entidade e
             * do mapper são iguais, deixei aqui exemplo apenas para referência

            // Eu sei transforar Cliente View Model em Cliente
            CreateMap<ClienteViewModel, Cliente>();
            // Eu também sei transforar Endereco View Model em Endereco
            CreateMap<EnderecoViewModel, Endereco>();
            */
        }
    }

}
