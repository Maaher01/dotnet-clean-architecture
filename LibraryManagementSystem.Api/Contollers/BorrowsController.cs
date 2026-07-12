using LibraryManagementSystem.Application.DTOs.Borrow;
using LibraryManagementSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowsController : ControllerBase
    {
        private readonly IBorrowService _borrowService;

        public BorrowsController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueBorrows()
        {
            var overdueBorrows = await _borrowService.GetOverdueBorrowsAsync();
            return Ok(overdueBorrows);
        }

        [HttpPost]
        public async Task<IActionResult> Borrow([FromBody] BorrowRequestDto dto)
        {
            await _borrowService.BorrowBookAsync(dto.MemberId, dto.BookId);
            return Ok();
        }

        [HttpPost("return")]
        public async Task<IActionResult> Return([FromBody] BorrowRequestDto dto)
        {
            await _borrowService.ReturnBookAsync(dto.MemberId, dto.BookId);
            return Ok();
        }
    }
}
