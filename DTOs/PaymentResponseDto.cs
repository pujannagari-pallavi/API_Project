namespace API_Project.DTOs
{
    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidOn { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public BookingDto Booking { get; set; }
    }
}
