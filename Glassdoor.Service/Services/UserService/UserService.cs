using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities;
using Glassdoor.Model.UserModels;
using Glassdoor.Service.Extensions;
using Glassdoor.Service.Interfaces;

namespace Glassdoor.Service.Services.UserService;

public class UserService : IUserService
{
    private Repository<User> repository;
    public UserService(Repository<User> repository)
    {
        this.repository = repository;

    }
    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existuser = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"user is not found with id {id}");

        await repository.DeleteAsync(existuser);
        await repository.SaveChangesAsync();
        return true;
    }
    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync()
    {
        return MapperExtension.MapTo<UserViewModel>(repository.SelectAllAsEnumerable());
    }
    public async ValueTask<User> GetByIdAsync(long id)
    {
        var existuser = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"user is not found with id {id}");
        return existuser;
    }
    public async ValueTask<User> LoginAsync(string phone, string password)
    {
        var exitUser = Task.FromResult(repository.SelectAllAsQueryable().FirstOrDefault(user => user.Phone == phone && user.Password == password))
            ?? throw new Exception($"user is not found");

        return await exitUser;
    }
    public async ValueTask<UserViewModel> RegisterAsync(UserCreateModel user)
    {
        var createdUser = await repository.InsertAsync(MapperExtension.MapTo<User>(user));
        await repository.SaveChangesAsync();
        return MapperExtension.MapTo<UserViewModel>(createdUser);
    }
    public async ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user)
    {
        var existuser = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"user is not found with id {id}");

        existuser.FirstName = user.FirstName;
        existuser.LastName = user.LastName;
        existuser.Phone = user.Phone;
        existuser.Password = user.Password;
        existuser.Address = user.Address;

        var result = await repository.UpdateAsync(existuser);
        await repository.SaveChangesAsync();

        return MapperExtension.MapTo<UserViewModel>(result);
    }

}
