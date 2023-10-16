using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstraction.Repositories;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<AppUser> GetUsers()
        {
            return _context.Users
                .Include(p => p.Photos)
                .ToList();
        }

        public AppUser GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public AppUser GetUserByName(string name)
        {
            return _context.Users
                .Include(p => p.Photos)
                .SingleOrDefault(u => u.UserName == name);
        }

        public AppUser UpdateUser(string name, AppUser user)
        {
            var model = _context.Users.FirstOrDefault(u => u.UserName == name);
            model.Gender = user.Gender;            
            model.Introduction = user.Introduction;            
            model.City = user.City;
            model.DateOfBirth = model.DateOfBirth;
            model.KnownAs = name;
            model.Country = user.Country;
            model.LastActive = user.LastActive;
            _context.SaveChanges();

            return model;
        }
    }
}
