using Infrastructure.Dtos;
using Infrastructure.Helpers;

namespace Service.Abstraction.Services
{
    public interface ILikesService
    {
        public Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
        public Task<bool> ToggleLike(int sourceUserId, int targetUserId);
    }
}
