using Glassdoor.Domain.Entities;

namespace Glassdoor.Service.Interfaces;

public interface ICompanyService
{
    Task<Company> LogInAsync(string phone, string password);
    Task<Company> RegisterAsync(Company model);
    Task<Company> UpdateAsync(long id, Company model);
    Task<bool> DeleteAsync(long id);
    Task<Company> GetByIdAsync(long id);
    Task<IEnumerable<Company>> GetAllAsync();
}
