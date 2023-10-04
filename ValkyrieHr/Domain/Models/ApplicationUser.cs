using Microsoft.AspNetCore.Identity;

namespace ValkyrieHr.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public Guid GenderId { get; set; }
        public string? Address { get; set; }
        public double? Salary { get; set; }
        public bool IsActive { get; set; }
        public string JobTitle { get; set; }
        public string? ProfileImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Gender Gender { get; set; }
    }
}
