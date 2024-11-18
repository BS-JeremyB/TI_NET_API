using System.ComponentModel.DataAnnotations;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.API.DTO
{
    public class UserPatchFormDTO
    {
        [Required]
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
    }
}
