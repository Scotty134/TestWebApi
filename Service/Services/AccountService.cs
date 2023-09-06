using AutoMapper;
using Domain.Entities;
using Infrastructure.Dtos;
using Infrastructure.Mapping;
using Persistence.Abstraction.Repositories;
using Persistence.Repositories;
using Service.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountService(IAccountRepository accountRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AddProfile<DefaultProfile>();
            });
            _mapper = new Mapper(config);

            //TODO: replace with DI
            _accountRepository = accountRepository;
        }

        public AccountDto Login(LoginAccountDto account)
        {
            var model = _accountRepository.Login(account.Username, account.Password);
            var user = _mapper.Map<AccountDto>(model);
            return user;
        }

        public AccountDto Register(RegisterAccountDto account)
        {
            var model = _mapper.Map<User>(account);
            model = _accountRepository.Register(model, account.Password);
            var user = _mapper.Map<AccountDto>(model);
            return user;
        }
    }
}
