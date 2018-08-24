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
        private readonly AuctionContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EfRepository(AuctionContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> FindOneAsync(string id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await _dbSet.AsQueryable().ToListAsync();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
