using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Interfaces
{
    public interface IBorrowRepository
    {
        Task<IEnumerable<Borrow>> GetAllAsync();
        Task<IEnumerable<Borrow>> GetCurrent();
        Task<IEnumerable<Borrow>> GetOverdueAsync();
        Task<Borrow?> GetByIdAsync(int id);
        Task<Borrow?> GetActiveBorrowAsync(int memberId, int bookId);
        Task AddAsync(Borrow borrow);
        Task UpdateAsync(Borrow borrow);
        Task<IEnumerable<Borrow>> GetByMemberIdAsync(int memberId);
        Task<IEnumerable<Borrow>> GetByBookIdAsync(int bookId);
    }
}
