using API_Project.Models;

namespace API_Project.Interface
{
    public interface IRoleRepository
    {
        Role GetByName(string roleName);
    }
}
