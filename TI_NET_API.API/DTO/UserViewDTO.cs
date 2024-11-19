using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.API.DTO
{
    // DTO pour un GetById
    public class UserViewDTO 
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public Role Role { get; set; }
    }

    // DTO pour un GetAll
    public class UserListViewDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
