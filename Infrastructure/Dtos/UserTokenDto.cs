using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos
{
    public class UserTokenDto
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public string PhotoUrl { get; set; }
    }
}
