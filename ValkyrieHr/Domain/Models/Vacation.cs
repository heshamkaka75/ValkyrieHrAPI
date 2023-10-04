using System.ComponentModel.DataAnnotations;

namespace ValkyrieHr.Domain.Models
{
    public class Vacation
    {
        public Vacation()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
        [Key]
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? VacationTypeId { get; set; }
        public int Duration { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public VacationType VacationType { get; set; }
        public Employee Employee { get; set; }
    }
}
