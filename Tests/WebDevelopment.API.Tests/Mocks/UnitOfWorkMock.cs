using Moq;
using WebDevelopment.Common.Requests.Country;
using WebDevelopment.Common.Requests.Department;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Domain;
using WebDevelopment.Domain.IRepos;

namespace WebDevelopment.API.Tests.Mocks;

public class UnitOfWorkMock : Mock<IUnitOfWork>
{

    public UnitOfWorkMock Setup_UserRepo()
    {
        SetupGet(u => u.UserRepo).Returns(new UserRepoMock().Setup().Object);
        SetupGet(u => u.CountryRepo).Returns(new CountryRepoMock().Object);
        SetupGet(u => u.DepartmentRepo).Returns(new DepartmentRepoMock().Object);
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
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i > 0))).ReturnsAsync(() => new NewUserRequest());
        this.Setup(us => us.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(() => new NewUserRequest());
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
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i > 0))).ReturnsAsync(() => new NewCountryRequest());
        this.Setup(us => us.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new NewCountryRequest());
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
        this.Setup(us => us.GetByIdAsync(It.Is<int>(i => i > 0))).ReturnsAsync(() => new NewDepartmentRequest());
        this.Setup(us => us.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new NewDepartmentRequest());
        return this;
    }
}