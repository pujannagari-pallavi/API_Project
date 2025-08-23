using API_Project.DTOs;
using API_Project.Interface;
using API_Project.Interfaces;
using API_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require authentication by default
    public class TourPackageController : ControllerBase
    {
        private readonly IGenericRepository<TourPackage> _repo;
        private readonly ITourPackageRepository _customRepo;

        public TourPackageController(
            IGenericRepository<TourPackage> repo,
            ITourPackageRepository customRepo)
        {
            _repo = repo;
            _customRepo = customRepo;
        }

        // ---------- CRUD ----------

        [HttpGet]
        [AllowAnonymous] // Publicly view all packages
        public ActionResult<IEnumerable<TourPackageDto>> GetAll()
        {
            var packages = _repo.GetAll()
                .Select(p => new TourPackageDto
                {
                    TourPackageId = p.TourPackageId,
                    PackName = p.PackName,
                    Location = p.Location,
                    Price = p.Price,
                    Days = p.Days
                }).ToList();

            return Ok(packages);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // Publicly view a single package
        public ActionResult<TourPackageDto> GetById(int id)
        {
            var p = _repo.GetById(id);
            if (p == null)
                return NotFound(new { Message = $"Tour package with ID {id} not found." });

            var dto = new TourPackageDto
            {
                TourPackageId = p.TourPackageId,
                PackName = p.PackName,
                Location = p.Location,
                Price = p.Price,
                Days = p.Days
            };

            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Only Admin can add
        public ActionResult<TourPackageDto> Add([FromBody] CreateTourPackageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new TourPackage
            {
                PackName = dto.PackName,
                Location = dto.Location,
                Price = dto.Price,
                Days = dto.Days
            };

            _repo.Add(entity);

            var response = new TourPackageDto
            {
                TourPackageId = entity.TourPackageId,
                PackName = entity.PackName,
                Location = entity.Location,
                Price = entity.Price,
                Days = entity.Days
            };

            return CreatedAtAction(nameof(GetById), new { id = response.TourPackageId }, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin can update
        public IActionResult Update(int id, [FromBody] CreateTourPackageDto dto)
        {
            var existing = _repo.GetById(id);
            if (existing == null)
                return NotFound(new { Message = $"Tour package with ID {id} not found." });

            existing.PackName = dto.PackName;
            existing.Location = dto.Location;
            existing.Price = dto.Price;
            existing.Days = dto.Days;

            _repo.Update(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin can delete
        public IActionResult Delete(int id)
        {
            var package = _repo.GetById(id);
            if (package == null)
                return NotFound(new { Message = $"Tour package with ID {id} not found." });

            _repo.Delete(id);
            return NoContent();
        }

        // ---------- Custom Search & Filter ----------

        [HttpGet("search")]
        [AllowAnonymous] // Publicly searchable
        public ActionResult<IEnumerable<TourPackageDto>> Search([FromQuery] string term)
        {
            var results = _customRepo.Search(term)
                .Select(p => new TourPackageDto
                {
                    TourPackageId = p.TourPackageId,
                    PackName = p.PackName,
                    Location = p.Location,
                    Price = p.Price,
                    Days = p.Days
                }).ToList();

            return Ok(results);
        }

        [HttpGet("filter")]
        [AllowAnonymous] // Publicly filterable
        public ActionResult<IEnumerable<TourPackageDto>> FilterByPrice(
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var results = _customRepo.FilterByPrice(minPrice ?? 0, maxPrice ?? decimal.MaxValue)
                .Select(p => new TourPackageDto
                {
                    TourPackageId = p.TourPackageId,
                    PackName = p.PackName,
                    Location = p.Location,
                    Price = p.Price,
                    Days = p.Days
                }).ToList();

            return Ok(results);
        }
    }
}
