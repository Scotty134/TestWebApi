using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstraction.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Persistence.Repositories
{
    public class AccountRepository: IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context= context;
        }

        public AppUser Login(string userName, string password)
        {
            var user = _context.Users
                .Include(p => p.Photos)
                .SingleOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                return null;
            }

            return user;
        }

        public AppUser Register(AppUser user, string password)
        {
            if (_context.Users.Any(u => u.UserName.ToLower() == user.UserName.ToLower()))
            {
                return null;
            }

            var newUser = new AppUser
            {
                UserName = user.UserName,                
                City= user.City,
                DateOfBirth= user.DateOfBirth,
                Country= user.Country,
                Gender= user.Gender,
                KnownAs = user.KnownAs
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

    }
}
