using Glassdoor.Domain.Entities;
using Glassdoor.Model.Applications;
using Glassdoor.Model.UserModels;

namespace Glassdoor.Service.Interfaces;

public interface IUserService
{
    Task<UserViewModel> RegisterAsync(UserCreateModel user);
    Task<bool> DeleteAsync(long id);
    Task<UserViewModel> UpdateAsync(long id, UserUpdateModel user);
    Task<UserViewModel> LoginAsync(string phone, string password);
    Task<IEnumerable<UserViewModel>> GetAllAsync();
    Task<UserViewModel> GetByIdAsync(long id);
    Task<IEnumerable<ApplicationViewModel>> GetAllApplications(long id);
}
