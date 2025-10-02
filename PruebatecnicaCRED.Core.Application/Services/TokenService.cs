

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.Auth;
using PruebatecnicaCRUD.Core.Application.Interfaces;
using System.Security.Claims;

namespace PruebatecnicaCRUD.Core.Application.Services
{
    public class TokenService(IConfiguration config, ILogger<TokenService> logger) : ITokenService
    {
        private readonly IConfiguration _config = config;
        private readonly ILogger<TokenService> _logger = logger;

        


        public async Task<ServiceResult<LoginResponseDto>?> AuthenticateAsync(LoginRequestDto req)
        {
            try
            {
                if (req.UserName != "admin" || req.Password != "1234")
                {
                    _logger.LogWarning("Intento de autenticación fallido para el usuario {UserName}", req.UserName);
                    return ServiceResult<LoginResponseDto>.Fail("Credenciales inválidas");
                }

                // Leer configuraciones de JWT desde appsettings.json
                var jwtSettings = _config.GetSection("JwtConfig");
                var signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

                // crar los claims

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, req.UserName),
                    new Claim(ClaimTypes.Nmae, req.UserName),
                    new Claim(Clam)
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al autenticar el usuario {UserName}", req.UserName);
                return ServiceResult<LoginResponseDto>.Fail("Error de inesperado");
            }
        }
    }
}
