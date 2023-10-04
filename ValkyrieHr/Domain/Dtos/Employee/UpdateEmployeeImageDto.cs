namespace ValkyrieHr.Domain.Dtos.Employee
{
    public class UpdateEmployeeImageDto
    {
        public Guid Id { get; set; }
        public IFormFile EmpImage { get; set; }
    }
}
