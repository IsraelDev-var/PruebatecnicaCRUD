namespace PruebatecnicaCRUD.Core.Application.Dtos.Author
{
    public class CreateAuthorDto
    {
        public required int AuthorId { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Nationality { get; set; } = string.Empty;
    }
}
