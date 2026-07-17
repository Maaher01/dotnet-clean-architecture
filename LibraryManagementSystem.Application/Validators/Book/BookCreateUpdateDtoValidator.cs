using FluentValidation;
using LibraryManagementSystem.Application.DTOs.Books;

namespace LibraryManagementSystem.Application.Validators.Book
{
    public class BookCreateUpdateDtoValidator : AbstractValidator<BookCreateUpdateDto>
    {
        public BookCreateUpdateDtoValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required")
                .MaximumLength(100).WithMessage("Author cannot exceed 100 characters.");

            RuleFor(x => x.TotalCopies)
                .GreaterThan(0).WithMessage("Total copies must be atleast 1");
        }
    }
}
