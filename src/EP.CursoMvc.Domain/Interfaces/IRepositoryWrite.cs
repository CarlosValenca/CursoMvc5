using EP.CursoMvc.Domain.Models;
using System;

namespace EP.CursoMvc.Domain.Interfaces
{
    public interface IRepositoryWrite<TEntity> : IDisposable where TEntity : Entity
    {
        void Adicionar(TEntity obj);

        void Atualizar(TEntity obj);

        void Remover(Guid id);

        int SaveChanges();
    }
}
