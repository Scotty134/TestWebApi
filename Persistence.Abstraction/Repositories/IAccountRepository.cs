using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Abstraction.Repositories
{
    public interface IAccountRepository
    {
        public User Register(string userName, string password);
        public User Login(string userName, string password);
    }
}
