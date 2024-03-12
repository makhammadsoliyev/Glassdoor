using Glassdoor.DataAccess.Contexts;
using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities.Companies;
using Glassdoor.Domain.Entities.Jobs;
using Glassdoor.Service.Extensions;
using Glassdoor.Service.Interfaces;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Service.Services;

public class JobService : IJob
{
    private Repository<Job> repository;
    private GlassdoorDbContext context;
    private List<Job> jobs;
    public JobService(GlassdoorDbContext context)
    {
        this.context = context;
        this.repository = new Repository<Job>(context);
        this.jobs = repository.SelectAllAsEnumerable().ToList();
    }
    public async Task<JobViewModel> CreateAsync(JobCreateModel model)
    {
        jobs.Add(model.MapTo<Job>());
        await repository.InsertAsync(repository.MapTo<Job>());
        await repository.SaveChanges();  
        return await Task.FromResult(model.MapTo<JobViewModel>());
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exist = jobs.FirstOrDefault(job => job.Id == id);
        if (exist is null)
            throw new Exception($"Job with id : {id} is not found");

        jobs.Remove(exist);
        await repository.DeleteAsync(exist);
        return true;
    }

    public async Task<IEnumerable<JobViewModel>> GetAllAsync()
    {
        return await Task.FromResult(repository.SelectAllAsEnumerable().MapTo<JobViewModel>());
    }

    public async Task<JobViewModel> GetByIdAsync(long id)
    {
        var exist = jobs.FirstOrDefault(job => job.Id == id);
        if (exist is null)
            throw new Exception($"Job with id : {id} is not found");

        return await Task.FromResult(exist.MapTo<JobViewModel>());
    }

    public async Task<JobViewModel> UpdateAsync(long id, JobUpdateModel model)
    {
        var exist = jobs.FirstOrDefault(job => job.Id == id);
        if (exist is null)
            throw new Exception($"Job with id : {id} is not found");

        exist.UpdatedAt = DateTime.UtcNow;
        exist.Name = model.Name;
        exist.Description = model.Description;
        exist.Status = model.Status;
        exist.SalarRange = model.SalarRange;
        await repository.UpdateAsync(exist);
        await repository.SaveChanges();
        return await Task.FromResult(exist.MapTo<JobViewModel>());
    }
}
