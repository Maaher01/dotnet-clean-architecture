namespace LibraryManagementSystem.Domain.Entities
{
    public class Book
    {
        public int Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Author { get; private set; } = string.Empty;
        public int TotalCopies { get; private set; }
        public int AvailableCopies { get; private set; }

        public Book(string title, string author, int totalCopies)
        {
            Title = title;
            Author = author;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
        }

        public Book() { }

        public void Update(string title, string author, int totalCopies)
        {
            Title = title;
            Author = author;
            TotalCopies = totalCopies;
        }

        public bool IsAvailable() => AvailableCopies > 0;

        public void Borrow()
        {
            if (!IsAvailable()) throw new InvalidOperationException("No copies available.");
            AvailableCopies--;
        }

        public void Return() => AvailableCopies++;
    }
}
