
namespace PruebatecnicaCRUD.Core.Application.Interfaces
{
    public interface IGenericService<TCreateDto, TUpdateDto, TReadDto>
    {
        Task<bool> CreateAsync(TCreateDto dto);
        Task<List<TReadDto>> GetAllAsync();
        Task<List<TReadDto>> GetAllWithIncludeAsync();
        Task<TReadDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(TUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
