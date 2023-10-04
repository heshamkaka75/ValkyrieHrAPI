namespace ValkyrieHr.Domain.Dtos.Vacation
{
    public class CreateEmployeeVacationDto
    {
        public Guid? EmployeeId { get; set; }
        public Guid? VacationTypeId { get; set; }
        public int Duration { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
