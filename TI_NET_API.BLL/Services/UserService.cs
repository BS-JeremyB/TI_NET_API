using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.BLL.Interfaces;
using TI_NET_API.DAL.Interfaces;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _auth;

        public UserService(IUserRepository repository, IAuthService auth)
        {
            _repository = repository;
            _auth = auth;
        }

        public User? Create(User user)
        {
            string passwordHash = Argon2.Hash(user.Password);
            user.Password = passwordHash;
            return _repository.Create(user);
        }

        public bool Delete(int id)
        {
            User? user = _repository.GetById(id);
            if(user is not null)
            {
                return _repository.Delete(user);
            }

            return false;
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public User? GetById(int id)
        {
            return _repository.GetById(id);
        }


        public User? Patch(int id, User user)
        {
            User? userToPatch = _repository.GetById(id);
            if(userToPatch is not null)
            {
                userToPatch.Role = user.Role;
                return _repository.Patch(userToPatch);
            }
            return null;
        }

        public User? Update(int id, User user)
        {
            User? userToUpdate = _repository.GetById(id);
            if (userToUpdate is not null)
            {
                userToUpdate.Role = user.Role;
                userToUpdate.Email = user.Email;
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Password = Argon2.Hash(user.Password);
                return _repository.Update(userToUpdate);
            }
            return null;
        }

        public string? Login(string email, string password)
        {
            User? user = _repository.GetByEmail(email);
            if (user is not null && Argon2.Verify(user.Password, password))
            {
                return _auth.GenerateToken(user);

            }

            return null;

        }
    }
}
