using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos
{
    public class LoginAccountDto
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}
