using Microsoft.EntityFrameworkCore;
using WebDevelopment.Common.Requests.Department;
using WebDevelopment.Common.Requests.Position;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Common.Requests.UserPosition;
using WebDevelopment.Domain.UserPosition;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repositories;

public class UserPositionRepository : IUserPositionRepository
{
    private readonly WebDevelopmentContext _context;

    public UserPositionRepository(WebDevelopmentContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserPositionWithIdRequest>> GetAll()
    {
        var result = await _context.UserPositions
            .Include(up => up.User)
            .Include(up => up.Position)
            .Include(up => up.Department).ToListAsync();
        return result.Select(Map);
    }

    public async Task<NewUserPositionRequest> Add(NewUserPositionRequest item)
    {
        var result = Map(item);
        var department = _context.Departments.First(d => item.Department !=null && string.Equals(d.Name, item.Department.Name));
        result.Department = department;
        _context.UserPositions.Add(result);
        await _context.SaveChangesAsync();
        return item;

    }

    public async Task<UserPositionWithIdRequest> Update(UserPositionWithIdRequest itemWithId)
    {
        var result = Map(itemWithId);
        var department = _context.Departments.First(d => itemWithId.Department != null && string.Equals(d.Name, itemWithId.Department.Name));
        result.Department = department;
        _context.UserPositions.Update(result);
        await _context.SaveChangesAsync();
        return itemWithId;
    }


    #region Mappers

    private static UserPositionWithIdRequest Map(UserPosition userPosition)
    {
        return new UserPositionWithIdRequest
        {
            Id = userPosition.Id,
            User = new UserWithIdRequest()
            {
                Id = userPosition!.User!.Id,
                FirstName = userPosition?.User?.FirstName,
                SecondName = userPosition?.User?.SecondName,
                UserEmail = userPosition?.User?.UserEmail
            },
            Position = new PositionWithIdRequest()
            {
                Id = userPosition!.Position!.Id,
                Name = userPosition?.Position?.Name,
                ShortName = userPosition?.Position?.ShortName
            },
            Department = new NewDepartmentRequest()
            {
                Name = userPosition?.Department?.Name
            },
            StartDate = userPosition?.StartDate,
            EndDate = userPosition?.EndDate
        };
    }

    private static UserPosition Map(NewUserPositionRequest userPosition)
    {
        return new UserPosition()
        {
            User = new User()
            {
                Id = userPosition!.User!.Id,
                FirstName = userPosition?.User?.FirstName,
                SecondName = userPosition?.User?.SecondName,
                UserEmail = userPosition?.User?.UserEmail
            },
            Position = new Position()
            {
                Id = userPosition!.Position!.Id,
                Name = userPosition?.Position?.Name,
                ShortName = userPosition?.Position?.ShortName
            },
            StartDate = DateTimeOffset.Now
        };
    }

    private static UserPosition Map(UserPositionWithIdRequest userPosition)
    {
        return new UserPosition
        {
            Id = userPosition.Id,
            User = new User()
            {
                Id = userPosition!.User!.Id,
                FirstName = userPosition?.User?.FirstName,
                SecondName = userPosition?.User?.SecondName,
                UserEmail = userPosition?.User?.UserEmail
            },
            Position = new Position()
            {
                Id = userPosition!.Position!.Id,
                Name = userPosition?.Position?.Name,
                ShortName = userPosition?.Position?.ShortName
            },
            StartDate = userPosition?.StartDate,
            EndDate = userPosition?.EndDate
        };
    }

    #endregion
}