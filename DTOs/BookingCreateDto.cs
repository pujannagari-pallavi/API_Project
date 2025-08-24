using System.ComponentModel.DataAnnotations;

namespace API_Project.DTOs
{
    public class BookingCreateDto
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfPersons { get; set; }
        public string PaymentMethod { get; set; }

        public TourPackageDto Package { get; set; }
        public UserDto User { get; set; }

        
        public decimal Payment { get; set; }
    }
}

