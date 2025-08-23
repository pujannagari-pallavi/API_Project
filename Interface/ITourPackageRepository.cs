using API_Project.Interface;
using API_Project.Models;
using System.Collections.Generic;

namespace API_Project.Interfaces
{
    // Extends generic repository with TourPackage-specific operations
    public interface ITourPackageRepository : IGenericRepository<TourPackage>
    {
        IEnumerable<TourPackage> Search(string term); // search by PackName or Location
        IEnumerable<TourPackage> FilterByPrice(decimal minPrice, decimal maxPrice); // filter by Price
    }
}
