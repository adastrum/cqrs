using cqrs.Domain.Entities;
using cqrs.Messaging.Interfaces;

namespace cqrs.CommandStack.Commands
{
    public class CreateAuctionCommand : ICommand
    {
        public string Name { get; }
        public string Description { get; }
        public int Days { get; }
        public int Hours { get; }
        public int Minutes { get; }
        public decimal InitialAmount { get; }
        public User Seller { get; }

        public CreateAuctionCommand(string name, string description, int days, int hours, int minutes, decimal initialAmount, User seller)
        {
            Name = name;
            Description = description;
            Days = days;
            Hours = hours;
            Minutes = minutes;
            InitialAmount = initialAmount;
            Seller = seller;
        }
    }
}
