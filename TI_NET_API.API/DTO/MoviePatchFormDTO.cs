using System.ComponentModel.DataAnnotations;

namespace TI_NET_API.API.DTO
{
    public class MoviePatchFormDTO
    {
        [Required]
        public DateTime Release { get; set; }
    }
}
