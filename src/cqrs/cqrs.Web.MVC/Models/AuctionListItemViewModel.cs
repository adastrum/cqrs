using System;

namespace cqrs.Web.MVC.Models
{
    public class AuctionListItemViewModel
    {
        public string Id { get; set; }
        public UserViewModel Seller { get; set; }
        public LotViewModel Lot { get; set; }
        public DateTime? Closes { get; set; }
        public MoneyViewModel CurrentAmount { get; set; }
        public int Bids { get; set; }
    }
}
