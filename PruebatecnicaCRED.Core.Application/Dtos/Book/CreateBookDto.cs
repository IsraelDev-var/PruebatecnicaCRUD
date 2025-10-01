

namespace PruebatecnicaCRUD.Core.Application.Dtos.Book
{
    public class CreateBookDto
    {
        public required string Title { get; set; } = string.Empty;
        public required int YearPublished { get; set; }
        public required string? Genre { get; set; }
        public required int AuthorId { get; set; }
    }
}
