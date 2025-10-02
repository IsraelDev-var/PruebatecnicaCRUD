

namespace PruebatecnicaCRUD.Core.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // se recomienda hash, no texto plano
        public string? Role { get; set; } = "User"; // Admin o User

    }
}
