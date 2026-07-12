namespace LibraryManagementSystem.Application.DTOs.Borrow
{
    public class BorrowDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public bool IsOverdue { get; set; }
    }
}
