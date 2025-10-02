

using PruebatecnicaCRUD.Core.Domain.Entities;

namespace PruebatecnicaCRUD.Core.Domain.Interfaces
{
    public interface IUserRepository 
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> AddAsync(User entity);
        Task<User?> UpdateAsync(int id, User entity);
        
        

    }
}
