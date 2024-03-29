﻿using Infrastructure.Dtos;
using Infrastructure.Helpers;

namespace Service.Abstraction.Services
{
    public interface IMessageService
    {
        public MessageDto AddMessage(MemberDto user, CreateMessageDto createMessage);
        public Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
        public Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientName);
        public Task<MessageDto> GetMessage(int id);
        public bool DeleteMessage(MessageDto message);
    }
}
