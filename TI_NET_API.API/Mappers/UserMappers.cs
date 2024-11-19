using TI_NET_API.API.DTO;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.API.Mappers
{
    public static class UserMappers
    {
        // FormDTO
        public static User ToUser(this UserCreateFormDTO user)
        {
            return new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,

            };
        }

        public static User ToUser(this UserUpdateFormDTO user)
        {
            return new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Role = user.Role,

            };
        }

        public static User ToUser(this UserPatchFormDTO user)
        {
            return new User
            {
                Role = user.Role,

            };
        }

        // ViewDTO
        public static UserViewDTO ToDTO(this User user)
        {
            return new UserViewDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public static UserListViewDTO ToListDTO(this User user)
        {
            return new UserListViewDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
