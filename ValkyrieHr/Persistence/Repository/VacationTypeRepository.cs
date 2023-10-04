
using ValkyrieHr.Domain.Models;
using ValkyrieHr.Models;

namespace ValkyrieHr.Persistence.Repository
{
    public interface IVacationTypeRepository : IBaseRepository<VacationType>
    {
        /// Add other interface here
    }

    #region Implementation
    public class VacationTypeRepository : BaseRepository<VacationType>, IVacationTypeRepository
    {
        public VacationTypeRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        /// Interface implemenation
    }
    #endregion
}
