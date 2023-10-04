using ValkyrieHr.Models;

namespace ValkyrieHr.Domain.Models
{
    public class Employee
    {
        public Employee()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            DateOfBirth = new DateTime(1990, 11, 22);
            HireDate = new DateTime(2022, 04, 13);
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid GenderId { get; set; }
        public string MotherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalID { get; set; }
        public string PhoneNo { get; set; }
        public DateTime HireDate { get; set; }
        public string JobTitle { get; set; }
        public string? ProfileImage { get; set; }
        public int Salary { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public Department Department { get; set; }
        public Gender Gender { get; set; }
        public List<VacationBalance> VacationBalance { get; set; }
        public List<Vacation> Vacation { get; set; }
    }
}
