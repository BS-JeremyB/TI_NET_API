using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        public IEnumerable<User> GetAll();
        public User? GetById(int id);
        public User? GetByEmail(string email);
        public User? Update(User user);
        public User? Patch(User user);
        public bool Delete(User user);
        public User? Create(User user);
    }
}
