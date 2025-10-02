
using Microsoft.Extensions.Logging;
using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.User;
using PruebatecnicaCRUD.Core.Application.Interfaces;
using PruebatecnicaCRUD.Core.Domain.Entities;
using PruebatecnicaCRUD.Core.Domain.Interfaces;

namespace PruebatecnicaCRUD.Core.Application.Services
{
    public class UserService(IUserRepository userRepository, ILogger<UserService> logger) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UserService> _logger = logger;

        public async Task<ServiceResult<UserDto>> AddAsync(CreateUserDto dto)
        {
            if (dto == null)
            {
                return ServiceResult<UserDto>.Fail("El objeto dto es nulo");
            }

            try
            {
                // mapeo manual
                User user = new()
                {
                    Id = 0,
                    Username = dto.Username,
                    PasswordHash = dto.Password,
                    Role = dto.Role
                };

                var created = await _userRepository.AddAsync(user);

                if (created == null)
                {
                    _logger.LogError("No se pudo crear el useuario {Name}", dto.Username);
                    return ServiceResult<UserDto>.Fail("No se pudo crear el useuario");
                }

                // mapeo manula
                var createdDto = new UserDto
                {
                    Id = created.Id,
                    Username = created.Username,
                    Role = created.Role

                };



                return ServiceResult<UserDto>.Ok(createdDto, "Useuario creado exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el autor {Name}", dto.Username);
                return ServiceResult<UserDto>.Fail("Error en la base de datos");
            }
        }

        public async Task<ServiceResult<UserDto>> GetByUsernameAsync(string username)
        {
            try
            {
                var user = await _userRepository.GetByUsernameAsync(username);
                if (user == null)
                {
                    _logger.LogWarning("Usuario no encontrado {UserName}", username);
                    return ServiceResult<UserDto>.Fail("Usuario no encontrado");
                }
                // mapeo manual
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role
                };
                return ServiceResult<UserDto>.Ok(userDto, "Usuario obtenido exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el usuario {UserName}", username);
                return ServiceResult<UserDto>.Fail("Error en la base de datos");
            }
        }
        public async Task<ServiceResult<UserDto>> UpdateAsync(int id, UpdateUserDto dto)
        {
            try
            {
                // mapeo manual
                User user = new()
                {
                    Id = id,
                    Username = dto.Username,
                    PasswordHash = dto.Password,
                    Role = dto.Role
                };
                var updated = await _userRepository.UpdateAsync(id, user);
                if (updated == null)
                {
                    _logger.LogError("No se pudo actualizar el usuario {Id}", id);
                    return ServiceResult<UserDto>.Fail("No se pudo actualizar el usuario");
                }
                // mapeo manula
                var updatedDto = new UserDto
                {
                    Id = updated.Id,
                    Username = updated.Username,
                    Role = updated.Role
                };
                return ServiceResult<UserDto>.Ok(updatedDto, "Usuario actualizado exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el usuario {Id}", id);
                return ServiceResult<UserDto>.Fail("Error en la base de datos");
            }
        }
    }
}
