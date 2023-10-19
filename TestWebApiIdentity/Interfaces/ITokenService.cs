using TestWebApiIdentity.Entities;

namespace TestWebApiIdentity.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}