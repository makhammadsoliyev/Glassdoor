using Glassdoor.Model.Jobs;

namespace Glassdoor.Service.Interfaces;

public interface IJobService
{
    Task<JobViewModel> CreateAsync(JobCreateModel model);
    Task<JobViewModel> UpdateAsync(long id, JobUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<JobViewModel> GetByIdAsync(long id);
    Task<IEnumerable<JobViewModel>> GetAllAsync();
}
