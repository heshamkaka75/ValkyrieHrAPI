using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace ValkyrieHr.Domain.Models
{
    public class VacationBalance
    {
        public VacationBalance()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
        [Key]
        public Guid Id { get; set; }
        public int NumberOfDays { get; set; }
        public int DaysLeft { get; set; }
        public Guid? VacationTypeId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public Employee Employee { get; set; }
        public VacationType VacationType { get; set; }
    }
}