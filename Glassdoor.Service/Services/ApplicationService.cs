using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities;
using Glassdoor.Service.Extensions;
using Glassdoor.Service.Interfaces;

namespace Glassdoor.Service.Services;

public class ApplicationService : IApplicationService
{
    private readonly IRepository<Application> repository;
    private readonly IJobService jobService;

    public ApplicationService(IRepository<Application> repository, IJobService jobService)
    {
        this.repository = repository;
        this.jobService = jobService;
    }

    public async Task<Application> CreateAsync(Application application)
    {
        var createdApplication = await repository.InsertAsync(application.MapTo<Application>());
        await repository.SaveChangesAsync();

        return createdApplication.MapTo<Application>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var application = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"Application was not found with this id: {id}");

        await repository.DeleteAsync(application);
        await repository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Application>> GetAllAsync()
    {
        var applications = repository.SelectAllAsEnumerable().MapTo<Application>();
        foreach (var application in applications)
        {
            application.Job = await jobService.GetByIdAsync(application.JobId);
        }
        return await Task.FromResult(applications);
    }

    public async Task<Application> GetByIdAsyncAsync(long id)
    {
        var application = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"Application was not found with this id: {id}");

        application.Job = await jobService.GetByIdAsync(application.JobId);

        return application.MapTo<Application>();
    }
}
