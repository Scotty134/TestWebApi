using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestWebApi.SeedData
{
    public class SeedData
    {
        public static void SeedUsers()
        {
            var context = new DataContext();

            var userData = File.ReadAllText("SeedData/data.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<User>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.PasswordSalt = hmac.Key;
                user.Photos.FirstOrDefault().PublicId = "-";
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
