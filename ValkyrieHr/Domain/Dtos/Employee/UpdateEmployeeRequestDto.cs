namespace ValkyrieHr.Domain.Dtos.Employee
{
    public class UpdateEmployeeRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid GenderId { get; set; }
        public string PhoneNo { get; set; }
        public int Salary { get; set; }

    }
}
