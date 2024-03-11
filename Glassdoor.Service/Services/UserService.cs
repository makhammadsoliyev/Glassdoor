using Glassdoor.DataAccess.Contexts;
using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities;
using Glassdoor.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Service.Services;

public class UserService : IUser
{
    private GlassdoorDbContext context;
    private Repository<User> repository;
    private List<User> users;
    public UserService(GlassdoorDbContext context)
    {
        this.context = context;
        this.repository = new Repository<User>(context);
        if(repository?.SelectAllAsEnumerable().ToList() is not null)
            this.users = repository.SelectAllAsEnumerable().ToList();
        else
            this.users = new List<User>();
    }
    public async Task<User> CreateAsync(User model)
    {
        var existPhone = users.FirstOrDefault(user => user.PhoneNumber == model.PhoneNumber)
            ?? throw new Exception($"This user with phone number {model.PhoneNumber} is always exists");

        var existEmail = users.FirstOrDefault(user => user.Email == model.Email)
            ?? throw new Exception($"This user with Email {model.Email} is always exists");
        await repository.InsertAsync(model);
        await repository.SaveChanges();
        return model;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exist = users.FirstOrDefault(user => user.Id == id);
        if (exist is null)
            throw new Exception("This user is not found");

        if (exist.IsDeleted)
            throw new Exception("This user is not found");

        await repository.DeleteAsync(exist);
        await repository.SaveChanges();
        return true;
    }

    public Task<IEnumerable<User>> GetAllAsEnumerable()
    {
        return Task.FromResult(repository.SelectAllAsEnumerable());
    }

    public Task<IQueryable<User>> GetAllAsQueyable()
    {
        return Task.FromResult(repository.SelectAllAsQueryable());
    }

    public Task<User> GetByIdAsync(long id)
    {
        var exist = users.FirstOrDefault(user => user.Id == id)
            ?? throw new Exception($"This user is with id {id} is not found");

        return Task.FromResult(exist);
    }

    public async Task<User> UpdateAsync(long id, User model)
    {
        var exist = users.FirstOrDefault(user => user.Id == id);
        if (exist is null)
            throw new Exception("This user is not found");

        exist.UpdatedAt = DateTime.UtcNow;
        exist.FirstName = model.FirstName;
        exist.LastName = model.LastName;
        exist.Email = model.Email;
        exist.Password = model.Password;
        exist.PhoneNumber = model.PhoneNumber;
        await repository.UpdateAsync(exist);
        await repository.SaveChanges();
        return model;
    }
}
