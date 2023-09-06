using Domain.Entities;

namespace Persistence.Abstraction.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers();
        public User GetUserById(int id);
        public User GetUserByName(string name);
        public User UpdateUser(string name, User user);
    }
}
