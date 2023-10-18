using Microsoft.AspNetCore.Identity;

namespace TestWebApiIdentity.Entities
{
    public class AppRole: IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }


    }
}
