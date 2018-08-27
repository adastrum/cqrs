using System;
using System.Linq;
using System.Linq.Expressions;
using cqrs.Domain.Entities;
using cqrs.Domain.Enums;
using cqrs.Domain.Interfaces;

namespace cqrs.Application.Specifications
{
    public class AuctionByStatus : ISpecification<Auction>
    {
        private readonly AuctionStatus[] _statuses;

        public AuctionByStatus(params AuctionStatus[] statuses)
        {
            _statuses = statuses;
        }

        public Expression<Func<Auction, bool>> GetExpression()
        {
            return x => _statuses.Contains(x.Status);
        }
    }
}
