using LibraryManagementSystem.Application.DTOs.Books;
using LibraryManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("available")]
        [Authorize]
        public async Task<IActionResult> GetAvailable()
        {
            var books = await _bookService.GetAvailableBooksAsync();
            return Ok(books);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> AddBook([FromBody] BookCreateUpdateDto dto)
        {
            await _bookService.AddBookAsync(dto);
            return CreatedAtAction(nameof(GetAll), null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookCreateUpdateDto dto)
        {
            await _bookService.UpdateBookAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
