using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace TestWebApiIdentity.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required] public string KnownAs { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public DateTime? DateOfBirth { get; set; } // optional to make required work!
        [Required] public string City { get; set; }
        [Required] public string Country { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}