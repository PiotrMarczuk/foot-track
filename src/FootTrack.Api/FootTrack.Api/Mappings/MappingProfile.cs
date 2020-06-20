using AutoMapper;

using FootTrack.Api.Models;
using FootTrack.Api.ViewModels;

namespace FootTrack.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<UserRegisterViewModel, User>();
            CreateMap<User, UserRegisterViewModel>();
        }
    }
}
