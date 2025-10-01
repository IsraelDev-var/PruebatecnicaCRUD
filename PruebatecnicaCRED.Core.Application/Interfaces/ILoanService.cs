using PruebatecnicaCRUD.Core.Application.Dtos.Loan;

namespace PruebatecnicaCRUD.Core.Application.Interfaces
{
    public interface ILoanService : IGenericService<CreateLoanDto, UpdateLoanDto, LoanDto>
    {
        
    }
}
