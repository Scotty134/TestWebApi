using Infrastructure.Dtos;
using Infrastructure.Helpers;
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

        [HttpGet]
        public async Task<ActionResult<PagedList<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUserName();

            var messages = await _messageService.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages));
            return messages;
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUserName();
            var messages = await _messageService.GetMessageThread(currentUsername, username);
            return Ok(messages);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = User.GetUserName();
            var message = await _messageService.GetMessage(id);

            if (message.SenderUsername != username && message.RecipientUsername != username) return Unauthorized();

            if (message.SenderUsername == username) message.SenderDeleted = true;
            if (message.RecipientUsername == username) message.RecipientDeleted = true;
            
            return (_messageService.DeleteMessage(message)) ? Ok() : BadRequest("Problem deleting the message.");
        }
    }
}
