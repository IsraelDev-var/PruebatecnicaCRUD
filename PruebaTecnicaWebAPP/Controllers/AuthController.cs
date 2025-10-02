
using Microsoft.AspNetCore.Mvc;
using PruebatecnicaCRUD.Core.Application.Dtos.Auth;
using PruebatecnicaCRUD.Core.Application.Interfaces;

namespace PruebaTecnicaWebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ITokenService tokenService) : ControllerBase
    {
        private readonly ITokenService _tokenService = tokenService;
        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto req)
        {
            
            var result = await _tokenService.AuthenticateAsync(req);
            if (result == null || result.Data == null)
            {
                return Unauthorized(new { message = result?.Message ?? "Error de autenticación" });
            }
            return Ok(result);
        }
    }
}
