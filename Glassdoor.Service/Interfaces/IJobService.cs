using Glassdoor.Domain.Entities;

namespace Glassdoor.Service.Interfaces;

public interface IJobService
{
    Task<Job> CreateAsync(Job model);
    Task<Job> UpdateAsync(long id, Job model);
    Task<bool> DeleteAsync(long id);
    Task<Job> GetByIdAsync(long id);
    Task<IEnumerable<Job>> GetAllAsync();
}
