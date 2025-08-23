namespace API_Project.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfPersons { get; set; }
        public TourPackageDto Package { get; set; }
        public UserDto User { get; set; }
    }
}

