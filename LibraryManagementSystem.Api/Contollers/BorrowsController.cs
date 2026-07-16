using LibraryManagementSystem.Application.DTOs.Borrow;
using LibraryManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetOverdueBorrows()
        {
            var overdueBorrows = await _borrowService.GetOverdueBorrowsAsync();
            return Ok(overdueBorrows);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Borrow([FromBody] BorrowRequestDto dto)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var memberId = User.FindFirstValue("memberId");

            if (userRole == "Member")
            {
                if (memberId == null) return Unauthorized();

                await _borrowService.BorrowBookAsync(int.Parse(memberId!), dto.BookId);
            }
            else
            {
                await _borrowService.BorrowBookAsync(dto.MemberId!.Value, dto.BookId);
            }

            return Ok();
        }

        [HttpPost("return")]
        [Authorize]
        public async Task<IActionResult> Return([FromBody] BorrowRequestDto dto)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var memberId = User.FindFirstValue("memberId");

            if (userRole == "Member")
            {
                if (memberId == null) return Unauthorized();

                await _borrowService.ReturnBookAsync(int.Parse(memberId!), dto.BookId);
            }
            else
            {
                await _borrowService.ReturnBookAsync(dto.MemberId!.Value, dto.BookId);
            }

            return Ok();
        }
    }
}
