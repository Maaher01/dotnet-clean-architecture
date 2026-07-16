using LibraryManagementSystem.Application.DTOs.Member;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetAllAsync();
        Task <MemberDto> GetByIdAsync(int id);
        Task UpdateMemberAsync(int id, MemberUpdateDto dto);
        Task DeactivateMemberAsync(int id);
        Task ActivateMemberAsync(int id);
    }
}
