using System;

namespace cqrs.Web.MVC.Models
{
    public class BidViewModel
    {
        public MoneyViewModel Amount { get; protected set; }
        public DateTime Date { get; protected set; }
        public UserViewModel Bidder { get; protected set; }
    }
}
