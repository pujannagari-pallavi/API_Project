using API_Project.Interface;
using API_Project.Models;
using System;
using System.Collections.Generic;

namespace API_Project.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        // Custom method only
        Dictionary<DateTime, int> GetBookingsCountPerDate();
    }
}
