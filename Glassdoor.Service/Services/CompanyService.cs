using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities;
using Glassdoor.Service.Extensions;
using Glassdoor.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Glassdoor.Service.Services;

public class CompanyService : ICompanyService
{
    private readonly IRepository<Company> repository;

    public CompanyService(IRepository<Company> repository)
    {
        this.repository = repository;
    }

    public async Task<Company> RegisterAsync(Company model)
    {
        var existCompany = await repository.SelectAllAsQueryable().FirstOrDefaultAsync(company => company.Phone == model.Phone);
        if (existCompany is not null)
            throw new Exception($"Company with Phone {model.Phone} is always exists");

        await repository.InsertAsync(model.MapTo<Company>());
        await repository.SaveChangesAsync();

        return model.MapTo<Company>();
    }

    public async Task<Company> LogInAsync(string phone, string password)
    {
        var existCompany = await repository.SelectAllAsQueryable().FirstOrDefaultAsync(company => company.Phone == phone && company.Password == password)
            ?? throw new Exception("Company was not found");

        return existCompany.MapTo<Company>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existCompany = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"Company with id: {id} is not Found!");

        await repository.DeleteAsync(existCompany);
        await repository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await Task.FromResult(repository.SelectAllAsEnumerable().MapTo<Company>());
    }

    public async Task<Company> GetByIdAsync(long id)
    {
        var existCompany = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"Company with id: {id} is not Found!");

        return existCompany.MapTo<Company>();
    }

    public async Task<Company> UpdateAsync(long id, Company model)
    {
        var existCompany = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"Company with id: {id} is not Found!");

        existCompany.Name = model.Name;
        existCompany.Phone = model.Phone;
        existCompany.Address = model.Address;
        existCompany.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(existCompany);
        await repository.SaveChangesAsync();

        return existCompany.MapTo<Company>();
    }
}
