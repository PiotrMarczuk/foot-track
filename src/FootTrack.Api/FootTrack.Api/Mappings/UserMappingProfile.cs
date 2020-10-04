using AutoMapper;
using FootTrack.Api.Dtos.Responses;
using FootTrack.BusinessLogic.Models.User;

namespace FootTrack.Api.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AuthenticatedUser, AuthenticatedUserDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email.Value));
            CreateMap<UserData, UserDto>()
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email.Value))
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id.Value));
        }
    }
}