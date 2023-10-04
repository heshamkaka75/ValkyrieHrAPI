namespace ValkyrieHr.Domain.Models
{
    public class Department
    {
        public Department()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public List<Employee> Employee { get; set; }
    }
}
