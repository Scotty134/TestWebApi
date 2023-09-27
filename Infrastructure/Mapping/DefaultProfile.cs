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

            CreateMap<User, LoginAccountDto>()
                .ReverseMap();

            CreateMap<User, RegisterAccountDto>()
                .ReverseMap();

            CreateMap<User, MemberUpdateDto>()
                .ReverseMap();

            CreateMap<Photo, PhotoDto>()
                .ReverseMap();

            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ReverseMap();
        }
    }
}
