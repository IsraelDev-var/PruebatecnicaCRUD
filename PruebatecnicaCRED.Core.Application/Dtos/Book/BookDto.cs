using PruebatecnicaCRUD.Core.Application.Dtos.Author;
using PruebatecnicaCRUD.Core.Application.Dtos.Loan;

namespace PruebatecnicaCRUD.Core.Application.Dtos.Book;

public class BookDto
{
    public required int Id { get; set; }
    public required string Title { get; set; } = string.Empty;
    public required int YearPublished { get; set; }
    public required string? Genre { get; set; }
    // FK
    public required int AuthorId { get; set; }

    // Navigation property
    public AuthorDto? Author { get; set; }

    // lista de libros prestados
    public ICollection<LoanDto> Loans { get; set; } = new List<LoanDto>();

}
