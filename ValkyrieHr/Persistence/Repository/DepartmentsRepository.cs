
using ValkyrieHr.Domain.Models;
using ValkyrieHr.Models;

namespace ValkyrieHr.Persistence.Repository
{
    public interface IDepartmentsRepository : IBaseRepository<Department>
    {
        /// Add other interface here
    }

    #region Implementation
    public class DepartmentsRepository : BaseRepository<Department>, IDepartmentsRepository
    {
        public DepartmentsRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        /// Interface implemenation
    }
    #endregion
}
