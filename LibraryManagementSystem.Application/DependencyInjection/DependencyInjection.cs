using LibraryManagementSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBorrowService, BorrowService>();
            services.AddScoped<IMemberService, MemberService>();

            return services;
        }
    }
}
