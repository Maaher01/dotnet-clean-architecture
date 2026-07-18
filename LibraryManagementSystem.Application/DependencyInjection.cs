using FluentValidation;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Application.Validators.Auth;
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

            // Validator
            services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

            return services;
        }
    }
}
