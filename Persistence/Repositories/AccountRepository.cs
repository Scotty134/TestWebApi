﻿using Domain.Entities;
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

        public User Register(User user, string password)
        {
            if (_context.Users.Any(u => u.UserName.ToLower() == user.UserName.ToLower()))
            {
                return null;
            }

            using var hmac = new HMACSHA512();

            var newUser = new User
            {
                UserName = user.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key,
                City= user.City,
                DateOfBirth= user.DateOfBirth,
                Country= user.Country,
                Gender= user.Gender,
                Name= user.Name
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

    }
}
