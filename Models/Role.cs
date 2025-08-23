using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_Project.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; } // Primary key

        [Required]
        [StringLength(50, ErrorMessage = "Role name cannot exceed 50 characters.")]
        public string Name { get; set; } // e.g., "Admin", "Customer", "Manager"

        // One-to-many relationship: One role can be assigned to many users
        public ICollection<User>? Users { get; set; }
    }
}
