using System;
using System.Linq.Expressions;
using cqrs.Domain.Common;

namespace cqrs.Domain.Interfaces
{
    public interface ISpecification<TEntity>
        where TEntity : Entity
    {
        Expression<Func<TEntity, bool>> GetExpression();
    }
}
