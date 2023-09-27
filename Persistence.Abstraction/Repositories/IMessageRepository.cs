﻿using Domain.Entities;

namespace Persistence.Abstraction.Repositories
{
    public interface IMessageRepository
    {
        public void AddMessage(Message message);
        public void DeleteMessage(Message message);
        public Task<Message> GetMessage(int id);     
        public Task<IEnumerable<Message>> GetMessageThread(int currentUserId, int recipientId);
    }
}
