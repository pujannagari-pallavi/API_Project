using API_Project.Data;
using API_Project.Interfaces;
using API_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Project.Repositories
{
    public class TourPackageRepository : GenericRepository<TourPackage>, ITourPackageRepository
    {
        public TourPackageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<TourPackage> Search(string term)
        {
            return _dbSet
                .Where(tp => tp.PackName.Contains(term) || tp.Location.Contains(term))
                .ToList();
        }

        public IEnumerable<TourPackage> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            return _dbSet
                .Where(tp => tp.Price >= minPrice && tp.Price <= maxPrice)
                .ToList();
        }
    }
}
