using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebatecnicaCRUD.Core.Application.Dtos.Author;
using PruebatecnicaCRUD.Core.Application.Interfaces;
using PruebatecnicaCRUD.Core.Domain.Entities;
using PruebatecnicaCRUD.Core.Domain.Interfaces;

namespace PruebatecnicaCRUD.Core.Application.Services
{
    public class AuthorService(IAuthorRepository authorRepository, ILogger<AuthorService> logger) : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly ILogger<AuthorService> _logger = logger;

        // create Author
        public async Task<bool> CreateAsync(CreateAuthorDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            try
            {
                // mapeo manual
                Author author = new()
                {
                    Id = 0,
                    Name = dto.Name,
                    Nationality = dto.Nationality,
                };

                var created = await _authorRepository.AddAsync(author);

                if (created != null) 
                {
                    _logger.LogError("No se pudo crear el Autor {Name}", dto.Name);
                    return false;
                }
                _logger.LogInformation("Autor creado exitosamente: {Name}", dto.Name);
                return true;

            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de base de datos al crear el autor {Name}", dto.Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el autor {Name}", dto.Name);
                throw;
            }

        }

        public  async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _authorRepository.DeleteAsync(id);
                return true;

            }
            catch (KeyNotFoundException)
            {
                _logger.LogError("No se pudo eliminar. El athor con el ID {Id}", id);
                throw;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de base de datos al eliminar el autor {Id}", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar el autor {Id}", id);
                throw;
            }

        }

        public Task<List<AuthorDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<AuthorDto>> GetAllWithIncludeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthorDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(UpdateAuthorDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            try
            {
                var entity = await _authorRepository.GetByIdAsync(dto.Id);
                if (entity == null)
                {
                    _logger.LogWarning("No se puede actualizar. Autor con ID {Id} no encontrado", dto.Id);
                    return false;
                }
                Author authoy = new () 
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Nationality = dto.Nationality,

                };

                return await _authorRepository.UpdateAsync(authoy) != null;

            }
            catch (DbUpdateException dbEx) {
                _logger.LogError(dbEx, "Error de base de datos al actualizar el autor {Id}", dto.Id);
                throw;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error inesperado al actualizar el autor {Id}", dto.Id);
                throw;
            }
           
        }
    }
}
