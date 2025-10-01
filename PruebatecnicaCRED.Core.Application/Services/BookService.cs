using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.Book;
using PruebatecnicaCRUD.Core.Application.Interfaces;

namespace PruebatecnicaCRUD.Core.Application.Services
{
    public class BookService : IBookService
    {
        public Task<ServiceResult<BookDto>> CreateAsync(CreateBookDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<BookDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<List<BookDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<List<BookDto>>> GetAllWithIncludeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<BookDto?>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<BookDto>> UpdateAsync(UpdateBookDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
