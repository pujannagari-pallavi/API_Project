using API_Project.Interface;
using API_Project.Models;
using System;
using System.Collections.Generic;

namespace API_Project.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        IEnumerable<Payment> GetPaymentsByStatus(string status);
        IEnumerable<Payment> GetPaymentsByDateRange(DateTime startDate, DateTime endDate);
        decimal GetTotalRevenue();
        decimal GetRevenueByMethod(string paymentMethod);
        IEnumerable<Payment> GetPaymentsByMethod(string method);
    }
}
