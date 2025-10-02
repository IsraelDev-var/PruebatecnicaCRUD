using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebatecnicaCRUD.Core.Application.Dtos.Loan;
using PruebatecnicaCRUD.Core.Application.Interfaces;

namespace PruebaTecnicaWebAPP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController(ILoanService loanService) : Controller
    {
        private readonly ILoanService _loanService = loanService;

        // GET: api/loans/not-returned
        [HttpGet("not-returned")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNoReturned()
        {
            var result = await _loanService.GetNotReturnedAsync();
            if (result.Data == null)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result);
        }

        // GET: api/loans
        [HttpGet()]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _loanService.GetAllAsync();
            if (result.Data == null)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result);
        }
        // GET: api/loans/{id}
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _loanService.GetByIdAsync(id);
            if (result.Data == null)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result);
        }
        // POST: api/loans
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create([FromBody] CreateLoanDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _loanService.CreateAsync(dto);
            if (created.Data == null)
            {
                return BadRequest(new { message = created.Message });
            }
            return Ok(created);
        }
        // PUT: api/loans/{id}
        [HttpPut("id:int")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateLoanDto dto)
        {
            var updated = await _loanService.UpdateAsync(dto);
            if (updated.Data == null)
            {
                return BadRequest(new { message = updated.Message });
            }
            return Ok(updated);
        }
        // DELETE: api/loans/{id}
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _loanService.DeleteAsync(id);
            if (deleted.Data == null)
            {
                return BadRequest(new { message = deleted.Message });
            }
            return Ok(deleted);
        }
        
        // GET: api/loans/with-books
        [HttpGet("with-books")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllIncludingReturned()
        {
            var result = await _loanService.GetAllWithIncludeAsync();
            if (result.Data == null)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result);
        }

        // PUT: api/loans/return
        [HttpPut("return")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Return([FromBody] ReturnLoanDto dto)
        {
            var returned = await _loanService.ReturnAsync(dto);
            if (returned.Data == null)
            {
                return BadRequest(new { message = returned.Message });
            }
            return Ok(returned);
        }



    }
}
