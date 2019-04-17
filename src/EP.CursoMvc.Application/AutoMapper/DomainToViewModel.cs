using AutoMapper;
using EP.CursoMvc.Application.ViewModels;
using EP.CursoMvc.Domain.Models;
namespace EP.CursoMvc.Application.AutoMapper
{
    // Esta classe transforma de Domínio para View Model
    public class DomainToViewModel : Profile
    {
        public DomainToViewModel()
        {
            // Eu sei transforar Cliente em Cliente View Model
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            // Eu também sei transforar Endereco em Endereco View Model
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
        }
    }

}
