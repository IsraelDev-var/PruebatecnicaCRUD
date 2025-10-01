

namespace PruebatecnicaCRUD.Core.Domain.Entities
{
    public class Author
    {
        public required int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Nationality { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
