

using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.User;

namespace PruebatecnicaCRUD.Core.Application.Interfaces
{
    public interface IUserService 
    {
         Task<ServiceResult<UserDto>> GetByUsernameAsync(string username);
         Task<ServiceResult<UserDto>> AddAsync(CreateUserDto dto);
         Task<ServiceResult<UserDto>> UpdateAsync(int id, UpdateUserDto dto);
    }
}
