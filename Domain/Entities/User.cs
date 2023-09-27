using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]{3,28}$") ]
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Country { get; set; }        
        public string? Introduction { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public List<Photo> Photos { get; set; } = new();
        public List<UserLike> LikedByUser { get; set; } = new();
        public List<UserLike> LikedUsers { get; set; } = new();
        public List<Message> MessagesSent { get; set; } = new();
        public List<Message> MessagesReceived { get; set; } = new();

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
