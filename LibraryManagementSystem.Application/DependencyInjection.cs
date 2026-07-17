using FluentValidation;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Application.Validators.Auth;
using LibraryManagementSystem.Application.Validators.Book;
using LibraryManagementSystem.Application.Validators.Borrow;
using LibraryManagementSystem.Application.Validators.Member;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBorrowService, BorrowService>();
            services.AddScoped<IMemberService, MemberService>();

            // Validators
            services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<BookCreateUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<ExtendDueDateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<MemberUpdateDtoValidator>();

            return services;
        }
    }
}
