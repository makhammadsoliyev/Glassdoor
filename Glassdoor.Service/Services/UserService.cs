using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities;
using Glassdoor.Model.Applications;
using Glassdoor.Model.UserModels;
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

    public async Task<IEnumerable<ApplicationViewModel>> GetAllApplications(long id)
    {
        var users = repository.SelectAllAsQueryable();
        var existUser = users
            .Where(user => user.Id == id)
            .FirstOrDefault()
            ?? throw new Exception($"user is not found with id {id}");

        return await Task.FromResult(existUser.Applications.MapTo<ApplicationViewModel>());
    }

    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        return await Task.FromResult(repository.SelectAllAsEnumerable().MapTo<UserViewModel>());
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        var existUser = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"user is not found with id {id}");

        return existUser.MapTo<UserViewModel>();
    }

    public async Task<UserViewModel> LoginAsync(string phone, string password)
    {
        var exitUser = await repository.SelectAllAsQueryable().FirstOrDefaultAsync(user => user.Phone == phone && user.Password == password)
            ?? throw new Exception($"user is not found");

        return exitUser.MapTo<UserViewModel>();
    }

    public async Task<UserViewModel> RegisterAsync(UserCreateModel user)
    {
        var createdUser = await repository.InsertAsync(user.MapTo<User>());
        await repository.SaveChangesAsync();

        return createdUser.MapTo<UserViewModel>();
    }

    public async Task<UserViewModel> UpdateAsync(long id, UserUpdateModel user)
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

        return result.MapTo<UserViewModel>();
    }
}
