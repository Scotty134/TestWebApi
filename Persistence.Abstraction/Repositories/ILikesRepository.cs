using Domain.Entities;

namespace Persistence.Abstraction.Repositories
{
    public interface ILikesRepository
    {
        public Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
        public Task<User> GetUserWithLikes(int userId);
        public Task<bool> AddLike(int sourceUserId, int targetUserId);
        public Task<bool> RemoveLike(int sourceUserId, int targetUserId);
    }
}
