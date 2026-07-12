using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Persistence.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int MemberId { get; set; }
        public Member? Member { get; set; }
    }
}
