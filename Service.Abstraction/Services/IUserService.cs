using Infrastructure.Dtos;
using Infrastructure.Helpers;

namespace Service.Abstraction.Services
{
    public  interface IUserService
    {
        public MemberDto GetUserById(int id);
        public MemberDto GetUserByName(string name);
        public IEnumerable<MemberDto> GetUsers();
        public Task<PagedList<MemberDto>> GetUsersAsync(UserParams userParams);
        public MemberDto UpdateUser(string name, MemberUpdateDto user);
        public MemberDto UpdateUser(string name, MemberDto user);
    }
}
