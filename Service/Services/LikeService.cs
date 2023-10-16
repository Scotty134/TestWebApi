using AutoMapper;
using Infrastructure.Dtos;
using Infrastructure.Helpers;
using Infrastructure.Mapping;
using Persistence;
using Persistence.Abstraction.Repositories;
using Service.Abstraction.Services;

namespace Service.Services
{
    public class LikeService: ILikesService
    {
        private readonly ILikesRepository _likesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public LikeService(DataContext context, ILikesRepository likesRepository, IUserRepository userRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AddProfile<DefaultProfile>();
            });
            _mapper = new Mapper(config);
            _context = context;
            _likesRepository = likesRepository;
            _userRepository = userRepository;
        }

        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if (likesParams.Predicate == "liked")
            {
                likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
                users = likes.Select(like => like.TargetUser);
            }

            if (likesParams.Predicate == "likedBy")
            {
                likes = likes.Where(like => like.TargetUserId == likesParams.UserId);
                users = likes.Select(like => like.SourceUser);
            }

            var models = users.Select(user => new LikeDto { 
                UserName = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.Age,
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                City = user.City,
                Id = user.Id
            });
            return await PagedList<LikeDto>.CreateAsync(models, likesParams.PageNumber, likesParams.PageSize);
        }

        public async Task<bool> ToggleLike(int sourceUserId, int targetUserId)
        {
            var sourceUser = await _likesRepository.GetUserWithLikes(sourceUserId);
            var targetUser = _userRepository.GetUserById(targetUserId);
            if (targetUser == null) return false;
            var userLike = await _likesRepository.GetUserLike(sourceUserId, targetUserId);
            if(userLike != null)
            {
               return await _likesRepository.RemoveLike(sourceUserId, targetUserId);              
            }
            else
            {
                return await _likesRepository.AddLike(sourceUserId, targetUserId);
            }           
        }
    }
}
