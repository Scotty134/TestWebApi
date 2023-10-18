using TestWebApiIdentity.Entities;

namespace TestWebApiIdentity.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}