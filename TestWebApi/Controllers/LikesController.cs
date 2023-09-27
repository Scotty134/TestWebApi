using Infrastructure.Dtos;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Services;
using TestWebApi.Extensions;

namespace TestWebApi.Controllers
{
    [Authorize]
    public class LikesController : BaseController
    {
        private readonly ILikesService _likesService;
        private readonly IUserService _userService;
        
        public LikesController(ILikesService likesService, IUserService userService)
        {
            _likesService = likesService;
            _userService = userService;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> ToggleLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var sourceUserName = User.GetUserName();
            var targetUser = _userService.GetUserByName(username);

            if (sourceUserName == username) return BadRequest("Dont be a narcissist!");
            var result = await _likesService.ToggleLike(sourceUserId, targetUser.Id);
            if (result) return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes([FromQuery] LikesParams likeParams)
        {
            likeParams.UserId = User.GetUserId();
            var users = await _likesService.GetUserLikes(likeParams);
            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));
            return Ok(users);
        }
    }
}
