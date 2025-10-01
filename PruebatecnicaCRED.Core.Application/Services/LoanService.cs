using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.Loan;
using PruebatecnicaCRUD.Core.Application.Interfaces;
using PruebatecnicaCRUD.Core.Domain.Entities;
using PruebatecnicaCRUD.Core.Domain.Interfaces;

namespace PruebatecnicaCRUD.Core.Application.Services
{
    public class LoanService(ILoanRepository loanRepository, IBookRepository bookRepository, ILogger<LoanService> logger) : ILoanService
    {

        private readonly ILoanRepository _loanRepository = loanRepository;
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly ILogger<LoanService> _logger = logger;
        public async Task<ServiceResult<LoanDto>> CreateAsync(CreateLoanDto dto)
        {
            if (dto == null)
                return ServiceResult<LoanDto>.Fail("El objeto dto es nulo");

            try
            {
                // mapeo manual
                Loan loan = new()
                {
                    Id = 0,
                    BookId = dto.BookId,
                    LoanDate = dto.LoanDate,
                    ReturnDate = null
                };
                var hasOpenLoan = await _loanRepository.GetAllQueryable()
                .AnyAsync(l => l.BookId == dto.BookId && l.ReturnDate == null);

                if (hasOpenLoan)
                {
                    return ServiceResult<LoanDto>.Fail("El libro ya esta prestado y no ha sido devuelto.");
                }
                    

                var created = await _loanRepository.AddAsync(loan);
                if (created == null)
                {
                    _logger.LogError("No se pudo crear el prestamo para el libro con Id {BookId}", dto.BookId);
                    return ServiceResult<LoanDto>.Fail("No se pudo crear el prestamo");
                }
                // mapeo manual
                var createdDto = new LoanDto
                {
                    Id = created.Id,
                    BookId = created.BookId,
                    LoanDate = created.LoanDate,
                    ReturnDate = created.ReturnDate
                };
                return ServiceResult<LoanDto>.Ok(createdDto, "Prestamo creado exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el préstamo");
                return ServiceResult<LoanDto>.Fail("Error en la base de datos");
            }
        }
        // DELETE
        public async Task<ServiceResult<LoanDto>> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return ServiceResult<LoanDto>.Fail("El ID es inválido");
            }
            try
            {
                var deleted = await _loanRepository.DeleteAsync(id);
                if (deleted == null)
                {
                    _logger.LogError("No se pudo eliminar. El prestamo con el ID {Id} no existe", id);
                    return ServiceResult<LoanDto>.Fail("El ID que usted a colocado no existe");
                }
                var deletedDto = new LoanDto
                {
                    Id = deleted.Id,
                    BookId = deleted.BookId,
                    LoanDate = deleted.LoanDate,
                    ReturnDate = deleted.ReturnDate
                };
                return ServiceResult<LoanDto>.Ok(deletedDto, "Prestamo eliminado exitosamente");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de base de datos al eliminar el prestamo {Id}", id);
                return ServiceResult<LoanDto>.Fail("Error en la base de datos");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar el prestamo {Id}", id);
                return ServiceResult<LoanDto>.Fail("Error en la base de datos");
            }
        }

        public async Task<ServiceResult<List<LoanDto>>> GetAllAsync()
        {
            try
            {
                var loans = await _loanRepository.GetAllAsync();
                var loanDtos = loans.Select(loan => new LoanDto
                {
                    Id = loan.Id,
                    BookId = loan.BookId,
                    LoanDate = loan.LoanDate,
                    ReturnDate = loan.ReturnDate
                })
                    .ToList();
                if (loanDtos.Count == 0 || loanDtos == null)
                {
                    return ServiceResult<List<LoanDto>>.Fail("No hay prestamos registrados");
                }

                return ServiceResult<List<LoanDto>>.Ok(loanDtos, "Prestamos obtenidos exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los prestamos");
                return ServiceResult<List<LoanDto>>.Fail("Error en la base de datos");
            }

        }

        public async Task<ServiceResult<List<LoanDto>>> GetAllWithIncludeAsync()
        {
            try
            {
                var loans = await _loanRepository.GetAllQueryable()
                    .Include(x => x.Book)
                    .ToListAsync();
                var loanDtos = loans.Select(loan => new LoanDto
                {
                    Id = loan.Id,
                    BookId = loan.BookId,
                    LoanDate = loan.LoanDate,
                    ReturnDate = loan.ReturnDate

                })
                    .ToList();
                if (loanDtos.Count == 0 || loanDtos == null)
                {
                    return ServiceResult<List<LoanDto>>.Fail("No hay prestamos registrados");
                }
                return ServiceResult<List<LoanDto>>.Ok(loanDtos, "Prestamos obtenidos exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los prestamos con include");
                return ServiceResult<List<LoanDto>>.Fail("Error en la base de datos");
            }
        }
        public async Task<ServiceResult<LoanDto?>> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return ServiceResult<LoanDto?>.Fail("El ID es invalido");
            }
            try
            {
                var loan = await _loanRepository.GetByIdAsync(id);
                if (loan == null)
                {
                    return ServiceResult<LoanDto?>.Fail($"El prestamo con ID {id} no existe");
                }
                var loanDto = new LoanDto
                {
                    Id = loan.Id,
                    BookId = loan.BookId,
                    LoanDate = loan.LoanDate,
                    ReturnDate = loan.ReturnDate
                };
                return ServiceResult<LoanDto?>.Ok(loanDto, "Prestamo obtenido exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el prestamo {Id}", id);
                return ServiceResult<LoanDto?>.Fail("Error en la base de datos");
            }


        }

        // PUT devolver préstamo
        public async Task<ServiceResult<LoanDto>> ReturnAsync(ReturnLoanDto dto)
        {
            if (dto == null || dto.Id <= 0)
                return ServiceResult<LoanDto>.Fail("Solicitud invalida.");

            try
            {
                var loan = await _loanRepository.GetByIdAsync(dto.Id);
                if (loan == null)
                {
                    return ServiceResult<LoanDto>.Fail($"Prestamo {dto.Id} no encontrado.");
                }


                if (loan.ReturnDate != null)
                {
                    return ServiceResult<LoanDto>.Fail("El prestamo ya fue devuelto.");
                }
                // si no se proporciona fecha de devolucion, usar la fecha actual
                loan.ReturnDate = dto.ReturnDate ?? DateTime.UtcNow;

                var updated = await _loanRepository.UpdateAsync(loan.Id, loan);
                if (updated == null)
                    return ServiceResult<LoanDto>.Fail("No se pudo actualizar el prestamo.");

                var loanDto = new LoanDto
                {
                    Id = updated.Id,
                    BookId = updated.BookId,
                    LoanDate = updated.LoanDate,
                    ReturnDate = updated.ReturnDate
                };

                return ServiceResult<LoanDto>.Ok(loanDto, "Prestamo devuelto correctamente.");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de BD al devolver prestamo {Id}", dto.Id);
                return ServiceResult<LoanDto>.Fail("Error en la base de datos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al devolver prestamo {Id}", dto.Id);
                return ServiceResult<LoanDto>.Fail("Error inesperado.");
            }

        }

        // Reasignar libro a un préstamo abierto
        public async Task<ServiceResult<LoanDto>> UpdateAsync(UpdateLoanDto dto)
        {
            if (dto == null || dto.Id <= 0 || dto.BookId <= 0)
            {
                return ServiceResult<LoanDto>.Fail("Solicitud invalida.");
            }

            try
            {
                //prestamo
                var loan = await _loanRepository.GetByIdAsync(dto.Id);
                if (loan == null)
                    return ServiceResult<LoanDto>.Fail($"Prestamo {dto.Id} no encontrado.");

                if (loan.ReturnDate != null)
                    return ServiceResult<LoanDto>.Fail("No se puede reasignar: el prestamo ya fue devuelto.");

                //nuevo libro existe
                var newBook = await _bookRepository.GetByIdAsync(dto.BookId);
                if (newBook == null)
                    return ServiceResult<LoanDto>.Fail($"Libro {dto.BookId} no existe.");

                // nuevo libro no tiene prestamo abierto
                var newBookHasOpenLoan = await _loanRepository.GetAllQueryable()
                    .AsNoTracking()
                    .AnyAsync(l => l.BookId == dto.BookId && l.ReturnDate == null && l.Id != dto.Id);

                if (newBookHasOpenLoan)
                    return ServiceResult<LoanDto>.Fail("El nuevo libro ya tiene un prestamo abierto.");

                //actualizar
                loan.BookId = dto.BookId;
                var updated = await _loanRepository.UpdateAsync(loan.Id, loan);
                if (updated == null)
                    return ServiceResult<LoanDto>.Fail("No se pudo actualizar el prestamo.");

                var loanDto = new LoanDto
                {
                    Id = updated.Id,
                    BookId = updated.BookId,
                    LoanDate = updated.LoanDate,
                    ReturnDate = updated.ReturnDate
                };

                _logger.LogInformation("Prestamo {Id} reasignado al libro {BookId}", updated.Id, updated.BookId);
                return ServiceResult<LoanDto>.Ok(loanDto, "Prestamo reasignado correctamente.");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de BD al reasignar libro en prestamo {Id}", dto.Id);
                return ServiceResult<LoanDto>.Fail("Error en la base de datos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al reasignar libro en prestamo {Id}", dto.Id);
                return ServiceResult<LoanDto>.Fail("Error inesperado.");
            }
        }

        // GET /prestamos/no-devueltos
        public async Task<ServiceResult<List<OpenLoanDto>>> GetNotReturnedAsync()
        {
            try
            {
                var items = await _loanRepository
                    .GetAllQueryable()                    // IQueryable<Loan>
                    .AsNoTracking()                    // solo lectura → mas rapido
                    .Where(l => l.ReturnDate == null)  // no devueltos
                    .Select(l => new OpenLoanDto       // proyeccion directa → evita N+1
                    {
                        AuthorId = l.Book.Author.Id,
                        AuthorName = l.Book.Author.Name,
                        BookId = l.BookId,
                        BookTitle = l.Book.Title
                    })
                    .ToListAsync();

                if (items.Count == 0)
                    return ServiceResult<List<OpenLoanDto>>.Fail("No hay prestamos abiertos.");

                return ServiceResult<List<OpenLoanDto>>.Ok(items, "Prestamos abiertos obtenidos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar prestamos no devueltos");
                return ServiceResult<List<OpenLoanDto>>.Fail("Error en la base de datos");
            }
        }
    }
}
