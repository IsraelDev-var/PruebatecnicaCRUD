using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebatecnicaCRUD.Core.Application.Dtos.Book;
using PruebatecnicaCRUD.Core.Application.Interfaces;

namespace PruebaTecnicaWebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookService bookService) : ControllerBase
    {
        private readonly IBookService _bookService = bookService;

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        // GET: api/books/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book.Data == null)
            {
                return NotFound("No se encontro el libro. Verifique el ID");
            }
            return Ok(book);
        }
        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _bookService.CreateAsync(dto);
            if (created.Data == null)
            {
                return BadRequest("No se pudo crear el libro.");
            }
            return Ok(created);
        }
        // PUT: api/books
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBookDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = await _bookService.UpdateAsync(dto);
            if (updated.Data == null)
            {
                return NotFound("No se pudo actualizar el libro. Verifique el ID");
            }
            return Ok(updated);
        }
        // DELETE: api/books/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (deleted.Data == null)
            {
                return NotFound("No se pudo eliminar el libro. Verifique el ID");
            }
            return Ok(deleted);
        }
        //GET: api/books/before-year-2000
        [HttpGet("before-year-2000")]
        public async Task<IActionResult> GetBooksBeforeYear2000()
        {
            var books = await _bookService.GetBooksBefore2000Async();
            return Ok(books);
        }

        // GET: api/books/with-authors-loans
        [HttpGet("with-authors-loans")]
        public async Task<IActionResult> GetBooksWithAuthorsAndLoans()
        {
            var books = await _bookService.GetAllWithIncludeAsync();
            return Ok(books);
        }

    }
}
