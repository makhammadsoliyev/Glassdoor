using Glassdoor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Service.Interfaces;

public interface IResume
{
    Task<Resume> CreateAsync(Resume model);
    Task<Resume> UpdateAsync(long id, Resume model);
    Task<bool> DeleteAsync(long id);
    Task<Resume> GetByIdAsync(long id);
    Task<IEnumerable<Resume>> GetAllAsEnumerable();
    Task<IQueryable<Resume>> GetAllAsQueyable();
}
