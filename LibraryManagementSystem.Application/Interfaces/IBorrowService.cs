using LibraryManagementSystem.Application.DTOs.Borrow;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IBorrowService
    {
        Task<IEnumerable<BorrowDto>> GetAllBorrowsAsync();
        Task<BorrowDto> GetBorrowByIdAsync(int borrowId);
        Task<IEnumerable<BorrowDto>> GetCurrentBorrowsAsync();
        Task<IEnumerable<BorrowDto>> GetOverdueBorrowsAsync();
        Task<IEnumerable<BorrowDto>> GetMyBorrowsAsync(int memberId);
        Task BorrowBookAsync(int memberId, int bookId);
        Task ReturnBookAsync(int memberId, int bookId);
        Task ExtendDueDateAsync(int borrowId, DateTime newDueDate);
        Task<IEnumerable<BorrowDto>> GetByBookIdAsync(int bookId);
        Task<IEnumerable<BorrowDto>> GetByMemberIdAsync(int memberId);
    }
}
