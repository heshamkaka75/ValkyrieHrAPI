
using Microsoft.EntityFrameworkCore;
using ValkyrieHr.Models;

namespace ValkyrieHr.Persistence.Repository
{
    public interface IGenderRepository : IBaseRepository<Gender>
    {
        /// Add other interface here
    }

    #region Implementation
    public class GenderRepository : BaseRepository<Gender>, IGenderRepository
    {
        public GenderRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        /// Interface implemenation
        
    }
    #endregion
}
