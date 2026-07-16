using LibraryManagementSystem.Application.DTOs.Borrow;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IBorrowService
    {
        Task BorrowBookAsync(int memberId, int bookId);
        Task ReturnBookAsync(int memberId, int bookId);
        Task<IEnumerable<BorrowDto>> GetOverdueBorrowsAsync();
        Task ExtendDueDateAsync(int borrowId, DateTime newDueDate);
    }
}
