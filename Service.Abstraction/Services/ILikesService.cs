using Domain.Entities;
using Infrastructure.Dtos;

namespace Service.Abstraction.Services
{
    public interface ILikesService
    {
        public Task<IEnumerable<LikesDto>> GetUserLikes(string predicate, int userId);
        public Task<bool> ToggleLike(int sourceUserId, int targetUserId);
    }
}
