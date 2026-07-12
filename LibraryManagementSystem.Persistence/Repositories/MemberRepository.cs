using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext _context;

        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Member?> GetByIdAsync(int id) => await _context.Members.FindAsync(id);
        public async Task<Member?> GetByEmailAsync(string email) => await _context.Members.FirstOrDefaultAsync(m => m.Email == email);
        public async Task<IEnumerable<Member>> GetAllAsync() => await _context.Members.ToListAsync();
        public async Task AddAsync(Member member) => await _context.Members.AddAsync(member);
        public async Task UpdateAsync(Member member) => _context.Members.Update(member);
        public async Task DeleteAsync(Member member) => _context.Members.Remove(member);
    }
}
