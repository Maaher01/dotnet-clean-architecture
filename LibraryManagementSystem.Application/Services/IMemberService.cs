using LibraryManagementSystem.Application.DTOs.Member;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetAllAsync();
        Task <MemberDto> GetByIdAsync(int id);
        Task AddMemberAsync(MemberCreateUpdateDto dto);
        Task UpdateMemberAsync(int id, MemberCreateUpdateDto dto);
        Task DeactivateMemberAsync(int id);
        Task ActivateMemberAsync(int id);
    }
}
