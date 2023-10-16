using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace TestWebApi.SeedData
{
    public class SeedData
    {
        public static async void SeedUsers(UserManager<AppUser> userManager)
        {
            //if (await userManager.Users.AnyAsync()) return;

            var userData = File.ReadAllText("SeedData/data.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            try
            {
                foreach (var user in users)
                {
                    user.UserName = user.UserName.ToLower();
                    user.Photos.FirstOrDefault().PublicId = "-";
                    var result = await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}
