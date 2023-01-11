using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Services;

namespace TestWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {            
            var users = _userService.GetUsers();
            return users.ToList();
        }
                
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUsers(int id)
        {
            var user = _userService.GetUserById(id);
            return user;
        }
    }
}
