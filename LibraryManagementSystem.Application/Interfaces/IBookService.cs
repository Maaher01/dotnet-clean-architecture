using LibraryManagementSystem.Application.DTOs.Books;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<IEnumerable<BookDto>> GetAvailableBooksAsync();
        Task AddBookAsync(BookCreateUpdateDto dto);
        Task UpdateBookAsync(int id, BookCreateUpdateDto dto);
        Task DeleteBookAsync(int id);
    }
}
