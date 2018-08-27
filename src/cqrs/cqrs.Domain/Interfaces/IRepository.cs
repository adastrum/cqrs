using System.Collections.Generic;
using System.Threading.Tasks;
using cqrs.Domain.Common;

namespace cqrs.Domain.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : Entity, IAggreagateRoot
    {
        Task<TEntity> FindOneAsync(string id);
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<IEnumerable<TEntity>> FindAllAsync(ISpecification<TEntity> specification);
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
