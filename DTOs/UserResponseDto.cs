namespace API_Project.DTOs
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }

        // ✅ Add this property to return JWT
        public string Token { get; set; }
    }
}
