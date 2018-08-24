using System.Collections.Generic;
using System.Threading.Tasks;

namespace cqrs.Domain.Interfaces
{
    public interface IRepository<T>
        where T : IAggreagateRoot
    {
        Task<T> FindOneAsync(string id);
        Task<IEnumerable<T>> FindAllAsync();
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
