namespace API_Project.DTOs
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }

       
        public string Token { get; set; }
    }
}

