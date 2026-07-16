using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.DTOs.Auth;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces;
using LibraryManagementSystem.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _users;
        private readonly IMemberRepository _members;
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> users, IMemberRepository members, IUnitOfWork uow, ITokenService tokenService)
        {
            _users = users;
            _members = members;
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _users.FindByEmailAsync(dto.Email);
            if (existingUser != null) throw new InvalidOperationException("Email address already in use.");

            var member = new Member(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);
            await _members.AddAsync(member);
            await _uow.SaveChangesAsync();

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                MemberId = member.Id
            };

            var result = await _users.CreateAsync(user, dto.Password);
            if (!result.Succeeded) throw new InvalidOperationException(result.Errors.First().Description);

            const string defaultRole = "Member";
            var roleResult = await _users.AddToRoleAsync(user, defaultRole);
            if(!roleResult.Succeeded) throw new InvalidOperationException(roleResult.Errors.First().Description);

            return new AuthResponseDto
            {
                Token = await _tokenService.GenerateToken(user),
                Email = dto.Email,
                FullName = member.FullName,
                Role = defaultRole
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _users.FindByEmailAsync(dto.Email) ?? throw new InvalidOperationException("Invalid email or password.");

            var passwordValid = await _users.CheckPasswordAsync(user, dto.Password);
            if (!passwordValid) throw new InvalidOperationException("Invalid email or password");

            string? fullName = null;
            if (user.MemberId.HasValue)
            {
                var member = await _members.GetByIdAsync(user.MemberId.Value);
                fullName = member?.FullName;
            }

            return new AuthResponseDto
            {
                Token = await _tokenService.GenerateToken(user),
                Email = dto.Email,
                FullName = fullName ?? user.UserName ?? string.Empty,
                Role = (await _users.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty
            };
        }

        public async Task UpdateUserRole(string userId, string role)
        {
            var user = await _users.FindByIdAsync(userId) ?? throw new InvalidOperationException("User not found.");

            var currentRole = await _users.GetRolesAsync(user);
            await _users.RemoveFromRolesAsync(user, currentRole);
            await _users.AddToRoleAsync(user, role);
        }
    }
}
