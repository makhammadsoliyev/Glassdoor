using Glassdoor.Model.Companies;

namespace Glassdoor.Service.Interfaces;

public interface ICompany
{
    Task<CompanyViewModel> CreateAsync(CompanyCreateModel model);
    Task<CompanyViewModel> UpdateAsync(long id, CompanyUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<CompanyViewModel> GetByIdAsync(long id);
    Task<IEnumerable<CompanyViewModel>> GetAllAsync();
}
