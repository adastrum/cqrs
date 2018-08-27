using AutoMapper;
using cqrs.Domain.ValueObjects;

namespace cqrs.Web.MVC.Models.MapperProfiles
{
    public class MoneyProfile : Profile
    {
        public MoneyProfile()
        {
            CreateMap<Money, MoneyViewModel>()
                .ForMember(x => x.CurrencyCode, x => x.ResolveUsing(y => y.Currency.ToString()));
        }
    }
}
