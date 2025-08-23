using API_Project.Interface;
using API_Project.Interfaces;
using API_Project.Models;

namespace API_Project.Service
{
    public class TourPackageService : ITourPackageService
    {
        private readonly ITourPackageRepository _repo;

        public TourPackageService(ITourPackageRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<TourPackage> GetAll() => _repo.GetAll();
        public TourPackage GetById(int id) => _repo.GetById(id);
        public void Create(TourPackage tp) => _repo.Add(tp);
        public void Update(TourPackage tp) => _repo.Update(tp);
        public void Delete(int id) => _repo.Delete(id);

        public IEnumerable<TourPackage> Search(string term) => _repo.Search(term);
        public IEnumerable<TourPackage> FilterByPrice(decimal minPrice, decimal maxPrice) =>
            _repo.FilterByPrice(minPrice, maxPrice);
    }
}
