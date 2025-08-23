using API_Project.Models;

namespace API_Project.Interface
{
    public interface IUserRepository
    {
        User GetByEmail(string email);
        void Add(User user);
        IEnumerable<User> GetAll();
    }
}
