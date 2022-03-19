using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
