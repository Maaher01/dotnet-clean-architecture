namespace LibraryManagementSystem.Application.DTOs.Borrow
{
    public class BorrowRequestDto
    {
        public int? MemberId { get; set; } 
        public int BookId { get; set; }
    }
}
