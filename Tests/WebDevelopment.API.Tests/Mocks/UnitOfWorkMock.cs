using Moq;
using WebDevelopment.Common.Requests.Country;
using WebDevelopment.Common.Requests.Department;
using WebDevelopment.Common.Requests.Position;
using WebDevelopment.Common.Requests.SalaryRange;
using WebDevelopment.Common.Requests.Task;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Common.Requests.UserPosition;
using WebDevelopment.Common.Requests.UserSalary;
using WebDevelopment.Common.Requests.UserTask;
using WebDevelopment.Domain;
using WebDevelopment.Domain.IRepos;

namespace WebDevelopment.API.Tests.Mocks;

public class UnitOfWorkMock : Mock<IUnitOfWork>
{

    public UnitOfWorkMock Setup_UOW()
    {
        SetupGet(u => u.UserRepo).Returns(new UserRepoMock().Setup().Object);
        SetupGet(u => u.CountryRepo).Returns(new CountryRepoMock().Setup().Object);
        SetupGet(u => u.DepartmentRepo).Returns(new DepartmentRepoMock().Setup().Object);
        SetupGet(u => u.PositionRepo).Returns(new PositionRepoMock().Setup().Object);
        SetupGet(u => u.SalaryRangeRepo).Returns(new SalaryRangeRepoMock().Setup().Object);
        SetupGet(u => u.TaskRepo).Returns(new TaskRepoMock().Setup().Object);
        SetupGet(u => u.UserPositionRepo).Returns(new UserPositionRepoMock().Setup().Object);
        SetupGet(u => u.UserSalaryRepo).Returns(new UserSalaryRepoMock().Setup().Object);
        SetupGet(u => u.UserTaskRepo).Returns(new UserTaskRepoMock().Setup().Object);
        return this;
    }
}

public class UserRepoMock : Mock<IUserRepo>
{

    public UserRepoMock Setup()
    {
        this.Setup(u => u.GetAllAsync(It.IsAny<string>())).ReturnsAsync(() => new List<IUserRequest>());
        this.Setup(us => us.UpdateAsync(It.IsAny<UserWithIdRequest>()));
        this.Setup(us => us.AddAsync(It.IsAny<NewUserRequest>()));
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i > 0))).ReturnsAsync(() => new UserWithIdRequest());
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i < 1))).Throws<ArgumentOutOfRangeException>();
        this.Setup(us => us.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(() => new UserWithIdRequest());
        return this;
    }
}

public class CountryRepoMock : Mock<ICountryRepo>
{

    public CountryRepoMock Setup()
    {
        this.Setup(u => u.GetAllAsync(It.IsAny<string>())).ReturnsAsync(() => new List<ICountryRequest>());
        this.Setup(us => us.UpdateAsync(It.IsAny<CountryWithIdRequest>()));
        this.Setup(us => us.AddAsync(It.IsAny<NewCountryRequest>()));
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i > 0))).ReturnsAsync(() => new CountryWithIdRequest());
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i < 1))).Throws<ArgumentOutOfRangeException>();
        this.Setup(us => us.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new CountryWithIdRequest());
        return this;
    }
}

public class DepartmentRepoMock : Mock<IDepartmentRepo>
{

    public DepartmentRepoMock Setup()
    {
        this.Setup(u => u.GetAllAsync(It.IsAny<string>())).ReturnsAsync(() => new List<IDepartmentRequest>());
        this.Setup(us => us.UpdateAsync(It.IsAny<DepartmentWithIdRequest>()));
        this.Setup(us => us.AddAsync(It.IsAny<NewDepartmentRequest>()));
        this.Setup(us => us.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => new DepartmentWithIdRequest());
        this.Setup(us => us.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new DepartmentWithIdRequest());
        return this;
    }
}
public class PositionRepoMock : Mock<IPositionRepo>
{

    public PositionRepoMock Setup()
    {
        this.Setup(u => u.GetAllAsync(It.IsAny<string>())).ReturnsAsync(() => new List<IPositionRequest>());
        this.Setup(us => us.UpdateAsync(It.IsAny<PositionWithIdRequest>()));
        this.Setup(us => us.AddAsync(It.IsAny<NewPositionRequest>()));
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i > 0))).ReturnsAsync(() => new PositionWithIdRequest());
        this.Setup(us => us.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new PositionWithIdRequest());
        return this;
    }
}

public class SalaryRangeRepoMock : Mock<ISalaryRangeRepo>
{

    public SalaryRangeRepoMock Setup()
    {
        this.Setup(u => u.GetAllAsync(It.IsAny<string>())).ReturnsAsync(() => new List<ISalaryRangeRequest>());
        this.Setup(us => us.UpdateAsync(It.IsAny<SalaryRangeWithIdRequest>()));
        this.Setup(us => us.AddAsync(It.IsAny<NewSalaryRangeRequest>()));
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i > 0))).ReturnsAsync(() => new SalaryRangeWithIdRequest());
        this.Setup(us => us.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new List<SalaryRangeWithIdRequest>());
        return this;
    }
}

public class TaskRepoMock : Mock<ITaskRepo>
{

    public TaskRepoMock Setup()
    {
        this.Setup(u => u.GetAllAsync(It.IsAny<string>())).ReturnsAsync(() => new List<ITaskRequest>());
        this.Setup(us => us.UpdateAsync(It.IsAny<TaskWithIdRequest>()));
        this.Setup(us => us.AddAsync(It.IsAny<NewTaskRequest>()));
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i > 0))).ReturnsAsync(() => new TaskWithIdRequest());
        this.Setup(us => us.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new TaskWithIdRequest());
        return this;
    }
}

public class UserPositionRepoMock : Mock<IUserPositionRepo>
{

    public UserPositionRepoMock Setup()
    {
        this.Setup(u => u.GetAllAsync(It.IsAny<string>())).ReturnsAsync(() => new List<IUserPositionRequest>());
        this.Setup(us => us.UpdateAsync(It.IsAny<UserPositionWithIdRequest>()));
        this.Setup(us => us.AddAsync(It.IsAny<NewUserPositionRequest>()));
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i > 0))).ReturnsAsync(() => new UserPositionWithIdRequest());
        this.Setup(us => us.GetByUserNameAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(() => new List<UserPositionWithIdRequest>());
        return this;
    }
}

public class UserSalaryRepoMock : Mock<IUserSalaryRepo>
{

    public UserSalaryRepoMock Setup()
    {
        this.Setup(u => u.GetAllAsync(It.IsAny<string>())).ReturnsAsync(() => new List<IUserSalaryRequest>());
        this.Setup(us => us.UpdateAsync(It.IsAny<UserSalaryWithIdRequest>()));
        this.Setup(us => us.AddAsync(It.IsAny<NewUserSalaryRequest>()));
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i > 0))).ReturnsAsync(() => new UserSalaryWithIdRequest());
        return this;
    }
}
public class UserTaskRepoMock : Mock<IUserTaskRepo>
{

    public UserTaskRepoMock Setup()
    {
        this.Setup(u => u.GetAllAsync(It.IsAny<string>())).ReturnsAsync(() => new List<IUserTaskRequest>());
        this.Setup(us => us.UpdateAsync(It.IsAny<UserTaskWithIdRequest>()));
        this.Setup(us => us.AddAsync(It.IsAny<NewUserTaskRequest>()));
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i > 0))).ReturnsAsync(() => new UserTaskWithIdRequest());
        return this;
    }
}