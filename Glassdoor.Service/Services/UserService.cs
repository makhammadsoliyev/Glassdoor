using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities;
using Glassdoor.Service.Extensions;
using Glassdoor.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Glassdoor.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> repository;

    public UserService(IRepository<User> repository)
    {
        this.repository = repository;

    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"user is not found with id {id}");

        await repository.DeleteAsync(existUser);
        await repository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Application>> GetAllApplications(long id)
    {
        var users = repository.SelectAllAsQueryable();
        var existUser = users
            .Where(user => user.Id == id)
            .FirstOrDefault()
            ?? throw new Exception($"user is not found with id {id}");

        return await Task.FromResult(existUser.Applications.MapTo<Application>());
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await Task.FromResult(repository.SelectAllAsEnumerable().MapTo<User>());
    }

    public async Task<User> GetByIdAsync(long id)
    {
        var existUser = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"user is not found with id {id}");

        return existUser.MapTo<User>();
    }

    public async Task<User> LoginAsync(string phone, string password)
    {
        var exitUser = await repository.SelectAllAsQueryable().FirstOrDefaultAsync(user => user.Phone == phone && user.Password == password)
            ?? throw new Exception($"user is not found");

        return exitUser.MapTo<User>();
    }

    public async Task<User> RegisterAsync(User user)
    {
        var createdUser = await repository.InsertAsync(user.MapTo<User>());
        await repository.SaveChangesAsync();

        return createdUser.MapTo<User>();
    }

    public async Task<User> UpdateAsync(long id, User user)
    {
        var existUser = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"user is not found with id {id}");

        existUser.FirstName = user.FirstName;
        existUser.LastName = user.LastName;
        existUser.Password = user.Password;
        existUser.Address = user.Address;
        existUser.Phone = user.Phone;

        var result = await repository.UpdateAsync(existUser);
        await repository.SaveChangesAsync();

        return result.MapTo<User>();
    }
}
