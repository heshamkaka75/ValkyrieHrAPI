namespace ValkyrieHr.Domain.Dtos.Employee
{
    public class CreateEmployeeRequestDto
    {
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid GenderId { get; set; }
        public string MotherName { get; set; }
        public string NationalID { get; set; }
        public string PhoneNo { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }


    }
}
