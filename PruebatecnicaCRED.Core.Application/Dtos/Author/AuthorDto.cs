using PruebatecnicaCRUD.Core.Application.Dtos.Book;

namespace PruebatecnicaCRUD.Core.Application.Dtos.Author
{
    public class AuthorDto
    {
        public  int Id { get; set; }
        public  string Name { get; set; } = string.Empty;
        public  string Nationality { get; set; } = string.Empty;

        // Navegation: Lista de libros del autor
        public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
    }
}
