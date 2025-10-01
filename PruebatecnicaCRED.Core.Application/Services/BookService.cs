using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.Author;
using PruebatecnicaCRUD.Core.Application.Dtos.Book;
using PruebatecnicaCRUD.Core.Application.Dtos.Loan;
using PruebatecnicaCRUD.Core.Application.Interfaces;
using PruebatecnicaCRUD.Core.Domain.Entities;
using PruebatecnicaCRUD.Core.Domain.Interfaces;

namespace PruebatecnicaCRUD.Core.Application.Services
{
    public class BookService(IBookRepository bookRepository,IAuthorRepository authorRepository, ILogger<BookService> logger) : IBookService
    {

        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly ILogger<BookService> _logger = logger;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        public async Task<ServiceResult<BookDto>> CreateAsync(CreateBookDto dto)
        {
            if (dto == null)
            {
                return ServiceResult<BookDto>.Fail("El objeto dto es nulo");
            }

            try
            {   // validar si el autor existe
                var existingAuthor = await _authorRepository.GetByIdAsync(dto.AuthorId);
                if (existingAuthor == null)
                {
                    return ServiceResult<BookDto>.Fail("El autor con el ID proporcionado no existe");
                }
                // mapeo manual
                Book book = new()
                {
                    Id = 0,
                    Title = dto.Title,
                    YearPublished = dto.YearPublished,
                    Genre = dto.Genre,
                    AuthorId = dto.AuthorId
                };

                var created = await _bookRepository.AddAsync(book);

                if (created == null)
                {
                    _logger.LogError("No se pudo crear el Autor {Title}", dto.Title);
                    return ServiceResult<BookDto>.Fail("No se pudo crear el Libro");
                }

                // mapeo manula
                var createdDto = new BookDto
                {
                    Id = created.Id,
                    Title = created.Title,
                    YearPublished = created.YearPublished,
                    Genre = created.Genre,
                    AuthorId = created.AuthorId

                };



                return ServiceResult<BookDto>.Ok(createdDto, "Libro creado exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el autor {Title}", dto.Title);
                return ServiceResult<BookDto>.Fail("Error en la base de datos");
            }
        }
        public async Task<ServiceResult<List<BookDto>>> GetAllAsync()
        {

            try
            {
                var books = await _bookRepository.GetAllAsync();
                var bookDtos = books.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    YearPublished = b.YearPublished,
                    Genre = b.Genre,
                    AuthorId = b.AuthorId
                }).ToList();
                return ServiceResult<List<BookDto>>.Ok(bookDtos, "Libros obtenidos exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los libros");
                return ServiceResult<List<BookDto>>.Fail("Error en la base de datos");
            }
        }

        public async Task<ServiceResult<BookDto>> DeleteAsync(int id)
        {
            try
            {
                var deleted = await _bookRepository.DeleteAsync(id);
                if (deleted == null)
                {
                    _logger.LogError("No se pudo eliminar. El libro con el ID {Id}", id);
                    return ServiceResult<BookDto>.Fail("El ID que usted a colocado no existe");
                }
                var deletedDto = new BookDto
                {
                    Id = deleted.Id,
                    Title = deleted.Title,
                    YearPublished = deleted.YearPublished,
                    Genre = deleted.Genre,
                    AuthorId = deleted.AuthorId
                };
                return ServiceResult<BookDto>.Ok(deletedDto, "Libro eliminado exitosamente");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de base de datos al eliminar el libro con ID {Id}", id);
                return ServiceResult<BookDto>.Fail("Error en la base de datos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar el libro con ID {Id}", id);
                return ServiceResult<BookDto>.Fail("Error en la base de datos");
            }

        }

        public async Task<ServiceResult<List<BookDto>>> GetBooksBefore2000Async()
        {
            try
            {
                var books = await _bookRepository.GetAllQueryable()
                    .Include(b => b.Author)
                    .Include(b => b.Loans)
                    .Where(b => b.YearPublished < 2000)
                    .Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        YearPublished = b.YearPublished,
                        Genre = b.Genre,
                        AuthorId = b.AuthorId
                    })
                    .ToListAsync();

                if (books == null || books.Count == 0)
                {
                    return ServiceResult<List<BookDto>>.Fail("No se encontraron libros publicados antes del año 2000");
                }
                return ServiceResult<List<BookDto>>.Ok(books, "Libros publicados antes del año 2000 encontrados exitosamente");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los libros publicados antes del año 2000");
                return ServiceResult<List<BookDto>>.Fail("Error en la base de datos");
            }
        }



        public async Task<ServiceResult<List<BookDto>>> GetAllWithIncludeAsync()
        {
            try
            {
                var book = await _bookRepository.GetAllQueryable()
                    .Include(b => b.Author)
                    .Include(b => b.Loans)
                    .ToListAsync();

                if (book == null)
                {
                    return ServiceResult<List<BookDto>>.Fail("No se encontraron libros");
                }

                var bookDtos = book.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    YearPublished = b.YearPublished,
                    Genre = b.Genre,
                    AuthorId = b.AuthorId,
                    Author = b.Author != null ? new AuthorDto
                    {
                        Id = b.Author.Id,
                        Name = b.Author.Name,
                        Nationality = b.Author.Nationality
                    } : null,
                    Loans = b.Loans?.Select(l => new LoanDto
                    {
                        Id = l.Id,
                        BookId = l.BookId,
                        LoanDate = l.LoanDate,
                        ReturnDate = l.ReturnDate,
                    }).ToList()
                }).ToList();



                return ServiceResult<List<BookDto>>.Ok(bookDtos, "Libros obtenidos exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los libros incluyendo su auto y prestamo ");
                return ServiceResult<List<BookDto>>.Fail("Error en la base de datos");
            }
        }
        public async Task<ServiceResult<BookDto?>> GetByIdAsync(int id)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book == null)
                {
                    return ServiceResult<BookDto?>.Fail("No se encontro el libro con el ID proporcionado");
                }
                var bookDto = new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    YearPublished = book.YearPublished,
                    Genre = book.Genre,
                    AuthorId = book.AuthorId
                };
                return ServiceResult<BookDto?>.Ok(bookDto, "Libro obtenido exitosamente");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el libro con ID {Id}", id);
                return ServiceResult<BookDto?>.Fail("Error en la base de datos");
            }
        }
        public async Task<ServiceResult<BookDto>> UpdateAsync(UpdateBookDto dto)
        {
            try
            {
                Book book = new()
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    YearPublished = dto.YearPublished,
                    Genre = dto.Genre,
                    AuthorId = dto.AuthorId
                };

                var updated = await _bookRepository.UpdateAsync(book.Id, book);
                if (updated == null)
                {
                    _logger.LogError("No se pudo actualizar. El libro con el ID {Id}", dto.Id);
                    return ServiceResult<BookDto>.Fail("El ID que usted a colocado no existe");
                }
                var updatedDto = new BookDto
                {
                    Id = updated.Id,
                    Title = updated.Title,
                    YearPublished = updated.YearPublished,
                    Genre = updated.Genre,
                    AuthorId = updated.AuthorId
                };
                return ServiceResult<BookDto>.Ok(updatedDto, "Libro actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el libro con ID {Id}", dto.Id);
                return ServiceResult<BookDto>.Fail("Error en la base de datos");
            }
        }
    }
}
