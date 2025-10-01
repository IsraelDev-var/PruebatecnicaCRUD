using PruebatecnicaCRUD.Core.Domain.Interfaces;
using PruebatecnicaCRUD.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace PruebatecnicaCRUD.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity>(PruebaAppContext context) : IGenericRepository<Entity>
        where Entity : class
    {
        private readonly PruebaAppContext _context = context;
        
        public async Task<Entity?> AddAsync(Entity entity)
        {
            var result = await _context.Set<Entity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<Entity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public IQueryable<Entity> GetAllQueryable()
        {
            return _context.Set<Entity>().AsQueryable();
        }
        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await Task.FromResult(_context.Set<Entity>().AsEnumerable());
        }
        public async Task<Entity?> GetByIdAsync(int id)
        {
            return await _context.Set<Entity>().FindAsync(id);
        }
        public async Task<Entity?> UpdateAsync(Entity entity)
        {
            var result = _context.Set<Entity>().Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}