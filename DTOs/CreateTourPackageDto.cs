using System.ComponentModel.DataAnnotations;

namespace API_Project.DTOs
{
    public class CreateTourPackageDto
    {
        [Required(ErrorMessage = "Package name is required")]
        public string PackName { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Days must be at least 1")]
        public int Days { get; set; }
    }
}
