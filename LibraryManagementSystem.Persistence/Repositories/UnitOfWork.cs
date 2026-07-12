using LibraryManagementSystem.Domain.Interfaces;

namespace LibraryManagementSystem.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
