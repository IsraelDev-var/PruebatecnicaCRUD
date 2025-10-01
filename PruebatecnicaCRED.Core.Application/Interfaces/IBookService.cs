

using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.Book;
namespace PruebatecnicaCRUD.Core.Application.Interfaces
{
    public interface IBookService : IGenericService<CreateBookDto, UpdateBookDto, BookDto>
    {
        // contato que solo implementa IBookService
        Task<ServiceResult<List<BookDto>>> GetBooksBefore2000Async();


    }
}
