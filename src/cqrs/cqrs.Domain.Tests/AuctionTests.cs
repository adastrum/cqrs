using System;
using cqrs.Domain.Entities;
using cqrs.Domain.Enums;
using cqrs.Domain.ValueObjects;
using Xunit;

namespace cqrs.Domain.Tests
{
    public class AuctionTests
    {
        private readonly User _seller;
        private readonly User _bidder;

        public AuctionTests()
        {
            _seller = new User("John Doe");
            _bidder = new User("Jane Doe");
        }

        private Auction CreateAuction()
        {
            return new Auction("apple", "fresh and juicy", TimeSpan.FromDays(2), new Money(999.99m), _seller);;
        }

        [Fact]
        public void Start_SetsStatusActive_SetsStartDate()
        {
            var auction = CreateAuction();
            auction.Start();

            Assert.Equal(AuctionStatus.Active, auction.Status);
            Assert.NotNull(auction.StartDate);
        }
    }
}
