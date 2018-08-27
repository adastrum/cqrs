using System;
using System.Collections.Generic;

namespace cqrs.Web.MVC.Models
{
    public class AuctionViewModel
    {
        public string Id { get; set; }
        public UserViewModel Seller { get; set; }
        public LotViewModel Lot { get; set; }
        public DateTime? Closes { get; set; }
        public MoneyViewModel CurrentAmount { get; set; }
        public IEnumerable<BidViewModel> Bids { get; set; }

        public bool CanManage { get; set; }
    }
}
