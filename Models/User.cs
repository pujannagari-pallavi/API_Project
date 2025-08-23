using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Project.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        
        public string Email { get; set; }

        [Required]
        
        public string PasswordHash { get; set; } // store hashed password

        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; } // Foreign key for Role

        public Role Role { get; set; } // Navigation property

        // One-to-many: One User can have many Bookings
        public ICollection<Booking>? Bookings { get; set; }
    }
}
