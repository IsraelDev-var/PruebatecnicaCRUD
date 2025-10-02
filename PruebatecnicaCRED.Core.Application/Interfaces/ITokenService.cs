

using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.Auth;

namespace PruebatecnicaCRUD.Core.Application.Interfaces
{
    public interface ITokenService 
    {
        Task<ServiceResult<LoginResponseDto>> AuthenticateAsync(LoginRequestDto req); 
        
    }
}
