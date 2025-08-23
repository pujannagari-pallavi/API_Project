using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Project.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }   // Primary key

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Prevents truncation issues
        [Range(1, 50000, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }  // Amount paid

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PaidOn { get; set; } // Payment date

        [Required]
        
        public string PaymentMethod { get; set; } // e.g., Credit Card, UPI

        [Required]
       
        public string Status { get; set; }   // Payment status: Pending, Completed, Failed

        //  One-to-one relationship with Booking
        [Required]
        [ForeignKey(nameof(Booking))]
        public int BookingId { get; set; }// Foreign key


        [JsonIgnore]
        public Booking? Booking { get; set; } // Navigation property
    }
}
