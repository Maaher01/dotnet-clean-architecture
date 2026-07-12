using LibraryManagementSystem.Domain.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Application.DTOs.Books;

namespace LibraryManagementSystem.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _books;
        private readonly IUnitOfWork _uow;

        public BookService(IBookRepository books, IUnitOfWork uow)
        {
            _books = books;
            _uow = uow;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            return (await _books.GetAllAsync()).Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                TotalCopies = b.TotalCopies,
                AvailableCopies = b.AvailableCopies
            });
        }

        public async Task<IEnumerable<BookDto>> GetAvailableBooksAsync()
        {
            return (await _books.GetAvailableAsync()).Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                TotalCopies = b.TotalCopies,
                AvailableCopies = b.AvailableCopies
            });
        }

        public async Task AddBookAsync(BookCreateUpdateDto dto)
        {
           await _books.AddAsync(new Book(dto.Title, dto.Author, dto.TotalCopies));
           await _uow.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(int id, BookCreateUpdateDto dto)
        {
            var book = await _books.GetByIdAsync(id) ?? throw new Exception("Book not found.");

            book.Update(dto.Title, dto.Author, dto.TotalCopies);
            await _books.UpdateAsync(book);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _books.GetByIdAsync(id) ?? throw new Exception("Book not found.");

            await _books.DeleteAsync(book);
            await _uow.SaveChangesAsync();
        }
    }
}
