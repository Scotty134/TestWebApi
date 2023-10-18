using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using TestWebApiIdentity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TestWebApiIdentity.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            try
            {
                foreach (var user in users)
                {
                    user.UserName = user.UserName.ToLower();

                    var result = await userManager.CreateAsync(user, "password");
                }
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}