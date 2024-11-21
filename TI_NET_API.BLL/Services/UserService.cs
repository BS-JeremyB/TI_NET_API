using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.BLL.Base;
using TI_NET_API.BLL.Exceptions;
using TI_NET_API.BLL.Interfaces;
using TI_NET_API.DAL.Interfaces;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la récupération de la liste : {ex.Message}");
            }
        }

        public User? GetById(int id)
        {
            try
            {
                return _repository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la récupération de l'utilisateur : {ex.Message}");
            }
        }

        public User? Create(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "L'utilisateur ne peut pas être null");
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    throw new ArgumentException("L'email est obligatoire");
                }

                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    throw new ArgumentException("Le mot de passe est obligatoire");
                }

                if (_repository.GetByEmail(user.Email) != null)
                {
                    throw new ArgumentException("Un utilisateur avec cet email existe déjà");
                }

                string passwordHash = Argon2.Hash(user.Password);
                user.Password = passwordHash;

                return _repository.Create(user);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la création de l'utilisateur : {ex.Message}");
            }
        }

        public User? Update(int id, User user)
        {
            try
            {
                User? userToUpdate = _repository.GetById(id);
                if (userToUpdate is null)
                {
                    return null;
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    throw new ArgumentException("L'email est obligatoire");
                }

                User? existingUser = _repository.GetByEmail(user.Email);
                if (existingUser != null && existingUser.Id != id)
                {
                    throw new ArgumentException("Un utilisateur avec cet email existe déjà");
                }

                userToUpdate.Email = user.Email;
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Role = user.Role;

                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    userToUpdate.Password = Argon2.Hash(user.Password);
                }

                return _repository.Update(userToUpdate);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la mise à jour de l'utilisateur : {ex.Message}");
            }
        }

        public User? Patch(int id, User user)
        {
            try
            {
                User? userToPatch = _repository.GetById(id);
                if (userToPatch is null)
                {
                    return null;
                }

                userToPatch.Role = user.Role;

                return _repository.Patch(userToPatch);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la mise à jour partielle de l'utilisateur : {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                User? user = _repository.GetById(id);
                if (user is null)
                {
                    return false;
                }

                return _repository.Delete(user);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la suppression de l'utilisateur : {ex.Message}");
            }
        }

        public User? Login(string email, string password)
        {
            try
            {
                User? user = _repository.GetByEmail(email);
                if (user is not null && Argon2.Verify(user.Password, password))
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la connexion : {ex.Message}");
            }
        }
    }
}
