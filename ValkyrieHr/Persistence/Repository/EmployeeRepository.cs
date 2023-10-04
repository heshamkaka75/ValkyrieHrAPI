

using Microsoft.EntityFrameworkCore;
using ValkyrieHr.Domain.Models;

namespace ValkyrieHr.Persistence.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// Add other interface here
        Task<IEnumerable<Employee>> GetEmpWithDepAsync();
        Task<IEnumerable<Employee>> GetEmpWithVicAsync();
    }

    #region Implementation
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        /// Interface implemenation
        public async Task<IEnumerable<Employee>> GetEmpWithDepAsync()
        {
            return await this.DbSet.AsNoTracking().Include(s => s.Department).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmpWithVicAsync()
        {
            return await this.DbSet.AsNoTracking().Include(s => s.VacationBalance).ThenInclude(v=>v.VacationType).ToListAsync();
        }


    }
    #endregion
}
