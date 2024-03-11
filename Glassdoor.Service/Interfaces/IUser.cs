using Glassdoor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Service.Interfaces;

public interface IUser
{
    Task<User> CreateAsync(User model);
    Task<User> UpdateAsync(long id, User model);
    Task<bool> DeleteAsync(long id);
    Task<User> GetByIdAsync(long id);
    Task<IEnumerable<User>> GetAllAsEnumerable();
    Task<IQueryable<User>> GetAllAsQueyable();
}
