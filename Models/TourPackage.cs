using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Project.Models
{
    public class TourPackage
    {
        [Key]
        public int TourPackageId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Package name cannot exceed 100 characters.")]
        public string PackName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string Location { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Prevents truncation of price
        [Range(100, 100000, ErrorMessage = "Price must be between 100 and 100000.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 60, ErrorMessage = "Days must be between 1 and 60.")]
        public int Days { get; set; }

        // One-to-many: One TourPackage can have many Bookings
        public ICollection<Booking>? Bookings { get; set; }
    }
}
