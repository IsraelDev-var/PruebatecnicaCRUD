using PruebatecnicaCRUD.Core.Application.Dtos.Loan;
using PruebatecnicaCRUD.Core.Application.Interfaces;

namespace PruebatecnicaCRUD.Core.Application.Services
{
    public class LoanService : ILoanService
    {
        public Task<bool> CreateAsync(CreateLoanDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LoanDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<LoanDto>> GetAllWithIncludeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LoanDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(UpdateLoanDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
