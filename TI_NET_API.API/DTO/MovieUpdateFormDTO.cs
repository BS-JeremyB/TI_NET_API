using System.ComponentModel.DataAnnotations;

namespace TI_NET_API.API.DTO
{
    public class MovieUpdateFormDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Synopsis { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public DateTime Release { get; set; }
    }
}
