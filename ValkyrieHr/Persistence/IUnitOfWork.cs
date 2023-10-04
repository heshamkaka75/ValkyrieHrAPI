using ValkyrieHr.Persistence.Repository;

namespace ValkyrieHr.Persistence
{
    public interface IUnitOfWork
    {
        #region Settings 
        IGenderRepository Genders { get; }
        IEmployeeRepository Employees { get; }
        IDepartmentsRepository Departments { get; }
        IVacationRepository Vacations { get; }
        IVacationBalanceRepository VacationBalances { get; }
        IVacationTypeRepository VacationTypes { get; }

        #endregion
        void Commit();
        Task CommitAsync();
    }
}

