using Glassdoor.Domain.Entities;
using Glassdoor.Model.UserModels;

namespace Glassdoor.Service.Interfaces;

public interface IUserService
{
    public ValueTask<UserViewModel> RegisterAsync(UserCreateModel user);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user);
    public ValueTask<User> LoginAsync(string phone, string password);
    public ValueTask<IEnumerable<UserViewModel>> GetAllAsync();
    public ValueTask<User> GetByIdAsync(long id);
    //public ValueTask<IEnumerable<ApplicationViewModel>> GetAllAplications();
}
