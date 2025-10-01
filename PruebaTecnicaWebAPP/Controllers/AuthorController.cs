
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

            if (created.Data == null)
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
            if (updated.Data == null)
            {
                return NotFound("No se pudo actualizar el autor.");
            }

            return Ok(updated);

        }
        // DELETE: api/authors/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _authorService.DeleteAsync(id);
            if (deleted.Data == null)
            {
                return NotFound("No se pudo eliminar el autor. verifique el ID");
            }
            return Ok(deleted);
        }
        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllList()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }
        // GET: api/authors/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author.Data == null)
            {
                return NotFound("Autor no encontrado.");
            }
            return Ok(author);
        }






    }
}
