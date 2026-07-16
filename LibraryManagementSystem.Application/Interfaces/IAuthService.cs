using LibraryManagementSystem.Application.DTOs.Auth;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task UpdateUserRole(string userId, string role);
    }
}
