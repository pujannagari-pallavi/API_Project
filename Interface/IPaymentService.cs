using API_Project.Models;
using System;
using System.Collections.Generic;

namespace API_Project.Interfaces
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetAll();
        Payment GetById(int id);
        void Add(Payment payment);
        void Update(Payment payment);
        void Delete(int id);

        // Extra methods
        IEnumerable<Payment> GetPaymentsByStatus(string status);
        IEnumerable<Payment> GetPaymentsByDateRange(DateTime startDate, DateTime endDate);
        decimal GetTotalRevenue();
        decimal GetRevenueByMethod(string paymentMethod);
        IEnumerable<Payment> GetPaymentsByMethod(string method);
    }
}
