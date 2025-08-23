using API_Project.Data;
using API_Project.Interfaces;
using API_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Project.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ApplicationDbContext _context; // ✅ add DbContext

        public BookingService(IBookingRepository bookingRepository, ApplicationDbContext context)
        {
            _bookingRepository = bookingRepository;
            _context = context;
        }

        public IEnumerable<Booking> GetAll() => _bookingRepository.GetAll();
        public Booking GetById(int id) => _bookingRepository.GetById(id);
        public void Add(Booking booking) => _bookingRepository.Add(booking);
        public void Update(Booking booking) => _bookingRepository.Update(booking);
        public void Delete(int id) => _bookingRepository.Delete(id);

        public Dictionary<DateTime, int> GetBookingsCountPerDate() =>
            _bookingRepository.GetBookingsCountPerDate();

        // ✅ Fix: Resolve user by email using DbContext.Users
        public int GetUserIdByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                throw new Exception("User not found for email: " + email);

            return user.UserId;
        }
    }
}
