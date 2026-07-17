using FluentValidation;
using LibraryManagementSystem.Application.DTOs.Auth;

namespace LibraryManagementSystem.Application.Validators.Auth
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator() 
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

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(7).WithMessage("Password must be atleast 7 characters")
                .Matches("[A-Z]").WithMessage("Password must contain atleast 1 uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain atleast 1 lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain atleast 1 digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain atleast 1 special");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{11}$").WithMessage("Please enter a valid 11 digit phone number.");
        }
    }
}
