using API_Project.Models;

namespace API_Project.Interface
{
    public interface ITourPackageService
    {
        IEnumerable<TourPackage> GetAll();
        TourPackage GetById(int id);
        void Create(TourPackage tp);
        void Update(TourPackage tp);
        void Delete(int id);

        // TourPackage-specific operations
        IEnumerable<TourPackage> Search(string term);
        IEnumerable<TourPackage> FilterByPrice(decimal minPrice, decimal maxPrice);
    }
}
