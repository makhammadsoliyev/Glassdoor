using Glassdoor.Domain.Entities.Companies;
using Glassdoor.Model.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Service.Interfaces;

public interface ICompany
{
    Task<CompanyViewModel> CreateAsync(CompanyCreateModel model);
    Task<CompanyViewModel> UpdateAsync(long id, CompanyUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<CompanyViewModel> GetByIdAsync(long id);
    Task<IEnumerable<CompanyViewModel>> GetAllAsync();
}
