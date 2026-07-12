using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Persistence.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Persistence
{
    public class LibraryDbContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) {}

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Borrow> Borrows => Set<Borrow>();
        public DbSet<Member> Members => Set<Member>();
    }
}
