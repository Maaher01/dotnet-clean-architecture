using FluentValidation;
using LibraryManagementSystem.Application.DTOs.Member;

namespace LibraryManagementSystem.Application.Validators.Member
{
    public class MemberUpdateDtoValidator : AbstractValidator<MemberUpdateDto>
    {
        public MemberUpdateDtoValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email address");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{11}$").WithMessage("Please enter a valid 11 digit phone number.");
        }
    }
}
