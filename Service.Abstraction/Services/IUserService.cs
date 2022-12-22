using Infrastructure.Dtos;

namespace Service.Abstraction.Services
{
    public  interface IUserService
    {
        public UserDto GetUserById(int id);

        public IEnumerable<UserDto> GetUsers();
    }
}
