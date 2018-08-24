using System;
using System.Threading.Tasks;
using cqrs.Domain.Entities;
using cqrs.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace cqrs.Data.Sql.EF.Tests
{
    public class EfRepositoryTests
    {
        private readonly DbContextOptions<AuctionContext> _dbContextOptions;

        public EfRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AuctionContext>()
                .UseInMemoryDatabase(nameof(EfRepositoryTests))
                .Options;
        }

        [Fact]
        public async Task Test1()
        {
            using (var dbContext = new AuctionContext(_dbContextOptions))
            {
                var repository = new EfRepository<Auction>(dbContext);
                var seller = new User("John Doe");
                var created = await repository.CreateAsync(
                    new Auction("apple", "fresh and juicy", TimeSpan.FromDays(2), new Money(999.99m), seller)
                );
                var one = await repository.FindOneAsync(created.Id);
                var all = await repository.FindAllAsync();
            }
        }
    }
}
