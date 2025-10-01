
using Microsoft.AspNetCore.Mvc;
using PruebatecnicaCRUD.Core.Application.Dtos.Author;
using PruebatecnicaCRUD.Core.Application.Interfaces;

namespace PruebaTecnicaWebAPP.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController(IAuthorService authorService) : Controller
    {
        private readonly IAuthorService _authorService = authorService;


        // POST: api/authors
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _authorService.CreateAsync(dto);

            if (created == null)
            {
                return BadRequest("No se pudo crear el autor.");
            }
            return Ok(created);
        }
        // PUT: api/authors/{id}
        [HttpPut("id:int")]

        public async Task<IActionResult> Update([FromBody] UpdateAuthorDto dto)
        {
            if (dto == null)
            {
                return BadRequest("EL iD NO COINCIDE.");
            }

            var updated = await _authorService.UpdateAsync(dto);
            if (updated == null)
            {
                return NotFound("No se pudo actualizar el autor.");
            }

            return Ok(updated);



        }
        
       

        
       
    }
}
