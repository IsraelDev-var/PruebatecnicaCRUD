namespace PruebatecnicaCRUD.Core.Application.Dtos.Loan
{
    public class CreateLoanDto
    {
        public required int BookId { get; set; }
        public required DateTime LoanDate { get; set; }
    }
}
