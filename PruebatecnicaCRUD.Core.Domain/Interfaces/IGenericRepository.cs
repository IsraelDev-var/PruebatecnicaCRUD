

namespace PruebatecnicaCRUD.Core.Domain.Interfaces
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity?> AddAsync(Entity entity);
        Task<IEnumerable<Entity>> GetAllAsync();
        IQueryable<Entity> GetAllQueryable();
        Task<Entity?> GetByIdAsync(int id);
        Task<Entity?> UpdateAsync(Entity entity);
        Task DeleteAsync(int id);
        //Task<List<Entity>> GetAllListWithInclude(List<string> properties);
        //IQueryable<Entity> GetAllQueryWithInclude(List<string> properties);
    }
}
