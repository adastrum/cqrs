using System;
using System.Linq.Expressions;
using cqrs.Domain.Entities;
using cqrs.Domain.Enums;
using cqrs.Domain.Interfaces;

namespace cqrs.Application.Specifications
{
    public class AuctionByStatus : ISpecification<Auction>
    {
        private readonly AuctionStatus _status;

        public AuctionByStatus(AuctionStatus status)
        {
            _status = status;
        }

        public Expression<Func<Auction, bool>> GetExpression()
        {
            return x => x.Status == _status;
        }
    }
}
