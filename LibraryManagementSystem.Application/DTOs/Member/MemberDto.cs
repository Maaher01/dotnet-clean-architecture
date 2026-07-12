namespace LibraryManagementSystem.Application.DTOs.Member
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime MemberSince { get; set; }
        public bool IsActive { get; set; }
    }
}
