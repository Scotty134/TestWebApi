using Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Services
{
    public interface IAccountService
    {
        public AccountDto Register(LoginAccountDto account);
        public AccountDto Login(LoginAccountDto account);
    }
}
