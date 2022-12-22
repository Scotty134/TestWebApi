using Infrastructure.Dtos;

namespace TestWebApi.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AccountDto user);
    }
}
