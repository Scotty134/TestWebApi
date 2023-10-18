using TestWebApiIdentity.Entities;
using TestWebApiIdentity.Helpers;
using TestWebApiIdentity.DTOs;

namespace TestWebApiIdentity.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}