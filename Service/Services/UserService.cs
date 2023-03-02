using AutoMapper;
using Domain.Entities;
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

        public MemberDto UpdateUser(string name, MemberUpdateDto user)
        {
            var model = _mapper.Map<User>(user);
            model = _userRepository.UpdateUser(name, model);
            var userModel = _mapper.Map<MemberDto>(model);
            return userModel;
        }
    }
}
