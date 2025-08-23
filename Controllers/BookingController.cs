using API_Project.DTOs;
using API_Project.Models;
using API_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require authentication by default
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // -----------------------------
        // GET all bookings (authenticated users)
        // -----------------------------
        [HttpGet]
        public ActionResult<IEnumerable<BookingDto>> GetAll()
        {
            var bookings = _bookingService.GetAll();
            var dtoList = bookings.Select(MapBookingToDto);
            return Ok(dtoList);
        }

        // -----------------------------
        // GET booking by Id (Admin only)
        // -----------------------------
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<BookingDto> GetById(int id)
        {
            var booking = _bookingService.GetById(id);
            if (booking == null) return NotFound();
            return Ok(MapBookingToDto(booking));
        }

        // -----------------------------
        // POST add booking (authenticated users)
        // -----------------------------
        [HttpPost]
        public ActionResult<BookingDto> Add([FromBody] BookingCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ✅ Map DTO -> Booking entity
            var booking = new Booking
            {
                BookingDate = dto.BookingDate == default ? DateTime.Now : dto.BookingDate,
                NumberOfPersons = dto.NumberOfPersons,

                // FK values resolved from nested objects
                UserId = _bookingService.GetUserIdByEmail(dto.User.Email),
                TourPackageId = dto.Package.TourPackageId,

            
                Payment = new Payment
                {
                    Amount = dto.Payment,
                    Status = "Pending",
                    PaymentMethod = dto.PaymentMethod   // ✅ required column
                }
            };

            _bookingService.Add(booking);

            // Reload from DB to include User & Package
            var createdBooking = _bookingService.GetById(booking.BookingId);

            return CreatedAtAction(nameof(GetById),
                new { id = booking.BookingId },
                MapBookingToDto(createdBooking));
        }

        // -----------------------------
        // PUT update booking (Admin only)
        // -----------------------------
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] Booking booking)
        {
            var existing = _bookingService.GetById(id);
            if (existing == null) return NotFound();

            existing.BookingDate = booking.BookingDate;
            existing.NumberOfPersons = booking.NumberOfPersons;
            existing.TourPackageId = booking.TourPackageId;
            existing.UserId = booking.UserId;

            _bookingService.Update(existing);
            return NoContent();
        }

        // -----------------------------
        // DELETE booking (Admin only)
        // -----------------------------
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var booking = _bookingService.GetById(id);
            if (booking == null) return NotFound();

            _bookingService.Delete(id);
            return NoContent();
        }

        // -----------------------------
        // GET bookings count per date (authenticated users)
        // -----------------------------
        [HttpGet("CountPerDate")]
        public ActionResult<Dictionary<DateTime, int>> GetBookingsCountPerDate() =>
            Ok(_bookingService.GetBookingsCountPerDate());

        // -----------------------------
        // Helper: Map Booking to DTO
        // -----------------------------
        private BookingDto MapBookingToDto(Booking booking)
        {
            return new BookingDto
            {
                BookingId = booking.BookingId,
                BookingDate = booking.BookingDate,
                NumberOfPersons = booking.NumberOfPersons,
                User = booking.User != null ? new UserDto
                {
                    UserName = booking.User.UserName,
                    Email = booking.User.Email
                } : null,
                Package = booking.TourPackage != null ? new TourPackageDto
                {
                    TourPackageId = booking.TourPackage.TourPackageId,
                    PackName = booking.TourPackage.PackName,
                    Location = booking.TourPackage.Location,
                    Price = booking.TourPackage.Price,
                    Days = booking.TourPackage.Days
                } : null
            };
        }
    }
}
