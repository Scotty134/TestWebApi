using Infrastructure.Dtos;
using Infrastructure.Helpers;

namespace Service.Abstraction.Services
{
    public interface IMessageService
    {
        public MessageDto AddMessage(MemberDto user, CreateMessageDto createMessage);
        public Task<PagedList<MessageDto>> GetMessagesForUser();
    }
}
