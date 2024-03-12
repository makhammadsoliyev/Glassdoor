using Glassdoor.DataAccess.Contexts;
using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities.Companies;
using Glassdoor.Service.Extensions;
using Glassdoor.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Service.Services;

public class CompanyService : ICompany
{
    private Repository<Company> repository;
    private GlassdoorDbContext context;
    private List<Company> companies;
    public CompanyService(GlassdoorDbContext context)
    {
        this.context = context;
        this.repository = new Repository<Company>(context);
        this.companies = repository.SelectAllAsEnumerable().ToList();
    }
    public async Task<CompanyViewModel> CreateAsync(CompanyCreateModel model)
    {
        var exist = companies.FirstOrDefault(company => company.Phone == model.Phone)
            ?? throw new Exception($"Company with Phone {model.Phone} is always exists");

        companies.Add(model.MapTo<Company>());
        await repository.InsertAsync(model.MapTo<Company>());
        await repository.SaveChanges();
        return await Task.FromResult(model.MapTo<CompanyViewModel>());
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exist = companies.FirstOrDefault(company => company.Id == id);
        if (exist is null)
            throw new Exception($"Company with id: {id} is not Found!");

        companies.Remove(exist);
        await repository.DeleteAsync(exist);
        await repository.SaveChanges();
        return true;
    }

    public async Task<IEnumerable<CompanyViewModel>> GetAllAsync()
    {
        return await Task.FromResult(companies.MapTo<CompanyViewModel>());
    }

    public async Task<CompanyViewModel> GetByIdAsync(long id)
    {
        var exist = companies.FirstOrDefault(company => company.Id == id);
        if (exist is null)
            throw new Exception($"Company with id: {id} is not Found!");

        return await Task.FromResult(exist.MapTo<CompanyViewModel>());
    }

    public async Task<CompanyViewModel> UpdateAsync(long id, CompanyUpdateModel model)
    {
        var exist = companies.FirstOrDefault(company => company.Id == id);
        if (exist is null)
            throw new Exception($"Company with id: {id} is not Found!");

        exist.Name = model.Name;
        exist.Phone = model.Phone;
        exist.Address = model.Address;
        exist.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(exist);
        await repository.SaveChanges();
        return await Task.FromResult(exist.MapTo<CompanyViewModel>());
    }
}
