using API_Project.Models;
using System;
using System.Collections.Generic;

namespace API_Project.Services
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetAll();
        Booking GetById(int id);
        void Add(Booking booking);
        void Update(Booking booking);
        void Delete(int id);
        int GetUserIdByEmail(string email);

        Dictionary<DateTime, int> GetBookingsCountPerDate();
    }
}
