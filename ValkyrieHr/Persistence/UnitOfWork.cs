using ValkyrieHr.Persistence.Repository;

namespace ValkyrieHr.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        #region Settings 
        public IGenderRepository Genders { get; private set; }
        public IEmployeeRepository Employees { get; private set; }
        public IDepartmentsRepository Departments { get; private set; }
        public IVacationRepository Vacations { get; private set; }
        public IVacationBalanceRepository VacationBalances { get; private set; }
        public IVacationTypeRepository VacationTypes { get; private set; }

        #endregion
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
            Genders = new GenderRepository(_context);
            Employees = new EmployeeRepository(_context);
            Departments = new DepartmentsRepository(_context);
            Vacations = new VacationRepository(_context);
            VacationBalances = new VacationBalanceRepository(_context);
            VacationTypes = new VacationTypeRepository(_context);
        }

        public void Commit()
        {
            this._context.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await this._context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
