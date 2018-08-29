using System.Collections.Generic;
using System.Threading.Tasks;
using cqrs.Domain.Common;

namespace cqrs.Domain.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : Entity, IAggreagateRoot
    {
        TEntity FindOne(string id);
        TEntity FindOne(ISpecification<TEntity> specification);
        IEnumerable<TEntity> FindAll();
        IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification);
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> FindOneAsync(string id);
        Task<TEntity> FindOneAsync(ISpecification<TEntity> specification);
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<IEnumerable<TEntity>> FindAllAsync(ISpecification<TEntity> specification);
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
