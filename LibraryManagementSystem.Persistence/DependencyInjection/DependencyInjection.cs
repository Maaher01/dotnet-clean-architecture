using LibraryManagementSystem.Domain.Interfaces;
using LibraryManagementSystem.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Persistence.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenseServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("LibraryDBCon"))
            );

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBorrowRepository, BorrowRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
