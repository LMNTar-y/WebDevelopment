using WebDevelopment.Common.Requests.Department;
using WebDevelopment.Common.Requests.Position;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Common.Requests.UserPosition;
using WebDevelopment.Domain.IRepos;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repos;

public class UserPositionRepo : GenericRepository<UserPosition>, IUserPositionRepo
{
    private readonly GenericRepository<User> _user;
    private readonly GenericRepository<Position> _position;
    private readonly GenericRepository<Department> _department;

    public UserPositionRepo(WebDevelopmentContext context) : base(context)
    {
        _user = new GenericRepository<User>(context);
        _department = new GenericRepository<Department>(context);
        _position = new GenericRepository<Position>(context);
    }

    public new async Task<IEnumerable<IUserPositionRequest>> GetAllAsync(string includeProperties = "")
    {
        includeProperties += "User, Position, Department";
        var result = await base.GetAllAsync(includeProperties);
        return result.Select(Map);
    }

    public async Task<IEnumerable<IUserPositionRequest>> GetByUserNameAsync(string firstName, string lastName)
    {
        var result = (await GetAllAsync()).Where(x =>
                         !string.IsNullOrWhiteSpace(x?.User?.FirstName) &&
                         !string.IsNullOrWhiteSpace(x.User.SecondName) && x.User.FirstName.Contains(firstName) &&
                         x.User.SecondName.Contains(lastName)) ??
                     throw new ArgumentNullException(nameof(firstName),
                         $"UserPosition with firstName: \"{firstName}\" and lastName: \"{lastName}\" was not found");
        return result;
    }

    public new async Task<IUserPositionRequest> GetByIdAsync(object id)
    {
        var includeProperties = "User, Position, Department";
        var result = (await base.GetAllAsync(includeProperties)).Single(x => x.Id == (int)id);
        return Map(result);
    }

    public async Task<IUserPositionRequest> AddAsync(IUserPositionRequest entity)
    {
        var result = await Map((NewUserPositionRequest)entity);

        await base.AddAsync(result);
        return entity;
    }

public async Task<IUserPositionRequest> UpdateAsync(IUserPositionRequest entity)
    {
        var result = await Map((UserPositionWithIdRequest)entity);

        await UpdateAsync(result);
        return entity;
    }

    #region Mappers

    private static UserPositionWithIdRequest Map(UserPosition userPosition)
    {
        return new UserPositionWithIdRequest
        {
            Id = userPosition.Id,
            User = new UserWithIdRequest()
            {
                Id = userPosition.User!.Id,
                FirstName = userPosition.User.FirstName,
                SecondName = userPosition.User.SecondName,
                UserEmail = userPosition.User.UserEmail
            },
            Position = new PositionWithIdRequest()
            {
                Id = userPosition.Position!.Id,
                Name = userPosition.Position?.Name,
                ShortName = userPosition.Position?.ShortName
            },
            Department = new NewDepartmentRequest()
            {
                Name = userPosition.Department?.Name
            },
            StartDate = userPosition.StartDate,
            EndDate = userPosition.EndDate
        };
    }

    private async Task<UserPosition> Map(NewUserPositionRequest userPosition)
    {
        var user = await _user.GetByIdAsync(userPosition.User!.Id);
        var position = await _position.GetByIdAsync(userPosition.Position!.Id);
        var department =
            (await _department.GetAllAsync()).Single(x => string.Equals(x.Name, userPosition.Department!.Name));
        return new UserPosition()
        {
            User = user,
            Position = position,
            Department = department,
            StartDate = DateTimeOffset.Now
        };
    }

    private async Task<UserPosition> Map(UserPositionWithIdRequest userPosition)
    {
        var user = await _user.GetByIdAsync(userPosition!.User!.Id);
        var position = await _position.GetByIdAsync(userPosition!.Position!.Id);
        var department =
            (await _department.GetAllAsync()).Single(x => string.Equals(x.Name, userPosition!.Department!.Name));

        return new UserPosition
        {
            Id = userPosition.Id,
            User = user,
            Position = position,
            Department = department,
            StartDate = userPosition?.StartDate,
            EndDate = userPosition?.EndDate
        };
    }

    #endregion
}