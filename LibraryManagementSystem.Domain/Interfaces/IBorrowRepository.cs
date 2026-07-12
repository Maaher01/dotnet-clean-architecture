using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Interfaces
{
    public interface IBorrowRepository
    {
        Task<IEnumerable<Borrow>> GetOverdueAsync();
        Task<Borrow?> GetActiveBorrowAsync(int memberId, int bookId);
        Task AddAsync(Borrow borrow);
        Task UpdateAsync(Borrow borrow);
    }
}
