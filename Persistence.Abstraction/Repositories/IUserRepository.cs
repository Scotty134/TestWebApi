using Domain.Entities;

namespace Persistence.Abstraction.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<AppUser> GetUsers();
        public AppUser GetUserById(int id);
        public AppUser GetUserByName(string name);
        public AppUser UpdateUser(string name, AppUser user);
    }
}
