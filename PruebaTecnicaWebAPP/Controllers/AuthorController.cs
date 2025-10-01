
using Microsoft.AspNetCore.Mvc;
using PruebatecnicaCRUD.Core.Application.Dtos.Author;
using PruebatecnicaCRUD.Core.Application.Interfaces;

namespace PruebaTecnicaWebAPP.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController(IAuthorService authorService) : Controller
    {
        private IAuthorService _authorService = authorService;


        // POST: api/authors
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authorService.CreateAsync(dto);

            if (!result)
            {
                return StatusCode(500, "No se pudo crear el Autor");
            }
            return Ok(new { message = "Autor creado corectamente" });
        }
        // PUT: api/authors/{id}
        [HttpPut("id:int")]

        public async Task<IActionResult> Update([FromBody] UpdateAuthorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authorService.UpdateAsync(dto);
            if (!result)
            {
                return NotFound(new {message = $"Autor con el ID {dto.Id} no encontrado"});
            }

            return Ok(new {message = "Autor actulizado correctamente"});
        }
        
       

        
       
    }
}
