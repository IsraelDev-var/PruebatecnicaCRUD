

using Microsoft.EntityFrameworkCore;
using PruebatecnicaCRUD.Core.Domain.Entities;
using PruebatecnicaCRUD.Core.Domain.Interfaces;
using PruebatecnicaCRUD.Infrastructure.Persistence.Contexts;

namespace PruebatecnicaCRUD.Infrastructure.Persistence.Repositories
{
    public class UserRepository(PruebaAppContext context) : IUserRepository
    {
        private readonly PruebaAppContext _context = context;

       

        public async Task<User?> AddAsync(User entity)
        {
            await _context.Set<User>().AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> UpdateAsync(int id, User entity)
        {
           
            var entry = await _context.Set<User>().FindAsync(id);

            if (entry == null) return null;
            _context.Entry(entry).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
