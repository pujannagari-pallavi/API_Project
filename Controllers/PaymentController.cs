using API_Project.DTOs;
using API_Project.Interfaces;
using API_Project.Models;
using API_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require authentication by default
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IBookingService _bookingService;

        public PaymentController(IPaymentService paymentService, IBookingService bookingService)
        {
            _paymentService = paymentService;
            _bookingService = bookingService;
        }

        // -----------------------------
        // Add Payment (authenticated users)
        // -----------------------------
        [HttpPost]
        public IActionResult Add([FromBody] AddPaymentDto dto)
        {
            var booking = _bookingService.GetById(dto.BookingId);
            if (booking == null)
                return BadRequest("Booking does not exist");

            if (booking.TourPackage == null)
                return BadRequest("Associated tour package not found");

            var payment = new Payment
            {
                BookingId = booking.BookingId,
                Amount = booking.NumberOfPersons * booking.TourPackage.Price,
                PaidOn = DateTime.Now,
                PaymentMethod = dto.PaymentMethod,
                Status = "Pending"
            };

            _paymentService.Add(payment);

            return Ok(MapPaymentToDto(payment, booking));
        }

        // -----------------------------
        // Get Payment by Id (Admin only)
        // -----------------------------
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            var payment = _paymentService.GetById(id);
            if (payment == null) return NotFound();
            if (payment.Booking == null) return BadRequest("Booking not found for this payment");

            return Ok(MapPaymentToDto(payment, payment.Booking));
        }

        // -----------------------------
        // Get All Payments (Admin only)
        // -----------------------------
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            var payments = _paymentService.GetAll()
                .Where(p => p.Booking != null)
                .Select(p => MapPaymentToDto(p, p.Booking))
                .ToList();

            return Ok(payments);
        }

        // -----------------------------
        // Update Payment Status (Admin only)
        // -----------------------------
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateStatus(int id, [FromBody] UpdatePaymentStatusDto dto)
        {
            var payment = _paymentService.GetById(id);
            if (payment == null) return NotFound();

            var allowedStatuses = new[] { "Pending", "Completed", "Failed" };
            if (!allowedStatuses.Contains(dto.Status))
                return BadRequest("Invalid status value");

            payment.Status = dto.Status;
            _paymentService.Update(payment);

            return Ok(new { payment.PaymentId, payment.Status });
        }

        // -----------------------------
        // Delete Payment (Admin only)
        // -----------------------------
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var payment = _paymentService.GetById(id);
            if (payment == null) return NotFound();
            if (payment.Status == "Completed")
                return BadRequest("Cannot delete a completed payment");

            _paymentService.Delete(id);
            return NoContent();
        }

        // -----------------------------
        // Helper: Map Payment + Booking to DTO
        // -----------------------------
        private PaymentResponseDto MapPaymentToDto(Payment payment, Booking booking)
        {
            return new PaymentResponseDto
            {
                PaymentId = payment.PaymentId,
                Amount = payment.Amount,
                PaidOn = payment.PaidOn,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status,
                Booking = new BookingDto
                {
                    BookingId = booking.BookingId,
                    BookingDate = booking.BookingDate,
                    NumberOfPersons = booking.NumberOfPersons,
                    Package = booking.TourPackage != null ? new TourPackageDto
                    {
                        TourPackageId = booking.TourPackage.TourPackageId,
                        PackName = booking.TourPackage.PackName,
                        Location = booking.TourPackage.Location,
                        Price = booking.TourPackage.Price,
                        Days = booking.TourPackage.Days
                    } : null,
                    User = booking.User != null ? new UserDto
                    {
                        UserName = booking.User.UserName,
                        Email = booking.User.Email
                    } : null
                }
            };
        }
    }
}
