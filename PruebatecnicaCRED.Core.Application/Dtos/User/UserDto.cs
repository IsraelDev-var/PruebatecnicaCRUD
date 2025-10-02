

namespace PruebatecnicaCRUD.Core.Application.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Role { get; set; } // Admin o User
    }
}
