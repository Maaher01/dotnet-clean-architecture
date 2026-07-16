using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBorrowService, BorrowService>();
            services.AddScoped<IMemberService, MemberService>();

            return services;
        }
    }
}
