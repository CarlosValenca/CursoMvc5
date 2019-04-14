using EP.CursoMvc.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EP.CursoMvc.Domain.Interfaces
{
    // Esta interface é do tipo destruível baseado na entidade Entity
    public interface IRepositoryRead<TEntity> : IDisposable where TEntity : Entity
    {
        // Como eu não sei que entidade eu retornarei, utiliamos o TEntity, desta forma podemos obter um cliente ou um endereço por exemplo
        TEntity ObterPorId(Guid Id);

        // Aqui receberemos uma coleção de entidades
        IEnumerable<TEntity> ObterTodos();
        // s de skip: quantos registros irá pular, t de take: quantos registros irá pegar
        // de 10 em 10
        // s = 0, t = 10 - página 1, pegue os primeiros 10
        // s = 10, t = 20 - página 2, pegue 20 registros mas pule os primeiros 10
        // s = 20, t = 30 - página 3, pegue 30 registros mas pule os primeiros 20
        IEnumerable<TEntity> ObterTodosPaginado(int s, int t);

        // Buscarei qualquer entidade através de qualquer campo (ou campos)
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);

    }
}
