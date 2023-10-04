using System.ComponentModel.DataAnnotations;
using ValkyrieHr.Domain.Models;

namespace ValkyrieHr.Models
{
    public class Gender
    {
        public Gender()
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
        public List<ApplicationUser> ApplicationUser { get; set; }
        public List<Employee> Employee { get; set; }
    }
}
