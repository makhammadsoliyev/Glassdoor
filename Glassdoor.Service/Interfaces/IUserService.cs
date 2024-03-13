using Glassdoor.Domain.Entities;

namespace Glassdoor.Service.Interfaces;

public interface IUserService
{
    Task<User> RegisterAsync(User user);
    Task<bool> DeleteAsync(long id);
    Task<User> UpdateAsync(long id, User user);
    Task<User> LoginAsync(string phone, string password);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(long id);
    Task<IEnumerable<Application>> GetAllApplications(long id);
}
