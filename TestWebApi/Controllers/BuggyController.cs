using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        public BuggyController()
        {

        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetAuthorized()
        {
            return "you authorized!";
        }

        [HttpGet("not-found")]
        public ActionResult<UserDto> GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("server-error")]
        public ActionResult<UserDto> GetServerError()
        {
            var model = new UserDto();
            model.UserName = null;
            var returnValue = model.UserName.ToString();

            return model;
        }

        [HttpGet("bad-request")]
        public ActionResult<UserDto> GetBadRequest()
        {
            return BadRequest("Bad request message");
        }

        //[HttpPost("seed-data")]
        //public ActionResult<string> PostSeedData()
        //{
        //    SeedData.SeedData.SeedUsers();
        //    return Ok("Done");
        //}
    }
}
