

namespace PruebatecnicaCRUD.Core.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        
        public required DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        // navegation property
        public Book Book { get; set; } = null!;
    }
}
