using AutoMapper;
using Domain.Entities;
using Infrastructure.Dtos;

namespace Infrastructure.Mapping
{
    public class DefaultProfile: Profile
    {
        public DefaultProfile()
        {
            AllowNullCollections= true;

            CreateMap<User, UserDto>()
                .ReverseMap();
            
            CreateMap<User, AccountDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ReverseMap();

            CreateMap<User, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ReverseMap();

            CreateMap<User, MemberUpdateDto>()
                .ReverseMap();

            CreateMap<Photo, PhotoDto>()
                .ReverseMap();
        }
    }
}
