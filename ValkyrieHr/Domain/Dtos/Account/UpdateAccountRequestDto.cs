using System.ComponentModel.DataAnnotations;

namespace ValkyrieHr.Domain.Dtos.Account
{
    public class UpdateAccountRequestDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid GenderId { get; set; }
        public string Address { get; set; }
        public string JobTitle { get; set; }
        public double? Salary { get; set; }

    }
}
