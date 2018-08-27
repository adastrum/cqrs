using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cqrs.Domain.Common;
using cqrs.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cqrs.Data.Sql.EF
{
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity, IAggreagateRoot
    {
        private readonly AuctionContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public EfRepository(AuctionContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> FindOneAsync(string id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        public async Task<TEntity> FindOneAsync(ISpecification<TEntity> specification)
        {
            return await _dbSet.AsQueryable().Where(specification.GetExpression()).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await _dbSet.AsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(ISpecification<TEntity> specification)
        {
            return await _dbSet.AsQueryable().Where(specification.GetExpression()).ToListAsync();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
