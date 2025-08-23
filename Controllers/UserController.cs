using API_Project.DTOs;
using API_Project.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require authentication by default
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        // -----------------------------
        // Register (public)
        // -----------------------------
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterUserDto dto)
        {
            try
            {
                _userService.Register(dto);
                return Ok("User registered successfully with default role 'User'!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // -----------------------------
        // Login (public)
        // -----------------------------
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginUserDto dto)
        {
            var user = _userService.Authenticate(dto);
            if (user == null)
                return Unauthorized("Invalid credentials");

            // Generate JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName ?? "User")
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            // Return token as part of response DTO
            user.Token = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(user);
        }

        // -----------------------------
        // Get all users (Admin only)
        // -----------------------------
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
