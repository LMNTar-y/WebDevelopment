using WebDevelopment.Domain;
using WebDevelopment.Domain.IRepos;
using WebDevelopment.Infrastructure.Repos;

namespace WebDevelopment.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly WebDevelopmentContext _context;
    private ICountryRepo? _countryRepo;
    private IDepartmentRepo? _departmentRepo;
    private IPositionRepo? _positionRepo;
    private IUserRepo? _userRepo;
    private ISalaryRangeRepo? _salaryRangeRepo;
    private TaskRepo? _taskRepo;
    private UserPositionRepo? _userPositionRepo;
    private UserSalaryRepo? _userSalaryRepo;
    private UserTaskRepo? _userTaskRepo;

    public UnitOfWork(WebDevelopmentContext context)
    {
        _context = context;
    }

    public ICountryRepo CountryRepo
    {
        get
        {
            if (_countryRepo == null)
            {
                _countryRepo = new CountryRepo(_context);
            }

            return _countryRepo;
        }
    }

    public IDepartmentRepo DepartmentRepo
    {
        get
        {
            if (_departmentRepo == null)
            {
                _departmentRepo = new DepartmentRepo(_context);
            }

            return _departmentRepo;
        }
    }
    public IPositionRepo PositionRepo
    {
        get
        {
            if (_positionRepo == null)
            {
                _positionRepo = new PositionRepo(_context);
            }

            return _positionRepo;
        }
    }
    public IUserRepo UserRepo
    {
        get
        {
            if (_userRepo == null)
            {
                _userRepo = new UserRepo(_context);
            }

            return _userRepo;
        }
    }

    public ISalaryRangeRepo SalaryRangeRepo
    {
        get
        {
            if (_salaryRangeRepo == null)
            {
                _salaryRangeRepo = new SalaryRangeRepo(_context);
            }

            return _salaryRangeRepo;
        }
    }

    public ITaskRepo TaskRepo
    {
        get
        {
            if (_taskRepo == null)
            {
                _taskRepo = new TaskRepo(_context);
            }

            return _taskRepo;
        }
    }

    public IUserPositionRepo UserPositionRepo
    {
        get
        {
            if (_userPositionRepo == null)
            {
                _userPositionRepo = new UserPositionRepo(_context);
            }

            return _userPositionRepo;
        }
    }

    public IUserSalaryRepo UserSalaryRepo
    {
        get
        {
            if (_userSalaryRepo == null)
            {
                _userSalaryRepo = new UserSalaryRepo(_context);
            }

            return _userSalaryRepo;
        }
    }

    public IUserTaskRepo UserTaskRepo
    {
        get
        {
            if (_userTaskRepo == null)
            {
                _userTaskRepo = new UserTaskRepo(_context);
            }

            return _userTaskRepo;
        }
    }
}