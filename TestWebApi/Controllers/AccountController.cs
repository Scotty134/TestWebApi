using Infrastructure.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Services;
using System.Security.Cryptography;
using System.Text;
using TestWebApi.Interfaces;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService= accountService;
            _tokenService= tokenService;
        }

        [HttpPost("register")]
        public ActionResult<UserTokenDto> Register(LoginAccountDto account)
        {
            var user = _accountService.Register(account);

            if(user == null)
            {
                return BadRequest("User already exist!");
            }

            return new UserTokenDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public ActionResult<UserTokenDto> Login(LoginAccountDto account)
        {
            var user = _accountService.Login(account);

            if (user == null)
            {
                return Unauthorized();
            }

            return new UserTokenDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.PhotoUrl
            }; 
        }
    }
}
