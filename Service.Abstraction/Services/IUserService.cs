using Infrastructure.Dtos;

namespace Service.Abstraction.Services
{
    public  interface IUserService
    {
        public MemberDto GetUserById(int id);

        public MemberDto GetUserByName(string name);

        public IEnumerable<MemberDto> GetUsers();

        public MemberDto UpdateUser(string name, MemberUpdateDto user);
    }
}
