using API_Project.Interfaces;
using API_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace API_Project.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public IEnumerable<Payment> GetAll() => _paymentRepository.GetAll();

        public Payment GetById(int id) => _paymentRepository.GetById(id);

        public void Add(Payment payment) => _paymentRepository.Add(payment);

        public void Update(Payment payment) => _paymentRepository.Update(payment);

        public void Delete(int id) => _paymentRepository.Delete(id);

        public IEnumerable<Payment> GetPaymentsByStatus(string status) =>
            _paymentRepository.GetPaymentsByStatus(status);

        public IEnumerable<Payment> GetPaymentsByDateRange(DateTime startDate, DateTime endDate) =>
            _paymentRepository.GetPaymentsByDateRange(startDate, endDate);

        public decimal GetTotalRevenue() => _paymentRepository.GetTotalRevenue();

        public decimal GetRevenueByMethod(string paymentMethod) =>
            _paymentRepository.GetRevenueByMethod(paymentMethod);
        public IEnumerable<Payment> GetPaymentsByMethod(string method) =>
            _paymentRepository.GetPaymentsByMethod(method);
    }
}
