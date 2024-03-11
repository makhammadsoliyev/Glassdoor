using Glassdoor.DataAccess.Contexts;
using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities;
using Glassdoor.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Service.Services;

public class AddressService : IAddress
{
    private GlassdoorDbContext context;
    private Repository<Address> repository;
    private List<Address> Addresss;
    public AddressService(GlassdoorDbContext context)
    {
        this.context = context;
        this.repository = new Repository<Address>(context);
        if (repository?.SelectAllAsEnumerable().ToList() is not null)
            this.Addresss = repository.SelectAllAsEnumerable().ToList();
        else
            this.Addresss = new List<Address>();
    }
    public async Task<Address> CreateAsync(Address model)
    {
        var existPhone = Addresss.FirstOrDefault(Address => Address.Id == model.Id)
            ?? throw new Exception($"This Address is always exists");

        await repository.InsertAsync(model);
        await repository.SaveChanges();
        return model;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exist = Addresss.FirstOrDefault(Address => Address.Id == id);
        if (exist is null)
            throw new Exception("This Address is not found");

        if (exist.IsDeleted)
            throw new Exception("This Address is not found");

        await repository.DeleteAsync(exist);
        await repository.SaveChanges();
        return true;
    }

    public Task<IEnumerable<Address>> GetAllAsEnumerable()
    {
        return Task.FromResult(repository.SelectAllAsEnumerable());
    }

    public Task<IQueryable<Address>> GetAllAsQueyable()
    {
        return Task.FromResult(repository.SelectAllAsQueryable());
    }

    public Task<Address> GetByIdAsync(long id)
    {
        var exist = Addresss.FirstOrDefault(Address => Address.Id == id)
            ?? throw new Exception($"This Address is with id {id} is not found");

        return Task.FromResult(exist);
    }

    public async Task<Address> UpdateAsync(long id, Address model)
    {
        var exist = Addresss.FirstOrDefault(Address => Address.Id == id);
        if (exist is null)
            throw new Exception("This Address is not found");

        exist.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(exist);
        await repository.SaveChanges();
        return model;
    }
}
