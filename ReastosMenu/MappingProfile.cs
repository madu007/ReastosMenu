using AutoMapper;
using ReastosMenu.Entities.Identity;
using ReastosMenu.Models;

namespace ReastosMenu
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserRegistationModel>().ForMember(u => u.Email, opt => opt.MapFrom(x => x.Email)
            ).ForMember(u => u.UserName, opt => opt.MapFrom(x => x.UserName))
            .ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.FirstName))
            .ForMember(u => u.LastName, opt => opt.MapFrom(x => x.LastName));


            CreateMap<UserRegistationModel, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(u => u.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(u => u.LastName, opt => opt.MapFrom(x => x.LastName))

                ;
        }
    }
}
