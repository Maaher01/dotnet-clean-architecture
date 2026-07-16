using LibraryManagementSystem.Domain.Interfaces;
using LibraryManagementSystem.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config)
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
