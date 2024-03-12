using Glassdoor.Domain.Entities.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Service.Interfaces;

public interface IJob
{
    Task<JobViewModel> CreateAsync(JobCreateModel model);
    Task<JobViewModel> UpdateAsync(long id, JobUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<JobViewModel> GetByIdAsync(long id);
    Task<IEnumerable<JobViewModel>> GetAllAsync();
}
