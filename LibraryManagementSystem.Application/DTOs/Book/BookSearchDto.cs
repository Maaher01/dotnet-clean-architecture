namespace LibraryManagementSystem.Application.DTOs.Book
{
    public class BookSearchDto
    {
        public string? Keyword { get; set; }
        public string? Author { get; set; }
        public bool? AvailableOnly { get; set; }
    }
}
