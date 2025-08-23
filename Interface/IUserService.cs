using API_Project.DTOs;

namespace API_Project.Interface
{
    public interface IUserService
    {
        void Register(RegisterUserDto dto);
        UserResponseDto Authenticate(LoginUserDto dto);
        IEnumerable<UserResponseDto> GetAllUsers();
    }
}
