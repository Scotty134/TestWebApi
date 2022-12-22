using AutoMapper;
using Infrastructure.Dtos;
using Infrastructure.Mapping;
using Persistence.Abstraction.Repositories;
using Persistence.Repositories;
using Service.Abstraction.Services;

namespace Service.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository) 
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AddProfile<DefaultProfile>();
            });
            _mapper = new Mapper(config);

            //TODO: replace with DI
            _userRepository = userRepository;
        }

        public UserDto GetUserById(int id)
        {
            var model = _userRepository.GetUserById(id);
            var user = _mapper.Map<UserDto>(model);
            return user;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var models = _userRepository.GetUsers();
            var users = _mapper.Map<IEnumerable<UserDto>>(models);
            return users;
        }
    }
}
