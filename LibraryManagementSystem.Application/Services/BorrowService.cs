using LibraryManagementSystem.Application.DTOs.Borrow;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces;

namespace LibraryManagementSystem.Application.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBookRepository _books;
        private readonly IBorrowRepository _borrows;
        private readonly IUnitOfWork _uow;

        public BorrowService(IBookRepository books, IBorrowRepository borrows, IUnitOfWork uow)
        {
            _books = books;
            _borrows = borrows;
            _uow = uow;
        }

        public async Task BorrowBookAsync(int memberId, int bookId)
        {
            var book = await _books.GetByIdAsync(bookId) ?? throw new Exception("Book not found.");

            book.Borrow();

            await _books.UpdateAsync(book);
            var borrow = new Borrow(memberId, bookId, DateTime.UtcNow, DateTime.UtcNow.AddDays(14));

            await _borrows.AddAsync(borrow);
            await _uow.SaveChangesAsync();
        }

        public async Task ReturnBookAsync(int memberId, int bookId)
        {
            var book = await _books.GetByIdAsync(bookId) ?? throw new Exception("Book not found.");
            var borrow = await _borrows.GetActiveBorrowAsync(memberId, bookId) ?? throw new Exception("Borrow record not found.");

            book.Return();
            borrow.Return();

            await _books.UpdateAsync(book);
            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<BorrowDto>> GetOverdueBorrowsAsync()
        {
            return (await _borrows.GetOverdueAsync()).Select(b => new BorrowDto
            {
                Id = b.Id,
                BookId = b.BookId,
                MemberId = b.MemberId,
                BorrowedAt = b.BorrowedAt,
                DueDate = b.DueDate,
                ReturnedAt = b.ReturnedAt,
                IsOverdue = b.IsOverdue()
            });
        }
    }
}
