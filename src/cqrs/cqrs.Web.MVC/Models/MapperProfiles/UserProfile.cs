using AutoMapper;
using cqrs.Domain.Entities;

namespace cqrs.Web.MVC.Models.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
