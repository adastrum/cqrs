using System.Collections.Generic;
using System.Threading.Tasks;
using cqrs.Domain.Entities;

namespace cqrs.Domain.Interfaces
{
    public interface IAuctionRepository
    {
        Task<Auction> FindOneAsync(string id);
        Task<IEnumerable<Auction>> FindAllAsync(ISpecification<Auction> specification);
        Task<Auction> CreateAsync(Auction auction);
        Task UpdateAsync(Auction auction);
    }
}
