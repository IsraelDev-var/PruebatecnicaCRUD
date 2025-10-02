
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
        {
            
            var created = await _authorService.CreateAsync(dto);

            if (created.Data == null)
            {
                return BadRequest(new {message = created.Message} );
            }
            return Ok(created);
        }
        // PUT: api/authors/{id}
        [HttpPut("id:int")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update([FromBody] UpdateAuthorDto dto)
        {
           
            var updated = await _authorService.UpdateAsync(dto);
            if (updated.Data == null)
            {
                return BadRequest(new { message = updated.Message });
            }

            return Ok(updated);

        }
        // DELETE: api/authors/{id}
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _authorService.DeleteAsync(id);
            if (deleted.Data == null)
            {
                return BadRequest(new { message = deleted.Message });
            }
            return Ok(deleted);
        }
        // GET: api/authors
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllList()
        {
            var authors = await _authorService.GetAllAsync();
            if (authors.Data == null)
            {
                return BadRequest(new { message = authors.Message });
            }
            return Ok(authors);
        }
        // GET: api/authors/{id}
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author.Data == null)
            {
                return BadRequest(new { message = author.Message });
            }
            return Ok(author);
        }

        // GET: api/authors/with-books
        [HttpGet("with-book")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllWithInclude()
        {
            var authors = await _authorService.GetAllWithIncludeAsync();
            
            return Ok(authors);
        }

    }
}
