using API_Project.Data;
using API_Project.Interface;
using API_Project.Models;
using System;
using System.Linq;

namespace API_Project.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Role GetByName(string roleName)
        {
            return _context.Roles.FirstOrDefault(r => r.Name == roleName);
        }
    }
}
