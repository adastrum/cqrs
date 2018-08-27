using AutoMapper;
using cqrs.Domain.Entities;

namespace cqrs.Web.MVC.Models.MapperProfiles
{
    public class LotProfile : Profile
    {
        public LotProfile()
        {
            CreateMap<Lot, LotViewModel>();
        }
    }
}
