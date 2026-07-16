using LibraryManagementSystem.Persistence.Identity;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
