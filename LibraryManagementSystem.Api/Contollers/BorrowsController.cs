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

        [HttpGet]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetAllBorrows()
        {
            var allBorrows = await _borrowService.GetAllBorrowsAsync();
            return Ok(allBorrows);
        }

        [HttpGet("{borrowId}")]
        [Authorize]
        public async Task<IActionResult> GetBorrowById(int borrowId)
        {
            var borrow = await _borrowService.GetBorrowByIdAsync(borrowId);
            return Ok(borrow);
        }

        [HttpGet("current")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetCurrentBorrows()
        {
            var currentBorrows = await _borrowService.GetCurrentBorrowsAsync();
            return Ok(currentBorrows);
        }

        [HttpGet("overdue")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetOverdueBorrows()
        {
            var overdueBorrows = await _borrowService.GetOverdueBorrowsAsync();
            return Ok(overdueBorrows);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMemberBorrows()
        {
            var memberId = User.FindFirstValue("memberId");
            if(memberId == null) return Unauthorized();

            var memberBorrows = await _borrowService.GetMyBorrowsAsync(int.Parse(memberId));
            return Ok(memberBorrows);
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

        [HttpPut("{id}/extend")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> ExtendDueDate(int id, [FromBody] ExtendDueDateDto dto)
        {
            await _borrowService.ExtendDueDateAsync(id, dto.NewDueDate);
            return Ok();
        }

        [HttpGet("member/{memberId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetByMember(int memberId)
        {
            var borrows = await _borrowService.GetByMemberIdAsync(memberId);
            return Ok(borrows);
        }

        [HttpGet("book/{bookId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetByBookId(int bookId)
        {
            var borrows = await _borrowService.GetByBookIdAsync(bookId);
            return Ok(borrows);
        }
    }
}
