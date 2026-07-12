using LibraryManagementSystem.Application.DTOs.Member;
using LibraryManagementSystem.Domain.Interfaces;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _members;
        private readonly IUnitOfWork _uow;

        public MemberService(IMemberRepository members, IUnitOfWork uow)
        {
            _members = members;
            _uow = uow;
        }

        public async Task<IEnumerable<MemberDto>> GetAllAsync()
        {
            return (await _members.GetAllAsync()).Select(m => new MemberDto
            {
                Id = m.Id,
                FullName = m.FullName,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber,
                IsActive = m.IsActive,
            });
        }

        public async Task<MemberDto> GetByIdAsync(int id)
        {
            var member = await _members.GetByIdAsync(id);
            if(member == null) return null;

            return new MemberDto
            {
                Id = member.Id,
                FullName = member.FullName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                IsActive =member.IsActive,
            };
        }

        public async Task AddMemberAsync(MemberCreateUpdateDto dto)
        {
            var existing = await _members.GetByEmailAsync(dto.Email);
            if (existing != null) throw new InvalidOperationException("A member with this email already exists.");

            var member = new Member(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);

            await _members.AddAsync(member);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateMemberAsync(int id, MemberCreateUpdateDto dto)
        {
            var member = await _members.GetByIdAsync(id) ?? throw new Exception("Member not found.");
            
            member.Update(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);
            await _members.UpdateAsync(member);
            await _uow.SaveChangesAsync();
        }

        public async Task DeactivateMemberAsync(int id)
        {
            var member = await _members.GetByIdAsync(id) ?? throw new Exception("Member not found.");

            member.Deactivate();
            await _members.UpdateAsync(member);
            await _uow.SaveChangesAsync();
        }

        public async Task ActivateMemberAsync(int id)
        {
            var member = await _members.GetByIdAsync(id) ?? throw new Exception("Member not found.");

            member.Activate();
            await _members.UpdateAsync(member);
            await _uow.SaveChangesAsync();
        }
    }
}
