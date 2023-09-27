using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstraction.Repositories;

namespace Persistence.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;

        public LikesRepository()
        {
            _context = new DataContext();
        }

        public async Task<bool> AddLike(int sourceUserId, int targetUserId)
        {
            var userLike = new UserLike 
            { 
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId
            };
            _context.Likes.Add(userLike);
            if(await _context.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, targetUserId);
        }

        public async Task<User> GetUserWithLikes(int userId)
        {
            return await _context.Users
                .Include(u => u.LikedUsers)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> RemoveLike(int sourceUserId, int targetUserId)
        {
            var userLike = await GetUserLike(sourceUserId, targetUserId);
            if (userLike != null)
            {
                _context.Likes.Remove(userLike);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
