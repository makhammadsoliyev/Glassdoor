using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities;
using Glassdoor.Model.Applications;
using Glassdoor.Service.Extensions;
using Glassdoor.Service.Interfaces;

namespace Glassdoor.Service.Services;

public class ApplicationService : IApplicationService
{
    private readonly IRepository<Application> repository;

    public ApplicationService(IRepository<Application> repository)
    {
        this.repository = repository;
    }

    public async Task<ApplicationViewModel> CreateAsync(ApplicationCreateModel application)
    {
        var createdApplication = await repository.InsertAsync(application.MapTo<Application>());
        await repository.SaveChangesAsync();

        return createdApplication.MapTo<ApplicationViewModel>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var application = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"Application was not found with this id: {id}");

        await repository.DeleteAsync(application);
        await repository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<ApplicationViewModel>> GetAllAsync()
    {
        var applications = repository.SelectAllAsEnumerable().MapTo<ApplicationViewModel>();

        return await Task.FromResult(applications);
    }

    public async Task<ApplicationViewModel> GetByIdAsyncAsync(long id)
    {
        var application = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"Application was not found with this id: {id}");

        return application.MapTo<ApplicationViewModel>();
    }
}
