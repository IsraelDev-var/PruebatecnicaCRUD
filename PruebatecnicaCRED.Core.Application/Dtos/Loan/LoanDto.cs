using PruebatecnicaCRUD.Core.Application.Dtos.Book;


namespace PruebatecnicaCRUD.Core.Application.Dtos.Loan
{
    public class LoanDto
    {
        // Pk
        public required int Id { get; set; }
        // fk
        public required int BookId { get; set; }
        
        public required DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Navegation property
        public BookDto? Book { get; set; }


    }
}
