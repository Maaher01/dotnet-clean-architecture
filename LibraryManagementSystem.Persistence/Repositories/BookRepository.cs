using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Book?> GetByIdAsync(int id) => await _context.Books.FindAsync(id);

        public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Books.ToListAsync();

        public async Task<IEnumerable<Book>> GetAvailableAsync() => await _context.Books.Where(b => b.AvailableCopies > 0).ToListAsync();

        public async Task AddAsync(Book book) => await _context.Books.AddAsync(book);
        
        public async Task UpdateAsync(Book book) => _context.Books.Update(book);

        public async Task DeleteAsync(Book book) => _context.Books.Remove(book);
    }
}
