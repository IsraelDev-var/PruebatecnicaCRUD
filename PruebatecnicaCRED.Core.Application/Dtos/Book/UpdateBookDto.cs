namespace PruebatecnicaCRUD.Core.Application.Dtos.Book;

public class UpdateBookDto
{
    public required string Title { get; set; } = string.Empty;
    public required int YearPublished { get; set; }
    public string? Genre { get; set; }
}

