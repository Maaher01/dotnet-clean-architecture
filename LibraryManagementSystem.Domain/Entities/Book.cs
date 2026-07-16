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
            if (totalCopies < 0) throw new InvalidOperationException("Total copies cannot be negative.");

            if (totalCopies < (TotalCopies - AvailableCopies)) 
                throw new InvalidOperationException($"Cannot reduce total copies below the number currently borrowed. " +
                    $"{TotalCopies - AvailableCopies} copies are currently out on loan.");

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
