using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PruebatecnicaCRUD.Core.Application.Dtos;
using PruebatecnicaCRUD.Core.Application.Dtos.Auth;
using PruebatecnicaCRUD.Core.Application.Interfaces;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PruebatecnicaCRUD.Core.Domain.Interfaces;

namespace PruebatecnicaCRUD.Core.Application.Services
{
    public class TokenService( IUserRepository userRepository, IConfiguration config, ILogger<TokenService> logger) : ITokenService
    {
         
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _config = config;
        private readonly ILogger<TokenService> _logger = logger;
        
        public async Task<ServiceResult<LoginResponseDto>> AuthenticateAsync(LoginRequestDto req)
        {
            try
            {
                // buscar el usuario en la DB
                var user = await _userRepository.GetByUsernameAsync(req.UserName);

                if (user == null)
                {
                    _logger.LogWarning("Intento de autenticación fallido para el usuario {UserName}", req.UserName);
                    return ServiceResult<LoginResponseDto>.Fail("Usuario o contraseña incorrectos");
                }

                // validamo el pass
                if (user.PasswordHash != req.Password)
                {
                    _logger.LogWarning("Intento de autenticación fallido para el usuario {UserName}", req.UserName);
                    return ServiceResult<LoginResponseDto>.Fail("Usuario o contraseña incorrectos");
                }

                // Leer configuraciones de JWT desde appsettings.json
                var jwtSettings = _config.GetSection("JwtConfig");
                var signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

                // crar los claims

                var claims = new List<Claim>
                {
                    new (JwtRegisteredClaimNames.Sub, req.UserName),
                    new (ClaimTypes.Name, req.UserName),
                    new (ClaimTypes.Role, "Admin")
                };

                // Algoritmo de firma
                var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["TokenValidityMins"]!)),
                    signingCredentials: credentials
                );

                return ServiceResult<LoginResponseDto>.Ok(new LoginResponseDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                }, "Usuario autenticado exitosamente");



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al autenticar el usuario {UserName}", req.UserName);
                return ServiceResult<LoginResponseDto>.Fail("Error de inesperado");
            }
        }

 
    }
}
