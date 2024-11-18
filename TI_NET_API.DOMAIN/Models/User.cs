using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI_NET_API.DOMAIN.Models
{
    public class User
    {
        public User()
        {
            
        }
        public User(int id, string lastName, string firstName, string email, string password, Role role)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Password = password;
            Role = role;
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; } = Role.User;

    }

    public enum Role
    {
        Admin,
        Moderator,
        User
    }
}
