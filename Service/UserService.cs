using API_Project.DTOs;
using API_Project.Interface;
using API_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Project.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public void Register(RegisterUserDto dto)
        {
            if (_userRepository.GetByEmail(dto.Email) != null)
                throw new Exception("Email already exists!");

          
            var defaultRole = _roleRepository.GetByName("User");
            if (defaultRole == null)
                throw new Exception("Default role 'User' not found. Please seed roles in DB.");

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = dto.Password, 
                RoleId = defaultRole.RoleId
            };

            _userRepository.Add(user);
        }

        public UserResponseDto Authenticate(LoginUserDto dto)
        {
            var user = _userRepository.GetByEmail(dto.Email);
            if (user == null)
                return null;

          
            if (user.PasswordHash != dto.Password)
                return null;

            return new UserResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                RoleName = user.Role?.Name
            };
        }

        public IEnumerable<UserResponseDto> GetAllUsers()
        {
            return _userRepository.GetAll().Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                RoleName = u.Role?.Name
            });
        }
    }
}


