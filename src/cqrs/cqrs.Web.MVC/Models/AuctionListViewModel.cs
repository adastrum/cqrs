using System.Collections.Generic;

namespace cqrs.Web.MVC.Models
{
    public class AuctionListViewModel
    {
        public IEnumerable<AuctionListItemViewModel> Auctions { get; set; }

        public AuctionListViewModel(IEnumerable<AuctionListItemViewModel> auctions)
        {
            Auctions = auctions;
        }
    }
}
