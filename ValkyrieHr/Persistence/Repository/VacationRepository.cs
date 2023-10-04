
using ValkyrieHr.Domain.Models;
using ValkyrieHr.Models;

namespace ValkyrieHr.Persistence.Repository
{
    public interface IVacationRepository : IBaseRepository<Vacation>
    {
        /// Add other interface here
    }

    #region Implementation
    public class VacationRepository : BaseRepository<Vacation>, IVacationRepository
    {
        public VacationRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        /// Interface implemenation
    }
    #endregion
}
