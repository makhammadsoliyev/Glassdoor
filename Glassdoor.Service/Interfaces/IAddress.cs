using Glassdoor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Service.Interfaces;

public interface IAddress
{
    Task<Address> CreateAsync(Address model);
    Task<Address> UpdateAsync(long id, Address model);
    Task<bool> DeleteAsync(long id);
    Task<Address> GetByIdAsync(long id);
    Task<IEnumerable<Address>> GetAllAsEnumerable();
    Task<IQueryable<Address>> GetAllAsQueyable();
}
