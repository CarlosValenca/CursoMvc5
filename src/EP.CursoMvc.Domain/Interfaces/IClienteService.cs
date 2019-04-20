using EP.CursoMvc.Domain.Models;
using System;

namespace EP.CursoMvc.Domain.Interfaces
{
    public interface IClienteService : IDisposable
    {
        Cliente Adicionar(Cliente cliente);
        Cliente Atualizar(Cliente cliente);
        void Remover(Guid id);
    }
}
