using Glassdoor.Model.Applications;

namespace Glassdoor.Service.Interfaces;

public interface IApplicationService
{
    Task<ApplicationViewModel> CreateAsync(ApplicationCreateModel application);
    Task<bool> DeleteAsync(long id);
    Task<ApplicationViewModel> GetByIdAsyncAsync(long id);
    Task<IEnumerable<ApplicationViewModel>> GetAllAsync();
}
