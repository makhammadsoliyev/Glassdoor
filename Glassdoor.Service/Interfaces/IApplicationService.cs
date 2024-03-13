using Glassdoor.Domain.Entities;

namespace Glassdoor.Service.Interfaces;

public interface IApplicationService
{
    Task<Application> CreateAsync(Application application);
    Task<bool> DeleteAsync(long id);
    Task<Application> GetByIdAsyncAsync(long id);
    Task<IEnumerable<Application>> GetAllAsync();
}
