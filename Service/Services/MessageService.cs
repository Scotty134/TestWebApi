using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Dtos;
using Infrastructure.Helpers;
using Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Abstraction.Repositories;
using Service.Abstraction.Services;

namespace Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly DataContext _context;

        public MessageService(DataContext context, IMessageRepository messageRepository, IUserRepository userRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AddProfile<DefaultProfile>();
            });
            _mapper = new Mapper(config);
            _context = context;
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

        public bool DeleteMessage(MessageDto message)
        {
            var model = _context.Messages.FirstOrDefault(m => m.Id == message.Id);
            if (model == null) return false;

            if (message.SenderDeleted && message.RecipientDeleted)
            {
                _context.Messages.Remove(model);
            }
            else
            {
                model.SenderDeleted = message.SenderDeleted;
                model.RecipientDeleted = message.RecipientDeleted;
                _context.Messages.Update(model);
            }
            if (_context.SaveChanges() > 0) return true;
            return false;
        }

        public async Task<MessageDto> GetMessage(int id)
        {
            var message = await _messageRepository.GetMessage(id);
            var dto = _mapper.Map<MessageDto>(message);
            return dto;
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages
                .OrderByDescending(m => m.MessageSent)
                .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.RecipientUsername == messageParams.Username && !u.RecipientDeleted),
                "Outbox" => query.Where(u => u.SenderUsername == messageParams.Username && !u.SenderDeleted),
                _ => query.Where(u => u.RecipientUsername == messageParams.Username && !u.RecipientDeleted && u.DateRead == null)
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);//_mapper.Map<IQueryable<MessageDto>>(query);
            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientName)
        {
            var messages = await _context.Messages
                .Include(m => m.Sender).ThenInclude(u => u.Photos)
                .Include(m => m.Recipient).ThenInclude(u => u.Photos)
                .Where(
                    m => m.RecipientUsername == currentUserName && 
                    !m.RecipientDeleted &&
                    m.SenderUsername == recipientName || 
                    m.RecipientUsername == recipientName && 
                    !m.SenderDeleted &&
                    m.SenderUsername == currentUserName
                )
                .OrderBy(m => m.MessageSent)
                .ToListAsync();
            var unreadMessages = messages.Where(m => m.DateRead == null && m.RecipientUsername == currentUserName).ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }
    }
}
