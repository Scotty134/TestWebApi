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
        public AppUser Register(AppUser user, string password);
        public AppUser Login(string userName, string password);
    }
}
