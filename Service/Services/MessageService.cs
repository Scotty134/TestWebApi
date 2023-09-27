using AutoMapper;
using Domain.Entities;
using Infrastructure.Dtos;
using Infrastructure.Helpers;
using Infrastructure.Mapping;
using Persistence.Abstraction.Repositories;
using Service.Abstraction.Services;

namespace Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AddProfile<DefaultProfile>();
            });
            _mapper = new Mapper(config);
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public MessageDto AddMessage(MemberDto user, CreateMessageDto createMessage)
        {
            var sender = _userRepository.GetUserByName(user.UserName);
            var recipient = _userRepository.GetUserByName(createMessage.RecipientUsername);

            var message = new Message
            {
                SenderId = sender.Id,
                RecipientId = recipient.Id,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessage.Content
            };

            _messageRepository.AddMessage(message);
            message.Sender = sender;
            message.Recipient = recipient;
            var dto = _mapper.Map<MessageDto>(message);
            return dto;
        }

        public Task<PagedList<MessageDto>> GetMessagesForUser()
        {
            throw new NotImplementedException();
        }
    }
}
