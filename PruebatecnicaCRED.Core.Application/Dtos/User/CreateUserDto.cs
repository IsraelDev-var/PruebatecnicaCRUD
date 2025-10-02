

namespace PruebatecnicaCRUD.Core.Application.Dtos.User
{
    public class CreateUserDto
    {
        public required string Username { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
        public string? Role { get; set; } = "User"; // Admin o User
    }
}
