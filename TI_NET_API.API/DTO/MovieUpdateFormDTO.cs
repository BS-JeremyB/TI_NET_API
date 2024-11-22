using System.ComponentModel.DataAnnotations;

namespace TI_NET_API.API.DTO
{
    public class MovieUpdateFormDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Oh con ! Ca fait beaucoup la non ?")]
        public string Synopsis { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public DateTime Release { get; set; }
    }
}
