using System;
using System.Collections.Generic;
using System.Linq;
using cqrs.Domain.Common;
using cqrs.Domain.Enums;
using cqrs.Domain.Exceptions;
using cqrs.Domain.Interfaces;
using cqrs.Domain.ValueObjects;

namespace cqrs.Domain.Entities
{
    public class Auction : Entity, IAggreagateRoot
    {
        private readonly List<Bid> _bids = new List<Bid>();

        public Lot Lot { get; protected set; }
        public DateTime? StartDate { get; protected set; }
        public DateTime? CloseDate { get; protected set; }
        public TimeSpan Duration { get; protected set; }
        public Money InitialAmount { get; protected set; }
        public User Seller { get; protected set; }
        public IList<Bid> Bids => _bids;
        public AuctionStatus Status { get; protected set; }

        protected Auction() { }

        public Auction(string name, string description, TimeSpan duration, Money initialAmount, User seller)
        {
            Lot = new Lot(name, description);
            Duration = duration;
            InitialAmount = initialAmount;
            Seller = seller;
        }

        public void Start()
        {
            if (Status != AuctionStatus.New)
            {
                throw new AuctionStatusException();
            }

            Status = AuctionStatus.Active;
            StartDate = DateTime.Now;
        }

        public void Close()
        {
            if (Status != AuctionStatus.Active)
            {
                throw new AuctionStatusException();
            }

            Status = AuctionStatus.Closed;
            CloseDate = DateTime.Now;
        }

        public void Cancel()
        {
            if (Status != AuctionStatus.Active)
            {
                throw new AuctionStatusException();
            }

            Status = AuctionStatus.Closed;
            CloseDate = DateTime.Now;
        }

        public void Bid(Money amount, User bidder)
        {
            if (Status != AuctionStatus.Active)
            {
                throw new AuctionStatusException();
            }

            if (amount < InitialAmount)
            {
                throw new BidAmountException();
            }

            var lastBid = _bids.OrderBy(x => x.Date).LastOrDefault();
            if (lastBid != null && amount <= lastBid.Amount)
            {
                throw new BidAmountException();
            }

            _bids.Add(new Bid(amount, bidder));
        }
    }
}
