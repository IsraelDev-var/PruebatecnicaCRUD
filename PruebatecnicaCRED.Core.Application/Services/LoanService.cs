using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.Loan;
using PruebatecnicaCRUD.Core.Application.Interfaces;

namespace PruebatecnicaCRUD.Core.Application.Services
{
    public class LoanService : ILoanService
    {
        public Task<ServiceResult<LoanDto>> CreateAsync(CreateLoanDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<LoanDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<List<LoanDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<List<LoanDto>>> GetAllWithIncludeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<LoanDto?>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<LoanDto>> UpdateAsync(UpdateLoanDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
