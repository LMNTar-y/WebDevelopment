using WebDevelopment.Domain.IRepos;

namespace WebDevelopment.Domain;

public interface IUnitOfWork
{
    public ICountryRepo CountryRepo { get; }
    public IDepartmentRepo DepartmentRepo { get; }
    public IPositionRepo PositionRepo { get; }
    public IUserRepo UserRepo { get; }
    public ISalaryRangeRepo SalaryRangeRepo { get; }
    public ITaskRepo TaskRepo { get; }
    public IUserPositionRepo UserPositionRepo { get; }
    public IUserSalaryRepo UserSalaryRepo { get; }
    public IUserTaskRepo UserTaskRepo { get; }
}