
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        // GET: api/books/{id}
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book.Data == null)
            {
                return BadRequest(new { message = book.Message });
            }
            return Ok(book);
        }
        // POST: api/books
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _bookService.CreateAsync(dto);
            if (created.Data == null)
            {
                return BadRequest(new { message = created.Message });
            }
            return Ok(created);
        }
        // PUT: api/books
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateBookDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = await _bookService.UpdateAsync(dto);
            if (updated.Data == null)
            {
                return BadRequest(new { message = updated.Message });
            }
            return Ok(updated);
        }
        // DELETE: api/books/{id}
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (deleted.Data == null)
            {
                return BadRequest(new { message = deleted.Message });
            }
            return Ok(deleted.Data);
        }
        //GET: api/books/before-year-2000
        [HttpGet("before-year-2000")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBooksBeforeYear2000()
        {
            var books = await _bookService.GetBooksBefore2000Async();
            if (books.Data == null || books.Data.Count == 0)
            {
                return NotFound(new { message = books.Message });
            }
            return Ok(books);
        }

        // GET: api/books/with-authors-loans
        [HttpGet("with-authors-loans")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetBooksWithAuthorsAndLoans()
        {
            var books = await _bookService.GetAllWithIncludeAsync();
            if (!books.IsSuccess)
            {
                return NotFound(new { messge = books.Message });
            }
            return Ok(books);
        }

    }
}
