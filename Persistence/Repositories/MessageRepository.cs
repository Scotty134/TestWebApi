using Domain.Entities;
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

        public Task<IEnumerable<Message>> GetMessageThread(int currentUserId, int recipientId)
        {
            throw new NotImplementedException();
        }
    }
}
