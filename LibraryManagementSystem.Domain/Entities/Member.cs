namespace LibraryManagementSystem.Domain.Entities
{
    public class Member
    {
        public int Id { get; private set; } 
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public DateTime MemberSince { get; private set; }
        public bool IsActive { get; private set; }

        public string FullName => $"{FirstName} {LastName}";

        public Member(string firstName, string lastName, string email, string phoneNumber)
        { 
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            MemberSince = DateTime.Now;
            IsActive = true;
        }

        private Member () { }

        public void Update(string firstName, string lastName, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void Deactivate()
        {
            if(!IsActive) throw new InvalidOperationException("Member is already inactive.");
            IsActive = false;
        }

        public void Activate()
        {
            if (IsActive) throw new InvalidOperationException("Member is already active.");
            IsActive = true;
        }
    }
}
