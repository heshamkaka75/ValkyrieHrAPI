using System.ComponentModel.DataAnnotations;

namespace ValkyrieHr.Domain.Dtos.Account
{
    public class CreateUserRequestDto
    {
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public Guid GenderId { get; set; }
        public string Address { get; set; }
        public string JobTitle { get; set; }
        public string? ProfileImage { get; set; }
        public string Password { get; set; }
        public string ConfirmePassword { get; set; }
        public double? Salary { get; set; }

    }
}
