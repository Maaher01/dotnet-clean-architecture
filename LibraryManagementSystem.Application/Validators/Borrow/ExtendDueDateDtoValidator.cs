using FluentValidation;
using LibraryManagementSystem.Application.DTOs.Borrow;

namespace LibraryManagementSystem.Application.Validators.Borrow
{
    public class ExtendDueDateDtoValidator : AbstractValidator<ExtendDueDateDto>
    {
        public ExtendDueDateDtoValidator() 
        {
            RuleFor(x => x.NewDueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("New due date must be in the future.");
        }
    }
}
