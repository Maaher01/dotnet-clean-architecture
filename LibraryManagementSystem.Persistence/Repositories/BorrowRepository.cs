using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Persistence.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Borrow>> GetOverdueAsync()
        {
            var today = DateTime.UtcNow;

            return await _context.Borrows
                .Where(b => b.DueDate < today && b.ReturnedAt == null)
                .ToListAsync();
        }

        public async Task<Borrow?> GetActiveBorrowAsync(int memberId, int bookId)
        {
            return await _context.Borrows
                .FirstOrDefaultAsync(b => b.MemberId == memberId && b.BookId == bookId && b.ReturnedAt == null);
        }

        public async Task<Borrow?> GetByIdAsync(int id)
        {
            return await _context.Borrows.FindAsync(id);
        }

        public async Task AddAsync(Borrow borrow)
        {
            await _context.Borrows.AddAsync(borrow);
        }

        public async Task UpdateAsync(Borrow borrow)
        {
            _context.Borrows.Update(borrow);
        }
    }
}
