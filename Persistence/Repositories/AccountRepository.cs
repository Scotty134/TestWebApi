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

        public AccountRepository()
        {
            _context= new DataContext();
        }

        public User Login(string userName, string password)
        {
            var user = _context.Users
                .Include(p => p.Photos)
                .SingleOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                return null;
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            //Check password
            foreach (var item in computedHash.Select((value, index) => (value, index)))
            {
                if (item.value != user.PasswordHash[item.index])
                {
                    return null;
                }
            }
            return user;
        }

        public User Register(string userName, string password)
        {
            if (_context.Users.Any(u => u.UserName.ToLower() == userName.ToLower()))
            {
                return null;
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = userName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

    }
}
