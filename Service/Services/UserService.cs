using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Dtos;
using Infrastructure.Helpers;
using Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Abstraction.Repositories;
using Service.Abstraction.Services;

namespace Service.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserService(IUserRepository userRepository) 
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AddProfile<DefaultProfile>();
            });
            _mapper = new Mapper(config);
            _context = new DataContext();

            //TODO: replace with DI
            _userRepository = userRepository;
        }

        public MemberDto GetUserById(int id)
        {
            var model = _userRepository.GetUserById(id);
            var user = _mapper.Map<MemberDto>(model);
            return user;
        }

        public MemberDto GetUserByName(string name)
        {
            var model = _userRepository.GetUserByName(name);
            var user = _mapper.Map<MemberDto>(model);
            return user;
        }

        public IEnumerable<MemberDto> GetUsers()
        {
            var models = _userRepository.GetUsers();
            var users = _mapper.Map<IEnumerable<MemberDto>>(models);
            return users;
        }

        public async Task<PagedList<MemberDto>> GetUsersAsync(UserParams userParams)
        {
            //var query = _context.Users.AsQueryable();

            //query = query.Where(u => u.UserName != userParams.CurrentUsername);
            //query = query.Where(u => u.Gender == userParams.Gender);

            //var minDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MaxAge - 1));
            //var maxDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MinAge));

            //query = query.Where(u => u.DateOfBirth >= minDob.ToDateTime(new TimeOnly()) 
            //            && u.DateOfBirth <= maxDob.ToDateTime(new TimeOnly()));

            //query = userParams.OrderBy switch
            //{
            //    "created" => query.OrderByDescending(u => u.Created),
            //    _ => query.OrderByDescending(u => u.LastActive)
            //};

            //return await PagedList<MemberDto>.CreateAsync(
            //    query.AsNoTracking().ProjectTo<MemberDto>(_mapper.ConfigurationProvider),
            //    userParams.PageNumber,
            //    userParams.PageSize);
            var query = _context.Users
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<MemberDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);

        }

        public MemberDto UpdateUser(string name, MemberUpdateDto user)
        {
            var model = _mapper.Map<User>(user);
            model = _userRepository.UpdateUser(name, model);
            var userModel = _mapper.Map<MemberDto>(model);
            return userModel;
        }
    }
}
