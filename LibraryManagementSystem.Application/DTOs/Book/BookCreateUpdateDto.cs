namespace LibraryManagementSystem.Application.DTOs.Books
{
    public class BookCreateUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int TotalCopies { get; set; }
    }
}
