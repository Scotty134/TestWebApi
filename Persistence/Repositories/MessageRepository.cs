using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstraction.Repositories;

namespace Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository()
        {
            _context = new DataContext();
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
            _context.SaveChanges();
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(string currentUserName, string recipientName)
        {
            var messages = await _context.Messages
                .Include(m => m.Sender).ThenInclude(u => u.Photos)
                .Include(m => m.Recipient).ThenInclude(u => u.Photos)
                .Where(
                    m => m.RecipientUsername == currentUserName && m.SenderUsername == recipientName || m.RecipientUsername == recipientName && m.SenderUsername == currentUserName
                )
                .OrderBy(m => m.MessageSent)
                .ToListAsync();
            var unreadMessages = messages.Where(m => m.DateRead == null && m.RecipientUsername == currentUserName).ToList();

            if (unreadMessages.Any())
            {
                foreach(var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
            }

            return messages;
        }
    }
}
