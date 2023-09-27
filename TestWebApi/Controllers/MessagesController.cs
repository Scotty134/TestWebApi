using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Services;
using TestWebApi.Extensions;

namespace TestWebApi.Controllers
{
    [Authorize]
    public class MessagesController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessage)
        {
            var username = User.GetUserName();
            var user = _userService.GetUserByName(username);

            if (username == createMessage.RecipientUsername.ToLower()) return BadRequest();

            var message = _messageService.AddMessage(user, createMessage);

            return Ok(message);
        }
    }
}
