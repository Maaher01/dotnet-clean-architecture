namespace LibraryManagementSystem.Domain.Entities
{
    public class Borrow
    {
        public int Id { get; private set; }
        public int BookId { get; private set; }
        public int MemberId { get; private set; }
        public DateTime BorrowedAt { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? ReturnedAt { get; private set; }

        public bool IsReturned() => ReturnedAt.HasValue;
        public bool IsOverdue() => !IsReturned() && DateTime.UtcNow > DueDate;

        public Borrow(int memberId, int bookId, DateTime borrowedAt, DateTime dueDate)
        {
            MemberId = memberId;
            BookId = bookId;
            BorrowedAt = borrowedAt;
            DueDate = dueDate;
        }

        private Borrow() { }

        public void Return()
        {
            if (IsReturned()) throw new InvalidOperationException("This book has already been returned.");
            ReturnedAt = DateTime.UtcNow;
        }
    }
}
