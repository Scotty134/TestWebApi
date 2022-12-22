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

            CreateMap<User, UserDto>().
                ReverseMap()
                ;
            CreateMap<User, AccountDto>().
                ReverseMap()
                ;
        }
    }
}
