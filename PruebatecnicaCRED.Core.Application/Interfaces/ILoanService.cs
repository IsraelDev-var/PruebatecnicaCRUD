using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.Loan;

namespace PruebatecnicaCRUD.Core.Application.Interfaces
{
    public interface ILoanService : IGenericService<CreateLoanDto, UpdateLoanDto, LoanDto>
    {
        // contrtos que solo implementa ILoanService
        Task<ServiceResult<List<OpenLoanDto>>> GetNotReturnedAsync();
        Task<ServiceResult<LoanDto>> ReturnAsync(ReturnLoanDto dto);
    }
}
