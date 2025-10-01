using PruebatecnicaCRUD.Core.Application.Dtos.Book;
using PruebatecnicaCRUD.Core.Application.Interfaces;

namespace PruebatecnicaCRUD.Core.Application.Services
{
    public class BookService : IBookService
    {
        public Task<bool> CreateAsync(CreateBookDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<BookDto>> GetAllWithIncludeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BookDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(UpdateBookDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
