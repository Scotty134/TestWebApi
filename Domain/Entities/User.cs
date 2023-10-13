using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User: IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }        
        public string? Introduction { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public List<Photo> Photos { get; set; } = new();
        public List<UserLike> LikedByUser { get; set; } = new();
        public List<UserLike> LikedUsers { get; set; } = new();
        public List<Message> MessagesSent { get; set; } = new();
        public List<Message> MessagesReceived { get; set; } = new();
        public ICollection<UserRole> UserRoles { get; set; }

        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.UtcNow;
                var age = today.Year - DateOfBirth.Year;
                if (today.DayOfYear < DateOfBirth.DayOfYear)
                {
                    age--;
                }
                return age;
            }
        }

    }
}
