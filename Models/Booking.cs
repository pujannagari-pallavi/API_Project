using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Project.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; }

        [Required]
        
       
        public int NumberOfPersons { get; set; }

        //  Relationship with TourPackage
        [Required]
        [ForeignKey(nameof(TourPackage))]
        public int TourPackageId { get; set; }
        public TourPackage TourPackage { get; set; }

        //  Relationship with User
        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        //  One-to-one relationship with Payment
        public Payment Payment { get; set; }  // Navigation property
    }
}

