

using PruebatecnicaCRUD.Core.Application.Dtos.Book;
namespace PruebatecnicaCRUD.Core.Application.Interfaces
{
    public interface IBookService : IGenericService<CreateBookDto, UpdateBookDto, BookDto>
    {
       
    }
}
