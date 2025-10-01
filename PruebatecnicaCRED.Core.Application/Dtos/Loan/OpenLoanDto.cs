

namespace PruebatecnicaCRUD.Core.Application.Dtos.Loan
{
    public class OpenLoanDto
    {

        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;

    }
}
