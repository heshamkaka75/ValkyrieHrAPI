using System.ComponentModel.DataAnnotations;

namespace ValkyrieHr.Domain.Models
{
    public class VacationType
    {
        public VacationType()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public List<VacationBalance> VacationBalance { get; set; }
        public List<Vacation> Vacation { get; set; }
    }
}