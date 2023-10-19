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
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            var roles = new List<AppRole> {
                new AppRole { Name = "Member"},
                new AppRole { Name = "Admin" },
                new AppRole { Name = "Moderator"}
            };

            try
            {
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                var admin = new AppUser
                {
                    UserName = "admin"
                };

                foreach (var user in users)
                {
                    if (user.UserName.ToLower() == "lisa") admin = user;
                    user.UserName = user.UserName.ToLower();
                    var result = await userManager.CreateAsync(user, "password");
                    await userManager.AddToRoleAsync(user, "Member");
                }

                admin.UserName = "admin";

                await userManager.CreateAsync(admin, "password");
                await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
            }
            catch (Exception ex)
            {

            }

        }
    }
}