using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.BLL.Interfaces
{
    public interface IAuthService
    {
        public string GenerateToken(User user);
    }
}
