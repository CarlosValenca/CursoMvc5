using AutoMapper;
using System;
namespace EP.CursoMvc.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Inicialize o auto mapper com estes 2 perfis de mapeamento
            Mapper.Initialize(i =>
            {
                i.AddProfile<DomainToViewModel>();
                i.AddProfile<ViewModelToDomain>();
            });
        }
    }
}
