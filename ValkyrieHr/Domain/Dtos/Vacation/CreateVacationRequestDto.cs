namespace ValkyrieHr.Domain.Dtos.Vacation
{
    public class CreateVacationRequestDto
    {
        public int NumberOfDays { get; set; }
        public int DaysLeft { get; set; }
        public Guid? VacationTypeId { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}
