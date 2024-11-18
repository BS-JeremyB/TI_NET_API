using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.DAL.Context;
using TI_NET_API.DAL.Interfaces;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.DAL.Repositories
{
    public class UserRepository : IUserRepository

    {
        private readonly FakeDB _db;

        public UserRepository(FakeDB db)
        {
            _db = db;
        }

        public User? Create(User user)
        {
            if(user is not null)
            {
                user.Id = ++_db.IdUserCount;
                _db.Users.Add(user);
                
                return user;
            }

            return null;
        }

        public bool Delete(User user)
        {
            return _db.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User? GetById(int id)
        {
            return _db.Users.SingleOrDefault(x => x.Id == id);
        }

        public User? GetByEmail(string email)
        {
            return _db.Users.SingleOrDefault(x => x.Email == email);
        }

        public User? Patch(User user)
        {
            return user is not null ? user : null;
        }

        public User? Update(User user)
        {
            return user is not null ? user : null;
            
        }
    }
}
