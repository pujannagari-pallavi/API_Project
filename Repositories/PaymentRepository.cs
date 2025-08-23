using API_Project.Data;
using API_Project.Interfaces;
using API_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Project.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

        // -----------------------------
        // Helper: Include all related entities
        // -----------------------------
        private IQueryable<Payment> IncludeAllRelations()
        {
            return _dbSet
                .Include(p => p.Booking)
                    .ThenInclude(b => b.TourPackage)
                .Include(p => p.Booking)
                    .ThenInclude(b => b.User);
        }

        // -----------------------------
        // Get Payment by Id
        // -----------------------------
        public Payment GetById(int id)
        {
            return IncludeAllRelations().FirstOrDefault(p => p.PaymentId == id);
        }

        // -----------------------------
        // Get all payments (for admin)
        // -----------------------------
        public override IEnumerable<Payment> GetAll()
        {
            return IncludeAllRelations().ToList();
        }

        // -----------------------------
        // Get payments by status
        // -----------------------------
        public IEnumerable<Payment> GetPaymentsByStatus(string status)
        {
            return IncludeAllRelations()
                   .Where(p => p.Status == status)
                   .ToList();
        }

        // -----------------------------
        // Get payments by date range
        // -----------------------------
        public IEnumerable<Payment> GetPaymentsByDateRange(DateTime startDate, DateTime endDate)
        {
            return IncludeAllRelations()
                   .Where(p => p.PaidOn >= startDate && p.PaidOn <= endDate)
                   .ToList();
        }

        // -----------------------------
        // Get payments by method
        // -----------------------------
        public IEnumerable<Payment> GetPaymentsByMethod(string method)
        {
            return IncludeAllRelations()
                   .Where(p => p.PaymentMethod == method)
                   .ToList();
        }

        // -----------------------------
        // Get total revenue
        // -----------------------------
        public decimal GetTotalRevenue()
        {
            return _dbSet.Sum(p => p.Amount);
        }

        // -----------------------------
        // Get revenue by payment method
        // -----------------------------
        public decimal GetRevenueByMethod(string paymentMethod)
        {
            return _dbSet.Where(p => p.PaymentMethod == paymentMethod)
                         .Sum(p => p.Amount);
        }
    }
}
