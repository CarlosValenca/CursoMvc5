using DomainValidation.Interfaces.Specification;
using EP.CursoMvc.Domain.Models;
using System;
using System.Linq.Expressions;

namespace EP.CursoMvc.Domain.Specifications
{
    // https://enterprisecraftsmanship.com/2016/02/08/specification-pattern-c-implementation/
    public class GenericSpecification<T> : ISpecification<T> where T : Entity
    {
        public Expression<Func<T, bool>> Expression { get; }

        public GenericSpecification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return Expression.Compile().Invoke(entity);
        }
    }
}
