using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Text.Json;

namespace TestWebApi.SeedData
{
    public class SeedData
    {
        public static async void SeedUsers(UserManager<User> userManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = File.ReadAllText("SeedData/data.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<User>>(userData);

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();                
                user.Photos.FirstOrDefault().PublicId = "-";
                await userManager.CreateAsync(user, "password");
            }
        }
    }
}
