
using Microsoft.EntityFrameworkCore;
using ValkyrieHr.Domain.Models;
using ValkyrieHr.Models;

namespace ValkyrieHr.Persistence.Repository
{
    public interface IVacationBalanceRepository : IBaseRepository<VacationBalance>
    {
        /// Add other interface here
        Task<IEnumerable<VacationBalance>> GetEmpWithDepAsync();
    }

    #region Implementation
    public class VacationBalanceRepository : BaseRepository<VacationBalance>, IVacationBalanceRepository
    {
        public VacationBalanceRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        /// Interface implemenation
        public async Task<IEnumerable<VacationBalance>> GetEmpWithDepAsync()
        {
            return await this.DbSet.AsNoTracking().Include(s => s.Employee).ThenInclude(d=>d.Department).Include(v=>v.VacationType).ToListAsync();
        }
    }
    #endregion
}
