using WebDevelopment.Common.Requests.UserSalary;

namespace WebDevelopment.Domain.UserSalary.Services;

public class UserSalaryService : IUserSalaryService
{
    private readonly IUserSalaryRepository _userSalaryRepository;

    public UserSalaryService(IUserSalaryRepository userSalaryRepository)
    {
        _userSalaryRepository = userSalaryRepository;
    }

    public async Task<IEnumerable<UserSalaryWithIdRequest>> GetAllAsync()
    {
        var result = await _userSalaryRepository.GetAll();
        return result;
    }

    public async Task<UserSalaryWithIdRequest> GetById(int id)
    {
        var result = (await _userSalaryRepository.GetAll()).First(x => x.Id == id);
        return result;
    }
    
    public async Task<bool> AddNewUserSalaryAsync(NewUserSalaryRequest request)
    {
        await _userSalaryRepository.Add(request);
        return true;
    }

    public async Task<bool> UpdateUserSalaryAsync(UserSalaryWithIdRequest requestWithId)
    {
        await _userSalaryRepository.Update(requestWithId);
        return true;
    }
}