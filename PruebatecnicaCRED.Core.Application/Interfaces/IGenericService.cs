
using PruebatecnicaCRUD.Core.Application.Dtos;

namespace PruebatecnicaCRUD.Core.Application.Interfaces
{
    public interface IGenericService<TCreateDto, TUpdateDto, TReadDto>
    {
        Task<ServiceResult<TReadDto>> CreateAsync(TCreateDto dto);
        Task<ServiceResult<List<TReadDto>>> GetAllAsync();
        Task<ServiceResult<List<TReadDto>>> GetAllWithIncludeAsync();
        Task<ServiceResult<TReadDto?>> GetByIdAsync(int id);
        Task<ServiceResult<TReadDto>> UpdateAsync(TUpdateDto dto);
        Task<ServiceResult<TReadDto>> DeleteAsync(int id);
    }
}
