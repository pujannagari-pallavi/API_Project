namespace API_Project.DTOs
{
    public class TourPackageDto
    {
        public int TourPackageId { get; set; } // ← Add this
        public string PackName { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int Days { get; set; }
    }
}
