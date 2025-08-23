using API_Project.Data;
using API_Project.Interfaces;
using API_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Project.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context) { }

        // Override to include related entities
        public override IEnumerable<Booking> GetAll()
        {
            return _dbSet
            .Include(b => b.TourPackage)
            .Include(b => b.User)
                .ThenInclude(u => u.Role)   
            .Include(b => b.Payment)
            .ToList();
        }

        // Override to include related entities
        public override Booking GetById(int id)
        {
            return _dbSet
            .Include(b => b.TourPackage)
            .Include(b => b.User)
                .ThenInclude(u => u.Role)   
            .Include(b => b.Payment)
            .FirstOrDefault(b => b.BookingId == id);
        
        }

        public Dictionary<DateTime, int> GetBookingsCountPerDate()
        {
            return _dbSet
                .GroupBy(b => b.BookingDate.Date)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
