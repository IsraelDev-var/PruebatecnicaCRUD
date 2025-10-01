using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebatecnicaCRUD.Core.Application.Dtos.Loan
{
    public class LoanDto
    {
        // fk
        public required int LoanId { get; set; }
        // fk
        public required int BookId { get; set; }
        public required string BookTitle { get; set; } = string.Empty;
        public required DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
