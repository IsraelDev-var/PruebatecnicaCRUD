using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.Author;
using PruebatecnicaCRUD.Core.Application.Dtos.Book;
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
        public async Task<ServiceResult<AuthorDto>> CreateAsync(CreateAuthorDto dto)
        {
            if (dto == null)
            {
                return ServiceResult<AuthorDto>.Fail("El objeto dto es nulo");
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

                if (created == null)
                {
                    _logger.LogError("No se pudo crear el Autor {Name}", dto.Name);
                    return ServiceResult<AuthorDto>.Fail("No se pudo crear el Autor");
                }

                // mapeo manula
                var createdDto = new AuthorDto
                {
                    Id = created.Id,
                    Nationality = created.Nationality
                };



                return ServiceResult<AuthorDto>.Ok(createdDto, "Author creado exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el autor {Name}", dto.Name);
                return ServiceResult<AuthorDto>.Fail("Error en la base de datos");
            }

        }



        public async Task<ServiceResult<AuthorDto>> UpdateAsync(UpdateAuthorDto dto)
        {
            if (dto == null)
            {
                return ServiceResult<AuthorDto>.Fail("El objeto dto es nulo");
            }
            ;
            try
            {


                Author author = new()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Nationality = dto.Nationality,

                };

                var upated = await _authorRepository.UpdateAsync(author.Id, author);
                if (upated == null)
                {
                    _logger.LogError("No se pudo actualizar. El athor con el ID {Id}", dto.Id);
                    return ServiceResult<AuthorDto>.Fail("El ID que usted a colocado no existe");
                }
                var upatedto = new AuthorDto
                {
                    Id = upated.Id,
                    Name = upated.Name,
                    Nationality = upated.Nationality,
                };
                return ServiceResult<AuthorDto>.Ok(upatedto, "Author actulizado exitosamente");

            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de base de datos al actualizar el autor {Id}", dto.Id);
                return ServiceResult<AuthorDto>.Fail("Error en la base de datos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el autor {Id}", dto.Id);
                return ServiceResult<AuthorDto>.Fail("Error en la base de datos");
            }

        }

        public async Task<ServiceResult<AuthorDto>> DeleteAsync(int id)
        {
            try
            {
                var deleted = await _authorRepository.DeleteAsync(id);
                if (deleted == null)
                {
                    _logger.LogError("No se pudo eliminar. El athor con el ID {Id}", id);
                    return ServiceResult<AuthorDto>.Fail("El ID que usted a colocado no existe");
                }
                var deletedDto = new AuthorDto
                {
                    Id = deleted.Id,
                    Name = deleted.Name,
                };
                return ServiceResult<AuthorDto>.Ok(deletedDto, "Author eliminado exitosamente");

            }
            catch (KeyNotFoundException)
            {
                _logger.LogError("No se pudo eliminar. El athor con el ID {Id}", id);
                return ServiceResult<AuthorDto>.Fail("El ID que usted a colocado no existe");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar el autor {Id}", id);
                return ServiceResult<AuthorDto>.Fail("Error en la base de datos");
            }
        }

        public async Task<ServiceResult<List<AuthorDto>>> GetAllAsync()
        {
            try
            {
                var authors = await _authorRepository.GetAllAsync();
                var authorDtos = authors.Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Nationality = a.Nationality
                }).ToList();

                return ServiceResult<List<AuthorDto>>.Ok(authorDtos, "Autores obtenidos exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los autores");
                return ServiceResult<List<AuthorDto>>.Fail("Error en la base de datos");
            }
        }

        public async Task<ServiceResult<List<AuthorDto>>> GetAllWithIncludeAsync()
        {
            try
            {
                var authors = await _authorRepository.GetAllQueryable()
                    .Include(c => c.Books)
                    .ToListAsync();
                if (authors == null)
                {
                    return ServiceResult<List<AuthorDto>>.Fail("No se encontraron autores");
                }
                var authorDtos = authors.Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Nationality = a.Nationality,
                    Books = [.. a.Books.Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        YearPublished = b.YearPublished,
                        Genre = b.Genre,
                        AuthorId = b.AuthorId
                    })]
                }).ToList();

                return ServiceResult<List<AuthorDto>>.Ok(authorDtos, "Autores obtenidos exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los autores");
                return ServiceResult<List<AuthorDto>>.Fail("Error en la base de datos");
            }
        }

        public async Task<ServiceResult<AuthorDto?>> GetByIdAsync(int id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                if (author == null)
                {
                    return ServiceResult<AuthorDto?>.Fail("El ID que usted a colocado no existe");
                }
                var authorDtos = new AuthorDto
                {
                    Id = author.Id,
                    Name = author.Name,
                    Nationality = author.Nationality,
                    Books = [.. author.Books.Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        YearPublished = b.YearPublished,
                        Genre = b.Genre,
                        AuthorId = b.AuthorId
                    })]
                    };

                return ServiceResult<AuthorDto?>.Ok(authorDtos, "Autor obtenido exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el autor con ID {Id}", id);
                return ServiceResult<AuthorDto?>.Fail("Error en la base de datos");
            }
        }

    }
}
