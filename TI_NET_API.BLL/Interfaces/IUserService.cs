using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.BLL.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetAll();
        public User? GetById(int id);
        public User? Update(int id, User user);
        public User? Patch(int id, User user);
        public bool Delete(int id);
        public User? Create(User user);

        public User? Login(string email, string password);
    }
}
