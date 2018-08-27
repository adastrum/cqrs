using AutoMapper;
using cqrs.Domain.Entities;

namespace cqrs.Web.MVC.Models.MapperProfiles
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            CreateMap<Auction, AuctionListItemViewModel>()
                .ForMember(x => x.Bids, x => x.ResolveUsing(y => y.Bids.Count))
                .ForMember(x => x.Closes, x => x.ResolveUsing(y => y.StartDate.Value + y.Duration));
        }
    }
}
