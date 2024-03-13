using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities;
using Glassdoor.Service.Extensions;
using Glassdoor.Service.Interfaces;

namespace Glassdoor.Service.Services;

public class JobService : IJobService
{
    private readonly IRepository<Job> repository;

    public JobService(IRepository<Job> repository)
    {
        this.repository = repository;
    }

    public async Task<Job> CreateAsync(Job model)
    {
        var result = await repository.InsertAsync(model.MapTo<Job>());
        await repository.SaveChangesAsync();

        return result.MapTo<Job>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exist = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"Job with id : {id} is not found");

        var result = await repository.DeleteAsync(exist);
        await repository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Job>> GetAllAsync()
    {
        return await Task.FromResult(repository.SelectAllAsEnumerable().MapTo<Job>());
    }

    public async Task<Job> GetByIdAsync(long id)
    {
        var existJob = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"Job with id : {id} is not found");

        return existJob.MapTo<Job>();
    }

    public async Task<Job> UpdateAsync(long id, Job model)
    {
        var existJob = await repository.SelectByIdAsync(id)
            ?? throw new Exception($"Job with id : {id} is not found");

        existJob.UpdatedAt = DateTime.UtcNow;
        existJob.Name = model.Name;
        existJob.Description = model.Description;
        existJob.Status = model.Status;
        existJob.SalaryRange = model.SalaryRange;

        await repository.UpdateAsync(existJob);
        await repository.SaveChangesAsync();

        return existJob.MapTo<Job>();
    }
}
