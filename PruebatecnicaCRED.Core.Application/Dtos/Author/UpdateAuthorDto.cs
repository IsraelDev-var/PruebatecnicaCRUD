namespace PruebatecnicaCRUD.Core.Application.Dtos.Author
{
    public class UpdateAuthorDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Nationality { get; set; } = string.Empty;
    }
}
