using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cqrs.Domain.Entities;
using cqrs.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cqrs.Data.Sql.EF
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly DbSet<Auction> _dbSet;
        private readonly IRepository<Auction> _repository;

        public AuctionRepository(
            AuctionContext dbContext,
            IRepository<Auction> repository
        )
        {
            _dbSet = dbContext.Set<Auction>();
            _repository = repository;
        }

        public async Task<Auction> FindOneAsync(string id)
        {
            return await _dbSet.AsQueryable()
                .Include(x => x.Lot)
                .Include(x => x.Seller)
                .Include(x => x.Bids).ThenInclude(x => x.Bidder)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Auction>> FindAllAsync(ISpecification<Auction> specification)
        {
            return await _dbSet.AsQueryable()
                .Include(x => x.Lot)
                .Include(x => x.Seller)
                .Include(x => x.Bids).ThenInclude(x => x.Bidder)
                .Where(specification.GetExpression())
                .ToListAsync();
        }

        public async Task<Auction> CreateAsync(Auction auction)
        {
            return await _repository.CreateAsync(auction);
        }

        public async Task UpdateAsync(Auction auction)
        {
            await _repository.UpdateAsync(auction);
        }
    }
}
