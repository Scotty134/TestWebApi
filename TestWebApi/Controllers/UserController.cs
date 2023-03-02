using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Services;
using System.Security.Claims;
using TestWebApi.Extensions;

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
        public ActionResult<IEnumerable<MemberDto>> GetUsers()
        {            
            var users = _userService.GetUsers();
            return users.ToList();
        }

        [HttpGet("id/{id}")]
        public ActionResult<MemberDto> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            return user;
        }

        [HttpGet("{name}")]
        public ActionResult<MemberDto> GetUserByName(string name)
        {
            var user = _userService.GetUserByName(name);
            return user;
        }

        [HttpPut]
        public ActionResult UpdateUser (MemberUpdateDto model)
        {
            var userName = User.GetUserName();
            if(userName == null) return NotFound();
            try
            {
                _userService.UpdateUser(userName, model);
                return NoContent();
            }
            catch
            {
                return BadRequest("Failed to update the user.");
            }            
        }
    }
}
